﻿using System;
using System.Collections.Generic;

namespace BlanquitaAPI.Data.BankModels;

public partial class TipoProducto
{
    public int IdTipoProducto { get; set; }

    public string Clave { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
