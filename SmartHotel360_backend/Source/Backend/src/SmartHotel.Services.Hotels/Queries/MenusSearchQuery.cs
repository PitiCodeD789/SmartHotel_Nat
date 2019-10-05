using SmartHotel.Services.Hotels.Data;
using SmartHotel.Services.Hotels.Domain.RoomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Queries
{
    public class MenuSearchResult
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsAvailable { get; set; }
        public string Image { get; set; }
    }


    public class MenusSearchQuery
    {
        private readonly HotelsDbContext _db;

        public MenusSearchQuery(HotelsDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<MenuSearchResult>> Get(int hotelId)
        {
            var query = _db.Menus
            .OrderByDescending(menu => menu.Id)
            .Where(menu => menu.HotelId == hotelId );

            var categories = _db.Categories
                .OrderByDescending(category => category.Id)
                .Where(category => category.HotelId == hotelId);

            var menus = query
                .Select(menu => new MenuSearchResult
                {
                    Id = menu.Id,
                    Description = menu.Description,
                    IsAvailable = menu.IsAvailable,
                    IsRecommended = menu.IsRecommended,
                    Item = menu.Item,
                    Price = menu.Price,
                    Image = menu.Image,
                    Category = categories.Where(cat => cat.Id == menu.CategoryId).FirstOrDefault()
                })
           .ToList();

            return menus;
        }
    }
}
