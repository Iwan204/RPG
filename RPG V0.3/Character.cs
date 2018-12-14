using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG_V0._3
{
    public class Character : Entity
    {
        public AttributesStruct Attributes;
        public int Elevation;
        public bool IsSelected;
        public bool IsPlayer;

        public Character(AttributesStruct att,Vector2 pos, ContentManager content, string name)
        {
            Attributes = att;
            Name = name;
            Initialize(content, pos);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, spriteBox, Color.Black);
            Console.WriteLine(Name + " Drawn at "+ spriteBox );
        }

        public override void Initialize(ContentManager content, Vector2 position)
        {
            IsCollidable = true;
            IsSelected = false;
            sprite = content.Load<Texture2D>("Characters/DebugDefault");
            spriteBox = new Rectangle((int)Position.X - 32, (int)Position.Y - 64, 32, 32);
            boundingBox = new Rectangle((int)Position.X - 32, (int)Position.Y - 64, 64, 32);
            Elevation = 0;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 TempPosition = Camera.camera.Position - Position;
            spriteBox = new Rectangle((int)TempPosition.X - 32, (int)TempPosition.Y - 64, 32, 32);
            boundingBox = new Rectangle((int)TempPosition.X - 32, (int)TempPosition.Y - 64, 64, 32);
        }
    }


}
