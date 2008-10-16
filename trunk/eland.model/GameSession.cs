using System;

namespace eland.model
{
   public class GameSession
   {
      public virtual Guid Id { get; set; }
      public virtual DateTime EnteredGame { get; set; }
      public virtual DateTime? LeftGame { get; set; }
      public virtual Game Game { get; set; }
      public virtual User User { get; set; }
      public virtual Nation Nation { get; set; }
   }
}