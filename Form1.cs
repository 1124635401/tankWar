using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _06_tankWar
{
    public partial class Form1 : Form
    {
        private Thread t;
        private static Graphics windowG;
        private static Bitmap tempBmp;
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            //阻塞

            windowG = this.CreateGraphics();


            tempBmp = new Bitmap(675,675);
            Graphics bmpG = Graphics.FromImage(tempBmp);
            GameFramework.g = bmpG;


            t= new Thread(new ThreadStart(GameMainThread));//创建线程
            t.Start();//启动线程
        }


        private static void GameMainThread()
        {
           //GameFramework

            GameFramework.Start();

            int sleepTime = 1000 / 60;//16ms

            //60
            while (true)
            {
                GameFramework.g.Clear(Color.Black);

                GameFramework.Update();// 60

                windowG.DrawImage(tempBmp, 0, 0);

                Thread.Sleep(sleepTime);
              
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }



        private void Form1_KeyUp(object sender, KeyEventArgs args)
        {
            GameObjectManager.KeyUp(args);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs args)
        {
            //if(e.KeyCode = Keys.W)
            GameObjectManager.KeyDown(args);
        }
    }
}
