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

namespace SkyView.Classes.Objects.GameObjects.Animated
{
    class Tree : AnimatedObject
    {
        public Tree()
        {
            ObjectType = GameObjects.TREE;
        }

        public override void LoadContent( GraphicsDevice device, ContentManager content )
        {
            ModelFilePath = "Models\\Tree\\Tree_";

            _fScale = 0.8f;

            base.LoadContent( device, content );
        }
    }

}
