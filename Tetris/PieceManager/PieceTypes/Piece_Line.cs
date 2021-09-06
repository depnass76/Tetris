using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Piece_Line : Piece
    {
        // constructor
        public Piece_Line(int xIn, int yIn) : base(xIn, yIn, Piece_Type.Line)
        {
            // arrange blocks

            ArrangeBlocks();
            this.pieceColor = DrawColor.Shade.COLOR_CYAN;
        }

        // arrange blocks

        public override void ArrangeBlocks()
        {
            switch (this.currOrient)
            {
                case Orientation.ORIENT_0:
                    setBlocks(1, 0, 1, 1, 1, 2, 1, 3);
                    break;
                case Orientation.ORIENT_1:
                    setBlocks(0, 2, 1, 2, 2, 2, 3, 2);
                    break;
                case Orientation.ORIENT_2:
                    setBlocks(2, 0, 2, 1, 2, 2, 2, 3);
                    break;
                case Orientation.ORIENT_3:
                    setBlocks(0, 1, 1, 1, 2, 1, 3, 1);
                    break;
            }
        }
    }
}
