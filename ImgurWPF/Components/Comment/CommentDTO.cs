namespace ImgurWPF.Components.Comment
{
    public class CommentDTO
    {
        public CommentDTO(long imageId, string comment, string author, long parentId, long id)
        {
            ImageId = imageId;
            Comment = comment;
            Author = author;
            ParentId = parentId;
            Id = id;
        }

        public long ImageId { get; set; }
        public string Comment { get; set; }
        public string Author { get; set; }
        public long ParentId { get; set; }
        public long Id { get; set; }
    }
}