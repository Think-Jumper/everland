using System;
using System.Collections;

namespace eland.model
{
    public class Settlement
    {
        public virtual String Name { get; set; }
        public virtual int Size { get; protected set; }
        public virtual Nation Ruler { get; set; }
        public virtual Hex Location { get; protected set; }

        public ReadOnlyCollectionBase Units
        {
            get { throw new NotImplementedException(); }
        }
    }
}
