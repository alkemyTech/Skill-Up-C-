using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Entities
{
    [Table("Account")]
    public class Account
    {
        public Account()
        {
            Transaction = new HashSet<Transaction>();
            FixedTermDeposit = new HashSet<FixedTermDeposit>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A Creation Date is Required")]
        //si solo se requiere year month day [StringLength(10)], si quieres tambien los sgundos borra el display format
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        [MaxLength(255)]
        [Required(ErrorMessage = "An Amount its Required")]
        public float Money { get; set; } = 0f;


        [Required(ErrorMessage = "Set The Account Status")]
        public bool IsBlocked { get; set; }=false;


        [Required(ErrorMessage = "User Id is Required")]
        public int User_id { get; set; }=0;
        [ForeignKey("User_id")]
        public User? User { get; set; }

        public ICollection<FixedTermDeposit> FixedTermDeposit { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
    }
}