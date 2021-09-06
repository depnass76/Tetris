using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Piece_Square : Piece
    {
        // constructor
        public Piece_Square(int xIn, int yIn) : base(xIn, yIn, Piece_Type.Square)
        {
            ArrangeBlocks();
            this.pieceColor = DrawColor.Shade.COLOR_YELLOW;
        }

        // arrange blocks

        public override void ArrangeBlocks()
        {
            switch (this.currOrient)
            {
                case Orientation.ORIENT_0:
                    setBlocks(1, 1, 2, 1, 1, 2, 2, 2);
                    break;
                case Orientation.ORIENT_1:
                    setBlocks(1, 1, 2, 1, 1, 2, 2, 2);
                    break;
                case Orientation.ORIENT_2:
                    setBlocks(1, 1, 2, 1, 1, 2, 2, 2);
                    break;
                case Orientation.ORIENT_3:
                    setBlocks(1, 1, 2, 1, 1, 2, 2, 2);
                    break;
            }
        }


    }
}
