/* Gavin Rouse
 * This class contains the implementation of one tic tac toe square.
 * It is represented by the top left point of the square and an X
 * and Y width. It also has a field that holds whether or not the
 * square is filled, and a field that holds what shape the square has.
 */

using System.Windows;

namespace TicTacToe
{
    class Square
    {
        public Point TopLeft { get; }
        public int XWidth { get; }
        public int YWidth { get; }
        public bool Filled { get; set; }
        public char Choice { get; set; }

        public Square()
        {
            TopLeft = new Point(0, 0);
            XWidth = 0;
            YWidth = 0;
            Filled = false;
            Choice = 'Z';
        }

        public Square(Point tl, int x, int y)
        {
            TopLeft = tl;
            XWidth = x;
            YWidth = y;
            Filled = false;
            Choice = 'Z';
        }
    }
}
