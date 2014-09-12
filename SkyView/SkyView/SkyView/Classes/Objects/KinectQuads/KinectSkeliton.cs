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
using KinectLibrary;

namespace SkyView.Classes.Objects.KinectQuads
{
    class KinectSkeliton
    {
        private KinectTexturedQuad _tqHead = null;

        private KinectTexturedQuad _tqCenterShoulder = null;
        private KinectTexturedQuad _tqLeftShoulder = null;
        private KinectTexturedQuad _tqRightShoulder = null;

        private KinectTexturedQuad _tqLeftHand = null;
        private KinectTexturedQuad _tqRightHand = null;

        private KinectTexturedQuad _tqSpine = null;

        private KinectTexturedQuad _tqCenterHip = null;
        private KinectTexturedQuad _tqLeftHip = null;
        private KinectTexturedQuad _tqRightHip = null;

        private KinectTexturedQuad _tqLeftKnee = null;
        private KinectTexturedQuad _tqRightKnee = null;

        private KinectTexturedQuad _tqLeftAnkle = null;
        private KinectTexturedQuad _tqRightAnkle = null;

        private KinectVideoQuad _kvQuad;
        private const float SCALEFACTOR = 5;
        
        public KinectSkeliton( GraphicsDevice graphicsDevice, ContentManager content )
        {
            KinectManager.Instance.CalculateSkelitonToCamoraPossitions = true;

            _kvQuad = new KinectVideoQuad( graphicsDevice, new Vector3( 0, 0, -200 ), SCALEFACTOR );  
            
            _tqHead = new KinectTexturedQuad( graphicsDevice, content.Load<Texture2D>( "head" ), new Vector3( 0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199 ) );
           
            _tqCenterShoulder = new KinectTexturedQuad( graphicsDevice, content.Load<Texture2D>( "body_point" ), new Vector3( 0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199 ) );
            _tqLeftShoulder = new KinectTexturedQuad( graphicsDevice, content.Load<Texture2D>( "body_point" ), new Vector3( 0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199 ) );        
            _tqRightShoulder = new KinectTexturedQuad( graphicsDevice, content.Load<Texture2D>( "body_point" ), new Vector3( 0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199 ) );        
            
            _tqLeftHand = new KinectTexturedQuad( graphicsDevice, content.Load<Texture2D>( "hand" ), new Vector3( 0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199 ) );
            _tqRightHand = new KinectTexturedQuad( graphicsDevice, content.Load<Texture2D>( "hand" ), new Vector3( 0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199 ) );

            _tqSpine = new KinectTexturedQuad( graphicsDevice, content.Load<Texture2D>( "body_point" ), new Vector3( 0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199 ) );        
           
            _tqCenterHip = new KinectTexturedQuad( graphicsDevice, content.Load<Texture2D>( "body_point" ), new Vector3( 0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199 ) );
            _tqLeftHip = new KinectTexturedQuad( graphicsDevice, content.Load<Texture2D>( "body_point" ), new Vector3( 0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199 ) );
            _tqRightHip = new KinectTexturedQuad( graphicsDevice, content.Load<Texture2D>( "body_point" ), new Vector3( 0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199 ) );

            _tqLeftKnee = new KinectTexturedQuad (graphicsDevice, content.Load<Texture2D> ("body_point"), new Vector3 (0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199));
            _tqRightKnee = new KinectTexturedQuad (graphicsDevice, content.Load<Texture2D> ("body_point"), new Vector3 (0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199));

            _tqLeftAnkle = new KinectTexturedQuad (graphicsDevice, content.Load<Texture2D> ("body_point"), new Vector3 (0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199));
            _tqRightAnkle = new KinectTexturedQuad (graphicsDevice, content.Load<Texture2D> ("body_point"), new Vector3 (0, KinectInfo.PIXELHEIGHT / SCALEFACTOR, -199));
        }

        public void UpdateSceliton()
        {
            MASkeliton sCamSkel = KinectManager.Instance.GetPlayer1CamRelativeSkeleton();

            if( sCamSkel != null )
            {
                _tqHead.UpdatePossition( sCamSkel.Head / SCALEFACTOR );

                _tqCenterShoulder.UpdatePossition( sCamSkel.CenterShoulder / SCALEFACTOR );
                _tqLeftShoulder.UpdatePossition( sCamSkel.LeftShoulder / SCALEFACTOR );
                _tqRightShoulder.UpdatePossition( sCamSkel.RightShoulder / SCALEFACTOR );

                _tqLeftHand.UpdatePossition( sCamSkel.LeftHand / SCALEFACTOR );
                _tqRightHand.UpdatePossition( sCamSkel.RightHand / SCALEFACTOR );

                _tqSpine.UpdatePossition( sCamSkel.Spine / SCALEFACTOR );

                _tqCenterHip.UpdatePossition( sCamSkel.CenterHip / SCALEFACTOR );
                _tqLeftHip.UpdatePossition( sCamSkel.LeftHip / SCALEFACTOR );
                _tqRightHip.UpdatePossition( sCamSkel.RightHip / SCALEFACTOR );

                _tqLeftKnee.UpdatePossition (sCamSkel.LeftKnee / SCALEFACTOR);
                _tqRightKnee.UpdatePossition (sCamSkel.RightKnee / SCALEFACTOR);

                _tqLeftAnkle.UpdatePossition (sCamSkel.LeftAnkle / SCALEFACTOR);
                _tqRightAnkle.UpdatePossition (sCamSkel.RightAnkle / SCALEFACTOR);
            }
        }

        public void DrawSceliton( Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix )
        {
            _kvQuad.DrawQuad( worldMatrix, viewMatrix, projectionMatrix );
            _tqHead.DrawQuad( worldMatrix, viewMatrix, projectionMatrix );
            _tqCenterShoulder.DrawQuad( worldMatrix, viewMatrix, projectionMatrix );
            _tqLeftShoulder.DrawQuad( worldMatrix, viewMatrix, projectionMatrix );
            _tqRightShoulder.DrawQuad( worldMatrix, viewMatrix, projectionMatrix );
            _tqLeftHand.DrawQuad( worldMatrix, viewMatrix, projectionMatrix );
            _tqRightHand.DrawQuad( worldMatrix, viewMatrix, projectionMatrix );

            _tqSpine.DrawQuad( worldMatrix, viewMatrix, projectionMatrix );
            _tqLeftHip.DrawQuad( worldMatrix, viewMatrix, projectionMatrix );
            _tqRightHip.DrawQuad( worldMatrix, viewMatrix, projectionMatrix );

            _tqLeftKnee.DrawQuad( worldMatrix, viewMatrix, projectionMatrix );
             _tqRightKnee.DrawQuad( worldMatrix, viewMatrix, projectionMatrix );

             _tqLeftAnkle.DrawQuad (worldMatrix, viewMatrix, projectionMatrix);
             _tqRightAnkle.DrawQuad (worldMatrix, viewMatrix, projectionMatrix);
        }
    }
}
