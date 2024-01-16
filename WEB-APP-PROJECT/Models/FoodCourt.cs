using System.ComponentModel.DataAnnotations;
namespace WEB_APP_PROJECT.Models
{
    public class FoodCourt
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? FoodShopName { get; set; }
        [Required]
        public string? ShopContact { get; set; }
        [Required]
        public string? FoodRecommened { get; set; }
        public int? RiderCount { get; set; }
        public string? FoodShopImg { get; set; }


    }
}
