using System.ComponentModel.DataAnnotations;

namespace HotelService.Promotion.CrossCutting.Dtos
{
    public static class PromotionDto
    {
        public class Read
        {
            public int Id { get; set; }
            public string Code { get; set; } = default!;
            public decimal DiscountPercentage { get; set; }
            public DateTime? ValidUntil { get; set; }
        }

        public class Create
        {
            [Required]
            [MaxLength(50)]
            public string Code { get; set; } = default!;

            [Required]
            [Range(0, 100)]
            public decimal DiscountPercentage { get; set; }

            public DateTime? ValidUntil { get; set; }
        }
    }
}
