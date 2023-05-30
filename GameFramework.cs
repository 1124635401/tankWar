using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _06_tankWar
{
    enum GameState
    {
        Running,
        GameOver
    }
    class GameFramework
    {
        public static Graphics g;
        private static GameState gameState = GameState.Running;

        public static void Start()
        {
            SoundManager.InitSound();
            GameObjectManager.Start();
            GameObjectManager.CreateMap();
            GameObjectManager.CreateMyTank();
            SoundManager.PlayStartAsync();

        }

        public static void Update()
        {
            //更新游戏中的所有对象 fps
            //GameObjectManager.DrawMap();
            //GameObjectManager.DrawMyTank();
            if (gameState==GameState.Running)
            {
                GameObjectManager.Update();
            }else if (gameState==GameState.GameOver)
            {
                GameOverUpdate();
            }
        }

        public static void GameOverUpdate()
        {
            Bitmap bmp = Properties.Resources.GameOver;
            bmp.MakeTransparent(Color.Black);
            int x = 450 / 2 - Properties.Resources.GameOver.Width / 2;
            int y = 450 / 2 - Properties.Resources.GameOver.Height / 2;
            g.DrawImage(Properties.Resources.GameOver,x,y);
        }
        public static void ChangeToGameOver()
        {
            gameState = GameState.GameOver;
        }

        //public static void KeyDown(KeyEventArgs args)
        //{
        //    GameObjectManager.KeyDown(args);
        //}
        //public static void KeyUp(KeyEventArgs args)
        //{
        //    GameObjectManager.KeyUp(args);
        //}
    }
}
