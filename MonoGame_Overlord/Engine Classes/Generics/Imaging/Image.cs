using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;



namespace MonoGame_Overlord
{
    public class Image
    {
        public float Alpha;
        public string Text, FontName, Path;
        public Vector2 Position, Scale;
        public Rectangle SourceRect;
        public Vector2 Dimensions;
        public bool IsActive;

        Texture2D texture;
        Vector2 origin;
        ContentManager content;
        RenderTarget2D RenderTarget;
        SpriteFont font;

        Dictionary<string, ImageEffect> effectList;
        public string Effects;

        public FadeEffect FadeEffect;

        public Image()
        {
            Alpha = 1.0f;
            Path = Text = Effects = String.Empty;
            FontName = "Fonts/Calibri_48";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            SourceRect = Rectangle.Empty;
            Dimensions = Vector2.Zero;

            origin = Vector2.Zero;

            IsActive = true;

            effectList = new Dictionary<string, ImageEffect>();
        }

        public void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            font = content.Load<SpriteFont>(FontName);


            if (Path != String.Empty)
            {
                texture = content.Load<Texture2D>(Path);
            }

            if (Dimensions.Y == 0 && Dimensions.Y == 0)
            {
                if (texture != null)
                {
                    Dimensions.X = texture.Width;
                }

                Dimensions.X += font.MeasureString(Text).X;

                if (texture != null)
                {
                    Dimensions.Y = Math.Max(texture.Height, font.MeasureString(Text).Y);
                }
                else
                {
                    Dimensions.Y = font.MeasureString(Text).Y;
                }

            }

            if (SourceRect == Rectangle.Empty)
            {
                SourceRect = new Rectangle(0, 0, (int)Dimensions.X, (int)Dimensions.Y);
            }


            RenderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)Dimensions.X, (int)Dimensions.Y);
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(RenderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();
            if (texture != null)
                ScreenManager.Instance.SpriteBatch.Draw(texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.DrawString(font, Text, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.End();

            texture = RenderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            SetEffect<FadeEffect>(ref FadeEffect);

            if (Effects != String.Empty)
            {
                string[] split = Effects.Split(':');

                foreach (string item in split)
                    ActivateEffect(item);
            }
        }

        public void UnloadContent()
        {
            content.Unload();
            foreach (var effect in effectList)
                DeactivateEffect(effect.Key);
            content.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in effectList)
            {
                if (effect.Value.IsActive)
                    effect.Value.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);
            spriteBatch.Draw(texture, Position + origin, SourceRect, Color.White * Alpha, 0.0f, origin, Scale, SpriteEffects.None, 0.0f);
        }

        void SetEffect<T>(ref T effect)
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }

            effectList.Add(effect.GetType().ToString().Replace("MonoGame_Overlord.", ""), (effect as ImageEffect));
        }

        public void ActivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = false;
                effectList[effect].UnloadContent();
            }
        }
    }
}
