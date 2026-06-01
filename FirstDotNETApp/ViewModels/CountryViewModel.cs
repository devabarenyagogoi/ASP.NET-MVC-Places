using System.ComponentModel.DataAnnotations;

namespace FirstDotNETApp.ViewModels
{
    public class CountryViewModel
    {
        [Display(Name = "Country Id")]
        public int CountryId { get; set; }

        [Display(Name = "Country Name")]
        public string CountryName { get; set; }
    }
}
