using System;
using eland.model;

namespace eland.api.Services
{
    public class GameService
    {
        public static Game Create(World world)
        {
            return new Game {GameWorld = world, Name = "default_new_game", Started = DateTime.Now};
        }

        public static GameSession CreateSession(Game game, User user)
        {
            return new GameSession { EnteredGame = DateTime.Now, Game = game, Nation = null, User = user };
        }
    }
}
