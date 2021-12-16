using System.ComponentModel.DataAnnotations;

namespace Assessment.Health.Domain.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
