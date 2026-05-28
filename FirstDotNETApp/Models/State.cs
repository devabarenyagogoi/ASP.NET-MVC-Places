using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstDotNETApp.Models;

public partial class State
{
    [Display(Name = "State Id")]
    public int StateId { get; set; }

    [Display(Name = "State Name")]
    public string StateName { get; set; } = null!;

    [Display(Name = "Country Id")]
    public int? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();
}
