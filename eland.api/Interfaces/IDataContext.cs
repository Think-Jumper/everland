using eland.model;
using eland.model.Units;

namespace eland.api.Interfaces
{
    public interface IDataContext
    {
        IRepository<User> UserRepository { get; set; }
        IRepository<Game> GameRepository { get; set; }
        IRepository<Hex> HexRepository { get; set; }
        IRepository<HexType> HexTypeRepository { get; set; }
        IRepository<World> WorldRepository { get; set; }
        IRepository<Nation> NationRepository { get; set; }
        IRepository<Race> RaceRepository { get; set; }
        IRepository<GameSession> GameSessionRepository { get; set; }
        IRepository<Unit> UnitRepository { get; set; }
    }
}