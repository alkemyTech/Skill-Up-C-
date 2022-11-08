using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{
    [Table("Role")]
    public class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name should have at least 1 letter")]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        [Required(ErrorMessage = "The Role its Required")]
        public string Description { get; set; } = string.Empty;

        public ICollection<User> User { get; set; }
    }
}
