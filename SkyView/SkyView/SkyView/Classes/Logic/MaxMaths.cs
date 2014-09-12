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

namespace SkyView.Classes.Logic
{
    class MaxMaths
    {
        public static Vector3[] GetLine( Vector3 position1, Vector3 position2 )
        {
            //calculate direction vector
            Vector3 vDirection = new Vector3( position1.X - position2.X, position1.Y - position2.Y, position1.Z - position2.Z );
            //calculate amplitude
            float fAmplitude = ( float ) Math.Sqrt( ( vDirection.X * vDirection.X ) + ( vDirection.Y * vDirection.Y ) + ( vDirection.Z * vDirection.Z ) );
            //normalize
            Vector3 vNormalizedDirection = Vector3.Normalize( vDirection );           
            //find points 
            //r(t) = <ax,ay,az> + t<dx,dy,dz>
            int iAmplitude = ( int ) fAmplitude;
            Vector3[] avPoints = new Vector3[iAmplitude]; 

            for ( int i = 0; i < iAmplitude; i++ )
            {
                Vector3 vPoint = position2 + ( ( ( float ) i ) * vNormalizedDirection );
                avPoints[i] = vPoint;
            }

            return avPoints;
        }

        public static float Amplitude( Vector3 vector )
        {
            float fAmplitude = ( float ) Math.Sqrt( ( vector.X * vector.X ) + ( vector.Y * vector.Y ) + ( vector.Z * vector.Z ) );

            return fAmplitude;
        }
        
        public static Vector3 NormalizeVector( Vector3 vNotNormalized )
        {
            //calculate length            
            float fAmplitude = ( float ) Math.Sqrt( ( vNotNormalized.X * vNotNormalized.X ) + ( vNotNormalized.Y * vNotNormalized.Y ) + ( vNotNormalized.Z * vNotNormalized.Z ) );

            Vector3 vNormalized = new Vector3( vNotNormalized.X / fAmplitude, vNotNormalized.Y / fAmplitude, vNotNormalized.Z / fAmplitude );

            return vNormalized;
        }
    }
}
