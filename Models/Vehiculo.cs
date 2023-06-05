using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace travelApp.Models;

public partial class Vehiculo
{
    public Vehiculo()
    {
    }

    public int Id { get; set; }

    public int IdTipo { get; set; }

    [Required]
    [RegularExpression("^(?:[A-Z]{2}\\s?-?\\d{3}\\s?-?[A-Z]{2}|\\d{3}\\s?-?[A-Z]{3})?$", ErrorMessage = "La patente no cumple con el formato requerido")]
    public string Patente { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public virtual TipoV? IdTipoNavigation { get; set; } = null!;

    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
