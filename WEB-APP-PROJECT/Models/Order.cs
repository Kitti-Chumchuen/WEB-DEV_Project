using System.ComponentModel.DataAnnotations;
namespace WEB_APP_PROJECT.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string? FoodShopName { get; set; }
        public string? customerId { get; set; }
        public string? riderName { get; set; }
        public string? callRider { get; set; }
        public string? userName { get; set; }
        public string? callUser { get; set; }
        public string? menu { get; set; }
        public int amount { get; set; }
        public string? status { get; set; }

    }
}
