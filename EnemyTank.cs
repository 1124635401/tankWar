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
    class EnemyTank:Movething
    {
        public int ChangeDirectionSpeed { get; set; }
        public int changeDirectionCount = 0;
        public int AttackSpeed { get; set; }
        public int attackCount = 0;
        private static Random r = new Random();
        public EnemyTank(int x, int y, int speed,Bitmap bmpdown,Bitmap bmpUp,Bitmap bmpRight,Bitmap bmpLeft)
        {
            //X为属性，x为参数
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            
            BitmapUp = bmpUp;
            BitmapDown = bmpdown;
            BitmapLeft = bmpRight;
            BitmapRight = bmpLeft;
            this.Dir = Direction.Down;
            AttackSpeed = 60;
            ChangeDirectionSpeed = 70;
        }

        public override void Update()
        {
            MoveCheck();
            Move();
            AttackCheck();
            AutoChangeDirection();

            base.Update();
        }

        public void MoveCheck()
        {
            //检查有没有超出边界
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    ChangeDirection() ; return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    ChangeDirection(); return;
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
                ChangeDirection(); return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                ChangeDirection(); return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                ChangeDirection(); return;
            }
        }
        private void AutoChangeDirection()
        {
            changeDirectionCount++;
            if (changeDirectionCount < ChangeDirectionSpeed) return;
            ChangeDirection();
            changeDirectionCount = 0;
        }

        private void ChangeDirection()
        {
            //随机改变方向
            //Random r = new Random();// 种子 这样写会有问题，因为太快了，每次都是一样的 
            //算法 伪随机数 
            while (true)
            {
                Direction dir = (Direction)r.Next(0, 4);
                if(dir == Dir)
                {
                    continue;
                }
                {
                    Dir = dir;break;
                }
            }
            MoveCheck();
            //0 1 2 3
        }

        private void Move()
        {

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

        private void AttackCheck()
        {
            attackCount++;
            if (attackCount < AttackSpeed) return;
          
            Attack();
            attackCount = 0;
           
        }

        private void Attack()
        {
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

            GameObjectManager.CreateBullet(x, y, Tag.EnemyTank, Dir);
        }
    }
}
