using System;

namespace eland.model.Interfaces
{
    public interface IUnit
    {
        String Name { get; set; }
        IUnitType Type { get; set;  }
    }
}