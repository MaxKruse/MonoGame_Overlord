using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Xml.Serialization;

namespace MonoGame_Overlord
{
    public class ScreenManager
    {
        private static ScreenManager instance;
        public Vector2 Dimensions;
        private XmlManager<GameScreen> XmlGameScreenManager;
        private GameScreen currentScreen, newScreen;
        public Image Image;

        [XmlIgnore]
        public ContentManager Content;
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch SpriteBatch;
        [XmlIgnore]
        public bool IsTransitioning { get; private set; }


        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    XmlManager<ScreenManager> xml = new XmlManager<ScreenManager>();
                    instance = xml.Load("Load/ScreenManager.xml");
                }
                return instance;
            }
        }

        private ScreenManager()
        {
            currentScreen = new SplashScreen();

            XmlGameScreenManager = new XmlManager<GameScreen>();
            XmlGameScreenManager.Type = currentScreen.Type;
            currentScreen = XmlGameScreenManager.Load(currentScreen.XmlPath);

            Dimensions = new Vector2(1600, 900);
        }

        public void LoadContent(ContentManager content)
        {
            Content = content;
            currentScreen.LoadContent();
            Image.LoadContent();

        }

        public void UnloadContent()
        {
            Content.Unload();
            currentScreen.UnloadContent();
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if (IsTransitioning)
                Image.Draw(spriteBatch);
        }

        public void ChangeScreens(string screenName)
        {
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("MonoGame_Overlord." + screenName));
            Image.IsActive = true;
            Image.FadeEffect.Increase = true;
            Image.Alpha = 0.0f;
            IsTransitioning = true;
        }

        void Transition(GameTime gameTime)
        {
            if (IsTransitioning)
            {
                Image.Update(gameTime);
                if (Image.Alpha >= 1.0f)
                {
                    currentScreen.UnloadContent();
                    currentScreen = newScreen;

                    XmlGameScreenManager.Type = currentScreen.Type;

                    if (File.Exists(currentScreen.XmlPath))
                        currentScreen = XmlGameScreenManager.Load(currentScreen.XmlPath);
                    currentScreen.LoadContent();
                }
                else if (Image.Alpha <= 0.0f)
                {
                    Image.IsActive = false;
                    IsTransitioning = false;
                }
            }
        }


    }
}
