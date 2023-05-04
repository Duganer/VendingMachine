namespace VendingMachineAPI
{
    public class PurchaseRequest
    {
        public List<ItemPurchase> Items { get; set; }
        public string PaymentType { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
