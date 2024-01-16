using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using WEB_APP_PROJECT.Data;
using WEB_APP_PROJECT.Models;

namespace WEB_APP_PROJECT.Repositories
{
    public class HomeRepository
    {
        private readonly ApplicationDbContext _db;
        public HomeRepository(ApplicationDbContext db) { 
            _db = db;
        }
    //    public async Task<IEnumerable<FoodCourt>> DisplayFoodCourt(string sTerm = "", int categoryId = 0)
    //    {
    //        sTerm = sTerm.ToLower();
    //        var foods = (from food in _db.FoodCourts
    //                     where string.IsNullOrEmpty(sTerm) || (food != null && food.FoodShopName.ToLower().StartsWith(sTerm))

    //                     select new FoodCourt
    //                     {
    //                         Id = food.Id,
    //                         FoodShopImg = food.FoodShopImg,
    //                         ShopContact = food.ShopContact,
    //                         FoodRecommened = food.FoodRecommened,
    //                         FoodShopName = food.FoodShopName

    //                     }
    //                     ).ToListAsync();
    //    }
    }
}
