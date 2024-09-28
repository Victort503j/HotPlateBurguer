using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPlateRestaurant.EN
{
    public class FoodImages
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public string ImageFormat { get; set; } = "png";
        public DateTime UploadedAt { get; set; } = DateTime.Now;
        public string AltText { get; set; }
        public byte IsPrimary { get; set; } = 0;

        [ForeignKey("FoodId")]
        public virtual foodTable foodTable { get; set; }
    }
}
