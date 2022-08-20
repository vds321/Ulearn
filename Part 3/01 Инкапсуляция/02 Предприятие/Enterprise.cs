using System;
using System.Linq;

namespace Incapsulation.EnterpriseTask
{
    public class Enterprise
    {
        private readonly Guid _guid;
        private string _inn;
        public Enterprise(Guid guid)
        {
            _guid = guid;
        }
        public Guid Guid => _guid;
        public string Name { get; set; }
        public DateTime EstablishDate { get; set; }
        public string Inn
        {
            get => _inn;
            set
            {
                if (value.Length != 10 || !value.All(char.IsDigit))
                    throw new ArgumentException();
                _inn = value;
            }
        }
        public TimeSpan ActiveTimeSpan => DateTime.Now - EstablishDate;

        public double GetTotalTransactionsAmount()
        {
            DataBase.OpenConnection();
            return DataBase.Transactions().Where(x => x.EnterpriseGuid == Guid).Sum(transaction => transaction.Amount);
        }
    }
}
