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
using SkyView.Classes.Logic;

namespace SkyView.Classes.Logic
{
    public class Camera
    {
        private float _fTargetDistance;
        private Vector3 _vTargetPossition;
        private Vector3 _vPosition;
        private Vector3 _vDirection;
        
        public Camera( float fWidth, float fHeight )
        {
            UpdateProjection( fWidth, fHeight );
        }

        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }

        public void UpdateProjection( float fWidth, float fHeight )
        {
            if( fHeight == 0 )
                return ;

            float fAspect = fWidth / fHeight;

            Projection = Matrix.CreatePerspectiveFieldOfView( ( float ) Math.Atan( fAspect ), fAspect, 1, 2000 );
        }

        public Vector3 Traget
        {
            get
            { 
                return _vTargetPossition; 
            }
            set
            {
                _vTargetPossition = value;
                RecalculateMatrix();
            }
        }

        public float Distance
        {
            get
            {
                return _fTargetDistance;
            }
            set
            {
                _fTargetDistance = value;
                RecalculateMatrix();
            }
        }

        public Vector3 Direction
        {
            get
            {
                return _vDirection;
            }
            set
            {
                _vDirection = value;
                RecalculateMatrix ();
            }
        }

        private void RecalculateMatrix()
        {
            _vPosition = _vTargetPossition - _vDirection * _fTargetDistance;
            _vPosition = GetPositionBasedOnTerrain( _vPosition );

            View = Matrix.CreateLookAt( _vPosition, _vTargetPossition, 
                                          Vector3.Transform( Vector3.Up, RotationFromForward( _vDirection ) ) );
        }

        private Vector3 GetPositionBasedOnTerrain( Vector3 vPossition )
        {
            Vector3 vPosDirection = vPossition + -Direction * 2;
            Vector3[] vaPoints = MaxMaths.GetLine( vPosDirection, _vTargetPossition );

            //check all points for collision
            foreach ( Vector3 vPoint in vaPoints )
            {
                //check for collision at point
                float fTerrPoint = SkyView.Instance.CurrentTerrain.GetExactHeightAt( vPoint.X, -vPoint.Z );
                
                if ( fTerrPoint > vPoint.Y )
                {
                   // vPossition.Y = fTerrPoint;
                    vPossition.X = vPoint.X + Direction.X * 2;
                    vPossition.Z = vPoint.Z + Direction.X * 2;
                    break;
                }
            }

            return vPossition;
        }

        private Matrix RotationFromForward( Vector3 vForwared )
        {
            float x = ( float ) Math.Asin( vForwared.Y );
            float y = ( float ) -Math.Atan2( vForwared.X, -vForwared.Z );
            Matrix m = Matrix.CreateRotationX( x ) * Matrix.CreateRotationY( y );

            return m;
        }

        public void Update()
        {
            Direction = SkyView.Instance.CurrentPlayer.RotationM.Forward;
            Traget = new Vector3( SkyView.Instance.CurrentPlayer.Position.X, SkyView.Instance.CurrentPlayer.Position.Y + 10, SkyView.Instance.CurrentPlayer.Position.Z );
            Distance = 50;
        }
    }
}
