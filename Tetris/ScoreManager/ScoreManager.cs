using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// SCORE MANAGER ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

// manages the score system of the game

// use static calls to update score 

// employs the SINGLETON PATTERN (http://csharpindepth.com/Articles/General/Singleton.aspx)
// to ensure that no duplicates of this class are made


namespace Tetris
{
    public class ScoreManager
    {
        // Data ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private static ScoreManager pInstance = null;

        private GameStats stats;
        public int score;
        public int levelCount = 1;

        // Setting up Singleton Pattern ------------------------------------------------------------------------------------------------------------------------------------------

        public static void CreateScoreManager()
        {
            if (pInstance == null)
            {
                pInstance = new ScoreManager();
            }
        }

        private static ScoreManager privGetInstance()
        {
            return pInstance;
        }

        protected ScoreManager()
        {
            this.stats = new GameStats();

            // create font 
            GlyphMan.AddXml("Consolas20pt.xml", new Azul.Texture("consolas20pt.tga"));
        }

        ~ScoreManager()
        {

        }
        public enum levelState
        {
            level1,
            level2,
            level3,
            level4,
            level5,
            level6,
            level7,
            level8,
            level9,
            level10
        }

        public void changeLevel()
        {
            int lineCount = privGetInstance().stats.getLineCount();

            if (lineCount > 10 && this.stats.getLevelNum() < 2)
            {
                // bump to level 2
                privGetInstance().stats.IncreaseLevel();
            }
            else if (lineCount > 20 && this.stats.getLevelNum() < 3)
            {
                // bump to level 3
                privGetInstance().stats.IncreaseLevel();
            }
            else if (lineCount > 30 && this.stats.getLevelNum() < 4)
            {
                // bump to level 4
                privGetInstance().stats.IncreaseLevel();
            }
            else if (lineCount > 40 && this.stats.getLevelNum() < 5)
            {
                // bump to level 5
                privGetInstance().stats.IncreaseLevel();
            }
            else if (lineCount > 50 && this.stats.getLevelNum() < 6)
            {
                // bump to level 6
                privGetInstance().stats.IncreaseLevel();
            }
            else if (lineCount > 60 && this.stats.getLevelNum() < 7)
            {
                // bump to level 7
                privGetInstance().stats.IncreaseLevel();
            }
            else if (lineCount > 70 && this.stats.getLevelNum() < 8)
            {
                // bump to level 8
                privGetInstance().stats.IncreaseLevel();
            }
            else if (lineCount > 80 && this.stats.getLevelNum() < 9)
            {
                // bump to level 9
                privGetInstance().stats.IncreaseLevel();
            }
            else if (lineCount > 90 && this.stats.getLevelNum() < 10)
            {
                // bump to level 10
                privGetInstance().stats.IncreaseLevel();
            }
        }

        // Add To Score ------------------------------------------------------------------------------------------------------------------------------------------
        public int getScore(int score)
        {
            this.score = score;
            return score;
        }

        public static void FilledRow(int numFilledRows)
        {
            // increase line count
            privGetInstance().stats.IncreaseLineCount(numFilledRows);

            // increase score
            privGetInstance().stats.IncreaseScore(numFilledRows);

            pInstance.changeLevel();


        }

        // Clear  Score ------------------------------------------------------------------------------------------------------------------------------------------

        public static void ClearScore()
        {
            privGetInstance().stats.ClearStats();
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

        }

        // Draw ------------------------------------------------------------------------------------------------------------------------------------------

        public static void Draw(GameManager.GameState stateIn)
        {
            switch (stateIn)
            {
                case GameManager.GameState.WELCOME:
                    privGetInstance().WelcomeDraw();
                    break;
                case GameManager.GameState.PLAY:
                    privGetInstance().PlayDraw();
                    break;
                case GameManager.GameState.GAMEOVER:
                    privGetInstance().GameOverDraw();
                    break;
            }
        }

        private void WelcomeDraw()
        {
            SOM.drawBackground();

            SOM.drawWelcomeScreen();

        }

        private void PlayDraw()
        {
            // first draw background
            SOM.drawBackground();

            // then draw the stats
            SOM.drawStrings(privGetInstance().stats);
        }

        private void GameOverDraw()
        {
            SOM.drawBackground();
            SOM.drawGameOverScreen(privGetInstance().stats);
        }
    }

}