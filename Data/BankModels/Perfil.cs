using System;
using System.Collections.Generic;

namespace BlanquitaAPI.Data.BankModels;

public partial class Perfil
{
    public int IdPerfil { get; set; }

    public string Clave { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
