using System.ComponentModel.DataAnnotations;

namespace AirMastera.Infrastructure.Identity.Models;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
}