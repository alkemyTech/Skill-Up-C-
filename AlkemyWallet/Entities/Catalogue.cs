using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{

    [Table("Catalogue")]
    public class Catalogue
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [MinLength(2)]
        [Required(ErrorMessage = "Product name or description is required")]
        public string Product_description { get; set; } = string.Empty;

        [MaxLength(255)]
        [Required(ErrorMessage = "A Valid Image is Required,The Name its too Long or the File its Too Big")]
        public string Image { get; set; } = string.Empty;

        [Range(0, 9999)]
        [Required(ErrorMessage = "The Amount of Points are Required")]
        public int Points { get; set; } = 0;
    }
}
