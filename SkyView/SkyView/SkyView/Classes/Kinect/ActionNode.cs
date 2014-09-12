using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace SkyView.Classes.Kinect
{
    class ActionNode
    {
        public ActionNode RootOfGesture { get; set; }
        public ActionNode NextAction { get; set; }
        public string EventKey { get; protected set; }
        public bool Active { get; set;  }
        public string ID { get; set; }

        protected int _Relationship;
        protected JointType _BodyPart1;
        protected JointType _BodyPart2;
        protected float _Distance;


        protected ActionNode( string id, string eventKey, int relationship, JointType bodyPart1, JointType bodyPart2, float distance )
        {
            EventKey = eventKey;
            Active = false;
            _Relationship = relationship;
            _BodyPart1 = bodyPart1;
            _BodyPart2 = bodyPart2;
            _Distance = distance;
            ID = id;
        }

        public virtual bool Acept()
        {
            return false;
        }

        public virtual void Proceed()
        {
            if( Active )
            {
                if ( NextAction != null )
                {
                    if ( NextAction.Acept() )
                    {
                        this.Active = false;                    
                    }
                }
                else
                {
                    this.Active = false;
                    KinectGestures.Instance.ResetGestuer( RootOfGesture.ID );
                }
            }
            else
            {
                if ( NextAction != null )
                {
                    NextAction.Proceed();
                }
                else
                {                    
                    this.Active = false;
                    KinectGestures.Instance.ResetGestuer( RootOfGesture.ID );
                }
                
            }            
        }
        
 
    }
}
