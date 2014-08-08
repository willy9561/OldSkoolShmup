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
    public class Enemy : FlxSprite
    {
        //TODO: add flight patterns
        //TODO: handles all common actions
        public Enemy(string Graphic, int X, int Y, int uid)
            : base(Graphic, X, Y, false)
        {
        }

        public override void update()
        {
            base.update();
        }

        public override void hurt(double Damage)
        {
            base.hurt(Damage);
        }
        public override void kill()
        {
            base.kill();
        }

        public virtual FlxArray<Bullet> Bullets { get; set; }
        public virtual byte uid {get; set;}
        public int m_AttackMode { get; set; }
        public virtual void move() { }
        public virtual void fire() { }
        public virtual void setUpdateRequest(PlayState game) { }
    }
}
