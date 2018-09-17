using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonoGame_Overlord
{
    public class MenuManager
    {
        Menu menu;

        public MenuManager()
        {
            menu = new Menu();
            menu.OnMenuChange += Menu_OnMenuChange;
        }

        private void Menu_OnMenuChange(object sender, System.EventArgs e)
        {
            XmlManager<Menu> xmlManager = new XmlManager<Menu>();
            menu.UnloadContent();
            menu = xmlManager.Load(menu.Id);
            menu.LoadContent();
        }

        public void LoadContent(string menuPath)
        {
            if (menuPath != string.Empty)
                menu.Id = menuPath;
        }

        public void UnloadContent()
        {
            menu.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            menu.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
