using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eland.model
{
   public class Hex
   {
      public virtual Guid Id { get; set; }
      public virtual int X { get; set; }
      public virtual int Y { get; set; }
      public virtual World world { get; set; }
   }
}
