using _06_tankWar.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _06_tankWar
{
    class Explosion : GameObject
    {
        private int playSpeed = 2;
        private int playCount = 0;

        private int index = 0;

        private Bitmap[] bmpArry = new Bitmap[]
        {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
            Resources.EXP4,
            Resources.EXP5,
        };

        public bool IsDestroy { get; private set; }
        public bool IsNeedDestroy { get; private set; }

        public Explosion(int x, int y)
        {
            foreach (Bitmap bmp in bmpArry)
            {
                bmp.MakeTransparent(Color.Black);
            }
            this.X = x - bmpArry[0].Width/2;
            this.Y = y - bmpArry[0].Height/2;
            this.Width = 30;
            this.Height = 30;
        }



        protected override Image GetImage()
        {
            //throw new NotImplementedException();
            //return null;
            if (index>4) return bmpArry[4];
            return bmpArry[index];
        }

        public override void Update()
        {
            playCount++;
            index = (playCount - 1) / playSpeed;
            if (index > 4)
            {
                IsNeedDestroy = true;
            }

            base.Update();
        }

    }


}

