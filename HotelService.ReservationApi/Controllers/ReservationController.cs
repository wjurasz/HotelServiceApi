using HotelService.ClientApi.Entities;
using HotelService.ReservationApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.ReservationApi.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Zwraca wszystkie rezerwacje.
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<Reservation.Storage.Entities.Reservation>> GetAll()
        {
            return await _reservationService.GetAll();
        }

        /// <summary>
        /// Pobiera rezerwację na podstawie identyfikatora.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reservation = await _reservationService.GetById(id);
            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        /// <summary>
        /// Tworzy nową rezerwację.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Reservation.Storage.Entities.Reservation reservation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reservationService.Add(reservation);
            return Ok();
        }

        /// <summary>
        /// Potwierdza rezerwację.
        /// </summary>
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
