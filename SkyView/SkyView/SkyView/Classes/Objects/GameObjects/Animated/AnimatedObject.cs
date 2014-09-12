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
using SkinnedModel;

namespace SkyView.Classes.Objects.GameObjects.Animated
{
    public class AnimatedObject : GameObject
    {
        private AnimationPlayer _AnimationPlayer;                

        public AnimatedObject ()
        {            
        }

        public override void LoadContent( GraphicsDevice device, ContentManager content )
        {
            _Model = content.Load<Model>( ModelFilePath );

             // Look up our custom skinning information.
             SkinningData skinningData = _Model.Tag as SkinningData;

             if (skinningData == null)
                 throw new InvalidOperationException
                     ("This model does not contain a SkinningData tag.");

             // Create an animation player, and start decoding an animation clip.
             _AnimationPlayer = new AnimationPlayer( skinningData );

             AnimationClip clip = skinningData.AnimationClips["ArmatureAction"];

             _AnimationPlayer.StartClip(clip);

             base.LoadContent( device, content );
        }

        public void ReloadAnimationClip()
        {
            // Look up our custom skinning information.
            SkinningData skinningData = _Model.Tag as SkinningData;

            if ( skinningData == null )
                throw new InvalidOperationException
                    ( "This model does not contain a SkinningData tag." );

            // Create an animation player, and start decoding an animation clip.
            _AnimationPlayer = new AnimationPlayer( skinningData );

            AnimationClip clip = skinningData.AnimationClips["ArmatureAction"];

            _AnimationPlayer.StartClip( clip );
        }

        public override void Update( GameTime gameTime )
        {
            base.Update( gameTime );

            _AnimationPlayer.Update( gameTime.ElapsedGameTime, true, Matrix.Identity );
        }

        public override void Draw( Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix )
        {
            Vector3 currentRotation = this.Rotation;

            Matrix[] bones = _AnimationPlayer.GetSkinTransforms();

            // Render the skinned mesh.
            foreach ( ModelMesh mesh in _Model.Meshes )
            {
                foreach ( SkinnedEffect seffect in mesh.Effects )
                {
                    seffect.SetBoneTransforms(bones);

                    seffect.View = viewMatrix;
                    seffect.Projection = projectionMatrix;
                    seffect.World = RotationM * Matrix.CreateScale( Scale ) * Matrix.CreateTranslation( Position );

                    seffect.EnableDefaultLighting();

                    seffect.SpecularColor = new Vector3(0.25f);
                    seffect.SpecularPower = 16;
                }

                mesh.Draw();
            }
        }
    }


}
