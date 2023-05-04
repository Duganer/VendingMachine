namespace VendingMachineAPI
{
    public class Transaction
    {
        public int Id { get; set; }
        public List<ItemPurchase> ItemsPurchased { get; set; }
        public Payment Payment { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal RefundAmount { get; set; }

        public Transaction()
        {
        }

        public Transaction(int id, List<ItemPurchase> itemsPurchased, Payment payment, decimal refundAmount)
        {
            Id = id;
            ItemsPurchased = itemsPurchased;
            Payment = payment;
            Timestamp = DateTime.UtcNow;
            RefundAmount = refundAmount;
        }
    }
}