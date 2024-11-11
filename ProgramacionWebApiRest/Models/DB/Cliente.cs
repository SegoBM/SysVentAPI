using System;
using System.Collections.Generic;

namespace ProgramacionWebApiRest.Models.DB;

public partial class Cliente
{
    public Guid ClienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }




}
