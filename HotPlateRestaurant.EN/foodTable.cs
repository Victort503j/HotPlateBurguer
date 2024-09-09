using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.EN
{
    public class foodTable
    {
        public int Id { get; set; }

        [ForeignKey("categoryTable")]
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Picture { get; set; }

        public categoryTable categoryTable { get; set; }
    }
}