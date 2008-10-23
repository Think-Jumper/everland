﻿using System;
using System.Collections.Generic;

namespace eland.model
{
   public class World
   {
      public virtual Guid Id { get; set; }
      public virtual string Name { get; set; }
      public virtual int Width { get; set; }
      public virtual int Height { get; set; }
      public virtual IList<Hex> Hexes { get; set; }
      public virtual Game Game { get; set; }
   }
}