using System;
using System.Collections.Generic;

namespace ProgramacionWebApiRest.Models.DB;

public partial class Proveedore
{
    public Guid ProveedorId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Direccion { get; set; }

}
