using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using System.IO;
using KinectLibrary;

namespace SkyView.Classes.Objects.KinectQuads
{
    class KinectVideoQuad
    {
        VertexPositionNormalTexture[] _avVertices = new VertexPositionNormalTexture[4];
        short[] _asIndices = new short[4];
        VertexBuffer _vbVertexBuffer;
        BasicEffect _beBasicEffect;
        GraphicsDevice _gdGraphicsDevice;
        IndexBuffer _ibIndexBuffer;
        Texture2D _tTexture;
        Matrix _mPossitionMatrix = new Matrix();

        public KinectVideoQuad( GraphicsDevice device, Vector3 possition, float scaleDownValue )
        {
            _gdGraphicsDevice = device;
            _mPossitionMatrix = Matrix.CreateTranslation( possition );
            _beBasicEffect = new BasicEffect( device );            

            _tTexture = new Texture2D( _gdGraphicsDevice, 640, 480, false, SurfaceFormat.Color );

            Vector2 textureLowerLeft = new Vector2( 0.0f, 1.0f );
            Vector2 textureLowerRight = new Vector2( 1.0f, 1.0f );
            Vector2 textureUpperLeft = new Vector2( 0.0f, 0.0f );
            Vector2 textureUpperRight = new Vector2( 1.0f, 0.0f );
                       
            Vector3 normal = new Vector3( 0, 0, 1 );

            float scaledHeight = 480 / scaleDownValue;
            float scaledWidth = 640 / scaleDownValue;

            _avVertices[0] = new VertexPositionNormalTexture( new Vector3( 0, 0, 0 ), normal,  textureLowerLeft );
            _avVertices[1] = new VertexPositionNormalTexture( new Vector3( scaledWidth, 0, 0 ), normal, textureLowerRight );
            _avVertices[2] = new VertexPositionNormalTexture( new Vector3( scaledWidth, scaledHeight, 0 ), normal, textureUpperRight );
            _avVertices[3] = new VertexPositionNormalTexture(new Vector3( 0, scaledHeight, 0 ), normal, textureUpperLeft );

            _vbVertexBuffer = new VertexBuffer(device, typeof(VertexPositionNormalTexture), 4, BufferUsage.WriteOnly);
            _vbVertexBuffer.SetData<VertexPositionNormalTexture>( _avVertices );

            _asIndices[0] = 1;
            _asIndices[1] = 2;
            _asIndices[2] = 0;
            _asIndices[3] = 3;

            _ibIndexBuffer = new IndexBuffer( _gdGraphicsDevice, typeof(short), _asIndices.Length, BufferUsage.WriteOnly );
            _ibIndexBuffer.SetData( _asIndices );
        }

        public void DrawQuad( Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix )
        {
            _beBasicEffect.World = _mPossitionMatrix;
            _beBasicEffect.View = viewMatrix;
            _beBasicEffect.Projection = projectionMatrix;
            
            byte[] abImageData = KinectManager.Instance.GetCamoraData();
            
            if( abImageData != null )
            {
                _tTexture.SetData<byte>(abImageData);

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
}
