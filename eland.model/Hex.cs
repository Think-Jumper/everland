using System;

namespace eland.model
{
   public class Hex
   {
      public virtual Guid Id { get; set; }
      public virtual int X { get; set; }
      public virtual int Y { get; set; }
      public virtual World World { get; set; }
      public virtual HexType HexType { get; set; }
   }
}
