using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using SkyView.Classes.Logic;

namespace SkyView.Classes.Objects.GameObjects.Animated
{
    public class Butterfly : AnimatedObject
    {
        float _DistanceTraveled = 0.0f;

        public Butterfly()
        {
            ObjectType = GameObjects.BUTTERFLY;
        }

        public override void LoadContent( GraphicsDevice device, ContentManager content )
        {
            ModelFilePath = "Models\\butterfly\\butterfly";

            _fScale = 5.0f;
            this.ForwardVelocity = 0.0f;
            _DistanceTraveled = 0.0f;

            base.LoadContent( device, content );
        }

        public override void Update( GameTime gameTime )
        {
            base.Update( gameTime );

            //check if inrange of player
            if ( SkyView.Instance.InRangeOfPlayer( Position + this.RotationM.Forward * 100, 60 ) )
            {
                 this.ForwardVelocity = 1.0f;
            }

            _DistanceTraveled += ForwardVelocity;

            if ( _DistanceTraveled > 400 )
            {
                Alive = false;
            }
        }

        public override void HandleColision( int GameObjectType )
        {
            SkyView.Instance.CurrentAudioManager.SetSingleCue( "ooo" );
            SkyView.Instance.ReduceCurrentLives();
            this.Alive = false;
        }

        protected override void UpdateMovement( GameTime gameTime )
        {
            base.UpdateMovement( gameTime );
        }


        public override void Draw( Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix )
        {
            RasterizerState state = new RasterizerState();
            RasterizerState before = _GraphicsDevice.RasterizerState;

            state.CullMode = CullMode.None;
            _GraphicsDevice.RasterizerState = state;

            base.Draw( worldMatrix, viewMatrix, projectionMatrix );

            _GraphicsDevice.RasterizerState = before;
        }
    }    


}
