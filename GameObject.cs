using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_tankWar
{
    abstract class GameObject
    {

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        //private int x;
        //public int X{ get { return x; } set{value = x;}}

        protected abstract Image GetImage();


        public virtual void  DrawSelf()
        {
            //画自己

            Graphics g= GameFramework.g;

            g.DrawImage(GetImage(),X,Y);
        }

        public virtual void Update()
        {
            //更新自己
            DrawSelf();
        }

        public Rectangle GetRectangle()
        {
            Rectangle rectangle = new Rectangle(X, Y, Width, Height);
            return rectangle;
        }


    }
}
