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
    class FlatPlain : GameObject
    {

        private VertexPositionColor[] _vertesies = new VertexPositionColor[6];

        public FlatPlain()
        {
            BuildVertesies();
        }

        private void BuildVertesies()
        {
            _vertesies[0] = new VertexPositionColor( new Vector3( -100.0f, 0.0f, 0.0f ), Color.White );
            _vertesies[1] = new VertexPositionColor( new Vector3( -100.0f, 0.0f, -100.0f ), Color.White );
            _vertesies[2] = new VertexPositionColor( new Vector3( 100.0f, 0.0f, 0.0f ), Color.White );
            _vertesies[3] = new VertexPositionColor( new Vector3( -100.0f, 0.0f, -100.0f ), Color.White );
            _vertesies[4] = new VertexPositionColor( new Vector3( 100.0f, 0.0f, -100.0f ), Color.White );
            _vertesies[5] = new VertexPositionColor( new Vector3( 100.0f, 0.0f, 0.0f ), Color.White );            
        }

        public void Draw( GraphicsDeviceManager graphics, BasicEffect effect )
        {
            VertexBuffer vertexBuffer;
            GraphicsDevice device = graphics.GraphicsDevice;

             vertexBuffer = new VertexBuffer(device, VertexPositionColor.VertexDeclaration, _vertesies.Length, BufferUsage.WriteOnly);
             vertexBuffer.SetData(_vertesies);

             device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.DarkSlateBlue, 1.0f, 0);

            effect.LightingEnabled = false;
 
             foreach (EffectPass pass in effect.CurrentTechnique.Passes)
             {
                 pass.Apply();
 
                 device.SetVertexBuffer(vertexBuffer);
                 device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
             }

             effect.LightingEnabled = true;
        }

    }
}
