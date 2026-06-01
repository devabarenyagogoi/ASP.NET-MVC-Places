using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstDotNETApp.Models;

public partial class District
{
    public int DistrictId { get; set; }

    public string DistrictName { get; set; } = null!;

    public int? StateId { get; set; }

    public virtual State? State { get; set; }
}
