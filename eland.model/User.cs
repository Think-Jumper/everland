using System;

namespace eland.model
{
   public class User
   {
      public virtual Guid Id { get; set; }
      public virtual string OpenId { get; set; }
      public virtual string FirstName { get; set; }
      public virtual string LastName { get; set; }
      public virtual string Email { get; set; }
   }
}
