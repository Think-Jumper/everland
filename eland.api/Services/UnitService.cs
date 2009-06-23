using eland.model.Units;

namespace eland.api.Services
{
    public class UnitService
    {
        public static Unit Create()
        {
            return new Archer();
        }
    }
}
