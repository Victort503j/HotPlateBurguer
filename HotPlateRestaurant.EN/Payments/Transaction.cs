using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.EN.Payments
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }

        [Required]
        [StringLength(100)]
        public string PaymentID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        [Required]
        public EstadoPago Estado { get; set; }

        [Required]
        public DateTime FechaTransaccion { get; set; }

        public Order Order { get; set; }
    }

    public enum EstadoPago
    {
        Aprobado = 1,
        Fallido = 2,
        Reembolsado = 3,
        Pendiente = 4
    }
}
