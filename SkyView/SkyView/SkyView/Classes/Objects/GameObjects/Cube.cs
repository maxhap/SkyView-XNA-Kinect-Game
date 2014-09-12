using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SkyView.Classes.Objects.GameObjects
{
    class Cube : GameObject
    {
        private VertexPositionNormalTexture[] _vpntVertices;        
        private float _fSize = 0.0f;
        private const int NUM_OF_VERTECIES = 36;
        private const int NUM_TRIANGLES = 12;
        private Texture2D _tTexture = null;

        public Vector3 shapeSize;
        private VertexPositionNormalTexture[] shapeVertices;
        private int shapeTriangles;
        private VertexBuffer shapeBuffer;
        public Texture2D shapeTexture;     

        public Cube( Vector3 position, float size )
        {
            _v3Position = position;
            _fSize = size; 
            
            shapeSize = new Vector3 ( size, size, size );
        }

        public VertexPositionNormalTexture[] Vertesies
        {
            get
            {
                return _vpntVertices;
            }
        }

        public Texture2D Texture 
        {
            get
            {
                return _tTexture;
            }
            set
            {
                _tTexture = value;
            }
        }

    private void BuildShape()
    {
      shapeTriangles = 12;

      shapeVertices =
        new VertexPositionNormalTexture[36];

      Vector3 topLeftFront = _v3Position+
        new Vector3(-1.0f, 1.0f, -1.0f) * shapeSize;
      Vector3 bottomLeftFront = _v3Position+
        new Vector3(-1.0f, -1.0f, -1.0f) * shapeSize;
      Vector3 topRightFront = _v3Position +
        new Vector3(1.0f, 1.0f, -1.0f) * shapeSize;
      Vector3 bottomRightFront = _v3Position +
        new Vector3(1.0f, -1.0f, -1.0f) * shapeSize;
      Vector3 topLeftBack = _v3Position +
        new Vector3(-1.0f, 1.0f, 1.0f) * shapeSize;
      Vector3 topRightBack = _v3Position +
        new Vector3(1.0f, 1.0f, 1.0f) * shapeSize;
      Vector3 bottomLeftBack = _v3Position +
        new Vector3(-1.0f, -1.0f, 1.0f) * shapeSize;
      Vector3 bottomRightBack = _v3Position +
        new Vector3(1.0f, -1.0f, 1.0f) * shapeSize;

      Vector3 frontNormal =
        new Vector3(0.0f, 0.0f, 1.0f) * shapeSize;
      Vector3 backNormal =
        new Vector3(0.0f, 0.0f, -1.0f) * shapeSize;
      Vector3 topNormal =
        new Vector3(0.0f, 1.0f, 0.0f) * shapeSize;
      Vector3 bottomNormal =
        new Vector3(0.0f, -1.0f, 0.0f) * shapeSize;
      Vector3 leftNormal =
        new Vector3(-1.0f, 0.0f, 0.0f) * shapeSize;
      Vector3 rightNormal =
        new Vector3(1.0f, 0.0f, 0.0f) * shapeSize;

      Vector2 textureTopLeft =
        new Vector2(0.5f * shapeSize.X,
        0.0f * shapeSize.Y);
      Vector2 textureTopRight =
        new Vector2(0.0f * shapeSize.X,
        0.0f * shapeSize.Y);
      Vector2 textureBottomLeft =
        new Vector2(0.5f * shapeSize.X,
        0.5f * shapeSize.Y);
      Vector2 textureBottomRight =
        new Vector2(0.0f * shapeSize.X,
        0.5f * shapeSize.Y);

      // Front face.
      shapeVertices[0] =
        new VertexPositionNormalTexture(
        topLeftFront, frontNormal,
        textureTopLeft);
      shapeVertices[1] =
        new VertexPositionNormalTexture(
        bottomLeftFront, frontNormal,
        textureBottomLeft);
      shapeVertices[2] =
        new VertexPositionNormalTexture(
        topRightFront, frontNormal,
        textureTopRight);
      shapeVertices[3] =
        new VertexPositionNormalTexture(
        bottomLeftFront, frontNormal,
        textureBottomLeft);
      shapeVertices[4] =
        new VertexPositionNormalTexture(
        bottomRightFront, frontNormal,
        textureBottomRight);
      shapeVertices[5] =
        new VertexPositionNormalTexture(
        topRightFront, frontNormal,
        textureTopRight);

      // Back face.
      shapeVertices[6] =
        new VertexPositionNormalTexture(
        topLeftBack, backNormal,
        textureTopRight);
      shapeVertices[7] =
        new VertexPositionNormalTexture(
        topRightBack, backNormal,
        textureTopLeft);
      shapeVertices[8] =
        new VertexPositionNormalTexture(
        bottomLeftBack, backNormal,
        textureBottomRight);
      shapeVertices[9] =
        new VertexPositionNormalTexture(
        bottomLeftBack, backNormal,
        textureBottomRight);
      shapeVertices[10] =
        new VertexPositionNormalTexture(
        topRightBack, backNormal,
        textureTopLeft);
      shapeVertices[11] =
        new VertexPositionNormalTexture(
        bottomRightBack, backNormal,
        textureBottomLeft);

      // Top face.
      shapeVertices[12] =
        new VertexPositionNormalTexture(
        topLeftFront, topNormal,
        textureBottomLeft);
      shapeVertices[13] =
        new VertexPositionNormalTexture(
        topRightBack, topNormal,
        textureTopRight);
      shapeVertices[14] =
        new VertexPositionNormalTexture(
        topLeftBack, topNormal,
        textureTopLeft);
      shapeVertices[15] =
        new VertexPositionNormalTexture(
        topLeftFront, topNormal,
        textureBottomLeft);
      shapeVertices[16] =
        new VertexPositionNormalTexture(
        topRightFront, topNormal,
        textureBottomRight);
      shapeVertices[17] =
        new VertexPositionNormalTexture(
        topRightBack, topNormal,
        textureTopRight);

      // Bottom face. 
      shapeVertices[18] =
        new VertexPositionNormalTexture(
        bottomLeftFront, bottomNormal,
        textureTopLeft);
      shapeVertices[19] =
        new VertexPositionNormalTexture(
        bottomLeftBack, bottomNormal,
        textureBottomLeft);
      shapeVertices[20] =
        new VertexPositionNormalTexture(
        bottomRightBack, bottomNormal,
        textureBottomRight);
      shapeVertices[21] =
        new VertexPositionNormalTexture(
        bottomLeftFront, bottomNormal,
        textureTopLeft);
      shapeVertices[22] =
        new VertexPositionNormalTexture(
        bottomRightBack, bottomNormal,
        textureBottomRight);
      shapeVertices[23] =
        new VertexPositionNormalTexture(
        bottomRightFront, bottomNormal,
        textureTopRight);

      // Left face.
      shapeVertices[24] =
        new VertexPositionNormalTexture(
        topLeftFront, leftNormal,
        textureTopRight);
      shapeVertices[25] =
        new VertexPositionNormalTexture(
        bottomLeftBack, leftNormal,
        textureBottomLeft);
      shapeVertices[26] =
        new VertexPositionNormalTexture(
        bottomLeftFront, leftNormal,
        textureBottomRight);
      shapeVertices[27] =
        new VertexPositionNormalTexture(
        topLeftBack, leftNormal,
        textureTopLeft);
      shapeVertices[28] =
        new VertexPositionNormalTexture(
        bottomLeftBack, leftNormal,
        textureBottomLeft);
      shapeVertices[29] =
        new VertexPositionNormalTexture(
        topLeftFront, leftNormal,
        textureTopRight);

      // Right face. 
      shapeVertices[30] =
        new VertexPositionNormalTexture(
        topRightFront, rightNormal,
        textureTopLeft);
      shapeVertices[31] =
        new VertexPositionNormalTexture(
        bottomRightFront, rightNormal,
        textureBottomLeft);
      shapeVertices[32] =
        new VertexPositionNormalTexture(
        bottomRightBack, rightNormal,
        textureBottomRight);
      shapeVertices[33] =
        new VertexPositionNormalTexture(
        topRightBack, rightNormal,
        textureTopRight);
      shapeVertices[34] =
        new VertexPositionNormalTexture(
        topRightFront, rightNormal,
        textureTopLeft);
      shapeVertices[35] =
        new VertexPositionNormalTexture(
        bottomRightBack, rightNormal,
        textureBottomRight);
    }
        

        public void Draw( GraphicsDeviceManager graphics, BasicEffect effect )
        {
            RasterizerState beforeState = graphics.GraphicsDevice.RasterizerState;

            // Build the cube, setting up the _vertices array
           if ( _vpntVertices == null )
           {
               BuildShape();
           }
       
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.CullClockwiseFace;
            graphics.GraphicsDevice.RasterizerState = rs;
            
            //effect.CurrentTechnique = effect.Techniques["TexturedNoShading"];
            effect.Texture = _tTexture;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, _vpntVertices, 0, 12);
            }

            graphics.GraphicsDevice.RasterizerState = beforeState;
        }
    }
}
