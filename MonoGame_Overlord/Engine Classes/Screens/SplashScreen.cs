using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Overlord
{
    public class SplashScreen : GameScreen
    {
        public Image Image;

        public SplashScreen()
        {
            Type = GetType();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            Image.Update(time);

            if (InputManager.Instance.KeyPressed(Keys.Enter, Keys.Space))
                ScreenManager.Instance.ChangeScreens("TitleScreen");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
