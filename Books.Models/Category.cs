using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class Category
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DisplayOrder { get; set; } // This is used to determine which category is displayed first when there are multiple categories
    }
}