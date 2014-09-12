using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KinectLibrary
{
    public class MASkeliton
    {
        public Vector3 Head = Vector3.Zero;
        public Vector3 CenterShoulder = Vector3.Zero;
        public Vector3 LeftShoulder = Vector3.Zero;
        public Vector3 RightShoulder = Vector3.Zero;
        public Vector3 LeftElboy = Vector3.Zero;
        public Vector3 RightElbow = Vector3.Zero;
        public Vector3 RightWrist = Vector3.Zero;
        public Vector3 LeftWrist = Vector3.Zero;
        public Vector3 RightHand = Vector3.Zero;
        public Vector3 LeftHand = Vector3.Zero;

        public Vector3 Spine = Vector3.Zero;
        public Vector3 CenterHip = Vector3.Zero;
        public Vector3 LeftHip = Vector3.Zero;
        public Vector3 RightHip = Vector3.Zero;
        public Vector3 RightKnee = Vector3.Zero;
        public Vector3 LeftKnee = Vector3.Zero;
        public Vector3 RightAnkle = Vector3.Zero;
        public Vector3 LeftAnkle = Vector3.Zero;
        public Vector3 RightFoot = Vector3.Zero;
        public Vector3 LeftFoot = Vector3.Zero;
    }
}
