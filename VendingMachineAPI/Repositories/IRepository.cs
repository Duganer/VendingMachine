namespace VendingMachineAPI.Repositories
{
    public interface IRepository
    {
        void SaveTransactions(List<Transaction> transactions);
        List<Transaction> LoadTransactions();
    }

}
