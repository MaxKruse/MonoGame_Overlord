using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace MonoGame_Overlord
{
    public class ScreenManager
    {
        private static ScreenManager instance;
        public Vector2 Dimensions;
        public ContentManager Content { private set; get; }

        private XmlManager<GameScreen> XmlGameScreenManager;

        private GameScreen currentScreen;

        public static ScreenManager Instance
        {
            get
            {
                return instance ?? (instance = new ScreenManager());
            }
        }

        private ScreenManager()
        {
            XmlGameScreenManager = new XmlManager<GameScreen>();

            currentScreen = new SplashScreen();
            XmlGameScreenManager.Type = currentScreen.Type;
            currentScreen = XmlGameScreenManager.Load("Load/SplashScreen.xml");

            Dimensions = currentScreen.Dimensions;

        }

        public void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");
            currentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            Content.Unload();
            currentScreen.UnloadContent();
        }

        public void Update(GameTime time)
        {
            currentScreen.Update(time);
        }

        public void Draw(SpriteBatch spriteBatch, string Text = "")
        {
            currentScreen.Draw(spriteBatch, Text);
        }


    }
}
