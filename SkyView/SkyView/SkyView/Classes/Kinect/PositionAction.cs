using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using KinectLibrary;
using Microsoft.Xna.Framework;
using SkyView.Classes.Logic;

namespace SkyView.Classes.Kinect
{
    class PositionAction : ActionNode
    {
        public static int LEFTOF = 0;
        public static int RIGHTOF = 1;
        public static int INFRONT = 2;
        public static int BEHIND = 3 ;
        public static int ABOVE = 4;
        public static int BELOW = 5;
        public static int TOGETHER = 6;
        public static int APART = 7;

        private float _DistnaceContraint = 0;
        public PositionAction( string id, string eventKey, int relationship, JointType bodyPart1, JointType bodyPart2, float distance, float distanceConstraint = 0 )
            : base( id, eventKey, relationship, bodyPart1, bodyPart2, distance )
        {
            _DistnaceContraint = distanceConstraint;
        }

        public override bool Acept()
        {
            bool acept = false;
            if ( _Relationship == LEFTOF )
            {
                acept = ToTheLeftOf();
            }

            else if ( _Relationship == RIGHTOF )
            {
                acept = ToTheRightOf();
            }

            else if ( _Relationship == INFRONT )
            {
                acept = InFront();
            }

            else if ( _Relationship == BEHIND )
            {
                acept = Behind();
            }

            else if ( _Relationship == ABOVE )
            {
                acept = Above();
            }

            else if ( _Relationship == BELOW )
            {
                acept = Below();
            }

            else if ( _Relationship == TOGETHER )
            {
                acept = Together();
            }

            else if ( _Relationship == APART )
            {
                acept = Apart();
            }

            if ( acept )
            {
                this.Active = true;
                KinectGestures.Instance.GestureEvent( EventKey );                

                Proceed();
            }

            return acept;
        }

        private bool ToTheLeftOf()
        {
            Skeleton player = KinectGestures.Instance.Player1;
            float distance = Math.Abs( player.Joints[_BodyPart1].Position.X - player.Joints[_BodyPart2].Position.X );

            if ( player.Joints[_BodyPart1].Position.X < player.Joints[_BodyPart2].Position.X && distance < _Distance && distance > _DistnaceContraint )
            {
                return true;
            }

            return false;
        }

        private bool ToTheRightOf()
        {
            Skeleton player = KinectGestures.Instance.Player1;
            float distance = Math.Abs(  player.Joints[_BodyPart1].Position.X - player.Joints[_BodyPart2].Position.X );

            if ( player.Joints[_BodyPart1].Position.X > player.Joints[_BodyPart2].Position.X && distance < _Distance && distance > _DistnaceContraint )
            {
                return true;
            }

            return false;
        }

        private bool InFront()
        {
            Skeleton player = KinectManager.Instance.GetPlayer1Skeleton();
            float distance = Math.Abs(  player.Joints[_BodyPart1].Position.Z - player.Joints[_BodyPart2].Position.Z );

            if ( player.Joints[_BodyPart1].Position.Z < player.Joints[_BodyPart2].Position.Z && distance < _Distance && distance > _DistnaceContraint )
            {
                 return true;
            }

            return false;
        }

        private bool Behind()
        {
            Skeleton player = KinectManager.Instance.GetPlayer1Skeleton();
            float distance = Math.Abs( player.Joints[_BodyPart1].Position.Z - player.Joints[_BodyPart2].Position.Z );

            if ( player.Joints[_BodyPart1].Position.Z > player.Joints[_BodyPart2].Position.Z && distance < _Distance && distance > _DistnaceContraint )
            {
                return true;
            }

            return false;
        }

        private bool Above()
        {
            Skeleton player = KinectGestures.Instance.Player1;
            float distance = Math.Abs( player.Joints[_BodyPart1].Position.Y - player.Joints[_BodyPart2].Position.Y );

            if ( player.Joints[_BodyPart1].Position.Y > player.Joints[_BodyPart2].Position.Y && distance < _Distance && distance > _DistnaceContraint )
            {
                return true;
            }

            return false;
        }

        private bool Below()
        {
            Skeleton player = KinectGestures.Instance.Player1;
            float distance = Math.Abs( player.Joints[_BodyPart1].Position.Y - player.Joints[_BodyPart2].Position.Y );

            if ( player.Joints[_BodyPart1].Position.Y < player.Joints[_BodyPart2].Position.Y && distance < _Distance && distance > _DistnaceContraint )
            {
                return true;
            }

            return false;
        }

        private bool Together()
        {
            Skeleton player = KinectGestures.Instance.Player1;

            Vector3 distance = Vector3.Zero;

            distance.X = Math.Abs( player.Joints[_BodyPart1].Position.X - player.Joints[_BodyPart2].Position.X );
            distance.Y = Math.Abs( player.Joints[_BodyPart1].Position.Y - player.Joints[_BodyPart2].Position.Y );
            distance.Z = Math.Abs( player.Joints[_BodyPart1].Position.Z - player.Joints[_BodyPart2].Position.Z );

            float amplitude = MaxMaths.Amplitude( distance );

            if ( player.Joints[_BodyPart1].Position.Y < player.Joints[_BodyPart2].Position.Y && amplitude < _Distance && amplitude > _DistnaceContraint )
            {
                return true;
            }

            return false;
        }

        private bool Apart()
        {
            Skeleton player = KinectGestures.Instance.Player1;

            Vector3 distance = Vector3.Zero;

            distance.X = Math.Abs( player.Joints[_BodyPart1].Position.X - player.Joints[_BodyPart2].Position.X );
            distance.Y = Math.Abs( player.Joints[_BodyPart1].Position.Y - player.Joints[_BodyPart2].Position.Y );
            distance.Z = Math.Abs( player.Joints[_BodyPart1].Position.Z - player.Joints[_BodyPart2].Position.Z );

            float amplitude = MaxMaths.Amplitude( distance );

            if ( player.Joints[_BodyPart1].Position.Y < player.Joints[_BodyPart2].Position.Y && amplitude > _Distance && amplitude > _DistnaceContraint )
            {
                return true;
            }

            return false;
        }
    }
}
