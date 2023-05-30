using _06_tankWar.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _06_tankWar
{
    class MyTank:Movething
    {
        public int Hp{ get; set; }

        public bool IsMoving { get; set; }
        private int originX;
        private int originY;
        public MyTank(int x,int y,int speed)
        {
            IsMoving = false;
            //X为属性，x为参数
            this.X = x;
            this.Y = y;
            originX = x;
            originY = y;
            this.Speed = speed;
            this.Dir = Direction.Up;
            BitmapUp = Resources.MyTankUp;
            BitmapDown = Resources.MyTankDown;
            BitmapLeft = Resources.MyTankLeft;
            BitmapRight = Resources.MyTankRight;
            Hp = 4;
        }

        public override void Update()
        {
            MoveCheck();
            Move();
            base.Update();
        }

        public void MoveCheck()
        {
            //检查有没有超出边界
            if (Dir == Direction.Up)
            {
                if (Y-Speed <0)
                {
                    IsMoving = false;return;
                }
            }else if (Dir == Direction.Down)
            {
                if (Y+Speed+Height>450)
                {
                    IsMoving = false;return;
                }
            }else if (Dir == Direction.Left)
            {
                if (X-Speed<0)
                {
                    IsMoving = false;return;
                }
            }else if (Dir == Direction.Right)
            {
                if (X+Speed+Width>450)
                {
                    IsMoving = false;return;
                }
            }


            //检查有没有和其它元素碰撞
            //要先拿到未来下一步的位置

            //当前位置
            Rectangle rect = GetRectangle();
            switch (Dir)
            {
                case Direction.Up:
                    rect.Y -= Speed;
                    break;
                case Direction.Down:
                    rect.Y += Speed;
                    break;
                case Direction.Left:
                    rect.X -= Speed;
                    break;
                case Direction.Right:
                    rect.X += Speed;
                    break;
            }

            if (GameObjectManager.IsCollidedWall(rect) != null)
            {
                IsMoving = false;return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                IsMoving = false; return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                IsMoving = false; return;
            }
        }


        private void Move()
        {
            if (IsMoving == false) return;

            switch (Dir)
            {
                  case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
            }

            
        }

        //GameMainThread 里面调用
        //加锁，防止多线程同时调用
        public void KeyDown(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    Dir = Direction.Up;
                    //if (Dir != Direction.Up)
                    //{
                    //    Dir = Direction.Up;
                    //}
                    IsMoving = true;
                    break;
                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = true;
                    break;
                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    break;
                case Keys.Space:
                    //发射子弹
                    Attack();
                    break;


            }
        }


        private  void Attack()
        {

            SoundManager.PlayFireAsync();
            int x = this.X;
            int y = this.Y;
            switch (Dir)
            {
                case Direction.Up:
                    x = x + Width / 2;
                    break; 
                case Direction.Down:
                    x = x + Width / 2;
                    y += Height;
                    break;
                case Direction.Left:
                    y = y + Height / 2;
                    break;
                case Direction.Right:
                    x += Width;
                    y = y + Height / 2;
                    break;
            }

            GameObjectManager.CreateBullet(x,y,Tag.MyTank,Dir);
        }

        public void KeyUp(KeyEventArgs args)
        {

            switch (args.KeyCode)
            {
                case Keys.W:
                    IsMoving = false;
                    break;
                case Keys.S:
                    IsMoving = false;
                    break;
                case Keys.A:
                    IsMoving = false;
                    break;
                case Keys.D:
                    IsMoving = false;
                    break;

            }

        }

        public void TankDamage()
        {
            Hp--;
            if (Hp <= 0)
            {
                X = originX;
                Y = originY;
                Hp = 4;
            }
        }

    }
}
