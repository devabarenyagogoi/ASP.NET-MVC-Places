using System.ComponentModel.DataAnnotations;

namespace FirstDotNETApp.ViewModels
{
    public class StateViewModel
    {
        [Display(Name = "State Id")]
        public int StateId { get; set; }

        [Display(Name = "State Name")]
        public string StateName { get; set; } = null!;

        [Display(Name = "Country Id")]
        public int? CountryId { get; set; }

        [Display(Name = "Country Name")]
        public string? CountryName { get; set; }
    }
}