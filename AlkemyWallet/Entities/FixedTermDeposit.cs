using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{

    [Table("FixedTermDeposit")]
    public class FixedTermDeposit
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "User Id is Required")]
        public int User_id { get; set; } = 0;
        [ForeignKey("User_id")]
        public User? User { get; set; }


        [Required(ErrorMessage = "Account Id is Required")]
        public int Account_id { get; set; } = 0;
        [ForeignKey("Account_id")]
        public Account? Account { get; set; }


        [MaxLength(255)]
        [Required(ErrorMessage = "An Amount its Required")]
        public float Amount { get; set; } = 0;

        [Required(ErrorMessage = "A Creation Date is Required")]
        //si solo se requiere year month day [StringLength(10)], si quieres tambien los sgundos borra el display format
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Creation_date { get; set; }

        [Required(ErrorMessage = "A Closing Date is Required")]
        //si solo se requiere year month day [StringLength(10)] , si quieres tambien los sgundos borra el display format
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Closing_date { get; set; }

    }
}
