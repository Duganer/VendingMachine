namespace VendingMachineAPI
{
    public class Payment
    {
        public string PaymentType { get; private set; }
        public decimal Amount { get; private set; }

        public Payment(string paymentType, decimal amount)
        {
            PaymentType = paymentType;
            Amount = amount;
        }

        public bool ProcessPayment(decimal requiredAmount)
        {
            if (Amount < requiredAmount)
            {
                return false;
            }
            return true;
        }
    }

}

