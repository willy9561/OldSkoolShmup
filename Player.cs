using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using org.flixel;

namespace OldSkoolShmup
{
    public class Player : FlxSprite
    {
        private static string ImgShip = "OldSkoolShmup;component/assets/littleship.png";

        
        private FlxArray<Bullet> bullets;
        private int bulletIndex;
        private FlxSprite m_bullet;
        public explode m_explode;

        public Player(int X, int Y, FlxArray<Bullet> Bullets) : base(ImgShip, X, Y, false, false, 0, 0, 0xff000000,false)
        {
            bullets = Bullets;
            bulletIndex = 0;
            
            width = 20;
            height = 20;

            
        }
        public override void update()
        {
            velocity.X = 0;


            if (FlxG.keys.LEFT)
            {
                velocity.X -= 100;
            }
            if (FlxG.keys.RIGHT)
            {
                velocity.X += 100;
            }

            //right bound
            if (x > (FlxG.width - width - 4))
                x = FlxG.width - width - 4; 
            
            //left bound
            if (x < 4)
                x = 4;

            if (FlxG.keys.justPressed("SPACE"))
            {
                m_bullet = bullets[bulletIndex];
                m_bullet.reset(x + ((width / 2) - m_bullet.width), y);
                m_bullet.velocity.Y = -150;
                bulletIndex++;
                if (bulletIndex >= bullets.length)
                    bulletIndex = 0;

            }


            base.update();
        }

        public override void hurt(double Damage)
        {
            base.hurt(Damage);
        }

        public override void kill()
        {
            if (dead)
                return;

            exists = true;
            m_explode = new explode((int)this.x, (int)this.y);
            FlxG.state.add(m_explode);
            m_explode.play("dead");
            base.kill();
        }
    }
}
