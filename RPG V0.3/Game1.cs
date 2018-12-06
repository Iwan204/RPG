using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using System;
using System.IO;

namespace RPG_V0._3
{

    [System.Serializable]
    public struct AttributesStruct
    {
        public int Strength;
        public int Dexterity;
        public int Constitution;
        public int Intelligence;
        public int Charisma;
        public int Age;
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Cursor Cursor;
        //
        public Screen currentScreen;
        
        public SpriteFont GreySpriteFont;
        public SpriteFont Nixie;
        public Texture2D GreyImageMap;
        public string GreyMap;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Cursor = new Cursor(Content);
            Camera.Initialise(graphics.GraphicsDevice);
            MapHandler.Initialize(Content, graphics.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Gui Initialisation
            GreyImageMap = Content.Load<Texture2D>(@"GreySkin\ImageMap");
            GreyMap = File.OpenText(@"Content\GreySkin\Map.txt").ReadToEnd();
            GreySpriteFont = Content.Load<SpriteFont>(@"Fonts\MenuFont");
            Nixie = Content.Load<SpriteFont>(@"Fonts\Nixie");
            currentScreen = new MainMenuScreen();
            currentScreen.Init(this);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Cursor.Update(gameTime);
            currentScreen.Update(gameTime);

            MapHandler.Update(gameTime);

            switch (GameManager.gameState)
            {
                case GameState.MainMenu:
                    Camera.UpdateMainMenu(gameTime);
                    break;
                case GameState.NewGame:
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameplayLoop:
                    break;
                case GameState.Combat:
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            MapHandler.Draw(spriteBatch);
            currentScreen.Draw();
            Cursor.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
