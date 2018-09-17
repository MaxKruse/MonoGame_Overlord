
using Microsoft.Xna.Framework;

namespace MonoGame_Overlord
{
    public class ImageEffect
    {
        protected Image Image;
        public bool IsActive;

        public ImageEffect()
        {
            IsActive = false;
        }

        public virtual void LoadContent(ref Image image)
        {
            Image = image;
        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
