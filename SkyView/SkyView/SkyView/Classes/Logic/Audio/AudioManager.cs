using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace SkyView.Classes.Logic.Audio
{
    public class AudioManager
    {
        private AudioEngine _AudioEngine;
        private WaveBank _WaveBank;
        private SoundBank _SoundBank;
        private ContentManager _ContentManager = null;

        private Cue _RepeatingCue = null;
        private Cue _RepeatingCue2 = null;
        private Cue _SingleCue = null;
        private string _RepeatingCueName = string.Empty;
        private string _RepeatingCueName2 = string.Empty;

        private AudioCategory _AudioCategoryMusic;
        private AudioCategory _AudioCategoryDefault;

        public AudioManager()
        {
            
        }

        public void SetVolumne( float volLevel )
        {
            volLevel = MathHelper.Clamp( volLevel, 0.0f, 2.0f );
            _AudioCategoryMusic.SetVolume( volLevel / 2 );

            _AudioCategoryDefault.SetVolume( volLevel );
        }

        public void LoadContent( ContentManager content )
        {
            _ContentManager = content;

            _AudioEngine = new AudioEngine( _ContentManager.RootDirectory + "\\Audio\\skyviewaudio.xgs" );
            _WaveBank = new WaveBank( _AudioEngine, _ContentManager.RootDirectory + "\\Audio\\Wave Bank.xwb" );
            _SoundBank = new SoundBank( _AudioEngine, _ContentManager.RootDirectory +  "\\Audio\\Sound Bank.xsb" );

            _AudioCategoryDefault = _AudioEngine.GetCategory( "Default" );
            _AudioCategoryMusic = _AudioEngine.GetCategory( "Music" );

            SetVolumne( 2.0f );
        }

        public void Update( GameTime gameTime )
        {
            if ( _RepeatingCue != null && _RepeatingCue.IsStopped )
            {
                _RepeatingCue = _SoundBank.GetCue( _RepeatingCueName );
                _RepeatingCue.Play();
            }

            if ( _RepeatingCue2 != null && _RepeatingCue2.IsStopped )
            {
                _RepeatingCue2 = _SoundBank.GetCue( _RepeatingCueName2 );
                _RepeatingCue2.Play();
            }
                
            _AudioEngine.Update();
        }

        public void SetRepeatingCue( string cueName )
        {
            _RepeatingCueName = cueName;
            
            if ( _RepeatingCue!= null )
            {
                _RepeatingCue.Stop( AudioStopOptions.Immediate );
            }
            
            _RepeatingCue = _SoundBank.GetCue( cueName );
            _RepeatingCue.Play();
        }

        public void StopRepeatingCues()
        {
            if ( _RepeatingCue != null )
            {
                _RepeatingCue.Stop( AudioStopOptions.Immediate );
               // _RepeatingCue = null;
            }

            if ( _RepeatingCue2 != null )
            {
                _RepeatingCue2.Stop( AudioStopOptions.Immediate );
                _RepeatingCue2 = null;
            }                       
        }

        public void SetRepeatingCue2( string cueName )
        {
            _RepeatingCueName2 = cueName;

            if ( _RepeatingCue2 != null )
            {
                _RepeatingCue2.Stop( AudioStopOptions.Immediate );
            }

            _RepeatingCue2 = _SoundBank.GetCue( cueName );
            _RepeatingCue2.Play();
        }

        public void SetSingleCue( string cueName )
        {
            _SingleCue = _SoundBank.GetCue( cueName );
            _SingleCue.Play();
        }
    }
}
