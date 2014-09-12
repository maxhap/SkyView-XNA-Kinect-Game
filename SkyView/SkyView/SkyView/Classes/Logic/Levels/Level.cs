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
using SkyView.Classes.Objects;
using SkyView.Classes.Objects.GameObjects;
using SkyView.Classes.Objects.GameObjects.Animated;
using SkyView.Classes.Objects.GameObjects.NoneAnimated;

namespace SkyView.Classes.Logic.Levels
{
    public class Level
    {
        protected Vector3 _v3PlayerStartingPossition = Vector3.Zero;
        protected List<GameObject> _LevelObjects = new List<GameObject>();
        protected float _CharacterSpeed = 1.0f;        

        public SkyDome LevelSkydome { get; protected set; }
        public float TotalDistanceTilLevelEnds { get; protected set; }

        public int LevelDifficutly { get; protected set; }
        public int LevelLives { get; protected set; }

        public string LevelMap { get; protected set; }
        public string HeightMap { get; protected set; }
        public string LevelMusic { get; protected set; }

        public Vector3 EndingPoint { get; protected set; }

        public float HeightmapSize { get; protected set; }

        protected Level( int levelDifficulty )
        {
            LevelDifficutly = levelDifficulty;
            LevelLives = 3;
            CreateHoops();
            CreateSkyBox();
            CreateCrosses();
            CreateRotationPoints();
            CreateButterflys();
            AddTrees();
        }

        public List<GameObject> LevelObjects 
        {
            get{ return _LevelObjects; }
        }

        public float CharacterSpeed
        {
            get { return _CharacterSpeed; }
        }

        public void LoadContent( GraphicsDevice device, ContentManager content )
        {           
            foreach( GameObject gameObject in _LevelObjects )
            {
                gameObject.LoadContent( device, content );
            }

            LevelSkydome.LoadContent( device, content );
        }

        public Vector3 StartingPossition
        {
            get
            {
                return _v3PlayerStartingPossition;
            }
        }

        protected virtual void AddTrees()
        {

        }

        protected virtual void CreateHoops()
        {

        }

        protected virtual void CreateSkyBox()
        {

        }

        protected virtual void CreateCrosses()
        {

        }

        protected virtual void CreateRotationPoints()
        {

        }

        protected virtual void CreateButterflys()
        {
        
        }

        public virtual bool PrecessHiScore( int score )
        {
            return false;
        }
    }
}
