using System.ComponentModel.DataAnnotations;
using CineControl.OperatorPanel.Models.DTOs.Theaters;

namespace CineControl.OperatorPanel.Validators;

public class SeatsPerRowCapacity : ValidationAttribute
{
    public string GetErrorMessage()
        => "The number of seats per row cannot be greater than the seating capacity of the theater";
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var theater = (AddTheaterRequest)validationContext.ObjectInstance;

        if (theater.SeatsPerRow > theater.SeatingCapacity)
        {
            return new ValidationResult(GetErrorMessage());
        }

        return ValidationResult.Success;
    }
}