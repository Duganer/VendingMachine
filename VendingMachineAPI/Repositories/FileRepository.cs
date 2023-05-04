using Newtonsoft.Json;

namespace VendingMachineAPI.Repositories
{
    public class FileRepository : IRepository
    {
        private readonly string _transactionsFilePath;

        public FileRepository(string transactionsFilePath)
        {
            _transactionsFilePath = transactionsFilePath;
        }

        public void SaveTransactions(List<Transaction> transactions)
        {
            string json = JsonConvert.SerializeObject(transactions);
            File.WriteAllText(_transactionsFilePath, json);
        }

        public List<Transaction> LoadTransactions()
        {
            if (!File.Exists(_transactionsFilePath))
            {
                return new List<Transaction>();
            }

            string json = File.ReadAllText(_transactionsFilePath);
            List<Transaction> transactions = JsonConvert.DeserializeObject<List<Transaction>>(json);
            return transactions;
        }
    }

}
