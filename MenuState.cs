using org.flixel;

namespace OldSkoolShmup
{
    public class MenuState : FlxState
    {
        FlxText txt;
        

        public MenuState()
        {
            

            txt = new FlxText((FlxG.width / 2)- 80, FlxG.height - 80, 180, 18, "ARROWS TO MOVE", 0xffffffff, null, 16, "center", 0);
            this.add(txt);

            txt = new FlxText((FlxG.width / 2)-55, FlxG.height - 24, 110, 10, "PRESS SPACE TO START", 0xffffffff, null, 8, "center", 0);
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
            
           FlxG.switchState(typeof(PlayState));
            
        }

    }
}
