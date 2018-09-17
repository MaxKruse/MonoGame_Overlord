using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml.Serialization;


namespace MonoGame_Overlord
{
    public class GameScreen
    {
        protected ContentManager Content;

        [XmlIgnore]
        public Type Type;

        public string XmlPath;

        public GameScreen()
        {
            Type = GetType();
            XmlPath = "Load/" + Type.Name + ".xml";
        }

        public virtual void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
            Content.Unload();
            Content.Dispose();
        }

        public virtual void Update(GameTime gameTime)
        {
            InputManager.Instance.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
