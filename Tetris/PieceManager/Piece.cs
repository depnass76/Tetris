using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Piece
    {
        // enums & structs

        public enum Piece_Type
        {
            Line,
            T,
            L1,
            L2,
            Z1,
            Z2,
            Square,
            Ghost
        }

        public enum Orientation
        {
            ORIENT_0,
            ORIENT_1,
            ORIENT_2,
            ORIENT_3
        };

        public enum Piece_Mode
        {
            FALLING, 
            ON_DECK,
            GHOST
        }

        public struct blockCoords
        {
            public int blockX;
            public int blockY;

            public blockCoords(int xIn, int yIn)
            {
                blockX = xIn;
                blockY = yIn;
            }
        }
       


        // public data

        public int x;
        public int y;

        public blockCoords[] blocks;

        public Piece_Type pieceType;
        public Orientation currOrient;
        public Piece_Mode mode;
        public DrawColor.Shade pieceColor;

        // constructors

        public Piece(int xIn, int yIn, Piece_Type typeIn)
        {
            // init x coords
            this.x = xIn;
            this.y = yIn;

            // type
            this.pieceType = typeIn;

            // mode 
            this.mode = Piece_Mode.ON_DECK;

            // initialize at orientation 0
            this.currOrient = Orientation.ORIENT_0;

            // create new blocks
            this.blocks = new blockCoords[4];

            this.blocks[0] = new blockCoords(0, 0);
            this.blocks[1] = new blockCoords(0, 0);
            this.blocks[2] = new blockCoords(0, 0);
            this.blocks[3] = new blockCoords(0, 0);
        }

        // arrange blocks

        public virtual void ArrangeBlocks()
        {
            // to be overridden by derived classes
        }


        // getters & setters

        public void setPosition(int xIn, int yIn)
        {
            this.x = xIn;
            this.y = yIn;
        }

        public void setGhostPosition(Piece pieceFalling)
        {
            this.x = pieceFalling.x;
            this.y = pieceFalling.y;
            this.currOrient = pieceFalling.currOrient;
            ArrangeBlocks();

            // todo: hard drop

            this.HardDrop();
        }

        public void setBlocks(int x0, int y0, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            this.blocks[0].blockX = x0;
            this.blocks[0].blockY = y0;

            this.blocks[1].blockX = x1;
            this.blocks[1].blockY = y1;

            this.blocks[2].blockX = x2;
            this.blocks[2].blockY = y2;

            this.blocks[3].blockX = x3;
            this.blocks[3].blockY = y3;
        }

        public int getHeight()
        {
            // NOT a stored variable, because it depends on orientation of the blocks!!
            return (getMaxBlockCoordY() - getMinBlockCoordY()) + 1;
        }

        public int getWidth()
        {
            // NOT a stored variable, because it depends on orientation of the blocks!!
            return (getMaxBlockCoordX() - getMinBlockCoordX()) + 1;
        }

        public int getMaxBlockCoordX()
        {
            int maxX = 0;

            for (int i = 0; i < 4; i++)
            {
                if (blocks[i].blockX > maxX) maxX = blocks[i].blockX;
            }
            return this.x + maxX;
        }

        public int getMinBlockCoordX()
        {
            int minX = 3;

            for (int i = 0; i < 4; i++)
            {
                if (blocks[i].blockX < minX) minX = blocks[i].blockX;
            }
            return this.x + minX;
        }

        public int getMaxBlockCoordY()
        {
            int maxY = 0;

            for (int i = 0; i < 4; i++)
            {
                if (blocks[i].blockY > maxY) maxY = blocks[i].blockY;
            }
            return this.y + maxY;
        }

        public int getMinBlockCoordY()
        {
            int minY = 3;

            for (int i = 0; i < 4; i++)
            {
                if (blocks[i].blockY < minY) minY = blocks[i].blockY;
            }
            return this.y + minY;
        }

        public bool OutOfBounds()
        {
            // looks at the grid coords and determines if out of bounds 

            if (this.mode == Piece_Mode.FALLING)
            {
                // look right
                if (getMaxBlockCoordX() > Constants.GAME_MAX_X)
                {
                    return true;
                }
                // look left
                else if (getMinBlockCoordX() < Constants.GAME_MIN_X)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
            
        }

        public bool IntersectsGrid()
        {
            if (this.mode == Piece_Mode.FALLING)
                return GridManager.IntersectsGrid(this);
            else return false;
        }

        // bump right, bump left

        public void BumpRight()
        {
            this.x += 1;

            if (OutOfBounds() || IntersectsGrid())
                this.x -= 1;
        }

        public void BumpLeft()
        {
            this.x -= 1;

            if (OutOfBounds() || IntersectsGrid())
                this.x += 1;
        }

        public void SoftDrop()
        {
            this.y -= 1;
        }

        public void HardDrop()
        {
            while ((!GridManager.Landing(this) && !OutOfBounds()) && (this.getMinBlockCoordY() > 0))
            {
                SoftDrop();
            }
        }

        public void Rotate_Clockwise()
        {
            if (this.currOrient == Orientation.ORIENT_3)
            {
                this.currOrient = Orientation.ORIENT_0;
                ArrangeBlocks();
                // turn back if out of bounds
                if (OutOfBounds() || IntersectsGrid())
                {
                    this.currOrient = Orientation.ORIENT_3;
                    ArrangeBlocks();
                }
                //Added a turning noise for a succesful turn
                else if (!(this.mode == Piece_Mode.ON_DECK) && !(this is Piece_Square)) SoundManager.PieceTurn();
            }
            else if (this.currOrient == Orientation.ORIENT_0)
            {
                this.currOrient = Orientation.ORIENT_1;
                ArrangeBlocks();
                // turn back if out of bounds
                if (OutOfBounds() || IntersectsGrid())
                {
                    this.currOrient = Orientation.ORIENT_0;
                    ArrangeBlocks();
                }
                else if (!(this.mode == Piece_Mode.ON_DECK) && !(this is Piece_Square)) SoundManager.PieceTurn();
            }
            else if (this.currOrient == Orientation.ORIENT_1)
            {
                this.currOrient = Orientation.ORIENT_2;
                ArrangeBlocks();

                // turn back if out of bounds
                if (OutOfBounds() || IntersectsGrid())
                {
                    this.currOrient = Orientation.ORIENT_1;
                    ArrangeBlocks();
                }
                else if (!(this.mode == Piece_Mode.ON_DECK) && !(this is Piece_Square)) SoundManager.PieceTurn();
            }
            else if (this.currOrient == Orientation.ORIENT_2)
            {
                this.currOrient = Orientation.ORIENT_3;
                ArrangeBlocks();

                // turn back if out of bounds
                if (OutOfBounds() || IntersectsGrid())
                {
                    this.currOrient = Orientation.ORIENT_2;
                    ArrangeBlocks();
                }
                else if (!(this.mode == Piece_Mode.ON_DECK) && !(this is Piece_Square)) SoundManager.PieceTurn();
            }

        }

        public void Rotate_CounterClockwise()
        {
            if (this.currOrient == Orientation.ORIENT_3)
            {
                this.currOrient = Orientation.ORIENT_2;
                ArrangeBlocks();
                // turn back if out of bounds
                if (OutOfBounds() || IntersectsGrid())
                {
                    this.currOrient = Orientation.ORIENT_3;
                    ArrangeBlocks();
                }
                //Added a turning noise for a succesful turn
                else if (!(this.mode == Piece_Mode.ON_DECK) && !(this is Piece_Square)) SoundManager.PieceTurn();
            }
            else if (this.currOrient == Orientation.ORIENT_0)
            {
                this.currOrient = Orientation.ORIENT_3;
                ArrangeBlocks();
                // turn back if out of bounds
                if (OutOfBounds() || IntersectsGrid())
                {
                    this.currOrient = Orientation.ORIENT_0;
                    ArrangeBlocks();
                }
                else if (!(this.mode == Piece_Mode.ON_DECK) && !(this is Piece_Square)) SoundManager.PieceTurn();
            }
            else if (this.currOrient == Orientation.ORIENT_1)
            {
                this.currOrient = Orientation.ORIENT_0;
                ArrangeBlocks();

                // turn back if out of bounds
                if (OutOfBounds() || IntersectsGrid())
                {
                    this.currOrient = Orientation.ORIENT_1;
                    ArrangeBlocks();
                }
                else if (!(this.mode == Piece_Mode.ON_DECK) && !(this is Piece_Square)) SoundManager.PieceTurn();
            }
            else if (this.currOrient == Orientation.ORIENT_2)
            {
                this.currOrient = Orientation.ORIENT_1;
                ArrangeBlocks();

                // turn back if out of bounds
                if (OutOfBounds() || IntersectsGrid())
                {
                    this.currOrient = Orientation.ORIENT_2;
                    ArrangeBlocks();
                }
                else if (!(this.mode == Piece_Mode.ON_DECK) && !(this is Piece_Square)) SoundManager.PieceTurn();
            }

        }


        // grid helpers (refactor me!)

        public int getXofBlock(int blockNum)
        {
            int returnX = this.x + this.blocks[blockNum].blockX;
            if (returnX < 0) return 0;
            else return returnX;
        }

        public int getYofBlock(int blockNum)
        {
            int returnY = this.y + this.blocks[blockNum].blockY;
            if (returnY < 0) return 0;
            else return returnY;
        }

        // update

        public void Update()
        {
            switch (mode)
            {
                case Piece_Mode.FALLING:
                    SoftDrop();
                    break;
                case Piece_Mode.ON_DECK:
                    Rotate_Clockwise();
                    break;
                case Piece_Mode.GHOST:
                    break;
            }
        }

        public virtual void Render()
        {
            // draw all four blocks
            SOM.drawBox(getXofBlock(0), getYofBlock(0), pieceColor);
            SOM.drawBox(getXofBlock(1), getYofBlock(1), pieceColor);
            SOM.drawBox(getXofBlock(2), getYofBlock(2), pieceColor);
            SOM.drawBox(getXofBlock(3), getYofBlock(3), pieceColor);
        }




    }
}
