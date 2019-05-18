namespace Outsourcing.Data.Models
{
    public class BlogImageMappings : BaseEntity
    {
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsMainPicture { get; set; }
        public int BlogId { get; set; }
        public int BlogImageId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual BlogImages BlogImage { get; set; }
    }
}
