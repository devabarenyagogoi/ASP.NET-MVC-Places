using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstDotNETApp.Models;

public partial class State
{  
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public int? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();
}
