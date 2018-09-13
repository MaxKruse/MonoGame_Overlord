using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MonoGame_Overlord
{
    public class GameScreen
    {
        protected ContentManager content;
        public Type Type;
        public Vector2 Dimensions;


        public GameScreen()
        {
            Type = GetType();
        }

        public virtual void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime time)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, string Text = "")
        {

        }
    }
}
