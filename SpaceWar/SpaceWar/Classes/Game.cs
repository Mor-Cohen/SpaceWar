using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SpaceWar.Classes
{
    class Game : Update
    {
        private Ship _ship;
        private List<Enemy> _enemies;
        private Random rand;
        private Canvas _gameBoard;
        private TextBlock _live, _level, _enemy;
        private int level;
        private bool _gameRun;

        public Game(Canvas gameBoard, Control control, TextBlock levelLebel, TextBlock enemyLebel, TextBlock liveLebel)
        {
            rand = new Random();
            _gameRun = true;
            _live = liveLebel;
            _enemy = enemyLebel;
            _level = levelLebel;
            _gameBoard = gameBoard;
            _ship = new Ship(gameBoard, control);
            gameBoard.Children.Add(_ship.Avatar);
            _enemies = new List<Enemy>();
            level = 0;
        }
        public override void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _live.Text = "Live: " + _ship.Live.ToString();
            _enemy.Text = "Enemy in map: " + _enemies.Count.ToString();
            _level.Text = "Level: " + level.ToString();
            if (_gameRun)
                if (IsLevelEnd())
                {
                    for (int i = 0; i < level + 5; i++)
                        _enemies.Add(new Enemy(_gameBoard, rand.Next(-600, -10), rand.Next(50, 200)));
                    for (int i = 0; i < level + 5; i++)
                        _enemies.Add(new Enemy(_gameBoard, rand.Next(600, 1200), rand.Next(50, 200)));
                }


            if (_ship.Live == 0)
            {
                Pause();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
                dispatcherTimer.Stop();
            }
        }

        public void Resume()
        {
            _gameRun = true;
            _ship.dispatcherTimer.Start();
            foreach (var enemy in _enemies)
                enemy.dispatcherTimer.Start();
        }

        public void Pause()
        {
            _gameRun = false;
            _ship.dispatcherTimer.Stop();
            foreach (var enemy in _enemies)
                enemy.dispatcherTimer.Stop();
        }

        private bool IsLevelEnd()
        {
            for (int i = 0; i < _enemies.Count; i++)
                if (!_gameBoard.Children.Contains(_enemies[i].Avatar))
                    _enemies.Remove(_enemies[i]);

            if (_enemies.Count > 0)
                return false;
            else
            {
                level++;
                return true;
            }
        }
    }
}
