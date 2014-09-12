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

namespace SkyView.Classes.Objects.GameObjects
{
    public class GameObject
    {            
        protected GraphicsDevice _GraphicsDevice = null;
        protected ContentManager _ContentManager = null;
        protected BasicEffect _BasicEffect = null;        

        protected Model _Model = null;
        
        protected Vector3 _v3Position = Vector3.Zero;
        protected Vector3 _v3Rotation = Vector3.Zero;
        protected Vector3 _v3Velocity = Vector3.Zero;
        
        protected float _fScale = 1.0f;
        
        protected bool _bAlive = true;

        public float ForwardVelocity { get; protected set; }
        public float UpwaredVelocity { get; protected set; }
        public float UpwardsAcceleration { get; protected set; }
        public float LeftRightVelocity { get; protected set; }
        public float LeftRightRotationVelocity { get; protected set; }
        public float LeftRightRotationAcceleration { get; protected set; }
        protected float _fAcceleration = 1.0f;

        public BoundingSphere _ObjectBoundingSphere = new BoundingSphere();
        public int ObjectType { get; protected set; }

        public bool FlyMode { get; set; }

        public string ModelFilePath { get; protected set; }

        public GameObject()
        {            
            RotationM = Matrix.Identity;
            ForwardVelocity = 0.0f;
            UpwaredVelocity = 0.0f;
            UpwardsAcceleration = 1.0f;
            LeftRightVelocity = 0.0f;
            LeftRightRotationVelocity = 0.0f;
            LeftRightRotationAcceleration = 0.01f;

            FlyMode = false;

            _v3Rotation.Y = 0.01f;
            RecalculateRotationMatrix();
        }

        public virtual void LoadContent( GraphicsDevice device, ContentManager content )
        {
            _GraphicsDevice = device;
            _ContentManager = content;            

            _BasicEffect = new BasicEffect( _GraphicsDevice );             
        }

        public void RecalculateBoundingSphere()
        {
            if ( _Model != null )
            {
                _ObjectBoundingSphere = new BoundingSphere();

                foreach ( ModelMesh mesh in _Model.Meshes )
                {
                    if ( _ObjectBoundingSphere.Radius == 0 )
                    {
                        _ObjectBoundingSphere = mesh.BoundingSphere;
                    }
                    else
                    {
                        _ObjectBoundingSphere = BoundingSphere.CreateMerged( ObjectBoundingSphere, mesh.BoundingSphere );
                    }
                }

                _ObjectBoundingSphere.Center = _v3Position;

                _ObjectBoundingSphere.Center = Position;
                _ObjectBoundingSphere.Radius *= Scale;
            }
        }

        public BoundingSphere ObjectBoundingSphere
        {
            get { return _ObjectBoundingSphere;  }
        }

        public Model Model
        {
            get
            {
                return _Model;
            }
            set
            {
                _Model = value;
            }
        }

        public Vector3 Position
        {
            get
            {
                return _v3Position;
            }
            set
            {
                _v3Position = value;
            }
        }

        public Vector3 Rotation
        {
            get
            {
                return _v3Rotation;
            }
            set
            {
                _v3Rotation = value;
                RecalculateRotationMatrix();
            }
        }

        public float Scale
        {
            get
            {
                return _fScale;
            }
            set
            {
                _fScale = value;
            }
        }

        public bool Alive
        {
            get
            {
                return _bAlive;
            }
            set
            {
                _bAlive = value;
            }
        }

        public Matrix RotationM
        {
            get; private set;            
        }

        protected void RecalculateRotationMatrix()
        {
            RotationM = Matrix.CreateFromYawPitchRoll( Rotation.Y, Rotation.X, Rotation.Z );
        }

        public virtual void Update(GameTime gameTime)
        {            
            UpdateMovement( gameTime );
        }

        protected virtual void UpdateMovement( GameTime gameTime )
        {
            //rotation
            _v3Rotation.Y += LeftRightRotationVelocity;
            RecalculateRotationMatrix();

            //rotation
            _v3Rotation.Y += LeftRightRotationVelocity;
            RecalculateRotationMatrix();


            //movement forward
            Vector3 v3Movement = RotationM.Forward;

            v3Movement.X = ( v3Movement.X == 0 ) ? 1 : v3Movement.X;
            // v3Movement.Y = ( v3Movement.Y == 0 ) ? 1 : v3Movement.Y;
            v3Movement.Z = ( v3Movement.Z == 0 ) ? 1 : v3Movement.Z;

            v3Movement.X *= ForwardVelocity;
            //v3Movement.Y *= ForwardVelocity;
            v3Movement.Z *= ForwardVelocity;

            _v3Position += v3Movement;

            v3Movement = RotationM.Right;

            v3Movement.X = ( v3Movement.X == 0 ) ? 1 : v3Movement.X;
            //v3Movement.Y = ( v3Movement.Y == 0 ) ? 1 : v3Movement.Y;
            v3Movement.Z = ( v3Movement.Z == 0 ) ? 1 : v3Movement.Z;

            v3Movement.X *= LeftRightVelocity;
            v3Movement.Z *= LeftRightVelocity;

            _v3Position += v3Movement;

            v3Movement = RotationM.Up;
            v3Movement.Y = ( v3Movement.Y == 0 ) ? 1 : v3Movement.Y;

            v3Movement.Y *= UpwaredVelocity;

            _v3Position += v3Movement;
        }

        public virtual void MoveForward( bool toMove )
        {
            ForwardVelocity += ( toMove ) ? _fAcceleration: -_fAcceleration;
        }

        public virtual void MoveBackwards( bool toMove )
        {
            ForwardVelocity += ( toMove ) ? -_fAcceleration : _fAcceleration;
        }

        public virtual void StrafeRight( bool toMove )
        {
            LeftRightVelocity += ( toMove ) ? _fAcceleration : -_fAcceleration;
        }

        public virtual void StrafeLeft( bool toMove )
        {
            LeftRightVelocity += ( toMove ) ? -_fAcceleration : _fAcceleration;
        }

        public virtual void RotateRight( bool toMove )
        {
            LeftRightRotationVelocity += ( toMove ) ? -LeftRightRotationAcceleration : LeftRightRotationAcceleration;
        }

        public virtual void RotateLeft( bool toMove )
        {
            LeftRightRotationVelocity += ( toMove ) ? LeftRightRotationAcceleration : -LeftRightRotationAcceleration;
        }

        public virtual void Jump( bool toMove )
        {
            UpwaredVelocity += ( toMove ) ? UpwardsAcceleration : -UpwardsAcceleration;
            FlyMode = true;
        }

        public virtual void PullDown( bool toMove )
        {
            UpwaredVelocity += ( toMove ) ? -UpwardsAcceleration : +UpwardsAcceleration;
        }

        public virtual void Draw( Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix )  
        {            
            if ( _Model != null )
            {
                foreach ( ModelMesh mesh in _Model.Meshes )
                {
                    foreach ( BasicEffect effect in mesh.Effects )
                    {                        
                        effect.EnableDefaultLighting();
                        effect.PreferPerPixelLighting = true;

                        effect.World = RotationM *
                           Matrix.CreateScale(Scale) *
                           Matrix.CreateTranslation(Position);

                        effect.Projection = projectionMatrix;
                        effect.View = viewMatrix;
                    }

                    mesh.Draw();
                }
            }
        }

        public virtual void HandleColision( int GameObjectType )
        {

        }
    }
}
