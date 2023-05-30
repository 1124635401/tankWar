using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_tankWar
{
    class NotMovething:GameObject
    {
        private Image img;
        //给img属性赋值的时候，同时给宽高赋值
        public Image Img { get { return img; } 
            set { 
                //给img赋值  
                img = value;
                Width = img.Width;
                Height = img.Height;
            }
        }

        protected override Image GetImage()
        {
            return Img;
        }

        public NotMovething(int x,int y,Image img)
        {
            this.X = x;
            this.Y = y; 
            this.Img = img;
        }
    }
}
