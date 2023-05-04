using Microsoft.AspNetCore.Mvc;
using VendingMachineAPI.Repositories;

namespace VendingMachineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendingMachineController : ControllerBase
    {
        private readonly VendingMachine _vendingMachine;
        private readonly IRepository _repository;

        public VendingMachineController(IRepository repository)
        {
            _repository = repository;
            _vendingMachine = new VendingMachine(_repository);
        }

        [HttpGet("transactions")]
        public IActionResult GetTransactions()
        {
            return Ok(_vendingMachine.Transactions);
        }

        [HttpGet("transactions/{id}")]
        public IActionResult GetTransactionById(int id)
        {
            var transaction = _vendingMachine.Transactions.FirstOrDefault(t => t.Id == id);

            if (transaction == null)
            {
                return NotFound($"Transaction with ID {id} not found.");
            }

            return Ok(transaction);
        }

        [HttpPost("purchase")]
        public IActionResult PurchaseItems([FromBody] PurchaseRequest request)
        {
            Dictionary<Item, int> itemsToPurchase = new Dictionary<Item, int>();

            foreach (var itemRequest in request.Items)
            {
                Item item = _vendingMachine.GetItemByName(itemRequest.Item.Name);

                if (item == null)
                {
                    return BadRequest($"Item '{itemRequest.Item.Name}' not found.");
                }

                itemsToPurchase.Add(item, itemRequest.Quantity);
            }

            Payment payment = new Payment(request.PaymentType, request.PaymentAmount);

            try
            {
                Transaction transaction = _vendingMachine.PurchaseItems(itemsToPurchase, payment);

                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
