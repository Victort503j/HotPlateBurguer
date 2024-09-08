using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.EN
{
    public class userTable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [NotMapped]
        public int Top_Aux { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Confirmar Password")]
        [StringLength(32, ErrorMessage = "Password debe estar entre 5 a 32 caracteres")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password y comfirmar password deben " +
           "ser iguales")]
        [Display(Name = "Confirmar Password")]
        public string ConfirmPassword_aux { get; set; }
        public byte Status { get; set; }
        public enum Estatus_Usuario
        {
            ACTIVE = 1,
            INACTIVE = 2
        }
    }
}
