using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelService.Reservation.CrossCutting.Dtos
{
    public class ReservationDto
    {
        /// <summary>
        /// Dane do tworzenia rezerwacji.
        /// </summary>
        public class Create
        {
            [Required]
            public int ClientId { get; set; }

            [Required]
            public DateTime StartDate { get; set; }

            [Required]
            public DateTime EndDate { get; set; }
        }

        /// <summary>
        /// Dane do odczytu rezerwacji.
        /// </summary>
        public class Read
        {
            public int Id { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Status { get; set; }

            public int ClientId { get; set; }
            public string ClientFullName { get; set; } = default!;
        }

        /// <summary>
        /// Dane do edytowania rezerwacji (np. przesunięcie terminu).
        /// </summary>
        public class Update
        {
            [Required]
            public DateTime StartDate { get; set; }

            [Required]
            public DateTime EndDate { get; set; }
        }
    }
}

