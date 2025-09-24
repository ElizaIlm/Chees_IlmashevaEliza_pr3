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
    public class Bishop
    {
        public int X, Y;
        public bool Select, Black;
        public Grid Figure;

        public Bishop(int X, int Y, bool Black)
        {
            this.X = X;
            this.Y = Y;
            this.Black = Black;
        }

        public void SelectFigure(object sender, MouseButtonEventArgs e)
        {
            MainWindow.init.OnSelectBishop(this);

            if (Select)
            {
                if (Black)
                    Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Bishop (black).png")));
                else
                    Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Bishop.png")));
                Select = false;
            }
            else
            {
                Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Bishop (select).png")));
                Select = true;
            }
        }

        public void Transform(int X, int Y)
        {
            // Проверка диагонали
            if (Math.Abs(X - this.X) != Math.Abs(Y - this.Y))
            {
                SelectFigure(null, null);
                return;
            }

            // Проверка пути
            if (!CheckDiagonalPathClear(this.X, this.Y, X, Y))
            {
                SelectFigure(null, null);
                return;
            }

            // Проверка на атаку
            Bishop attackedBishop = MainWindow.init.Bishops.Find(b =>
                b.X == X && b.Y == Y && b.Black != this.Black);

            Pawn attackedPawn = MainWindow.init.Pawns.Find(p =>
                p.X == X && p.Y == Y && p.Black != this.Black);

            // Удаление фигуры при атаке
            if (attackedBishop != null)
            {
                MainWindow.init.Bishops.Remove(attackedBishop);
                MainWindow.init.gameBoard.Children.Remove(attackedBishop.Figure);
            }
            else if (attackedPawn != null)
            {
                MainWindow.init.Pawns.Remove(attackedPawn);
                MainWindow.init.gameBoard.Children.Remove(attackedPawn.Figure);
            }

            // Перемещение слона
            Grid.SetColumn(this.Figure, X);
            Grid.SetRow(this.Figure, Y);
            this.X = X;
            this.Y = Y;

            SelectFigure(null, null);
        }

        private bool CheckDiagonalPathClear(int startX, int startY, int endX, int endY)
        {
            int xDirection = (endX > startX) ? 1 : -1;
            int yDirection = (endY > startY) ? 1 : -1;
            int distance = Math.Abs(endX - startX);

            for (int i = 1; i < distance; i++)
            {
                int checkX = startX + i * xDirection;
                int checkY = startY + i * yDirection;

                // Проверка пешек на пути
                foreach (var pawn in MainWindow.init.Pawns)
                {
                    if (pawn.X == checkX && pawn.Y == checkY)
                        return false;
                }

                // Проверка других слонов на пути
                foreach (var bishop in MainWindow.init.Bishops)
                {
                    if (bishop != this && bishop.X == checkX && bishop.Y == checkY)
                        return false;
                }
            }

            return true;
        }
    }
}
