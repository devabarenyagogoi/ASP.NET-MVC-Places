using FirstDotNETApp.ViewModels;

namespace FirstDotNETApp.Interfaces
{
    public interface IStateService
    {
        Task<List<StateViewModel>> GetAllStatesAsync();

        Task<StateViewModel?> GetStateByIdAsync(int id);

        Task CreateStateAsync(StateViewModel vm);

        Task UpdateStateAsync(StateViewModel vm);

        Task DeleteStateAsync(int id);

        Task<bool> StateExistsAsync(int id);

    }
}