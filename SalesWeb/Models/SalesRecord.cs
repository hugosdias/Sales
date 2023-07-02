using SalesWeb.Models.Enums;

namespace SalesWeb.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public SalesStatus Satus { get; set; }
        public Seller Seller { get; set; }

        public SalesRecord()
        {
        }
        public SalesRecord(int id, DateTime date, double amount, SalesStatus satus, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Satus = satus;
            Seller = seller;
        }
    }
}
