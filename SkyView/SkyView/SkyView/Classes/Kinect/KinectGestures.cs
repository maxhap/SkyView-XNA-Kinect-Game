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
using KinectLibrary;
using Microsoft.Kinect;
using SkyView.Classes.Logic;

namespace SkyView.Classes.Kinect
{
    class KinectGestures
    {
        public bool MenuItemUp { get; set; }
        public bool MenuItemDown { get; set; }        

        private static KinectGestures _Instance = null;
        public Skeleton Player1 { get; private set; }

        private List<ActionNode> _Gestures = new List<ActionNode>();        

        private KinectGestures()
        {
            BuildMenuGestures();            
        }

        public static KinectGestures Instance
        {
            get 
            {
                if ( _Instance == null )
                {
                    _Instance = new KinectGestures();                   
                }

                return _Instance;
            }
        }

        private void BuildMenuGestures()
        {
            // menu up gesture
            ActionNode menuUpStub = new PositionAction( "menuUpStub", "", PositionAction.LEFTOF, JointType.HandRight, JointType.ShoulderCenter, 0.1f );
            menuUpStub.Active = true;

            ActionNode nodeCenterBodyBesidesUp = new PositionAction( "nodeCenterBodyBesidesUp", "", PositionAction.RIGHTOF, JointType.HandRight, JointType.ShoulderCenter, 0.2f );
            nodeCenterBodyBesidesUp.RootOfGesture = menuUpStub;

            menuUpStub.NextAction = nodeCenterBodyBesidesUp;

            ActionNode nodeCenterBodyInfrontUp = new PositionAction( "nodeCenterBodyInfrontUp", "", PositionAction.INFRONT, JointType.HandRight, JointType.ShoulderCenter, 1.0f );
            nodeCenterBodyInfrontUp.RootOfGesture = menuUpStub;

            nodeCenterBodyBesidesUp.NextAction = nodeCenterBodyInfrontUp;

            ActionNode nodeCenterBodyAboveUp = new PositionAction( "nodeCenterBodyAboveUp", "", PositionAction.ABOVE, JointType.HandRight, JointType.Head, 0.5f );
            nodeCenterBodyAboveUp.RootOfGesture = menuUpStub;

            nodeCenterBodyInfrontUp.NextAction = nodeCenterBodyAboveUp;

            ActionNode nodeCenterBodyBelowUp = new PositionAction( "nodeCenterBodyBelowUp", "MenuItemUp", PositionAction.BELOW, JointType.HandRight, JointType.ShoulderCenter, 0.5f );
            nodeCenterBodyBelowUp.RootOfGesture = menuUpStub;

            nodeCenterBodyAboveUp.NextAction = nodeCenterBodyBelowUp;

            _Gestures.Add( menuUpStub );
            //end of menu up gesture

            // menu down gesture 
            ActionNode menuDownStub = new PositionAction( "menuDownStub", "", PositionAction.LEFTOF, JointType.HandRight, JointType.ShoulderCenter, 0.1f );
            menuUpStub.Active = true;

            ActionNode nodeCenterBodyBesidesDown = new PositionAction( "nodeCenterBodyBesidesDown", "", PositionAction.RIGHTOF, JointType.HandRight, JointType.ShoulderCenter, 0.2f );
            nodeCenterBodyBesidesDown.RootOfGesture = menuDownStub;

            menuDownStub.NextAction = nodeCenterBodyBesidesDown;

            ActionNode nodeCenterBodyInfrontDown = new PositionAction( "nodeCenterBodyInfrontDown", "", PositionAction.INFRONT, JointType.HandRight, JointType.ShoulderCenter, 1.0f );
            nodeCenterBodyInfrontDown.RootOfGesture = menuDownStub;

            nodeCenterBodyBesidesDown.NextAction = nodeCenterBodyInfrontDown;

            ActionNode nodeCenterBodyBelowDown = new PositionAction( "nodeCenterBodyBelowDown", "", PositionAction.BELOW, JointType.HandRight, JointType.Spine, 0.5f );
            nodeCenterBodyBelowDown.RootOfGesture = menuDownStub;

            nodeCenterBodyInfrontDown.NextAction = nodeCenterBodyBelowDown;

            ActionNode nodeCenterBodyAboveDown = new PositionAction( "nodeCenterBodyAboveDown", "MenuItemDown", PositionAction.ABOVE, JointType.HandRight, JointType.ShoulderCenter, 0.1f );
            nodeCenterBodyAboveDown.RootOfGesture = menuDownStub;

            nodeCenterBodyBelowDown.NextAction = nodeCenterBodyAboveDown;

            _Gestures.Add( menuDownStub );
            //end of menu down gesture

            //select menu
            ActionNode menuSelectStub = new PositionAction( "menuDownStub", "", PositionAction.LEFTOF, JointType.HandRight, JointType.ShoulderCenter, 0.1f );
            menuUpStub.Active = true;

            ActionNode nodeHandsTogether = new PositionAction( "nodeHandsTogether", "", PositionAction.TOGETHER, JointType.HandRight, JointType.HandLeft, 0.1f );
            nodeHandsTogether.RootOfGesture = menuSelectStub;

            menuSelectStub.NextAction = nodeHandsTogether;

            ActionNode nodeHandsAppart = new PositionAction( "nodeHandsAppart", "MenuItemSelect", PositionAction.APART, JointType.HandRight, JointType.HandLeft, 0.6f );
            nodeHandsAppart.RootOfGesture = menuSelectStub;

            nodeHandsTogether.NextAction = nodeHandsAppart;

            _Gestures.Add( menuSelectStub );

            //end of select menu

            //start rotateRight
            ActionNode rotateRightStub = new PositionAction( "rotateRightStub", "", PositionAction.INFRONT, JointType.ShoulderLeft, JointType.ShoulderRight, 0.1f );
            rotateRightStub.Active = true;

            ActionNode rotateRight = new PositionAction( "rotateRight", "RotateRight", PositionAction.INFRONT, JointType.HandLeft, JointType.HipCenter, 1.0f, 0.3f );
            rotateRight.RootOfGesture = rotateRightStub;

            rotateRightStub.NextAction = rotateRight;

            _Gestures.Add( rotateRightStub );

            //end of rotate right

            //start rotate left 
            ActionNode rotateLeftStub = new PositionAction( "rotateLeftStub", "", PositionAction.INFRONT, JointType.ShoulderLeft, JointType.ShoulderLeft, 0.1f );
            rotateLeftStub.Active = true;

            ActionNode rotateLeft = new PositionAction( "rotateLeft", "RotateLeft", PositionAction.INFRONT, JointType.HandRight, JointType.HipCenter, 1.0f, 0.3f );
            rotateLeft.RootOfGesture = rotateLeftStub;

            rotateLeftStub.NextAction = rotateLeft;

            _Gestures.Add( rotateLeftStub );

            // end rotate left

            //strafe left 
            ActionNode strafeLeftStub = new PositionAction( "strafeLeftStub", "", PositionAction.INFRONT, JointType.ShoulderLeft, JointType.ShoulderLeft, 0.1f );
            strafeLeftStub.Active = true;

            ActionNode strafeLeft = new PositionAction( "strafeLeft", "strafeLeft", PositionAction.ABOVE, JointType.HandRight, JointType.ShoulderRight, 1.0f );
            strafeLeft.RootOfGesture = strafeLeftStub;

            strafeLeftStub.NextAction = strafeLeft;

            _Gestures.Add( strafeLeftStub );

            //strafe Right 
            ActionNode strafeRightStub = new PositionAction( "strafeRightStub", "", PositionAction.INFRONT, JointType.ShoulderRight, JointType.ShoulderRight, 0.1f );
            strafeRightStub.Active = true;

            ActionNode strafeRight = new PositionAction( "strafeRight", "strafeRight", PositionAction.ABOVE, JointType.HandLeft, JointType.ShoulderLeft, 1.0f );
            strafeRight.RootOfGesture = strafeRightStub;

            strafeRightStub.NextAction = strafeRight;

            _Gestures.Add( strafeRightStub );

            //right hand fly
            ActionNode flyStub = new PositionAction( "flyStub", "", PositionAction.INFRONT, JointType.ShoulderLeft, JointType.ShoulderLeft, 0.1f );
            flyStub.Active = true;

            ActionNode handNearHipRightOf = new PositionAction( "handNearHipRightOf", "", PositionAction.RIGHTOF, JointType.HandRight, JointType.HipRight, 1.0f );
            handNearHipRightOf.RootOfGesture = flyStub;

            flyStub.NextAction = handNearHipRightOf;

            ActionNode handNearHipInfront = new PositionAction( "handNearHipInfront", "", PositionAction.INFRONT, JointType.HandRight, JointType.HipRight, 0.5f );
            handNearHipInfront.RootOfGesture = flyStub;

            handNearHipRightOf.NextAction = handNearHipInfront;

            ActionNode handNearHipBelow = new PositionAction( "handNearHipBelow", "fly", PositionAction.BELOW, JointType.HandRight, JointType.Spine, 1.0f, 0.2f );
            handNearHipBelow.RootOfGesture = flyStub;

            handNearHipInfront.NextAction = handNearHipBelow;

            _Gestures.Add( flyStub );
        }

        public void UpdateGestuers()
        {            
            RestActivations();
            Player1 = KinectManager.Instance.GetPlayer1Skeleton();
            
            if ( Player1 == null )
            {
                return;
            }
            
            //update nodes
            foreach ( ActionNode node in _Gestures )
            {
                node.Proceed();
            }
            
            CheckForInactiveMovementGestures();

        }

        private bool _RotateRightActivated = false;
        private bool _RotateLeftActivated = false;
        private bool _StrafeLeftActivated = false;
        private bool _StrafeRightActivated = false;
        private bool _FlyActivated = false;

        private int _GestureConcurency = 1;
        private int _RotateRightConcurency = 0;
        private int _RotateLeftConcurency = 0;
        private int _StrafeLeftConcurency = 0;
        private int _StrafeRightConcurency = 0;
        private int _FlyConcurency = 0;

        private void RestActivations()
        {
            if ( _RotateRightConcurency > _GestureConcurency )
            {
                _RotateRightActivated = false;
                _RotateRightConcurency = 0;
            }
            else
            {
                _RotateRightConcurency++;
            }

            if ( _RotateLeftConcurency > _GestureConcurency )
            {
                _RotateLeftActivated = false;
                _RotateLeftConcurency = 0;
            }
            else
            {
                _RotateLeftConcurency++;
            }

            if ( _StrafeLeftConcurency > _GestureConcurency)
            {
                _StrafeLeftActivated = false;
                _StrafeLeftConcurency = 0;
            }
            else
            {
                _StrafeLeftConcurency++;
            }

            if ( _StrafeRightConcurency > _GestureConcurency )
            {
                _StrafeRightActivated = false;
                _StrafeRightConcurency = 0;
            }
            else
            {
                _StrafeRightConcurency++;
            }
            
            if ( _FlyConcurency > _GestureConcurency )
            {
                _FlyActivated = false;
                _FlyConcurency = 0;
            }
            else
            {
                _FlyConcurency++;
            }
        }

        public void GestureEvent( string eventID )
        {
            if ( SkyView.Instance.CurrentGameState != GameStates.GAMEPLAY )
            {
                if ( eventID == "MenuItemDown" )
                {
                    SkyView.Instance.CurrentMenueSystem.ActiveMenu.MenuItemDown();
                }
                else if ( eventID == "MenuItemUp" )
                {
                    SkyView.Instance.CurrentMenueSystem.ActiveMenu.MenuItemUp();
                }
                else if ( eventID == "MenuItemSelect" )
                {
                    SkyView.Instance.CurrentMenueSystem.ActiveMenu.MenuItemSelect();
                }
            }
            else
            {
                if ( eventID == "RotateRight" )
                {
                    SkyView.Instance.RotateRight = true;
                    _RotateRightActivated = true;
                    _RotateRightConcurency = 0;
                }

                if ( eventID == "RotateLeft" )
                {
                    SkyView.Instance.RotateLeft = true;
                    _RotateLeftActivated = true;
                    _RotateLeftConcurency = 0;
                }

                if ( eventID == "strafeRight" )
                {
                    SkyView.Instance.StrafeRight = true;
                    _StrafeRightActivated = true;
                    _StrafeRightConcurency = 0;
                }

                if ( eventID == "strafeLeft" )
                {
                    SkyView.Instance.StrafeLeft = true;
                    _StrafeLeftActivated = true;
                    _StrafeLeftConcurency = 0;
                }

                if ( eventID == "fly" )
                {
                    SkyView.Instance.Fly = true;
                    _FlyActivated = true;
                    _FlyConcurency = 0;
                }
            }
        }

        public void ResetGestuer( string gestureID )
        {
            //update nodes
            foreach ( ActionNode node in _Gestures )
            {
                if ( node.ID == gestureID )
                {
                    node.Active = true;
                }
            }
        }

        private void CheckForInactiveMovementGestures()
        {
            if ( !_RotateRightActivated )
            {
                SkyView.Instance.RotateRight = false;
            }

            if ( !_RotateLeftActivated )
            {
                SkyView.Instance.RotateLeft = false;
            }

            if ( !_StrafeRightActivated )
            {
                SkyView.Instance.StrafeRight = false;
            }

            if ( !_StrafeLeftActivated )
            {
                SkyView.Instance.StrafeLeft = false;
            }

            if ( !_FlyActivated )
            {
                SkyView.Instance.Fly= false;
            }
        }
    }
}
