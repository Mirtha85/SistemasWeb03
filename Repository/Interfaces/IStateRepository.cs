using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface IStateRepository
    {
        IEnumerable<State> AllStates { get; }
        IEnumerable<State> StatesByCountryId(int countryId);
        State? GetStateById(int id);

        void CreateState(State state);

        void EditState(State state);

        void DeleteState(State state);
    }
}
