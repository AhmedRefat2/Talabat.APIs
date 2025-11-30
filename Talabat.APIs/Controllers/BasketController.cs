using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Domain.Entities.Basket;
using Talabat.Domain.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        // GET: api/Basket?id=123
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            // if basket is expired or not found, return a new basket
            return Ok(basket ?? new CustomerBasket(id));
        }

        // POST: api/Basket
        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasket), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var createdOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(basket);
            if (createdOrUpdatedBasket == null)
                return BadRequest(new ApiResponse(400));
            return Ok(createdOrUpdatedBasket);
        }

        // DELETE: api/Basket?id=123
        [HttpDelete]
        public async Task DeleteBasket(string id)
            => await _basketRepository.DeleteBasketAsync(id);
    }
}
