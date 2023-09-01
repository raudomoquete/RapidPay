using AuthSysPay.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthSysPay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] Card card)
        {
            await _cardService.CreateCard(card);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBalance(int id)
        {
            var card = new Card { Id = id };
            var balance = await _cardService.GetBalance(card);
            return Ok(balance);
        }

        [HttpPut("{id}/pay")]
        public async Task<IActionResult> Pay(int id, [FromBody] decimal amount)
        {
            var card = new Card { Id = id };
            await _cardService.Pay(card, amount);
            return Ok();
        }
    }
}
