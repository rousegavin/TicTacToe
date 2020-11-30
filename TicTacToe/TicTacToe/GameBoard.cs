/* Gavin Rouse
 * This class contains the implementation of the tic tac toe gameboard.
 * It consists of a canvas and a list of squares that are on the canvas.
 */

using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TicTacToe
{
    class GameBoard
    {
        Canvas gameBoard;
        ArrayList squares;

        public GameBoard(Canvas c)
        {
            gameBoard = c;
            squares = new ArrayList();
            InitializeSquares();
        }

        private void InitializeSquares()
        {
            Point p = new Point(0, 0);
            squares.Add(new Square(p, 117, 97));
            p.X = 122;
            squares.Add(new Square(p, 115, 97));
            p.X = 242;
            squares.Add(new Square(p, 113, 97));
            p.X = 0;
            p.Y = 102;
            squares.Add(new Square(p, 117, 95));
            p.X = 122;
            squares.Add(new Square(p, 115, 95));
            p.X = 242;
            squares.Add(new Square(p, 113, 95));
            p.X = 0;
            p.Y = 202;
            squares.Add(new Square(p, 117, 93));
            p.X = 122;
            squares.Add(new Square(p, 115, 93));
            p.X = 242;
            squares.Add(new Square(p, 113, 93));
        }

        public Square GetSquare(int i)
        {
            return (Square)squares[i];
        }

        public Point HitSquare(MouseButtonEventArgs e, char turn)
        {
            foreach (Square s in squares)
            {
                double x = e.GetPosition(gameBoard).X;
                double y = e.GetPosition(gameBoard).Y;

                if (x <= s.TopLeft.X + s.XWidth && x >= s.TopLeft.X)
                {
                    if (y <= s.TopLeft.Y + s.YWidth && y >= s.TopLeft.Y)
                    {
                        if (!s.Filled)
                        {
                            s.Filled = true;
                            s.Choice = turn;
                            return s.TopLeft;
                        }
                    }
                }
            }

            return new Point(-1, -1);
        }

        public bool IsFull()
        {
            foreach (Square s in squares)
            {
                if (s.Filled == false)
                    return false;
            }

            return true;
        }

        public char IsWin()
        {
            for (int i = 0; i <= 2; i++)
            {
                Square s1 = (Square)squares[i];
                Square s2 = (Square)squares[i + 3];
                Square s3 = (Square)squares[i + 6];

                if (s1.Choice != 'Z')
                {
                    if (s1.Choice == s2.Choice && s1.Choice == s3.Choice)
                        return s1.Choice;
                }
            }

            for (int i = 0; i <= 6; i += 3)
            {
                Square s1 = (Square)squares[i];
                Square s2 = (Square)squares[i + 1];
                Square s3 = (Square)squares[i + 2];

                if (s1.Choice != 'Z')
                {
                    if (s1.Choice == s2.Choice && s1.Choice == s3.Choice)
                        return s1.Choice;
                }
            }

            for (int i = 0; i == 0; i++)
            {
                Square s1 = (Square)squares[i];
                Square s2 = (Square)squares[i + 4];
                Square s3 = (Square)squares[i + 8];

                if (s1.Choice != 'Z')
                {
                    if (s1.Choice == s2.Choice && s1.Choice == s3.Choice)
                        return s1.Choice;
                }
            }

            for (int i = 2; i == 2; i++)
            {
                Square s1 = (Square)squares[i];
                Square s2 = (Square)squares[i + 2];
                Square s3 = (Square)squares[i + 4];

                if (s1.Choice != 'Z')
                {
                    if (s1.Choice == s2.Choice && s1.Choice == s3.Choice)
                        return s1.Choice;
                }
            }

            return 'N';
        }
    }
}
