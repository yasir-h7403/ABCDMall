using ABCDMall.Models;
using System.Collections.Generic;

namespace ABCDMall.ViewModels
{
    public class SearchViewModel
    {
        public List<Shops> Shops { get; set; } = new List<Shops>();
        public List<FoodCourt> FoodCourts { get; set; } = new List<FoodCourt>();
    }

}
