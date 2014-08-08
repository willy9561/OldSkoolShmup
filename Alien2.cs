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
    public class Alien2 : Enemy
    {
        private static string ImgShip = "OldSkoolShmup;component/assets/Alienship1.png";
        

        public FlxArray<Bullet> bullets;
        private int bulletIndex;
        private FlxSprite m_bullet;
        private bool m_firing = false;
        
        private byte m_uid;
        private int m_halfwidth;
        private int m_halfheight;
        private int m_step;
        private int m_stage;
        private int m_pattern;

        private double _mMoveTimer;
        private double _mFireTimer;
        private bool _mMoveTimerExpired;
        private bool _mFireTimerExpired;

        private PlayState _mgame;

        public Alien2(int X, int Y, int uid, int Stage, int Pattern, FlxArray<Bullet> Bullets)
            : base(ImgShip, X, Y, uid)
        {
            bullets = Bullets;
            bulletIndex = 0;
            m_uid = (byte)uid;
            m_stage = Stage;
            m_pattern = Pattern;


            m_halfwidth = width / 2;
            m_halfheight = height / 2;
            maxVelocity.Y = 200;
            maxVelocity.X = 200;

            _mMoveTimerExpired = false;
            _mFireTimerExpired = false;

            _mMoveTimer = 1.5;
            _mFireTimer = 3 + FlxG.random() * 10;
            m_firing = false;



        }
        private void fly_ToPlayer(FlxCore player)
		{
			double dx = player.x - x;
			double dy = player.y - y;
			// get radians and degrees
			double aRadians = Math.Atan2(dy,dx);
			double aDegrees = 360*(aRadians/(2*Math.PI));
			
			// point at player
			angle = aDegrees;
			
			if ( y < 100 )
			{
				velocity.X = Math.Cos(aRadians) * 100;
				velocity.Y = Math.Sin(aRadians) * 100;
			}else {
				if ( dx > 1 )
				{
					velocity.X += 30;
				} else {
					velocity.X -= 30;
				}
				velocity.Y += 30;
			}
			
		}

        public override void update()
        {
            //wrap the aliens around the screen
            if (x < -_bw + m_halfwidth)
                last.X = x = FlxG.width + m_halfwidth;
            else if (x > FlxG.width + m_halfwidth)
                last.X = x = -_bw + m_halfwidth;
            if (y < -_bh + m_halfheight)
                last.Y = y - FlxG.height + m_halfheight;
            else if (y > FlxG.height + m_halfheight)
                last.Y = y = -_bh + m_halfheight;

            //start shooting
            if (m_firing == true)
            {
                m_bullet = bullets[bulletIndex];
                m_bullet.reset(x + ((width / 2) - m_bullet.width), y);
                m_bullet.velocity.Y = 150;
                bulletIndex++;
                if (bulletIndex >= bullets.length)
                    bulletIndex = 0;

                m_firing = false;

            }


            //Move timer check
            if (_mMoveTimer <= 0.0)
            {

                if (!_mMoveTimerExpired)
                    setMoveRequest();

            }
            else _mMoveTimer -= FlxG.elapsed;

            //Fire timer check
            if (_mFireTimer <= 0.0)
            {

                if (!_mFireTimerExpired)
                    setFireRequest();

            }
            else _mFireTimer -= FlxG.elapsed;



            if (velocity.X > maxVelocity.X)
                velocity.X = maxVelocity.X;

            if (velocity.Y > maxVelocity.Y)
                velocity.Y = maxVelocity.Y;

            setUpdateRequest(_mgame);

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
            explode m_explode = new explode((int)this.x, (int)this.y);
            FlxG.state.add(m_explode);
            m_explode.play("dead");
            base.kill();
        }

        public override FlxArray<Bullet> Bullets { get { return bullets; } }

        public override byte uid { get { return m_uid; } }

        public override void move()
        {

            _mMoveTimer = 1 + FlxG.random() * 10;

            _mMoveTimerExpired = false;

            if (m_uid == m_AttackMode)
            {
                m_step++;

                if (m_step < 5)
                {
                    if (m_pattern == 1)
                    {
                        velocity.Y += 2;
                        velocity.X += 1;
                    }
                    else
                    {
                        velocity.Y += 2;
                        velocity.X -= 1;
                    }
                }
                else if (m_step < 15)
                {
                    if (m_pattern == 1)
                    {
                        velocity.Y += 1;
                        velocity.X += 1;
                    }
                    else
                    {
                        velocity.Y += 1;
                        velocity.X -= 1;
                    }
                }
                else if (m_step < 20)
                {
                    if (m_pattern == 1)
                    {
                        velocity.Y -= 1;
                        velocity.X -= 2;
                    }
                    else
                    {
                        velocity.Y -= 1;
                        velocity.X += 2;
                    }
                }
                else
                {
                    m_step = 1;
                }


            }
        }

        public void setMoveRequest()
        {
            _mgame.enemyMove(uid);
            _mMoveTimerExpired = true;
        }
        public override void fire()
        {
            _mFireTimer = 6 + FlxG.random() * 10;
            _mFireTimerExpired = false;
            m_firing = true;
        }
        public void setFireRequest()
        {
            _mgame.enemyFire(uid);
            _mFireTimerExpired = true;
        }
        // public void StrategicUpdate(BoyLib::Vector2 WayPnt)
        // {
        //  _mWayPoint = WayPnt;
        //  _mStratTimer = 1.0;
        //  _mStratTimerExpired = false;
        // }
        public void setStrategicUpdateRequest()
        {
            // _mgame.enemyStrat(uid);
            // _mStratTimerExpired = true;
        }
        public override void setUpdateRequest(PlayState game)
        {
            _mgame = game;
            _mgame.enemyUpdate(uid);

        }
    }
}
