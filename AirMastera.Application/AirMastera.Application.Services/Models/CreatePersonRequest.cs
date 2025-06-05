using System.ComponentModel.DataAnnotations;

namespace AirMastera.Application.Services.Models;

public class CreatePersonRequest
{
    //DataAnotations
    //[Required(ErrorMessage = "Имя обязательно")]
    //[StringLength(50, MinimumLength = 3, ErrorMessage = "Имя должно быть от 3 до 50 символов")]
    public string FullName { get; set; }
    public string Phone { get; set; }
}