using VendingMachineAPI.Repositories;

namespace VendingMachineAPI
{
    public class VendingMachine
    {
        public Dictionary<Item, int> Inventory { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        private IRepository _repository;

        public VendingMachine(IRepository repository)
        {
            _repository = repository;
            Transactions = _repository.LoadTransactions();

            // Initialize the inventory
            Inventory = new Dictionary<Item, int>
            {
                { new Item("Soda", 0.95m), 5 },
                { new Item("Candy Bar", 0.60m), 5 },
                { new Item("Chips", 0.99m), 5 }
            };
        }

        public Transaction PurchaseItems(Dictionary<Item, int> itemsToPurchase, Payment payment)
        {
            // Validate the items to purchase and the payment
            decimal totalAmount = 0;
            List<ItemPurchase> itemPurchases = new List<ItemPurchase>();

            foreach (var item in itemsToPurchase)
            {
                if (!Inventory.ContainsKey(item.Key))
                {
                    throw new ArgumentException($"Item '{item.Key.Name}' not found.");
                }

                if (Inventory[item.Key] < item.Value)
                {
                    throw new ArgumentException($"Not enough stock for '{item.Key.Name}'.");
                }

                totalAmount += item.Key.Price * item.Value;
                itemPurchases.Add(new ItemPurchase(item.Key, item.Value));
            }

            if (!payment.ProcessPayment(totalAmount))
            {
                throw new InvalidOperationException("Payment failed.");
            }

            // Update the inventory
            foreach (var item in itemsToPurchase)
            {
                Inventory[item.Key] -= item.Value;
            }

            // Calculate the refund amount and create a transaction
            decimal refundAmount = Refund(totalAmount, payment);
            int transactionId = Transactions.Count > 0 ? Transactions.Max(t => t.Id) + 1 : 1;
            Transaction transaction = new Transaction(transactionId, itemPurchases, payment, refundAmount);
            Transactions.Add(transaction);

            _repository.SaveTransactions(Transactions);

            return transaction;
        }

        public decimal Refund(decimal totalAmount, Payment payment)
        {
            if (payment.Amount - totalAmount <= 0)
            {
                throw new ArgumentException("No refund is necessary for this transaction.");
            }

            return payment.Amount - totalAmount;
        }

        public Item GetItemByName(string name)
        {
            return Inventory.Keys.FirstOrDefault(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}

