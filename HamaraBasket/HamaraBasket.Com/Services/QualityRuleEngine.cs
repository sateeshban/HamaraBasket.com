using HamaraBasket.Com.Interfaces;
using HamaraBasket.Com.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HamaraBasket.Com.Services
{
    public class QualityRuleEngine : IRuleEngine<List<Items>>
    {
        const string Legendary = "Legendary";
        const string IndianWine = "Indian Wine";
        const string Movietickets = "Movie Tickets";

        public IDataRetriever<Items> dataRetrieverItems;
        public IDataRetriever<ItemTypes> dataRetrieverItemTypes;

        private readonly ILogger<QualityRuleEngine> _logger;
        public QualityRuleEngine(ILogger<QualityRuleEngine> pLogger, IDataRetriever<Items> pDataRetrieverItems,
            IDataRetriever<ItemTypes> pDataRetrieverItemTypes
           )
        {
            _logger = pLogger;
            dataRetrieverItems = pDataRetrieverItems;
            dataRetrieverItemTypes = pDataRetrieverItemTypes;

        }
        public List<Items> RuleEngine()
        {
            List<Items> ItemsResult;
            try
            {
                if (dataRetrieverItems == null)
                {
                    throw new ArgumentNullException("[QualityRuleEngine][RuleEngine] dataRetrieverItems Should not be null");
                }
                if (dataRetrieverItemTypes == null)
                {
                    throw new ArgumentNullException("[QualityRuleEngine][RuleEngine] dataRetrieverItemTypes Should not be null");
                }


                ItemsResult = dataRetrieverItems.Retriever();
                var ItemTypesResult = dataRetrieverItemTypes.Retriever();


                Quality_Rule_Engine(ItemsResult, ItemTypesResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ItemsResult;
        }

        private void Quality_Rule_Engine(List<Items> itemsResult, List<ItemTypes> itemTypesResult)
        {
            itemsResult.ForEach(itm =>
            {
                if (itm.QualityValue >= 0 && itm.QualityValue <= 50)
                {

                    var iTypeResult = itemTypesResult.FirstOrDefault(itype => itype.Id == itm.TypeId).typeName;

                    if (iTypeResult != Legendary && iTypeResult != Movietickets & iTypeResult != IndianWine)
                    {
                        if (itm.SellByValue == 0)
                        {
                            itm.QualityValue = itm.QualityValue > 0 ? itm.QualityValue / 2 : 0;
                        }
                        else
                        {
                            itm.QualityValue = itm.QualityValue > 0 ? itm.QualityValue - 1 : 0;
                            itm.SellByValue = itm.SellByValue > 0 ? itm.SellByValue - 1 : 0;
                        }
                    }
                    else if (iTypeResult == Movietickets)
                    {
                        if (itm.SellByValue <= 10 && itm.SellByValue > 5)
                        {
                            itm.QualityValue = itm.QualityValue * 2;
                        }
                        if (itm.SellByValue <= 5 && itm.SellByValue > 0)
                        {
                            itm.QualityValue = itm.QualityValue * 3;
                        }
                        if (itm.SellByValue == 0)
                        {
                            itm.QualityValue = 0;
                        }
                    }
                    else if (iTypeResult == IndianWine)
                    {
                        itm.QualityValue = itm.QualityValue + 1;
                    }


                    _logger.LogError("[QualityRuleEngine][RuleEngine] QualityValue should be in between 0 to 50 Item id: ", itm.Id);

                }

                if (itm.QualityValue > 50)
                {
                    itm.QualityValue = 50;
                }
                else if (itm.QualityValue < 0)
                {
                    itm.QualityValue = 0;
                }

            });
        }
    }
}
