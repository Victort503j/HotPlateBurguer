using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.EN
{
    public class categoryTable
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
