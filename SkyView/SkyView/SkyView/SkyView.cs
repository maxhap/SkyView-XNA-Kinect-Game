using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using SkyView.Classes;
using SkyView.Classes.Logic;
using SkyView.Classes.Objects;
using SkyView.Classes.Objects.KinectQuads;
using KinectLibrary;
using SkinnedModel;
using SkyView.Classes.Logic.Levels;
using SkyView.Classes.Objects.GameObjects;
using SkyView.Classes.Objects.GameObjects.Animated;
using SkyView.Classes.Objects.GameObjects.NoneAnimated;
using SkyView.Classes.Menues;
using SkyView.Classes.Menues.Menues;
using SkyView.Classes.Logic.Audio;
using SkyView.Classes.Kinect;

namespace SkyView
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SkyView : Microsoft.Xna.Framework.Game
    {
        public static bool KINEKTENABLED = true;
        public static bool SHOWDEBUG = false;
        public bool KinectOperational { get; set; }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;    

        private BasicEffect effect;

        private AudioManager _AudioManager = new AudioManager();
                    
        private Level _CurrentLevel = null;
        private Terrain _Terrain = null;
        private SkyDome _SkyDome = new SkyDome();
        private Player _Player = null;
        private Camera _Camera = null;
        private List<GameObject> _CurrentGameObjects = new List<GameObject>();        

        public int Score { get; set; }
        public int ScoreMultiplier { get; set; }

        private SpriteFont spriteFont;

        private int _CurrentGameState = GameStates.MAINMENU;
        private int frameRate = 0;
        private int frameCounter = 0;
        private TimeSpan elapsedTime = TimeSpan.Zero;

        public static SkyView _Instance;

        private MenuSystem _MenuSystem = new MenuSystem();

        private int _LevelState = 0;
        private float _LevelTimer;

        private Texture2D _StarTexture;
        private Texture2D _ClockTexture;
        private Texture2D _CrossTexture;
        private Texture2D _ThreeTexture;
        private Texture2D _TwoTexture;
        private Texture2D _OneTexture;
        private Texture2D _GoTexture;
        private Texture2D _ButterflyTexture;
        private Texture2D _LevelMapTexture;
        private Texture2D _LevelMapPinTexture;
        private Texture2D _BodyPointTexture;

        private static int _ScreenWidth = 1920;
        private static int _ScreenHeight = 1080;

        private int _Lives = 0;

        #region DrawKinektScelitonVars
        int SkScale = 5;
        Rectangle bounderies = new Rectangle( _ScreenWidth - 640 / 5 - 30, _ScreenHeight / 2 - 480 / 5 / 2, 640 / 5, 480 / 5 );

        Texture2D _KinectTexture = null;
        #endregion

        private SkyView()
        {
            graphics = new GraphicsDeviceManager(this);


            graphics.PreferredBackBufferHeight = _ScreenHeight;
            graphics.PreferredBackBufferWidth = _ScreenWidth;            

            //graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.IsFullScreen = false;

            Content.RootDirectory = "Content";
                      
            _Player = new Player();

            _Camera = new Camera( graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight );

            if ( KINEKTENABLED )
            {
                KinectOperational = KinectManager.Instance.StartServer();
                KinectManager.Instance.CalculateSkelitonToCamoraPossitions = true;
            }
            else
            {
                KinectOperational = false;
            }
            
        }

        public static SkyView Instance
        {
            get
            {
                if ( _Instance == null )
                {
                    _Instance = new SkyView();
                }

                return _Instance;
            }
        }

        public void FullScreenMode()
        {            
            graphics.ToggleFullScreen();

           _Camera.UpdateProjection( graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight );

           GraphicsDevice.Reset();
        }

        public int CurrentLives
        {
            get { return _Lives; }
            set { _Lives = value; }
        }

        public MenuSystem CurrentMenueSystem
        {
            get { return _MenuSystem; }
        }
        public Terrain CurrentTerrain
        {
            get { return _Terrain; }
        }

        public Player CurrentPlayer
        {
            get { return _Player; }
        }

        public Camera CurrentCamera
        {
            get { return _Camera; }
        }

        public Level CurrentLevel
        {
            get { return _CurrentLevel; }
        }

        public AudioManager CurrentAudioManager
        {
            get { return _AudioManager; }
        }

        public int CurrentGameState
        {
            get { return _CurrentGameState; }
            set { _CurrentGameState = value; }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {           

            // TODO: Add your initialization logic here
            effect = new BasicEffect(graphics.GraphicsDevice);
           // effect.AmbientLightColor = Vector3.One;
            effect.AmbientLightColor = new Vector3(0.0f, 1.0f, 0.0f);


            effect.DirectionalLight0.Enabled = true;
            effect.DirectionalLight0.DiffuseColor = Vector3.One;
            effect.DirectionalLight0.Direction = Vector3.Normalize(Vector3.One);
            effect.LightingEnabled = true;

            base.Initialize();  
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            spriteFont = Content.Load<SpriteFont>( "Fonts\\font" );

            _StarTexture = Content.Load<Texture2D>( "Textures\\star" );
            _ClockTexture = Content.Load<Texture2D>( "Textures\\Clock" );
            _CrossTexture = Content.Load<Texture2D>( "Textures\\cross" );

            _ThreeTexture = Content.Load<Texture2D>( "Textures\\three" );
            _TwoTexture = Content.Load<Texture2D>( "Textures\\two" );
            _OneTexture = Content.Load<Texture2D>( "Textures\\one" );
            _GoTexture = Content.Load<Texture2D>( "Textures\\go" );
            _ButterflyTexture = Content.Load<Texture2D>( "Textures\\butterfly" );
            _LevelMapPinTexture = Content.Load<Texture2D>( "LevelMiniMaps\\pin" );
            _BodyPointTexture = Content.Load<Texture2D>( "Textures\\bodypoint" );        

            _AudioManager.LoadContent( Content );
            _MenuSystem.LoadConetent( GraphicsDevice, Content );

            if ( KinectOperational )
            {
                _KinectTexture = new Texture2D( GraphicsDevice, KinectInfo.PIXELWIDTH, KinectInfo.PIXELHEIGHT );
            }
        }

        private int _CurrentLevelNumber;
        private int _CurrentLevelDifficulty;

        public void LoadLevel( int levelNumber, int levelDifficulty )
        {
            _CurrentLevelNumber = levelNumber;
            _CurrentLevelDifficulty = levelDifficulty;

            if ( levelNumber == 1 )
            {
                _CurrentLevel = new Level1( levelDifficulty );
            }
            else if ( levelNumber == 2 )
            {
                _CurrentLevel = new Level2( levelDifficulty );
            }

            _LevelMapTexture = Content.Load<Texture2D>( CurrentLevel.LevelMap );
            Score = 0;
            ScoreMultiplier = 1;
            
            _Player = new Player();
            _Player.LoadContent( GraphicsDevice, Content );
            _Player.Position = _CurrentLevel.StartingPossition;
            _Player.SetLevelForwardMovement( 0.0f );            

            _CurrentLevel.LoadContent( GraphicsDevice, Content );
            _Lives = _CurrentLevel.LevelLives;

            _CurrentGameObjects = _CurrentLevel.LevelObjects;

            _SkyDome = _CurrentLevel.LevelSkydome;

            _Terrain = new Terrain( GraphicsDevice, Content, _CurrentLevel.HeightMap );

            _AudioManager.SetRepeatingCue( _CurrentLevel.LevelMusic );

            _LevelState = 0;
            _LevelTimer = 0.0f;

            beep1 = false;
            beep2 = false;
            beep3 = false;
            beep4 = false;

            playing = false;

            ResetKeyStates();
        }

        public void ReLoadLevel()
        {
            LoadLevel( _CurrentLevelNumber, _CurrentLevelDifficulty );
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        protected override void Update( GameTime gameTime )
        {
            // Allows the game to exit            
            if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed )
                this.Exit();
            
            // TODO: Add your update logic here

            if ( KinectOperational )
            {
                KinectGestures.Instance.UpdateGestuers();
            }
            if ( _CurrentGameState == GameStates.GAMEPLAY )
            {
                UpdateGamePlay( gameTime );
                PlayRandomWindSound();

            }
            else if ( _CurrentGameState == GameStates.MAINMENU || _CurrentGameState == GameStates.GAMEOVERWIN || _CurrentGameState == GameStates.GAMEOVERFAIL )
            {
                _MenuSystem.Update( gameTime, Keyboard.GetState() );
            }

            _AudioManager.Update( gameTime );

            base.Update( gameTime );
        }

        Random _Random = new Random();
        bool playing = false;

        private void PlayRandomWindSound()
        {
            if ( !playing )
            {
                playing = true;
                _AudioManager.SetRepeatingCue2( "wind2" );            
            }           
        }

        private void UpdateGamePlay( GameTime gameTime )
        {
           // but1.Update( gameTime);
            FPSCount( gameTime );

            if ( _LevelState == 0 )
            {
                //_Player.SetLevelForwardMovement( 0.0f ); 
                if ( _LevelTimer <= 1000 )
                {
                    //draw 1
                }
                else if ( _LevelTimer <= 2000 )
                {
                    //draw 2
                }
                else if ( _LevelTimer <= 4000 )
                {
                    //draw 3
                }
                else
                {
                    //start level
                    _LevelState = 1;
                    _LevelTimer = 0.0f;
                }
            }
            else if ( _LevelState == 1 )
            {
                _LevelState = 2;
                _Player.SetLevelForwardMovement( _CurrentLevel.CharacterSpeed );
            }
            else
            {
                HandleKeyPress();

                if ( KINEKTENABLED )
                {
                    HandleGestures();
                }
            }

            _Player.Update( gameTime );

            _Camera.Update();

            _SkyDome.Update( gameTime );

            foreach ( GameObject gameObject in _CurrentGameObjects )
            {
                if ( gameObject.Alive )
                {
                    gameObject.Update( gameTime );  
                }
            }

            CheckForCollisions();

            _LevelTimer += gameTime.ElapsedGameTime.Milliseconds;

            CheckPlayerAtEndOfLevel();
        }

        private void FPSCount( GameTime gameTime )
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if ( elapsedTime > TimeSpan.FromSeconds( 1 ) )
            {
                elapsedTime -= TimeSpan.FromSeconds( 1 );
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }

        public void ExitGame()
        {
            Exit();
        }

        #region KeyStates
        
        bool WKeyDown = false;
        bool SKeyDown = false;
        bool AKeyDown = false;
        bool DKeyDown = false;
        bool LeftKeyDown = false;
        bool RightKeyDown = false;
        bool SpaceKeyDown = false;
        #endregion

        #region KinectGestureStates
        public bool RotateRight = false;
        private bool RotateRightActive = false;

        public bool RotateLeft = false;
        private bool RotateLeftActive = false;

        public bool StrafeRight = false;
        private bool StrafeRightActive = false;

        public bool StrafeLeft = false;
        private bool StrafeLeftActive = false;

        public bool Fly = false;
        private bool FlyActive = false;

        #endregion

        private void ResetKeyStates()
        {
            WKeyDown = false;
            SKeyDown = false;
            AKeyDown = false;
            DKeyDown = false;
            LeftKeyDown = false;
            RightKeyDown = false;
            SpaceKeyDown = false;

            RotateRight = false;
            RotateRightActive = false;

            RotateLeft = false;
            RotateLeftActive = false;

            StrafeRight = false;
            StrafeRightActive = false;

            StrafeLeft = false;
            StrafeLeftActive = false;

            FlyActive = false;
        }

        private void HandleGestures()
        {
            if ( !RotateRightActive && RotateRight )
            {
                RotateRightActive = true;
                _Player.RotateRight( true );
            }

            if ( RotateRightActive && !RotateRight )
            {
                RotateRightActive = false;
                _Player.RotateRight( false );
            }

            if ( !RotateLeftActive && RotateLeft )
            {
                RotateLeftActive = true;
                _Player.RotateLeft( true );
            }

            if ( RotateLeftActive && !RotateLeft )
            {
                RotateLeftActive = false;
                _Player.RotateLeft( false );
            }

            if ( !StrafeRightActive && StrafeRight )
            {
                StrafeRightActive = true;
                _Player.StrafeRight( true );
            }

            if ( StrafeRightActive && !StrafeRight )
            {
                StrafeRightActive = false;
                _Player.StrafeRight( false );
            }

            if ( !StrafeLeftActive && StrafeLeft )
            {
                StrafeLeftActive = true;
                _Player.StrafeLeft( true );
            }

            if ( StrafeLeftActive && !StrafeLeft )
            {
                StrafeLeftActive = false;
                _Player.StrafeLeft( false );
            }

            if ( !FlyActive && Fly)
            {
                FlyActive = true;
                _Player.Jump( true );
            }

            if ( FlyActive && !Fly)
            {
                FlyActive = false;
                _Player.Jump( false );
            }
        }

        private void HandleKeyPress( )
        {
            KeyboardState keyState = Keyboard.GetState();

            if( !WKeyDown && keyState.IsKeyDown( Keys.W ) )
            {
                WKeyDown = true;
               // _Player.MoveForward( true );
            }
            if( WKeyDown && keyState.IsKeyUp( Keys.W ) )
            {
                WKeyDown = false;
                //_Player.MoveForward( false );
            }
            
            if ( !SKeyDown && keyState.IsKeyDown ( Keys.S ) )
            {
                SKeyDown = true;
                _Player.MoveBackwards( true );
            }
            if ( SKeyDown&& keyState.IsKeyUp( Keys.S ) )
            {
                SKeyDown = false;
                _Player.MoveBackwards( false );
            }

            if ( !AKeyDown && keyState.IsKeyDown( Keys.A ) )
            {
                AKeyDown = true;
                _Player.StrafeLeft( true );
            }
            if ( AKeyDown && keyState.IsKeyUp( Keys.A ) )
            {
                AKeyDown = false;
                _Player.StrafeLeft( false );
            }

            if ( !DKeyDown && keyState.IsKeyDown( Keys.D ) )
            {
                DKeyDown = true;
                _Player.StrafeRight( true );
            }
            if ( DKeyDown && keyState.IsKeyUp( Keys.D ) )
            {
                DKeyDown = false;
                _Player.StrafeRight( false );
            }

            if ( !LeftKeyDown && keyState.IsKeyDown( Keys.Left ) )
            {
                LeftKeyDown = true;
                _Player.RotateLeft( true );
            }
            if ( LeftKeyDown && keyState.IsKeyUp( Keys.Left ) )
            {
                LeftKeyDown = false;
                _Player.RotateLeft( false );
            }

            if ( !RightKeyDown && keyState.IsKeyDown( Keys.Right) )
            {
                RightKeyDown= true;
                _Player.RotateRight( true );
            }
            if ( RightKeyDown && keyState.IsKeyUp( Keys.Right) )
            {
                RightKeyDown = false;
                _Player.RotateRight( false );
            }

            if ( !SpaceKeyDown && keyState.IsKeyDown( Keys.Space ) )
            {
                SpaceKeyDown = true;
                _Player.Jump( true );
            }
            if ( SpaceKeyDown && keyState.IsKeyUp( Keys.Space ) )
            {
                SpaceKeyDown = false;
                _Player.Jump( false );
            }            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {            
            graphics.GraphicsDevice.Clear( ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1, 0 );

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;            

            if ( _CurrentGameState == GameStates.MAINMENU || _CurrentGameState == GameStates.GAMEOVERWIN || _CurrentGameState == GameStates.GAMEOVERFAIL )
            {
                _MenuSystem.Draw( gameTime );
            }
            else if ( _CurrentGameState == GameStates.GAMEPLAY )
            {
                DrawGamePlay( gameTime );
            }

            DrawKinektSceliton();

            base.Draw(gameTime);        
        }

        private void DrawGamePlay( GameTime gameTime )
        {
            _SkyDome.Draw( Matrix.Identity, _Camera.View, _Camera.Projection );
            _Player.Draw( Matrix.Identity, _Camera.View, _Camera.Projection );            
            _Terrain.Draw( Matrix.Identity, _Camera.View, _Camera.Projection );

            DrawLevelObejcts( gameTime );

            DrawGamplay2D();
        }

        private void DrawLevelObejcts( GameTime gameTime )
        {
            foreach( GameObject gameObject in _CurrentGameObjects )
            {
                if ( gameObject.Alive )
                {
                    gameObject.Draw( Matrix.Identity, _Camera.View, _Camera.Projection );
                }                
            }
        }


        #region beeps
        bool beep1 = false;
        bool beep2 = false;
        bool beep3 = false;
        bool beep4 = false;

        #endregion
        private void DrawGamplay2D()
        {
            frameCounter++;   

            spriteBatch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend );

            if ( SHOWDEBUG )
            {
                string fps = string.Format( "fps: {0}", frameRate );

                spriteBatch.DrawString( spriteFont, fps, new Vector2( 33, 150 ), Color.Black );
                spriteBatch.DrawString( spriteFont, fps, new Vector2( 32, 149 ), Color.White );

                string ph = string.Format( "Player Position X: {0} Y:{1} Z: {2}", _Player.Position.X, _Player.Position.Y, _Player.Position.Z );
                string rotation = string.Format( "Player Rotation X: {0} Y:{1} Z: {2}", _Player.Rotation.X, _Player.Rotation.Y, _Player.Rotation.Z );

                spriteBatch.DrawString( spriteFont, ph, new Vector2( 32, 180 ), Color.Black );
                spriteBatch.DrawString( spriteFont, ph, new Vector2( 33, 179 ), Color.White );

                spriteBatch.DrawString( spriteFont, rotation, new Vector2( 32, 220 ), Color.Black );
                spriteBatch.DrawString( spriteFont, rotation, new Vector2( 33, 219 ), Color.White );
            }            

            string scoreMulti = string.Format( "{0}", ScoreMultiplier );
            spriteBatch.DrawString( spriteFont, scoreMulti, new Vector2( 85, 21 ), Color.Black );
            spriteBatch.DrawString( spriteFont, scoreMulti, new Vector2( 84, 20 ), Color.White );
            spriteBatch.Draw( _CrossTexture, new Rectangle( 32, 10, 50, 50 ), Color.White );

            string score = string.Format( "{0}", Score );
            spriteBatch.DrawString( spriteFont, score, new Vector2( _ScreenWidth - 40, 21 ), Color.Black );
            spriteBatch.DrawString( spriteFont, score, new Vector2( _ScreenWidth - 41, 20 ), Color.White );
            spriteBatch.Draw( _StarTexture, new Rectangle( _ScreenWidth - 41 - 50, 10, 50, 50 ), Color.White );

            string time = string.Format( "{0}", ( _LevelTimer / 1000 ).ToString( "0.00" ) );
            spriteBatch.DrawString( spriteFont, time, new Vector2( _ScreenWidth / 2 + 5, 21 ), Color.Black );
            spriteBatch.DrawString( spriteFont, time, new Vector2( _ScreenWidth / 2 + 5, 20 ), Color.White );
            spriteBatch.Draw( _ClockTexture, new Rectangle( _ScreenWidth / 2 - 50, 10, 50, 50 ), Color.White );

            string lives = string.Format( "{0}", ( _Lives ) );
            spriteBatch.DrawString( spriteFont, lives, new Vector2( _ScreenWidth - 40, _ScreenHeight - 50 ), Color.Black );
            spriteBatch.DrawString( spriteFont, lives, new Vector2( _ScreenWidth - 41, _ScreenHeight - 51 ), Color.White );
            spriteBatch.Draw( _ButterflyTexture, new Rectangle( _ScreenWidth - 41 - 50, _ScreenHeight - 31 - 35, 50, 50 ), Color.White );

            spriteBatch.Draw( _LevelMapTexture, new Rectangle( 30, _ScreenHeight - 30 - 150, 150, 150 ), Color.White );

            float scale = 150.0f / CurrentLevel.HeightmapSize;
            float posX = ( _Player.Position.X - CurrentLevel.StartingPossition.X ) * scale;
            posX += 35;

            float posY = ( _Player.Position.Z - CurrentLevel.StartingPossition.Z ) * scale;
            float yPoint = _ScreenHeight - 30 - 28;
            posY = yPoint + posY;

            spriteBatch.Draw( _LevelMapPinTexture, new Rectangle( ( int ) posX, ( int )posY, 25, 25 ), Color.White );

            if ( _LevelState == 0 )
            {                
                if ( _LevelTimer <= 1000 )
                {
                    spriteBatch.Draw( _ThreeTexture, new Rectangle( _ScreenWidth / 2 - 126 / 2, _ScreenHeight/ 2 - 126 / 2, 128, 128 ), Color.White );
                    
                    if ( !beep1 )
                    {
                        SkyView.Instance.CurrentAudioManager.SetSingleCue( "beep" );
                        beep1 = true;
                    }
                    
                }
                else if ( _LevelTimer <= 2000 )
                {
                    spriteBatch.Draw( _TwoTexture, new Rectangle( _ScreenWidth / 2 - 126 / 2, _ScreenHeight / 2 - 126 / 2, 128, 128 ), Color.White );                    

                    if ( !beep2 )
                    {
                        SkyView.Instance.CurrentAudioManager.SetSingleCue( "beep" );
                        beep2 = true;
                    }
                }
                else if ( _LevelTimer <= 3000 )
                {
                    spriteBatch.Draw( _OneTexture, new Rectangle( _ScreenWidth / 2 - 126 / 2, _ScreenHeight / 2 - 126 / 2, 128, 128 ), Color.White );                    

                    if ( !beep3 )
                    {
                        SkyView.Instance.CurrentAudioManager.SetSingleCue( "beep" );
                        beep3 = true;
                    }
                }
                else
                {
                    spriteBatch.Draw( _GoTexture, new Rectangle( _ScreenWidth / 2 - 126 / 2, _ScreenHeight / 2 - 126 / 2, 128, 128 ), Color.White );
                    
                    if ( !beep4 )
                    {
                        SkyView.Instance.CurrentAudioManager.SetSingleCue( "beep" );
                        beep4 = true;
                    }

                }
            }

            spriteBatch.End();
        }



        public void DrawKinektSceliton()
        {
            if ( !KinectOperational )
            {
                return;
            }

            byte[] data = KinectManager.Instance.GetCamoraData();

            if ( data != null )
            {
                _KinectTexture.SetData<byte>( data );
            }
            
            spriteBatch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend );

            spriteBatch.Draw( _KinectTexture, bounderies, Color.White );

            MASkeliton sCamSkel = KinectManager.Instance.GetPlayer1CamRelativeSkeleton();
            spriteBatch.Draw( _BodyPointTexture, new Rectangle( bounderies.X + ( int ) ( sCamSkel.Head.X / SkScale ), bounderies.Y + ( int ) ( sCamSkel.Head.Y / SkScale ), 25, 25 ), Color.White );
            //spriteBatch.Draw( _BodyPointTexture, new Rectangle( bounderies.X + ( int ) ( sCamSkel.CenterShoulder.X / SkScale ), bounderies.Y + ( int ) ( sCamSkel.CenterShoulder.Y / SkScale ), 25, 25 ), Color.White );
            spriteBatch.Draw( _BodyPointTexture, new Rectangle( bounderies.X + ( int ) ( sCamSkel.LeftHand.X / SkScale ), bounderies.Y + ( int ) ( sCamSkel.LeftHand.Y / SkScale ), 25, 25 ), Color.White );
            spriteBatch.Draw( _BodyPointTexture, new Rectangle( bounderies.X + ( int ) ( sCamSkel.RightHand.X / SkScale ), bounderies.Y + ( int ) ( sCamSkel.RightHand.Y / SkScale ), 25, 25 ), Color.White );
            //spriteBatch.Draw( _BodyPointTexture, new Rectangle( bounderies.X + ( int ) ( sCamSkel.CenterHip.X / SkScale ), bounderies.Y + ( int ) ( sCamSkel.CenterHip.Y / SkScale ), 25, 25 ), Color.White );
            //spriteBatch.Draw( _BodyPointTexture, new Rectangle( bounderies.X + ( int ) ( sCamSkel.RightKnee.X / SkScale ), bounderies.Y + ( int ) ( sCamSkel.RightKnee.Y / SkScale ), 25, 25 ), Color.White );
            //spriteBatch.Draw( _BodyPointTexture, new Rectangle( bounderies.X + ( int ) ( sCamSkel.LeftKnee.X / SkScale ), bounderies.Y + ( int ) ( sCamSkel.LeftKnee.Y / SkScale ), 25, 25 ), Color.White );
            //spriteBatch.Draw( _BodyPointTexture, new Rectangle( bounderies.X + ( int ) ( sCamSkel.LeftFoot.X / SkScale ), bounderies.Y + ( int ) ( sCamSkel.LeftFoot.Y / SkScale ), 25, 25 ), Color.White );
            //spriteBatch.Draw( _BodyPointTexture, new Rectangle( bounderies.X + ( int ) ( sCamSkel.RightFoot.X / SkScale ), bounderies.Y + ( int ) ( sCamSkel.RightFoot.Y / SkScale ), 25, 25 ), Color.White );

            spriteBatch.End();
        }


        protected override void OnExiting( Object sender, EventArgs args )
        {
            base.OnExiting( sender, args );

            // Stop the threads
            if( KINEKTENABLED )
            {
                KinectManager.Instance.StopServer();
            }            
        }

        private void CheckForCollisions()
        {
            //player collision
            _Player.RecalculateBoundingSphere();
            
            foreach ( GameObject gameObject in _CurrentGameObjects )
            {
                if ( gameObject.Alive )
                {                
                    gameObject.RecalculateBoundingSphere();

                    if ( gameObject.ObjectBoundingSphere!= null )
                    {
                        if ( gameObject.ObjectBoundingSphere.Intersects( _Player.ObjectBoundingSphere ) )
                        {
                            gameObject.HandleColision( GameObjects.PLAYER );
                        }
                    }
                }
            }
        }

        public void IncreaseScore( int points )
        {
            Score += ( points * ScoreMultiplier );
        }

        public void CheckPlayerAtEndOfLevel()
        {
            Vector3 distanceVector = CurrentLevel.EndingPoint - _Player.Position;
            float magnitude = ( float ) Math.Sqrt( Math.Pow( distanceVector.X, 2.0 ) + Math.Pow( distanceVector.Y, 2) + Math.Pow( distanceVector.Z, 2)  );

            if ( magnitude < 25 )
            {
                _CurrentGameState = GameStates.GAMEOVERWIN;
                
                if ( _CurrentLevel.PrecessHiScore( Score ) )
                {
                    _MenuSystem.ReloadMainMenu();
                }

                _MenuSystem.SetActiveMenu( Menues.ENDGAMEWIN );
                _AudioManager.StopRepeatingCues();
            }
        }

        public bool InRangeOfPlayer( Vector3 position, float distance )
        {
            Vector3 distanceVector = position - _Player.Position;
            float magnitude = ( float ) Math.Sqrt( Math.Pow( distanceVector.X, 2.0 ) + Math.Pow( distanceVector.Y, 2 ) + Math.Pow( distanceVector.Z, 2 ) );

            if ( magnitude < distance )
            {
                return true;
            }

            return false;
        }

        public bool PlayerInFieldOfView( Vector3 position, Vector3 direction, float fieldOfView )
        {
            Vector3 distanceVector = position - _Player.Position;
            float hypotonuse = ( float )Math.Sqrt( Math.Pow( distanceVector.X, 2 ) *  Math.Pow( distanceVector.Z, 2 ) );
            float angle = distanceVector.Z / hypotonuse;

            if ( fieldOfView >= angle )
            {
                return true;
            }

            return false;
        }

        public void ReduceCurrentLives()
        {
            _Lives--;

            if ( _Lives == 0 )
            {
                _AudioManager.StopRepeatingCues();
                _CurrentGameState = GameStates.GAMEOVERFAIL;
                _MenuSystem.SetActiveMenu( Menues.ENDGAMEFAIL );                
            }

        }
    }
}
