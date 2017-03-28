namespace Cotide.Domain.Dtos
{
    public class ArticleTagDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string TagName { get; set; }

        public bool IsShow { get; set; }

        public int UserId { get; set; }
    }
}
