using pr3itogovaya.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr3itogovaya
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Pawn> Pawns = new List<Pawn>();
        //public List<Bishop> Bishops = new List<Bishop>();
        public static MainWindow init;

        public MainWindow()
        {
            InitializeComponent();
            init = this;

            // Находим gameBoard из XAML
            gameBoard = (Grid)FindName("gameBoard");

            // Если не нашли через FindName, создаем доску программно
            if (gameBoard == null)
            {
                gameBoard = new Grid();
                gameBoard.Name = "gameBoard";
                gameBoard.Background = Brushes.Beige;

                // Создаем строки и столбцы
                for (int i = 0; i < 8; i++)
                {
                    gameBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    gameBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }

                // Создаем клетки доски
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Grid cell = new Grid();
                        if ((i + j) % 2 == 0)
                            cell.Background = Brushes.White;
                        else
                            cell.Background = Brushes.Gray;

                        cell.MouseDown += SelectTile;
                        Grid.SetRow(cell, i);
                        Grid.SetColumn(cell, j);
                        gameBoard.Children.Add(cell);
                    }
                }

                // Добавляем gameBoard в основное окно
                Content = new Grid { Children = { gameBoard } };
            }

            // Создание пешек
            for (int i = 0; i <= 7; i++)
            {
                Pawns.Add(new Pawn(i, 1, false)); // Белые пешки
                Pawns.Add(new Pawn(i, 6, true));   // Черные пешки
            }

            //// Создание слонов
            //Bishops.Add(new Bishop(2, 0, false));  // Белый слон (c1)
            //Bishops.Add(new Bishop(5, 0, false));  // Белый слон (f1)
            //Bishops.Add(new Bishop(2, 7, true));   // Черный слон (c8)
            //Bishops.Add(new Bishop(5, 7, true));   // Черный слон (f8)

            CreateFigures();
        }

        public void CreateFigures()
        {
            // Создание пешек
            foreach (Pawn pawn in Pawns)
            {
                pawn.Figure = new Grid()
                {
                    Width = 50,
                    Height = 50
                };

                if (pawn.Black)
                    pawn.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn (black).png")));
                else
                    pawn.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn.png")));

                Grid.SetColumn(pawn.Figure, pawn.X);
                Grid.SetRow(pawn.Figure, pawn.Y);
                pawn.Figure.MouseDown += pawn.SelectFigure;
                gameBoard.Children.Add(pawn.Figure);
            }

            //// Создание слонов
            //foreach (Bishop bishop in Bishops)
            //{
            //    bishop.Figure = new Grid()
            //    {
            //        Width = 50,
            //        Height = 50
            //    };

            //    if (bishop.Black)
            //        bishop.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Bishop (black).png")));
            //    else
            //        bishop.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Bishop.png")));

            //    Grid.SetColumn(bishop.Figure, bishop.X);
            //    Grid.SetRow(bishop.Figure, bishop.Y);
            //    bishop.Figure.MouseDown += bishop.SelectFigure;
            //    gameBoard.Children.Add(bishop.Figure);
            //}
        }

        private void SelectTile(object sender, MouseButtonEventArgs e)
        {
            Grid Tile = sender as Grid;
            int X = Grid.GetColumn(Tile);
            int Y = Grid.GetRow(Tile);

            // Обработка хода пешки
            Pawn selectedPawn = Pawns.Find(p => p.Select);
            if (selectedPawn != null)
            {
                selectedPawn.Transform(X, Y);
                return;
            }

            //// Обработка хода слона
            //Bishop selectedBishop = Bishops.Find(b => b.Select);
            //if (selectedBishop != null)
            //{
            //    selectedBishop.Transform(X, Y);
            //}
        }

        public void OnSelect(Pawn pawn)
        {
            // Снятие выделения с других пешек
            foreach (Pawn p in Pawns)
                if (p != pawn && p.Select)
                    p.SelectFigure(null, null);

            //// Снятие выделения со всех слонов
            //foreach (Bishop b in Bishops)
            //    if (b.Select)
            //        b.SelectFigure(null, null);
        }

        //public void OnSelectBishop(Bishop bishop)
        //{
        //    // Снятие выделения с других слонов
        //    foreach (Bishop b in Bishops)
        //        if (b != bishop && b.Select)
        //            b.SelectFigure(null, null);

        //    // Снятие выделения со всех пешек
        //    foreach (Pawn p in Pawns)
        //        if (p.Select)
        //            p.SelectFigure(null, null);
        //}
    }
}
