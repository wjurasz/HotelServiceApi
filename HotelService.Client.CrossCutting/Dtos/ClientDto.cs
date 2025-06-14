using System.ComponentModel.DataAnnotations;

namespace HotelService.Client.CrossCutting.DTOs
{
    public static class ClientDto
    {
        public class Create
        {
            [Required]
            [MaxLength(200)]
            public string FirstName { get; set; } = default!;

            [Required]
            [MaxLength(200)]
            public string LastName { get; set; } = default!;

            [Required]
            [EmailAddress]
            public string Email { get; set; } = default!;

            [Required]
            [MinLength(9)]
            [MaxLength(15)]
            public string PhoneNumber { get; set; } = default!;
        }

        public class Read
        {
            public int Id { get; set; }
            public string FirstName { get; set; } = default!;
            public string LastName { get; set; } = default!;
            public string Email { get; set; } = default!;
            public string PhoneNumber { get; set; } = default!;
        }

        public class Update
        {
            [Required]
            [MaxLength(200)]
            public string FirstName { get; set; } = default!;

            [Required]
            [MaxLength(200)]
            public string LastName { get; set; } = default!;

            [Required]
            [EmailAddress]
            public string Email { get; set; } = default!;

            [Required]
            [MinLength(9)]
            [MaxLength(15)]
            public string PhoneNumber { get; set; } = default!;
        }
    }
}
