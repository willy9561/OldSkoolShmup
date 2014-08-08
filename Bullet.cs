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
    public class Bullet : FlxSprite
    {
        private static string ImgBullet = "OldSkoolShmup;component/assets/bullet.png";
        private static string SndHit = "OldSkoolShmup;component/assets/jump.mp3";
        private static string SndShoot = "OldSkoolShmup;component/assets/shoot.mp3";

        public Bullet() : base(ImgBullet, 0, 0, true)
        {
            width = 6;
            height = 6;
            offset.X = 1;
            offset.Y = 1;
            exists = false;
        }

        public override void update()
        {
            if (dead && finished) exists = false;
            else base.update();
        }
        public override void render()
        {
            base.render();
        }
        override public bool hitWall(FlxCore Contact) { hurt(0); return true; }
        override public bool hitFloor(FlxCore Contact) { hurt(0); return true; }
        override public bool hitCeiling(FlxCore Contact) { hurt(0); return true; }
        override public void hurt(double Damage)
        {
            if (dead) return;
            velocity.X = 0;
            velocity.Y = 0;
            //if (onScreen()) FlxG.play(SndHit);
            dead = true;
            //play("poof");
            //shd be set when animation complete
            finished = true;
        }
    }
}
