using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

// SOUND MANAGER ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

// manages the sound system

// employs the SINGLETON PATTERN (http://csharpindepth.com/Articles/General/Singleton.aspx)
// to ensure that no duplicates of this class are made


namespace Tetris
{
    class SoundManager
    {
        // Data ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        private static SoundManager pInstance = null;
        private static IrrKlang.ISoundEngine sfxEngine = new IrrKlang.ISoundEngine();

        private static IrrKlang.ISound BGM = null;        
        private static IrrKlang.ISound sfx1 = null;
        private static bool bgmOff;
        // Setting up Singleton Pattern ------------------------------------------------------------------------------------------------------------------------------------------

        public static void CreateSoundManager()
        {
            if (pInstance == null)
            {
                // create instance
                pInstance = new SoundManager();

                //Default have sound
                //BGM = sfxEngine.Play2D("theme.wav", true);
                bgmOff = true;
                
            }
        }

        private static SoundManager privGetInstance()
        {
            return pInstance;
        }

        protected SoundManager()
        {
            
        }

        ~SoundManager()
        {

        }
        

        // Turning Music On/Off ------------------------------------------------------------------------------------------------------------------------------------------

        public static void MusicOnOff()
        {
            if (bgmOff)
            {
               // Console.WriteLine("Music On");
                BGM = sfxEngine.Play2D("theme.wav", true);
                bgmOff = false;
            }
            else {
               // Console.WriteLine("Music Off");
                BGM.Stop();
                bgmOff = true;
            }
        }

        // In-Game Sounds ------------------------------------------------------------------------------------------------------------------------------------------

        public static void FilledRow()
        {
            if (!bgmOff)
            {
                sfx1 = sfxEngine.Play2D("shoot.wav", false);
            }
        }

        public static void PieceTurn()
        {
            if (!bgmOff)
            {
                sfx1 = sfxEngine.Play2D("click.wav", false);
            }
        }

        public static void GameOver()
        {
            if (!bgmOff)
            {
                sfx1 = sfxEngine.Play2D("Game_Over.wav", false); //Plays final noise
            }

        }

        // Update ------------------------------------------------------------------------------------------------------------------------------------------

        public static void Update(GameManager.GameState stateIn)
        {
            switch (stateIn)
            {
                case GameManager.GameState.WELCOME:
                    privGetInstance().WelcomeUpdate();
                    break;
                case GameManager.GameState.PLAY:
                    privGetInstance().PlayUpdate();
                    break;
                case GameManager.GameState.GAMEOVER:
                    privGetInstance().GameOverUpdate();
                    break;
            }

        }

        private void WelcomeUpdate()
        {

        }

        private void PlayUpdate()
        {

        }

        private void GameOverUpdate()
        {
            MusicOnOff();  //Turns off theme 
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------


    }

}
