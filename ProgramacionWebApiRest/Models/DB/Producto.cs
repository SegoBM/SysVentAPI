using System;
using System.Collections.Generic;

namespace ProgramacionWebApiRest.Models.DB;

public partial class Producto
{
    public Guid ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int Cantidad { get; set; }

    public Guid? ProveedorId { get; set; }

   
}
