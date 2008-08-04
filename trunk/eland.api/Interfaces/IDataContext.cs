﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eland.model;

namespace eland.api.Interfaces
{
   public interface IDataContext
   {
      IRepository<User> UserRepository { get; set; }
      IRepository<Game> GameRepository { get; set; }
      IRepository<Hex> HexRepository { get; set; }
      IRepository<HexType> HexTypeRepository { get; set; }
      IRepository<World> WorldRepository { get; set; }
   }
}
