﻿using System;

namespace eland.model
{
   public class Race
   {
      public virtual Guid Id { get; set; }
      public virtual string Name { get; set; }
      public virtual string Description { get; set; }
      public virtual string ImageFileName { get; set; }
       
   }
}
