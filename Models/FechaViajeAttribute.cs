using System.ComponentModel.DataAnnotations;

public class FechaViajeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime fechaViaje = (DateTime)value;
        DateTime fechaMinima = DateTime.Now.AddDays(1);
        DateTime fechaMaxima = DateTime.Now.AddDays(10);

        if (fechaViaje >= fechaMinima && fechaViaje <= fechaMaxima)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("La fecha debe estar dentro de los próximos 10 días.");
    }
}
