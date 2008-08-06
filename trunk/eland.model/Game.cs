using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eland.model
{
   public class Game
   {
      public virtual Guid Id { get; set; }
      public virtual string Name { get; set; }
      public virtual DateTime Started { get; set; }
      public virtual DateTime? Finished { get; set; }
   }
}
