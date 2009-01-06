using eland.model.Interfaces;

namespace eland.model.Interfaces
{
    public interface IRangedUnit : IOffensiveUnit
    {
        int Range { get; }
    }
}