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

namespace SkyView.Classes.Objects.GameObjects.NoneAnimated
{
    public class SkyDome : GameObject
    {
        Model skydomeModel;
        Vector3 skydomeRotation;
        Texture2D skydomeTexture;

        public SkyDome()
        {

        }

        public override void LoadContent( GraphicsDevice device, ContentManager content )
        {
            _GraphicsDevice = device;
            _ContentManager = content;
            skydomeModel = content.Load<Model>( "Models\\Skydome\\Skydome" );
            skydomeTexture = content.Load<Texture2D>( "Models\\Skydome\\SkydomeTex" );
            skydomeRotation = Vector3.Zero;
            
        }

        public override void Update( GameTime gameTime )
        {
            skydomeRotation += new Vector3( 0, ( float ) gameTime.ElapsedGameTime.TotalSeconds * 0.05f, 0 );
        }

        public override void Draw( Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix )
        {
            Vector3 v3SkyDomePos = SkyView.Instance.CurrentPlayer.Position;
            v3SkyDomePos.Y = -500;

            //draw the skydome
            _GraphicsDevice.DepthStencilState = DepthStencilState.None;
            foreach ( ModelMesh mesh in skydomeModel.Meshes )
            {
                foreach ( BasicEffect effect in mesh.Effects )
                {
                    effect.Texture = skydomeTexture;
                    effect.TextureEnabled = true;
                    effect.World = worldMatrix;
                    effect.World = Matrix.CreateRotationY( skydomeRotation.Y ) *
                      
                        Matrix.CreateTranslation( v3SkyDomePos );
                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh.Draw();
            }
           _GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }
    }
}
