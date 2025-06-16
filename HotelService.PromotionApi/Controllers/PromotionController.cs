using HotelService.Promotion.CrossCutting.Dtos;
using HotelService.Promotion.Storage.Entities;
using HotelService.PromotionApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.PromotionApi.Controllers
{
    [ApiController]
    [Route("api/promotions")]
    public class PromotionController : ControllerBase
    {
        private readonly PromotionService _promotionService;

        public PromotionController(PromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        /// <summary>
        /// Zwraca listę wszystkich promocji.
        /// </summary>
        /// <returns>Lista obiektów PromotionDto.Read.</returns>
        [HttpGet]
        public async Task<IEnumerable<PromotionDto.Read>> GetAll()
        {
            var promos = await _promotionService.GetAll();
            return promos.Select(p => new PromotionDto.Read
            {
                Id = p.Id,
                Code = p.Code,
                DiscountPercentage = p.DiscountPercentage,
                ValidUntil = p.ValidUntil
            });
        }

        /// <summary>
        /// Pobiera promocję na podstawie identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator promocji.</param>
        /// <returns>Obiekt PromotionDto.Read lub 404 jeśli nie znaleziono.</returns>
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var promo = await _promotionService.GetById(id);
            if (promo == null) return NotFound();

            return Ok(new PromotionDto.Read
            {
                Id = promo.Id,
                Code = promo.Code,
                DiscountPercentage = promo.DiscountPercentage,
                ValidUntil = promo.ValidUntil
            });
        }


        /// <summary>
        /// Pobiera promocję na podstawie kodu.
        /// </summary>
        /// <param name="code">Kod promocji (np. "SUMMER").</param>
        /// <returns>Obiekt PromotionDto.Read lub 404 jeśli nie znaleziono.</returns>
        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var promo = await _promotionService.GetByCode(code);
            if (promo == null) return NotFound();

            return Ok(new PromotionDto.Read
            {
                Id = promo.Id,
                Code = promo.Code,
                DiscountPercentage = promo.DiscountPercentage,
                ValidUntil = promo.ValidUntil
            });
        }

        /// <summary>
        /// Tworzy nową promocję.
        /// </summary>
        /// <param name="dto">Obiekt zawierający dane promocji do utworzenia.</param>
        /// <returns>Status 200 po sukcesie lub 400 jeśli dane są nieprawidłowe.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PromotionDto.Create dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var promo = new Promotion.Storage.Entities.Promotion
            {
                Code = dto.Code,
                DiscountPercentage = dto.DiscountPercentage,
                ValidUntil = dto.ValidUntil
            };

            await _promotionService.Add(promo);
            return Ok();
        }

        /// <summary>
        /// Usuwa promocję na podstawie identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator promocji.</param>
        /// <returns>Status 204 po pomyślnym usunięciu.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _promotionService.Delete(id);
            return NoContent();
        }
    }
}
