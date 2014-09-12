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
    class Level2 : Level
    {
        public Level2( int levelDifficulty ) : base( levelDifficulty )
        {            
            HeightMap = "HeightMaps\\level2heightmap";
            LevelMusic = "Kokiri Forest";
            LevelMap = "LevelMiniMaps\\level2levelmap";
            LevelSkydome = new SkyDome();
            _v3PlayerStartingPossition = new Vector3( 70, 50, -50 );
            EndingPoint = new Vector3( 467, 60, -460 );
            HeightmapSize = 512.0f;
        }

        protected override void CreateRotationPoints()
        {
        }

        protected override void CreateHoops()
        {
            if ( LevelDifficutly == 1 )
            {
                Hoop hoop1 = new Hoop( 30 );
                hoop1.Position = new Vector3( 90, 50, -145 );

                _LevelObjects.Add( hoop1 );

                Hoop hoop3 = new Hoop( 20 );
                hoop3.Position = new Vector3( 136, 60, -285 );

                _LevelObjects.Add( hoop3 );

                Hoop hoop4 = new Hoop( 20 );
                hoop4.Position = new Vector3( 84, 40, -379 );

                _LevelObjects.Add( hoop4 );

                Hoop hoop5 = new Hoop( 20 );
                hoop5.Position = new Vector3( 2154, 30, -400 );

                _LevelObjects.Add( hoop5 );

                Hoop hoop6 = new Hoop( 20 );
                hoop6.Position = new Vector3( 411, 60, -188 );

                _LevelObjects.Add( hoop6 );

                Hoop hoop7 = new Hoop( 20 );
                hoop7.Position = new Vector3( 431, 40, -88 );

                _LevelObjects.Add( hoop7 );                

            }
            else if ( LevelDifficutly == 2 )
            {
                Hoop hoop1 = new Hoop( 30 );
                hoop1.Position = new Vector3( 90, 50, -145 );

                _LevelObjects.Add( hoop1 );

                Hoop hoop3 = new Hoop( 20 );
                hoop3.Position = new Vector3( 136, 60, -285 );

                _LevelObjects.Add( hoop3 );

                Hoop hoop4 = new Hoop( 20 );
                hoop4.Position = new Vector3( 84, 40, -379 );

                _LevelObjects.Add( hoop4 );

                Hoop hoop5 = new Hoop( 20 );
                hoop5.Position = new Vector3( 2154, 30, -400 );

                _LevelObjects.Add( hoop5 );

                Hoop hoop6 = new Hoop( 20 );
                hoop6.Position = new Vector3( 411, 60, -188 );

                _LevelObjects.Add( hoop6 );

                Hoop hoop7 = new Hoop( 20 );
                hoop7.Position = new Vector3( 431, 40, -88 );

                _LevelObjects.Add( hoop7 );
            }
            else if ( LevelDifficutly == 3 )
            {
                Hoop hoop1 = new Hoop( 30 );
                hoop1.Position = new Vector3( 90, 50, -145 );

                _LevelObjects.Add( hoop1 );

                Hoop hoop3 = new Hoop( 20 );
                hoop3.Position = new Vector3( 136, 60, -285 );

                _LevelObjects.Add( hoop3 );

                Hoop hoop4 = new Hoop( 20 );
                hoop4.Position = new Vector3( 84, 40, -379 );

                _LevelObjects.Add( hoop4 );

                Hoop hoop5 = new Hoop( 20 );
                hoop5.Position = new Vector3( 2154, 30, -400 );

                _LevelObjects.Add( hoop5 );

                Hoop hoop6 = new Hoop( 20 );
                hoop6.Position = new Vector3( 411, 60, -188 );

                _LevelObjects.Add( hoop6 );

                Hoop hoop7 = new Hoop( 20 );
                hoop7.Position = new Vector3( 431, 40, -88 );

                _LevelObjects.Add( hoop7 );   
            }

            Block endingBlock = new Block();
            endingBlock.Position = new Vector3( 467, 60, -460 );
            _LevelObjects.Add( endingBlock );

        }

        protected override void CreateCrosses()
        {
            Cross star1 = new Cross();
            star1.Position =  new Vector3( 285, 50, -298 );
            star1.Rotation = new Vector3( 0.0f, 0.0f, -1.5f );

            _LevelObjects.Add( star1 );

            Cross star2 = new Cross();
            star2.Position = new Vector3( 227, 61, -227 );
            star2.Rotation = new Vector3( 0.0f, 0.0f, -1.5f );

            _LevelObjects.Add( star2 );
        }

        protected override void CreateButterflys()
        {
            if ( LevelDifficutly == 1 )
            {
                Butterfly butterfly1 = new Butterfly();
                butterfly1.Position = new Vector3( 362, 50, -304 );
                butterfly1.Rotation = new Vector3( 0.0f, -10.0f, 0.0f );
                _LevelObjects.Add( butterfly1 );
            }

            if ( LevelDifficutly == 2 )
            {
                Butterfly butterfly1 = new Butterfly();
                butterfly1.Position = new Vector3( 362, 50, -304 );
                butterfly1.Rotation = new Vector3( 0.0f, -10.0f, 0.0f );
                _LevelObjects.Add( butterfly1 );

                Butterfly butterfly2 = new Butterfly();
                butterfly2.Position = new Vector3( 235, 30, -358 );
                butterfly2.Rotation = new Vector3( 0.0f, -10.0f, 0.0f );
                _LevelObjects.Add( butterfly2 );

                Butterfly butterfly3 = new Butterfly();
                butterfly3.Position = new Vector3( 412, 30, -271 );
                butterfly3.Rotation = new Vector3( 0.0f, -10.0f, 0.0f );
                _LevelObjects.Add( butterfly3 );

            }

            if ( LevelDifficutly == 3 )
            {
                Butterfly butterfly1 = new Butterfly();
                butterfly1.Position = new Vector3( 362, 50, -304 );
                butterfly1.Rotation = new Vector3( 0.0f, -10.0f, 0.0f );
                _LevelObjects.Add( butterfly1 );

                Butterfly butterfly2 = new Butterfly();
                butterfly2.Position = new Vector3( 235, 30, -358 );
                butterfly2.Rotation = new Vector3( 0.0f, -10.0f, 0.0f );
                _LevelObjects.Add( butterfly2 );

                Butterfly butterfly3 = new Butterfly();
                butterfly3.Position = new Vector3( 412, 30, -271 );
                butterfly3.Rotation = new Vector3( 0.0f, -10.0f, 0.0f );
                _LevelObjects.Add( butterfly3 );

                Butterfly butterfly4 = new Butterfly();
                butterfly4.Position = new Vector3( 454, 40, -94 );
                butterfly4.Rotation = new Vector3( 0.0f, -8.2f, 0.0f );
                _LevelObjects.Add( butterfly4 );

                Butterfly butterfly5 = new Butterfly();
                butterfly5.Position = new Vector3( 412, 30, -271 );
                butterfly5.Rotation = new Vector3( 0.0f, -3.7f, 0.0f );
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
            int OldScore = int.Parse( ScoringSystem.LoadScoars()[1] );

            if ( score > OldScore )
            {
                ScoringSystem.SaveScore( score, 1 );
                return true;
            }

            return false;
        }
    }
}
