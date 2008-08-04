using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eland.api.Interfaces;
using eland.api;
using eland.model;

namespace eland.Utilities
{
   public class DataContext : IDataContext
   {
      public IRepository<User> UserRepository { get; set; }
      public IRepository<Game> GameRepository { get; set; }
      public IRepository<Hex> HexRepository { get; set; }
      public IRepository<HexType> HexTypeRepository { get; set; }
      public IRepository<World> WorldRepository { get; set; }

   }
}
