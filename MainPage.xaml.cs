using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using org.flixel;

namespace OldSkoolShmup
{
    public partial class MainPage : UserControl
    {
        protected Game game;

        public MainPage()
        {
            InitializeComponent();
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            this.LostFocus += new RoutedEventHandler(MainPage_LostFocus);
            this.GotFocus += new RoutedEventHandler(MainPage_GotFocus);
            this.KeyDown += new KeyEventHandler(MainPage_KeyDown);
            this.KeyUp += new KeyEventHandler(MainPage_KeyUp);
            this.MouseMove += new MouseEventHandler(MainPage_MouseMove);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(MainPage_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(MainPage_MouseLeftButtonUp);

            FlxG.container = this.LayoutRoot;
            game = new Game();
        }

        

        void MainPage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FlxG.mouseUp();
        }

        void MainPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FlxG.mouseDown();
        }

        void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePoint = e.GetPosition(this);
            FlxG.updateMouse((int)mousePoint.X, (int)mousePoint.Y);
        }

        void MainPage_GotFocus(object sender, RoutedEventArgs e)
        {
            FlxG.focus();
        }

        void MainPage_LostFocus(object sender, RoutedEventArgs e)
        {
            FlxG.focusLost();
        }

        void MainPage_KeyUp(object sender, KeyEventArgs e)
        {
            KeyboardEvent keyboardEvent = new KeyboardEvent();
            keyboardEvent.keyCode = (int)e.Key;
            FlxG.keyUp(keyboardEvent);
        }

        void MainPage_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardEvent keyboardEvent = new KeyboardEvent();
            keyboardEvent.keyCode = (int)e.Key;
            FlxG.keyDown(keyboardEvent);
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            FlxG.enterFrame();
            this.FlixelDisplay.Source = FlxG.frontBuffer;
        }
    }
}