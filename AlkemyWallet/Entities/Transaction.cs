using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{
    [Table("Transaction")]
    public class Transaction
    {

        [Key]
        public int Transaction_id { get; set; }

        [MaxLength (100)]
        [Required(ErrorMessage = "An Amount its Required")]
        public int Amount { get; set; }=0;

        [MaxLength(100)]
        [Required(ErrorMessage = "Enter a Payment Details")]
        public string Concept { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Date is Required")]
        //si solo se requiere year month day [StringLength(10)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Choose Transaction Type")]
        public string Type { get; set; }= string.Empty;


        [Required(ErrorMessage = "User Id is Required")]
        public int User_id { get; set; } = 0;

        [ForeignKey("User_id")]
        public User? User { get; set; }
        [Required(ErrorMessage = "Account Id is Required")]

        public int To_Account { get; set; } = 0;

        public int Account_id { get; set; } = 0;
        [ForeignKey("Account_id")]
        public Account? Account { get; set; }

       
    }
}
