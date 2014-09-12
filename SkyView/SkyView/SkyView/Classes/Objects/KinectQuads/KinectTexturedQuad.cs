using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms;

namespace SkyView.Classes.Objects.KinectQuads
{
    public class KinectTexturedQuad
    {
        VertexPositionNormalTexture[] _avVertices = new VertexPositionNormalTexture[4];
        VertexPositionNormalTexture[] _avOriginVertices = new VertexPositionNormalTexture[4];

        short[] _asIndices = new short[4];
        VertexBuffer _vbVertexBuffer;
        BasicEffect _beBasicEffect;
        GraphicsDevice _gdGraphicsDevice;
        IndexBuffer _ibIndexBuffer;
        Texture2D _tTexture = null;
        Matrix _mPossitionMatrix = new Matrix();
        Matrix _mOrigionlPossitionMatrix = new Matrix();

        public KinectTexturedQuad( GraphicsDevice device, Texture2D texture, Vector3 possition )
        {
            _gdGraphicsDevice = device;
            _beBasicEffect = new BasicEffect( device );
            _tTexture  = texture;

            _mPossitionMatrix = Matrix.CreateTranslation( possition );
            _mOrigionlPossitionMatrix = Matrix.CreateTranslation( possition );

            Vector2 textureLowerLeft = new Vector2( 0.0f, 1.0f );
            Vector2 textureLowerRight = new Vector2( 1.0f, 1.0f );
            Vector2 textureUpperLeft = new Vector2( 0.0f, 0.0f );
            Vector2 textureUpperRight = new Vector2( 1.0f, 0.0f );
                       
            Vector3 normal = new Vector3( 0, 0, 1 );

            _avVertices[0] = new VertexPositionNormalTexture( new Vector3( -5, 0, 0 ), normal,  textureLowerLeft );
            _avVertices[1] = new VertexPositionNormalTexture( new Vector3( 5, 0, 0 ), normal, textureLowerRight );
            _avVertices[2] = new VertexPositionNormalTexture( new Vector3( 5, 5, 0 ), normal, textureUpperRight );
            _avVertices[3] = new VertexPositionNormalTexture(new Vector3(-5, 5, 0 ), normal, textureUpperLeft );

            _avOriginVertices[0] =  _avVertices[0];
            _avOriginVertices[1] =  _avVertices[1];
            _avOriginVertices[2] =  _avVertices[2];
            _avOriginVertices[3] =  _avVertices[3];

            _vbVertexBuffer = new VertexBuffer(device, typeof(VertexPositionNormalTexture), 4, BufferUsage.WriteOnly);
            _vbVertexBuffer.SetData<VertexPositionNormalTexture>( _avVertices );

            _asIndices[0] = 1;
            _asIndices[1] = 2;
            _asIndices[2] = 0;
            _asIndices[3] = 3;

            _ibIndexBuffer = new IndexBuffer( _gdGraphicsDevice, typeof(short), _asIndices.Length, BufferUsage.WriteOnly );
            _ibIndexBuffer.SetData( _asIndices );
        }

        public void UpdatePossition( Vector3 possitionIncrease )
        {
            possitionIncrease.Y *= -1; 
            _mPossitionMatrix = _mOrigionlPossitionMatrix * Matrix.CreateTranslation( possitionIncrease );
        }

        public void DrawQuad( Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix )
        {
            _beBasicEffect.World = _mPossitionMatrix;
            _beBasicEffect.View = viewMatrix;
            _beBasicEffect.Projection = projectionMatrix;
            _beBasicEffect.Texture = _tTexture;
            _beBasicEffect.TextureEnabled = true;
            _beBasicEffect.EnableDefaultLighting();             
            
            _gdGraphicsDevice.SetVertexBuffer( _vbVertexBuffer );
            _gdGraphicsDevice.Indices = _ibIndexBuffer;

            _beBasicEffect.CurrentTechnique.Passes[0].Apply();

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            _gdGraphicsDevice.RasterizerState = rasterizerState;

            _gdGraphicsDevice.DrawIndexedPrimitives( PrimitiveType.TriangleStrip, 0, 0, _avVertices.Length, 0, 2 );           
        }
    }
}
