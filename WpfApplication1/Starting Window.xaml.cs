using System;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Text;
using System.Threading;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Threading.Tasks;
using System.Drawing;
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
using System.Timers;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Starting_Window.xaml
    /// </summary>
    /// 
    public partial class Starting_Window : Window
    {
        #region initalize
        DateTime dt;
        DispatcherTimer t;
        System.Windows.Forms.Timer homedelay = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer awaydelay = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer manualHome = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer manualAway = new System.Windows.Forms.Timer();

        int homeTeamchoice = 0;
        int awayTeamchoice = 0;
        int homewinsnum;
        int homelossesnum;
        int awaywinsnum;
        int awaylossesnum;

        bool homeSelect = false;
        bool awaySelect = false;
        bool homewins = false;
        bool homelosses = false;
        bool awaywins = false;
        bool awaylosses = false;
        bool Animation;
        bool Final;
        bool RecordAnimation;
        bool EndAnimation;
        bool Internet;
        bool CustomInput;
        bool custom = false;
        bool loadingFailed;
        bool LoadRecords;
        bool TeamAnimation;
        bool internetWorked = false;
        bool InternetScoresLoaded;
        bool InternetScores;
        bool loadScores;
        bool HomeSelected;
        bool AwaySelected;
        bool bothSelect;
        bool animdone = true;
        bool manual = false;
        bool readyInternet = false;
        bool loadingPass;
        bool loadingdone;
        bool ManualInput;
        bool manualClicked = false;

        bool homeChanged = false;
        bool awayChanged = false;

        string HomeTeamName = null;
        string AwayTeamName = null;
        string HomeOverallWins = null;
        string HomeOverallLosses = null;
        string HomeConferenceWins = null;
        string HomeConferenceLosses = null;
        string AwayOverallWins = null;
        string AwayOverallLosses = null;
        string AwayConferenceWins = null;
        string AwayConferenceLosses = null;

        string matchAlbanyS1 = null;
        string matchAlbanyS2 = null;
        string matchAlbanyS3 = null;
        string matchAlbanyS4 = null;

        string matchBinghamptonS1 = null;
        string matchBinghamptonS2 = null;
        string matchBinghamptonS3 = null;
        string matchBinghamptonS4 = null;

        string matchHartfordS1 = null;
        string matchHartfordS2 = null;
        string matchHartfordS3 = null;
        string matchHartfordS4 = null;

        string matchMaineS1 = null;
        string matchMaineS2 = null;
        string matchMaineS3 = null;
        string matchMaineS4 = null;

        string matchUmbcS1 = null;
        string matchUmbcS2 = null;
        string matchUmbcS3 = null;
        string matchUmbcS4 = null;

        string matchUmasslowellS1 = null;
        string matchUmasslowellS2 = null;
        string matchUmasslowellS3 = null;
        string matchUmasslowellS4 = null;

        string matchUnhS1 = null;
        string matchUnhS2 = null;
        string matchUnhS3 = null;
        string matchUnhS4 = null;

        string matchStonyBrookS1 = null;
        string matchStonyBrookS2 = null;
        string matchStonyBrookS3 = null;
        string matchStonyBrookS4 = null;

        string matchVermontS1 = null;
        string matchVermontS2 = null;
        string matchVermontS3 = null;
        string matchVermontS4 = null;

        public Starting_Window()
        {
            InitializeComponent();
            this.DataContext = this;
            t = new DispatcherTimer();
            t.Tick += new EventHandler(t_Tick);
            gobutton.IsEnabled = false;
            okButton.Opacity = 0;
            cancelButton.Opacity = 0;
            messageLabel.Content = "";
            messageLabel.Opacity = 0;
            ualbanyHome.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            binghamptonHome.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            hartfordHome.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            maineHome.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            umbcHome.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            umasslowellHome.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            newhampshireHome.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            stonybrookHome.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            vermontHome.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            ualbanyAway.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            binghamptonAway.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            hartfordAway.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            maineAway.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            umbcAway.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            umasslowellAway.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            newhampshireAway.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            stonybrookAway.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            vermontAway.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            internetGrid.Opacity = 0;
            homeRecordsGrid.Opacity = 0;
            awayRecordsGrid.Opacity = 0;
            awayTeamAwait.Opacity = 0;
            homeTeamAwait.Opacity = 0;
            internet(this, new EventArgs());
        }

        void t_Tick(object sender, EventArgs e)
        {
            dt = DateTime.Now;
            t.Interval = new TimeSpan(0, 0, 1);
            t.IsEnabled = true;
        }
        #endregion
        public bool ManualMode
        {
            set
            {
                manual = true;
                manualClicked = true;
                homeTeamAwait.Opacity = 0;
                awayTeamAwait.Opacity = 0;
                manualInput(ref ManualInput);
                customInput(ref CustomInput);
            }
        }
        #region teamchoices
        private void teamchoices(ref int homeTeamchoice)
        {
            if (homeTeamchoice != 1)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));

                ualbanyHome.Background = button;
            }
            if (homeTeamchoice == 1)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 70, 22, 107); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0));
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                ualbanyHome.Background = myRadialGradientBrush;


            }
            if (homeTeamchoice != 2)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                binghamptonHome.Background = button;
            }
            if (homeTeamchoice == 2)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 0, 178, 92); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                binghamptonHome.Background = myRadialGradientBrush;
            }
            if (homeTeamchoice != 3)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                hartfordHome.Background = button;
            }
            if (homeTeamchoice == 3)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 237, 28, 46); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                hartfordHome.Background = myRadialGradientBrush;
            }
            if (homeTeamchoice != 4)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                maineHome.Background = button;
            }
            if (homeTeamchoice == 4)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 146, 148, 151); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                maineHome.Background = myRadialGradientBrush;
            }
            if (homeTeamchoice != 5)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                umbcHome.Background = button;
            }
            if (homeTeamchoice == 5)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 250, 196, 10); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                umbcHome.Background = myRadialGradientBrush;
            }
            if (homeTeamchoice != 6)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                umasslowellHome.Background = button;
            }
            if (homeTeamchoice == 6)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 0, 83, 161); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                umasslowellHome.Background = myRadialGradientBrush;
            }
            if (homeTeamchoice != 7)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                newhampshireHome.Background = button;
            }
            if (homeTeamchoice == 7)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 0, 39, 93); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                newhampshireHome.Background = myRadialGradientBrush;
            }
            if (homeTeamchoice != 8)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                stonybrookHome.Background = button;
            }
            if (homeTeamchoice == 8)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 184, 17, 55); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                stonybrookHome.Background = myRadialGradientBrush;
            }
            if (homeTeamchoice != 9)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                vermontHome.Background = button;
            }
            if (homeTeamchoice == 9)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 1, 56, 35); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                vermontHome.Background = myRadialGradientBrush;
            }
            if (awayTeamchoice != 1)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                ualbanyAway.Background = button;
            }
            if (awayTeamchoice == 1)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 70, 22, 107); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0));
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                ualbanyAway.Background = myRadialGradientBrush;
            }
            if (awayTeamchoice != 2)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                binghamptonAway.Background = button;
            }
            if (awayTeamchoice == 2)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 0, 178, 92); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                binghamptonAway.Background = myRadialGradientBrush;
            }
            if (awayTeamchoice != 3)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                hartfordAway.Background = button;
            }
            if (awayTeamchoice == 3)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 237, 28, 46); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                hartfordAway.Background = myRadialGradientBrush;
            }
            if (awayTeamchoice != 4)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                maineAway.Background = button;
            }
            if (awayTeamchoice == 4)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 146, 148, 151); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                maineAway.Background = myRadialGradientBrush;
            }
            if (awayTeamchoice != 5)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                umbcAway.Background = button;
            }
            if (awayTeamchoice == 5)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 250, 196, 10); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                umbcAway.Background = myRadialGradientBrush;
            }
            if (awayTeamchoice != 6)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                umasslowellAway.Background = button;
            }
            if (awayTeamchoice == 6)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 0, 83, 161); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                umasslowellAway.Background = myRadialGradientBrush;
            }
            if (awayTeamchoice != 7)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                newhampshireAway.Background = button;
            }
            if (awayTeamchoice == 7)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 0, 39, 93); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                newhampshireAway.Background = myRadialGradientBrush;
            }
            if (awayTeamchoice != 8)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                stonybrookAway.Background = button;
            }
            if (awayTeamchoice == 8)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 184, 17, 55); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                stonybrookAway.Background = myRadialGradientBrush;
            }
            if (awayTeamchoice != 9)
            {
                LinearGradientBrush button = new LinearGradientBrush();
                button.StartPoint = new System.Windows.Point(0.5, 0);
                button.EndPoint = new System.Windows.Point(0.5, 1);
                System.Drawing.Color buttoncolor1 = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                System.Windows.Media.Color buttonColor1 = System.Windows.Media.Color.FromArgb(buttoncolor1.A, buttoncolor1.R, buttoncolor1.G, buttoncolor1.B);

                System.Drawing.Color buttoncolor2 = System.Drawing.Color.FromArgb(255, 212, 212, 212);
                System.Windows.Media.Color buttonColor2 = System.Windows.Media.Color.FromArgb(buttoncolor2.A, buttoncolor2.R, buttoncolor2.G, buttoncolor2.B);

                button.GradientStops.Add(new GradientStop(buttonColor1, 0.5));
                button.GradientStops.Add(new GradientStop(buttonColor2, 0.51));
                vermontAway.Background = button;
            }
            if (awayTeamchoice == 9)
            {
                RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
                myRadialGradientBrush.GradientOrigin = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.Center = new System.Windows.Point(0.5, 0.5);
                myRadialGradientBrush.RadiusX = 0.5;
                myRadialGradientBrush.RadiusY = 0.5;
                myRadialGradientBrush.Opacity = 0.5;
                GradientStop purple = new GradientStop();
                System.Drawing.Color buttoncolor = System.Drawing.Color.FromArgb(255, 212, 212, 212); // This is your color to convert from
                System.Windows.Media.Color buttonColor = System.Windows.Media.Color.FromArgb(buttoncolor.A, buttoncolor.R, buttoncolor.G, buttoncolor.B);
                System.Drawing.Color teamcolor = System.Drawing.Color.FromArgb(255, 1, 56, 35); // This is your color to convert from
                System.Windows.Media.Color teamColor = System.Windows.Media.Color.FromArgb(teamcolor.A, teamcolor.R, teamcolor.G, teamcolor.B);
                myRadialGradientBrush.GradientStops.Add(new GradientStop(teamColor, 1.0)); ;
                myRadialGradientBrush.GradientStops.Add(new GradientStop(buttonColor, 0.7));
                vermontAway.Background = myRadialGradientBrush;
            }
        }
        # endregion
        #region buttonsHome
        private void ualbanyHome_Click(object sender, RoutedEventArgs e)
        {
            if(internetWorked == true && homeTeamchoice != 1)
            {
                homeSelected(ref HomeSelected);
                homeChanged = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 1;
                if(manual == false)
                {
                    internetScores(ref InternetScores);
                }    
            }
            if(internetWorked == false && homeTeamchoice != 1)
            {
                homeChanged = true;
                homeSelect = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 1;
            }
            awaitgoClick(ref homeSelect);
            homeTeamRecordText.Text = "Great Danes\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyAway.IsEnabled = false;
            binghamptonAway.IsEnabled = true;
            hartfordAway.IsEnabled = true;
            maineAway.IsEnabled = true;
            umbcAway.IsEnabled = true;
            umasslowellAway.IsEnabled = true;
            newhampshireAway.IsEnabled = true;
            stonybrookAway.IsEnabled = true;
            vermontAway.IsEnabled = true;
        }

        private void binghamptonHome_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && homeTeamchoice != 2)
            {
                homeSelected(ref HomeSelected);
                homeChanged = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 2;
                if(manual == false)
                {
                    internetScores(ref InternetScores);
                }                
            }
            if (internetWorked == false && homeTeamchoice != 2)
            {
                homeChanged = true;
                homeSelect = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 2;
            }
            awaitgoClick(ref homeSelect);
            homeTeamRecordText.Text = "Bearcats\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyAway.IsEnabled = true;
            binghamptonAway.IsEnabled = false;
            hartfordAway.IsEnabled = true;
            maineAway.IsEnabled = true;
            umbcAway.IsEnabled = true;
            umasslowellAway.IsEnabled = true;
            newhampshireAway.IsEnabled = true;
            stonybrookAway.IsEnabled = true;
            vermontAway.IsEnabled = true;
        }
        private void hartfordHome_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && homeTeamchoice != 3)
            {
                homeSelected(ref HomeSelected);
                homeChanged = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 3;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && homeTeamchoice != 3)
            {
                homeChanged = true;
                homeSelect = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 3;
            }
            awaitgoClick(ref homeSelect);
            homeTeamRecordText.Text = "Hawks\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyAway.IsEnabled = true;
            binghamptonAway.IsEnabled = true;
            hartfordAway.IsEnabled = false;
            maineAway.IsEnabled = true;
            umbcAway.IsEnabled = true;
            umasslowellAway.IsEnabled = true;
            newhampshireAway.IsEnabled = true;
            stonybrookAway.IsEnabled = true;
            vermontAway.IsEnabled = true;
        }
        private void maineHome_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && homeTeamchoice != 4)
            {
                homeSelected(ref HomeSelected);
                homeChanged = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 4;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && homeTeamchoice != 4)
            {
                homeChanged = true;
                homeSelect = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 4;
            }
            awaitgoClick(ref homeSelect);
            homeTeamRecordText.Text = "Black Bears\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyAway.IsEnabled = true;
            binghamptonAway.IsEnabled = true;
            hartfordAway.IsEnabled = true;
            maineAway.IsEnabled = false;
            umbcAway.IsEnabled = true;
            umasslowellAway.IsEnabled = true;
            newhampshireAway.IsEnabled = true;
            stonybrookAway.IsEnabled = true;
            vermontAway.IsEnabled = true;
        }
        private void umbcHome_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && homeTeamchoice != 5)
            {
                homeSelected(ref HomeSelected);
                homeChanged = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 5;
                if (manual == false)
                {
                    homeSelect = true;
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && homeTeamchoice != 5)
            {
                homeChanged = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 5;
            }
            awaitgoClick(ref homeSelect);
            homeTeamRecordText.Text = "Retrievers\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyAway.IsEnabled = true;
            binghamptonAway.IsEnabled = true;
            hartfordAway.IsEnabled = true;
            maineAway.IsEnabled = true;
            umbcAway.IsEnabled = false;
            umasslowellAway.IsEnabled = true;
            newhampshireAway.IsEnabled = true;
            stonybrookAway.IsEnabled = true;
            vermontAway.IsEnabled = true;
        }
        private void umasslowellHome_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && homeTeamchoice != 6)
            {
                homeSelected(ref HomeSelected);
                homeChanged = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 6;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && homeTeamchoice != 6)
            {
                homeChanged = true;
                homeSelect = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 6;
            }
            awaitgoClick(ref homeSelect);
            homeTeamRecordText.Text = "River Hawks\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyAway.IsEnabled = true;
            binghamptonAway.IsEnabled = true;
            hartfordAway.IsEnabled = true;
            maineAway.IsEnabled = true;
            umbcAway.IsEnabled = true;
            umasslowellAway.IsEnabled = false;
            newhampshireAway.IsEnabled = true;
            stonybrookAway.IsEnabled = true;
            vermontAway.IsEnabled = true;
        }
        private void newhampshireHome_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && homeTeamchoice != 7)
            {
                homeSelected(ref HomeSelected);
                homeChanged = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 7;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && homeTeamchoice != 7)
            {
                homeChanged = true;
                homeSelect = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 7;
            }
            awaitgoClick(ref homeSelect);
            homeTeamRecordText.Text = "Wildcats\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyAway.IsEnabled = true;
            binghamptonAway.IsEnabled = true;
            hartfordAway.IsEnabled = true;
            maineAway.IsEnabled = true;
            umbcAway.IsEnabled = true;
            umasslowellAway.IsEnabled = true;
            newhampshireAway.IsEnabled = false;
            stonybrookAway.IsEnabled = true;
            vermontAway.IsEnabled = true;
        }
        private void stonybrookHome_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && homeTeamchoice != 8)
            {
                homeSelected(ref HomeSelected);
                homeChanged = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 8;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && homeTeamchoice != 8)
            {
                homeChanged = true;
                homeSelect = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 8;
            }
            awaitgoClick(ref homeSelect);
            homeTeamRecordText.Text = "Seawolves\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyAway.IsEnabled = true;
            binghamptonAway.IsEnabled = true;
            hartfordAway.IsEnabled = true;
            maineAway.IsEnabled = true;
            umbcAway.IsEnabled = true;
            umasslowellAway.IsEnabled = true;
            newhampshireAway.IsEnabled = true;
            stonybrookAway.IsEnabled = false;
            vermontAway.IsEnabled = true;
        }
        private void vermontHome_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && homeTeamchoice != 9)
            {
                homeSelected(ref HomeSelected);
                homeChanged = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 9;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && homeTeamchoice != 9)
            {
                homeChanged = true;
                homeSelect = true;
                teamAnimation(ref TeamAnimation);
                homeTeamchoice = 9;
            }
            awaitgoClick(ref homeSelect);
            homeTeamRecordText.Text = "Catamounts\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyAway.IsEnabled = true;
            binghamptonAway.IsEnabled = true;
            hartfordAway.IsEnabled = true;
            maineAway.IsEnabled = true;
            umbcAway.IsEnabled = true;
            umasslowellAway.IsEnabled = true;
            newhampshireAway.IsEnabled = true;
            stonybrookAway.IsEnabled = true;
            vermontAway.IsEnabled = false;
        }
#endregion
        private void ualbanyAway_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && awayTeamchoice != 1)
            {
                awaySelected(ref AwaySelected);
                awayChanged = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 1;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && awayTeamchoice != 1)
            {
                awayChanged = true;
                awaySelect = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 1;
            }
            awaitgoClick(ref homeSelect);
            awayTeamRecordText.Text = "Great Danes\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyHome.IsEnabled = false;
            binghamptonHome.IsEnabled = true;
            hartfordHome.IsEnabled = true;
            maineHome.IsEnabled = true;
            umbcHome.IsEnabled = true;
            umasslowellHome.IsEnabled = true;
            newhampshireHome.IsEnabled = true;
            stonybrookHome.IsEnabled = true;
            vermontHome.IsEnabled = true;
        }

        private void binghamptonAway_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && awayTeamchoice != 2)
            {
                awaySelected(ref AwaySelected);
                awayChanged = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 2;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && awayTeamchoice != 2)
            {
                awayChanged = true;
                awaySelect = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 2;
            }
            awaitgoClick(ref homeSelect);
            awayTeamRecordText.Text = "Bearcats\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyHome.IsEnabled = true;
            binghamptonHome.IsEnabled = false;
            hartfordHome.IsEnabled = true;
            maineHome.IsEnabled = true;
            umbcHome.IsEnabled = true;
            umasslowellHome.IsEnabled = true;
            newhampshireHome.IsEnabled = true;
            stonybrookHome.IsEnabled = true;
            vermontHome.IsEnabled = true;
        }
        private void hartfordAway_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && awayTeamchoice != 3)
            {
                awaySelected(ref AwaySelected);
                awayChanged = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 3;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && awayTeamchoice != 3)
            {
                awayChanged = true;
                awaySelect = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 3;
            }
            awaitgoClick(ref homeSelect);
            awayTeamRecordText.Text = "Hawks\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyHome.IsEnabled = true;
            binghamptonHome.IsEnabled = true;
            hartfordHome.IsEnabled = false;
            maineHome.IsEnabled = true;
            umbcHome.IsEnabled = true;
            umasslowellHome.IsEnabled = true;
            newhampshireHome.IsEnabled = true;
            stonybrookHome.IsEnabled = true;
            vermontHome.IsEnabled = true;
        }
        private void maineAway_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && awayTeamchoice != 4)
            {
                awaySelected(ref AwaySelected);
                awayChanged = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 4;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && awayTeamchoice != 4)
            {
                awayChanged = true;
                awaySelect = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 4;
            }
            awaitgoClick(ref homeSelect);
            awayTeamRecordText.Text = "Black Bears\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyHome.IsEnabled = true;
            binghamptonHome.IsEnabled = true;
            hartfordHome.IsEnabled = true;
            maineHome.IsEnabled = false;
            umbcHome.IsEnabled = true;
            umasslowellHome.IsEnabled = true;
            newhampshireHome.IsEnabled = true;
            stonybrookHome.IsEnabled = true;
            vermontHome.IsEnabled = true;
        }
        private void umbcAway_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && awayTeamchoice != 5)
            {
                awaySelected(ref AwaySelected);
                awayChanged = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 5;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && awayTeamchoice != 5)
            {
                awayChanged = true;
                awaySelect = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 5;
            }
            awaitgoClick(ref homeSelect);
            awayTeamRecordText.Text = "Retrievers\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyHome.IsEnabled = true;
            binghamptonHome.IsEnabled = true;
            hartfordHome.IsEnabled = true;
            maineHome.IsEnabled = true;
            umbcHome.IsEnabled = false;
            umasslowellHome.IsEnabled = true;
            newhampshireHome.IsEnabled = true;
            stonybrookHome.IsEnabled = true;
            vermontHome.IsEnabled = true;
        }
        private void umasslowellAway_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && awayTeamchoice != 6)
            {
                awaySelected(ref AwaySelected);
                awayChanged = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 6;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && awayTeamchoice != 6)
            {
                awayChanged = true;
                awaySelect = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 6;
            }
            awaitgoClick(ref homeSelect);
            awayTeamRecordText.Text = "River Hawks\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyHome.IsEnabled = true;
            binghamptonHome.IsEnabled = true;
            hartfordHome.IsEnabled = true;
            maineHome.IsEnabled = true;
            umbcHome.IsEnabled = true;
            umasslowellHome.IsEnabled = false;
            newhampshireHome.IsEnabled = true;
            stonybrookHome.IsEnabled = true;
            vermontHome.IsEnabled = true;
        }
        private void newhampshireAway_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && awayTeamchoice != 7)
            {
                awaySelected(ref AwaySelected);
                awayChanged = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 7;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && awayTeamchoice != 7)
            {
                awayChanged = true;
                awaySelect = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 7;
            }
            awaitgoClick(ref homeSelect);
            awayTeamRecordText.Text = "Wildcats\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyHome.IsEnabled = true;
            binghamptonHome.IsEnabled = true;
            hartfordHome.IsEnabled = true;
            maineHome.IsEnabled = true;
            umbcHome.IsEnabled = true;
            umasslowellHome.IsEnabled = true;
            newhampshireHome.IsEnabled = false;
            stonybrookHome.IsEnabled = true;
            vermontHome.IsEnabled = true;
        }
        private void stonybrookAway_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && awayTeamchoice != 8)
            {
                awaySelected(ref AwaySelected);
                awayChanged = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 8;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && awayTeamchoice != 8)
            {
                awayChanged = true;
                awaySelect = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 8;
            }
            awaitgoClick(ref homeSelect);
            awayTeamRecordText.Text = "Seawolves\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyHome.IsEnabled = true;
            binghamptonHome.IsEnabled = true;
            hartfordHome.IsEnabled = true;
            maineHome.IsEnabled = true;
            umbcHome.IsEnabled = true;
            umasslowellHome.IsEnabled = true;
            newhampshireHome.IsEnabled = true;
            stonybrookHome.IsEnabled = false;
            vermontHome.IsEnabled = true;
        }
        private void vermontAway_Click(object sender, RoutedEventArgs e)
        {
            if (internetWorked == true && awayTeamchoice != 9)
            {
                awaySelected(ref AwaySelected);
                awayChanged = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 9;
                if (manual == false)
                {
                    internetScores(ref InternetScores);
                }
            }
            if (internetWorked == false && awayTeamchoice != 9)
            {
                awayChanged = true;
                awaySelect = true;
                teamAnimation(ref TeamAnimation);
                awayTeamchoice = 9;
            }
            awaitgoClick(ref homeSelect);
            awayTeamRecordText.Text = "Catamounts\nRecord";
            teamchoices(ref homeTeamchoice);
            ualbanyHome.IsEnabled = true;
            binghamptonHome.IsEnabled = true;
            hartfordHome.IsEnabled = true;
            maineHome.IsEnabled = true;
            umbcHome.IsEnabled = true;
            umasslowellHome.IsEnabled = true;
            newhampshireHome.IsEnabled = true;
            stonybrookHome.IsEnabled = true;
            vermontHome.IsEnabled = false;
        }
      
        private void homewinsBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(homewinsBox.Text))
            {
                e.Handled = true;
                homewins = false;
                homewinsBox.Background = System.Windows.Media.Brushes.White;
                awaitgoClick(ref homeSelect);
            }
            int.TryParse(homewinsBox.Text, out homewinsnum);
            if (homewinsnum >= 0 && homewinsnum < 40)
            {
                if (homewinsnum > 0 && homelossesnum > 0 && homewinsnum + homelossesnum >= 40)
                {
                    homewins = false;
                    awaitgoClick(ref homeSelect);
                    homewinsBox.Background = System.Windows.Media.Brushes.Red;
                    homelossesBox.Background = System.Windows.Media.Brushes.Red;
                    messageLabel.Content = "There can't be 40 games in one season...";
                    animation(ref Animation);
                }
                if (homewinsnum >= 0 && homelossesnum >= 0 && homewinsnum + homelossesnum < 40)
                {
                    homewins = true;
                    awaitgoClick(ref homeSelect);
                    homewinsBox.Background = System.Windows.Media.Brushes.White;
                    homelossesBox.Background = System.Windows.Media.Brushes.White;
                }
            }
            if (homewinsnum >= 40)
            {
                homewins = false;
                awaitgoClick(ref homeSelect);
                //homerecord(ref homeRecord);
                homewinsBox.Background = System.Windows.Media.Brushes.Red;
                messageLabel.Content = "There can't be 40 games in one season...";
                animation(ref Animation); 
            }
            if (((System.Windows.Controls.TextBox)sender).Text.Length == 2)
            {
                homelossesBox.Focus();
            }
        }
        private void homelossesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(homelossesBox.Text))
            {
                e.Handled = true;
                homelosses = false;
                homelossesBox.Background = System.Windows.Media.Brushes.White;
                awaitgoClick(ref homeSelect);
                //homerecord(ref homeRecord);
            }
            int.TryParse(homelossesBox.Text, out homelossesnum);
            if (homelossesnum >= 0 && homelossesnum < 40)
            {
                homelosses = true;
                awaitgoClick(ref homeSelect);
                //homerecord(ref homeRecord);               
                if (homewinsnum > 0 && homelossesnum > 0 && homewinsnum + homelossesnum >= 40)
                {
                    homewins = false;
                    awaitgoClick(ref homeSelect);
                    homewinsBox.Background = System.Windows.Media.Brushes.Red;
                    homelossesBox.Background = System.Windows.Media.Brushes.Red;
                    messageLabel.Content = "There can't be 40 games in one season...";
                    animation(ref Animation);
                }
                if (homewinsnum >= 0 && homelossesnum >= 0 && homewinsnum + homelossesnum < 40)
                {
                    homewins = true;
                    awaitgoClick(ref homeSelect);
                    homewinsBox.Background = System.Windows.Media.Brushes.White;
                    homelossesBox.Background = System.Windows.Media.Brushes.White;
                }
            }
            if (homelossesnum >= 40)
            {
                homelosses = false;
                awaitgoClick(ref homeSelect);
                homelossesBox.Background = System.Windows.Media.Brushes.Red;
                messageLabel.Content = "There can't be 40 games in one season...";
                animation(ref Animation);
            }
            if (((System.Windows.Controls.TextBox)sender).Text.Length == 2)
            {
                awaywinsBox.Focus();
            }
        }
        
        private void awaywinsBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(awaywinsBox.Text))
            {
                e.Handled = true;
                awaywins = false;
                awaywinsBox.Background = System.Windows.Media.Brushes.White;
                awaitgoClick(ref homeSelect);
            }
            int.TryParse(awaywinsBox.Text, out awaywinsnum);
            if (awaywinsnum >= 0 && awaywinsnum < 40)
            {
                if (awaywinsnum > 0 && awaylossesnum > 0 && awaywinsnum + awaylossesnum >= 40)
                {
                    awaywins = false;
                    awaitgoClick(ref homeSelect);
                    awaywinsBox.Background = System.Windows.Media.Brushes.Red;
                    awaylossesBox.Background = System.Windows.Media.Brushes.Red;
                    messageLabel.Content = "There can't be 40 games in one season...";
                    animation(ref Animation);
                }
                if (awaywinsnum >= 0 && awaylossesnum >= 0 && awaywinsnum + awaylossesnum < 40)
                {
                    awaywins = true;
                    awaitgoClick(ref homeSelect);
                    awaywinsBox.Background = System.Windows.Media.Brushes.White;
                    awaylossesBox.Background = System.Windows.Media.Brushes.White;
                }
            }
            if (awaywinsnum >= 40)
            {
                awaywins = false;
                awaitgoClick(ref homeSelect);
                awaywinsBox.Background = System.Windows.Media.Brushes.Red;
                messageLabel.Content = "There can't be 40 games in one season...";
                animation(ref Animation);
            }
            if (((System.Windows.Controls.TextBox)sender).Text.Length == 2)
            {
                awaylossesBox.Focus();
            }
        }
        private void awaylossesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(awaylossesBox.Text))
            {
                e.Handled = true;
                awaylosses = false;
                awaylossesBox.Background = System.Windows.Media.Brushes.White;
                awaitgoClick(ref homeSelect);
            }
            int.TryParse(awaylossesBox.Text, out awaylossesnum);
            if (awaylossesnum >= 0 && awaylossesnum < 40)
            {
                if (awaywinsnum > 0 && awaylossesnum > 0 && awaywinsnum + awaylossesnum >= 40)
                {
                    awaylosses = false;
                    awaywinsBox.Background = System.Windows.Media.Brushes.Red;
                    awaylossesBox.Background = System.Windows.Media.Brushes.Red;
                    messageLabel.Content = "There can't be 40 games in one season...";
                    awaitgoClick(ref homeSelect);
                    animation(ref Animation);
                }
                if (awaywinsnum >= 0 && awaylossesnum >= 0 && awaywinsnum + awaylossesnum < 40)
                {
                    awaylosses = true;
                    awaitgoClick(ref homeSelect);
                    awaywinsBox.Background = System.Windows.Media.Brushes.White;
                    awaylossesBox.Background = System.Windows.Media.Brushes.White;
                }
            }
            if (awaylossesnum >= 40)
            {
                awaylosses = false;
                awaitgoClick(ref homeSelect);
                awaylossesBox.Background = System.Windows.Media.Brushes.Red;
                messageLabel.Content = "There can't be 40 games in one season...";
                animation(ref Animation);
            }
            if (((System.Windows.Controls.TextBox)sender).Text.Length == 2)
            {
                gobutton.Focus();
            }
        }
        private void awaitgoClick(ref bool homeSelect)
        {
            if (manual == true && homeSelect == true && awaySelect == true && homewins == true && homelosses == true && awaywins == true && awaylosses == true)
            {
                gobutton.IsEnabled = true;
            }
            if (manual == false && homeSelect == true && awaySelect == true)
            {
                tryagainButton.IsEnabled = true;
                readyInternet = true;
            }
        }
        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            int homeRecord = homewinsnum + homelossesnum;
            int awayRecord = awaywinsnum + awaylossesnum;
            int recorddif = homeRecord - awayRecord;
            int recorddifAbs = Math.Abs(recorddif);
            if (recorddifAbs <= 3)
            {
                final(ref Final);                                
            }
            if (recorddifAbs > 3)
            {
                messageLabel.Content = "The " + homeTeamRecordText.Text.ToString() + " and " + awayTeamRecordText.Text.ToString() + " records differ by more than 3 games. Are you sure this is correct?";
                recordanimation(ref RecordAnimation);
                gobutton.IsEnabled = false;
                homewinsBox.IsEnabled = false;
                homelossesBox.IsEnabled = false;
                awaywinsBox.IsEnabled = false;
                awaylossesBox.IsEnabled = false;
            }
        }
        private void animation(ref bool Animation)
        {
            messageLabel.BorderBrush = System.Windows.Media.Brushes.Red;
            messageLabel.BorderThickness = new Thickness(1, 1, 1, 1);

            var windowlarge = new Storyboard();
            var windowsmall = new Storyboard();
            if (animdone == false)
            {
                var delay2 = new DispatcherTimer { Interval = TimeSpan.FromSeconds(6) };
                delay2.Start();
                delay2.Tick += (parent2, args2) =>
                {
                    animdone = false;
                    Window mainWindow = System.Windows.Application.Current.MainWindow;
                    if (null == System.Windows.Application.Current.MainWindow)
                    {
                        new System.Windows.Application();
                    }
                    //var windowlarge = new Storyboard();
                    var incWindHeight = new DoubleAnimation();

                    incWindHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));
                    incWindHeight.From = mainWindow.ActualHeight;
                    incWindHeight.To = 440;

                    Storyboard.SetTargetProperty(incWindHeight, new PropertyPath(MainWindow.HeightProperty));

                    SineEase easingFunction = new SineEase();
                    easingFunction.EasingMode = EasingMode.EaseInOut;
                    incWindHeight.EasingFunction = easingFunction;
                    windowlarge.Children.Add(incWindHeight);
                    windowlarge.Begin(this);

                    messageLabel.Opacity = 0;
                    RadialGradientBrush messageBrush = new RadialGradientBrush();
                    messageBrush.GradientStops.Add(new GradientStop(Colors.White, 0.5));
                    messageBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
                    messageLabel.Background = messageBrush;

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

                        decHeight.From = mainWindow.ActualHeight;
                        decHeight.To = 410;

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
            if (animdone == true)
            {
                animdone = false;
                Window mainWindow = System.Windows.Application.Current.MainWindow;
                if (null == System.Windows.Application.Current.MainWindow)
                {
                    new System.Windows.Application();
                }
                //var windowlarge = new Storyboard();
                var incWindHeight = new DoubleAnimation();

                incWindHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));
                incWindHeight.From = mainWindow.ActualHeight;
                incWindHeight.To = 440;

                Storyboard.SetTargetProperty(incWindHeight, new PropertyPath(MainWindow.HeightProperty));

                SineEase easingFunction = new SineEase();
                easingFunction.EasingMode = EasingMode.EaseInOut;
                incWindHeight.EasingFunction = easingFunction;
                windowlarge.Children.Add(incWindHeight);
                windowlarge.Begin(this);

                messageLabel.Opacity = 0;
                RadialGradientBrush messageBrush = new RadialGradientBrush();
                messageBrush.GradientStops.Add(new GradientStop(Colors.White, 0.5));
                messageBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
                messageLabel.Background = messageBrush;

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

                    decHeight.From = mainWindow.ActualHeight;
                    decHeight.To = 410;

                    Storyboard.SetTargetProperty(decHeight, new PropertyPath(MainWindow.HeightProperty));

                    easingFunction.EasingMode = EasingMode.EaseInOut;
                    decHeight.EasingFunction = easingFunction;

                    windowsmall.Children.Add(decHeight);
                    windowsmall.Completed += new EventHandler(Animation_Completed);
                    windowsmall.Begin(this);
                    animdone = false;
                };
            }
        }
        private void recordanimation(ref bool RecordAnimation)
        {
            messageLabel.BorderBrush = System.Windows.Media.Brushes.Red;
            messageLabel.BorderThickness = new Thickness(1, 1, 1, 1);

            var windowlarge = new Storyboard();
            var windowsmall = new Storyboard();
            if (animdone == false)
            {
                var delay2 = new DispatcherTimer { Interval = TimeSpan.FromSeconds(6) };
                delay2.Start();
                delay2.Tick += (parent2, args2) =>
                {
                    animdone = false;
                    Window mainWindow = System.Windows.Application.Current.MainWindow;
                    if (null == System.Windows.Application.Current.MainWindow)
                    {
                        new System.Windows.Application();
                    }
                    //var windowlarge = new Storyboard();
                    var incWindHeight = new DoubleAnimation();

                    incWindHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));
                    incWindHeight.From = mainWindow.ActualHeight;
                    incWindHeight.To = 480;

                    Storyboard.SetTargetProperty(incWindHeight, new PropertyPath(MainWindow.HeightProperty));

                    SineEase easingFunction = new SineEase();
                    easingFunction.EasingMode = EasingMode.EaseInOut;
                    incWindHeight.EasingFunction = easingFunction;
                    windowlarge.Children.Add(incWindHeight);
                    windowlarge.Begin(this);

                    messageLabel.Opacity = 0;
                    RadialGradientBrush messageBrush = new RadialGradientBrush();
                    messageBrush.GradientStops.Add(new GradientStop(Colors.White, 0.5));
                    messageBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
                    messageLabel.Background = messageBrush;

                    DoubleAnimation da = new DoubleAnimation();
                    da.From = 0;
                    da.To = 1;
                    da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                    da.BeginTime = TimeSpan.FromSeconds(.8);
                    da.RepeatBehavior = new RepeatBehavior(1.0);
                    messageLabel.BeginAnimation(OpacityProperty, da);
                    okButton.BeginAnimation(OpacityProperty, da);
                    cancelButton.BeginAnimation(OpacityProperty, da);
                };
            }
            if (animdone == true)
            {
                animdone = false;
                Window mainWindow = System.Windows.Application.Current.MainWindow;
                if (null == System.Windows.Application.Current.MainWindow)
                {
                    new System.Windows.Application();
                }
                //var windowlarge = new Storyboard();
                var incWindHeight = new DoubleAnimation();

                incWindHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));
                incWindHeight.From = mainWindow.ActualHeight;
                incWindHeight.To = 480;

                Storyboard.SetTargetProperty(incWindHeight, new PropertyPath(MainWindow.HeightProperty));

                SineEase easingFunction = new SineEase();
                easingFunction.EasingMode = EasingMode.EaseInOut;
                incWindHeight.EasingFunction = easingFunction;
                windowlarge.Children.Add(incWindHeight);
                windowlarge.Begin(this);

                messageLabel.Opacity = 0;
                RadialGradientBrush messageBrush = new RadialGradientBrush();
                messageBrush.GradientStops.Add(new GradientStop(Colors.White, 0.5));
                messageBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
                messageLabel.Background = messageBrush;

                DoubleAnimation da = new DoubleAnimation();
                da.From = 0;
                da.To = 1;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                da.BeginTime = TimeSpan.FromSeconds(.8);
                da.RepeatBehavior = new RepeatBehavior(1.0);
                messageLabel.BeginAnimation(OpacityProperty, da);
                okButton.BeginAnimation(OpacityProperty, da);
                cancelButton.BeginAnimation(OpacityProperty, da);
            }
        }
        private void teamAnimation(ref bool TeamAnimation)
        {
            if (homeChanged == true)
            { 
                DoubleAnimation da = new DoubleAnimation();
                da.From = 1;
                da.To = 0;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                HomeTeam.BeginAnimation(OpacityProperty, da);
                if(manual == true)
                {
                    manualHome.Tick += new EventHandler(manualhomeAnim);
                    manualHome.Interval = 300;
                    manualHome.Start();
                    homeChanged = false;
                }
                DoubleAnimation db = new DoubleAnimation();
                db.From = 0;
                db.To = 1;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                db.BeginTime = TimeSpan.FromMilliseconds(300);
                db.RepeatBehavior = new RepeatBehavior(1.0);
                HomeTeam.BeginAnimation(OpacityProperty, db);
            }
            if (awayChanged == true)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 1;
                da.To = 0;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                awayTeam.BeginAnimation(OpacityProperty, da);
                if (manual == true)
                {
                    manualAway.Tick += new EventHandler(manualawayAnim);
                    manualAway.Interval = 300;
                    manualAway.Start();
                    awayChanged = false;
                }
                DoubleAnimation db = new DoubleAnimation();
                db.From = 0;
                db.To = 1;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                db.BeginTime = TimeSpan.FromMilliseconds(300);
                db.RepeatBehavior = new RepeatBehavior(1.0);
                awayTeam.BeginAnimation(OpacityProperty, db);
            }
        }
        private void manualhomeAnim(object sender, EventArgs e)
        {
            manualHome.Stop();
            if (homeTeamchoice == 0)
            {
                HomeTeam.Source = new BitmapImage(new Uri("Data Elements/hometeam.png", UriKind.Relative));
            }
            if (homeTeamchoice == 1)
            {
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/Albany.png", UriKind.Relative));
            }
            if (homeTeamchoice == 2)
            {
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/binghampton.png", UriKind.Relative));
            }
            if (homeTeamchoice == 3)
            {
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/hartford.png", UriKind.Relative));
            }
            if (homeTeamchoice == 4)
            {
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/maine.png", UriKind.Relative));
            }
            if (homeTeamchoice == 5)
            {
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/umbc.png", UriKind.Relative));
            }
            if (homeTeamchoice == 6)
            {
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/umasslowell.png", UriKind.Relative));
            }
            if (homeTeamchoice == 7)
            {
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/unh.png", UriKind.Relative));
            }
            if (homeTeamchoice == 8)
            {
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/seawolves.png", UriKind.Relative));
            }
            if (homeTeamchoice == 9)
            {
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/vermont.png", UriKind.Relative));
            }
        }
        private void manualawayAnim(object sender, EventArgs e)
        {
            manualAway.Stop();
            if (awayTeamchoice == 0)
            {
                awayTeam.Source = new BitmapImage(new Uri("Data Elements/awayteam.png", UriKind.Relative));
            }
            if (awayTeamchoice == 1)
            {
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/Albany.png", UriKind.Relative));
            }
            if (awayTeamchoice == 2)
            {
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/binghampton.png", UriKind.Relative));
            }
            if (awayTeamchoice == 3)
            {
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/hartford.png", UriKind.Relative));
            }
            if (awayTeamchoice == 4)
            {
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/maine.png", UriKind.Relative));
            }
            if (awayTeamchoice == 5)
            {
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/umbc.png", UriKind.Relative));
            }
            if (awayTeamchoice == 6)
            {
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/umasslowell.png", UriKind.Relative));
            }
            if (awayTeamchoice == 7)
            {
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/unh.png", UriKind.Relative));
            }
            if (awayTeamchoice == 8)
            {
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/seawolves.png", UriKind.Relative));
            }
            if (awayTeamchoice == 9)
            {
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/vermont.png", UriKind.Relative));
            }
        }
        private void customInput(ref bool CustomInput)
        {
            custominputGrid.Visibility = Visibility.Visible;
            custominputGrid.Opacity = 0;
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.BeginTime = TimeSpan.FromMilliseconds(400);
            da.Duration = new Duration(TimeSpan.FromMilliseconds(500));
            da.RepeatBehavior = new RepeatBehavior(1.0);
            custominputGrid.BeginAnimation(OpacityProperty, da);
            Canvas.SetZIndex(custominputGrid, 1);
        }
        private void loadingfailed(ref bool loadingFailed)
        {
            if(custom == false)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 0;
                da.To = 1;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                internetGrid.BeginAnimation(OpacityProperty, da);

                ualbanyHome.IsEnabled = false;
                binghamptonHome.IsEnabled = false;
                hartfordHome.IsEnabled = false;
                maineHome.IsEnabled = false;
                umbcHome.IsEnabled = false;
                umasslowellHome.IsEnabled = false;
                newhampshireHome.IsEnabled = false;
                stonybrookHome.IsEnabled = false;
                vermontHome.IsEnabled = false;
                ualbanyAway.IsEnabled = false;
                binghamptonAway.IsEnabled = false;
                hartfordAway.IsEnabled = false;
                maineAway.IsEnabled = false;
                umbcAway.IsEnabled = false;
                umasslowellAway.IsEnabled = false;
                newhampshireAway.IsEnabled = false;
                stonybrookAway.IsEnabled = false;
                vermontAway.IsEnabled = false;
            }
            
        }
        private void manualInput(ref bool ManualInput)
        {
            ualbanyHome.IsEnabled = true;
            binghamptonHome.IsEnabled = true;
            hartfordHome.IsEnabled = true;
            maineHome.IsEnabled = true;
            umbcHome.IsEnabled = true;
            umasslowellHome.IsEnabled = true;
            newhampshireHome.IsEnabled = true;
            stonybrookHome.IsEnabled = true;
            vermontHome.IsEnabled = true;
            ualbanyAway.IsEnabled = true;
            binghamptonAway.IsEnabled = true;
            hartfordAway.IsEnabled = true;
            maineAway.IsEnabled = true;
            umbcAway.IsEnabled = true;
            umasslowellAway.IsEnabled = true;
            newhampshireAway.IsEnabled = true;
            stonybrookAway.IsEnabled = true;
            vermontAway.IsEnabled = true;

            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            da.RepeatBehavior = new RepeatBehavior(1.0);
            internetGrid.BeginAnimation(OpacityProperty, da);
            if (loadingLabel.Opacity != 0)
            {
                loadingLabel.BeginAnimation(OpacityProperty, da);
            }
            if(HomeSelected == true && AwaySelected == false)
            {
                homeRecordsGrid.BeginAnimation(OpacityProperty, da);
                if(awayTeamAwait.Opacity != 0)
                {
                    awayTeamAwait.BeginAnimation(OpacityProperty, da);
                }    
            }
            if (HomeSelected == false && AwaySelected == true)
            {
                if(homeTeamAwait.Opacity != 0)
                {
                    homeTeamAwait.BeginAnimation(OpacityProperty, da);
                }
                awayRecordsGrid.BeginAnimation(OpacityProperty, da);
            }
            if (HomeSelected == true && AwaySelected == true)
            {
                homeRecordsGrid.BeginAnimation(OpacityProperty, da);
                awayRecordsGrid.BeginAnimation(OpacityProperty, da);
            }
            if (HomeSelected == false && AwaySelected == false && manualClicked == false)
            {
                homeTeamAwait.BeginAnimation(OpacityProperty, da);
                awayTeamAwait.BeginAnimation(OpacityProperty, da);
            }   
            if (manualClicked == true)
            {
                loadingLabel.Visibility = Visibility.Hidden;
                homeTeamAwait.Visibility = Visibility.Hidden;
                awayTeamAwait.Visibility = Visibility.Hidden;
            }
            var delay = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            delay.Start();
            delay.Tick += (parent, args) =>
            {
                delay.Stop();
                internetGrid.Visibility = Visibility.Hidden;
            };
        }
        private void loadrecords(ref bool LoadRecords)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            da.RepeatBehavior = new RepeatBehavior(1.0);
            custominputGrid.BeginAnimation(OpacityProperty, da);
            var delay = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(300) };
            delay.Start();
            delay.Tick += (parent, args) =>
            {
                delay.Stop();
                DoubleAnimation db = new DoubleAnimation();
                db.From = 0;
                db.To = 1;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                loadingLabel.BeginAnimation(OpacityProperty, db);
            };
            custom = false;
            internet(this, new EventArgs());
        }
        private void loadscores(ref bool loadScores)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            da.RepeatBehavior = new RepeatBehavior(1.0);
            if (loadingdone == false && manual == false)
            {
                loadingLabel.BeginAnimation(OpacityProperty, da);
            }
            var delay = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(300) };
            delay.Start();
            delay.Tick += (parent, args) =>
            {
                delay.Stop();
                DoubleAnimation db = new DoubleAnimation();
                db.From = 0;
                db.To = 1;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                loadingLabel.Text = "\nLoading Successful";
                if (loadingdone == false && manual == false)
                {
                    loadingLabel.BeginAnimation(OpacityProperty, db);
                    var delay2 = new DispatcherTimer { Interval = TimeSpan.FromSeconds(4) };
                    delay2.Start();
                    delay2.Tick += (parent2, args2) =>
                    {
                        delay2.Stop();
                        if(manual == false)
                        {
                            loadingLabel.BeginAnimation(OpacityProperty, da);
                        }              
                        loadingdone = true;
                    };
                }
                if (homeSelect == false && awaySelect == false)
                {
                    homeTeamAwait.BeginAnimation(OpacityProperty, db);
                    awayTeamAwait.BeginAnimation(OpacityProperty, db);
                }
                if (homeSelect == true && awaySelect == false)
                {
                    hometextChanged(this, new EventArgs());
                    homeRecordsGrid.BeginAnimation(OpacityProperty, db);
                    awayTeamAwait.BeginAnimation(OpacityProperty, db);
                }
                if (homeSelect == false && awaySelect == true)
                {
                    awaytextChanged(this, new EventArgs());
                    homeTeamAwait.BeginAnimation(OpacityProperty, db);
                    awayRecordsGrid.BeginAnimation(OpacityProperty, db);
                }
                if (homeSelect == true && awaySelect == true)
                {
                    hometextChanged(this, new EventArgs());
                    awaytextChanged(this, new EventArgs());
                    homeRecordsGrid.BeginAnimation(OpacityProperty, db);
                    awayRecordsGrid.BeginAnimation(OpacityProperty, db);
                }
            };
        }
        private void Animation_Completed(object sender, EventArgs e)
        {
            animdone = true;
        }
        private void okButton_Click(object sender, RoutedEventArgs e)
        {           
            final(ref Final);
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            gobutton.IsEnabled = true;
            homewinsBox.IsEnabled = true;
            homelossesBox.IsEnabled = true;
            awaywinsBox.IsEnabled = true;
            awaylossesBox.IsEnabled = true;
            endAnimation(ref EndAnimation);
        }
        private void endAnimation (ref bool EndAnimation)
        {
            var windowsmall = new Storyboard();
            Window mainWindow = System.Windows.Application.Current.MainWindow;
            DoubleAnimation db = new DoubleAnimation();
            db.From = 1;
            db.To = 0;
            db.Duration = new Duration(TimeSpan.FromMilliseconds(800));
            db.BeginTime = TimeSpan.FromMilliseconds(1);
            db.RepeatBehavior = new RepeatBehavior(1.0);
            messageLabel.BeginAnimation(OpacityProperty, db);
            okButton.BeginAnimation(OpacityProperty, db);
            cancelButton.BeginAnimation(OpacityProperty, db);

            //var windowsmall = new Storyboard();
            var decHeight = new DoubleAnimation();

            decHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));

            decHeight.From = mainWindow.ActualHeight;
            decHeight.To = 410;

            Storyboard.SetTargetProperty(decHeight, new PropertyPath(MainWindow.HeightProperty));

            SineEase easingFunction = new SineEase();
            easingFunction.EasingMode = EasingMode.EaseInOut;
            decHeight.EasingFunction = easingFunction;

            windowsmall.Children.Add(decHeight);
            windowsmall.Completed += new EventHandler(Animation_Completed);
            windowsmall.Begin(this);
            animdone = false;
        }
        public int homeTeamChoices
        {
            get
            {
                return homeTeamchoice;
            }
            set
            {
                homeTeamChoices = homeTeamchoice;
            }
        }
        private void final (ref bool Final)
        {
            MainWindow Game_Start = new MainWindow();
            Game_Start.Show();
            this.Close();
            if (homeTeamchoice == 1)
            {
                Game_Start.homeTeamAlbany = Convert.ToString(homeTeamchoice);
            }
            if (homeTeamchoice == 2)
            {
                Game_Start.homeTeamBinghampton = Convert.ToString(homeTeamchoice);
            }
            if (homeTeamchoice == 3)
            {
                Game_Start.homeTeamHartford = Convert.ToString(homeTeamchoice);
            }
            if (homeTeamchoice == 4)
            {
                Game_Start.homeTeamMaine = Convert.ToString(homeTeamchoice);
            }
            if (homeTeamchoice == 5)
            {
                Game_Start.homeTeamUmbc = Convert.ToString(homeTeamchoice);
            }
            if (homeTeamchoice == 6)
            {
                Game_Start.homeTeamUmassLowell = Convert.ToString(homeTeamchoice);
            }
            if (homeTeamchoice == 7)
            {
                Game_Start.homeTeamUnh = Convert.ToString(homeTeamchoice);
            }
            if (homeTeamchoice == 8)
            {
                Game_Start.homeTeamStony = Convert.ToString(homeTeamchoice);
            }
            if (homeTeamchoice == 9)
            {
                Game_Start.homeTeamVermont = Convert.ToString(homeTeamchoice);
            }
            if (awayTeamchoice == 1)
            {
                Game_Start.awayTeamAlbany = Convert.ToString(awayTeamchoice);
            }
            if (awayTeamchoice == 2)
            {
                Game_Start.awayTeamBinghampton = Convert.ToString(awayTeamchoice);
            }
            if (awayTeamchoice == 3)
            {
                Game_Start.awayTeamHartford = Convert.ToString(awayTeamchoice);
            }
            if (awayTeamchoice == 4)
            {
                Game_Start.awayTeamMaine = Convert.ToString(awayTeamchoice);
            }
            if (awayTeamchoice == 5)
            {
                Game_Start.awayTeamUmbc = Convert.ToString(awayTeamchoice);
            }
            if (awayTeamchoice == 6)
            {
                Game_Start.awayTeamUmassLowell = Convert.ToString(awayTeamchoice);
            }
            if (awayTeamchoice == 7)
            {
                Game_Start.awayTeamUnh = Convert.ToString(awayTeamchoice);
            }
            if (awayTeamchoice == 8)
            {
                Game_Start.awayTeamStony = Convert.ToString(awayTeamchoice);
            }
            if (awayTeamchoice == 9)
            {
                Game_Start.awayTeamVermont = Convert.ToString(awayTeamchoice);
            }
            if (manual == true)
            {
                string StringhomeWins = homewinsBox.Text;
                string StringhomeLosses = homelossesBox.Text;
                string StringawayWins = awaywinsBox.Text;
                string StringawayLosses = awaylossesBox.Text;
                int inthomeWins = Convert.ToInt32(StringhomeWins);
                int inthomeLosses = Convert.ToInt32(StringhomeLosses);
                int intawayWins = Convert.ToInt32(StringawayWins);
                int intawayLosses = Convert.ToInt32(StringawayLosses);
                Game_Start.inthomeWins = inthomeWins;
                Game_Start.inthomeLosses = inthomeLosses;
                Game_Start.intawayWins = intawayWins;
                Game_Start.intawayLosses = intawayLosses;
            }
            if(manual == false)
            {
                string StringhomeWins = HomeConferenceWinsBox.Text;
                string StringhomeLosses = HomeConferenceLossesBox.Text;
                string StringawayWins = AwayConferenceWinsBox.Text;
                string StringawayLosses = AwayConferenceLossesBox.Text;
                int inthomeWins = Convert.ToInt32(StringhomeWins);
                int inthomeLosses = Convert.ToInt32(StringhomeLosses);
                int intawayWins = Convert.ToInt32(StringawayWins);
                int intawayLosses = Convert.ToInt32(StringawayLosses);
                Game_Start.inthomeWins = inthomeWins;
                Game_Start.inthomeLosses = inthomeLosses;
                Game_Start.intawayWins = intawayWins;
                Game_Start.intawayLosses = intawayLosses;
            }
            
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
        private void awaylossesBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key);
        }
        private void homewinsBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key);
        }

        private void homelossesBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key);
        }

        private void awaywinsBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key);
        }
        private void internet(object sender, EventArgs e)
        {
            try
            {
                string source = "http://www.espn.go.com";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(source);
                HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                request.Method = "GET";
                Stream receiveStream = res.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(receiveStream, encode);
                request.Timeout = 2000;
                request.KeepAlive = false;
                res = request.GetResponse() as HttpWebResponse;
                if (request.Method == "GET")
                {
                    ualbanyHome.IsEnabled = true;
                    binghamptonHome.IsEnabled = true;
                    hartfordHome.IsEnabled = true;
                    maineHome.IsEnabled = true;
                    umbcHome.IsEnabled = true;
                    umasslowellHome.IsEnabled = true;
                    newhampshireHome.IsEnabled = true;
                    stonybrookHome.IsEnabled = true;
                    vermontHome.IsEnabled = true;
                    ualbanyAway.IsEnabled = true;
                    binghamptonAway.IsEnabled = true;
                    hartfordAway.IsEnabled = true;
                    maineAway.IsEnabled = true;
                    umbcAway.IsEnabled = true;
                    umasslowellAway.IsEnabled = true;
                    newhampshireAway.IsEnabled = true;
                    stonybrookAway.IsEnabled = true;
                    vermontAway.IsEnabled = true;

                    internetScoresLoaded(ref InternetScoresLoaded);
                    res.Close();
                }
            }         
            catch(Exception ex)
            {
                loadingLabel.Text = "Error loading team records. Check your connection.\nDo you want to input manually?";
                if (custom == false)
                {
                    loadingfailed(ref loadingFailed);
                }
                if (custom == true)
                {
                    manualInput(ref ManualInput);
                }
            }
        }
        private void loadingpass(ref bool loadingPass)
        {
            internetGrid.Visibility = Visibility.Visible;
            internetGrid.Opacity = 0;
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            da.RepeatBehavior = new RepeatBehavior(1.0);
            tryagainButton.Content = "Go!";
            tryagainButton.IsEnabled = false;
            internetGrid.BeginAnimation(OpacityProperty, da);
        }
        private void manualButton_Click(object sender, RoutedEventArgs e)
        {
            customInput(ref CustomInput);
            custom = true;
            manual = true;
            Canvas.SetZIndex(custominputGrid, 2);
            Canvas.SetZIndex(internetGrid, 1);
            loadingfailed(ref loadingFailed);
        }
        private void loadRecords_Click(object sender, RoutedEventArgs e)
        {
            manual = false;
            loadrecords(ref LoadRecords);
            loadingdone = false;
            Canvas.SetZIndex(internetGrid, 2);
            Canvas.SetZIndex(custominputGrid, 1);
            //tryagainButton.IsEnabled = true;
        }

        private void tryagainButton_Click(object sender, RoutedEventArgs e)
        {
            if(readyInternet == false)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 1;
                da.To = 0;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                internetGrid.BeginAnimation(OpacityProperty, da);
                internet(this, new EventArgs());
            }
            if(readyInternet == true)
            {
                final(ref Final);
            }
            
        }
        private void internetScoresLoaded(ref bool InternetScoresLoaded)
        {
            internetWorked = true;
            WebClient web = new WebClient();
            string pattern = @"</strong></li><li>\s*(.*?)\s*</li></ul></div>";
            string s1 = @"(\d+)";
            Regex find1 = new Regex(pattern, RegexOptions.Singleline);
            Regex find2 = new Regex(s1, RegexOptions.None);

            String htmlAlbany = web.DownloadString("http://www.espn.go.com/mens-college-basketball/team/_/id/399/albany-great-danes.html");
            Match matchAlbany = find1.Match(htmlAlbany);

            string matchAlbanyString = matchAlbany.Groups[1].Value;
            var matchAlbanyV1 = find2.Match(matchAlbanyString);
            var matchAlbanyV2 = matchAlbanyV1.NextMatch();
            var matchAlbanyV3 = matchAlbanyV2.NextMatch();
            var matchAlbanyV4 = matchAlbanyV3.NextMatch();
            matchAlbanyS1 = matchAlbanyV1.Groups[1].Value;
            matchAlbanyS2 = matchAlbanyV2.Groups[1].Value;
            matchAlbanyS3 = matchAlbanyV3.Groups[1].Value;
            matchAlbanyS4 = matchAlbanyV4.Groups[1].Value;

            String htmlBinghampton = web.DownloadString("http://www.espn.go.com/mens-college-basketball/team/_/id/2066/binghamton-bearcats");
            Match matchBinghampton = find1.Match(htmlBinghampton);

            string matchBinghamptonString = matchBinghampton.Groups[1].Value;
            var matchBinghamptonV1 = find2.Match(matchBinghamptonString);
            var matchBinghamptonV2 = matchBinghamptonV1.NextMatch();
            var matchBinghamptonV3 = matchBinghamptonV2.NextMatch();
            var matchBinghamptonV4 = matchBinghamptonV3.NextMatch();
            matchBinghamptonS1 = matchBinghamptonV1.Groups[1].Value;
            matchBinghamptonS2 = matchBinghamptonV2.Groups[1].Value;
            matchBinghamptonS3 = matchBinghamptonV3.Groups[1].Value;
            matchBinghamptonS4 = matchBinghamptonV4.Groups[1].Value;

            String htmlHartford = web.DownloadString("http://espn.go.com/mens-college-basketball/team/_/id/42/hartford-hawks");
            Match matchHartford = find1.Match(htmlHartford);

            string matchHartfordString = matchHartford.Groups[1].Value;
            var matchHartfordV1 = find2.Match(matchHartfordString);
            var matchHartfordV2 = matchHartfordV1.NextMatch();
            var matchHartfordV3 = matchHartfordV2.NextMatch();
            var matchHartfordV4 = matchHartfordV3.NextMatch();
            matchHartfordS1 = matchHartfordV1.Groups[1].Value;
            matchHartfordS2 = matchHartfordV2.Groups[1].Value;
            matchHartfordS3 = matchHartfordV3.Groups[1].Value;
            matchHartfordS4 = matchHartfordV4.Groups[1].Value;

            String htmlMaine = web.DownloadString("http://espn.go.com/mens-college-basketball/team/_/id/311/maine-black-bears");
            Match matchMaine = find1.Match(htmlMaine);

            string matchMaineString = matchMaine.Groups[1].Value;
            var matchMaineV1 = find2.Match(matchMaineString);
            var matchMaineV2 = matchMaineV1.NextMatch();
            var matchMaineV3 = matchMaineV2.NextMatch();
            var matchMaineV4 = matchMaineV3.NextMatch();
            matchMaineS1 = matchMaineV1.Groups[1].Value;
            matchMaineS2 = matchMaineV2.Groups[1].Value;
            matchMaineS3 = matchMaineV3.Groups[1].Value;
            matchMaineS4 = matchMaineV4.Groups[1].Value;

            String htmlUmbc = web.DownloadString("http://espn.go.com/mens-college-basketball/team/_/id/2378/umbc-retrievers");
            Match matchUmbc = find1.Match(htmlUmbc);

            string matchUmbcString = matchUmbc.Groups[1].Value;
            var matchUmbcV1 = find2.Match(matchUmbcString);
            var matchUmbcV2 = matchUmbcV1.NextMatch();
            var matchUmbcV3 = matchUmbcV2.NextMatch();
            var matchUmbcV4 = matchUmbcV3.NextMatch();
            matchUmbcS1 = matchUmbcV1.Groups[1].Value;
            matchUmbcS2 = matchUmbcV2.Groups[1].Value;
            matchUmbcS3 = matchUmbcV3.Groups[1].Value;
            matchUmbcS4 = matchUmbcV4.Groups[1].Value;

            String htmlUmasslowell = web.DownloadString("http://espn.go.com/mens-college-basketball/team/_/id/2378/Umasslowell-retrievers");
            Match matchUmasslowell = find1.Match(htmlUmasslowell);

            string matchUmasslowellString = matchUmasslowell.Groups[1].Value;
            var matchUmasslowellV1 = find2.Match(matchUmasslowellString);
            var matchUmasslowellV2 = matchUmasslowellV1.NextMatch();
            var matchUmasslowellV3 = matchUmasslowellV2.NextMatch();
            var matchUmasslowellV4 = matchUmasslowellV3.NextMatch();
            matchUmasslowellS1 = matchUmasslowellV1.Groups[1].Value;
            matchUmasslowellS2 = matchUmasslowellV2.Groups[1].Value;
            matchUmasslowellS3 = matchUmasslowellV3.Groups[1].Value;
            matchUmasslowellS4 = matchUmasslowellV4.Groups[1].Value;

            String htmlUnh = web.DownloadString("http://espn.go.com/mens-college-basketball/team/_/id/160/new-hampshire-wildcats");
            Match matchUnh = find1.Match(htmlUnh);

            string matchUnhString = matchUnh.Groups[1].Value;
            var matchUnhV1 = find2.Match(matchUnhString);
            var matchUnhV2 = matchUnhV1.NextMatch();
            var matchUnhV3 = matchUnhV2.NextMatch();
            var matchUnhV4 = matchUnhV3.NextMatch();
            matchUnhS1 = matchUnhV1.Groups[1].Value;
            matchUnhS2 = matchUnhV2.Groups[1].Value;
            matchUnhS3 = matchUnhV3.Groups[1].Value;
            matchUnhS4 = matchUnhV4.Groups[1].Value;

            String htmlStonyBrook = web.DownloadString("http://espn.go.com/mens-college-basketball/team/_/id/2619/stony-brook-seawolves");
            Match matchStonyBrook = find1.Match(htmlStonyBrook);

            string matchStonyBrookString = matchStonyBrook.Groups[1].Value;
            var matchStonyBrookV1 = find2.Match(matchStonyBrookString);
            var matchStonyBrookV2 = matchStonyBrookV1.NextMatch();
            var matchStonyBrookV3 = matchStonyBrookV2.NextMatch();
            var matchStonyBrookV4 = matchStonyBrookV3.NextMatch();
            matchStonyBrookS1 = matchStonyBrookV1.Groups[1].Value;
            matchStonyBrookS2 = matchStonyBrookV2.Groups[1].Value;
            matchStonyBrookS3 = matchStonyBrookV3.Groups[1].Value;
            matchStonyBrookS4 = matchStonyBrookV4.Groups[1].Value;

            String htmlVermont = web.DownloadString("http://espn.go.com/mens-college-basketball/team/_/id/261/vermont-catamounts");
            Match matchVermont = find1.Match(htmlVermont);

            string matchVermontString = matchVermont.Groups[1].Value;
            var matchVermontV1 = find2.Match(matchVermontString);
            var matchVermontV2 = matchVermontV1.NextMatch();
            var matchVermontV3 = matchVermontV2.NextMatch();
            var matchVermontV4 = matchVermontV3.NextMatch();
            matchVermontS1 = matchVermontV1.Groups[1].Value;
            matchVermontS2 = matchVermontV2.Groups[1].Value;
            matchVermontS3 = matchVermontV3.Groups[1].Value;
            matchVermontS4 = matchVermontV4.Groups[1].Value;
            
            loadingpass(ref loadingPass);
            loadscores(ref loadScores);
        }
        private void internetScores(ref bool InternetScores)
        { 
            if(homeChanged == true)
            {
                homedelay.Tick += new EventHandler(hometextChanged);
                homedelay.Interval = 300;
                homedelay.Start();
            }
            if (awayChanged == true)
            {
                awaydelay.Tick += new EventHandler(awaytextChanged);
                awaydelay.Interval = 300;
                awaydelay.Start();
            }
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            da.RepeatBehavior = new RepeatBehavior(1.0);
            //da.Completed += new EventHandler(hometextChanged);
            DoubleAnimation db = new DoubleAnimation();
            db.From = 0;
            db.To = 1;
            db.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            db.BeginTime = TimeSpan.FromMilliseconds(300);
            db.RepeatBehavior = new RepeatBehavior(1.0);
            var delay = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(300) };
            if (awaySelect == true && awayChanged == true)
            {
                awayRecordsGrid.BeginAnimation(OpacityProperty, da);
                awayRecordsGrid.BeginAnimation(OpacityProperty, db);
            }
            if (homeSelect == true && homeChanged == true)
            {
                homeRecordsGrid.BeginAnimation(OpacityProperty, da);
                homeRecordsGrid.BeginAnimation(OpacityProperty, db);
            }
            if (homeSelect == true && awaySelect == true)
            {
                bothSelect = true;
            }
            homeChanged = false;
        }
        private void homeSelected(ref bool HomeSelected)
        {
            if(HomeSelected == false)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 1;
                da.To = 0;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                if(homeTeamAwait.Opacity != 0)
                {
                    homeTeamAwait.BeginAnimation(OpacityProperty, da);
                }     
            }
            homeSelect = true;
            HomeSelected = true;
        }
        private void awaySelected(ref bool AwaySelected)
        {
            if (AwaySelected == false)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 1;
                da.To = 0;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                if(awayTeamAwait.Opacity != 0)
                {
                    awayTeamAwait.BeginAnimation(OpacityProperty, da);
                }     
            }
            AwaySelected = true;
            awaySelect = true;
        }
        private void hometextChanged(object sender, EventArgs e)
        {
            homedelay.Stop();
            if (homeTeamchoice == 0)
            {
                HomeTeamName = "Home Team";
                HomeOverallWinsBox.Text = null;
                HomeOverallLossesBox.Text = null;
                HomeConferenceWinsBox.Text = null;
                HomeConferenceLossesBox.Text = null;
            }
            if (homeTeamchoice == 1)
            {
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/Albany.png", UriKind.Relative));
                HomeTeamName = "Great Danes";
                HomeOverallWins = matchAlbanyS1;
                HomeOverallLosses = matchAlbanyS2;
                HomeConferenceWins = matchAlbanyS3;
                HomeConferenceLosses = matchAlbanyS4;
            }
            if (homeTeamchoice == 2)
            {
                HomeTeamName = "Bearcats";
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/binghampton.png", UriKind.Relative));
                HomeOverallWins = matchBinghamptonS1;
                HomeOverallLosses = matchBinghamptonS2;
                HomeConferenceWins = matchBinghamptonS3;
                HomeConferenceLosses = matchBinghamptonS4;
            }
            if (homeTeamchoice == 3)
            {
                HomeTeamName = "Hawks";
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/hartford.png", UriKind.Relative));
                HomeOverallWins = matchHartfordS1;
                HomeOverallLosses = matchHartfordS2;
                HomeConferenceWins = matchHartfordS3;
                HomeConferenceLosses = matchHartfordS4;
            }
            if (homeTeamchoice == 4)
            {
                HomeTeamName = "Black Bears";
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/maine.png", UriKind.Relative));
                HomeOverallWins = matchMaineS1;
                HomeOverallLosses = matchMaineS2;
                HomeConferenceWins = matchMaineS3;
                HomeConferenceLosses = matchMaineS4;
            }
            if (homeTeamchoice == 5)
            {
                HomeTeamName = "Retreivers";
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/umbc.png", UriKind.Relative));
                HomeOverallWins = matchUmbcS1;
                HomeOverallLosses = matchUmbcS2;
                HomeConferenceWins = matchUmbcS3;
                HomeConferenceLosses = matchUmbcS4;
            }
            if (homeTeamchoice == 6)
            {
                HomeTeamName = "River Hawks";
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/umasslowell.png", UriKind.Relative));
                HomeOverallWins = matchUmasslowellS1;
                HomeOverallLosses = matchUmasslowellS2;
                HomeConferenceWins = matchUmasslowellS3;
                HomeConferenceLosses = matchUmasslowellS4;
            }
            if (homeTeamchoice == 7)
            {
                HomeTeamName = "Wildcats";
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/unh.png", UriKind.Relative));
                HomeOverallWins = matchUnhS1;
                HomeOverallLosses = matchUnhS2;
                HomeConferenceWins = matchUnhS3;
                HomeConferenceLosses = matchUnhS4;
            }
            if (homeTeamchoice == 8)
            {
                HomeTeamName = "Seawolves";
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/seawolves.png", UriKind.Relative));
                HomeOverallWins = matchStonyBrookS1;
                HomeOverallLosses = matchStonyBrookS2;
                HomeConferenceWins = matchStonyBrookS3;
                HomeConferenceLosses = matchStonyBrookS4;
            }
            if (homeTeamchoice == 9)
            {
                HomeTeamName = "Catamounts";
                HomeTeam.Source = new BitmapImage(new Uri("Team Data/America East/vermont.png", UriKind.Relative));
                HomeOverallWins = matchVermontS1;
                HomeOverallLosses = matchVermontS2;
                HomeConferenceWins = matchVermontS3;
                HomeConferenceLosses = matchVermontS4;
            }
            if (homeTeamchoice >= 0)
            {
                int hOverallWinsInt = int.Parse(HomeOverallWins);
                int hOverallLossesInt = int.Parse(HomeOverallLosses);
                int hConferenceWinsInt = int.Parse(HomeConferenceWins);
                int hConferenceLossesInt = int.Parse(HomeConferenceLosses);
                if(hOverallWinsInt < 10)
                {
                    HomeOverallWins = " " + HomeOverallWins;
                }
                if (hConferenceWinsInt < 10)
                {
                    HomeConferenceWins = " " + HomeConferenceWins;
                }
                HomeOverallWinsBox.Text = HomeOverallWins;
                HomeOverallLossesBox.Text = HomeOverallLosses;
                HomeConferenceWinsBox.Text = HomeConferenceWins;
                HomeConferenceLossesBox.Text = HomeConferenceLosses;
                homeTeamOverall.Text = HomeTeamName + "\nOverall Record";
                homeTeamConference.Text = HomeTeamName + "\nConference Record";
            }
        }
        private void awaytextChanged(object sender, EventArgs e)
        {
            awaydelay.Stop();
            if (awayTeamchoice == 0)
            {
                AwayTeamName = "Away Team";
                AwayOverallWinsBox.Text = null;
                AwayOverallLossesBox.Text = null;
                AwayConferenceWinsBox.Text = null;
                AwayConferenceLossesBox.Text = null;
            }
            if (awayTeamchoice == 1)
            {
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/Albany.png", UriKind.Relative));
                AwayTeamName = "Great Danes";
                AwayOverallWins = matchAlbanyS1;
                AwayOverallLosses = matchAlbanyS2;
                AwayConferenceWins = matchAlbanyS3;
                AwayConferenceLosses = matchAlbanyS4;
            }
            if (awayTeamchoice == 2)
            {
                AwayTeamName = "Bearcats";
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/binghampton.png", UriKind.Relative));
                AwayOverallWins = matchBinghamptonS1;
                AwayOverallLosses = matchBinghamptonS2;
                AwayConferenceWins = matchBinghamptonS3;
                AwayConferenceLosses = matchBinghamptonS4;
            }
            if (awayTeamchoice == 3)
            {
                AwayTeamName = "Hawks";
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/hartford.png", UriKind.Relative));
                AwayOverallWins = matchHartfordS1;
                AwayOverallLosses = matchHartfordS2;
                AwayConferenceWins = matchHartfordS3;
                AwayConferenceLosses = matchHartfordS4;
            }
            if (awayTeamchoice == 4)
            {
                AwayTeamName = "Black Bears";
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/maine.png", UriKind.Relative));
                AwayOverallWins = matchMaineS1;
                AwayOverallLosses = matchMaineS2;
                AwayConferenceWins = matchMaineS3;
                AwayConferenceLosses = matchMaineS4;
            }
            if (awayTeamchoice == 5)
            {
                AwayTeamName = "Retreivers";
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/umbc.png", UriKind.Relative));
                AwayOverallWins = matchUmbcS1;
                AwayOverallLosses = matchUmbcS2;
                AwayConferenceWins = matchUmbcS3;
                AwayConferenceLosses = matchUmbcS4;
            }
            if (awayTeamchoice == 6)
            {
                AwayTeamName = "River Hawks";
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/umasslowell.png", UriKind.Relative));
                AwayOverallWins = matchUmasslowellS1;
                AwayOverallLosses = matchUmasslowellS2;
                AwayConferenceWins = matchUmasslowellS3;
                AwayConferenceLosses = matchUmasslowellS4;
            }
            if (awayTeamchoice == 7)
            {
                AwayTeamName = "Wildcats";
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/unh.png", UriKind.Relative));
                AwayOverallWins = matchUnhS1;
                AwayOverallLosses = matchUnhS2;
                AwayConferenceWins = matchUnhS3;
                AwayConferenceLosses = matchUnhS4;
            }
            if (awayTeamchoice == 8)
            {
                AwayTeamName = "Seawolves";
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/seawolves.png", UriKind.Relative));
                AwayOverallWins = matchStonyBrookS1;
                AwayOverallLosses = matchStonyBrookS2;
                AwayConferenceWins = matchStonyBrookS3;
                AwayConferenceLosses = matchStonyBrookS4;
            }
            if (awayTeamchoice == 9)
            {
                AwayTeamName = "Catamounts";
                awayTeam.Source = new BitmapImage(new Uri("Team Data/America East/vermont.png", UriKind.Relative));
                AwayOverallWins = matchVermontS1;
                AwayOverallLosses = matchVermontS2;
                AwayConferenceWins = matchVermontS3;
                AwayConferenceLosses = matchVermontS4;
            }
            if (awayTeamchoice >= 0)
            {
                int hOverallWinsInt = int.Parse(AwayOverallWins);
                int hOverallLossesInt = int.Parse(AwayOverallLosses);
                int hConferenceWinsInt = int.Parse(AwayConferenceWins);
                int hConferenceLossesInt = int.Parse(AwayConferenceLosses);
                if (hOverallWinsInt < 10)
                {
                    AwayOverallWins = " " + AwayOverallWins;
                }
                if (hConferenceWinsInt < 10)
                {
                    AwayConferenceWins = " " + AwayConferenceWins;
                }
                AwayOverallWinsBox.Text = AwayOverallWins;
                AwayOverallLossesBox.Text = AwayOverallLosses;
                AwayConferenceWinsBox.Text = AwayConferenceWins;
                AwayConferenceLossesBox.Text = AwayConferenceLosses;
                awayTeamOverall.Text = AwayTeamName + "\nOverall Record";
                awayTeamConference.Text = AwayTeamName + "\nConference Record";
            }
        }
    }
}
