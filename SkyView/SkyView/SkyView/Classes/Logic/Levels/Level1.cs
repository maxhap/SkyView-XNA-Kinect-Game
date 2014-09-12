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
    public class Level1 : Level
    {
        public Level1( int levelDifficulty ) : base( levelDifficulty )
        {            
            HeightMap = "HeightMaps\\level1heightmap";
            LevelMusic = "Zelda Skyward Sword Music   Bamboo Cutting";
            LevelMap = "LevelMiniMaps\\level1levelmap";
            LevelSkydome = new SkyDome();
            _v3PlayerStartingPossition = new Vector3( 70, 50, -50 );
            EndingPoint = new Vector3( 440, 50, -378 );
            HeightmapSize = 512.0f;
        }

        protected override void CreateRotationPoints()
        {
            //RotationDistances = new List<float>();
            //RotationAngles = new List<Vector3>();

            //RotationDistances.Add( 450 );
            //RotationAngles.Add( new Vector3( 0, -1.5f, 0 ) );

            //RotationDistances.Add( 150 );
            //RotationAngles.Add( new Vector3( 0, -3.0f, 0 ) );

            //RotationDistances.Add( 420 );
            //RotationAngles.Add( new Vector3( 0, -1.5f, 0 ) );
            //RotationDistances.Add( 200 );
            //RotationAngles.Add( new Vector3( 0, 6.2f, 0 ) );
        }

        protected override void CreateHoops()
        {
            if ( LevelDifficutly == 1 )
            {
                Hoop hoop1 = new Hoop( 30 );
                hoop1.Position = new Vector3( 61, 20, -130 );

                _LevelObjects.Add( hoop1 );

                Hoop hoop3 = new Hoop( 20 );
                hoop3.Position = new Vector3( 248, 60, -386 );

                _LevelObjects.Add( hoop3 );

                Hoop hoop4 = new Hoop( 20 );
                hoop4.Position = new Vector3( 61, 60, -280 );

                _LevelObjects.Add( hoop4 );

                Hoop hoop5 = new Hoop( 20 );
                hoop5.Position = new Vector3( 293, 80, -200 );

                _LevelObjects.Add( hoop5 );

                Hoop hoop6 = new Hoop( 20 );
                hoop6.Position = new Vector3( 445, 60, -260 );

                _LevelObjects.Add( hoop6 );

                

            }
            else if ( LevelDifficutly == 2 )
            {
                Hoop hoop1 = new Hoop( 30 );
                hoop1.Position = new Vector3( 61, 20, -130 );

                _LevelObjects.Add( hoop1 );

                Hoop hoop3 = new Hoop( 20 );
                hoop3.Position = new Vector3( 248, 60, -386 );

                _LevelObjects.Add( hoop3 );

                Hoop hoop4 = new Hoop( 20 );
                hoop4.Position = new Vector3( 61, 60, -280 );

                _LevelObjects.Add( hoop4 );

                Hoop hoop5 = new Hoop( 20 );
                hoop5.Position = new Vector3( 293, 80, -200 );

                _LevelObjects.Add( hoop5 );

                Hoop hoop6 = new Hoop( 20 );
                hoop6.Position = new Vector3( 445, 60, -260 );

                _LevelObjects.Add( hoop6 );

                Hoop hoop7 = new Hoop( 20 );
                hoop7.Position = new Vector3( 230, 60, -285 );

                _LevelObjects.Add( hoop7 );

                Hoop hoop8 = new Hoop( 20 );
                hoop8.Position = new Vector3( 223, 50, -61 );

                _LevelObjects.Add( hoop8 ); 
            }
            else if ( LevelDifficutly == 3 )
            {
                Hoop hoop1 = new Hoop( 30 );
                hoop1.Position = new Vector3( 61, 20, -130 );

                _LevelObjects.Add( hoop1 );

                Hoop hoop3 = new Hoop( 20 );
                hoop3.Position = new Vector3( 248, 60, -386 );

                _LevelObjects.Add( hoop3 );

                Hoop hoop4 = new Hoop( 20 );
                hoop4.Position = new Vector3( 61, 60, -280 );

                _LevelObjects.Add( hoop4 );

                Hoop hoop4b = new Hoop( 20 );
                hoop4b.Position = new Vector3( 68, 40, -180 );

                _LevelObjects.Add( hoop4b );

                Hoop hoop5 = new Hoop( 20 );
                hoop5.Position = new Vector3( 293, 80, -200 );

                _LevelObjects.Add( hoop5 );

                Hoop hoop6 = new Hoop( 20 );
                hoop6.Position = new Vector3( 445, 60, -260 );

                _LevelObjects.Add( hoop6 );

                Hoop hoop7 = new Hoop( 20 );
                hoop7.Position = new Vector3( 230, 60, -285 );

                _LevelObjects.Add( hoop7 );

                Hoop hoop8 = new Hoop( 20 );
                hoop8.Position = new Vector3( 223, 50, -61 );

                _LevelObjects.Add( hoop8 );

                Hoop hoop9 = new Hoop( 20 );
                hoop9.Position = new Vector3( 76, 50, -458 );

                _LevelObjects.Add( hoop9 );

                Hoop hoop10 = new Hoop( 20 );
                hoop10.Position = new Vector3( 435, 40, -80 );

                _LevelObjects.Add( hoop10 );
            }

            Block endingBlock = new Block();
            endingBlock.Position = new Vector3( 440, 50, -378 );
            _LevelObjects.Add( endingBlock );

        }

        protected override void CreateCrosses()
        {
            Cross star1 = new Cross();
            star1.Position =  new Vector3( 161, 70, -440 );
            star1.Rotation = new Vector3( 0.0f, 0.0f, -1.5f );

            _LevelObjects.Add( star1 );

            Cross star2 = new Cross();
            star2.Position = new Vector3( 195, 75, -115 );
            star2.Rotation = new Vector3( 0.0f, 0.0f, -1.5f );

            _LevelObjects.Add( star2 );
        }

        protected override void CreateButterflys()
        {
            if ( LevelDifficutly == 1 )
            {
                Butterfly butterfly1 = new Butterfly();
                butterfly1.Position = new Vector3( 61, 70, -430 );
                butterfly1.Rotation = new Vector3( 0.0f, -3.0f, 0.0f );
                _LevelObjects.Add( butterfly1 );
            }

            if ( LevelDifficutly == 2 )
            {
                Butterfly butterfly1 = new Butterfly();
                butterfly1.Position = new Vector3( 61, 70, -430 );
                butterfly1.Rotation = new Vector3( 0.0f, -3.0f, 0.0f );
                _LevelObjects.Add( butterfly1 );

                Butterfly butterfly2 = new Butterfly();
                butterfly2.Position = new Vector3( 45, 50, -330 );
                butterfly2.Rotation = new Vector3( 0.0f, -3.0f, 0.0f );
                _LevelObjects.Add( butterfly2 );

                Butterfly butterfly3 = new Butterfly();
                butterfly3.Position = new Vector3( 254, 100, -290 );
                butterfly3.Rotation = new Vector3( 0.0f, -5.6f, 0.0f );
                _LevelObjects.Add( butterfly3 );

                Butterfly butterfly4 = new Butterfly();
                butterfly4.Position = new Vector3( 248, 80, -120 );
                butterfly4.Rotation = new Vector3( 0.0f, -0.02f, 0.0f );
                _LevelObjects.Add( butterfly4 );

                Butterfly butterfly5 = new Butterfly();
                butterfly5.Position = new Vector3( 440, 40, -320 );
                butterfly5.Rotation = new Vector3( 0.0f, 3, 0.0f );
                _LevelObjects.Add( butterfly5 );                

            }

            if ( LevelDifficutly == 3 )
            {
                Butterfly butterfly1 = new Butterfly();
                butterfly1.Position = new Vector3( 61, 70, -430 );
                butterfly1.Rotation = new Vector3( 0.0f, -3.0f, 0.0f );
                _LevelObjects.Add( butterfly1 );

                Butterfly butterfly2 = new Butterfly();
                butterfly2.Position = new Vector3( 45, 50, -330 );
                butterfly2.Rotation = new Vector3( 0.0f, -3.0f, 0.0f );
                _LevelObjects.Add( butterfly2 );

                Butterfly butterfly2b = new Butterfly();
                butterfly2b.Position = new Vector3( 80, 40, -330 );
                butterfly2b.Rotation = new Vector3( 0.0f, -3.0f, 0.0f );
                _LevelObjects.Add( butterfly2b );

                Butterfly butterfly3 = new Butterfly();
                butterfly3.Position = new Vector3( 254, 100, -290 );
                butterfly3.Rotation = new Vector3( 0.0f, -5.6f, 0.0f );
                _LevelObjects.Add( butterfly3 );

                Butterfly butterfly4 = new Butterfly();
                butterfly4.Position = new Vector3( 248, 80, -120 );
                butterfly4.Rotation = new Vector3( 0.0f, -0.02f, 0.0f );
                _LevelObjects.Add( butterfly4 );

                Butterfly butterfly5 = new Butterfly();
                butterfly5.Position = new Vector3( 440, 40, -320 );
                butterfly5.Rotation = new Vector3( 0.0f, 3, 0.0f );
                _LevelObjects.Add( butterfly5 );  
            }
            
        }

        protected override void AddTrees()
        {
            Tree tree = new Tree();
            tree.Position = new Vector3( 24, 20, -100 );
            //tree.Rotation = new Vector3( 1.5f, 0.0f, 0.0f );

            _LevelObjects.Add( tree );

            tree = new Tree();
            tree.Position = new Vector3( 102, 20, -200 );
            _LevelObjects.Add( tree );

            tree = new Tree();
            tree.Position = new Vector3( 35, 20, -341 );
            _LevelObjects.Add( tree );

            tree = new Tree();
            tree.Position = new Vector3( 241, 20, -475 );
            _LevelObjects.Add( tree );

            tree = new Tree();
            tree.Position = new Vector3( 287, 75, -444 );
            _LevelObjects.Add( tree );

            tree = new Tree();
            tree.Position = new Vector3( 250, 20, -252 );
            _LevelObjects.Add( tree );

            tree = new Tree();
            tree.Position = new Vector3( 217, 20, -113 );
            _LevelObjects.Add( tree );

            tree = new Tree();
            tree.Position = new Vector3( 407, 20, -147 );
            _LevelObjects.Add( tree );
        }

        public override bool PrecessHiScore( int score )
        {
            int OldScore = int.Parse( ScoringSystem.LoadScoars()[0] );

            if ( score > OldScore )
            {
                ScoringSystem.SaveScore( score, 0 );
                return true;
            }

            return false;
        }
    }

}
