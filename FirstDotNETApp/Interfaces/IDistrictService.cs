using FirstDotNETApp.ViewModels;

namespace FirstDotNETApp.Interfaces
{
    public interface IDistrictService
    {
        Task<List<DistrictViewModel>> GetAllDistrictsAsync();

        Task<DistrictViewModel?> GetDistrictByIdAsync(int id);

        Task CreateDistrictAsync(DistrictViewModel vm);

        Task UpdateDistrictAsync(DistrictViewModel vm);

        Task DeleteDistrictAsync(int id);

        Task<bool> DistrictExistsAsync(int id);
    }
}
