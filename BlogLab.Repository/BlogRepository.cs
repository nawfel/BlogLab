﻿using BlogLab.Models.Blog;
using BlogLab.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogLab.Repository
{
    public class BlogRepository : IBlogRepository
    {

        private readonly IConfiguration _config;

        public BlogRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> DeleteAsync(int blogId)
        {
            int affectedRows = 0;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                affectedRows = await connection.ExecuteAsync("Blog_Delete",
                    new { BlogId = blogId },
                    commandType: System.Data.CommandType.StoredProcedure);

            }
            return affectedRows;
        }

        public async Task<PageResults<Blog>> GetAllAsync(BlogPaging blogPaging)
        {
           var results = new PageResults<Blog>();

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                using (var multi = await connection.QueryMultipleAsync(
                    "Blog_All",
                    new
                    {
                        Offset = (blogPaging.Page - 1) * blogPaging.PageSize,
                        PageSize = blogPaging.PageSize,
                    },
                    commandType: System.Data.CommandType.StoredProcedure))
                {
                    results.Items = multi.Read<Blog>();
                    results.TotalCount=multi.ReadFirst<int>();
                }
            }
            return results;
        }

        public async Task<List<Blog>> GetAllByUserAsync(int applicationUserId)
        {
            IEnumerable<Blog> blogs;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                blogs = await connection.QueryAsync<Blog>("Blog_GetByUserId",
                    new { ApplicationUserId = applicationUserId },
                    commandType: System.Data.CommandType.StoredProcedure);

            }
            return blogs.ToList();
        }

        public async Task<List<Blog>> GetAllFamousAsync()
        {
            IEnumerable<Blog> famousBlogs;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                famousBlogs = await connection.QueryAsync<Blog>("Blog_GetAllFamous",
                    commandType: System.Data.CommandType.StoredProcedure);

            }
            return famousBlogs.ToList();
        }

        public async Task<Blog> GetAsync(int blogId)
        {
            Blog blog;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                blog = await connection.QueryFirstOrDefaultAsync<Blog>(
                    "Photo_Get",
                    new { blogId = blogId },
                    commandType: System.Data.CommandType.StoredProcedure);

            }
            return blog;
        }

        public async Task<Blog> UpsertAsync(BlogCreate blogCreate, int applicationUserId)
        {
            var dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("BlogId", typeof(string));
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("Content", typeof(string));
            dataTable.Columns.Add("PhotoId", typeof(string));

            dataTable.Rows.Add(blogCreate.BlogId, blogCreate.Title, blogCreate.Content,blogCreate.PhotoId) ;

            int? newBlogId;
            
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                newBlogId = await connection.ExecuteScalarAsync<int?>(
                    "Blog_Upsert",
                    new { Blog = dataTable.AsTableValuedParameter("dbo.BlogType"), ApplicationUserId = applicationUserId },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
            newBlogId = newBlogId ?? blogCreate.BlogId;
            Blog blog =await GetAsync(newBlogId.Value);
            return blog;

        }

    }
}