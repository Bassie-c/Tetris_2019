using Microsoft.Xna.Framework;

namespace Tetris
{
    class TetrisBlock
    {
        char I , J, L, O, S, T, Z;
        bool[,] block;
        Color color;        
        Vector2 position;

        public TetrisBlock(char id)
        {
            /*switch (id)
            {
                case I:

                    break;
            }
               
    */
            if (id == I)
                block = new bool[1, 4];
            for (int i = 0; i < block.GetLength(1);)
            {
                block[1, i] = true;
            }

            position = new Vector2(135, 0);
            color = Color.LightBlue;
        }
    }
}