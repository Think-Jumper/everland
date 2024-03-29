﻿using System;
using eland.model;
using eland.model.Enums;

namespace eland.api.Services
{
    public class GameService
    {
        public static Game Create(World world)
        {
            return new Game {GameWorld = world, Name = "default_new_game", Started = DateTime.Now};
        }

        public static GameSession CreateSession(User user, Race race)
        {
            var nation = new Nation { Name = "Default Nation", Race = race };
            var world = new World { Height = 100, Width = 100, Name = "Default World" };
            var game = new Game { Name = "Default Game", Started = DateTime.Now, GameWorld = world };
            var gameSession = new GameSession { EnteredGame = DateTime.Now, Nation = nation, Game = game, User = user };

            for (var y = 1; y <= world.Width; y++)
            {
                for (var x = 1; x <= world.Height; x++)
                {
                    world.AddHex(new Hex { World = world, HexType = HexType.Grass, X = x, Y = y });
                }
            }

            nation.AddUnit(new Unit());
            nation.AddUnit(new Unit());
            nation.AddUnit(new Unit());
            nation.AddUnit(new Unit());

            return gameSession;

        }

    }
}
