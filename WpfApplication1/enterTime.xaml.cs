using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for enterTime.xaml
    /// </summary>
    public partial class enterTime : Window
    {
        int minutes;
        int seconds;
        int milli;
        bool minutesCheck = false;
        bool secondsCheck = false;
        bool milliCheck = false;
        bool twenty = false;
        bool animdone = true;
        bool animation;
        bool check;
        public enterTime()
        {
            InitializeComponent();
            minutesBox.FontSize = 16;
            secondsBox.FontSize = 16;
            milliBox.FontSize = 16;
            minutesBox.Focus();
            goButton.IsEnabled = false;
            messageLabel.Opacity = 0;
            milliBox.MaxLength = 1;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minutesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(minutesBox.Text, out minutes);
            if(((System.Windows.Controls.TextBox)sender).Text.Length == 2)
            {
                secondsBox.Focus();
            }
            if (minutes > 20)
            {
                minutesBox.Background = System.Windows.Media.Brushes.Red;
                if (animdone == true)
                {
                    messageLabel.Content = "Can't have more than 20 minutes.";
                    Animation(ref animation);
                }
                minutesCheck = false;
            }
            if(minutes <= 20)
            {
                minutesBox.Background = System.Windows.Media.Brushes.White;
                minutesCheck = true;
            }
            goButtonCheck(ref check);
        }
        private void Animation(ref bool animation)
        {
            if(animdone == true)
            {
                var windowlarge = new Storyboard();
                var windowsmall = new Storyboard();
                Window mainWindow = System.Windows.Application.Current.MainWindow;
                if (null == System.Windows.Application.Current.MainWindow)
                {
                    new System.Windows.Application();
                }
                //var windowlarge = new Storyboard();
                var incWindHeight = new DoubleAnimation();

                incWindHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));
                incWindHeight.From = 122;
                incWindHeight.To = 140;

                Storyboard.SetTargetProperty(incWindHeight, new PropertyPath(MainWindow.HeightProperty));

                SineEase easingFunction = new SineEase();
                easingFunction.EasingMode = EasingMode.EaseInOut;
                incWindHeight.EasingFunction = easingFunction;
                windowlarge.Children.Add(incWindHeight);
                windowlarge.Begin(this);

                DoubleAnimation da = new DoubleAnimation();
                da.From = 0;
                da.To = 1;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                da.BeginTime = TimeSpan.FromSeconds(.8);
                da.RepeatBehavior = new RepeatBehavior(1.0);
                messageLabel.BeginAnimation(OpacityProperty, da);
                var delay = new DispatcherTimer { Interval = TimeSpan.FromSeconds(6) };
                delay.Start();
                delay.Tick += (parent, args) =>
                {
                    delay.Stop();
                    messageLabel.Content = "";
                    DoubleAnimation db = new DoubleAnimation();
                    db.From = 1;
                    db.To = 0;
                    db.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                    db.BeginTime = TimeSpan.FromMilliseconds(1);
                    db.RepeatBehavior = new RepeatBehavior(1.0);
                    messageLabel.BeginAnimation(OpacityProperty, db);

                    //var windowsmall = new Storyboard();
                    var decHeight = new DoubleAnimation();

                    decHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));

                    decHeight.From = 140;
                    decHeight.To = 122;

                    Storyboard.SetTargetProperty(decHeight, new PropertyPath(MainWindow.HeightProperty));

                    easingFunction.EasingMode = EasingMode.EaseInOut;
                    decHeight.EasingFunction = easingFunction;

                    windowsmall.Children.Add(decHeight);
                    windowsmall.Completed += new EventHandler(Animation_Completed);
                    windowsmall.Begin(this);
                    animdone = false;
                };
            }
            if (animdone == false)
            {
                var delay2 = new DispatcherTimer { Interval = TimeSpan.FromSeconds(6) };
                delay2.Start();
                delay2.Tick += (parent, args) =>
                {
                    var windowlarge = new Storyboard();
                    var windowsmall = new Storyboard();
                    Window mainWindow = System.Windows.Application.Current.MainWindow;
                    if (null == System.Windows.Application.Current.MainWindow)
                    {
                        new System.Windows.Application();
                    }
                    //var windowlarge = new Storyboard();
                    var incWindHeight = new DoubleAnimation();

                    incWindHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));
                    incWindHeight.From = 122;
                    incWindHeight.To = 140;

                    Storyboard.SetTargetProperty(incWindHeight, new PropertyPath(MainWindow.HeightProperty));

                    SineEase easingFunction = new SineEase();
                    easingFunction.EasingMode = EasingMode.EaseInOut;
                    incWindHeight.EasingFunction = easingFunction;
                    windowlarge.Children.Add(incWindHeight);
                    windowlarge.Begin(this);

                    DoubleAnimation da = new DoubleAnimation();
                    da.From = 0;
                    da.To = 1;
                    da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                    da.BeginTime = TimeSpan.FromSeconds(.8);
                    da.RepeatBehavior = new RepeatBehavior(1.0);
                    messageLabel.BeginAnimation(OpacityProperty, da);
                    var delay = new DispatcherTimer { Interval = TimeSpan.FromSeconds(6) };
                    delay.Start();
                    delay.Tick += (parent2, args2) =>
                    {
                        delay.Stop();
                        messageLabel.Content = "";
                        DoubleAnimation db = new DoubleAnimation();
                        db.From = 1;
                        db.To = 0;
                        db.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                        db.BeginTime = TimeSpan.FromMilliseconds(1);
                        db.RepeatBehavior = new RepeatBehavior(1.0);
                        messageLabel.BeginAnimation(OpacityProperty, db);

                        //var windowsmall = new Storyboard();
                        var decHeight = new DoubleAnimation();

                        decHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));

                        decHeight.From = 140;
                        decHeight.To = 122;

                        Storyboard.SetTargetProperty(decHeight, new PropertyPath(MainWindow.HeightProperty));

                        easingFunction.EasingMode = EasingMode.EaseInOut;
                        decHeight.EasingFunction = easingFunction;

                        windowsmall.Children.Add(decHeight);
                        windowsmall.Completed += new EventHandler(Animation_Completed);
                        windowsmall.Begin(this);
                        animdone = false;
                    };
                };
            }
        }
        private void Animation_Completed(object sender, EventArgs e)
        {
            animdone = true;
        }
        private bool IsNumberKey(Key inKey)
        {
            if (inKey < Key.D0 || inKey > Key.D9)
            {
                if (inKey < Key.NumPad0 || inKey > Key.NumPad9)
                {
                    return false;
                }
            }
            return true;
        }

        private void minutesBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key);
        }

        private void secondsBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key);
        }

        private void milliBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key);
        }

        private void secondsBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(secondsBox.Text, out seconds);
            if (((System.Windows.Controls.TextBox)sender).Text.Length == 2)
            {
                milliBox.Focus();
            }
            if (seconds >= 60)
            {
                secondsBox.Background = System.Windows.Media.Brushes.Red;
                if (animdone == true)
                {
                    messageLabel.Content = "Can't have more than 59 seconds.";
                    Animation(ref animation);
                }
                secondsCheck = false;
            }
            if (seconds <= 59)
            {
                minutesBox.Background = System.Windows.Media.Brushes.White;
                secondsCheck = true;
            }
            goButtonCheck(ref check);
        }

        private void milliBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(minutesBox.Text, out minutes);
            if (((System.Windows.Controls.TextBox)sender).Text.Length == 2)
            {
                goButton.Focus();
            }
            if (milli <= 9)
            {
                minutesBox.Background = System.Windows.Media.Brushes.White;
                milliCheck = true;
            }
            goButtonCheck(ref check);
        }
        private void goButtonCheck(ref bool check)
        {
            if (string.IsNullOrEmpty(minutesBox.Text))
            {
                minutesCheck = false;
            }
           if (minutes >= 20 && (milli > 0 || seconds > 0))
            {
                minutesBox.Background = System.Windows.Media.Brushes.Red;
                secondsBox.Background = System.Windows.Media.Brushes.Red;
                milliBox.Background = System.Windows.Media.Brushes.Red;
                twenty = false;
                messageLabel.Content = "Can't have more than 20 minutes.";
                Animation(ref animation);
            }
            else
            {
                twenty = true;
            }
            if(minutesCheck && secondsCheck && milliCheck && twenty)
            {
                minutesBox.Background = System.Windows.Media.Brushes.White;
                secondsBox.Background = System.Windows.Media.Brushes.White;
                milliBox.Background = System.Windows.Media.Brushes.White;
                goButton.IsEnabled = true;
            }
        }

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            string Stringminutes = minutesBox.Text;
            string Stringseconds = secondsBox.Text;
            string Stringmilli = milliBox.Text;
            int minutes = Convert.ToInt32(Stringminutes);
            int seconds = Convert.ToInt32(Stringseconds);
            int milli = Convert.ToInt32(Stringmilli);
            int totaltime = (minutes * 60 * 1000) + (seconds * 1000) + (milli * 100);
            ((MainWindow)System.Windows.Application.Current.MainWindow).timer = totaltime;
        }
    }
}
