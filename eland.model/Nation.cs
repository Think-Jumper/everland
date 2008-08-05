using System;

namespace eland.model
{
   public class Nation
   {
      public virtual Guid Id { get; set; }
      public virtual string Name{ get; set; }
      public virtual Race Race { get; set; }
   }
}
