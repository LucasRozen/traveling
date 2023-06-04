using System;
using System.Collections.Generic;

namespace travelApp.Models;

public partial class TipoV
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
