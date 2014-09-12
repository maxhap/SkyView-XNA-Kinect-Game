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

namespace SkyView.Classes.Objects.GameObjects.NoneAnimated
{
    class Cross : GameObject
    {
        public Cross()
        {

        }

        public override void LoadContent( GraphicsDevice device, ContentManager content )
        {
            base.LoadContent( device, content );
            _Model = _ContentManager.Load<Model>( "Models\\cross" );

        }

        public override void HandleColision( int GameObjectType )
        {
            SkyView.Instance.ScoreMultiplier++;
            SkyView.Instance.CurrentAudioManager.SetSingleCue( "Wahooo" );

            this.Alive = false;
        }

        public override void Update( GameTime gameTime )
        {
            _v3Rotation.Y += ( float ) gameTime.ElapsedGameTime.TotalSeconds * 2.0f;
            RecalculateRotationMatrix();
        }
    }
}
