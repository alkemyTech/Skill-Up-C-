using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Core.Models
{

   
    public class CatalogueDTO
    {       
        public string Product_description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int Points { get; set; }
    }

    public class CatalogueForCreationDTO
    {
        [Required(ErrorMessage = "El producto es requerido")]
        [StringLength(100, MinimumLength = 2)]
        public string Product_description { get; set; }        
        [Required(ErrorMessage = "La Imagen es requerida")]
        public IFormFile ImageFile { get; set; }
        [Required(ErrorMessage = "Los puntos son un campo requerido.")]
        [Range(0, 9999)]
        public int Points { get; set; }
    }

    public class CatalogueForUpdateDTO
    {
        
        [StringLength(100, MinimumLength = 2)]
        public string? Product_description { get; set; }        
        public IFormFile? ImageFile { get; set; }     
        [Range(0, 9999)]
        public int? Points { get; set; }
    }
}
