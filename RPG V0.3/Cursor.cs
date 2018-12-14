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
    public abstract class Entity
    {
        public abstract void Initialize(ContentManager content, Vector2 position);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        private Vector2 position;
        private string name;
        private Texture2D Sprite;
        private Rectangle Spritebox;
        private Rectangle BoundingBox;
        private bool Collision;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool IsCollidable
        {
            get { return Collision; }
            set { Collision = value; }
        }

        public Rectangle spriteBox
        {
            get { return Spritebox; }
            set { Spritebox = value; }
        }

        public Rectangle boundingBox
        {
            get { return BoundingBox; }
            set { BoundingBox = value; }
        }

        public Texture2D sprite
        {
            get { return Sprite; }
            set { Sprite = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    class Cursor : Entity
    {
        public Cursor(ContentManager content)
        {
            Initialize(content, new Vector2(0, 0));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBox = new Rectangle((int)(Camera.camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y))).X - 8, (int)(Camera.camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y))).Y - 16, 64, 64);
            boundingBox = new Rectangle((int)(Camera.camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y))).X - 8, (int)(Camera.camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y))).Y - 16, 16, 16);
            spriteBatch.Begin(transformMatrix: Camera.camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(sprite, spriteBox, Color.White);
            spriteBatch.End();
        }

        public override void Initialize(ContentManager content, Vector2 position)
        {
            sprite = content.Load<Texture2D>(@"Cursors\cursor");
            IsCollidable = false;
            Name = "Cursor";
        }

        public override void Update(GameTime gameTime)
        {
            /*
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                bool found = false;
                foreach (var player in PlayerManager.GetRawPlayerParty)
                {
                    if (boundingBox.Intersects(player.boundingBox))
                    {
                        //select player
                        if (player.IsSelected)
                        {
                            player.IsSelected = false;

                        }
                        else
                        {
                            player.IsSelected = true;
                        }
                        found = true;
                    }
                    if (found)
                    {
                        break;
                    }
                }
                if (!found)
                {
                    PlayerManager.selectedVector = new Vector2(boundingBox.Location.X, boundingBox.Location.Y);
                    Console.WriteLine("selected vector");
                    found = false;
                }
            }
            */
            //Console.WriteLine(Camera.camera.ScreenToWorld(Mouse.GetState().Position.X, Mouse.GetState().Position.Y).X / 2 + "," + Camera.camera.ScreenToWorld(Mouse.GetState().Position.X, Mouse.GetState().Position.Y).Y);

        }

        public Vector2 Tile(Vector2 point)
        {
            Vector2 tempPt = new Vector2(0, 0);

            tempPt.X = point.X - point.Y;

            tempPt.Y = (point.X + point.Y) / 2;

            return (tempPt);
        }

    }
}
