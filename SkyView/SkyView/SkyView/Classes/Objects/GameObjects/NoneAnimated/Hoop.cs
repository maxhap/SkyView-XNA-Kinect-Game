using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyView.Classes.Objects.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SkyView.Classes.Objects.GameObjects.NoneAnimated
{
    class Hoop: GameObject
    {
        private float _fDiameter = 0.0f;
        private VertexPositionColor[] _vpcVertices = new VertexPositionColor[1440];
        private const int NUM_OF_VERTECIES = 36;
        private const int NUM_TRIANGLES = 12;        

        public Hoop( float diameter )
        {
            ObjectType = GameObjects.HOOP;            

            _fDiameter = diameter; 
            BuildShape();
        }

        private void BuildShape()
        {
            int iArrayPosiotion = 0;
            //front inner ring
            for ( int i = 0; i < 360; i++ )
            {
                double cos = Math.Cos( i * ( float ) Math.PI / 180.0f );
                double sin = Math.Sin( i * ( float ) Math.PI / 180.0f );
                float x = ( float ) cos * _fDiameter;
                float y = ( float ) sin * _fDiameter;
                float z = 0.0f;

                _vpcVertices[iArrayPosiotion] = new VertexPositionColor( new Vector3( x, y, z ), Color.Blue );
                iArrayPosiotion += 2;
            }

            iArrayPosiotion = 1;
            //front outer ring
            for ( int i = 1; i < 360; i++ )
            {
                double cos = Math.Cos( i * ( float ) Math.PI / 180.0f );
                double sin = Math.Sin( i * ( float ) Math.PI / 180.0f );
                float x = ( float ) cos * _fDiameter + 5;
                float y = ( float ) sin * _fDiameter + 5;
                float z = 0.0f;

                _vpcVertices[iArrayPosiotion] = new VertexPositionColor( new Vector3( x, y, z ), Color.Blue );
                iArrayPosiotion += 2;
            }
        }

        public override void LoadContent( GraphicsDevice device, ContentManager content )
        {
            base.LoadContent( device, content );
            _Model = _ContentManager.Load<Model>( "Models\\star" );            

        }

        public override void HandleColision( int GameObjectType )
        {
            SkyView.Instance.IncreaseScore( 1 );
            SkyView.Instance.CurrentAudioManager.SetSingleCue( "Coins" );

            this.Alive = false;
        }

        public override void Update( GameTime gameTime )
        {
            _v3Rotation.Y += ( float ) gameTime.ElapsedGameTime.TotalSeconds * 2.0f;
            RecalculateRotationMatrix();
        }

        public override void Draw (Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {

            base.Draw( worldMatrix, viewMatrix, projectionMatrix );

            RasterizerState beforeState = _GraphicsDevice.RasterizerState;

            // Build the cube, setting up the _vertices array
            if ( _vpcVertices == null )
            {
                BuildShape();
            }

            RasterizerState rs = new RasterizerState();
            //rs.CullMode = CullMode.CullClockwiseFace;
            _BasicEffect.GraphicsDevice.RasterizerState = rs; 
 
            _BasicEffect.View = viewMatrix;
            _BasicEffect.Projection = projectionMatrix;
            _BasicEffect.World = RotationM * Matrix.CreateTranslation( Position ) * Matrix.CreateScale( Scale );

            foreach ( EffectPass pass in _BasicEffect.CurrentTechnique.Passes )
            {
                pass.Apply();
                _GraphicsDevice.DrawUserPrimitives<VertexPositionColor>( PrimitiveType.TriangleList, _vpcVertices, 0, 239 );
            }
           _GraphicsDevice.RasterizerState = beforeState;
        }
    }
}
