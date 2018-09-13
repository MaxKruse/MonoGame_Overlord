using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonoGame_Overlord
{
    public class SplashScreen : GameScreen
    {
        Texture2D image;
        SpriteFont font;
        public string ImagePath;
        public string FontPath;
        private float scale;
        public Vector2 SplashDimensions;

        public override void LoadContent()
        {
            base.LoadContent();
            image = content.Load<Texture2D>(ImagePath);
            font = content.Load<SpriteFont>(FontPath);
            font.LineSpacing = 3;
            SplashDimensions = Dimensions;
            scale = (SplashDimensions.X / image.Width);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch, string Text = "")
        {
            spriteBatch.Draw(image, Vector2.Zero, new Rectangle(0, 0, image.Width, image.Height), Color.White, 0.0f, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 1.0f);

            spriteBatch.DrawString(font, Text, new Vector2(40, 90), new Color(24, 28, 26));
        }
    }
}
