using HotelService.ClientApi.Entities;
using HotelService.Promotion.Storage.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelService.Reservation.Storage.Entities
{
        [Table("Reservations", Schema = "Hotel")]
        public class Reservation
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public int ClientId { get; set; }

            [Required]
            public DateTime StartDate { get; set; }

            [Required]
            public DateTime EndDate { get; set; }

            [Required]
            public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

            public int? PromotionId { get; set; }

    
    }

        public enum ReservationStatus
        {
            Pending,
            Confirmed,
            Cancelled
        }
    }
