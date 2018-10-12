using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class TetrisBlock
    {
        protected bool[,] block;

        public TetrisBlock()
        {
            //block = new bool[,];
        }

        /// <summary>
        /// Rotates a block clockwise.
        /// </summary>
        void RotateBlock()
        {
            bool[,] rotatedBlock = new bool[block.GetLength(1), block.GetLength(0)];
            for (int x = 0; x < block.GetLength(0); x++)
            {
                for (int y = 0; y < block.GetLength(1); y++)
                {
                    if (block[x, y])
                        rotatedBlock[rotatedBlock.GetLength(0) - y, x] = true;
                }
            }
            block = rotatedBlock;
        }
    }

    class LBlock : TetrisBlock
    {
    }

    //class Block : TetrisBlock

}
