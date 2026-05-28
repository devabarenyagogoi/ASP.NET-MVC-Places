using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstDotNETApp.Models;

public partial class District
{
    [Display(Name = "District Id")]
    public int DistrictId { get; set; }

    [Display(Name = "District Name")]
    public string DistrictName { get; set; } = null!;

    [Display(Name = "State Id")]
    public int? StateId { get; set; }

    public virtual State? State { get; set; }
}
