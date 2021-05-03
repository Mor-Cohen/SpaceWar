using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Packaging;
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
using SpaceWar.Classes;
using System.Windows.Threading;

namespace SpaceWar.views
{
    /// <summary>
    /// Interaction logic for Play.xaml
    /// </summary>
    public partial class Play : UserControl
    {
        private Game game;
        public Play()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Hidden)
            {
                if (game != null)
                {
                    game.Pause();
                    resumeWindow.Visibility = Visibility.Visible;
                }
                
            }       
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            playWindow.Visibility = Visibility.Hidden;
            game = new Game(gameBoard, this, levelText, enemeyText, liveText);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            resumeWindow.Visibility = Visibility.Hidden;
            game.Resume();
        }
    }
}
