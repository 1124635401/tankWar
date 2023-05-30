using _06_tankWar.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_tankWar
{
    enum Tag
    {
        MyTank,
        EnemyTank
    }

    class Bullet : Movething
    {
        public Tag Tag { get; set; }
        public bool IsDestroy { get; set; }
        public Bullet(int x, int y, int speed, Direction dir, Tag tag)
        {
            IsDestroy = false;
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapUp = Resources.BulletUp;
            BitmapDown = Resources.BulletDown;
            BitmapLeft = Resources.BulletLeft;
            BitmapRight = Resources.BulletRight;

            this.Dir = dir;
            this.Tag = tag;

            this.X -= Width / 2;
            this.Y -= Height / 2;
        }

        public override void DrawSelf()
        {
            base.DrawSelf();
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
                if (Y + Height / 2 + 3 < 0)
                {
                    IsDestroy = true; return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Height / 2 - 3 > 450)
                {
                    IsDestroy = true; return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X + Width / 2 - 3 < 0)
                {
                    IsDestroy = true; return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Width / 2 + 3 > 450)
                {
                    IsDestroy = true; return;
                }
            }



            //检查有没有和其它元素碰撞
            //要先拿到未来下一步的位置

            //当前位置
            Rectangle rect = GetRectangle();

            rect.X = X + Width / 2 - 3;
            rect.Y = Y + Height / 2 - 3;
            rect.Width = 3;
            rect.Height = 3;

            //1.墙 2.钢墙 3.敌人坦克 4.我方坦克

            int xExplosion = this.X + Width / 2;
            int yExplosion = this.Y + Height / 2;
            NotMovething wall = null;
            
            if ((wall = GameObjectManager.IsCollidedWall(rect)) != null)//先赋值 再判断
            {
                IsDestroy = true; 
                GameObjectManager.DestroyWall(wall);
                GameObjectManager.CreateExplosion(xExplosion,yExplosion);
                SoundManager.PlayBlastAsync();
                return; 
            }

            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                IsDestroy = true; ; return;
            }

            if(GameObjectManager.IsCollidedBoss(rect))
            {
                GameFramework.ChangeToGameOver();
                SoundManager.PlayBlastAsync();
                return;
            }

            if (Tag ==Tag.MyTank)
            {
                EnemyTank tank = null;
                if ((tank = GameObjectManager.IsCollidedEnemyTank(rect)) != null)
                {
                    IsDestroy = true; 
                    GameObjectManager.DestroyTnak(tank);
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    SoundManager.PlayHitAsync();
                    return;
                   
                }
            }else if(Tag==Tag.EnemyTank)
            {
                MyTank myTank = null;
                if ((myTank = GameObjectManager.IsCollidedMyTank(rect))!= null)
                {
                    IsDestroy = true;
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    myTank.TankDamage();
                    SoundManager.PlayBlastAsync();
                    return;
                }
            }




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
    }
}
