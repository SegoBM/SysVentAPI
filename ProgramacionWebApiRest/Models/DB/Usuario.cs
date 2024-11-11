using System;
using System.Collections.Generic;

namespace ProgramacionWebApiRest.Models.DB;

public partial class Usuario
{
    public Guid UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string Rol { get; set; } = null!;
}
