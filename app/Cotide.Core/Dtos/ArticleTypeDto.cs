namespace Cotide.Domain.Dtos
{
    public class ArticleTypeDto
    {
        public int Id { get; set; }


        public string TypeName { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public bool IsShow { get; set; }
    }
}
