using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace SpaceWar.Classes
{
    class Ship : Update
    {
        public Rectangle Avatar { get; private set; }
        public int Speed { get; set; }
        public int Live { get; set; }
        public int Level { get; set; }
        private bool _left, _right, _up, _down;
        private Control _control;
        private Canvas _gameBoard;
        public Ship(Canvas gameBoard,Control control)
        {
            ImageBrush img = new ImageBrush();
            img.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/roketship3.png"));
            Avatar = new Rectangle
            {
                Height = 80,
                Width = 100,
                Fill = img
            };
            Speed = 20;
            Live = 3;
            Level = 1;
            Canvas.SetLeft(Avatar, 350);
            Canvas.SetTop(Avatar, 450);
            _control = control;
            _control.KeyUp += KeyPressUp;
            _control.KeyDown += KeyPressDown;
            _gameBoard = gameBoard;
        }

        public override void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (_left && Canvas.GetLeft(Avatar) > 0)
                Canvas.SetLeft(Avatar, Canvas.GetLeft(Avatar) - Speed);

            if (_right && Canvas.GetLeft(Avatar) < 700)
                Canvas.SetLeft(Avatar, Canvas.GetLeft(Avatar) + Speed);

            if (_up && Canvas.GetTop(Avatar) > 0)
                Canvas.SetTop(Avatar, Canvas.GetTop(Avatar) - Speed);

            if (_down && Canvas.GetTop(Avatar) < 500)
                Canvas.SetTop(Avatar, Canvas.GetTop(Avatar) + Speed);

            if (Hit())
                Live -= 1;


        }

        private bool Hit()
        {
            Rect shipHitBox = new Rect(Canvas.GetLeft(Avatar), Canvas.GetTop(Avatar), Avatar.Width, Avatar.Height);
            foreach (var item in _gameBoard.Children.OfType<Rectangle>())
            {
                if ((string)item.Tag == "enemy" || (string)item.Tag == "enemyAttack")
                {
                    Rect enemyHitBox = new Rect(Canvas.GetLeft(item), Canvas.GetTop(item), item.Width, item.Height);
                    if (enemyHitBox.IntersectsWith(shipHitBox))
                    {
                        item.Width = item.Width + 5;
                        return true;
                    }
                }
            }
            return false;
        }

        private void Attack()
        {
            var bullet3 = new Bullet(Canvas.GetLeft(Avatar) + Avatar.Width / 3, Canvas.GetTop(Avatar) - Avatar.Height / 2, _gameBoard);
            _gameBoard.Children.Add(bullet3.Avatar);
            //var bullet = new Bullet(Canvas.GetLeft(Avatar) + Avatar.Width /3 + 10, Canvas.GetTop(Avatar) - Avatar.Height /2, _gameBoard);
            //bullet.Avatar.RenderTransform = new RotateTransform(15);
            //var bullet1 = new Bullet(Canvas.GetLeft(Avatar) + Avatar.Width / 3 - 10, Canvas.GetTop(Avatar) - Avatar.Height / 2 +7, _gameBoard);
            //bullet1.Avatar.RenderTransform = new RotateTransform(-15);
            //_gameBoard.Children.Add(bullet.Avatar);
            //_gameBoard.Children.Add(bullet1.Avatar);

        }

        private void KeyPressDown(object sender, KeyEventArgs e)
        {
            Move(e,true);

            if (e.Key == Key.Space)
                Attack();
        }

        private void KeyPressUp(object sender, KeyEventArgs e)
        {
            Move(e,false);
        }

        private void Move(KeyEventArgs e, bool isMove)
        {
            if (e.Key == Key.Left)
                _left = isMove;

            if (e.Key == Key.Right)
                _right = isMove;

            if (e.Key == Key.Up)
                _up = isMove;

            if (e.Key == Key.Down)
                _down = isMove;
        }

    }
}
