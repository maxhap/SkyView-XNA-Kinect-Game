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
    public class EndGameFailMenu : Menu
    {
        private MenuWindow _Window = null;
        private MenuWindow _RepeatStage = null;
        private MenuWindow _BackToMainMenu = null;

        public EndGameFailMenu() : base()
        {
            _MenuList = new List<MenuWindow>();
            MenuRunning = true; 
        }

        public override void LoadConetent( GraphicsDevice graphics, ContentManager content )
        {
            _GraphicsDevice = graphics;
            _ContentManager = content;

            _SpriteBatch = new SpriteBatch( _GraphicsDevice );
            SpriteFont menuFont = _ContentManager.Load<SpriteFont>( "Fonts\\font" );
            Texture2D backgroundImage = _ContentManager.Load<Texture2D>( "Backgrounds\\stagefail" );

            _Window = new MenuWindow( menuFont, "Stage Failed", backgroundImage );
            _Window.TextPosstionIncrease = new Vector2( 0, 100 );
            _RepeatStage = new MenuWindow( menuFont, "never seen", backgroundImage );
            _BackToMainMenu = new MenuWindow( menuFont, "never seen", backgroundImage );

            _Window.AddMenuItem( "Repeat Stage", _RepeatStage );
            _Window.AddMenuItem( "Back to Main Menu", _BackToMainMenu );
 

            _ActiveMenu = _Window;

            SkyView.Instance.CurrentAudioManager.SetRepeatingCue( "Zelda Skyward Sword Music   Song of the Hero" );
            _ActiveMenu.WakeUp();
        }

        public override void Update( GameTime gameTime, KeyboardState keybaordState )
        {
            if ( MenuRunning )
            {
                foreach ( MenuWindow currentMenu in _MenuList )
                {
                    currentMenu.Update( gameTime.ElapsedGameTime.TotalMilliseconds );
                }

                MenuInput( keybaordState );
            }
            else
            {
            }

            _LastKeybState = keybaordState;
        }

        protected override void HandleChange( MenuWindow newActive )
        {
            if ( newActive != _ActiveMenu )
            {
                _MenuActive = false;
            }

            if ( newActive == _BackToMainMenu )
            {
                SkyView.Instance.CurrentMenueSystem.SetActiveMenu( Menues.MAIN );
                SkyView.Instance.CurrentAudioManager.StopRepeatingCues();
                newActive = _Window;

                newActive.WakeUp();
            }
            else if ( newActive == _RepeatStage )
            {
                SkyView.Instance.ReLoadLevel();
                SkyView.Instance.CurrentGameState = GameStates.GAMEPLAY;
                newActive = _Window;
                newActive.WakeUp();
                SkyView.Instance.CurrentAudioManager.StopRepeatingCues();
            }

            else if ( newActive == null )
            {
                SkyView.Instance.ExitGame();
            }
            else if ( newActive != _ActiveMenu )
            {
                newActive.WakeUp();
            }

            _ActiveMenu = newActive;
        }

        public override void Draw( GameTime gameTime )
        {
            if ( MenuRunning )
            {
                _SpriteBatch.Begin( SpriteSortMode.BackToFront, BlendState.AlphaBlend );

                _ActiveMenu.Draw( _SpriteBatch );

                _SpriteBatch.End();
            }
        }
    }
}
