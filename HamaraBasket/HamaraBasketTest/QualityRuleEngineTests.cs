using HamaraBasket.Com;
using HamaraBasket.Com.Models;
using HamaraBasket.Com.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace HamaraBasketTest
{
    [TestClass]
    public class QualityRuleEngineTests
    {
        Mock<IDataRetriever<Items>> MockItem;
        Mock<IDataRetriever<ItemTypes>> MockItemstype;
        Mock<ILogger<QualityRuleEngine>> _logger;

        QualityRuleEngine qualityRuleEngine;

        [TestInitialize]
        public void init()
        {
            MockItem = new Mock<IDataRetriever<Items>>();
            MockItemstype = new Mock<IDataRetriever<ItemTypes>>();
            _logger = new Mock<ILogger<QualityRuleEngine>>();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QualityRuleEngineTests_Through_ArgumentNullException_when_ItemDataretriever_AsNull()
        {
            MockItemstype = new Mock<IDataRetriever<ItemTypes>>();
            qualityRuleEngine = new QualityRuleEngine(_logger.Object, null, MockItemstype.Object);
            var result = qualityRuleEngine.RuleEngine();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QualityRuleEngineTests_Through_ArgumentNullException_when_MockItemstypeDataretriever_AsNull()
        {
            var items = new List<Items>();
            items.Add(new Items() { Id = 1, ItemName = "Forest Honey", Price = 20.5, QualityValue = 1, TypeId = 1, SellByValue = 1 });
            items.Add(new Items() { Id = 2, ItemName = "Movie Tickets", Price = 20.5, QualityValue = 2, TypeId = 1, SellByValue = 3 });
            items.Add(new Items() { Id = 3, ItemName = "Cycles", Price = 20.5, QualityValue = 3, TypeId = 1, SellByValue = 5 });
            items.Add(new Items() { Id = 4, ItemName = "Bed", Price = 20.5, QualityValue = 4, TypeId = 1, SellByValue = 10 });

            MockItem.Setup(e => e.Retriever()).Returns(items);
            qualityRuleEngine = new QualityRuleEngine(_logger.Object, MockItem.Object, null);
            var result = qualityRuleEngine.RuleEngine();
        }

        [TestMethod]
        public void QualityRuleEngineTests_Return_Same_NumberofCount_passed()
        {
            var items = new List<Items>();
            items.Add(new Items() { Id = 1, ItemName = "Forest Honey", Price = 20.5, QualityValue = 1, TypeId = 1, SellByValue = 1 });
            items.Add(new Items() { Id = 2, ItemName = "Movie Tickets", Price = 20.5, QualityValue = 2, TypeId = 1, SellByValue = 3 });
            items.Add(new Items() { Id = 3, ItemName = "Cycles", Price = 20.5, QualityValue = 3, TypeId = 1, SellByValue = 5 });
            items.Add(new Items() { Id = 4, ItemName = "Bed", Price = 20.5, QualityValue = 4, TypeId = 1, SellByValue = 10 });

            var itemsTypes = new List<ItemTypes>();
            itemsTypes.Add(new ItemTypes() { Id = 1, typeName = "Legendary" });
            itemsTypes.Add(new ItemTypes() { Id = 2, typeName = "Indian Wine" });
            itemsTypes.Add(new ItemTypes() { Id = 3, typeName = "Movie Tickets" });
            itemsTypes.Add(new ItemTypes() { Id = 4, typeName = "Grocery" });
            itemsTypes.Add(new ItemTypes() { Id = 5, typeName = "Clothes" });

            MockItem.Setup(e => e.Retriever()).Returns(items);
            MockItemstype.Setup(i => i.Retriever()).Returns(itemsTypes);

            qualityRuleEngine = new QualityRuleEngine(_logger.Object, MockItem.Object, MockItemstype.Object);

            var result = qualityRuleEngine.RuleEngine();
            Assert.AreEqual(result.Count, items.Count);

        }
        /// <summary>
        /// When we get Qualityvalue MoreThan 50
        /// </summary>
        [TestMethod]
        public void QualityRuleEngineTests_Quantity_MoreThan_50()
        {
            var items = new List<Items>();
            items.Add(new Items() { Id = 1, ItemName = "Movie Tickets", Price = 20.5, QualityValue = 51, TypeId = 1, SellByValue = 1 });
            var itemsTypes = new List<ItemTypes>();
            itemsTypes.Add(new ItemTypes() { Id = 1, typeName = "Legendary" });
            itemsTypes.Add(new ItemTypes() { Id = 2, typeName = "Indian Wine" });
            itemsTypes.Add(new ItemTypes() { Id = 3, typeName = "Movie Tickets" });
            itemsTypes.Add(new ItemTypes() { Id = 4, typeName = "Grocery" });
            itemsTypes.Add(new ItemTypes() { Id = 5, typeName = "Clothes" });

            MockItem.Setup(e => e.Retriever()).Returns(items);
            MockItemstype.Setup(i => i.Retriever()).Returns(itemsTypes);

            qualityRuleEngine = new QualityRuleEngine(_logger.Object, MockItem.Object, MockItemstype.Object);

            var result = qualityRuleEngine.RuleEngine();
            var tt = result.Find(ex => ex.Id == 1);

            Assert.AreEqual(result.Count, items.Count);
            Assert.AreEqual(tt.QualityValue, 50);
        }

        /// <summary>
        /// When we get Qualityvalue LessThan 0
        /// </summary>
        [TestMethod]
        public void QualityRuleEngineTests_Quantity_LessThan_0()
        {
            var items = new List<Items>();
            items.Add(new Items() { Id = 1, ItemName = "Movie Tickets", Price = 20.5, QualityValue = -51, TypeId = 1, SellByValue = 1 });
            var itemsTypes = new List<ItemTypes>();
            itemsTypes.Add(new ItemTypes() { Id = 1, typeName = "Legendary" });
            itemsTypes.Add(new ItemTypes() { Id = 2, typeName = "Indian Wine" });
            itemsTypes.Add(new ItemTypes() { Id = 3, typeName = "Movie Tickets" });
            itemsTypes.Add(new ItemTypes() { Id = 4, typeName = "Grocery" });
            itemsTypes.Add(new ItemTypes() { Id = 5, typeName = "Clothes" });

            MockItem.Setup(e => e.Retriever()).Returns(items);
            MockItemstype.Setup(i => i.Retriever()).Returns(itemsTypes);

            qualityRuleEngine = new QualityRuleEngine(_logger.Object, MockItem.Object, MockItemstype.Object);

            var result = qualityRuleEngine.RuleEngine();
            var tt = result.Find(ex => ex.Id == 1);

            Assert.AreEqual(result.Count, items.Count);
            Assert.AreEqual(tt.QualityValue, 0);



        }
        /// <summary>
        /// Item Type Other then Legendary, Indian_Wine, Movie_Tickets any items should be reduced qaulity and sellby values by 1 
        /// </summary>
        [TestMethod]
        public void QualityRuleEngineTests_ItemType_is_OtherThan_Legendary_Indian_Wine_Movie_Tickets()
        {
            var items = new List<Items>();
            items.Add(new Items() { Id = 1, ItemName = "Vegitables", Price = 20.5, QualityValue = 5, TypeId = 4, SellByValue = 2 });
            var itemsTypes = new List<ItemTypes>();
            itemsTypes.Add(new ItemTypes() { Id = 1, typeName = "Legendary" });
            itemsTypes.Add(new ItemTypes() { Id = 2, typeName = "Indian Wine" });
            itemsTypes.Add(new ItemTypes() { Id = 3, typeName = "Movie Tickets" });
            itemsTypes.Add(new ItemTypes() { Id = 4, typeName = "Grocery" });
            itemsTypes.Add(new ItemTypes() { Id = 5, typeName = "Clothes" });

            MockItem.Setup(e => e.Retriever()).Returns(items);
            MockItemstype.Setup(i => i.Retriever()).Returns(itemsTypes);

            qualityRuleEngine = new QualityRuleEngine(_logger.Object, MockItem.Object, MockItemstype.Object);

            var result = qualityRuleEngine.RuleEngine();
            var tt = result.Find(ex => ex.Id == 1);

            Assert.AreEqual(result.Count, items.Count);
            Assert.AreEqual(tt.QualityValue, 4);
            Assert.AreEqual(tt.SellByValue, 1);


        }

        /// <summary>
        /// Item Type Other then Legendary, Indian_Wine, Movie_Tickets any items qaulity should dropped twice 
        /// </summary>
        [TestMethod]
        public void QualityRuleEngineTests_ItemType_is_OtherThan_Legendary_Indian_Wine_Movie_Tickets_and_SellByvalue_is_0()
        {
            var items = new List<Items>();
            items.Add(new Items() { Id = 1, ItemName = "Vegitables", Price = 20.5, QualityValue = 6, TypeId = 4, SellByValue = 0 });
            var itemsTypes = new List<ItemTypes>();
            itemsTypes.Add(new ItemTypes() { Id = 1, typeName = "Legendary" });
            itemsTypes.Add(new ItemTypes() { Id = 2, typeName = "Indian Wine" });
            itemsTypes.Add(new ItemTypes() { Id = 3, typeName = "Movie Tickets" });
            itemsTypes.Add(new ItemTypes() { Id = 4, typeName = "Grocery" });
            itemsTypes.Add(new ItemTypes() { Id = 5, typeName = "Clothes" });

            MockItem.Setup(e => e.Retriever()).Returns(items);
            MockItemstype.Setup(i => i.Retriever()).Returns(itemsTypes);

            qualityRuleEngine = new QualityRuleEngine(_logger.Object, MockItem.Object, MockItemstype.Object);

            var result = qualityRuleEngine.RuleEngine();
            var tt = result.Find(ex => ex.Id == 1);

            Assert.AreEqual(result.Count, items.Count);
            Assert.AreEqual(tt.QualityValue, 3);

        }

        /// <summary>
        /// Item Type is Movie_Tickets and SellbyDate is LessThanOREqual 10 days Quality should be 2 times the existing value of Quantity
        /// </summary>
        [TestMethod]
        public void QualityRuleEngineTests_ItemType_is_Movie_Tickets_and_SellByDate_is_LessThanOREqual_10_days()
        {
            var items = new List<Items>();
            items.Add(new Items() { Id = 1, ItemName = "Vegitables", Price = 20.5, QualityValue = 6, TypeId = 3, SellByValue = 10 });
            var itemsTypes = new List<ItemTypes>();
            itemsTypes.Add(new ItemTypes() { Id = 1, typeName = "Legendary" });
            itemsTypes.Add(new ItemTypes() { Id = 2, typeName = "Indian Wine" });
            itemsTypes.Add(new ItemTypes() { Id = 3, typeName = "Movie Tickets" });
            itemsTypes.Add(new ItemTypes() { Id = 4, typeName = "Grocery" });
            itemsTypes.Add(new ItemTypes() { Id = 5, typeName = "Clothes" });

            MockItem.Setup(e => e.Retriever()).Returns(items);
            MockItemstype.Setup(i => i.Retriever()).Returns(itemsTypes);

            qualityRuleEngine = new QualityRuleEngine(_logger.Object, MockItem.Object, MockItemstype.Object);

            var result = qualityRuleEngine.RuleEngine();
            var tt = result.Find(ex => ex.Id == 1);

            Assert.AreEqual(result.Count, items.Count);
            Assert.AreEqual(tt.QualityValue, 12);

        }
        /// <summary>
        /// Item Type is Movie_Tickets and SellbyDate is LessThanOREqual 5 days Quality should be 3 times the existing value of Quantity
        /// </summary>

        [TestMethod]
        public void QualityRuleEngineTests_ItemType_is_Movie_Tickets_and_SellByvalue_is_LessThanOREqual_5_days()
        {
            var items = new List<Items>();
            items.Add(new Items() { Id = 1, ItemName = "Vegitables", Price = 20.5, QualityValue = 6, TypeId = 3, SellByValue = 5 });
            var itemsTypes = new List<ItemTypes>();
            itemsTypes.Add(new ItemTypes() { Id = 1, typeName = "Legendary" });
            itemsTypes.Add(new ItemTypes() { Id = 2, typeName = "Indian Wine" });
            itemsTypes.Add(new ItemTypes() { Id = 3, typeName = "Movie Tickets" });
            itemsTypes.Add(new ItemTypes() { Id = 4, typeName = "Grocery" });
            itemsTypes.Add(new ItemTypes() { Id = 5, typeName = "Clothes" });

            MockItem.Setup(e => e.Retriever()).Returns(items);
            MockItemstype.Setup(i => i.Retriever()).Returns(itemsTypes);

            qualityRuleEngine = new QualityRuleEngine(_logger.Object, MockItem.Object, MockItemstype.Object);

            var result = qualityRuleEngine.RuleEngine();
            var tt = result.Find(ex => ex.Id == 1);

            Assert.AreEqual(result.Count, items.Count);
            Assert.AreEqual(tt.QualityValue, 18);

        }

        /// <summary>
        /// Item Type is Movie_Tickets and SellbyDate is LessThanOREqual 5 days Quality should be 3 times the existing value of Quantity
        /// </summary>

        [TestMethod]
        public void QualityRuleEngineTests_ItemType_is_Movie_Tickets_AfterShowDay()
        {
            var items = new List<Items>();
            items.Add(new Items() { Id = 1, ItemName = "Vegitables", Price = 20.5, QualityValue = 6, TypeId = 3, SellByValue = 0 });
            var itemsTypes = new List<ItemTypes>();
            itemsTypes.Add(new ItemTypes() { Id = 1, typeName = "Legendary" });
            itemsTypes.Add(new ItemTypes() { Id = 2, typeName = "Indian Wine" });
            itemsTypes.Add(new ItemTypes() { Id = 3, typeName = "Movie Tickets" });
            itemsTypes.Add(new ItemTypes() { Id = 4, typeName = "Grocery" });
            itemsTypes.Add(new ItemTypes() { Id = 5, typeName = "Clothes" });

            MockItem.Setup(e => e.Retriever()).Returns(items);
            MockItemstype.Setup(i => i.Retriever()).Returns(itemsTypes);

            qualityRuleEngine = new QualityRuleEngine(_logger.Object, MockItem.Object, MockItemstype.Object);

            var result = qualityRuleEngine.RuleEngine();
            var tt = result.Find(ex => ex.Id == 1);

            Assert.AreEqual(result.Count, items.Count);
            Assert.AreEqual(tt.QualityValue, 0);

        }


        /// <summary>
        /// Item Type is Movie_Tickets and SellbyDate is LessThanOREqual 5 days Quality should be 3 times the existing value of Quantity
        /// </summary>

        [TestMethod]
        public void QualityRuleEngineTests_ItemType_Indian_WInes_Quality_never_decreases()
        {
            var items = new List<Items>();
            items.Add(new Items() { Id = 1, ItemName = "Vegitables", Price = 20.5, QualityValue = 6, TypeId = 2, SellByValue = 10 });
            var itemsTypes = new List<ItemTypes>();
            itemsTypes.Add(new ItemTypes() { Id = 1, typeName = "Legendary" });
            itemsTypes.Add(new ItemTypes() { Id = 2, typeName = "Indian Wine" });
            itemsTypes.Add(new ItemTypes() { Id = 3, typeName = "Movie Tickets" });
            itemsTypes.Add(new ItemTypes() { Id = 4, typeName = "Grocery" });
            itemsTypes.Add(new ItemTypes() { Id = 5, typeName = "Clothes" });

            MockItem.Setup(e => e.Retriever()).Returns(items);
            MockItemstype.Setup(i => i.Retriever()).Returns(itemsTypes);

            qualityRuleEngine = new QualityRuleEngine(_logger.Object, MockItem.Object, MockItemstype.Object);

            var result = qualityRuleEngine.RuleEngine();
            var tt = result.Find(ex => ex.Id == 1);

            Assert.AreEqual(result.Count, items.Count);
            Assert.AreEqual(tt.QualityValue, 7);

        }
    }
}
