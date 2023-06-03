using System;
using System.Collections.Generic;

namespace travelApp.Models;

public partial class Viaje
{
    public int Id { get; set; }

    public int IdCiudad { get; set; }

    public int IdVehiculo { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Ciudad IdCiudadNavigation { get; set; } = null!;

    public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
}
