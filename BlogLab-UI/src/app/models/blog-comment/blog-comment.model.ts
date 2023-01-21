export class BlogComment{

    constructor(
       public blogCommentId :number,
       public blogId :number,
       public content : string,
       public username : string,
       public publishDate:Date,
       public updateDate : Date,
       public applicationUserId :number,
       public parentBlogCommentId? :number
      
    ){}
}