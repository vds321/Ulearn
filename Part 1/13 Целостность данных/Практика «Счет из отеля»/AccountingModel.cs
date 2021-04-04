using System;

namespace HotelAccounting
{
    class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double total;
        private double discount;
        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0) throw new ArgumentException();
                price = value;
                total = Price * NightsCount * (1 - (Discount / 100));
                Notify(nameof(Price));
                Notify(nameof(Total));
            }
        }
        public int NightsCount
        {
            get { return nightsCount; }
            set
            {
                if (value <= 0) throw new ArgumentException();
                nightsCount = value;
                total = Price * NightsCount * (1 - (Discount / 100));
                Notify(nameof(Total));
                Notify(nameof(NightsCount));
            }
        }
        public double Discount
        {
            get { return discount; }//  }
            set
            {
                discount = value;
                total = Price * NightsCount * (1 - (Discount / 100));
                if (total < 0) throw new ArgumentException();
                Notify(nameof(Total));
                Notify(nameof(Discount));
            }
        }
        public double Total
        {
            get
            {
                return total;
            }
            set
            {
                if (value < 0) throw new ArgumentException();
                total = value;
                discount = (Price * NightsCount - Total) * 100 / (Price * NightsCount);
                Notify(nameof(Total));
                Notify(nameof(Discount));
            }
        }
    }
}
