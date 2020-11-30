/* Gavin Rouse
 * This class contains all of the event handlers for the main window,
 * as well as all necessary supporting methods.
 */

using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameBoard gameBoard;
        char turn = 'X';
        bool coinFlip = false;
        bool won = false;
        bool full = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeEvents();
            gameBoard = new GameBoard(ticTacToeCanvas);
        }

        private void InitializeEvents()
        {
            ticTacToeCanvas.MouseDown += new MouseButtonEventHandler(BoardMouseClick);
            coinTossButton.Click += new RoutedEventHandler(CoinTossButtonClick);
            resetButton.Click += new RoutedEventHandler(ResetButtonClick);
            coinTossMenu.Click += new RoutedEventHandler(CoinTossButtonClick);
            resetMenu.Click += new RoutedEventHandler(ResetButtonClick);
            onePlayerMenu.Click += new RoutedEventHandler(OnePlayerMenuClick);
            twoPlayerMenu.Click += new RoutedEventHandler(TwoPlayerMenuClick);
            aboutMenu.Click += new RoutedEventHandler(AboutMenuClick);
            rulesMenu.Click += new RoutedEventHandler(RulesMenuClick);
        }

        private void BoardMouseClick(object sender, MouseButtonEventArgs e)
        {
            if (!won && !full && coinFlip)
            {
                Point p = gameBoard.HitSquare(e, turn);

                if (p.X == -1 && p.Y == -1)
                    return;

                DrawShape(p);
                UpdateStatus();
                IsFull();
                IsWin();

                if (onePlayerButton.IsChecked == true && !won && !full)
                    AIMove();
            }
        }

        private void CoinTossButtonClick(object sender, EventArgs e)
        {
            Random rand = new Random();

            if (rand.Next(1, 3) == 1)
                turn = 'X';

            else
                turn = 'O';

            coinTossButton.IsEnabled = false;
            coinTossMenu.IsEnabled = false;
            onePlayerButton.IsEnabled = false;
            twoPlayerButton.IsEnabled = false;
            onePlayerMenu.IsEnabled = false;
            twoPlayerMenu.IsEnabled = false;
            coinFlip = true;
            UpdateStatus();

            if (onePlayerButton.IsChecked == true)
            {
                if (turn == 'O')
                    AIMove();
            }
        }

        private void ResetButtonClick(object sender, EventArgs e)
        {
            ticTacToeCanvas.Children.RemoveRange(4, ticTacToeCanvas.Children.Count);
            gameBoard = new GameBoard(ticTacToeCanvas);
            coinTossButton.IsEnabled = true;
            onePlayerButton.IsEnabled = true;
            twoPlayerButton.IsEnabled = true;
            coinTossMenu.IsEnabled = true;
            onePlayerMenu.IsEnabled = true;
            twoPlayerMenu.IsEnabled = true;
            statusLabel.Content = "Flip The Coin";
            won = false;
            full = false;
            coinFlip = false;
        }

        private void OnePlayerMenuClick(object sender, EventArgs e)
        {
            twoPlayerButton.IsChecked = false;
            twoPlayerMenu.IsChecked = false;
            onePlayerButton.IsChecked = true;
            onePlayerMenu.IsChecked = true;
        }

        private void TwoPlayerMenuClick(object sender, EventArgs e)
        {
            onePlayerButton.IsChecked = false;
            onePlayerMenu.IsChecked = false;
            twoPlayerButton.IsChecked = true;
            twoPlayerMenu.IsChecked = true;
        }

        private void AboutMenuClick(object sender, EventArgs e)
        {
            MessageBox.Show("Developer: Gavin Rouse\n\nVersion: 1.0\n\n.NET Framework Version: 4.5.2\n\nBit Version: 32-bit");
        }

        private void RulesMenuClick(object sender, EventArgs e)
        {
            MessageBox.Show("The object of Tic Tac Toe is to get three in a row. You play on a three by three game board."
                + " The first player is known as X and the second is O. Players alternate placing Xs and Os on the game"
                + " board until either opponent has three in a row or all nine squares are filled.\n\nFirst, choose 'One "
                + "Player' if you would like to play against the computer or 'Two Players' if you "
                + "and another person would like to play. Then, click 'Coin Toss' to determine who will go first. Then, "
                + "take turns clicking a square to play in. If someone wins or the board fills up, click 'Reset' to "
                + "reset the game.");
        }

        private void DrawShape(Point p)
        {
            if (turn == 'X')
            {
                Line l1 = new Line();
                l1.X1 = p.X + 15;
                l1.Y1 = p.Y + 15;
                l1.X2 = p.X + 102;
                l1.Y2 = p.Y + 82;
                l1.Stroke = Brushes.Blue;
                l1.StrokeThickness = 5;
                Line l2 = new Line();
                l2.X1 = p.X + 102;
                l2.Y1 = p.Y + 15;
                l2.X2 = p.X + 15;
                l2.Y2 = p.Y + 82;
                l2.Stroke = Brushes.Blue;
                l2.StrokeThickness = 5;

                ticTacToeCanvas.Children.Add(l1);
                ticTacToeCanvas.Children.Add(l2);
                turn = 'O';
                return;
            }

            else if (turn == 'O')
            {
                Ellipse e = new Ellipse();
                e.Stroke = Brushes.Red;
                e.StrokeThickness = 5;
                e.Width = 63;
                e.Height = e.Width;
                e.Margin = new Thickness(p.X + 57 / 2, p.Y + 37 / 2, 0, 0);

                ticTacToeCanvas.Children.Add(e);
                turn = 'X';
                return;
            }
        }

        private void UpdateStatus()
        {
            if (twoPlayerButton.IsChecked == true)
            {
                if (turn == 'X')
                    statusLabel.Content = "Player One's Turn";

                else
                    statusLabel.Content = "Player Two's Turn";
            }

            else if (onePlayerButton.IsChecked == true)
            {
                if (turn == 'X')
                    statusLabel.Content = "Your Turn";

                else
                    statusLabel.Content = "Computer's Turn";
            }
        }

        private void AIMove()
        {
            bool chosen = false;
            Random rand = new Random();
            int choice = 0;

            while (!chosen)
            {
                choice = rand.Next(0, 9);
                Square s = gameBoard.GetSquare(choice);

                if (!s.Filled)
                {
                    s.Filled = true;
                    s.Choice = turn;
                    DrawShape(s.TopLeft);
                    chosen = true;
                }
            }
            
            UpdateStatus();
            IsFull();
            IsWin();
        }

        private void IsFull()
        {
            full = gameBoard.IsFull();
        }
        
        private void IsWin()
        {
            char c = gameBoard.IsWin();

            if (c == 'N')
            {
                if (full)
                    statusLabel.Content = "It's A Draw!";

                return;
            }

            if (c == 'X')
            {
                won = true;
                statusLabel.Content = "Player One Wins!";
            }

            if (c == 'O')
            {
                won = true;
                statusLabel.Content = "Player Two Wins!";
            }
        }
    }
}
