using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Piece_Z1 : Piece
    {
        // constructor
        public Piece_Z1(int xIn, int yIn) : base(xIn, yIn, Piece_Type.Z1)
        {
            ArrangeBlocks();
            this.pieceColor = DrawColor.Shade.COLOR_LT_GREEN;
        }

        // arrange blocks

        public override void ArrangeBlocks()
        {
            switch (this.currOrient)
            {
                case Orientation.ORIENT_0:
                    setBlocks(0, 2, 1, 2, 1, 1, 2, 1);
                    break;
                case Orientation.ORIENT_1:
                    setBlocks(0, 1, 0, 2, 1, 2, 1, 3);
                    break;
                case Orientation.ORIENT_2:
                    setBlocks(0, 3, 1, 3, 1, 2, 2, 2);
                    break;
                case Orientation.ORIENT_3:
                    setBlocks(1, 1, 1, 2, 2, 2, 2, 3);
                    break;
            }
        }


    }
}
