using System;
using System.Diagnostics;

// INPUT MANAGER ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

// looks for input and responds accordingly

// employs the SINGLETON PATTERN (http://csharpindepth.com/Articles/General/Singleton.aspx)
// to ensure that no duplicates of this class are made

namespace Tetris
{
    public class InputManager
    {
        // Data ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private static InputManager pInstance = null;

        int countSinceMoveRight = 0;
        int countSinceMoveLeft = 0;
        int countSinceRotate = 0;
        int countSinceMusicSwitched = 0;
        int countSinceHardDrop = 0;

        // Setting up Singleton Pattern ------------------------------------------------------------------------------------------------------------------------------------------

        public static void CreateInputManager()
        {
            if (pInstance == null)
            {
                pInstance = new InputManager();
            }
        }

        private static InputManager privGetInstance()
        {
            return pInstance;
        }

        protected InputManager()
        {

        }

        ~InputManager()
        {

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
            // look for input
            privGetInstance().privTestStart();
        }

        private void PlayUpdate()
        {
            // update counters

            privGetInstance().privUpdateCounters();

            // look for input

            privGetInstance().privTestRightArrow();
            privGetInstance().privTestLeftArrow();
            privGetInstance().privTestDownArrow();
            privGetInstance().privTestHardDrop();

            privGetInstance().privTestRotateClockwise();
            privGetInstance().privTestRotateCounterClockwise();

            privGetInstance().privTestM();
        }

        private void GameOverUpdate()
        {
            // look for input
            privGetInstance().privTestStart();
        }

        // Update Counters ------------------------------------------------------------------------------------------------------------------------------------------

        private void privUpdateCounters()
        {
            privGetInstance().countSinceMoveRight++;
            privGetInstance().countSinceMoveLeft++;
            privGetInstance().countSinceRotate++;
            privGetInstance().countSinceMusicSwitched++;
            privGetInstance().countSinceHardDrop++;
        }

        // Look For Keyboard Input ------------------------------------------------------------------------------------------------------------------------------------------

        private void privTestStart()
        {
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_G))
            {
                Controls.StartGame();
            }
        }

        private void privTestRightArrow()
        {
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) && privGetInstance().countSinceMoveRight > Constants.TIME_BETWEEN_MOVES)
            {
                privGetInstance().countSinceMoveRight = 0;
                Controls.MoveRight();
            }
        }

        private void privTestLeftArrow()
        {
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) && privGetInstance().countSinceMoveLeft > Constants.TIME_BETWEEN_MOVES)
            {
                privGetInstance().countSinceMoveLeft = 0;
                Controls.MoveLeft();
            }
        }

        private void privTestDownArrow()
        {
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_DOWN))
            {
                Controls.SoftDrop();
            }
        }

        private void privTestHardDrop()
        {
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE) && privGetInstance().countSinceHardDrop > Constants.TIME_BETWEEN_MOVES * 2)
            {
                privGetInstance().countSinceHardDrop = 0;
                Controls.HardDrop();
            }
        }

        private void privTestRotateClockwise()
        {
            if ((Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_UP) || Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_X)) && privGetInstance().countSinceRotate > Constants.TIME_BETWEEN_MOVES)
            {
                privGetInstance().countSinceRotate = 0;
                Controls.Rotate_Clockwise();
            }
        }

        private void privTestRotateCounterClockwise()
        {
            if ((((Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_RIGHT_CONTROL)) ||(Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_LEFT_CONTROL))) && (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_Z))) && (privGetInstance().countSinceRotate > Constants.TIME_BETWEEN_MOVES))
            {
                privGetInstance().countSinceRotate = 0;
                Controls.Rotate_CounterClockwise();
            }
        }

        private void privTestM()
        {
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_M) && privGetInstance().countSinceMusicSwitched > Constants.DELAYED_MOVE_TIME)
            {
                privGetInstance().countSinceMusicSwitched = 0;
                Controls.MusicOnOff();
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }
}
