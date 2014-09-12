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
    public class Player : AnimatedObject
    {
        private bool _Jumping = false;
        private bool _LiftOffStage = false;
        private float _InitialJumpHeight = 0;

        private Model _IdelModel;
        private Model _RunModel;
        private Model _FlyModel;

        public Player()
        {
            ObjectType = GameObjects.PLAYER;
        }

        public override void LoadContent( GraphicsDevice device, ContentManager content )
        {
            ModelFilePath = "Models\\Character\\Character_Idel";
        
            _fScale = 0.8f;

            _RunModel = content.Load<Model>( "Models\\Character\\Character_Run" );
            _FlyModel = content.Load<Model>( "Models\\Character\\Character_Fly" );
            

            base.LoadContent( device, content );

            _IdelModel = _Model;
        }

        public void SetLevelForwardMovement( float movement )
        {
            _fAcceleration = movement;
            ForwardVelocity = movement;

            if ( movement > 0.0f )
            {
                SetToRun();
            }
            else
            {
                SetToIdel();
            }
        }

        public override void Update( GameTime gameTime )
        {
            LiftOffCheck();

            if ( !_LiftOffStage )
            {
                float terrainHeight = SkyView.Instance.CurrentTerrain.GetExactHeightAt( Position.X, -Position.Z ) + 4;

                if ( terrainHeight > Position.Y )
                {
                    FlyMode = false;
                    SetToRun();
                }
            }

            if( FlyMode && !_Jumping )
            {
                PullDown( true );
            }

            base.Update( gameTime );

            if ( FlyMode && !_Jumping )
            {
                PullDown( false );
            }

        }

        public void LiftOffCheck()
        {
            if ( _InitialJumpHeight + 1 < Position.Y )
            {
                _LiftOffStage = false;
            }
        }

        protected override void UpdateMovement( GameTime gameTime )
        {
            Vector3 v3BeforePossition = Position;

            //rotation
            _v3Rotation.Y += LeftRightRotationVelocity;
            RecalculateRotationMatrix();

            //rotation
            _v3Rotation.Y += LeftRightRotationVelocity;
            RecalculateRotationMatrix();


            //movement forward
            Vector3 v3ForwardMovement = RotationM.Forward;

            v3ForwardMovement.X = ( v3ForwardMovement.X == 0 ) ? 1 : v3ForwardMovement.X;
            // v3Movement.Y = ( v3Movement.Y == 0 ) ? 1 : v3Movement.Y;
            v3ForwardMovement.Z = ( v3ForwardMovement.Z == 0 ) ? 1 : v3ForwardMovement.Z;

            v3ForwardMovement.X *= ForwardVelocity;
            //v3Movement.Y *= ForwardVelocity;
            v3ForwardMovement.Z *= ForwardVelocity;

            _v3Position += v3ForwardMovement;

            Vector3 v3LeftRightMovement = RotationM.Right;

            v3LeftRightMovement.X = ( v3LeftRightMovement.X == 0 ) ? 1 : v3LeftRightMovement.X;
            //v3Movement.Y = ( v3Movement.Y == 0 ) ? 1 : v3Movement.Y;
            v3LeftRightMovement.Z = ( v3LeftRightMovement.Z == 0 ) ? 1 : v3LeftRightMovement.Z;

            v3LeftRightMovement.X *= LeftRightVelocity;
            v3LeftRightMovement.Z *= LeftRightVelocity;

            _v3Position += v3LeftRightMovement;

            Vector3 v3UpDownMovment = RotationM.Up;
            v3UpDownMovment.Y = ( v3UpDownMovment.Y == 0 ) ? 1 : v3UpDownMovment.Y;

            v3UpDownMovment.Y *= UpwaredVelocity;

            _v3Position += v3UpDownMovment;
            
            if ( !FlyMode )
            {
                _v3Position.Y = SkyView.Instance.CurrentTerrain.GetExactHeightAt( Position.X, -Position.Z ) + 4;                                                                                  
            }

            if ( _v3Position.Y >= GlobalValues.MAXFLYHEIGHT )
            {
                Position = v3BeforePossition;
            }
        }

        public override void Jump( bool toMove )
        {
            if ( toMove )
            {
                _InitialJumpHeight = Position.Y;
                _LiftOffStage = true;
                SetToFLy();
            }

            _Jumping = toMove;

            base.Jump( toMove );
        }

        public override void PullDown( bool toMove )
        {
            float fBeforeUpwardAceleration = UpwardsAcceleration;
            UpwardsAcceleration = UpwardsAcceleration / 4;

            base.PullDown( toMove );

            UpwardsAcceleration = fBeforeUpwardAceleration;
        }

        public void SetToIdel()
        {
            _Model = _IdelModel;
            ReloadAnimationClip();
        }

        public void SetToRun()
        {
            _Model = _RunModel;
            ReloadAnimationClip();
        }

        public void SetToFLy()
        {
            _Model = _FlyModel;
            ReloadAnimationClip();
        }
    }
}
