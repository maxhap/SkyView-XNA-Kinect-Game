using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using SkyView.Classes.Logic;

namespace SkyView.Classes.Menues.Menues
{
    public class Menu
    {
        protected GraphicsDevice _GraphicsDevice;
        protected ContentManager _ContentManager;

        protected List<MenuWindow> _MenuList;
        protected MenuWindow _ActiveMenu;
        protected KeyboardState _LastKeybState;

        public bool MenuRunning { get; set; }
        protected SpriteBatch _SpriteBatch = null;
        protected bool _MenuActive = false;

        public Menu()
        {

        }

        public virtual void LoadConetent( GraphicsDevice graphics, ContentManager content )
        {

        }

        public virtual void Update( GameTime gameTime, KeyboardState keybaordState )
        {

        }

        protected virtual void HandleChange( MenuWindow newActive )
        {

        }

        protected void MenuInput( KeyboardState currentKeybState )
        {
            MenuWindow newActive = _ActiveMenu.ProcessInput( _LastKeybState, currentKeybState );
            HandleChange( newActive );
        }

        public virtual void Draw( GameTime gameTime )
        {

        }

        public void MenuItemUp()
        {
            _ActiveMenu.MenuItemUp();
        }

        public void MenuItemDown()
        {
            _ActiveMenu.MenuItemDown();
        }

        public void MenuItemSelect()
        {
            HandleChange( _ActiveMenu.CurentMenuItemLink() );
        }
    }
}
