using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;

namespace RPG_V0._3
{
    public enum GameState
    {
        MainMenu,
        CharacterCreate,
        NewGame,
        LoadGame,
        Pause,
        GameplayLoop,
        Quit,
        Combat,
    }

    static class GameManager
    {
        public static GameState gameState;

        private static AttributesStruct NewPlayer;

        public static void Initialise()
        {
            gameState = GameState.MainMenu; //set default gamestate
        }


        public static void CharCreationStatsChange(AttributesStruct statsToADD)
        {
            NewPlayer.Age = NewPlayer.Age + statsToADD.Age;
            NewPlayer.Charisma = NewPlayer.Charisma + statsToADD.Charisma;
            NewPlayer.Constitution = NewPlayer.Constitution + statsToADD.Constitution;
            NewPlayer.Dexterity = NewPlayer.Dexterity + statsToADD.Dexterity;
            NewPlayer.Intelligence = NewPlayer.Intelligence + statsToADD.Intelligence;
            NewPlayer.Strength = NewPlayer.Strength + statsToADD.Strength;
        }
    }
}