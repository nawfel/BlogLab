using BlogLab.Models;
using BlogLab.Models.Blog;
using BlogLab.Models.BlogComment;
using BlogLab.Models.Photo;
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
    public class BlogCommentRepository : IBlogCommentRepository
    {
        private readonly IConfiguration _config;

        public BlogCommentRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> DeleteAsync(int blogId)
        {
            int affectedRows = 0;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                affectedRows = await connection.ExecuteAsync("BlogComment_Delete",
                    new { blogId = blogId },
                    commandType: System.Data.CommandType.StoredProcedure);

            }
            return affectedRows;
        }

        public async Task<List<BlogComment>> GetAllAsync(int blogId)
        {
            IEnumerable<BlogComment> blogComments;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                blogComments = await connection.QueryAsync<BlogComment>("BlogComment_GetAll",
                    new { BlogId = blogId },
                    commandType: System.Data.CommandType.StoredProcedure);

            }
            return blogComments.ToList();
        }



        public async Task<BlogComment> GetAsync(int blogId)
        {
            BlogComment blogComment;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                blogComment = await connection.QueryFirstOrDefaultAsync<BlogComment>(
                    "BlogComment_Get",
                    new { photoId = blogId },
                    commandType: System.Data.CommandType.StoredProcedure);

            }
            return blogComment;
        }

        public async Task<BlogComment> UpsertAsync(BlogCommentCreate blogCreate, int applicationUserId)
        {
            var dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("BlogCommentId", typeof(int));
            dataTable.Columns.Add("ParentBlogCommentId", typeof(int));
            dataTable.Columns.Add("BlogId", typeof(int));
            dataTable.Columns.Add("Content", typeof(string));

            dataTable.Rows.Add(
                blogCreate.BlogCommentId,
                blogCreate.ParentBlogCommentId,
                blogCreate.BlogId,
                blogCreate.Content);

            int? newBlogCommentId;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                newBlogCommentId = await connection.ExecuteScalarAsync<int?>(
                    "BlogComment_Upsert",
                    new { BlogCreate = dataTable.AsTableValuedParameter("dbo.BlogCommentType") },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
            newBlogCommentId = newBlogCommentId??blogCreate.BlogCommentId;
            BlogComment blogComment = await GetAsync(newBlogCommentId.Value);
            return blogComment;
        }

    }
}
