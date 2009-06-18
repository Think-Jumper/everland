using eland.model;
using eland.model.Enums;

namespace eland.api.Services
{
    public class WorldService
    {

        //TODO: inject repository / datacontext

        // use service locator

        public static World Create()
        {
            var world = new World { Height = 100, Width = 100, Name = "Default World" };

            for (var y = 1; y <= world.Width; y++)
            {
                for (var x = 1; x <= world.Height; x++)
                {
                    world.AddHex(new Hex { World = world, HexType = HexType.Grass, X = x, Y = y });
                }
            }

            // should persist?

            return world;
        }

        public World Get()
        {
            return null;
        }
    }
}
