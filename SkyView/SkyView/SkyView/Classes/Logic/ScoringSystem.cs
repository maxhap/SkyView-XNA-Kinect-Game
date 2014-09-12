using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SkyView.Classes.Logic
{
    public class ScoringSystem
    {
        public static string[] LoadScoars()
        {
            string[] lines = new string[2];

            StreamReader streamReader = null;
            
            try
            {
                if ( !Directory.Exists( "Content\\TextFiles" ) )
                {
                    Directory.CreateDirectory( "Content\\TextFiles" );
                }                

                if ( !File.Exists( "Content\\TextFiles\\scoreboard.txt" ) )
                {
                    File.Create( "Content\\TextFiles\\scoreboard.txt" );
                    SaveScore( 0, 0 );
                    SaveScore( 0, 1 );

                }

                streamReader = new StreamReader( "Content\\TextFiles\\scoreboard.txt" );    

                string line;
                int counter = 0;
                
                while( ( line = streamReader.ReadLine() ) != null )
                {
                    lines[counter] = line;
                    counter++;
                }
            }
            catch ( Exception ex )
            {
            	
            }

            streamReader.Close();

            return lines;
        }

        public static void SaveScore( int score, int line )
        {
            string[] lines = File.ReadAllLines( "Content\\TextFiles\\scoreboard.txt" );
            lines[line] = score.ToString();

            StreamWriter writer = new StreamWriter( "Content\\TextFiles\\scoreboard.txt", false );


            foreach ( string l in lines )
            {
                writer.WriteLine( l );
            }

            writer.Close();

        }
    }
}
