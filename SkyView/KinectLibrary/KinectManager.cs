using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows.Media;
using Microsoft.Xna.Framework;
using System.Windows.Forms;

namespace KinectLibrary
{
    public class KinectManager
    {
        private KinectSensor _kSensor;
        private static KinectManager _kmInstance = null;
        private byte[] _bPixels;

        private const int _iSkelitonCount = 6;
        private Skeleton[] _sSkelitons = new Skeleton[_iSkelitonCount];
        private MASkeliton[] _sCamSkelitons = new MASkeliton[_iSkelitonCount];
        private ValueOutput _wValueOutput = new ValueOutput();
        private bool _bShowOutputs = false;
        
        private bool _bCalculateSkelitonToCamoraPossitions = false;

        private Skeleton _Player1;

        private KinectManager()
        {
            for ( int i = 0; i < _sCamSkelitons.Length; i++ )
            {
                _sCamSkelitons[i] = new MASkeliton();   
            }
        }

        public static KinectManager Instance 
        {
            get
            {
                if( _kmInstance == null )
                {
                    _kmInstance = new KinectManager();
                }

                return _kmInstance;
            }
        }

        public bool CalculateSkelitonToCamoraPossitions
        {
            set { _bCalculateSkelitonToCamoraPossitions = value; }
            get { return _bCalculateSkelitonToCamoraPossitions; }
            
        }

        private bool StartSentor()
        {
            bool bActive = false;

            if( KinectSensor.KinectSensors.Count > 0 )
            {
                _kSensor = KinectSensor.KinectSensors[0];

                if( _kSensor.Status == KinectStatus.Connected )
                {
                    _kSensor.ColorStream.Enable();
                    _kSensor.DepthStream.Enable();
                    _kSensor.SkeletonStream.Enable();
                    _kSensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>( _kSensor_AllFramesReady ); 
             
                    try
                    {
                        _kSensor.Start();
                        bActive = true;
                    }
                    catch ( System.IO.IOException )
                    {
                        //kinect already in use
                    }

                    bActive = true;
                }                
            }

            return bActive; 
        }

        public bool StartServer()
        {
            _wValueOutput.Visible = false;
            return StartSentor();
        }

        public void ShowOutputs()
        {
            _bShowOutputs = true;
            _wValueOutput.Visible = true;
        }

        public void StopServer()
        {
            StopKinect( _kSensor );
        }

        private void StopKinect( KinectSensor sensor )
        {
            if( sensor != null )
            {
                sensor.Stop();
                //sensor.AudioSource.Stop();
            }
        }

        private void _kSensor_AllFramesReady( object sender, AllFramesReadyEventArgs e )
        {
            using( ColorImageFrame colorFrame = e.OpenColorImageFrame() )
            {
                if( colorFrame != null )
                {
                    _bPixels = new byte[colorFrame.PixelDataLength];
                    colorFrame.CopyPixelDataTo( _bPixels );            
                }                                                    
            }

            Skeleton firstSkel = getFirstSkeliton( e );

            if( firstSkel == null )
            {
                return;
            }
            
            if( _bCalculateSkelitonToCamoraPossitions )
            {
                GetCameraPoint( firstSkel, e );
            }

            _Player1 = firstSkel;
        }

        private void GetCameraPoint( Skeleton first, AllFramesReadyEventArgs e )
        {
            using ( DepthImageFrame depth = e.OpenDepthImageFrame() )
            {
                if ( depth == null )
                {
                    _wValueOutput.addValue( "Depth NUll" );
                    return;
                }

                //head
                DepthImagePoint headDepthPoint = depth.MapFromSkeletonPoint( first.Joints[JointType.Head].Position );

                ColorImagePoint headColourImagePoint = depth.MapToColorImagePoint( headDepthPoint.X, headDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );
                _sCamSkelitons[0].Head.X = headColourImagePoint.X;
                _sCamSkelitons[0].Head.Y = headColourImagePoint.Y;
                _sCamSkelitons[0].Head.Z = first.Joints[JointType.Head].Position.Z;

                // center Shoulder
                DepthImagePoint centerShoulderDepthPoint = depth.MapFromSkeletonPoint( first.Joints[JointType.ShoulderCenter].Position );

                ColorImagePoint centerShoulderColourImagePoint = depth.MapToColorImagePoint( centerShoulderDepthPoint.X, centerShoulderDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );
                _sCamSkelitons[0].CenterShoulder.X = centerShoulderColourImagePoint.X;
                _sCamSkelitons[0].CenterShoulder.Y = centerShoulderColourImagePoint.Y;
                _sCamSkelitons[0].CenterShoulder.Z = first.Joints[JointType.ShoulderCenter].Position.Z;

                //left shoulder
                DepthImagePoint leftShoulderDepthPoint = depth.MapFromSkeletonPoint( first.Joints[JointType.ShoulderLeft].Position );

                ColorImagePoint leftShoulderColourImagePoint = depth.MapToColorImagePoint( leftShoulderDepthPoint.X, leftShoulderDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );
                _sCamSkelitons[0].LeftShoulder.X = leftShoulderColourImagePoint.X;
                _sCamSkelitons[0].LeftShoulder.Y =  leftShoulderColourImagePoint.Y;
                _sCamSkelitons[0].LeftShoulder.Z = first.Joints[JointType.ShoulderLeft].Position.Z;

                //right shoulder 
                DepthImagePoint rightShoulderDepthPoint = depth.MapFromSkeletonPoint( first.Joints[JointType.ShoulderRight].Position );

                ColorImagePoint rightShoulderColourImagePoint = depth.MapToColorImagePoint( rightShoulderDepthPoint.X, rightShoulderDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );
                _sCamSkelitons[0].RightShoulder.X = rightShoulderColourImagePoint.X;
                _sCamSkelitons[0].RightShoulder.Y = rightShoulderColourImagePoint.Y;
                _sCamSkelitons[0].RightShoulder.Z = first.Joints[JointType.ShoulderRight].Position.Z;

                //left elbow
                DepthImagePoint leftElbowDepthPoint = depth.MapFromSkeletonPoint( first.Joints[JointType.ElbowLeft].Position );

                ColorImagePoint leftElbowColourImagePoint = depth.MapToColorImagePoint( leftElbowDepthPoint.X, leftElbowDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );
                _sCamSkelitons[0].LeftElboy.X =  leftElbowColourImagePoint.X;
                _sCamSkelitons[0].LeftElboy.Y = leftElbowColourImagePoint.Y;
                _sCamSkelitons[0].LeftElboy.Z = first.Joints[JointType.ElbowLeft].Position.Z;

                //right elbow 
                DepthImagePoint rightElbowDepthPoint = depth.MapFromSkeletonPoint( first.Joints[JointType.ElbowRight].Position );

                ColorImagePoint rightElbowColourImagePoint = depth.MapToColorImagePoint( rightElbowDepthPoint.X, rightElbowDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );
                _sCamSkelitons[0].RightElbow.X = rightElbowColourImagePoint.X;
                _sCamSkelitons[0].RightElbow.Y = rightElbowColourImagePoint.Y;
                _sCamSkelitons[0].RightElbow.Z = first.Joints[JointType.ElbowRight].Position.Z;

                //left wrist
                DepthImagePoint leftWristDepthPoint = depth.MapFromSkeletonPoint ( first.Joints[JointType.WristLeft].Position );

                ColorImagePoint leftWristColourImagePoint = depth.MapToColorImagePoint ( leftWristDepthPoint.X, leftWristDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );                
                _sCamSkelitons[0].LeftWrist.X = leftWristColourImagePoint.X;
                _sCamSkelitons[0].LeftWrist.Y = leftWristColourImagePoint.Y;
                _sCamSkelitons[0].LeftWrist.Z = first.Joints[JointType.WristLeft].Position.Z;

                //right wrist 
                DepthImagePoint rightWristDepthPoint = depth.MapFromSkeletonPoint ( first.Joints[JointType.WristRight].Position );

                ColorImagePoint rightWristColourImagePoint = depth.MapToColorImagePoint ( rightWristDepthPoint.X, rightWristDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );
                _sCamSkelitons[0].RightWrist.X = rightWristColourImagePoint.X;
                _sCamSkelitons[0].RightWrist.Y = rightWristColourImagePoint.Y;
                _sCamSkelitons[0].RightWrist.Z = first.Joints[JointType.WristRight].Position.Z;
                //left hand
                DepthImagePoint leftHandDepthPoint = depth.MapFromSkeletonPoint( first.Joints[JointType.HandLeft].Position );

                ColorImagePoint leftHandColourImagePoint = depth.MapToColorImagePoint(leftHandDepthPoint.X, leftHandDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );                
                _sCamSkelitons[0].LeftHand.X = leftHandColourImagePoint.X;
                _sCamSkelitons[0].LeftHand.Y = leftHandColourImagePoint.Y;
                _sCamSkelitons[0].LeftHand.Z = first.Joints[JointType.HandLeft].Position.Z;

                //right hand
                DepthImagePoint rightHandDepthPoint = depth.MapFromSkeletonPoint( first.Joints[JointType.HandRight].Position );

                ColorImagePoint rightHandColourImagePoint = depth.MapToColorImagePoint( rightHandDepthPoint.X, rightHandDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );                
                _sCamSkelitons[0].RightHand.X = rightHandColourImagePoint.X;
                _sCamSkelitons[0].RightHand.Y = rightHandColourImagePoint.Y;
                _sCamSkelitons[0].RightHand.Z = first.Joints[JointType.HandRight].Position.Z;

                //spine
                DepthImagePoint spineDepthPoint = depth.MapFromSkeletonPoint (first.Joints[JointType.Spine].Position);

                ColorImagePoint spineColourImagePoint = depth.MapToColorImagePoint ( spineDepthPoint.X, spineDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );                
                _sCamSkelitons[0].Spine.X = spineColourImagePoint.X;
                _sCamSkelitons[0].Spine.Y = spineColourImagePoint.Y;
                _sCamSkelitons[0].Spine.Z = first.Joints[JointType.Spine].Position.Z;

                // center Hip
                DepthImagePoint centerHipDepthPoint = depth.MapFromSkeletonPoint ( first.Joints[JointType.HipCenter].Position );

                ColorImagePoint centerHipColourImagePoint = depth.MapToColorImagePoint ( centerHipDepthPoint.X, centerHipDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );                
                _sCamSkelitons[0].CenterHip.X = centerHipColourImagePoint.X;
                _sCamSkelitons[0].CenterHip.Y = centerHipColourImagePoint.Y;
                _sCamSkelitons[0].CenterHip.Z = first.Joints[JointType.HipCenter].Position.Z;


                //left hip
                DepthImagePoint leftHipDepthPoint = depth.MapFromSkeletonPoint ( first.Joints[JointType.HipLeft].Position );

                ColorImagePoint leftHipColourImagePoint = depth.MapToColorImagePoint ( leftHipDepthPoint.X, leftHipDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );                
                _sCamSkelitons[0].LeftHip.X = leftHipColourImagePoint.X;
                _sCamSkelitons[0].LeftHip.Y = leftHipColourImagePoint.Y;
                _sCamSkelitons[0].LeftHip.Z = first.Joints[JointType.HipLeft].Position.Z;

                //right hip
                DepthImagePoint rightHipDepthPoint = depth.MapFromSkeletonPoint ( first.Joints[JointType.HipRight].Position );

                ColorImagePoint rightHipColourImagePoint = depth.MapToColorImagePoint ( rightHipDepthPoint.X, rightHipDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );                
                _sCamSkelitons[0].RightHip.X = rightHipColourImagePoint.X;
                _sCamSkelitons[0].RightHip.Y = rightHipColourImagePoint.Y;
                _sCamSkelitons[0].RightHip.Z = first.Joints[JointType.HipRight].Position.Z;

                //left Knee
                DepthImagePoint leftKneeDepthPoint = depth.MapFromSkeletonPoint ( first.Joints[JointType.KneeLeft].Position );

                ColorImagePoint leftKneeColourImagePoint = depth.MapToColorImagePoint ( leftKneeDepthPoint.X, leftKneeDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );                
                _sCamSkelitons[0].LeftKnee.X = leftKneeColourImagePoint.X;
                _sCamSkelitons[0].LeftKnee.Y = leftKneeColourImagePoint.Y;
                _sCamSkelitons[0].LeftKnee.Z = first.Joints[JointType.KneeLeft].Position.Z;

                //right Knee
                DepthImagePoint rightKneeDepthPoint = depth.MapFromSkeletonPoint ( first.Joints[JointType.KneeRight].Position );

                ColorImagePoint rightKneeColourImagePoint = depth.MapToColorImagePoint ( rightKneeDepthPoint.X, rightKneeDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );                
                _sCamSkelitons[0].RightKnee.X = rightKneeColourImagePoint.X;
                _sCamSkelitons[0].RightKnee.Y = rightKneeColourImagePoint.Y;
                _sCamSkelitons[0].RightKnee.Z = first.Joints[JointType.KneeRight].Position.Z;

                //left Ankle
                DepthImagePoint leftAnkleDepthPoint = depth.MapFromSkeletonPoint ( first.Joints[JointType.AnkleLeft].Position );

                ColorImagePoint leftAnkleColourImagePoint = depth.MapToColorImagePoint ( leftAnkleDepthPoint.X, leftAnkleDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );                
                _sCamSkelitons[0].LeftAnkle.X = leftAnkleDepthPoint.X;
                _sCamSkelitons[0].LeftAnkle.Y = leftAnkleDepthPoint.Y;
                _sCamSkelitons[0].LeftAnkle.Z = first.Joints[JointType.AnkleLeft].Position.Z;

                //right Ankle
                DepthImagePoint rightAnkleDepthPoint = depth.MapFromSkeletonPoint ( first.Joints[JointType.AnkleRight].Position );

                ColorImagePoint rightAnkleColourImagePoint = depth.MapToColorImagePoint ( rightAnkleDepthPoint.X, rightAnkleDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );                
                _sCamSkelitons[0].RightAnkle.X = rightAnkleColourImagePoint.X;
                _sCamSkelitons[0].RightAnkle.Y = rightAnkleColourImagePoint.Y;
                _sCamSkelitons[0].RightAnkle.Z = first.Joints[JointType.AnkleRight].Position.Z;

                //left Foot
                DepthImagePoint leftFootDepthPoint = depth.MapFromSkeletonPoint ( first.Joints[JointType.FootLeft].Position );

                ColorImagePoint leftFootColourImagePoint = depth.MapToColorImagePoint ( leftFootDepthPoint.X, leftFootDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );                
                _sCamSkelitons[0].LeftFoot.X = leftFootColourImagePoint.X;
                _sCamSkelitons[0].LeftFoot.Y = leftFootColourImagePoint.Y;
                _sCamSkelitons[0].LeftFoot.Z = first.Joints[JointType.FootLeft].Position.Z;

                //right Foot
                DepthImagePoint rightFootDepthPoint = depth.MapFromSkeletonPoint ( first.Joints[JointType.FootRight].Position );

                ColorImagePoint rightFootColourImagePoint = depth.MapToColorImagePoint ( rightFootDepthPoint.X, rightFootDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30 );                
                _sCamSkelitons[0].RightFoot.X = rightFootColourImagePoint.X;
                _sCamSkelitons[0].RightFoot.Y = rightFootColourImagePoint.Y;
                _sCamSkelitons[0].RightFoot.Z = first.Joints[JointType.FootRight].Position.Z; 

            }
        }

        private Skeleton getFirstSkeliton( AllFramesReadyEventArgs e )
        {
            using (SkeletonFrame skelitonFrame = e.OpenSkeletonFrame())
            {
                if (skelitonFrame == null)
                {
                    return null;
                }
                
                skelitonFrame.CopySkeletonDataTo( _sSkelitons );

                Skeleton first = ( from s in _sSkelitons where s.TrackingState == SkeletonTrackingState.Tracked select s ).FirstOrDefault();

                return first;
            }
        }

        public byte[] GetCamoraData()
        {
            return _bPixels;
        }

        public Skeleton GetPlayer1Skeleton()
        {
            return _Player1;
        }

        public MASkeliton GetPlayer1CamRelativeSkeleton()
        {
            return _sCamSkelitons[0];
        }
    }
    
}
