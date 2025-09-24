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
        public static MainWindow init;
        public List<Pawn> Pawns = new List<Pawn>();
        public MainWindow()
        {
            InitializeComponent();   
            init = this;
    }

        private void SelectTile(object sender, MouseButtonEventArgs e)
        {

        }
        public void OnSelect(Pawn pawn)
        {
            // Снятие выделения с других пешек
            foreach (Pawn p in Pawns)
                if (p != pawn && p.Select)
                    p.SelectFigure(null, null);

            
        }

    }
}
