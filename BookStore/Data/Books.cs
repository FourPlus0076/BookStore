namespace BookStore.Data
{
#nullable disable
    public class Books
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Category { get; set; }
        public int languageId { get; set; }
        public int TotalPages { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }       
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set;}
        public bool IsActive { get; set; }

        public string CoverImageURL { get; set; }

        public Language language { get; set; }

        public ICollection<BookGallery> BookGallery { get; set;}

    }
}
