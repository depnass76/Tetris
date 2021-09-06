using System;

namespace Tetris
{
    class GameStats
    {
        // data
        private int levelNum;
        private int lineCount;
        private int gameScore;

        public GameStats()
        {
            this.levelNum = 1;
            this.lineCount = 0;
            this.gameScore = 0;
        }

        public void ClearStats()
        {
            this.levelNum = 1;
            this.lineCount = 0;
            this.gameScore = 0;

        }

        public int getLevelNum()
        {
            return this.levelNum;
        }

        public void setLevelNum(int level)
        {
            this.levelNum = level;
        }

        public int getLineCount()
        {
            return this.lineCount;
        }

        public void setLineCount(int line)
        {
            this.lineCount = line;
        }

        public int getScore()
        {
            return this.gameScore;
        }

        public void setScore(int score)
        {
            this.gameScore = score;
        }

        public void IncreaseLevel()
        {
            this.levelNum++;
        }

        public void IncreaseLineCount(int numFilledRows)
        {
            this.lineCount+= numFilledRows;
        }

        public void IncreaseScore(int numFilledRows)
        {
            // see game specifications doc! 
            // https://docs.google.com/document/d/1UIIebtX2XOy0ykVot1NJOOqVm9uoc5icvwj657-e4vo/edit

            switch (numFilledRows)
            {
                case 0:
                    break;
                case 1:
                    this.gameScore += (40 * this.levelNum);
                    break;
                case 2:
                    this.gameScore += (100 * this.levelNum);
                    break;
                case 3:
                    this.gameScore += (300 * this.levelNum);
                    break;
                case 4:
                    this.gameScore += (1200 * this.levelNum);
                    break;
            }
        }
    }
}
