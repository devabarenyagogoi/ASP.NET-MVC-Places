// String, Int32, DateTime
using System;
// ICollection<State>, List<State>
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstDotNETApp.Models;

// Defines a model class named Country
// Partial -> Class can be split into multiple files
public partial class Country
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    // Navigation property
    // virtual -> Allows entity framework features like: lazy loading, proxy creation
    // ICollection<State> -> A collection of state objects (state: another model class)
    // States -> Property name
    // new List<State>() -> creates a new empty list initially (country.States.Add(...) would crash with NullReferenceException)
    // Overall this property stores multiple State objects
    public virtual ICollection<State> States { get; set; } = new List<State>();
}
