using System.ComponentModel.DataAnnotations;

namespace FirstDotNETApp.ViewModels
{
    public class DistrictViewModel
    {
        [Display(Name = "District Id")]
        public int DistrictId { get; set; }

        [Display(Name = "District Name")]
        public string DistrictName { get; set; } = null!;

        [Display(Name = "State Id")]
        public int? StateId { get; set; }

        [Display(Name = "State Name")]
        public string? StateName { get; set; }
    }
}