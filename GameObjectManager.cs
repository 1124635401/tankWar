using _06_tankWar.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _06_tankWar
{
     class GameObjectManager
    {
        private static List<NotMovething> wallList = new List<NotMovething>();
        private static List<NotMovething> steelList = new List<NotMovething>();
        private static NotMovething boss ;
        private static MyTank myTank;

        //创建一个集合来管理敌人的坦克
        private static List<EnemyTank> tankList = new List<EnemyTank>();
        // 子弹集合
        private static List<Bullet> bulletList = new List<Bullet>();

        private static List<Explosion> expList = new List<Explosion>();

        //敌人出生速度
        private static int enemyBornSpeed = 60;
        //敌人出生计数器
        private static int enemyBornCount = 60;
        //声明一个数组，用来存放敌人出生点
        private static Point[] points = new Point[3];

     

        //创建一个集合来管理敌人坦克的初始化位置
        public static void Start()
        {
            points[0].X = 0; points[0].Y = 0;

            points[1].X = 7*30; points[1].Y = 0;

            points[2].X = 14*30; points[2].Y = 0;

        }

        public static void Update()
        {
            //绘制地图
            foreach (NotMovething nm in wallList)
            {
                nm.Update();
            }
            foreach (NotMovething nm in steelList)
            {
                nm.Update();
            }

            //绘制敌人的坦克
            foreach (EnemyTank tank in tankList)
            {
                tank.Update();
            }

            CheckAndDestroyBullet();
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();
            }

            foreach (Explosion exp in expList)
            {
                exp.Update();
            }

            // 检查爆炸对象是否需要销毁
            CheckAndDestroyExplosion();

            boss.Update();

            myTank.Update();

            EnemyBorn();

            // 先更新所有子弹对象
            //foreach (Bullet bullet in bulletList)
            //{
            //    bullet.Update();
            //}

        }

        private static void CheckAndDestroyBullet()
        {
            List<Bullet> needToDestroy = new List<Bullet>();
            foreach (Bullet bullet in bulletList)
            {
                if (bullet.IsDestroy == true)
                {
                    needToDestroy.Add(bullet);
                }
            }
            foreach (Bullet bullet in needToDestroy)
            {
                bulletList.Remove(bullet);
            }
        }


        //创建一个方法，用来创建爆炸
        public static void CreateExplosion(int x, int y)
        {
            Explosion exp = new Explosion(x, y);
            expList.Add(exp);
        }

        private static void CheckAndDestroyExplosion()
        {
            List<Explosion> needToDestroy = new List<Explosion>();
            foreach (Explosion exp in expList)
            {
                if (exp.IsNeedDestroy == true)
                {
                    needToDestroy.Add(exp);
                }
            }
            foreach (Explosion exp in needToDestroy)
            {
                expList.Remove(exp);
            }
        }


        //摧毁墙
        public static void DestroyWall(NotMovething wall)
        {
            wallList.Remove(wall);
        }

        public static void DestroyTnak(EnemyTank tank)
        {
            tankList.Remove(tank);
        }

        private static void EnemyBorn()
        {
            enemyBornCount++;
            if (enemyBornCount < enemyBornSpeed) return;

            //SoundManager.PlayAdd();
            //随机产生一个敌人的坦克
            Random rd = new Random();
            int index = rd.Next(0, 3);
            Point p = points[index];
            int enemyType = rd.Next(1, 5);
            switch (enemyType)
            {
                case 1:
                    CreateEnemyTank1(p.X, p.Y);
                    break;
                case 2:
                    CreateEnemyTank2(p.X, p.Y);
                    break;
                case 3:
                    CreateEnemyTank3(p.X, p.Y);
                    break;
                case 4:
                    CreateEnemyTank4(p.X, p.Y);
                    break;
            }

            enemyBornCount = 0;


        }

        private static void CreateEnemyTank1(int x,int y) 
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GrayDown, Resources.GrayUp, Resources.GrayRight, Resources.GrayLeft);
            tankList.Add(tank);
        }

        private static void CreateEnemyTank2(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GreenDown, Resources.GreenUp, Resources.GreenRight, Resources.GreenLeft);
            tankList.Add(tank);
        }

        private static void CreateEnemyTank3(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 4, Resources.QuickDown, Resources.QuickUp, Resources.QuickRight, Resources.QuickLeft);
            tankList.Add(tank);
        }

        private static void CreateEnemyTank4(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 1, Resources.SlowDown, Resources.SlowUp, Resources.SlowRight, Resources.SlowLeft);
            tankList.Add(tank);
        }



        //判断与可摧毁墙是否碰撞
        public static NotMovething IsCollidedWall(Rectangle rt)
        {
            foreach (NotMovething wall in wallList)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;
                }
            }
            return null;
        }

        //判断与钢墙是否碰撞
        public static NotMovething IsCollidedSteel(Rectangle rt)
        {
            foreach (NotMovething wall in steelList)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;
                }
            }
            return null;
        }

        //判断与boss是否碰撞
        public static bool IsCollidedBoss(Rectangle rt)
        {
            if (boss.GetRectangle().IntersectsWith(rt))
            {
                return true;
            }
            return false;
        }

        public static MyTank IsCollidedMyTank(Rectangle rt)
        {
            if (myTank.GetRectangle().IntersectsWith(rt)) return myTank;
            else return null;
        }
        public static EnemyTank IsCollidedEnemyTank(Rectangle rt)
        {
            foreach (EnemyTank tank in tankList)
            {
                if (tank.GetRectangle().IntersectsWith(rt))
                {
                    return tank;
                }
            }
            return null;
        }



        ////绘制地图
        //public static void DrawMap()
        //{
        //    foreach(NotMovething nm in wallList)
        //    {
        //        nm.DrawSelf();
        //    }
        //    foreach (NotMovething nm in steelList)
        //    {
        //        nm.DrawSelf();
        //    }


        //    boss.DrawSelf();
        //}

        //public static void DrawMyTank()
        //{
        //    myTank.DrawSelf();

        //}

        public static void CreateMyTank()
        {
            int x = 5 * 30;
            int y = 14 * 30;
             
            myTank = new MyTank(x, y, 2);
        }


        public static void CreateMap()
        {
            //上侧可摧毁墙
            CreateWall(1, 1, 5, Resources.wall, wallList);
            CreateWall(3, 1, 5, Resources.wall, wallList);
            CreateWall(5, 1, 4, Resources.wall, wallList);
            CreateWall(7, 1, 3, Resources.wall, wallList);
            CreateWall(9, 1, 4, Resources.wall, wallList);
            CreateWall(11, 1, 5, Resources.wall, wallList);
            CreateWall(13, 1, 5, Resources.wall, wallList);

            CreateWall(7, 5, 1, Resources.steel, steelList);

            CreateWall(0, 7, 1, Resources.steel, steelList);

            //中部可摧毁墙
            CreateWall(2, 7, 1, Resources.wall, wallList);
            CreateWall(3, 7, 1, Resources.wall, wallList);
            CreateWall(4, 7, 1, Resources.wall, wallList);
            //中部墙
            CreateWall(6, 7, 1, Resources.wall, wallList);
            CreateWall(7, 6, 2, Resources.wall, wallList);
            CreateWall(8, 7, 1, Resources.wall, wallList);
            //中部可摧毁墙
            CreateWall(10, 7, 1, Resources.wall, wallList);
            CreateWall(11, 7, 1, Resources.wall, wallList);
            CreateWall(12, 7, 1, Resources.wall, wallList);

            CreateWall(14, 7, 1, Resources.steel, steelList);

            //下左侧墙
            CreateWall(1, 9, 5, Resources.wall, wallList);
            CreateWall(3, 9, 5, Resources.wall, wallList);
            CreateWall(5, 9, 3, Resources.wall, wallList);

            //下侧连接墙
            CreateWall(6, 10, 1, Resources.wall, wallList);
            CreateWall(7, 10, 2, Resources.wall, wallList);
            CreateWall(8, 10, 1, Resources.wall, wallList);
            //下右侧
            CreateWall(9, 9, 3, Resources.wall, wallList);
            CreateWall(11, 9, 5, Resources.wall, wallList);
            CreateWall(13, 9, 5, Resources.wall, wallList);
            
            //boss
            CreateWall(6, 13, 2, Resources.wall, wallList);
            CreateWall(7, 13, 1, Resources.wall, wallList);
            CreateWall(8, 13, 2, Resources.wall, wallList);

            CreateBoss(7, 14, Resources.Boss);

        }

        private static void CreateBoss(int x,int y,Image img)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            boss = new NotMovething(xPosition, yPosition, img);

        }

        private static void CreateWall(int x,int y,int count, Image img, List<NotMovething>wallList)
        {
            //List<NotMovething> wallList = new List<NotMovething>();
            int xPosition = x*30;
            int yPosition = y*30;
            for(int i = yPosition; i < yPosition + count * 30; i += 15)
            {
                NotMovething wall1 = new NotMovething(xPosition, i, img);
                NotMovething wall2 = new NotMovething(xPosition + 15, i, img);
                wallList.Add(wall1);
                wallList.Add(wall2);
            }
       
        }


        public static void CreateBullet(int x, int y, Tag tag, Direction dir)
        {
            Bullet bullet = new Bullet(x, y, 8, dir, tag);

            bulletList.Add(bullet);

        }

        //public static void DestroyBUllet(Bullet bullet)
        //{
        //    bulletList.Remove(bullet);
        //}



        public static void KeyDown(KeyEventArgs args)
        {
            myTank.KeyDown(args);
        }

        public static void KeyUp(KeyEventArgs args)
        {
            myTank.KeyUp(args);
        }

    }
}
