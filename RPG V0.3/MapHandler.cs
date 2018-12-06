using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;

namespace RPG_V0._3
{
    public struct Level
    {
        public TiledMap Map;
        public string MapName;
        public TiledMapObjectLayer ObjectLayer;

        public int[,] ElevationMatrix;
        public int[,] NavigationMatrix;

        public Level(TiledMap map)
        {
            Map = map;
            map.Properties.TryGetValue("MapName", out MapName);
            ObjectLayer = map.GetLayer<TiledMapObjectLayer>("GameObjects");
            NavigationMatrix = new int[map.Width,map.Height];
            ElevationMatrix = new int[map.Width, map.Height];
        }
    }

    static class MapHandler
    {
        public static List<Level> availableMaps;
        public static TiledMapRenderer mapRenderer;
        public static Level currentLevel;
        public static TiledMapObjectLayer objectLayer;

        private static ContentManager Content;
        private static GraphicsDevice graphicsDevice;

        public static void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            Content = content;
            graphicsDevice = graphics;
            availableMaps = new List<Level>();
            mapRenderer = new TiledMapRenderer(graphicsDevice);
            availableMaps.Add(new Level(content.Load<TiledMap>(@"Maps\DEMO2")));
            availableMaps.Add(new Level(content.Load<TiledMap>(@"Maps\ObjectTest")));

            currentLevel = availableMaps.First(); 

        }

        public static void LoadMap(string loadMapName)
        {
            //if the current map is loaded
            //note: make map, mapname and objectlayer a struct called level for easy access
            if (loadMapName != currentLevel.MapName)
            {
                foreach (var map in availableMaps)
                {
                    var nameToComapre = "";
                    map.Map.Properties.TryGetValue("MapName", out nameToComapre);
                    if (nameToComapre == loadMapName)
                    {
                        //map found in available maps
                        currentLevel = map;
                        objectLayer = currentLevel.ObjectLayer;
                        //set parameters from objects in map
                        foreach (var entity in objectLayer.Objects)
                        {
                            //camerastart parameter
                            if (entity.Name == "CameraStart")
                            {
                                Camera.camera.Position = entity.Position;
                            }
                        }
                    }
                }
            }
        }

        public static void Update(GameTime gameTime)
        {
            switch (GameManager.gameState)
            {
                case GameState.MainMenu:
                    LoadMap("Demo");
                    mapRenderer.Update(currentLevel.Map, gameTime);
                    break;
                case GameState.CharacterCreate:
                    LoadMap("Demo");
                    mapRenderer.Update(currentLevel.Map, gameTime);
                    break;
                case GameState.NewGame:
                    break;
                case GameState.LoadGame:
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameplayLoop:
                    mapRenderer.Update(currentLevel.Map, gameTime);
                    break;
                case GameState.Quit:
                    break;
                case GameState.Combat:
                    break;
                default:
                    break;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            switch (GameManager.gameState)
            {
                case GameState.NewGame:
                    break;
                case GameState.LoadGame:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Quit:
                    break;
                case GameState.Combat:
                    break;
                default:
                    spriteBatch.Begin(transformMatrix: Camera.camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
                    mapRenderer.Draw(currentLevel.Map, Camera.camera.GetViewMatrix());
                    spriteBatch.End();
                    break;
            }
        }

        public static void DrawLayer(SpriteBatch spriteBatch, string LayerName)
        {
            spriteBatch.Begin(transformMatrix: Camera.camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            try
            {
                TiledMapLayer layer = currentLevel.Map.GetLayer(LayerName);
                mapRenderer.Draw(layer, Camera.camera.GetViewMatrix());
            }
            catch (Exception)
            {

                //if no layer exists nothing will happen
            }
            spriteBatch.End();
        }

    }
}
