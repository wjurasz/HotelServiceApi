using HotelService.ClientApi.Entities;
using HotelService.Reservation.CrossCutting.Dtos;
using HotelService.Reservation.Storage.Entities;
using HotelService.ReservationApi.Resolvers;
using HotelService.ReservationApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.ReservationApi.Controllers
{
    [ApiController]
    [Route("/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;
        private readonly ClientResolver _clientResolver;
        private readonly PromotionResolver _promotionResolver;

        public ReservationController(
            ReservationService reservationService,
            ClientResolver clientResolver,
            PromotionResolver promotionResolver)
        {
            _reservationService = reservationService;
            _clientResolver = clientResolver;
            _promotionResolver = promotionResolver;
        }

        /// <summary>
        /// Zwraca listę wszystkich rezerwacji.
        /// </summary>
        /// <returns>Lista rezerwacji z bazy danych.</returns>
        [HttpGet]
        public async Task<IEnumerable<Reservation.Storage.Entities.Reservation>> GetAll()
        {
            return await _reservationService.GetAll();
        }

        /// <summary>
        /// Pobiera rezerwację na podstawie identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator rezerwacji.</param>
        /// <returns>Obiekt rezerwacji z danymi klienta i ewentualnie promocji.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto.Read>> GetById(int id)
        {
            var r = await _reservationService.GetById(id);
            if (r == null) return NotFound();

            var client = await _clientResolver.ResolveClient(r.ClientId);

            var promo = r.PromotionId.HasValue
                ? await _promotionResolver.GetPromotionById(r.PromotionId.Value)
                : null;

            return Ok(new ReservationDto.Read
            {
                Id = r.Id,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                Status = r.Status.ToString(),
                ClientId = r.ClientId,
                ClientFullName = client != null ? $"{client.FirstName} {client.LastName}" : "[nieznany]",
                ClientEmail = client?.Email ?? "[nieznany]",
                ClientPhoneNumber = client?.PhoneNumber ?? "[nieznany]",
                PromotionId = r.PromotionId,
                PromotionCode = promo?.Code,
                DiscountPercentage = promo?.DiscountPercentage
            });
        }


        /// <summary>
        /// Tworzy nową rezerwację.
        /// </summary>
        /// <param name="dto">Dane rezerwacji do utworzenia.</param>
        /// <returns>Id rezerwacji, cena końcowa oraz użyta promocja (jeśli występuje).</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationDto.Create dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reservation = new Reservation.Storage.Entities.Reservation
            {
                ClientId = dto.ClientId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = ReservationStatus.Pending,
                PromotionId = dto.PromotionId
            };

            await _reservationService.Add(reservation);

            var promo = dto.PromotionId.HasValue
                ? await _promotionResolver.GetPromotionById(dto.PromotionId.Value)
                : null;

            var finalPrice = await _reservationService.CalculateDiscountedPrice(
                dto.StartDate, dto.EndDate, dto.PromotionId);

            return Ok(new
            {
                ReservationId = reservation.Id,
                FinalPrice = finalPrice,
                AppliedPromotion = promo != null
                    ? $"{promo.Code} ({promo.DiscountPercentage}%)"
                    : "[brak]"
            });
        }

        /// <summary>
        /// Potwierdza rezerwację.
        /// </summary>
        /// <param name="id">Identyfikator rezerwacji.</param>
        /// <returns>Status 200 po pomyślnym potwierdzeniu.</returns>
        [HttpPatch("{id}/confirm")]
        public async Task<IActionResult> Confirm(int id)
        {
            var reservation = await _reservationService.GetById(id);
            if (reservation == null)
                return NotFound();

            await _reservationService.Confirm(id);
            return Ok();
        }

        /// <summary>
        /// Anuluje rezerwację.
        /// </summary>
        /// <param name="id">Identyfikator rezerwacji.</param>
        /// <returns>Status 200 po pomyślnym anulowaniu.</returns>
        [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var reservation = await _reservationService.GetById(id);
            if (reservation == null)
                return NotFound();

            await _reservationService.Cancel(id);
            return Ok();
        }
    }
}
