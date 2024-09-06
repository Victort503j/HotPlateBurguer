using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.EN
{
    public class orderTable
    {
        public string Confirmation_ID { get; set; }  
        public string CustomerName { get; set; }    
        public string Total { get; set; }      
        public string Time { get; set; }           
        public string Address { get; set; }     
        public string Email { get; set; }          
        public string Phone { get; set; }          
        public string OrderDetails { get; set; }
    }
}
