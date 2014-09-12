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
using SkyView.Classes.Menues.Menues;
using SkyView.Classes.Logic;

namespace SkyView.Classes.Menues
{
    public class MenuSystem
    {
        private Menu _ActiveMenu = null;
        private MainMenu _MainMenu = new MainMenu();
        private EndGameWinMenu _EndGameWinMenu = new EndGameWinMenu();
        private EndGameFailMenu _EndGameFailMenu = new EndGameFailMenu();

        private GraphicsDevice _Graphics;
        private ContentManager _Content;

        public MenuSystem()
        {
            _ActiveMenu = _MainMenu;
        }

        public void LoadConetent( GraphicsDevice graphics, ContentManager content )
        {
            _Graphics = graphics;
            _Content = content;

            _MainMenu.LoadConetent( graphics, content );
            _EndGameWinMenu.LoadConetent( graphics, content );
            _EndGameFailMenu.LoadConetent( graphics, content );
        }

        public void Update( GameTime gameTime, KeyboardState keyboardState )
        {
            _ActiveMenu.Update( gameTime, keyboardState );
        }

        public void Draw( GameTime gameTime )
        {
            _ActiveMenu.Draw( gameTime );
        }

        public void SetActiveMenu( int menuID )
        {
            if ( menuID == Menues.Menues.MAIN )
            {
                _MainMenu.MenuRunning = true;
                _ActiveMenu = _MainMenu;
                SkyView.Instance.CurrentAudioManager.StopRepeatingCues();
                SkyView.Instance.CurrentAudioManager.SetRepeatingCue( "Zelda Skyward Sword Music   Song of the Hero" );
            }
            else if ( menuID == Menues.Menues.ENDGAMEWIN )
            {
                _EndGameWinMenu.MenuRunning = true;
                _ActiveMenu = _EndGameWinMenu;
                SkyView.Instance.CurrentAudioManager.StopRepeatingCues();
                SkyView.Instance.CurrentAudioManager.SetRepeatingCue( "Zelda Skyward Sword Music   Song of the Hero" );
            }

            else if ( menuID == Menues.Menues.ENDGAMEFAIL )
            {
                _EndGameWinMenu.MenuRunning = true;
                _ActiveMenu = _EndGameFailMenu;
                SkyView.Instance.CurrentAudioManager.StopRepeatingCues();
                SkyView.Instance.CurrentAudioManager.SetRepeatingCue( "Zelda Skyward Sword Music   Song of the Hero" );
            }
        }

        public void ReloadMainMenu()
        {
            _MainMenu = new MainMenu();
            _MainMenu.LoadConetent( _Graphics, _Content );
        }

        public Menu ActiveMenu
        {
            get { return _ActiveMenu; }
        }

    }
}
