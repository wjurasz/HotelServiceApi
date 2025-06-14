using HotelService.ClientApi.Entities;
using HotelService.Reservation.CrossCutting.Dtos;
using HotelService.ReservationApi.Resolvers;
using HotelService.ReservationApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.ReservationApi.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;
        private readonly ClientResolver _clientResolver;

        public ReservationController(ReservationService reservationService, ClientResolver clientResolver)
        {
            _reservationService = reservationService;
            _clientResolver = clientResolver;
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
        public async Task<ActionResult<ReservationDto.Read>> GetById(int id)
        {
            var r = await _reservationService.GetById(id);
            if (r == null) return NotFound();

            string clientFullName = await _clientResolver.ResolveFullName(r.ClientId);

            return Ok(new ReservationDto.Read
            {
                Id = r.Id,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                Status = r.Status.ToString(),
                ClientId = r.ClientId,
                ClientFullName = clientFullName
            });
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
