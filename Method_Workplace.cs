using System;

public class Class1
{
	public Class1()
	{
        /// <summary>
        /// Rotates a block clockwise.
        /// </summary>
        void RotateBlock(bool[,] block)
        {
            bool[,] rotatedBlock = new bool[block.GetLength(1), block.GetLength(0)];
            for (int x = 0; x < block.GetLength(0); x++)
            {
                for (int y = 0; y < block.GetLength(1); y++)
                {
                    if (block[x, y])
                        rotatedBlock[rotatedBlock.GetLength(0)-y, x] = true;
                }
            }
            block = rotatedBlock;
        }
	}
}
