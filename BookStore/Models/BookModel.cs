
using System.ComponentModel.DataAnnotations;
using BookStore.Helpers;

namespace BookStore.Models
{
#nullable disable
    public class BookModel
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required]
        public string Name { get; set; }
        [CustomValidationAttributes]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Category { get; set; }
        [Display(Name ="Choose Language")]
        public int languageId { get; set; }     

        public string language { get; set; }
        [Required]
        public int TotalPages { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        //public bool IsActive { get; set; }

        [Display(Name = "Choose Cover Image for Books")]
        [Required]
        public IFormFile CoverImage { get; set; }

        public string CoverImageURL { get; set; }


        [Display(Name = "Choose Gallery Image for your books")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }


        public List<GalleryModel> Gallery { get; set; }

    }
}
