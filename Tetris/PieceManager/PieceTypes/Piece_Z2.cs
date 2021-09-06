﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Piece_Z2 : Piece
    {
        // constructor
        public Piece_Z2(int xIn, int yIn) : base(xIn, yIn, Piece_Type.Z2)
        {
            ArrangeBlocks();
            this.pieceColor = DrawColor.Shade.COLOR_RED;
        }

        // arrange blocks

        public override void ArrangeBlocks()
        {
            switch (this.currOrient)
            {
                case Orientation.ORIENT_0:
                    setBlocks(0, 1, 1, 1, 1, 2, 2, 2);
                    break;
                case Orientation.ORIENT_1:
                    setBlocks(0, 2, 0, 3, 1, 2, 1, 1);
                    break;
                case Orientation.ORIENT_2:
                    setBlocks(0, 2, 1, 2, 1, 3, 2, 3);
                    break;
                case Orientation.ORIENT_3:
                    setBlocks(1, 2, 1, 3, 2, 1, 2, 2);
                    break;
            }
        }


    }
}
