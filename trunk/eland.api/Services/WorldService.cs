using eland.model;
using eland.model.Enums;

namespace eland.api.Services
{
    public class WorldService
    {

        //TODO: inject repository / datacontext
        //TODO: use service locator

        public static World Create(int height, int width)
        {
            var world = new World { Height = height, Width = width, Name = "TestWorld" };

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
