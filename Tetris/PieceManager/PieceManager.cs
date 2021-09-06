using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// PIECE MANAGER ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

// manages the falling piece & the piece "on deck"

// employs the SINGLETON PATTERN (http://csharpindepth.com/Articles/General/Singleton.aspx)
// to ensure that no duplicates of this class are made

namespace Tetris
{
    public class PieceManager
    {
        // Data ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        Piece pieceFalling;
        Piece pieceOnDeck;
        Piece pieceGhost;

        int fallCounter;

        private static PieceManager pInstance = null;

        // Setting up Singleton Pattern ------------------------------------------------------------------------------------------------------------------------------------------

        public static void CreatePieceManager()
        {
            if (pInstance == null)
            {
                // create instance
                pInstance = new PieceManager();

                // create new pieces
                pInstance.SetFalling(pInstance.privRandomPiece());
                pInstance.pieceOnDeck = pInstance.privRandomPiece();
                pInstance.SetGhost();
            }
        }

        private static PieceManager privGetInstance()
        {
            return pInstance;
        }

        protected PieceManager()
        {
            // init data
            pieceFalling = null;
            pieceOnDeck = null;
            fallCounter = 0;
        }

        ~PieceManager()
        {

        }

        // Move Methods ------------------------------------------------------------------------------------------------------------------------------------------

        public static void moveFallingPieceRight()
        {
            Piece pieceFalling = privGetInstance().pieceFalling;
            if (pieceFalling != null) pieceFalling.BumpRight();
        }

        public static void moveFallingPieceLeft()
        {
            Piece pieceFalling = privGetInstance().pieceFalling;
            if (pieceFalling != null) pieceFalling.BumpLeft();
        }

        public static void moveFallingPieceSoftDrop()
        {
            Piece pieceFalling = privGetInstance().pieceFalling;
            if (pieceFalling != null) pieceFalling.SoftDrop();
        }

        public static void moveFallingPieceHardDrop()
        {
            Piece pieceFalling = privGetInstance().pieceFalling;
            if (pieceFalling != null) pieceFalling.HardDrop();
        }

        public static void RotateFallingPiece_Clockwise()
        {
            Piece pieceFalling = privGetInstance().pieceFalling;
            if (pieceFalling != null) pieceFalling.Rotate_Clockwise();
        }

        public static void RotateFallingPiece_CounterClockwise()
        {
            Piece pieceFalling = privGetInstance().pieceFalling;
            if (pieceFalling != null) pieceFalling.Rotate_CounterClockwise();
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
            // do nothing
        }

        private void PlayUpdate()
        {
           // update fall counter

            privGetInstance().fallCounter++;

            // check for landings, otherwise move the falling piece down

            if (privGetInstance().Landing())
            {
                GridManager.AddToGrid(privGetInstance().pieceFalling);
                privGetInstance().SwapPieces();
            }
            else
            {
                //update ghost piece
                privGetInstance().pieceGhost.setGhostPosition(privGetInstance().pieceFalling);

                // if fall counter matches up, update falling pieces
                if (privGetInstance().fallCounter % 50 == 0)
                {
                    // update falling piece
                    privGetInstance().pieceFalling.Update();
                    privGetInstance().pieceOnDeck.Update();
                }
            }
        }

        private void GameOverUpdate()
        {
            // do nothing
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

        }

        private void PlayDraw()
        {
            privGetInstance().pieceGhost.Render();
            privGetInstance().pieceFalling.Render();
            privGetInstance().pieceOnDeck.Render();
        }

        private void GameOverDraw()
        {

        }

        // Create Random Piece ------------------------------------------------------------------------------------------------------------------------------------------

        private Piece privRandomPiece()
        {
            Random r = new Random();
            int nextPiece = r.Next(1, 8);

            switch (nextPiece)
            {
                case 1:
                    return new Piece_Line(Constants.PREVIEW_WINDOW_X - 1, Constants.PREVIEW_WINDOW_Y - 1);
                case 2:
                    return new Piece_Square(Constants.PREVIEW_WINDOW_X - 1, Constants.PREVIEW_WINDOW_Y - 1);
                case 3:
                    return new Piece_L1(Constants.PREVIEW_WINDOW_X - 1, Constants.PREVIEW_WINDOW_Y - 1);
                case 4:
                    return new Piece_L2(Constants.PREVIEW_WINDOW_X - 1, Constants.PREVIEW_WINDOW_Y - 1);
                case 5:
                    return new Piece_T(Constants.PREVIEW_WINDOW_X - 1, Constants.PREVIEW_WINDOW_Y - 1);
                case 6:
                    return new Piece_Z1(Constants.PREVIEW_WINDOW_X - 1, Constants.PREVIEW_WINDOW_Y - 1);
                case 7:
                    return new Piece_Z2(Constants.PREVIEW_WINDOW_X - 1, Constants.PREVIEW_WINDOW_Y - 1);
            }
            return null;

        }

        // Check For Landing ------------------------------------------------------------------------------------------------------------------------------------------
        
        private bool Landing()
        {
            Piece piece = privGetInstance().pieceFalling;

            if (piece.getMinBlockCoordY() <= Constants.GAME_MIN_Y)
                return true;
            else if (GridManager.Landing(piece))
            {
                return true;
            }
            else
                return false;
        }

        // Swap Falling & On Deck Pieces ------------------------------------------------------------------------------------------------------------------------------------------

        private void SwapPieces()
        {
            // update falling piece
            privGetInstance().SetFalling(privGetInstance().pieceOnDeck);

            // update on deck piece
            privGetInstance().SetOnDeck(privRandomPiece());

            // update ghost piece
            privGetInstance().SetGhost();
        }

        private void SetFalling(Piece piece)
        {
            if (piece != null)
            {
                privGetInstance().pieceFalling = piece;
                privGetInstance().pieceFalling.setPosition(Constants.GAME_MIDDLE_X, Constants.GAME_MAX_Y-1);
                privGetInstance().pieceFalling.mode = Piece.Piece_Mode.FALLING;
            }
        }

        private void SetGhost()
        {
            if (this.pieceFalling != null)
            {
                switch (this.pieceFalling.pieceType)
                {
                    case Piece.Piece_Type.Line:
                        this.pieceGhost = new Piece_Line(0, 0);
                        this.pieceGhost.pieceColor = DrawColor.Shade.COLOR_GREY;
                        this.pieceGhost.setGhostPosition(this.pieceFalling);
                        break;
                    case Piece.Piece_Type.T:
                        this.pieceGhost = new Piece_T(0, 0);
                        this.pieceGhost.pieceColor = DrawColor.Shade.COLOR_GREY;
                        this.pieceGhost.setGhostPosition(this.pieceFalling);
                        break;
                    case Piece.Piece_Type.L1:
                        this.pieceGhost = new Piece_L1(0, 0);
                        this.pieceGhost.pieceColor = DrawColor.Shade.COLOR_GREY;
                        this.pieceGhost.setGhostPosition(this.pieceFalling);
                        break;
                    case Piece.Piece_Type.L2:
                        this.pieceGhost = new Piece_L2(0, 0);
                        this.pieceGhost.pieceColor = DrawColor.Shade.COLOR_GREY;
                        this.pieceGhost.setGhostPosition(this.pieceFalling);
                        break;
                    case Piece.Piece_Type.Z1:
                        this.pieceGhost = new Piece_Z1(0, 0);
                        this.pieceGhost.pieceColor = DrawColor.Shade.COLOR_GREY;
                        this.pieceGhost.setGhostPosition(this.pieceFalling);
                        break;
                    case Piece.Piece_Type.Z2:
                        this.pieceGhost = new Piece_Z2(0, 0);
                        this.pieceGhost.pieceColor = DrawColor.Shade.COLOR_GREY;
                        this.pieceGhost.setGhostPosition(this.pieceFalling);
                        break;
                    case Piece.Piece_Type.Square:
                        this.pieceGhost = new Piece_Square(0, 0);
                        this.pieceGhost.pieceColor = DrawColor.Shade.COLOR_GREY;
                        this.pieceGhost.setGhostPosition(this.pieceFalling);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetOnDeck(Piece piece)
        {
            if (piece != null)
            {
                privGetInstance().pieceOnDeck = piece;
                privGetInstance().pieceOnDeck.mode = Piece.Piece_Mode.ON_DECK;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------
    }
}
