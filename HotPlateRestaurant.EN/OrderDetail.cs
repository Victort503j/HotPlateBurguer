using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.EN
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderTableId { get; set; }
        public int FoodTableId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public orderTable OrderTable {  get; set; }
        public foodTable FoodTable { get; set; }
    }
}
