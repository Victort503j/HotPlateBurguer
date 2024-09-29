using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.EN.Payments
{
    public class PaymentDetail
    {
        [Key]
        public int PaymentID { get; set; }

        [ForeignKey("Transaction")]
        public int TransactionID { get; set; }

        [Required]
        [StringLength(50)]
        public string PayerID { get; set; }

        [Required]
        [StringLength(100)]
        public string Token { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentStatus { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }

        public Transaction Transaction { get; set; }
    }
}
