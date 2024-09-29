using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.EN.Payments
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserTable")]
        public int UserTableId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderState { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public userTable UserTable { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
