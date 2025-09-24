using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace pr3itogovaya.Classes
{
    public class Pawn
    {
        public int X, Y;
        public bool Select, Black;
        public Grid Figure;

        public Pawn(int X, int Y, bool Black)
        {
            this.X = X;
            this.Y = Y;
            this.Black = Black;
        }

        public void SelectFigure(object sender, MouseButtonEventArgs e)
        {
            MainWindow.init.OnSelect(this);

            if (Select)
            {
                if (Black)
                    Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn (black).png")));
                else
                    Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn.png")));
                Select = false;
            }
            else
            {
                Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn (select).png")));
                Select = true;
            }
        }

        public void Transform(int X, int Y)
        {
            // Проверка горизонтали
            if (X != this.X)
            {
                // Проверка атаки по диагонали
                if ((Black && Y == this.Y - 1 && (X == this.X - 1 || X == this.X + 1)) ||
                    (!Black && Y == this.Y + 1 && (X == this.X - 1 || X == this.X + 1)))
                {
                    // Удаление атакованной фигуры
                    Pawn attackedPawn = MainWindow.init.Pawns.Find(p =>
                        p.X == X && p.Y == Y && p.Black != this.Black);

                    if (attackedPawn != null)
                    {
                        MainWindow.init.Pawns.Remove(attackedPawn);
                        MainWindow.init.gameBoard.Children.Remove(attackedPawn.Figure);
                    }

                    // Перемещение пешки
                    Grid.SetColumn(this.Figure, X);
                    Grid.SetRow(this.Figure, Y);
                    this.X = X;
                    this.Y = Y;
                    SelectFigure(null, null);
                }
                else
                {
                    SelectFigure(null, null);
                }
                return;
            }

            // Проверка хода вперед
            if (Black)
            {
                if ((this.Y == 6 && this.Y - 2 == Y) || this.Y - 1 == Y)
                {
                    Grid.SetColumn(this.Figure, X);
                    Grid.SetRow(this.Figure, Y);
                    this.X = X;
                    this.Y = Y;
                }
            }
            else
            {
                if ((this.Y == 1 && this.Y + 2 == Y) || this.Y + 1 == Y)
                {
                    Grid.SetColumn(this.Figure, X);
                    Grid.SetRow(this.Figure, Y);
                    this.X = X;
                    this.Y = Y;
                }
            }
            SelectFigure(null, null);
        }
    }
}
