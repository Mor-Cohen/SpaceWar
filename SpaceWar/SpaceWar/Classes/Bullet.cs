using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;

namespace SpaceWar.Classes
{
    class Bullet:Update
    {
        public Rectangle Avatar { get; private set; }
        private Canvas _gameBoard;
        public Bullet(double x,double y,Canvas gameBoard)
        {
            ImageBrush bullet = new ImageBrush();
            bullet.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/bullet.png"));
            Avatar = new Rectangle
            {
                Tag = "bullet",
                Height = 50,
                Width = 40,
                Fill = bullet,

            };
            _gameBoard = gameBoard;
            Canvas.SetLeft(Avatar, x);
            Canvas.SetTop(Avatar, y);
        }
        public override void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Canvas.SetTop(Avatar, Canvas.GetTop(Avatar) - 10);
            //RotateTransform rotate = Avatar.RenderTransform as RotateTransform;
            //if (rotate != null)
            //{
            //    if (rotate.Angle > 0)
            //        Canvas.SetLeft(Avatar, Canvas.GetLeft(Avatar) + 3);
            //    else
            //        Canvas.SetLeft(Avatar, Canvas.GetLeft(Avatar) - 3);
            //}






            if (Canvas.GetTop(Avatar) < 0)
                RemoveBullet();

            if (!_gameBoard.Children.Contains(Avatar))
                RemoveBullet();
                     
        }

        private void RemoveBullet()
        {
            _gameBoard.Children.Remove(Avatar);
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            dispatcherTimer.Stop();
        }

    }
}
