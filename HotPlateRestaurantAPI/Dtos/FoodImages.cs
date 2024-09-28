using HotPlateRestaurant.EN;

namespace HotPlateRestaurantAPI.Dtos
{
    public class FoodWithImagesDTO
    {
        public foodTable Food { get; set; }
        public List<FoodImagesDTO> Images { get; set; }
    }

    public class FoodImagesDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public byte IsPrimary { get; set; }
        public IFormFile File { get; set; } // Para el archivo de imagen
    }

}
