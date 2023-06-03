using System;
using System.Collections.Generic;

namespace travelApp.Models;

public partial class Vehiculo
{
    public int Id { get; set; }

    public int IdTipo { get; set; }

    public string Patente { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public virtual TiposV IdTipoNavigation { get; set; } = null!;

    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
