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
    public class Game : FlxGame
    {
         public Game() :
             base(320, 240, typeof(MenuState), 2, 0xff131c1b, true, 0xff729954, null, null, 0, 0)
        {

        }
    }
}
