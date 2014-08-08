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
    public class explode : FlxSprite
    {
        private static string ImgExp = "OldSkoolShmup;component/assets/explosion.png";

 
        public explode(int X, int Y)
            : base(ImgExp, X, Y, true)
        {
            height = 30;
            width = 30;
            offset.X = 1;
            offset.Y = 1;
            

             addAnimation("dead", new int[] {0,1,2,3}, 50, false);
             
        }

        public override void update()
        {
            if (finished) exists = false;
            else base.update();
        }

        override public void hurt(double Damage)
        {
            base.hurt(Damage);
        }
        public override void kill()
        {
            base.kill();
        }
        public override void render()
        {
            base.render();
        }

        
    }
}
