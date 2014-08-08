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
    public class Credits : FlxState
    {
        public Credits()
        {
            FlxText txt;
            txt = new FlxText(20, (FlxG.width / 2) - 80, 170, 20, "ART BY JAKMAN4242", 0xffffffff, null, 16, "center", 0);
            this.add(txt);

            txt = new FlxText(20, (FlxG.width / 2) - 60, 120, 20, "AND EASYNAME", 0xffffffff, null, 16, "center", 0);
            this.add(txt);
        }

        public override void update()
        {
            if (FlxG.keys.pressed("SPACE"))
            {
                FlxG.flash(0xffffffff, 0.75, null, false);
                FlxG.fade(0xff000000, 1, OnFade, false);
            }
            base.update();
        }

        public void OnFade()
        {
            FlxG.switchState(typeof(MenuState));
        }
    }
}
