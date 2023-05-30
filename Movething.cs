using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _06_tankWar
{
    enum Direction
    {
        Up=0,
        Down=1,
        Left=2,
        Right=3
    }

    class Movething:GameObject
    {
        //定义一个锁
        private Object _lock = new object();
        public Bitmap BitmapUp { get; set; }
        public Bitmap BitmapDown { get; set; }
        public Bitmap BitmapLeft { get; set; }
        public Bitmap BitmapRight { get; set; }

        //public bool IsMoving { get; set; }
        public int Speed { get; set; }

        private Direction dir;

        public Direction Dir { get { return dir; }
            set { 
                dir = value;
                //根据方向，设置宽高
                Bitmap bmp = null;
           
                switch (dir)
                {
                    case Direction.Up:
                        bmp = BitmapUp;
                        break;
                    case Direction.Down:
                        bmp = BitmapDown;
                        break;
                    case Direction.Left:
                        bmp = BitmapLeft;
                        break;
                    case Direction.Right:
                        bmp = BitmapRight;
                        break;
                }
                lock (_lock)
                {
                    //如果bmp不为空，就设置宽高
                    if (bmp != null)
                    {
                        Width = bmp.Width;
                        Height = bmp.Height;
                    }
                }
          
                
            }
        }

        //public bool IsMoving { get; set; }

        protected override Image GetImage()
        {
            Bitmap bitmap = null;

            switch (Dir)
            {
                case Direction.Up:
                    bitmap= BitmapUp;
                    break;
                case Direction.Down:
                    bitmap= BitmapDown;
                    break;
                case Direction.Left:
                    bitmap= BitmapLeft;
                    break;
                case Direction.Right:
                   bitmap = BitmapRight;
                    break;
              
            }
            // 透明
            bitmap.MakeTransparent(Color.Black);

            //BitmapUp只是在Direction.Up的情况下使用 在其他情况下，BitmapUp没有被赋值给bitmap。
            //因此，如果你始终返回BitmapUp，则无论Dir的值如何，都会返回相同的位图对象。
            //renturn BitmapUp;

            return bitmap;
        }

        public override void DrawSelf()
        {
            lock (_lock)
            {
                base.DrawSelf();
            }

        }

    }
}
