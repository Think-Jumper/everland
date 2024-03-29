﻿using eland.api.Interfaces;
using eland.model;

namespace eland.api
{
    public class DataContext : IDataContext
    {
        public IRepository<User> UserRepository { get; set; }
        public IRepository<Game> GameRepository { get; set; }
        public IRepository<Hex> HexRepository { get; set; }
        public IRepository<World> WorldRepository { get; set; }
        public IRepository<Nation> NationRepository { get; set; }
        public IRepository<Race> RaceRepository { get; set; }
        public IRepository<GameSession> GameSessionRepository { get; set; }
        public IRepository<Unit> UnitRepository  { get; set; }
      
    }
}