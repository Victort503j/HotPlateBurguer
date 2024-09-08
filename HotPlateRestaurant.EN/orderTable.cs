using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.EN
{
    public class orderTable
    {
        [Key]
        public int Confirmation_ID { get; set; }  
        public string CustomerName { get; set; }    
        public string Total { get; set; }      
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public string Address { get; set; }     
        public string Email { get; set; }          
        public string Phone { get; set; }          
        public string Orders { get; set; }

    }
}
