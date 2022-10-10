using System.ComponentModel.DataAnnotations;

namespace Sample.Account.Api.Models
{
    public class SignUpModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
