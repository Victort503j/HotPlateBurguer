using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.EN.Payments
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodID { get; set; }

        [ForeignKey("UserTable")]
        public int UserTableID { get; set; }

        [Required]
        [StringLength(50)]
        public PaymentMethodType PaymentType { get; set; }

        [Required]
        [StringLength(100)]
        public string Token { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        public userTable UserTable { get; set; }

        public enum PaymentMethodType
        {
            PayPal = 1,
            Tarjeta = 2,
        }
    }
}
