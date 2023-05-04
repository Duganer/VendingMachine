namespace VendingMachineAPI
{
    public class Item
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Item (string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
