using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Models
{
    [Table("AccountDTO")]
    public class AccountDTO
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public float Money { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]

        public bool IsBlocked { get; set; }

        public int User_id { get; set; }
        [ForeignKey("User_id")]
        public User? User { get; set; }

        public ICollection<FixedTermDepositDTO>? FixedTermDeposit { get; set; }
    }
}