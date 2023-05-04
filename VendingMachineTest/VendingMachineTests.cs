using VendingMachineAPI;
using VendingMachineAPI.Repositories;
using Transaction = VendingMachineAPI.Transaction;

namespace VendingMachineTest
{
    public class VendingMachineTests
    {
        [Fact]
        public void TestPurchaseItemsSuccess()
        {
            IRepository repository = new FileRepository("transactions.json");
            VendingMachine vm = new VendingMachine(repository);
            Dictionary<Item, int> itemsToPurchase = new Dictionary<Item, int>
            {
                { vm.GetItemByName("Soda"), 1 },
                { vm.GetItemByName("Candy Bar"), 2 }
            };
            Payment payment = new Payment("cash", 3m);

            // Act
            Transaction transaction = vm.PurchaseItems(itemsToPurchase, payment);

            // Assert
            Assert.NotNull(transaction);
            Assert.Equal(0.85m, transaction.RefundAmount);
        }

        [Fact]
        public void TestInventoryUpdatedAfterPurchase()
        {
            IRepository repository = new FileRepository("transactions.json");
            VendingMachine vm = new VendingMachine(repository);
            Dictionary<Item, int> itemsToPurchase = new Dictionary<Item, int>
            {
                { vm.GetItemByName("Soda"), 1 },
                { vm.GetItemByName("Chips"), 2 }
            };
            Payment payment = new Payment("cash", 4m);

            // Act
            Transaction transaction = vm.PurchaseItems(itemsToPurchase, payment);

            // Assert
            Assert.Equal(4, vm.Inventory[vm.GetItemByName("Soda")]);
            Assert.Equal(3, vm.Inventory[vm.GetItemByName("Chips")]);
        }

        [Fact]
        public void TestExceptionThrownForNonExistentItem()
        {
            IRepository repository = new FileRepository("transactions.json");
            VendingMachine vm = new VendingMachine(repository);
            Dictionary<Item, int> itemsToPurchase = new Dictionary<Item, int>
            {
                { new Item("NonExistentItem", 1m), 1 }
            };
            Payment payment = new Payment("cash", 1m);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => vm.PurchaseItems(itemsToPurchase, payment));
        }

        [Fact]
        public void TestExceptionThrownForInsufficientStock()
        {
            IRepository repository = new FileRepository("transactions.json");
            VendingMachine vm = new VendingMachine(repository);
            Dictionary<Item, int> itemsToPurchase = new Dictionary<Item, int>
            {
                { vm.GetItemByName("Soda"), 10 }
            };
            Payment payment = new Payment("cash", 9.5m);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => vm.PurchaseItems(itemsToPurchase, payment));
        }
    }
}
