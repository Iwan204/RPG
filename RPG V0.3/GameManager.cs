using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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

        public static Dictionary<string,Character> CharacterDictionary;
        public static AttributesStruct NewPlayer;
        public static string NewPlayerName;

        public static void Initialise(ContentManager Content)
        {
            gameState = GameState.MainMenu; //set default gamestate
            NewPlayer = new AttributesStruct();
            NewPlayerName = "Default";

            CharacterDictionary = new Dictionary<string, Character>();

            CharacterDictionary["0001"] = new Character(
                new AttributesStruct(),
                new Vector2(2,2),
                Content,
                "Test"
                );
        }

        public static void Draw(SpriteFont soritefont)
        {
            switch (gameState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.CharacterCreate:
                    break;
                case GameState.NewGame:
                    break;
                case GameState.LoadGame:
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameplayLoop:
                    break;
                case GameState.Quit:
                    break;
                case GameState.Combat:
                    break;
                default:
                    break;
            }
        }
    }
}