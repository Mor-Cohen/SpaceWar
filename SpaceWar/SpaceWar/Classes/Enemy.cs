using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace SpaceWar.Classes
{
    class Enemy : Update
    {
        private DispatcherTimer expoldeTimer;
        public Rectangle Avatar { get; private set; }
        public int Speed { get; set; }
        public int Live { get; set; }
        private int dir = 1;
        private Canvas _gameBoard;
        private static Random rand;
        public Enemy(Canvas gameBoard, double x, double y)
        {
            ImageBrush img = new ImageBrush();
            img.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/enemy.png"));
            Avatar = new Rectangle
            {
                Tag = "enemy",
                Height = 80,
                Width = 80,
                Fill = img
            };
            rand = new Random();
            Speed = 5;
            Live = 1;
            _gameBoard = gameBoard;
            Canvas.SetLeft(Avatar, x);
            Canvas.SetTop(Avatar, y);
            Avatar.Visibility = Visibility.Hidden;
            _gameBoard.Children.Add(Avatar);
            
        }

        public override void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var left = Canvas.GetLeft(Avatar);
            var top = Canvas.GetTop(Avatar);


            if (left > 700)
                dir = 0;
            else
                Avatar.Visibility = Visibility.Visible;


            if (left < 0)
                dir = 1;

            if (dir == 0)
                Canvas.SetLeft(Avatar, left - Speed);

            if (dir == 1)
                Canvas.SetLeft(Avatar, left + Speed);

            if (rand.Next(1, 1000) == 2 && left > 0 && left < 700)
            {
                EnemyAttack enemyAttack = new EnemyAttack(_gameBoard, left, top);
                _gameBoard.Children.Add(enemyAttack.Avatar);
            }

            if (Hit() || Avatar.Width < 80)
                Explode();
        }

        private bool Hit()
        {
            Rect enemyHitBox = new Rect(Canvas.GetLeft(Avatar), Canvas.GetTop(Avatar), Avatar.Width, Avatar.Height);
            foreach (var item in _gameBoard.Children.OfType<Rectangle>())
            {
                if ((string)item.Tag == "bullet")
                {
                    Rect bulletHitBox = new Rect(Canvas.GetLeft(item), Canvas.GetTop(item), item.Width, item.Height);
                    if (bulletHitBox.IntersectsWith(enemyHitBox))
                    {
                        _gameBoard.Children.Remove(item);
                        return true;
                    }
                }
            }
            return false;
        }

        private void Attack()
        {
            ImageBrush img = new ImageBrush();
            img.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/enemy.png"));
            Rectangle attack = new Rectangle
            {
                Width = 30,
                Height = 30,
                Tag = "enemyAttack",
                Fill = img
            };
        }


        public void Destroy(object sender, EventArgs e)
        {
            _gameBoard.Children.Remove(Avatar);
            expoldeTimer.Tick -= Destroy;
            expoldeTimer.Stop();

        }

        private void Explode()
        {
            expoldeTimer = new DispatcherTimer();
            expoldeTimer.Interval = TimeSpan.FromSeconds(0.5);
            expoldeTimer.Tick += Destroy;
            expoldeTimer.Start();
            ImageBrush img = new ImageBrush();
            img.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/boom.png"));
            Avatar.Fill = img;
            Avatar.Tag = "explode";
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            dispatcherTimer.Stop();
        }

    }
}
