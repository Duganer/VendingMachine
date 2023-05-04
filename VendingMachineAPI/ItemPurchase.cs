namespace VendingMachineAPI
{
    public class ItemPurchase
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public ItemPurchase()
        {
        }

        public ItemPurchase(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}
