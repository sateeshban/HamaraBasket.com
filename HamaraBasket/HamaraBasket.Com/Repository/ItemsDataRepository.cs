using HamaraBasket.Com.Models;
using System.Collections.Generic;

namespace HamaraBasket.Com.Repository
{
    public class ItemsDataRepository : IDataRetriever<Items>
    {
        public List<Items> Retriever()
        {
            var items = new List<Items>();
            items.Add(new Items() { Id = 1, ItemName = "Forest Honey", Price = 20.5, QualityValue = 1, TypeId = 1, SellByValue = 1 });
            items.Add(new Items() { Id = 2, ItemName = "Movie Tickets", Price = 20.5, QualityValue = 2, TypeId = 1, SellByValue = 3 });
            items.Add(new Items() { Id = 3, ItemName = "Cycles", Price = 20.5, QualityValue = 3, TypeId = 1, SellByValue = 5 });
            items.Add(new Items() { Id = 4, ItemName = "Bed", Price = 20.5, QualityValue = 4, TypeId = 1, SellByValue = 10 });
            items.Add(new Items() { Id = 5, ItemName = "Red Wine", Price = 20.5, QualityValue = 3, TypeId = 2, SellByValue = 5 });
            items.Add(new Items() { Id = 6, ItemName = "Wiskey", Price = 20.5, QualityValue = 30, TypeId = 2, SellByValue = 8 });
            items.Add(new Items() { Id = 7, ItemName = "Honey", Price = 20.5, QualityValue = 4, TypeId = 1, SellByValue = 10 });
            items.Add(new Items() { Id = 8, ItemName = "Boll", Price = 20.5, QualityValue = 3, TypeId = 1, SellByValue = 6 });
            return items;
        }
    }
}
