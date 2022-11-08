using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Models
{

    public class UserDTO
    {
        [Required(ErrorMessage = "el campo es requerido")]
        public string First_name { get; set; }= string.Empty;

        [Required(ErrorMessage = "el campo es requerido")]
        public string Last_name { get; set; } = string.Empty;

        [Required(ErrorMessage = "el campo es requerido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "el campo es requerido")]
        public int Rol_id { get; set; }
    }
    public class UserByIdDTO
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public string First_name { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public string Last_name { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public int Points { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public int Rol_id { get; set; }

        [ForeignKey("Rol_id")]
        public Role Role { get; set; }

    }
    public class UserForCreatoionDto
    {
            [Required(ErrorMessage = "el campo es requerido")]
            public string First_name { get; set; }

            [Required(ErrorMessage = "el campo es requerido")]
            public string Last_name { get; set; }

            [Required(ErrorMessage = "el campo es requerido")]
            public string Email { get; set; }

            [Required(ErrorMessage = "el campo es requerido")]
            public string Password { get; set; }


            [Required(ErrorMessage = "el campo es requerido")]
            public int Points { get; set; }


            [Required(ErrorMessage = "el campo es requerido")]
            public int Rol_id { get; set; }

            [ForeignKey("Rol_id")]
            public Role Role { get; set; }

    
    }
    public class UserForUpdateDto
    {
        [StringLength(100, MinimumLength = 2)]
        public string First_name { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string Last_name { get; set; }

        public int Rol_id { get; set; }
    }
}
