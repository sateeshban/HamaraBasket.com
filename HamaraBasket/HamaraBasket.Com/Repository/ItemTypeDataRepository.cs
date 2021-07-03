using HamaraBasket.Com.Models;
using System.Collections.Generic;

namespace HamaraBasket.Com.Repository
{
    public class ItemTypeDataRepository : IDataRetriever<ItemTypes>
    {
        public List<ItemTypes> Retriever()
        {
            var items = new List<ItemTypes>();
            items.Add(new ItemTypes() { Id = 1, typeName = "Legendary" });
            items.Add(new ItemTypes() { Id = 2, typeName = "Indian Wine" });
            items.Add(new ItemTypes() { Id = 3, typeName = "Movie Tickets" });
            items.Add(new ItemTypes() { Id = 4, typeName = "Grocery" });
            items.Add(new ItemTypes() { Id = 5, typeName = "Clothes" });

            return items;
        }
    }
}
