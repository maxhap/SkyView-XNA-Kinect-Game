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
    class MainMenu :Menu
    {
        private MenuWindow _StartGameEasy;
        private MenuWindow _StartGameNormal;
        private MenuWindow _StartGameHard;

        private MenuWindow _HillyCreek;
        private MenuWindow _MistyMoutains;

        private MenuWindow _VolumeMenu;
        private MenuWindow _VolumeHigh;
        private MenuWindow _VolumeMedium;
        private MenuWindow _VolumeLow;

        private MenuWindow _Controls;

        private MenuWindow _FullScreen;

        private int SelectedStage = 0;

        private MenuWindow menuOptions;
        private MenuWindow menuMain;

        public MainMenu() : base()
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
            Texture2D backgroundImage = _ContentManager.Load<Texture2D>( "Backgrounds\\mainMenu" );
            Texture2D optionImage = _ContentManager.Load<Texture2D>( "Backgrounds\\options" );
            Texture2D stageImage = _ContentManager.Load<Texture2D>( "Backgrounds\\stage" );
            Texture2D controlsImage = _ContentManager.Load<Texture2D>( "Backgrounds\\controls" );

            menuMain = new MenuWindow( menuFont, "Main Menu", backgroundImage );
            menuMain.TextPosstionIncrease = new Vector2( 0, -250 );
            MenuWindow menuNewGame = new MenuWindow( menuFont, "Select a Stage", stageImage );
            menuNewGame.TextPosstionIncrease = new Vector2( 0, -180 );

            menuOptions = new MenuWindow( menuFont, "Options Menu", optionImage );
            menuOptions.TextPosstionIncrease = new Vector2( 0, 880 );

            menuMain.AddMenuItem( "New Game", menuNewGame );
            menuMain.AddMenuItem( "Options", menuOptions );
            menuMain.AddMenuItem( "Exit Game", null );

            _HillyCreek = new MenuWindow( menuFont, "Select a Difficulty", stageImage );
            _HillyCreek.TextPosstionIncrease = new Vector2( 0, -150 );

            _MistyMoutains = new MenuWindow( menuFont, "Select a Difficulty", stageImage );
            _MistyMoutains.TextPosstionIncrease = new Vector2( 0, -150 );

            _StartGameEasy = new MenuWindow( menuFont, "Never seen", stageImage );
            _StartGameNormal = new MenuWindow( menuFont, "Never seen", stageImage );
            _StartGameHard = new MenuWindow( menuFont, "Never seen", stageImage );

            _HillyCreek.AddMenuItem( "Easy", _StartGameEasy );
            _HillyCreek.AddMenuItem( "Normal", _StartGameNormal );
            _HillyCreek.AddMenuItem( "Hard", _StartGameHard );
            _HillyCreek.AddMenuItem( "Back to Stages", menuNewGame );

            _MistyMoutains.AddMenuItem( "Easy", _StartGameEasy );
            _MistyMoutains.AddMenuItem( "Normal", _StartGameNormal );
            _MistyMoutains.AddMenuItem( "Hard", _StartGameHard );
            _MistyMoutains.AddMenuItem( "Back to Stages", menuNewGame );

            _VolumeHigh = new MenuWindow( menuFont, "Never seen", stageImage );
            _VolumeMedium = new MenuWindow( menuFont, "Never seen", stageImage );
            _VolumeLow = new MenuWindow( menuFont, "Never seen", stageImage );

            _VolumeMenu = new MenuWindow( menuFont, "Volume Menu", optionImage );
            _VolumeMenu.TextPosstionIncrease = new Vector2( 0, 880 );
            _VolumeMenu.AddMenuItem( "Low", _VolumeLow );
            _VolumeMenu.AddMenuItem( "Medium", _VolumeMedium );
            _VolumeMenu.AddMenuItem( "High", _VolumeHigh );
            _VolumeMenu.AddMenuItem( "Back to Options Menu", menuOptions );

            _MenuList.Add( menuMain );
            _MenuList.Add( _VolumeMenu );
            _MenuList.Add( menuNewGame );
            _MenuList.Add( menuOptions );
            _MenuList.Add( _HillyCreek );
            _MenuList.Add( _MistyMoutains );

            _FullScreen = new MenuWindow( menuFont, "Never seen", stageImage );

            _Controls = new MenuWindow( menuFont, "Controls", controlsImage );
            _Controls.AddMenuItem( "Back to Options", menuOptions );
            _Controls.TextPosstionIncrease = new Vector2( 0, -180 );

            _MenuList.Add( _Controls );            

            menuOptions.AddMenuItem( "View controls", _Controls );
            menuOptions.AddMenuItem( "Change sound setting", _VolumeMenu );
            menuOptions.AddMenuItem( "Toggle Full Screen", _FullScreen );
            menuOptions.AddMenuItem( "Back to Main menu", menuMain );

            string[] score = ScoringSystem.LoadScoars();
        
            menuNewGame.AddMenuItem( "Hilly Creek        Hight Score: " + score[0], _HillyCreek );
            
            if ( int.Parse( score[0] ) > 6 )
            {
                menuNewGame.AddMenuItem( "Misty Mountains    Hight Score: " + score[1], _MistyMoutains );
            }
            
            menuNewGame.AddMenuItem( "Back to Main Menu", menuMain );


            _ActiveMenu = menuMain;
            SkyView.Instance.CurrentAudioManager.StopRepeatingCues();
            SkyView.Instance.CurrentAudioManager.SetRepeatingCue( "Zelda Skyward Sword Music   Song of the Hero" );
            menuMain.WakeUp();
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

            if ( newActive == _StartGameEasy )
            {
                //set level to easy
                SkyView.Instance.LoadLevel( SelectedStage, 1 );
                SkyView.Instance.CurrentGameState = GameStates.GAMEPLAY;
                menuMain.WakeUp();
                newActive = menuMain;

                MenuRunning = false;
            }
            else if ( newActive == _StartGameNormal )
            {
                //set level to easy
                SkyView.Instance.LoadLevel( SelectedStage, 2 );
                SkyView.Instance.CurrentGameState = GameStates.GAMEPLAY;
                menuMain.WakeUp();
                newActive = menuMain;

                MenuRunning = false;
            }
            else if ( newActive == _StartGameHard )
            {
                //set level to easy
                SkyView.Instance.LoadLevel( SelectedStage, 3 );
                SkyView.Instance.CurrentGameState = GameStates.GAMEPLAY;
                menuMain.WakeUp();
                newActive = menuMain;

                MenuRunning = false;
            }
            else if ( newActive == _HillyCreek )
            {
                SelectedStage = 1;

                if ( !_MenuActive )
                {
                    newActive.WakeUp();
                    _MenuActive = true;
                }
            }
            else if ( newActive == _MistyMoutains )
            {
                SelectedStage = 2;
                if ( !_MenuActive )
                {
                    newActive.WakeUp();
                    _MenuActive = true;
                }
            }
            else if ( newActive == _VolumeHigh )
            {
                SkyView.Instance.CurrentAudioManager.SetVolumne( 2.0f );
                newActive = menuOptions;
                newActive.WakeUp();
            }

            else if ( newActive == _VolumeMedium )
            {
                SkyView.Instance.CurrentAudioManager.SetVolumne( 1.0f );
                newActive = menuOptions;
                newActive.WakeUp();
            }

            else if ( newActive == _VolumeLow )
            {
                SkyView.Instance.CurrentAudioManager.SetVolumne( 0.5f );
                newActive = menuOptions;
                newActive.WakeUp();
            }
            else if ( newActive == _FullScreen )
            {
                SkyView.Instance.FullScreenMode();
                newActive = menuOptions;
                newActive.WakeUp();
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
