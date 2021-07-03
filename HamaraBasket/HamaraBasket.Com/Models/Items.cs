using System;

namespace HamaraBasket.Com.Models
{
    public class Items
    {
        private const string V = "yyyy-MM-dd HH:mm:ss";

        public int Id { get; set; }
        public string ItemName { get; set; }
        public int TypeId { get; set; }
        public int QualityValue { get; set; }
        public double Price { get; set; }
        public int SellByValue { get; set; }
        public string SellByDate
        {
            get
            {
                return DateTime.Now.AddDays(SellByValue).ToString(V);
            }
            set
            {

            }
        }

    }
}
