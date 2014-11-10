#region System Libraries
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Documents;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Threading;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.DataVisualization;
#endregion
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        #region Begin statements
        #region Classes
        DateTime Time = new DateTime();
        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        DispatcherTimer delay = new DispatcherTimer();
        DispatcherTimer hcHome = new DispatcherTimer();
        DispatcherTimer hcAway = new DispatcherTimer();

        DispatcherTimer graphUpdateTimer = new DispatcherTimer();
        DispatcherTimer graphMarkerRemove = new DispatcherTimer();

        System.Windows.Media.Color homecolor = new System.Windows.Media.Color();
        System.Windows.Media.Color awaycolor = new System.Windows.Media.Color();

        SolidColorBrush homebrush = new SolidColorBrush();
        SolidColorBrush awaybrush = new SolidColorBrush();

        BitmapImage bmpImage = new BitmapImage();
       
        RadialGradientBrush buzzerHomebrush = new RadialGradientBrush();
        RadialGradientBrush buzzerAwaybrush = new RadialGradientBrush();

        //CHART LINES
        ObservableCollection<KeyValuePair<double, double>> HomeSeriesMarker = new ObservableCollection<KeyValuePair<double, double>>();
        ObservableCollection<KeyValuePair<double, double>> HomeSeriesPoints = new ObservableCollection<KeyValuePair<double, double>>();
        ObservableCollection<KeyValuePair<double, double>> HomeSeries = new ObservableCollection<KeyValuePair<double, double>>();
        ObservableCollection<KeyValuePair<double, double>> AwaySeriesMarker = new ObservableCollection<KeyValuePair<double, double>>();
        ObservableCollection<KeyValuePair<double, double>> AwaySeriesPoints = new ObservableCollection<KeyValuePair<double, double>>();
        ObservableCollection<KeyValuePair<double, double>> AwaySeries = new ObservableCollection<KeyValuePair<double, double>>();

        LinkedList<KeyValuePair<double, double>> HomePointsPair = new LinkedList<KeyValuePair<double, double>>();
        LinkedList<KeyValuePair<double, double>> AwayPointsPair = new LinkedList<KeyValuePair<double, double>>();

        LineSeries HomeLineMarker = new LineSeries();
        LineSeries HomeLinePoints = new LineSeries();
        LineSeries HomeLine = new LineSeries();

        RadialGradientBrush HomePointRGB = new RadialGradientBrush();

        Style HomePointsToolStyle = new Style();
        ControlTemplate HomePointsToolTemp = new ControlTemplate(typeof(System.Windows.Controls.ToolTip));
        Setter HomePointsToolSetter = new Setter();
        FrameworkElementFactory HomePointsToolGrid = new FrameworkElementFactory(typeof(Grid));
        //FrameworkElementFactory HomePointsToolBorder = new FrameworkElementFactory(typeof(Border));
        //Grid HomePointsToolGrid = new Grid();
        //Border HomePointsToolBorder = new Border();
        //System.Windows.Controls.Label HomePointsToolText = new System.Windows.Controls.Label();

        //TEST
        HomeBrushClass hbc = new HomeBrushClass();
        //TEST

        FrameworkElementFactory HomeCCTime = new FrameworkElementFactory(typeof(System.Windows.Controls.Label));
        FrameworkElementFactory HomeCCPerc = new FrameworkElementFactory(typeof(System.Windows.Controls.Label));
        FrameworkElementFactory HomeStackPanel = new FrameworkElementFactory(typeof(Border));

        Style AwayPointsToolStyle = new Style(typeof(LineDataPoint));        
        Setter AwayPointsToolSetter = new Setter();
        ControlTemplate AwayPointsToolTemp = new ControlTemplate(typeof(LineDataPoint));
        FrameworkElementFactory AwayPointsToolGrid = new FrameworkElementFactory(typeof(Grid));
        System.Windows.Controls.Label AwayCCTime = new System.Windows.Controls.Label();
       // LinkedList<ContentControl> AwayCCPerc = new LinkedList<ContentControl>();
        Grid AwayTTGrid = new Grid();
        System.Windows.Shapes.Rectangle AwayTTRect = new System.Windows.Shapes.Rectangle();
        ContentControl AwayTTBlock = new ContentControl();


        ContentControl AwayCCPerc = new ContentControl();
        StackPanel AwayStackPanel = new StackPanel();
        FrameworkElementFactory AwayPointsEllipse = new FrameworkElementFactory(typeof(Ellipse));

        Style HomePolyLineNull = new System.Windows.Style();
        Style AwayPolyLineNull = new System.Windows.Style();
        Setter PolyLineNull = new Setter();

        LineSeries AwayLineMarker = new LineSeries();
        LineSeries AwayLinePoints = new LineSeries();
        LineSeries AwayLine = new LineSeries();

        //CHART LEGEND
        Style XAxisLegend = new Style(typeof(LinearAxis));
        ControlTemplate XAxisLegendTemp = new ControlTemplate(typeof(LinearAxis));

        FrameworkElementFactory XAxisLegendBorder = new FrameworkElementFactory(typeof(Border));
        FrameworkElementFactory XAxisLegendGrid = new FrameworkElementFactory(typeof(System.Windows.Controls.Grid));

        FrameworkElementFactory LegendZone1 = new FrameworkElementFactory(typeof(ColumnDefinition));
        FrameworkElementFactory LegendZone2 = new FrameworkElementFactory(typeof(ColumnDefinition));

        FrameworkElementFactory XAxisHomeLegendLabel1 = new FrameworkElementFactory(typeof(System.Windows.Controls.Label));
        FrameworkElementFactory XAxisHomeLegendLabel2 = new FrameworkElementFactory(typeof(System.Windows.Controls.Label));
        FrameworkElementFactory XAxisAwayLegendLabel1 = new FrameworkElementFactory(typeof(System.Windows.Controls.Label));
        FrameworkElementFactory XAxisAwayLegendLabel2 = new FrameworkElementFactory(typeof(System.Windows.Controls.Label));
        FrameworkElementFactory XAxisHomeLegendImage = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));
        FrameworkElementFactory XAxisAwayLegendImage = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));

        //Bottom B2 Axis (Zones)
        Style XAxisZoneStyle = new Style(typeof(LinearAxis));
        ControlTemplate XAxisZoneTemp = new ControlTemplate(typeof(LinearAxis));
        FrameworkElementFactory XAxisZoneGrid = new FrameworkElementFactory(typeof(System.Windows.Controls.Grid));
        FrameworkElementFactory Zone1 = new FrameworkElementFactory(typeof(ColumnDefinition));
        FrameworkElementFactory Zone2 = new FrameworkElementFactory(typeof(ColumnDefinition));

        FrameworkElementFactory XAxisZoneMinBorder = new FrameworkElementFactory(typeof(Border));
        FrameworkElementFactory XAxisZoneMinLabel = new FrameworkElementFactory(typeof(System.Windows.Controls.Label));
        FrameworkElementFactory XAxisZone1Label = new FrameworkElementFactory(typeof(System.Windows.Controls.Label));
        FrameworkElementFactory XAxisZone2Label = new FrameworkElementFactory(typeof(System.Windows.Controls.Label));

        FrameworkElementFactory XAxisZone1Line1 = new FrameworkElementFactory(typeof(Line));
        FrameworkElementFactory XAxisZone1Line2 = new FrameworkElementFactory(typeof(Line));
        FrameworkElementFactory XAxisZone1Line3 = new FrameworkElementFactory(typeof(Line));
        FrameworkElementFactory XAxisZone2Line1 = new FrameworkElementFactory(typeof(Line));
        FrameworkElementFactory XAxisZone2Line2 = new FrameworkElementFactory(typeof(Line));
        FrameworkElementFactory XAxisZone2Line3 = new FrameworkElementFactory(typeof(Line));
        Setter XAxisZoneSetter = new Setter();

        //Chart Logos
        FrameworkElementFactory HomeChartLogoGrid = new FrameworkElementFactory(typeof(System.Windows.Controls.Grid));
        FrameworkElementFactory AwayChartLogoGrid = new FrameworkElementFactory(typeof(System.Windows.Controls.Grid));

        FrameworkElementFactory HomeChartLogo = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));
        FrameworkElementFactory AwayChartLogo = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));
        
        ControlTemplate HomeChartLogoTemp = new ControlTemplate(typeof(LineDataPoint));
        ControlTemplate AwayChartLogoTemp = new ControlTemplate(typeof(LineDataPoint));

        //Chart Background
        FrameworkElementFactory ChartBackgroundGrid = new FrameworkElementFactory(typeof(System.Windows.Controls.Grid));
        FrameworkElementFactory ChartBackgroundBorder = new FrameworkElementFactory(typeof(Border));
        ControlTemplate ChartBackgroundTemp = new ControlTemplate(typeof(LinearAxis));
        FrameworkElementFactory ChartBackgroundHomeImage = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));
        FrameworkElementFactory ChartBackgroundAwayImage = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));
        FrameworkElementFactory ChartBackgroundDivider = new FrameworkElementFactory(typeof(Line));

        FrameworkElementFactory HChartBackgroundGrid = new FrameworkElementFactory(typeof(System.Windows.Controls.Grid));
        FrameworkElementFactory HChartBackgroundBorder = new FrameworkElementFactory(typeof(Border));
        ControlTemplate HChartBackgroundTemp = new ControlTemplate(typeof(LinearAxis));
        FrameworkElementFactory HChartBackgroundHomeImage = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));
        FrameworkElementFactory HChartHomePoly = new FrameworkElementFactory(typeof(Polygon));
        Style HChartBackgroundStyle = new Style(typeof(LinearAxis));
        Setter HChartBackgroundSetter = new Setter();

        FrameworkElementFactory AChartBackgroundGrid = new FrameworkElementFactory(typeof(System.Windows.Controls.Grid));
        FrameworkElementFactory AChartBackgroundBorder = new FrameworkElementFactory(typeof(Border));
        ControlTemplate AChartBackgroundTemp = new ControlTemplate(typeof(LinearAxis));
        FrameworkElementFactory AChartBackgroundAwayImage = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));
        FrameworkElementFactory AChartAwayPoly = new FrameworkElementFactory(typeof(Polygon));
        Style AChartBackgroundStyle = new Style(typeof(LinearAxis));
        Setter AChartBackgroundSetter = new Setter();

        FrameworkElementFactory ChartHomePoly = new FrameworkElementFactory(typeof(Polygon));
        FrameworkElementFactory ChartAwayPoly = new FrameworkElementFactory(typeof(Polygon));

        PointCollection HomePolyCollection = new PointCollection();
        PointCollection AwayPolyCollection = new PointCollection();
        PointCollection FullCollection = new PointCollection();

        LinearGradientBrush DividerBrush = new LinearGradientBrush();
        Setter ChartBackgroundSetter = new Setter();
        Style ChartBackgroundStyle = new Style(typeof(LinearAxis));

        PathGeometry HomeBackgroundClip = new PathGeometry();
        PathGeometry AwayBackgroundClip = new PathGeometry();

        GradientStop HomeBackStopFade = new GradientStop();
        GradientStop HomeBackStopFull = new GradientStop();
        GradientStop AwayBackStopFull = new GradientStop();
        GradientStop AwayBackStopFade = new GradientStop();

        RadialGradientBrush ChartHomeRGB = new RadialGradientBrush();
        RadialGradientBrush ChartAwayRGB = new RadialGradientBrush();

        RadialGradientBrush HChartHomeRGB = new RadialGradientBrush();
        RadialGradientBrush AChartAwayRGB = new RadialGradientBrush();

        GradientStop HomeRGB1 = new GradientStop();
        GradientStop HomeRGB2 = new GradientStop();
        GradientStop AwayRGB1 = new GradientStop();
        GradientStop AwayRGB2 = new GradientStop();

        //Slider Controls
        SliderImages sliderImages = new SliderImages();
        LinearGradientBrush SliderGradient = new LinearGradientBrush();
        GradientStop SliderGradientH1 = new GradientStop();
        GradientStop SliderGradientH2 = new GradientStop();
        GradientStop SliderGradientA1 = new GradientStop();
        GradientStop SliderGradientA2 = new GradientStop();
                
        FrameworkElementFactory SliderThumb = new FrameworkElementFactory(typeof(Thumb));
        FrameworkElementFactory SliderGrid = new FrameworkElementFactory(typeof(Grid));
        FrameworkElementFactory SliderBorder = new FrameworkElementFactory(typeof(Border));
        FrameworkElementFactory SliderBackground = new FrameworkElementFactory(typeof(System.Windows.Shapes.Rectangle));
        FrameworkElementFactory homeSliderSelection = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));
        FrameworkElementFactory awaySliderSelection = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));
        FrameworkElementFactory SliderTrack = new FrameworkElementFactory(typeof(Track));
        Setter SliderSetter = new Setter();
        ControlTemplate SliderTemp = new ControlTemplate(typeof(Slider));
        Style CustomSliderStyle = new Style();

        Style ThumbDisabledStyle = new System.Windows.Style();
        Setter ThumbDisabledSetter = new Setter();
        ControlTemplate ThumbDisabledTemp = new ControlTemplate(typeof(Thumb));
        FrameworkElementFactory ThumbDisabledImage = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));
        FrameworkElementFactory ThumbDisabledGrid = new FrameworkElementFactory(typeof(Grid));

        Style ThumbStyle = new System.Windows.Style();
        Setter ThumbSetter = new Setter();
        ControlTemplate ThumbTemp = new ControlTemplate(typeof(Thumb));
        FrameworkElementFactory ThumbImage = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));
        FrameworkElementFactory ThumbGrid = new FrameworkElementFactory(typeof(Grid));

        //Other Chart Properties
        LinearAxis LegendAxis = new LinearAxis();
        LinearAxis BackgroundAxis = new LinearAxis();

        LinearAxis yaxis = new LinearAxis();
        LinearAxis yaxis2 = new LinearAxis();
        
        LinearAxis xaxis = new LinearAxis();
        LinearAxis XAxisZone = new LinearAxis();
        Setter XAxisLegendSetter = new Setter();

        Style XAxisHomePointStyle = new Style(typeof(System.Windows.Controls.DataVisualization.Charting.DataPoint));
        Style XAxisAwayPointStyle = new Style(typeof(System.Windows.Controls.DataVisualization.Charting.DataPoint));
        Style HomeLineStyle = new Style();
        Style AwayLineStyle = new Style();
        Style HomeLinePointStyle = new Style();

        Setter HomeLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty,
                                    new SolidColorBrush(System.Windows.Media.Color.FromArgb(0,0,0,0)));
        Setter AwayLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty,
                                    new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0)));
        Setter HomeLogoSetter = new Setter();
        Setter AwayLogoSetter = new Setter();

        FrameworkElementFactory HomePointEllipse = new FrameworkElementFactory(typeof(Ellipse));
        FrameworkElementFactory HomePointGrid = new FrameworkElementFactory(typeof(Grid));
        Setter HomePointSetter = new Setter();
        ControlTemplate HomePointTemp = new ControlTemplate();

        
        
        Setter NullMarker = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.TemplateProperty, null);

        SolidColorBrush ZoneBrush = new SolidColorBrush();

        ColumnDefinition columnLeft1 = new ColumnDefinition();
        ColumnDefinition columnLeft2 = new ColumnDefinition();
        ColumnDefinition columnRight1 = new ColumnDefinition();
        ColumnDefinition columnRight2 = new ColumnDefinition();

        //HomeSliderImage test1 = new HomeSliderImage();

        System.Windows.Forms.DataVisualization.Charting.Title mainTitle = new System.Windows.Forms.DataVisualization.Charting.Title();
        System.Windows.Forms.DataVisualization.Charting.LegendItem homeChartLegend = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
        System.Windows.Forms.DataVisualization.Charting.LegendItem awayChartLegend = new System.Windows.Forms.DataVisualization.Charting.LegendItem();

        #endregion
        #region Int, Doubles, Strings, Bools
        int HomePVCount = 0;
        int totalTime = 0;
        double totalTimeint10 = 0;
        int totalTimeAdd = 0;
        int totalTimeCount = 0;
        int FH = 120000;
        int SH = 120000;
        int OT = 30000;
        int FH0 = 1200000;
        int OT0 = 300000;
        int homescore = 0;
        int awayscore = 0;
        int homeStreak = 0;
        int awayStreak = 0;
        int homeLetters;
        int awayLetters;
        int overtimeperiod = 0;
        int scoreDif;
        int homeChoice;
        int awayChoice;

        double HomePercentValue;
        double AwayPercentValue;

        double homeWins;
        double awayWins;
        double homeLosses;
        double awayLosses;
        double homeTotal;
        double awayTotal;
        double homeRecord;
        double awayRecord;
        double homeRecordEqu;
        double awayRecordEqu;
        double AVGtotal;
        double recordValue;
        double hcValue;
        double graphCount;
        double graphMarkerCount = 1;

        long elapsedTicks;
        long elapsedTicksSum;
        long elapsedTicksNew;
        long elapsedTicksClock;
        long elapsedTicksClockSum;
        long elapsedTicksCount10 = 240000;
        long elapsedTicksCount1000;
        long elapsedTicksCountNew = 240000;
        long HomeelapsedTicks;
        long AwayelapsedTicks;
        long hcHomeTime;
        long hcAwayTime;
        long hcHomeTotalTime;
        long hcHomeSum;
        long hcHomeTotalSum;
        long hcHomecurrenttime = 0;
        long hcAwaycurrenttime = 0;
        long hcAwayTotalTime;
        long hcAwaySum;
        long hcAwayTotalSum;
        long hcHomeNSCount;
        long hcAwayNSCount;
        long hcHomeNSCountTicks;
        long hcAwayNSCountTicks;
        long hcHomeNoShoot;
        long hcAwayNoShoot;
        long hcHomeNoShoot1000;
        long hcAwayNoShoot1000;
        long hcHomeCountAddOn;
        long hcAwayCountAddOn;

        double timeLeftValue;
        double startingvalue;
        double swingvalue;
        double winningPercent;
        double curvevaluefix;
        double bonusvalue;
        double elapsedTicksAdj;
        double minutesRemaining;

        string time;
        string homeTeam;
        string awayTeam;
        string homeTeamLower = "Home Team";
        string awayTeamLower = "Away Team";

        string homePicSource;
        string awayPicSource;

        string homePicSourceFull;
        string awayPicSourceFull;

        string homeSmallPicSource;
        string awaySmallPicSource;

        string hcHomeNSTime;
        string hcAwayNSTime;
        string homeWinsAdj;
        string homeLossesAdj;
        string awayWinsAdj;
        string awayLossesAdj;

        string homeStreakAdj;
        string awayStreakAdj;

        string homePercentString;
        string awayPercentString;
        string totalTimeString;

        float opacityHomeValue;
        float opacityAwayValue;
        float opacitybackvalue;

        bool periodstart = true;
        bool secHalf = false;
        bool overtime = false;
        bool firstposs = false;
        bool poss = true;
        bool SliderHome = false;
        bool SliderAway = false;
        bool possession;
        bool Animation;
        bool hcanimation;
        bool keys;
        bool streaks;
        bool streak;
        bool NSHomeCounter;
        bool NSAwayCounter;
        bool backdrops;
        bool lastPoint = false;
        bool homeBonusClick = false;
        bool homeBonusPlusClick = false;
        bool awayBonusClick = false;
        bool awayBonusPlusClick = false;
        bool bonus;
        bool winpercent;
        bool winteam;
        bool wpAnimation;
        bool winNeutral;
        bool blinking;
        bool caution = false;
        bool homecaution = false;
        bool homewarning = false;
        bool homegood1 = false;
        bool homegood = false;
        bool awaycaution = false;
        bool awaywarning = false;
        bool awaygood1 = false;
        bool awaygood = false;
        bool inconsistent = false;
        bool regulation = false;
        bool buzzer = false;
        bool buzzHome = false;
        bool buzzAway = false;
        bool EOR = false;
        bool timerStart = false;
        bool periodover = false;
        bool final = false;
        bool animdone = true;
        bool gameover = false;
        bool shiftdown = false;
        bool controldown = false;
        bool awayNeutral = false;
        bool graphOpen = false;
        bool timeHigh = false;
        bool timeLow = false;
        bool switchTeams = false;
        bool graphmaker = false;
        bool HomeGraphMaker = false;
        bool AwayGraphMaker = false;
        bool HomePointAdd = false;
        bool AwayPointAdd = false;
        #endregion
        #endregion
        #region startwindow
        public MainWindow()
        {
            #region Visible Components
            InitializeComponent();
            startButton.IsEnabled = true;
            stopButton.IsEnabled = false;
            pauseButton.IsEnabled = false;
            homeScoreText.Content = homescore;
            awayScoreText.Content = awayscore;
            overtimeBack.Opacity = 0;
            overtimeName.Opacity = 0;
            startSecHalfButton.Visibility = Visibility.Hidden;
            hcHomeNS.Visibility = Visibility.Hidden;
            hcAwayNS.Visibility = Visibility.Hidden;
            streakHomeLabel.Visibility = Visibility.Hidden;
            streakAwayLabel.Visibility = Visibility.Hidden;
            homeStreakBack.Visibility = Visibility.Hidden;
            awayStreakBack.Visibility = Visibility.Hidden;
            homeTeamNameBackEven.Visibility = Visibility.Hidden;
            homeTeamNameBackOdd.Visibility = Visibility.Hidden;
            favoredHome.Visibility = Visibility.Hidden;
            favoredAway.Visibility = Visibility.Hidden;
            //hotcoldHome.Visibility = Visibility.Hidden;
            startButton.Focus();
            messageLabel.Content = "";
            periodNum.Content = "1";
            time = "20:00.0";

            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            this.Loaded += new RoutedEventHandler(Window_Loaded);

            buzzerChoice1.Opacity = 0;
            buzzerChoice2.Opacity = 0;
            buzzerChoice3.Opacity = 0;

            homeRect.Stroke = System.Windows.Media.Brushes.Transparent;
            awayRect.Stroke = System.Windows.Media.Brushes.Transparent;

            homeStreakPic.Opacity = .5;
            awayStreakPic.Opacity = .5;

            WinningPercentAnimation(ref wpAnimation);
            var myColor = "#FF04A831";
            var myControlColor = System.Drawing.ColorTranslator.FromHtml(myColor);

            //WinningPercentValue(ref winpercent);

            pgHomeButton.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            fgHomeButton.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            ftHomeButton.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            minus1Home.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");

            pgAwayButton.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            fgAwayButton.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            ftAwayButton.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            minus1Away.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");

            startButton.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            pauseButton.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            stopButton.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");

            enterTimeButton.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            startSecHalfButton.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");

            plus1min.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            minus1min.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            plus1sec.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            minus1sec.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            plus1mil.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
            minus1mil.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");

            columnLeft1.Width = new GridLength(285, GridUnitType.Pixel);
            columnLeft2.Width = new GridLength(105, GridUnitType.Pixel);
            columnRight1.Width = new GridLength(105, GridUnitType.Pixel);
            columnRight2.Width = new GridLength(285, GridUnitType.Pixel);

            ThumbImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Data Elements/basketball100.png", UriKind.Absolute)));
            ThumbImage.SetValue(System.Windows.Controls.Image.UseLayoutRoundingProperty, true);
            ThumbImage.SetValue(System.Windows.Controls.Image.HeightProperty, 25d);
            ThumbImage.SetValue(System.Windows.Controls.Image.WidthProperty, 25d);
            ThumbGrid.AppendChild(ThumbImage);
            ThumbTemp.VisualTree = ThumbGrid;
            ThumbSetter = new Setter(Thumb.TemplateProperty, ThumbTemp);
            ThumbStyle.Setters.Add(ThumbSetter);
            sliderImages._SliderImage = ThumbStyle;

            SliderGrid.AppendChild(homeSliderSelection);
            //SliderGrid.AppendChild(awaySliderSelection);
            SliderGrid.AppendChild(SliderTrack);   
            SliderGrid.AppendChild(SliderBackground);
            SliderGrid.AppendChild(SliderThumb);

            SliderTemp.VisualTree = SliderGrid;
            SliderSetter = new Setter(Slider.TemplateProperty, SliderTemp);
            CustomSliderStyle.Setters.Add(SliderSetter);

            CustomSlider.Value = 2;
            //CustomSlider.Width = 80;
            //CustomSlider.Height = 25;
            //CustomSlider.Minimum = 1;
            //CustomSlider.Maximum = 3;
            //CustomSlider.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight;
            //CustomSlider.Style = CustomSliderStyle;
            DoubleCollection tickMarcks = new DoubleCollection();
            tickMarcks.Add(1);
            tickMarcks.Add(2);
            tickMarcks.Add(3);
            //CustomSlider.Ticks = tickMarcks;

            #endregion
            #region Chart Components
            #region Chart2 Properties
            //Chart 2

            percentChart.Title = "Percent Chance Win vs. Time";

            percentChart.FontFamily = new System.Windows.Media.FontFamily("Arial");
            percentChart.FontWeight = FontWeights.Bold;
            percentChart.FontSize = 14;

            Style ChartTitle = new Style(typeof(System.Windows.Controls.DataVisualization.Title));
            ControlTemplate ChartTitleTemp = new ControlTemplate(typeof(System.Windows.Controls.DataVisualization.Title));
            var ChartTitleLabel = new FrameworkElementFactory(typeof(System.Windows.Controls.Label));
            ChartTitleLabel.SetValue(System.Windows.Controls.DataVisualization.Title.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            ChartTitleLabel.SetValue(System.Windows.Controls.DataVisualization.Title.MarginProperty, new Thickness(0,0,0,-20));
            ChartTitleLabel.SetValue(System.Windows.Controls.DataVisualization.Title.ContentProperty, "Percent Chance Win vs. Time");
            ChartTitleTemp.VisualTree = ChartTitleLabel;
            Setter ChartTitleSetter = new Setter(System.Windows.Controls.DataVisualization.Title.TemplateProperty, ChartTitleTemp);
            ChartTitle.Setters.Add(ChartTitleSetter);
            percentChart.TitleStyle = ChartTitle;

            Style legendStyle = new System.Windows.Style(typeof(System.Windows.Controls.Control));
            Setter NullLegend = new Setter(System.Windows.Controls.DataVisualization.Legend.TemplateProperty, null);
            legendStyle.Setters.Add(NullLegend);
            percentChart.LegendStyle = legendStyle;
            #endregion
            #region BackgroundAxis
            //Chart Background
            BackgroundAxis.Orientation = AxisOrientation.X;
            BackgroundAxis.Location = AxisLocation.Bottom;

            ChartHomeRGB.Center = new System.Windows.Point(0.72, 0.55);
            ChartHomeRGB.GradientOrigin = new System.Windows.Point(0.72, 0.55);
            ChartHomeRGB.RadiusX = 0.4;
            ChartHomeRGB.RadiusY = 0.7;

            ChartAwayRGB.Center = new System.Windows.Point(0.22, 0.55);
            ChartAwayRGB.GradientOrigin = new System.Windows.Point(0.22, 0.55);
            ChartAwayRGB.RadiusX = 0.4;
            ChartAwayRGB.RadiusY = 0.7;

            ChartHomePoly.SetValue(Polygon.VerticalAlignmentProperty, System.Windows.VerticalAlignment.Bottom);
            ChartAwayPoly.SetValue(Polygon.RenderTransformProperty, new ScaleTransform { ScaleY = -1});
            ChartAwayPoly.SetValue(Polygon.RenderTransformOriginProperty, new System.Windows.Point{X=0.5, Y=0.5});
            ChartAwayPoly.SetValue(Polygon.VerticalAlignmentProperty, System.Windows.VerticalAlignment.Bottom);
            ChartHomePoly.SetValue(Grid.ZIndexProperty, 4);
            ChartAwayPoly.SetValue(Grid.ZIndexProperty, 4);

            System.Windows.Point HomePoly1 = new System.Windows.Point(0, 189);
            System.Windows.Point HomePoly2 = new System.Windows.Point(156.25, 189);
            System.Windows.Point HomePoly3 = new System.Windows.Point(343.7, 0);
            System.Windows.Point HomePoly4 = new System.Windows.Point(0, 0);
            System.Windows.Point AwayPoly1 = new System.Windows.Point(343.7, 189);
            System.Windows.Point AwayPoly2 = new System.Windows.Point(500, 189);
            System.Windows.Point AwayPoly3 = new System.Windows.Point(500, 0);
            System.Windows.Point AwayPoly4 = new System.Windows.Point(156.25, 0);

            HomePolyCollection.Add(HomePoly1);
            HomePolyCollection.Add(HomePoly2);
            HomePolyCollection.Add(HomePoly3);
            HomePolyCollection.Add(HomePoly4);
            AwayPolyCollection.Add(AwayPoly1);
            AwayPolyCollection.Add(AwayPoly2);
            AwayPolyCollection.Add(AwayPoly3);
            AwayPolyCollection.Add(AwayPoly4);
            FullCollection.Add(HomePoly1);
            FullCollection.Add(AwayPoly2);
            FullCollection.Add(AwayPoly3);
            FullCollection.Add(HomePoly4);

            ChartHomePoly.SetValue(Polygon.PointsProperty, HomePolyCollection);
            ChartAwayPoly.SetValue(Polygon.PointsProperty, AwayPolyCollection);

            DividerBrush.SetValue(LinearGradientBrush.TransformProperty, new RotateTransform { Angle = 90, CenterX = 125, CenterY = 125 });

            ChartBackgroundBorder.SetValue(Border.OpacityProperty, 0.2);
            ChartBackgroundBorder.SetValue(Border.MarginProperty, new Thickness(0, -279, 0, 0));
            ChartBackgroundBorder.SetValue(Border.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            ChartBackgroundBorder.SetValue(Border.BorderBrushProperty, System.Windows.Media.Brushes.Black);
            ChartBackgroundBorder.SetValue(Border.BorderThicknessProperty, new Thickness(0));
            ChartBackgroundBorder.SetValue(Border.HeightProperty, 189d);
            ChartBackgroundBorder.SetValue(Border.WidthProperty, 500d);

            EllipseGeometry RadialClip = new EllipseGeometry();
            RadialClip.Center = new System.Windows.Point(130,130);
            RadialClip.RadiusX = 85;
            RadialClip.RadiusY = 85;

            HomeBackgroundClip = new PathGeometry(new[] { new PathFigure(new System.Windows.Point(146,0), new[] {
    new LineSegment(new System.Windows.Point(0, 146), true),
    new LineSegment(new System.Windows.Point(0, 0), true)}, true) });

            AwayBackgroundClip = new PathGeometry(new[] { new PathFigure(new System.Windows.Point(0,154), new[] {
    new LineSegment(new System.Windows.Point(154, 0), true),
    new LineSegment(new System.Windows.Point(154, 154), true)}, true) });

            ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
            ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Grid.ZIndexProperty, 5);
            ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.ClipProperty, HomeBackgroundClip);
            ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.HeightProperty, 150d);
            ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.WidthProperty, 150d);
            ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.MarginProperty, new Thickness(10, 0, 0, 0));

            ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
            ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Grid.ZIndexProperty, 5);
            ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.ClipProperty, AwayBackgroundClip);
            ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.HeightProperty, 150d);
            ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.WidthProperty, 150d);
            ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.MarginProperty, new Thickness(0, 0, 10, 0));

            ChartBackgroundDivider.SetValue(Line.RenderTransformProperty, new ScaleTransform { ScaleX = -1 });
            ChartBackgroundDivider.SetValue(Line.RenderTransformOriginProperty, new System.Windows.Point(0.5, 0.5));
            ChartBackgroundDivider.SetValue(Grid.ZIndexProperty, 7);
            ChartBackgroundDivider.SetValue(Line.ClipProperty, RadialClip);
            ChartBackgroundDivider.SetValue(Line.X1Property, 250d);
            ChartBackgroundDivider.SetValue(Line.Y1Property, 250d);
            ChartBackgroundDivider.SetValue(Line.X2Property, 0d);
            ChartBackgroundDivider.SetValue(Line.Y2Property, 0d);
            ChartBackgroundDivider.SetValue(Line.StrokeThicknessProperty, 30d);
            ChartBackgroundDivider.SetValue(Grid.VerticalAlignmentProperty, System.Windows.VerticalAlignment.Center);
            ChartBackgroundDivider.SetValue(Grid.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            #endregion
            #region Home Background
            HChartHomeRGB.Center = new System.Windows.Point(0.5, 0.5);
            HChartHomeRGB.GradientOrigin = new System.Windows.Point(0.5, 0.5);
            HChartHomeRGB.RadiusX = 0.27;
            HChartHomeRGB.RadiusY = 0.62;

            HChartBackgroundBorder.SetValue(Border.OpacityProperty, 0.2);
            HChartBackgroundBorder.SetValue(Border.MarginProperty, new Thickness(0, -279, 0, 0));
            HChartBackgroundBorder.SetValue(Border.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            HChartBackgroundBorder.SetValue(Border.BorderBrushProperty, System.Windows.Media.Brushes.Black);
            HChartBackgroundBorder.SetValue(Border.BorderThicknessProperty, new Thickness(0));
            HChartBackgroundBorder.SetValue(Border.HeightProperty, 189d);
            HChartBackgroundBorder.SetValue(Border.WidthProperty, 500d);

            HChartHomePoly.SetValue(Polygon.VerticalAlignmentProperty, System.Windows.VerticalAlignment.Bottom);
            HChartHomePoly.SetValue(Polygon.VerticalAlignmentProperty, System.Windows.VerticalAlignment.Bottom);
            HChartHomePoly.SetValue(Grid.ZIndexProperty, 4);
            HChartHomePoly.SetValue(Polygon.PointsProperty, FullCollection);
            HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
            HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Grid.ZIndexProperty, 5);
            //HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.ClipProperty, HomeBackgroundClip);
            HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.HeightProperty, 150d);
            HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.WidthProperty, 150d);
            HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.MarginProperty, new Thickness(10, 0, 0, 0));
            #endregion
            #region Away Background
            AChartAwayRGB.Center = new System.Windows.Point(0.5, 0.5);
            AChartAwayRGB.GradientOrigin = new System.Windows.Point(0.5, 0.5);
            AChartAwayRGB.RadiusX = 0.27;
            AChartAwayRGB.RadiusY = 0.62;

            AChartBackgroundBorder.SetValue(Border.OpacityProperty, 0.2);
            AChartBackgroundBorder.SetValue(Border.MarginProperty, new Thickness(0, -279, 0, 0));
            AChartBackgroundBorder.SetValue(Border.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            AChartBackgroundBorder.SetValue(Border.BorderBrushProperty, System.Windows.Media.Brushes.Black);
            AChartBackgroundBorder.SetValue(Border.BorderThicknessProperty, new Thickness(0));
            AChartBackgroundBorder.SetValue(Border.HeightProperty, 189d);
            AChartBackgroundBorder.SetValue(Border.WidthProperty, 500d);

            AChartAwayPoly.SetValue(Polygon.VerticalAlignmentProperty, System.Windows.VerticalAlignment.Bottom);
            AChartAwayPoly.SetValue(Polygon.VerticalAlignmentProperty, System.Windows.VerticalAlignment.Bottom);
            AChartAwayPoly.SetValue(Grid.ZIndexProperty, 4);
            AChartAwayPoly.SetValue(Polygon.PointsProperty, FullCollection);
            AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
            AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Grid.ZIndexProperty, 5);
            AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.HeightProperty, 150d);
            AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.WidthProperty, 150d);
            AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.MarginProperty, new Thickness(0, 0, 10, 0));
            #endregion
            #region XAxis - LegendAxis
            //XAxis (Top 1 - Legend)

            LegendZone1.SetValue(ColumnDefinition.WidthProperty, new GridLength(50, GridUnitType.Star));
            LegendZone2.SetValue(ColumnDefinition.WidthProperty, new GridLength(50, GridUnitType.Star));

            XAxisLegendGrid.AppendChild(LegendZone1);
            XAxisLegendGrid.AppendChild(LegendZone2);

            XAxisLegendBorder.SetValue(Border.BorderBrushProperty, new SolidColorBrush(Colors.Black));
            XAxisLegendBorder.SetValue(Border.BorderThicknessProperty, new Thickness(1));
            XAxisLegendBorder.SetValue(Border.WidthProperty, 270d);
            XAxisLegendBorder.SetValue(Border.HeightProperty, 25d);
            XAxisLegendBorder.SetValue(Border.MarginProperty, new Thickness(0,0,0,5));
            XAxisLegendGrid.SetValue(StackPanel.OrientationProperty, System.Windows.Controls.Orientation.Horizontal);
            XAxisLegendGrid.SetValue(StackPanel.WidthProperty, 270d);
            XAxisLegendGrid.SetValue(StackPanel.HeightProperty, 25d);

            XAxisLegendBorder.AppendChild(XAxisLegendGrid);

            XAxisHomeLegendImage.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
            XAxisHomeLegendImage.SetValue(System.Windows.Controls.Grid.ZIndexProperty, 1);
            XAxisHomeLegendImage.SetValue(System.Windows.Controls.Image.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Left);
            XAxisHomeLegendImage.SetValue(System.Windows.Controls.Image.HeightProperty, 15d);
            XAxisHomeLegendImage.SetValue(System.Windows.Controls.Image.WidthProperty, 15d);
            XAxisHomeLegendImage.SetValue(System.Windows.Controls.Image.MarginProperty, new Thickness(22, 0, 0, 0));

            XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
            XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Label.HeightProperty, 2d);
            XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Label.WidthProperty, 40d);
            XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Image.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Left);
            XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Label.MarginProperty, new Thickness(10,0,0,0));

            XAxisHomeLegendLabel2.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
            XAxisHomeLegendLabel2.SetValue(System.Windows.Controls.Label.ContentProperty, "Home Team");
            XAxisHomeLegendLabel2.SetValue(System.Windows.Controls.Label.FontSizeProperty, 12d);
            XAxisHomeLegendLabel2.SetValue(System.Windows.Controls.Image.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Right);
            XAxisHomeLegendLabel2.SetValue(System.Windows.Controls.Label.FontFamilyProperty, new System.Windows.Media.FontFamily("Arial"));
            XAxisHomeLegendLabel2.SetValue(System.Windows.Controls.Label.FontWeightProperty, FontWeights.Normal);
            XAxisHomeLegendLabel2.SetValue(System.Windows.Controls.Label.MarginProperty, new Thickness(0, 0, 10, 0));

            XAxisAwayLegendImage.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
            XAxisAwayLegendImage.SetValue(System.Windows.Controls.Grid.ZIndexProperty, 1);
            XAxisAwayLegendImage.SetValue(System.Windows.Controls.Image.HeightProperty, 15d);
            XAxisAwayLegendImage.SetValue(System.Windows.Controls.Image.WidthProperty, 15d);
            XAxisAwayLegendImage.SetValue(System.Windows.Controls.Image.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Left);
            XAxisAwayLegendImage.SetValue(System.Windows.Controls.Image.MarginProperty, new Thickness(22, 0, 0, 0));

            XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
            XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Label.HeightProperty, 2d);
            XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Label.WidthProperty, 40d);
            XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Image.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Left);
            XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(Colors.Red));
            XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Label.MarginProperty, new Thickness(10, 0, 0, 0));

            XAxisAwayLegendLabel2.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
            XAxisAwayLegendLabel2.SetValue(System.Windows.Controls.Label.ContentProperty, "Away Team");
            XAxisAwayLegendLabel2.SetValue(System.Windows.Controls.Label.FontSizeProperty, 12d);
            XAxisAwayLegendLabel2.SetValue(System.Windows.Controls.Image.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Right);
            XAxisAwayLegendLabel2.SetValue(System.Windows.Controls.Label.FontFamilyProperty, new System.Windows.Media.FontFamily("Arial"));
            XAxisAwayLegendLabel2.SetValue(System.Windows.Controls.Label.FontWeightProperty, FontWeights.Normal);
            XAxisAwayLegendLabel2.SetValue(System.Windows.Controls.Label.MarginProperty, new Thickness(0, 0, 10, 0));

            XAxisLegendTemp.VisualTree = XAxisLegendBorder;
            XAxisLegendSetter = new Setter(LinearAxis.TemplateProperty, XAxisLegendTemp);
            
            LegendAxis.Orientation = AxisOrientation.X;
            
            LegendAxis.Location = AxisLocation.Top;
            #endregion
            #region Xaxis - B2(Zones)
            //Xaxis (Bottom 2 - Half Zones)

            XAxisZoneGrid.SetValue(System.Windows.Controls.Grid.MarginProperty, new Thickness(0, -35, 0, 0));

            Zone1.SetValue(ColumnDefinition.WidthProperty, new GridLength(50, GridUnitType.Star));
            Zone2.SetValue(ColumnDefinition.WidthProperty, new GridLength(50, GridUnitType.Star));

            XAxisZoneGrid.AppendChild(Zone1);
            XAxisZoneGrid.AppendChild(Zone2);

            ZoneBrush.Color = Colors.White;
            ZoneBrush.Opacity = .65;

            XAxisZoneMinLabel.SetValue(System.Windows.Controls.Label.ContentProperty, "Minutes Remaining");
            XAxisZoneMinLabel.SetValue(System.Windows.Controls.Label.BackgroundProperty, ZoneBrush);
            XAxisZoneMinLabel.SetValue(System.Windows.Controls.Label.FontFamilyProperty, new System.Windows.Media.FontFamily("Arial"));
            XAxisZoneMinLabel.SetValue(System.Windows.Controls.Label.FontWeightProperty, FontWeights.Normal);
            XAxisZoneMinLabel.SetValue(System.Windows.Controls.Label.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);

            XAxisZoneMinBorder.SetValue(Border.BorderBrushProperty, System.Windows.Media.Brushes.Black);
            XAxisZoneMinBorder.SetValue(Border.BorderThicknessProperty, new Thickness(1));
            XAxisZoneMinBorder.SetValue(Border.MarginProperty, new Thickness(0, 10, 0, 0));
            XAxisZoneMinBorder.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
            XAxisZoneMinBorder.SetValue(System.Windows.Controls.Label.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            XAxisZoneMinBorder.AppendChild(XAxisZoneMinLabel);
            XAxisZoneMinBorder.SetValue(System.Windows.Controls.Grid.ZIndexProperty, 1);
            XAxisZoneGrid.AppendChild(XAxisZoneMinBorder);

            XAxisZone1Label.SetValue(System.Windows.Controls.Label.ContentProperty, "First Half");
            XAxisZone1Label.SetValue(System.Windows.Controls.Label.FontFamilyProperty, new System.Windows.Media.FontFamily("Arial"));
            XAxisZone1Label.SetValue(System.Windows.Controls.Label.FontWeightProperty, FontWeights.Normal);
            XAxisZone1Label.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
            XAxisZone1Label.SetValue(System.Windows.Controls.Label.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            XAxisZone1Label.SetValue(System.Windows.Controls.Grid.MarginProperty, new Thickness(0, 10, 0, 0));
            XAxisZoneGrid.AppendChild(XAxisZone1Label);

            XAxisZone2Label.SetValue(System.Windows.Controls.Label.ContentProperty, "Second Half");
            XAxisZone2Label.SetValue(System.Windows.Controls.Label.FontFamilyProperty, new System.Windows.Media.FontFamily("Arial"));
            XAxisZone2Label.SetValue(System.Windows.Controls.Label.FontWeightProperty, FontWeights.Normal);
            XAxisZone2Label.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
            XAxisZone2Label.SetValue(System.Windows.Controls.Label.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            XAxisZone2Label.SetValue(System.Windows.Controls.Grid.MarginProperty, new Thickness(0, 10, 0, 0));
            XAxisZoneGrid.AppendChild(XAxisZone2Label);

            XAxisZone1Line1.SetValue(Line.MarginProperty, new Thickness(11, 30, 0, 0));
            XAxisZone1Line1.SetValue(Line.StrokeThicknessProperty, 1d);
            XAxisZone1Line1.SetValue(Line.StrokeProperty, System.Windows.Media.Brushes.Black);
            XAxisZone1Line1.SetValue(Line.X1Property, 0d);
            XAxisZone1Line1.SetValue(Line.Y1Property, 0d);
            XAxisZone1Line1.SetValue(Line.X2Property, 250d);
            XAxisZone1Line1.SetValue(Line.Y2Property, 0d);
            XAxisZone1Line1.SetValue(System.Windows.Controls.Grid.ZIndexProperty, 0);
            XAxisZoneGrid.AppendChild(XAxisZone1Line1);

            XAxisZone1Line2.SetValue(Line.MarginProperty, new Thickness(12, 30, 0, 0));
            XAxisZone1Line2.SetValue(Line.StrokeThicknessProperty, 1d);
            XAxisZone1Line2.SetValue(Line.StrokeProperty, System.Windows.Media.Brushes.Black);
            XAxisZone1Line2.SetValue(Line.X1Property, 0d);
            XAxisZone1Line2.SetValue(Line.Y1Property, -15d);
            XAxisZone1Line2.SetValue(Line.X2Property, 0d);
            XAxisZone1Line2.SetValue(Line.Y2Property, 0d);
            XAxisZone1Line2.SetValue(System.Windows.Controls.Grid.ZIndexProperty, 0);
            XAxisZoneGrid.AppendChild(XAxisZone1Line2);

            XAxisZone1Line3.SetValue(Line.MarginProperty, new Thickness(0, 30, 0, 0));
            XAxisZone1Line3.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
            XAxisZone1Line3.SetValue(Line.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Right);
            XAxisZone1Line3.SetValue(Line.StrokeThicknessProperty, 1d);
            XAxisZone1Line3.SetValue(Line.StrokeProperty, System.Windows.Media.Brushes.Black);
            XAxisZone1Line3.SetValue(Line.X1Property, 0d);
            XAxisZone1Line3.SetValue(Line.Y1Property, 0d);
            XAxisZone1Line3.SetValue(Line.X2Property, 0d);
            XAxisZone1Line3.SetValue(Line.Y2Property, -15d);
            XAxisZone1Line3.SetValue(System.Windows.Controls.Grid.ZIndexProperty, 0);
            XAxisZoneGrid.AppendChild(XAxisZone1Line3);

            XAxisZone2Line1.SetValue(Line.MarginProperty, new Thickness(0, 30, 11, 0));
            XAxisZone2Line1.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
            XAxisZone2Line1.SetValue(Line.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            XAxisZone2Line1.SetValue(Line.StrokeThicknessProperty, 1d);
            XAxisZone2Line1.SetValue(Line.StrokeProperty, System.Windows.Media.Brushes.Black);
            XAxisZone2Line1.SetValue(Line.X1Property, 0d);
            XAxisZone2Line1.SetValue(Line.Y1Property, 0d);
            XAxisZone2Line1.SetValue(Line.X2Property, 250d);
            XAxisZone2Line1.SetValue(Line.Y2Property, 0d);
            XAxisZone2Line1.SetValue(System.Windows.Controls.Grid.ZIndexProperty, 0);
            XAxisZoneGrid.AppendChild(XAxisZone2Line1);

            XAxisZone2Line2.SetValue(Line.MarginProperty, new Thickness(0, 30, 0, 0));
            XAxisZone2Line2.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
            XAxisZone2Line2.SetValue(Line.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Left);
            XAxisZone2Line2.SetValue(Line.StrokeThicknessProperty, 1d);
            XAxisZone2Line2.SetValue(Line.StrokeProperty, System.Windows.Media.Brushes.Black);
            XAxisZone2Line2.SetValue(Line.X1Property, 0d);
            XAxisZone2Line2.SetValue(Line.Y1Property, -15d);
            XAxisZone2Line2.SetValue(Line.X2Property, 0d);
            XAxisZone2Line2.SetValue(Line.Y2Property, 0d);
            XAxisZone2Line2.SetValue(System.Windows.Controls.Grid.ZIndexProperty, 0);
            XAxisZoneGrid.AppendChild(XAxisZone2Line2);

            XAxisZone2Line3.SetValue(Line.MarginProperty, new Thickness(0, 30, 11, 0));
            XAxisZone2Line3.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
            XAxisZone2Line3.SetValue(Line.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Right);
            XAxisZone2Line3.SetValue(Line.StrokeThicknessProperty, 1d);
            XAxisZone2Line3.SetValue(Line.StrokeProperty, System.Windows.Media.Brushes.Black);
            XAxisZone2Line3.SetValue(Line.X1Property, 0d);
            XAxisZone2Line3.SetValue(Line.Y1Property, 0d);
            XAxisZone2Line3.SetValue(Line.X2Property, 0d);
            XAxisZone2Line3.SetValue(Line.Y2Property, -15d);
            XAxisZone2Line3.SetValue(System.Windows.Controls.Grid.ZIndexProperty, 0);
            XAxisZoneGrid.AppendChild(XAxisZone2Line3);

            XAxisZoneTemp.VisualTree = XAxisZoneGrid;

            XAxisZoneSetter = new Setter(LinearAxis.TemplateProperty, XAxisZoneTemp);
            XAxisZoneStyle.Setters.Add(XAxisZoneSetter);

            XAxisZone.Orientation = AxisOrientation.X;
            XAxisZone.Location = AxisLocation.Bottom;
            XAxisZone.Style = XAxisZoneStyle;

            #endregion
            #region XAxis - B1
            //XAXIS (Bottom 1)

            Style XAxisFlip = new Style(typeof(System.Windows.Controls.DataVisualization.Title));
            ControlTemplate XFlipTemp = new ControlTemplate(typeof(System.Windows.Controls.DataVisualization.Title));
            var XFlipBorder = new FrameworkElementFactory(typeof(Border));
            var XFlip = new FrameworkElementFactory(typeof(System.Windows.Controls.Label));
            XFlipBorder.SetValue(Border.BorderThicknessProperty, new Thickness(0));
            XFlipBorder.SetValue(Border.BorderBrushProperty, System.Windows.Media.Brushes.Black);
            XFlip.SetValue(System.Windows.Controls.Label.ContentProperty, "");
            XFlip.SetValue(System.Windows.Controls.Label.BackgroundProperty, System.Windows.Media.Brushes.White);
            //XFlip.SetValue(System.Windows.Controls.Label.OpacityProperty, 50d);
            XFlipBorder.AppendChild(XFlip);
            XFlipTemp.VisualTree = XFlipBorder;
            Setter XAxisFlipTemp = new Setter(System.Windows.Controls.DataVisualization.Title.TemplateProperty, XFlipTemp);
            Setter XAxisFlip180 = new Setter(System.Windows.Controls.DataVisualization.Title.RenderTransformProperty, new ScaleTransform { ScaleX = -1 });
            Setter XAxisFlipReset = new Setter(System.Windows.Controls.DataVisualization.Title.RenderTransformOriginProperty, new System.Windows.Point { X = 0.5, Y = 0.5 });
            XAxisFlip.Setters.Add(XAxisFlipTemp);
            XAxisFlip.Setters.Add(XAxisFlip180);
            XAxisFlip.Setters.Add(XAxisFlipReset);

            xaxis.Minimum = -1;
            xaxis.Maximum = 41;
            xaxis.Interval = 5;
            xaxis.Orientation = AxisOrientation.X;
            xaxis.FontSize = 12;
            xaxis.FontWeight = FontWeights.Normal;
            xaxis.RenderTransform = new ScaleTransform()
            {
                ScaleX = -1.0
            };
            xaxis.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            xaxis.ShowGridLines = true;
            xaxis.TitleStyle = XAxisFlip;
            
            //LegendItem
            
            Style XAxisStyle = new Style(typeof(AxisLabel));
            Setter XAxisScale = new Setter(AxisLabel.RenderTransformProperty, new ScaleTransform() { ScaleX = -1 });
            Setter XAxisOrigin = new Setter(AxisLabel.RenderTransformOriginProperty, new System.Windows.Point(0.5, 0.5));
            XAxisStyle.Setters.Add(XAxisScale);
            XAxisStyle.Setters.Add(XAxisOrigin);

            xaxis.AxisLabelStyle = XAxisStyle;
            #endregion
            #region Yaxis - Left & Right
            yaxis.Location = AxisLocation.Left;
            yaxis.Minimum = -5;
            yaxis.Maximum = 105;
            yaxis.Interval = 10;
            yaxis.Title = "Percent Chance Win";
            yaxis.FontSize = 12;
            yaxis.FontWeight = FontWeights.Normal;
            yaxis.Orientation = AxisOrientation.Y;
            yaxis.ShowGridLines = true;

            yaxis2.Location = AxisLocation.Right;
            yaxis2.Minimum = -5;
            yaxis2.Maximum = 105;
            yaxis2.Interval = 10;
            yaxis2.FontSize = 12;
            yaxis2.FontWeight = FontWeights.Normal;
            yaxis2.Orientation = AxisOrientation.Y;
            #endregion
            #region HomePoint
            #region Marker
            HomeChartLogoGrid.SetValue(System.Windows.Controls.Grid.HeightProperty, 20d);
            HomeChartLogoGrid.SetValue(System.Windows.Controls.Grid.WidthProperty, 20d);
            HomeChartLogoGrid.SetValue(System.Windows.Controls.Grid.MarginProperty, new Thickness(-10,-10,-10,-10));
            HomeChartLogoGrid.SetValue(System.Windows.Controls.Grid.RenderTransformProperty, new ScaleTransform { ScaleX = -1 });
            HomeChartLogoGrid.SetValue(System.Windows.Controls.Grid.RenderTransformOriginProperty, new System.Windows.Point(0.5, 0.5));
            HomeChartLogo.SetValue(System.Windows.Controls.Image.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            HomeChartLogo.SetValue(System.Windows.Controls.Image.VerticalAlignmentProperty, System.Windows.VerticalAlignment.Center);

            XAxisHomePointStyle = new Style(typeof(System.Windows.Controls.DataVisualization.Charting.DataPoint));
            HomeLineMarker.RenderTransform = new ScaleTransform()
            {
                ScaleX = -1.0
            };
            HomeLineMarker.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            HomeLineMarker.ItemsSource = HomeSeriesMarker;
            HomeLineMarker.DependentValuePath = "Value";
            HomeLineMarker.IndependentValuePath = "Key";
            HomeLineMarker.IndependentAxis = xaxis;
            HomeLineMarker.DependentRangeAxis = yaxis;
            #endregion
            #region Line
            HomeLineStyle = new Style(typeof(System.Windows.Controls.DataVisualization.Charting.DataPoint));

            HomeLine.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            HomeLine.ItemsSource = HomeSeries;
            HomeLine.DependentValuePath = "Value";
            HomeLine.IndependentValuePath = "Key";
            HomeLine.IndependentAxis = xaxis;
            HomeLine.DependentRangeAxis = yaxis;
            HomeLine.RenderTransform = new ScaleTransform()
            {
                ScaleX = -1.0
            };
            #endregion
            #region ToolTip
            HomePointRGB.RadiusX = .5;
            HomePointRGB.RadiusY = .5;
            HomePointRGB.Center = new System.Windows.Point(0.5, 0.5);
            HomePointRGB.GradientStops.Add(new GradientStop(Colors.Purple, 0));
            HomePointRGB.GradientStops.Add(new GradientStop(Colors.Black, 1));
            
            HomePointEllipse.SetValue(Ellipse.FillProperty, HomePointRGB);
            HomePointEllipse.SetValue(Ellipse.WidthProperty, 8d);
            HomePointEllipse.SetValue(Ellipse.HeightProperty, 8d);

            PolyLineNull = new Setter(Polyline.StrokeThicknessProperty, 0d);
            HomePolyLineNull.Setters.Add(PolyLineNull);

            //HomePointsEllipse.SetValue(Ellipse.StrokeThicknessProperty, 1d);
            //HomePointsEllipse.SetValue(Ellipse.StrokeProperty, System.Windows.Media.Brushes.Black);
            //HomePointsEllipse.SetValue(Ellipse.FillProperty, System.Windows.Media.Brushes.Red);
            //HomePointsToolGrid.AppendChild(HomePointsEllipse);

            //HomeCCPerc.Content = HomeLinePoints.IndependentValuePath;
            //HomeCCPerc.SetValue(ContentControl.ContentStringFormatProperty, "Percent : {00.00}");
            //HomeStackPanel.Children.Add(HomeCCPerc);
            //HomeStackPanel.SetValue(StackPanel.WidthProperty, 250d);
            //HomeStackPanel.SetValue(StackPanel.HeightProperty, 50d);
            //HomePointsToolGrid.SetValue(ToolTipService.ToolTipProperty, HomeStackPanel);

            HomeLinePoints.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            HomeLinePoints.ItemsSource = HomeSeriesPoints;
            HomeLinePoints.DependentValuePath = "Value";
            HomeLinePoints.IndependentValuePath = "Key";
            HomeLinePoints.MouseEnter += 
            HomeLinePoints.DataPointStyle = 
            //HomeLinePoints.DataPointStyle = this.FindResource("ToolTipDataPointStyle") as Style;
            HomeLinePoints.PolylineStyle = HomePolyLineNull;
            HomeLinePoints.IndependentAxis = xaxis;
            HomeLinePoints.DependentRangeAxis = yaxis;
            HomeLinePoints.RenderTransform = new ScaleTransform()
            {
                ScaleX = -1.0
            };
            #endregion
            #endregion
            #region AwayPoint
            #region Marker
            AwayChartLogoGrid.SetValue(System.Windows.Controls.Grid.HeightProperty, 20d);
            AwayChartLogoGrid.SetValue(System.Windows.Controls.Grid.WidthProperty, 20d);
            AwayChartLogoGrid.SetValue(System.Windows.Controls.Grid.MarginProperty, new Thickness(-10, -10, -10, -10));
            AwayChartLogoGrid.SetValue(System.Windows.Controls.Grid.RenderTransformProperty, new ScaleTransform { ScaleX = -1 });
            AwayChartLogoGrid.SetValue(System.Windows.Controls.Grid.RenderTransformOriginProperty, new System.Windows.Point(0.5, 0.5));
            AwayChartLogo.SetValue(System.Windows.Controls.Image.HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
            AwayChartLogo.SetValue(System.Windows.Controls.Image.VerticalAlignmentProperty, System.Windows.VerticalAlignment.Center);

            XAxisAwayPointStyle = new Style(typeof(System.Windows.Controls.DataVisualization.Charting.DataPoint));
            AwayLineMarker.RenderTransform = new ScaleTransform()
            {
                ScaleX = -1.0
            };
            AwayLineMarker.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            AwayLineMarker.ItemsSource = AwaySeriesMarker;
            AwayLineMarker.DependentValuePath = "Value";
            AwayLineMarker.IndependentValuePath = "Key";
            AwayLineMarker.IndependentAxis = xaxis;
            AwayLineMarker.DependentRangeAxis = yaxis;

            AwayLineStyle = new Style(typeof(System.Windows.Controls.DataVisualization.Charting.DataPoint));
            AwayLine.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            #endregion
            #region Line
            AwayLine.ItemsSource = AwaySeries;
            AwayLine.DependentValuePath = "Value";
            AwayLine.IndependentValuePath = "Key";
            AwayLine.IndependentAxis = xaxis;
            AwayLine.DependentRangeAxis = yaxis;
            AwayLine.RenderTransform = new ScaleTransform()
            {
                ScaleX = -1.0
            };
            #endregion
            #region ToolTip          
            AwayPointsEllipse.SetValue(Ellipse.StrokeThicknessProperty, 1d);
            AwayPointsEllipse.SetValue(Ellipse.StrokeProperty, System.Windows.Media.Brushes.Black);
            AwayPointsEllipse.SetValue(Ellipse.FillProperty, System.Windows.Media.Brushes.Red);
            AwayPointsToolGrid.AppendChild(AwayPointsEllipse);

            PolyLineNull = new Setter(Polyline.StrokeThicknessProperty, 0d);
            AwayPolyLineNull.Setters.Add(PolyLineNull);

            AwayTTGrid.Width = 100;
            AwayTTGrid.Height = 70;
            AwayTTRect.Width = 100;
            AwayTTRect.Height = 70;
            AwayTTGrid.Children.Add(AwayTTRect);
            AwayTTGrid.Children.Add(AwayTTBlock);

            AwayPointsToolTemp.VisualTree = AwayPointsToolGrid;
            AwayPointsToolSetter = new Setter(LineDataPoint.TemplateProperty, AwayPointsToolTemp);
            AwayPointsToolStyle.Setters.Add(AwayPointsToolSetter);

            AwayLinePoints.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            AwayLinePoints.ItemsSource = AwaySeriesPoints;
            AwayLinePoints.DependentValuePath = "Value";
            AwayLinePoints.IndependentValuePath = "Key";
            AwayLinePoints.DataPointStyle = AwayPointsToolStyle;
            AwayLinePoints.PolylineStyle = AwayPolyLineNull;
            AwayLinePoints.IndependentAxis = xaxis;
            AwayLinePoints.DependentRangeAxis = yaxis;

            //var textblock = new System.Windows.Controls.Label();
            //textblock.Content = AwayLinePoints.IndependentValuePath;

            //var binding = new System.Windows.Data.Binding("Content");
            //binding.RelativeSource = new RelativeSource(RelativeSourceMode.Self);
            //BindingOperations.SetBinding(textblock, System.Windows.Controls.Label.ContentProperty, binding);
            //TemplateBindingExtension test1 = new TemplateBindingExtension(LineDataPoint.IndependentValueProperty);
            //AwayCCPerc.Content = AwayLinePoints.IndependentValuePath;
            //if (AwayPointsPair.First != null)
            //AwayCCPerc.Content = AwayPointsPair.GetType();//.Last<KeyValuePair<double, double>>().Key;
            //AwayCCPerc.SetValue(ContentControl.ContentStringFormatProperty, "Percent : {00.00}");
            AwayStackPanel.Children.Add(AwayCCPerc);
            AwayStackPanel.SetValue(StackPanel.WidthProperty, 250d);
            AwayStackPanel.SetValue(StackPanel.HeightProperty, 50d);
            AwayPointsToolGrid.SetValue(ToolTipService.ToolTipProperty, AwayStackPanel);

            AwayLinePoints.RenderTransform = new ScaleTransform()
            {
                ScaleX = -1.0
            };
            #endregion
            #endregion

            SliderGradient.StartPoint = new System.Windows.Point(0, 0);
            SliderGradient.EndPoint = new System.Windows.Point(1, 0);
            SliderGradient.Opacity = .5;

            graphMarkerRemove.Tick += new System.EventHandler(GraphMarkerRemoval);
            graphMarkerRemove.Interval = TimeSpan.FromMilliseconds(1000);

            graphUpdateTimer.Tick += new System.EventHandler(GraphUpdater);
            graphUpdateTimer.Interval = TimeSpan.FromMilliseconds(1000);
            graphUpdateTimer.Start();
            #endregion
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.MainWindow = this;
            this.Height = 565;
            this.Width = 780;
        }
       
        #endregion
        #region teamnames&choices
        //Set team names and records
        //TEST
        #region AmericaEast
        //America East
        public string homeTeamAlbany
        {
            set
            {
                homePicSource = "Team Data/America East/Albany.png";
                homePicSourceFull = "pack://application:,,,/WpfApplication1;component/" + homePicSource;
                homeSmallPicSource = "Team Data/America East/AlbanyLogoSmall.png";
                homeTeam = "GREAT DANES";
                homeTeamLower = "Great Danes";
                homeTeamName.Content = homeTeam;
                homeChoice = 1;

                homeTeamPic.Source = new BitmapImage(new Uri(homePicSource, UriKind.Relative));
                            
                homecolor = System.Windows.Media.Color.FromArgb(255, 70, 22, 106);
                homebrush.Color = homecolor;
                
                HomeBackColor(ref homecolor);

                foreach(char letter in homeTeam)
                {
                    homeLetters++;
                    HomeNameBackdrop(ref homeLetters);
                }
                var homedrawingcolor = System.Drawing.Color.FromArgb(255, 70, 22, 106);

                HomeLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(homecolor));
                HomeChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(homecolor));

                ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));
                HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));

                HomeBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.45);
                HomeBackStopFull = new GradientStop(homecolor, 0.499);

                HomeRGB1 = new GradientStop(homecolor, .7);
                HomeRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 1);

                SliderGradientH1 = new GradientStop(homecolor, 0.0);
                SliderGradientH2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.4);

                SliderGradient.GradientStops.Add(SliderGradientH1);
                SliderGradient.GradientStops.Add(SliderGradientH2);
                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string homeTeamBinghampton
        {
            set
            {
                homePicSource = "Team Data/America East/binghampton.png";
                homePicSourceFull = "pack://application:,,,/WpfApplication1;component/" + homePicSource;
                homeSmallPicSource = "Team Data/America East/BinghamptonLogoSmall.png";
                homeTeam = "BEARCATS";
                homeTeamLower = "Bearcats";
                
                homeTeamName.Content = homeTeam;

                homeChoice = 2;

                homeTeamPic.Source = new BitmapImage(new Uri(homePicSource, UriKind.Relative));

                homecolor = System.Windows.Media.Color.FromArgb(255, 0, 178, 92);
                homebrush.Color = homecolor;
                
                HomeBackColor(ref homecolor);

                foreach (char letter in homeTeam)
                {
                    homeLetters++;
                    HomeNameBackdrop(ref homeLetters);
                }
                var homedrawingcolor = System.Drawing.Color.FromArgb(255, 0, 178, 92);

                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(homecolor));
                HomeChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(homecolor));

                ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));
                HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));

                HomeBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.45);
                HomeBackStopFull = new GradientStop(homecolor, 0.499);

                HomeRGB1 = new GradientStop(homecolor, .7);
                HomeRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 1);

                SliderGradientH1 = new GradientStop(homecolor, 0.0);
                SliderGradientH2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.4);

                SliderGradient.GradientStops.Add(SliderGradientH1);
                SliderGradient.GradientStops.Add(SliderGradientH2);
                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string homeTeamHartford
        {
            set
            {
                homePicSource = "Team Data/America East/hartford.png";
                homePicSourceFull = "pack://application:,,,/WpfApplication1;component/" + homePicSource;
                homeSmallPicSource = "Team Data/America East/HartfordLogoSmall.png";
                homeTeam = "HAWKS";
                homeTeamLower = "Hawks";
                
                homeTeamName.Content = homeTeam; 

                homeChoice = 3;

                homeTeamPic.Source = new BitmapImage(new Uri(homePicSource, UriKind.Relative));
                    
                homecolor = System.Windows.Media.Color.FromArgb(255, 237, 28, 46);
                homebrush.Color = homecolor;
                
                HomeBackColor(ref homecolor);

                foreach (char letter in homeTeam)
                {
                    homeLetters++;
                    HomeNameBackdrop(ref homeLetters);
                }
                var homedrawingcolor = System.Drawing.Color.FromArgb(255, 237, 28, 46);

                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(homecolor));
                HomeChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(homecolor));

                ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));
                HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));

                HomeBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.45);
                HomeBackStopFull = new GradientStop(homecolor, 0.499);

                HomeRGB1 = new GradientStop(homecolor, .7);
                HomeRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 1);

                SliderGradientH1 = new GradientStop(homecolor, 0.0);
                SliderGradientH2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.4);

                SliderGradient.GradientStops.Add(SliderGradientH1);
                SliderGradient.GradientStops.Add(SliderGradientH2);
                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string homeTeamMaine
        {
            set
            {
                homePicSource = "Team Data/America East/maine.png";
                homePicSourceFull = "pack://application:,,,/WpfApplication1;component/" + homePicSource;
                homeSmallPicSource = "Team Data/America East/MaineLogoSmall.png";
                homeTeam = "BLACK BEARS";
                homeTeamLower = "Black Bears";
                homeTeamName.Content = homeTeam;

                homeChoice = 4;

                homeTeamPic.Source = new BitmapImage(new Uri(homePicSource, UriKind.Relative));

                homecolor = System.Windows.Media.Color.FromArgb(255, 54, 152, 212);
                homebrush.Color = homecolor;
                
                HomeBackColor(ref homecolor);

                foreach (char letter in homeTeam)
                {
                    homeLetters++;
                    HomeNameBackdrop(ref homeLetters);
                }
                var homedrawingcolor = System.Drawing.Color.FromArgb(255, 54, 152, 212);

                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(homecolor));
                HomeChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(homecolor));

                ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));
                HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));

                HomeBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.45);
                HomeBackStopFull = new GradientStop(homecolor, 0.499);

                HomeRGB1 = new GradientStop(homecolor, .7);
                HomeRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 1);

                SliderGradientH1 = new GradientStop(homecolor, 0.0);
                SliderGradientH2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.4);

                SliderGradient.GradientStops.Add(SliderGradientH1);
                SliderGradient.GradientStops.Add(SliderGradientH2);
                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string homeTeamUmbc
        {
            set
            {
                homePicSource = "Team Data/America East/umbc.png";
                homePicSourceFull = "pack://application:,,,/WpfApplication1;component/" + homePicSource;
                homeSmallPicSource = "Team Data/America East/UMBCLogoSmall.png";
                homeTeam = "RETREIVERS";
                homeTeamLower = "Retreivers";
                homeTeamName.Content = homeTeam;

                homeChoice = 5;

                homeTeamPic.Source = new BitmapImage(new Uri(homePicSource, UriKind.Relative));
                
                homecolor = System.Windows.Media.Color.FromArgb(255, 255, 194, 22);
                homebrush.Color = homecolor;
                
                HomeBackColor(ref homecolor);

                foreach (char letter in homeTeam)
                {
                    homeLetters++;
                    HomeNameBackdrop(ref homeLetters);
                }
                var homedrawingcolor = System.Drawing.Color.FromArgb(255, 255, 194, 22);

                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(homecolor));
                HomeChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(homecolor));

                ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));
                HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));

                HomeBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.45);
                HomeBackStopFull = new GradientStop(homecolor, 0.499);

                HomeRGB1 = new GradientStop(homecolor, .7);
                HomeRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 1);

                SliderGradientH1 = new GradientStop(homecolor, 0.0);
                SliderGradientH2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.4);

                SliderGradient.GradientStops.Add(SliderGradientH1);
                SliderGradient.GradientStops.Add(SliderGradientH2);
                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string homeTeamUmassLowell
        {
            set
            {
                homePicSource = "Team Data/America East/umasslowell.png";
                homePicSourceFull = "pack://application:,,,/WpfApplication1;component/" + homePicSource;
                homeSmallPicSource = "Team Data/America East/UmassLowellLogoSmall.png";
                homeTeam = "RIVER HAWKS";
                homeTeamLower = "River Hawks";
                
                homeTeamName.Content = homeTeam;

                homeChoice = 6;

                homeTeamPic.Source = new BitmapImage(new Uri(homePicSource, UriKind.Relative));
                
                homecolor = System.Windows.Media.Color.FromArgb(255, 0, 83, 161);
                homebrush.Color = homecolor;
                
                HomeBackColor(ref homecolor);

                foreach (char letter in homeTeam)
                {
                    homeLetters++;
                    HomeNameBackdrop(ref homeLetters);
                }
                var homedrawingcolor = System.Drawing.Color.FromArgb(255, 0, 83, 161);

                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(homecolor));
                HomeChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(homecolor));

                ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));
                HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));

                HomeBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.45);
                HomeBackStopFull = new GradientStop(homecolor, 0.499);

                HomeRGB1 = new GradientStop(homecolor, .7);
                HomeRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 1);

                SliderGradientH1 = new GradientStop(homecolor, 0.0);
                SliderGradientH2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.4);

                SliderGradient.GradientStops.Add(SliderGradientH1);
                SliderGradient.GradientStops.Add(SliderGradientH2);
                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string homeTeamUnh
        {
            set
            {
                homePicSource = "Team Data/America East/unh.png";
                homePicSourceFull = "pack://application:,,,/WpfApplication1;component/" + homePicSource;
                homeSmallPicSource = "Team Data/America East/UNHLogoSmall.png";
                homeTeam = "WILDCATS";
                homeTeamLower = "Wildcats";
                
                homeTeamName.Content = homeTeam;  

                homeChoice = 7;

                homeTeamPic.Source = new BitmapImage(new Uri(homePicSource, UriKind.Relative));
                               
                homecolor = System.Windows.Media.Color.FromArgb(255, 0, 39, 93);
                homebrush.Color = homecolor;
                
                HomeBackColor(ref homecolor);

                foreach (char letter in homeTeam)
                {
                    homeLetters++;
                    HomeNameBackdrop(ref homeLetters);
                }
                var homedrawingcolor = System.Drawing.Color.FromArgb(255, 0, 39, 93);

                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(homecolor));
                HomeChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(homecolor));

                ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));
                HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));

                HomeBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.45);
                HomeBackStopFull = new GradientStop(homecolor, 0.499);

                HomeRGB1 = new GradientStop(homecolor, .7);
                HomeRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 1);

                SliderGradientH1 = new GradientStop(homecolor, 0.0);
                SliderGradientH2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.4);

                SliderGradient.GradientStops.Add(SliderGradientH1);
                SliderGradient.GradientStops.Add(SliderGradientH2);
                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string homeTeamStony
        {
            set
            {
                homePicSource = "Team Data/America East/seawolves.png";
                homePicSourceFull = "pack://application:,,,/WpfApplication1;component/" + homePicSource;
                homeSmallPicSource = "Team Data/America East/StonyBrookLogoSmall.png";
                homeTeam = "SEAWOLVES";
                homeTeamLower = "Seawolves";
                homeTeamName.Content = homeTeam;
                
                homeChoice = 8;

                homeTeamPic.Source = new BitmapImage(new Uri(homePicSource, UriKind.Relative));

                homecolor = System.Windows.Media.Color.FromArgb(255, 184, 17, 55);
                homebrush.Color = homecolor;
                
                HomeBackColor(ref homecolor);

                foreach (char letter in homeTeam)
                {
                    homeLetters++;
                    HomeNameBackdrop(ref homeLetters);
                }
                var homedrawingcolor = System.Drawing.Color.FromArgb(255, 184, 17, 55);

                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(homecolor));
                HomeChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(homecolor));

                ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));
                HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));

                HomeBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.45);
                HomeBackStopFull = new GradientStop(homecolor, 0.499);

                HomeRGB1 = new GradientStop(homecolor, .7);
                HomeRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 1);

                SliderGradientH1 = new GradientStop(homecolor, 0.0);
                SliderGradientH2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.4);

                SliderGradient.GradientStops.Add(SliderGradientH1);
                SliderGradient.GradientStops.Add(SliderGradientH2);
                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string homeTeamVermont
        {
            set
            {
                homePicSource = "Team Data/America East/vermont.png";
                homePicSourceFull = "pack://application:,,,/WpfApplication1;component/" + homePicSource;
                homeSmallPicSource = "Team Data/America East/VermontLogoSmall.png";
                homeTeam = "CATAMOUNTS";
                homeTeamLower = "Catamounts";
                homeTeamName.Content = homeTeam;
                
                homeChoice = 9;

                homeTeamPic.Source = new BitmapImage(new Uri(homePicSource, UriKind.Relative));
                     
                homecolor = System.Windows.Media.Color.FromArgb(255, 1, 56, 35);
                homebrush.Color = homecolor;
                
                HomeBackColor(ref homecolor);

                foreach (char letter in homeTeam)
                {
                    homeLetters++;
                    HomeNameBackdrop(ref homeLetters);
                }
                var homedrawingcolor = System.Drawing.Color.FromArgb(255, 1, 56, 35);

                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(homecolor));
                HomeChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homeSmallPicSource, UriKind.Relative)));
                XAxisHomeLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(homecolor));

                ChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));
                HChartBackgroundHomeImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(homePicSourceFull, UriKind.Absolute)));

                HomeBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.45);
                HomeBackStopFull = new GradientStop(homecolor, 0.499);

                HomeRGB1 = new GradientStop(homecolor, .7);
                HomeRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 1);

                SliderGradientH1 = new GradientStop(homecolor, 0.0);
                SliderGradientH2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.4);

                SliderGradient.GradientStops.Add(SliderGradientH1);
                SliderGradient.GradientStops.Add(SliderGradientH2);
                sliderImages.SetHomeImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + homeSmallPicSource));

                HomeGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string awayTeamAlbany
        {
            set
            {
                awayPicSource = "Team Data/America East/Albany.png";
                awayPicSourceFull = "pack://application:,,,/WpfApplication1;component/" + awayPicSource;
                awaySmallPicSource = "Team Data/America East/AlbanyLogoSmall.png";
                awayTeam = "GREAT DANES";
                awayTeamLower = "Great Danes";
                
                awayTeamName.Content = awayTeam;

                awayChoice = 1;

                awayTeamPic.Source = new BitmapImage(new Uri(awayPicSource, UriKind.Relative));
                
                awaycolor = System.Windows.Media.Color.FromArgb(255, 70, 22, 106);
                awaybrush.Color = awaycolor;
                
                AwayBackColor(ref awaycolor);

                foreach (char letter in awayTeam)
                {
                    awayLetters++;
                    AwayNameBackdrop(ref awayLetters);
                }
                var awaydrawingcolor = System.Drawing.Color.FromArgb(255, 70, 22, 106);

                sliderImages.SetAwayImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + awaySmallPicSource));
                DataContext = sliderImages;

                AwayLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(awaycolor));
                AwayChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(awaycolor));

                ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                ChartAwayPoly.SetValue(Polygon.FillProperty, awaybrush);

                AwayBackStopFull = new GradientStop(awaycolor, 0.501);
                AwayBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, 0, 178, 92), 0.55);

                AwayRGB1 = new GradientStop(awaycolor, .7);
                AwayRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, awaycolor.R, awaycolor.G, awaycolor.B), 1);

                SliderGradientA1 = new GradientStop(awaycolor, 1);
                SliderGradientA2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.6);

                SliderGradient.GradientStops.Add(SliderGradientA1);
                SliderGradient.GradientStops.Add(SliderGradientA2);

                sliderImages.SetColorGradient(SliderGradient);
                sliderImages.SetAwayImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + awaySmallPicSource));
                DataContext = sliderImages;
                
                AwayGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string awayTeamBinghampton
        {
            set
            {
                awayPicSource = "Team Data/America East/binghampton.png";
                awayPicSourceFull = "pack://application:,,,/WpfApplication1;component/" + awayPicSource;
                awaySmallPicSource = "Team Data/America East/BinghamptonLogoSmall.png";
                awayTeam = "BEARCATS";
                awayTeamLower = "Bearcats";
                
                awayTeamName.Content = awayTeam;

                awayChoice = 2;

                awayTeamPic.Source = new BitmapImage(new Uri(awayPicSource, UriKind.Relative));
                
                awaycolor = System.Windows.Media.Color.FromArgb(255, 0, 178, 92);
                awaybrush.Color = awaycolor;
                
                AwayBackColor(ref awaycolor);

                foreach (char letter in awayTeam)
                {
                    awayLetters++;
                    AwayNameBackdrop(ref awayLetters);
                }
                var awaydrawingcolor = System.Drawing.Color.FromArgb(255, 0, 178, 92);

                AwayLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(awaycolor));
                AwayChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                AwayChartLogo.SetValue(System.Windows.Controls.Image.HeightProperty, 20d);
                AwayChartLogo.SetValue(System.Windows.Controls.Image.WidthProperty, 20d);
                XAxisAwayLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(awaycolor));

                ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                ChartAwayPoly.SetValue(Polygon.FillProperty, awaybrush);

                AwayBackStopFull = new GradientStop(awaycolor, 0.501);
                AwayBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, 0, 178, 92), 0.55);

                AwayRGB1 = new GradientStop(awaycolor, .7);
                AwayRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, awaycolor.R, awaycolor.G, awaycolor.B), 1);

                SliderGradientA1 = new GradientStop(awaycolor, 1);
                SliderGradientA2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.6);

                SliderGradient.GradientStops.Add(SliderGradientA1);
                SliderGradient.GradientStops.Add(SliderGradientA2);

                sliderImages.SetColorGradient(SliderGradient);
                sliderImages.SetAwayImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + awaySmallPicSource));
                DataContext = sliderImages;
                
                AwayGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string awayTeamHartford
        {
            set
            {
                awayPicSource = "Team Data/America East/hartford.png";
                awayPicSourceFull = "pack://application:,,,/WpfApplication1;component/" + awayPicSource;
                awaySmallPicSource = "Team Data/America East/HartfordLogoSmall.png";
                awayTeam = "HAWKS";
                awayTeamLower = "Hawks";
                
                awayTeamName.Content = awayTeam;

                awayChoice = 3;

                awayTeamPic.Source = new BitmapImage(new Uri(awayPicSource, UriKind.Relative));
                
                awaycolor = System.Windows.Media.Color.FromArgb(255, 237, 28, 46);
                awaybrush.Color = awaycolor;
                
                AwayBackColor(ref awaycolor);

                foreach (char letter in awayTeam)
                {
                    awayLetters++;
                    AwayNameBackdrop(ref awayLetters);
                }
                var awaydrawingcolor = System.Drawing.Color.FromArgb(255, 237, 28, 46);

                AwayLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(awaycolor));
                AwayChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(awaycolor));

                ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                ChartAwayPoly.SetValue(Polygon.FillProperty, awaybrush);

                AwayBackStopFull = new GradientStop(awaycolor, 0.501);
                AwayBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, 0, 178, 92), 0.55);

                AwayRGB1 = new GradientStop(awaycolor, .7);
                AwayRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, awaycolor.R, awaycolor.G, awaycolor.B), 1);

                SliderGradientA1 = new GradientStop(awaycolor, 1);
                SliderGradientA2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.6);

                SliderGradient.GradientStops.Add(SliderGradientA1);
                SliderGradient.GradientStops.Add(SliderGradientA2);

                sliderImages.SetColorGradient(SliderGradient);
                sliderImages.SetAwayImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + awaySmallPicSource));
                DataContext = sliderImages;

                AwayGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string awayTeamMaine
        {
            set
            {
                awayPicSource = "Team Data/America East/maine.png";
                awayPicSourceFull = "pack://application:,,,/WpfApplication1;component/" + awayPicSource;
                awaySmallPicSource = "Team Data/America East/MaineLogoSmall.png";
                awayTeam = "BLACK BEARS";
                awayTeamLower = "Black Bears";
                
                awayTeamName.Content = awayTeam;

                awayChoice = 4;

                awayTeamPic.Source = new BitmapImage(new Uri(awayPicSource, UriKind.Relative));
                
                awaycolor = System.Windows.Media.Color.FromArgb(255, 54, 152, 212);
                awaybrush.Color = awaycolor;
                
                AwayBackColor(ref awaycolor);

                foreach (char letter in awayTeam)
                {
                    awayLetters++;
                    AwayNameBackdrop(ref awayLetters);
                }
                var awaydrawingcolor = System.Drawing.Color.FromArgb(255, 54, 152, 212);

                AwayLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(awaycolor));
                AwayChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(awaycolor));

                ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                ChartAwayPoly.SetValue(Polygon.FillProperty, awaybrush);

                AwayBackStopFull = new GradientStop(awaycolor, 0.501);
                AwayBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, 0, 178, 92), 0.55);

                AwayRGB1 = new GradientStop(awaycolor, .7);
                AwayRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, awaycolor.R, awaycolor.G, awaycolor.B), 1);

                SliderGradientA1 = new GradientStop(awaycolor, 1);
                SliderGradientA2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.6);

                SliderGradient.GradientStops.Add(SliderGradientA1);
                SliderGradient.GradientStops.Add(SliderGradientA2);

                sliderImages.SetColorGradient(SliderGradient);
                sliderImages.SetAwayImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + awaySmallPicSource));
                DataContext = sliderImages;

                AwayGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string awayTeamUmbc
        {
            set
            {
                awayPicSource = "Team Data/America East/umbc.png";
                awayPicSourceFull = "pack://application:,,,/WpfApplication1;component/" + awayPicSource;
                awaySmallPicSource = "Team Data/America East/UMBCLogoSmall.png";
                awayTeam = "RETREIVERS";
                awayTeamLower = "Retreivers";
                awayTeamName.Content = awayTeam;
                
                awayChoice = 5;

                awayTeamPic.Source = new BitmapImage(new Uri("Team Data/America East/umbc.png", UriKind.Relative));
      
                awaycolor = System.Windows.Media.Color.FromArgb(255, 255, 194, 22);
                awaybrush.Color = awaycolor;
                
                AwayBackColor(ref awaycolor);

                foreach (char letter in awayTeam)
                {
                    awayLetters++;
                    AwayNameBackdrop(ref awayLetters);
                }
                var awaydrawingcolor = System.Drawing.Color.FromArgb(255, 255, 194, 22);

                AwayLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(awaycolor));
                AwayChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(awaycolor));

                ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                ChartAwayPoly.SetValue(Polygon.FillProperty, awaybrush);

                AwayBackStopFull = new GradientStop(awaycolor, 0.501);
                AwayBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, 0, 178, 92), 0.55);

                AwayRGB1 = new GradientStop(awaycolor, .7);
                AwayRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, awaycolor.R, awaycolor.G, awaycolor.B), 1);

                SliderGradientA1 = new GradientStop(awaycolor, 1);
                SliderGradientA2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.6);

                SliderGradient.GradientStops.Add(SliderGradientA1);
                SliderGradient.GradientStops.Add(SliderGradientA2);

                sliderImages.SetColorGradient(SliderGradient);
                sliderImages.SetAwayImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + awaySmallPicSource));
                DataContext = sliderImages;

                AwayGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string awayTeamUmassLowell
        {
            set
            {
                awayPicSource = "Team Data/America East/umasslowell.png";
                awayPicSourceFull = "pack://application:,,,/WpfApplication1;component/" + awayPicSource;
                awaySmallPicSource = "Team Data/America East/UMassLowellLogoSmall.png";
                awayTeam = "RIVER HAWKS";
                awayTeamLower = "River Hawks";
                awayTeamName.Content = awayTeam;

                awayChoice = 6;

                awayTeamPic.Source = new BitmapImage(new Uri(awayPicSource, UriKind.Relative));

                awaycolor = System.Windows.Media.Color.FromArgb(255, 0, 83, 161);
                awaybrush.Color = awaycolor;
                
                AwayBackColor(ref awaycolor);

                foreach (char letter in awayTeam)
                {
                    awayLetters++;
                    AwayNameBackdrop(ref awayLetters);
                }
                var awaydrawingcolor = System.Drawing.Color.FromArgb(255, 0, 83, 161);

                AwayLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(awaycolor));
                AwayChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(awaycolor));

                ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                ChartAwayPoly.SetValue(Polygon.FillProperty, awaybrush);

                AwayBackStopFull = new GradientStop(awaycolor, 0.501);
                AwayBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, 0, 178, 92), 0.55);

                AwayRGB1 = new GradientStop(awaycolor, .7);
                AwayRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, awaycolor.R, awaycolor.G, awaycolor.B), 1);

                SliderGradientA1 = new GradientStop(awaycolor, 1);
                SliderGradientA2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.6);

                SliderGradient.GradientStops.Add(SliderGradientA1);
                SliderGradient.GradientStops.Add(SliderGradientA2);

                sliderImages.SetColorGradient(SliderGradient);
                sliderImages.SetAwayImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + awaySmallPicSource));
                DataContext = sliderImages;

                AwayGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string awayTeamUnh
        {
            set
            {
                awayPicSource = "Team Data/America East/unh.png";
                awayPicSourceFull = "pack://application:,,,/WpfApplication1;component/" + awayPicSource;
                awaySmallPicSource = "Team Data/America East/UnhLogoSmall.png";
                awayTeam = "WILDCATS";
                awayTeamLower = "Wildcats";
                 
                awayTeamName.Content = awayTeam;

                awayChoice = 7;

                awayTeamPic.Source = new BitmapImage(new Uri(awayPicSource, UriKind.Relative));
        
                awaycolor = System.Windows.Media.Color.FromArgb(255, 0, 39, 93);
                awaybrush.Color = awaycolor;
                
                AwayBackColor(ref awaycolor);

                foreach (char letter in awayTeam)
                {
                    awayLetters++;
                    AwayNameBackdrop(ref awayLetters);
                }
                var awaydrawingcolor = System.Drawing.Color.FromArgb(255, 0, 39, 93);

                AwayLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(awaycolor));
                AwayChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(awaycolor));

                ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                ChartAwayPoly.SetValue(Polygon.FillProperty, awaybrush);

                AwayBackStopFull = new GradientStop(awaycolor, 0.501);
                AwayBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, 0, 178, 92), 0.55);

                AwayRGB1 = new GradientStop(awaycolor, .7);
                AwayRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, awaycolor.R, awaycolor.G, awaycolor.B), 1);

                SliderGradientA1 = new GradientStop(awaycolor, 1);
                SliderGradientA2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.6);

                SliderGradient.GradientStops.Add(SliderGradientA1);
                SliderGradient.GradientStops.Add(SliderGradientA2);

                sliderImages.SetColorGradient(SliderGradient);
                sliderImages.SetAwayImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + awaySmallPicSource));
                DataContext = sliderImages;

                AwayGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string awayTeamStony
        {
            set
            {
                awayPicSource = "Team Data/America East/seawolves.png";
                awayPicSourceFull = "pack://application:,,,/WpfApplication1;component/" + awayPicSource;
                awaySmallPicSource = "Team Data/America East/StonyBrookLogoSmall.png";
                awayTeam = "SEAWOLVES";
                awayTeamLower = "Seawolves";
                
                awayTeamName.Content = awayTeam;   

                awayChoice = 8;

                awayTeamPic.Source = new BitmapImage(new Uri(awayPicSource, UriKind.Relative));
                
                         
                awaycolor = System.Windows.Media.Color.FromArgb(255, 184, 17, 55);
                awaybrush.Color = awaycolor;
                
                AwayBackColor(ref awaycolor);

                foreach (char letter in awayTeam)
                {
                    awayLetters++;
                    AwayNameBackdrop(ref awayLetters);
                }
                var awaydrawingcolor = System.Drawing.Color.FromArgb(255, 184, 17, 55);

                AwayLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(awaycolor));
                AwayChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(awaycolor));

                ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                ChartAwayPoly.SetValue(Polygon.FillProperty, awaybrush);

                AwayBackStopFull = new GradientStop(awaycolor, 0.501);
                AwayBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, 0, 178, 92), 0.55);

                AwayRGB1 = new GradientStop(awaycolor, .7);
                AwayRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, awaycolor.R, awaycolor.G, awaycolor.B), 1);

                SliderGradientA1 = new GradientStop(awaycolor, 1);
                SliderGradientA2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.6);

                SliderGradient.GradientStops.Add(SliderGradientA1);
                SliderGradient.GradientStops.Add(SliderGradientA2);

                sliderImages.SetColorGradient(SliderGradient);
                sliderImages.SetAwayImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + awaySmallPicSource));
                DataContext = sliderImages;

                AwayGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        public string awayTeamVermont
        {
            set
            {
                awayPicSource = "Team Data/America East/vermont.png";
                awayPicSourceFull = "pack://application:,,,/WpfApplication1;component/" + awayPicSource;
                awaySmallPicSource = "Team Data/America East/VermontLogoSmall.png";
                awayTeam = "CATAMOUNTS";
                awayTeamLower = "Catamounts";
                
                awayTeamName.Content = awayTeam; 

                awayChoice = 9;

                awayTeamPic.Source = new BitmapImage(new Uri(awayPicSource, UriKind.Relative));

                awaycolor = System.Windows.Media.Color.FromArgb(255, 1, 56, 35);
                awaybrush.Color = awaycolor;
                
                AwayBackColor(ref awaycolor);

                foreach (char letter in awayTeam)
                {
                    awayLetters++;
                    AwayNameBackdrop(ref awayLetters);
                }
                var awaydrawingcolor = System.Drawing.Color.FromArgb(255, 1, 56, 35);

                AwayLineColor = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.BackgroundProperty, new SolidColorBrush(awaycolor));
                AwayChartLogo.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awaySmallPicSource, UriKind.Relative)));
                XAxisAwayLegendLabel1.SetValue(System.Windows.Controls.Label.BackgroundProperty, new SolidColorBrush(awaycolor));

                ChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                AChartBackgroundAwayImage.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(awayPicSourceFull, UriKind.Absolute)));
                ChartAwayPoly.SetValue(Polygon.FillProperty, awaybrush);

                AwayBackStopFull = new GradientStop(awaycolor, 0.501);
                AwayBackStopFade = new GradientStop(System.Windows.Media.Color.FromArgb(0, 0, 178, 92), 0.55);

                AwayRGB1 = new GradientStop(awaycolor, .7);
                AwayRGB2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, awaycolor.R, awaycolor.G, awaycolor.B), 1);

                SliderGradientA1 = new GradientStop(awaycolor, 1);
                SliderGradientA2 = new GradientStop(System.Windows.Media.Color.FromArgb(0, homecolor.R, homecolor.G, homecolor.B), 0.6);

                SliderGradient.GradientStops.Add(SliderGradientA1);
                SliderGradient.GradientStops.Add(SliderGradientA2);

                sliderImages.SetColorGradient(SliderGradient);
                sliderImages.SetAwayImageData(File.ReadAllBytes(@"\WpfApplication1\WpfApplication1\" + awaySmallPicSource));
                DataContext = sliderImages;

                AwayGraphMaker = true;
                GraphMaker(ref graphmaker);
            }
        }
        #endregion
        #region home and away brushes
        public System.Windows.Media.Brush homeTeamBrush
        {
            get
            {
                return homebrush;
            }
            set
            {
                homeTeamBrush = homebrush;
            }
        }
        public System.Windows.Media.Brush awayTeamBrush
        {
            get
            {
                return awaybrush;
            }
            set
            {
                awayTeamBrush = awaybrush;
            }
        }
        #endregion
        #endregion
        #region teamrecords
        public int inthomeWins
        {
            set
            {
                homeWins = value;
                if(homeWins < 10)
                {
                    homeWinsAdj = "0" + homeWins.ToString();
                }
                else { homeWinsAdj = homeWins.ToString(); }
            homeTeamRecord.Content = homeWinsAdj + "-" + homeLossesAdj;
            awayTeamRecord.Content = awayWinsAdj + "-" + awayLossesAdj;
            }
        }
        public int inthomeLosses
        {
            set
            {
                homeLosses = value;
                if (homeLosses < 10)
                {
                    homeLossesAdj = "0" + homeLosses.ToString();
                }
                else { homeLossesAdj = homeLosses.ToString(); }
            homeTeamRecord.Content = homeWinsAdj + "-" + homeLossesAdj;
            awayTeamRecord.Content = awayWinsAdj + "-" + awayLossesAdj;
            }
        }
        public int intawayWins
        {
            set
            {
                awayWins = value;
                if (awayWins < 10)
                {
                    awayWinsAdj = "0" + awayWins.ToString();
                }
                else { awayWinsAdj = awayWins.ToString(); }
            homeTeamRecord.Content = homeWinsAdj + "-" + homeLossesAdj;
            awayTeamRecord.Content = awayWinsAdj + "-" + awayLossesAdj;
            }
        }
        public int intawayLosses
        {
            set
            {
                awayLosses = value;
                if (awayLosses < 10)
                {
                    awayLossesAdj = "0" + awayLosses.ToString();
                }
                else { awayLossesAdj = awayLosses.ToString(); }
            homeTeamRecord.Content = homeWinsAdj + "-" + homeLossesAdj;
            awayTeamRecord.Content = awayWinsAdj + "-" + awayLossesAdj;
            WinningPercentValue(ref winpercent);
            }
        }
        #endregion
        #region timer
        //TIMER
        public int timer
        {
            set
            {
                elapsedTicksCountNew = value;
                elapsedTicks = 0;
                timer1.Stop();
                timer2.Stop();
                hcHome.Stop();
                hcAway.Stop();
                stopButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                var timespan = TimeSpan.FromMilliseconds(elapsedTicksCountNew);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
                elapsedTicksSum = 120000 - (elapsedTicksCountNew / 10);
                periodstart = false;
                WinningPercentValue(ref winpercent);
            }
        }
        public void dispatchertimer_Tick(object sender, EventArgs e)
        {
            TimeSpan Difference = DateTime.Now.Subtract(Time);
            totalTimeCount = Difference.Milliseconds;

            totalTimeint10 = totalTime;
            elapsedTicks = Difference.Ticks / 100000;
            elapsedTicksClock = (elapsedTicksClockSum*10) + Difference.Ticks / 10000;
            //Milliseconds Left Timer
            elapsedTicksNew = elapsedTicksSum + Difference.Ticks / 100000;
            if (secHalf == false)
            {
                elapsedTicksCount10 = (FH + SH) - elapsedTicksNew;
            }
            if (secHalf == true)
            {
                elapsedTicksCount10 = (FH) - elapsedTicksNew;
            }
            if (overtime == true)
            {
                elapsedTicksCount10 = (OT) - elapsedTicksNew;
            }
            
            //Countdown timer
            if (periodstart == true && timerStart == true && overtime == false)
            {
                elapsedTicksCount1000 = FH0 - elapsedTicksClock;
            }
            if (periodstart == false && timerStart == true && overtime == false)
            {
                elapsedTicksCount1000 = elapsedTicksCountNew - elapsedTicksClock;
            }
            if (periodstart == true && timerStart == true && overtime == true)
            {
                elapsedTicksCount1000 = OT0 - elapsedTicksClock;
            }
            if (periodstart == false && timerStart == true && overtime == true)
            {
                elapsedTicksCount1000 = elapsedTicksCountNew - elapsedTicksClock;
            }

            var timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
            time = string.Format("{0:D2}:{1:D2}.{2:D0}",timespan.Minutes, timespan.Seconds, timespan.Milliseconds/100);
            totalTimeLeft.Content = time;
            if(elapsedTicksCount1000 <= 0 || elapsedTicksCountNew <= 0 && secHalf == false)
            {
                totalTimeLeft.Content = "00:00.0";
                timer1.Stop();
                timer2.Stop();
                hcHome.Stop();
                hcAway.Stop();
                plus1min.IsEnabled = false;
                plus1sec.IsEnabled = false;
                plus1mil.IsEnabled = false;
                minus1min.IsEnabled = false;
                minus1sec.IsEnabled = false;
                minus1mil.IsEnabled = false;
                startButton.Visibility = Visibility.Hidden;
                pauseButton.Visibility = Visibility.Hidden;
                startSecHalfButton.Visibility = Visibility.Visible;
                startSecHalfButton.Focus();
                WinningPercentAnimation(ref wpAnimation);
                periodstart = true;
                timerStart = false;
            }
            if(elapsedTicksCount1000 <= 0 && secHalf == true)
            {
                totalTimeLeft.Content = "00:00.0";
                timer1.Stop();
                timer2.Stop();
                hcHome.Stop();
                hcAway.Stop();
                plus1min.IsEnabled = false;
                plus1sec.IsEnabled = false;
                plus1mil.IsEnabled = false;
                minus1min.IsEnabled = false;
                minus1sec.IsEnabled = false;
                minus1mil.IsEnabled = false;
                startButton.Visibility = Visibility.Hidden;
                pauseButton.Visibility = Visibility.Hidden;
                startSecHalfButton.Visibility = Visibility.Visible;
                startSecHalfButton.Focus();
                regulation = true;
                periodstart = true;
                WinningPercentAnimation(ref wpAnimation);
                
            }
            WinningPercentValue(ref winpercent);
            graphUpdateTimer.Start();
            graphMarkerRemove.Start();
        }
        //HOT-COLD Timers
        public void Homedispatchertimer_Tick(object sender, EventArgs e)
        {
            if (secHalf == false)
            {
                TimeSpan Difference = DateTime.Now.Subtract(Time);
                if (timerStart == true)
                {
                    hcHomeCountAddOn = Difference.Ticks / 100000;
                }
                if (timerStart == false)
                {
                    hcHomeCountAddOn = 0;
                }
                HomeelapsedTicks = hcHomeCountAddOn;
                hcHomeNSCountTicks = hcHomeCountAddOn;
                hcHomeTime = hcHomeSum + hcHomeCountAddOn;
                hcHomeTotalTime = hcHomeTotalSum + hcHomeCountAddOn;
                hcHomeNoShoot = hcHomeNSCount + hcHomeCountAddOn;
                hcHomeNoShoot1000 = hcHomeNoShoot * 10;
                if (hcHomeNoShoot >= 12000)
                {
                    hcHomeNS.Visibility = Visibility.Visible;
                }
                if (hcHomeNoShoot < 12000)
                {
                    hcHomeNS.Visibility = Visibility.Hidden;
                }
                if (hcHomeTime >= 3000)
                {
                    hcHomeSum = hcHomeSum - 3000;
                }
                if (hcHomeTotalTime == 0)
                {
                    hcHomeSum = hcHomeSum - long.Parse(hcHomeTime.ToString());
                }
                hcHomeTimer.Content = hcHomeTime;
                hcHomeTT.Content = hcHomeTotalTime;
                if (hcHomeTotalTime <= -15000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl5hotnew.png", UriKind.Relative));
                    //hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl5hotnew.png", UriKind.Relative));
                    //opacityHomeValue = (-float.Parse(hcHomeTotalTime.ToString()) - 12000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > -15000 && hcHomeTotalTime <= -12000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl4hotnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl5hotnew.png", UriKind.Relative));
                    opacityHomeValue = (-float.Parse(hcHomeTotalTime.ToString()) - 12000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > -12000 && hcHomeTotalTime <= -9000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl3hotnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl4hotnew.png", UriKind.Relative));
                    opacityHomeValue = (-float.Parse(hcHomeTotalTime.ToString()) - 9000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > -9000 && hcHomeTotalTime <= -6000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl2hotnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl3hotnew.png", UriKind.Relative));
                    opacityHomeValue = (-float.Parse(hcHomeTotalTime.ToString()) - 6000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > -6000 && hcHomeTotalTime <= -3000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl1hotnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl2hotnew.png", UriKind.Relative));
                    opacityHomeValue = (-float.Parse(hcHomeTotalTime.ToString()) - 3000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > -3000 && hcHomeTotalTime <= -1)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl0.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl1hotnew.png", UriKind.Relative));
                    opacityHomeValue = -float.Parse(hcHomeTotalTime.ToString()) / 3000;
                    opacitybackvalue = 1 - (-float.Parse(hcHomeTotalTime.ToString()) / 3000);
                }
                if (hcHomeTotalTime == 0)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl0.png", UriKind.Relative));
                    hotcoldHome.Source = null;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > 0 && hcHomeTotalTime <= 3000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl0.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl1coldnew.png", UriKind.Relative));
                    opacitybackvalue = 1 - float.Parse(hcHomeTotalTime.ToString()) / 3000;
                    opacityHomeValue = float.Parse(hcHomeTotalTime.ToString()) / 3000;
                }
                if (hcHomeTotalTime > 3000 && hcHomeTotalTime <= 6000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl1coldnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl2coldnew.png", UriKind.Relative));
                    opacityHomeValue = (float.Parse(hcHomeTotalTime.ToString()) - 3000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > 6000 && hcHomeTotalTime <= 9000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl2coldnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl3coldnew.png", UriKind.Relative));
                    opacityHomeValue = (float.Parse(hcHomeTotalTime.ToString()) - 6000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > 9000 && hcHomeTotalTime <= 12000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl3coldnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl4coldnew.png", UriKind.Relative));
                    opacityHomeValue = (float.Parse(hcHomeTotalTime.ToString()) - 9000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > 12000 && hcHomeTotalTime <= 15000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl4coldnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl5coldnew.png", UriKind.Relative));
                    opacityHomeValue = (float.Parse(hcHomeTotalTime.ToString()) - 12000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > 15000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl5coldnew.png", UriKind.Relative));
                    opacityHomeValue = (float.Parse(hcHomeTotalTime.ToString()) - 15000) / 3000;
                    opacitybackvalue = 1;
                }
                hcHomeSlider.Value = opacityHomeValue;
                hcHomeBackSlider.Value = opacitybackvalue;
                hotcoldHomebackimage.Opacity = hcHomeBackSlider.Value;
                hotcoldHome.Opacity = hcHomeSlider.Value;
                var hcHometimespan = TimeSpan.FromMilliseconds(hcHomeNoShoot1000);
                hcHomeNSTime = string.Format("{0:D2}:{1:D2}", hcHometimespan.Minutes, hcHometimespan.Seconds);
                hcHomeNS.Content = hcHomeNSTime;
            }
            if (secHalf == true)
            {
                TimeSpan Difference2 = DateTime.Now.Subtract(Time);
                if (timerStart == true)
                {
                    hcHomeCountAddOn = Difference2.Ticks / 100000;
                }
                if (timerStart == false)
                {
                    hcHomeCountAddOn = 0;
                }
                HomeelapsedTicks = hcHomeCountAddOn;
                hcHomeNSCountTicks = hcHomeCountAddOn;
                hcHomeTime = hcHomeSum + hcHomeCountAddOn;
                hcHomeTotalTime = hcHomeTotalSum + hcHomeCountAddOn;
                hcHomeNoShoot = hcHomeNSCount + hcHomeCountAddOn;
                hcHomeNoShoot1000 = hcHomeNoShoot * 10;
                if (hcHomeNoShoot >= 12000)
                {
                    hcHomeNS.Visibility = Visibility.Visible;
                }
                if (hcHomeNoShoot < 12000)
                {
                    hcHomeNS.Visibility = Visibility.Hidden;
                }
                if (hcHomeTime >= 3000)
                {
                    hcHomeSum = hcHomeSum - 3000;
                }
                if (hcHomeTotalTime == 0)
                {
                    hcHomeSum = hcHomeSum - long.Parse(hcHomeTime.ToString());
                }
                hcHomeTimer.Content = hcHomeTime;
                hcHomeTT.Content = hcHomeTotalTime;
                if (hcHomeTotalTime <= -15000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl5hotnew.png", UriKind.Relative));
                    //hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl5hotnew.png", UriKind.Relative));
                    //opacityHomeValue = (-float.Parse(hcHomeTotalTime.ToString()) - 12000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > -15000 && hcHomeTotalTime <= -12000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl4hotnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl5hotnew.png", UriKind.Relative));
                    opacityHomeValue = (-float.Parse(hcHomeTotalTime.ToString()) - 12000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > -12000 && hcHomeTotalTime <= -9000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl3hotnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl4hotnew.png", UriKind.Relative));
                    opacityHomeValue = (-float.Parse(hcHomeTotalTime.ToString()) - 9000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > -9000 && hcHomeTotalTime <= -6000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl2hotnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl3hotnew.png", UriKind.Relative));
                    opacityHomeValue = (-float.Parse(hcHomeTotalTime.ToString()) - 6000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > -6000 && hcHomeTotalTime <= -3000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl1hotnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl2hotnew.png", UriKind.Relative));
                    opacityHomeValue = (-float.Parse(hcHomeTotalTime.ToString()) - 3000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > -3000 && hcHomeTotalTime <= -1)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl0.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl1hotnew.png", UriKind.Relative));
                    opacityHomeValue = -float.Parse(hcHomeTotalTime.ToString()) / 3000;
                    opacitybackvalue = 1 - (-float.Parse(hcHomeTotalTime.ToString()) / 3000);
                }
                if (hcHomeTotalTime == 0)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl0.png", UriKind.Relative));
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > 0 && hcHomeTotalTime <= 3000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl0.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl1coldnew.png", UriKind.Relative));
                    opacitybackvalue = 1 - float.Parse(hcHomeTime.ToString()) / 3000;
                    opacityHomeValue = float.Parse(hcHomeTime.ToString()) / 3000;
                }
                if (hcHomeTotalTime > 3000 && hcHomeTotalTime <= 6000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl1coldnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl2coldnew.png", UriKind.Relative));
                    opacityHomeValue = float.Parse(hcHomeTime.ToString()) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > 6000 && hcHomeTotalTime <= 9000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl2coldnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl3coldnew.png", UriKind.Relative));
                    opacityHomeValue = float.Parse(hcHomeTime.ToString()) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > 9000 && hcHomeTotalTime <= 12000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl3coldnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl4coldnew.png", UriKind.Relative));
                    opacityHomeValue = float.Parse(hcHomeTime.ToString()) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > 12000 && hcHomeTotalTime <= 15000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl4coldnew.png", UriKind.Relative));
                    hotcoldHome.Source = new BitmapImage(new Uri("Data Elements/lvl5coldnew.png", UriKind.Relative));
                    opacityHomeValue = float.Parse(hcHomeTime.ToString()) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcHomeTotalTime > 15000)
                {
                    hotcoldHomebackimage.Source = new BitmapImage(new Uri("Data Elements/lvl5coldnew.png", UriKind.Relative));
                    opacityHomeValue = float.Parse(hcHomeTime.ToString()) / 3000;
                    opacitybackvalue = 1;
                }
                hcHomeSlider.Value = opacityHomeValue;
                hcHomeBackSlider.Value = opacitybackvalue;
                hotcoldHomebackimage.Opacity = hcHomeBackSlider.Value;
                hotcoldHome.Opacity = hcHomeSlider.Value;
                var hcHometimespan = TimeSpan.FromMilliseconds(hcHomeNoShoot1000);
                hcHomeNSTime = string.Format("{0:D2}:{1:D2}", hcHometimespan.Minutes, hcHometimespan.Seconds);
                //hcHomeNSTime = (string)(App.Current.Resources["HomeNSTime"]);
                hcHomeNS.Content = hcHomeNSTime;
            }
        }
        public void Awaydispatchertimer_Tick(object sender, EventArgs e)
        {
            if (secHalf == false)
            {
                TimeSpan Difference = DateTime.Now.Subtract(Time);
                if (timerStart == true)
                {
                    hcAwayCountAddOn = Difference.Ticks / 100000;
                }
                if (timerStart == false || awayNeutral == true)
                {
                    hcAwayCountAddOn = 0;
                    awayNeutral = false;
                }
                AwayelapsedTicks = hcAwayCountAddOn;
                hcAwayNSCountTicks = hcAwayCountAddOn;
                hcAwayTime = hcAwaySum + hcAwayCountAddOn;
                hcAwayTotalTime = hcAwayTotalSum + hcAwayCountAddOn;
                hcAwayNoShoot = hcAwayNSCount + hcAwayCountAddOn;
                hcAwayNoShoot1000 = hcAwayNoShoot * 10;
                if (hcAwayNoShoot >= 12000)
                {
                    hcAwayNS.Visibility = Visibility.Visible;
                }
                if (hcAwayNoShoot < 12000)
                {
                    hcAwayNS.Visibility = Visibility.Hidden;
                }
                if (hcAwayTime >= 3000)
                {
                    hcAwaySum = hcAwaySum - 3000;
                }
                if (hcAwayTotalTime == 0)
                {
                    hcAwaySum = hcAwaySum - long.Parse(hcAwayTime.ToString());
                }
                hcAwayTimer.Content = hcAwayTime;
                hcAwayTT.Content = hcAwayTotalTime;
                if (hcAwayTotalTime <= -15000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl5hotnew.png", UriKind.Relative));
                    //hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl5hotnew.png", UriKind.Relative));
                    //opacityAwayValue = (-float.Parse(hcAwayTotalTime.ToString()) - 12000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > -15000 && hcAwayTotalTime <= -12000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl4hotnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl5hotnew.png", UriKind.Relative));
                    opacityAwayValue = (-float.Parse(hcAwayTotalTime.ToString()) - 12000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > -12000 && hcAwayTotalTime <= -9000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl3hotnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl4hotnew.png", UriKind.Relative));
                    opacityAwayValue = (-float.Parse(hcAwayTotalTime.ToString()) - 9000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > -9000 && hcAwayTotalTime <= -6000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl2hotnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl3hotnew.png", UriKind.Relative));
                    opacityAwayValue = (-float.Parse(hcAwayTotalTime.ToString()) - 6000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > -6000 && hcAwayTotalTime <= -3000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl1hotnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl2hotnew.png", UriKind.Relative));
                    opacityAwayValue = (-float.Parse(hcAwayTotalTime.ToString()) - 3000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > -3000 && hcAwayTotalTime <= -1)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl0.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl1hotnew.png", UriKind.Relative));
                    opacityAwayValue = -float.Parse(hcAwayTotalTime.ToString()) / 3000;
                    opacitybackvalue = 1 - (-float.Parse(hcAwayTotalTime.ToString()) / 3000);
                }
                if (hcAwayTotalTime == 0)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl0.png", UriKind.Relative));
                    hotcoldAway.Source = null;
                    opacityAwayValue = 0;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > 0 && hcAwayTotalTime <= 3000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl0.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl1coldnew.png", UriKind.Relative));
                    opacitybackvalue = 1 - float.Parse(hcAwayTotalTime.ToString()) / 3000;
                    opacityAwayValue = float.Parse(hcAwayTotalTime.ToString()) / 3000;
                }
                if (hcAwayTotalTime > 3000 && hcAwayTotalTime <= 6000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl1coldnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl2coldnew.png", UriKind.Relative));
                    opacityAwayValue = (float.Parse(hcAwayTotalTime.ToString()) - 3000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > 6000 && hcAwayTotalTime <= 9000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl2coldnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl3coldnew.png", UriKind.Relative));
                    opacityAwayValue = (float.Parse(hcAwayTotalTime.ToString()) - 6000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > 9000 && hcAwayTotalTime <= 12000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl3coldnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl4coldnew.png", UriKind.Relative));
                    opacityAwayValue = (float.Parse(hcAwayTotalTime.ToString()) - 9000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > 12000 && hcAwayTotalTime <= 15000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl4coldnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl5coldnew.png", UriKind.Relative));
                    opacityAwayValue = (float.Parse(hcAwayTotalTime.ToString()) - 12000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > 15000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl5coldnew.png", UriKind.Relative));
                    opacityAwayValue = (float.Parse(hcAwayTotalTime.ToString()) - 15000) / 3000;
                    opacitybackvalue = 1;
                }
                hcAwaySlider.Value = opacityAwayValue;
                hcAwayBackSlider.Value = opacitybackvalue;
                hotcoldAwaybackimage.Opacity = hcAwayBackSlider.Value;
                hotcoldAway.Opacity = hcAwaySlider.Value;
                var hcAwaytimespan = TimeSpan.FromMilliseconds(hcAwayNoShoot1000);
                hcAwayNSTime = string.Format("{0:D2}:{1:D2}", hcAwaytimespan.Minutes, hcAwaytimespan.Seconds);
                hcAwayNS.Content = hcAwayNSTime;
            }
            if (secHalf == true)
            {
                TimeSpan Difference2 = DateTime.Now.Subtract(Time);
                if (timerStart == true)
                {
                    hcAwayCountAddOn = Difference2.Ticks / 100000;
                }
                if (timerStart == false)
                {
                    hcAwayCountAddOn = 0;
                }
                AwayelapsedTicks = hcAwayCountAddOn;
                hcAwayNSCountTicks = hcAwayCountAddOn;
                hcAwayTime = hcAwaySum + hcAwayCountAddOn;
                hcAwayTotalTime = hcAwayTotalSum + hcAwayCountAddOn;
                hcAwayNoShoot = hcAwayNSCount + hcAwayCountAddOn;
                hcAwayNoShoot1000 = hcAwayNoShoot * 10;
                if (hcAwayNoShoot >= 12000)
                {
                    hcAwayNS.Visibility = Visibility.Visible;
                }
                if (hcAwayNoShoot < 12000)
                {
                    hcAwayNS.Visibility = Visibility.Hidden;
                }
                if (hcAwayTime >= 3000)
                {
                    hcAwaySum = hcAwaySum - 3000;
                }
                if (hcAwayTotalTime == 0)
                {
                    hcAwaySum = hcAwaySum - long.Parse(hcAwayTime.ToString());
                }
                hcAwayTimer.Content = hcAwayTime;
                hcAwayTT.Content = hcAwayTotalTime;
                if (hcAwayTotalTime <= -15000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl5hotnew.png", UriKind.Relative));
                    //hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl5hotnew.png", UriKind.Relative));
                    //opacityAwayValue = (-float.Parse(hcAwayTotalTime.ToString()) - 12000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > -15000 && hcAwayTotalTime <= -12000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl4hotnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl5hotnew.png", UriKind.Relative));
                    opacityAwayValue = (-float.Parse(hcAwayTotalTime.ToString()) - 12000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > -12000 && hcAwayTotalTime <= -9000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl3hotnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl4hotnew.png", UriKind.Relative));
                    opacityAwayValue = (-float.Parse(hcAwayTotalTime.ToString()) - 9000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > -9000 && hcAwayTotalTime <= -6000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl2hotnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl3hotnew.png", UriKind.Relative));
                    opacityAwayValue = (-float.Parse(hcAwayTotalTime.ToString()) - 6000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > -6000 && hcAwayTotalTime <= -3000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl1hotnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl2hotnew.png", UriKind.Relative));
                    opacityAwayValue = (-float.Parse(hcAwayTotalTime.ToString()) - 3000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > -3000 && hcAwayTotalTime <= -1)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl0.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl1hotnew.png", UriKind.Relative));
                    opacityAwayValue = -float.Parse(hcAwayTotalTime.ToString()) / 3000;
                    opacitybackvalue = 1 - (-float.Parse(hcAwayTotalTime.ToString()) / 3000);
                }
                if (hcAwayTotalTime == 0)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl0.png", UriKind.Relative));
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > 0 && hcAwayTotalTime <= 3000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl0.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl1coldnew.png", UriKind.Relative));
                    opacitybackvalue = 1 - float.Parse(hcAwayTotalTime.ToString()) / 3000;
                    opacityAwayValue = float.Parse(hcAwayTotalTime.ToString()) / 3000;
                }
                if (hcAwayTotalTime > 3000 && hcAwayTotalTime <= 6000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl1coldnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl2coldnew.png", UriKind.Relative));
                    opacityAwayValue = (float.Parse(hcAwayTotalTime.ToString()) - 3000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > 6000 && hcAwayTotalTime <= 9000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl2coldnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl3coldnew.png", UriKind.Relative));
                    opacityAwayValue = (float.Parse(hcAwayTime.ToString()) - 6000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > 9000 && hcAwayTotalTime <= 12000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl3coldnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl4coldnew.png", UriKind.Relative));
                    opacityAwayValue = (float.Parse(hcAwayTime.ToString()) - 9000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > 12000 && hcAwayTotalTime <= 15000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl4coldnew.png", UriKind.Relative));
                    hotcoldAway.Source = new BitmapImage(new Uri("Data Elements/lvl5coldnew.png", UriKind.Relative));
                    opacityAwayValue = (float.Parse(hcAwayTime.ToString()) - 12000) / 3000;
                    opacitybackvalue = 1;
                }
                if (hcAwayTotalTime > 15000)
                {
                    hotcoldAwaybackimage.Source = new BitmapImage(new Uri("Data Elements/lvl5coldnew.png", UriKind.Relative));
                    opacityAwayValue = (float.Parse(hcAwayTime.ToString()) - 15000) / 3000;
                    opacitybackvalue = 1;
                }
                hcAwaySlider.Value = opacityAwayValue;
                hcAwayBackSlider.Value = opacitybackvalue;
                hotcoldAwaybackimage.Opacity = hcAwayBackSlider.Value;
                hotcoldAway.Opacity = hcAwaySlider.Value;
                var hcAwaytimespan = TimeSpan.FromMilliseconds(hcAwayNoShoot1000);
                hcAwayNSTime = string.Format("{0:D2}:{1:D2}", hcAwaytimespan.Minutes, hcAwaytimespan.Seconds);
                hcAwayNS.Content = hcAwayNSTime;
            }
            
        }
        private void startButton_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if(timerStart == false)
            {
                timer1.Tick += new System.EventHandler(dispatchertimer_Tick);
                //Percent_Graph.TeamStringUpdate += new System.EventHandler(dispatchertimer_Tick);
                timer1.Interval = new TimeSpan(0, 0, 0);
                Time = DateTime.Now;
                timer1.Start();
                timerStart = true;                
            }
            else
            {
                timer1.Tick += new System.EventHandler(dispatchertimer_Tick);
                timer1.Interval = new TimeSpan(0, 0, 0);
                Time = DateTime.Now;
                timer1.Start();
            }
            if(secHalf == true)
            {
                if (timerStart == false)
                {
                    timer2.Tick += new System.EventHandler(dispatchertimer_Tick);
                    timer2.Interval = new TimeSpan(0, 0, 0);
                    Time = DateTime.Now;
                    timer2.Start();
                    timerStart = true;
                }
                else
                {
                    timer2.Tick += new System.EventHandler(dispatchertimer_Tick);
                    timer2.Interval = new TimeSpan(0, 0, 0);
                    Time = DateTime.Now;
                    timer2.Start();
                }
            }
            startButton.IsEnabled = false;
            pauseButton.IsEnabled = true;
            stopButton.IsEnabled = true;
            timerStart = true;
            pauseButton.Focus();
            hcHome.Tick += new System.EventHandler(Homedispatchertimer_Tick);
            hcHome.Interval = new TimeSpan(0, 0, 0);
            hcHome.Start();
            hcAway.Tick += new System.EventHandler(Awaydispatchertimer_Tick);
            hcAway.Interval = new TimeSpan(0, 0, 0);
            hcAway.Start();
            graphUpdateTimer.Start();
        }                    

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            totalTimeAdd = totalTime;
            elapsedTicksSum = elapsedTicksSum + elapsedTicks;
            elapsedTicksClockSum = elapsedTicksClockSum + elapsedTicks;
            hcHomeSum = hcHomeSum + HomeelapsedTicks;
            hcHomeTotalSum = hcHomeTotalSum + HomeelapsedTicks;
            hcHomeNSCount = hcHomeNSCount + hcHomeNSCountTicks;
            hcAwaySum = hcAwaySum + AwayelapsedTicks;
            hcAwayTotalSum = hcAwayTotalSum + AwayelapsedTicks;
            hcAwaycurrenttime = hcAwayTime + hcAwaycurrenttime;
            hcAwayNSCount = hcAwayNSCount + hcAwayNSCountTicks;
            timer1.Stop();
            timer2.Stop();
            hcHome.Stop();
            hcAway.Stop();
            graphUpdateTimer.Stop();
            graphMarkerRemove.Stop();
            timerStart = false;
            startButton.IsEnabled = true;
            pauseButton.IsEnabled = false;
            stopButton.IsEnabled = true;
            totalTime = totalTimeAdd + totalTime;
            startButton.Focus();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            elapsedTicksSum = 0;
            elapsedTicksClockSum = 0;
            timer1.Stop();
            timer2.Stop();
            hcHome.Stop();
            hcAway.Stop();
            startButton.IsEnabled = true;
            pauseButton.IsEnabled = false;
            stopButton.IsEnabled = false;
            timerStart = false;
        }
        private void enterTimeButton_Click(object sender, RoutedEventArgs e)
        {
            var enterTime = new enterTime();
            enterTime.Show();
        }
        #endregion
        #region timestopbuttons
        private void plus1min_Click(object sender, RoutedEventArgs e)
        {
            elapsedTicksSum = elapsedTicksSum - 6000;
            elapsedTicksClockSum = elapsedTicksClockSum - 6000;
            hcHomeTotalSum = hcHomeTotalSum - 6000;
            hcAwayTotalSum = hcAwayTotalSum - 6000;
            hcHomeNSCount = hcHomeNSCount - 6000;
            hcAwayNSCount = hcAwayNSCount - 6000;
            var timespan = new TimeSpan();
            timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
            time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
            totalTimeLeft.Content = time;
            if (timerStart == true)
            {
                elapsedTicksCount1000 = elapsedTicksCount1000 + 60000;
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
            }
            if (timerStart == false)
            {
                elapsedTicksClock = (elapsedTicksClockSum * 10);
                if (periodstart == true)
                {
                    elapsedTicksCount1000 = FH0 - elapsedTicksClock;
                }
                if (periodstart == false)
                {
                    elapsedTicksCount1000 = elapsedTicksCountNew - elapsedTicksClock;
                }
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
                startButton.Focus();
            }
            if(elapsedTicksCount1000 > 1200000 && overtime == false || elapsedTicksCount1000 > 300000 && overtime == true)
            {
                timeHigh = true;
            }
            if (timeHigh == true)
            {
                elapsedTicksSum = elapsedTicksSum + 6000;
                elapsedTicksClockSum = elapsedTicksClockSum + 6000;
                elapsedTicksCount1000 = elapsedTicksCount1000 - 60000;
                hcHomeTotalSum = hcHomeTotalSum + 6000;
                hcAwayTotalSum = hcAwayTotalSum + 6000;
                hcHomeNSCount = hcHomeNSCount + 6000;
                hcAwayNSCount = hcAwayNSCount + 6000;
                if (overtime == false)
                {
                    messageLabel.Content = "You can't have more than 20 minutes";
                }
                if (overtime == true)
                {
                    messageLabel.Content = "You can't have more than 5 minutes in overtime";
                }
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
                animation(ref Animation);
                timeHigh = false;
            }                
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            Homedispatchertimer_Tick(this, new EventArgs());
            Awaydispatchertimer_Tick(this, new EventArgs());
            GraphUpdater(this, new EventArgs());
        }
        private void plus1sec_Click(object sender, RoutedEventArgs e)
        {
            elapsedTicksSum = elapsedTicksSum - 100;
            elapsedTicksClockSum = elapsedTicksClockSum - 100;
            hcHomeTotalSum = hcHomeTotalSum - 100;
            hcAwayTotalSum = hcAwayTotalSum - 100;
            hcHomeNSCount = hcHomeNSCount - 100;
            hcAwayNSCount = hcAwayNSCount - 100;
            Homedispatchertimer_Tick(this, new EventArgs());
            Awaydispatchertimer_Tick(this, new EventArgs());
            var timespan = new TimeSpan();
            if (timerStart == true)
            {
                elapsedTicksCount1000 = elapsedTicksCount1000 + 1000;
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
            }
            if (timerStart == false)
            {
                elapsedTicksClock = (elapsedTicksClockSum * 10);
                if (periodstart == true)
                {
                    elapsedTicksCount1000 = FH0 - elapsedTicksClock;
                }
                if (periodstart == false)
                {
                    elapsedTicksCount1000 = elapsedTicksCountNew - elapsedTicksClock;
                }
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
                startButton.Focus();
            }
            if (elapsedTicksCount1000 > 1200000 && overtime == false || elapsedTicksCount1000 > 300000 && overtime == true)
            {
                timeHigh = true;
            }           
            if (timeHigh == true)
            {
                elapsedTicksSum = elapsedTicksSum + 100;
                elapsedTicksClockSum = elapsedTicksClockSum + 100;
                elapsedTicksCount1000 = elapsedTicksCount1000 - 1000;
                hcHomeTotalSum = hcHomeTotalSum + 100;
                hcAwayTotalSum = hcAwayTotalSum + 100;
                hcHomeNSCount = hcHomeNSCount + 100;
                hcAwayNSCount = hcAwayNSCount + 100;
                Homedispatchertimer_Tick(this, new EventArgs());
                Awaydispatchertimer_Tick(this, new EventArgs());
                if (overtime == false)
                {
                    messageLabel.Content = "You can't have more than 20 minutes";
                }
                if (overtime == true)
                {
                    messageLabel.Content = "You can't have more than 5 minutes in overtime";
                }
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
                animation(ref Animation);
                timeHigh = false;
            }
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            GraphUpdater(this, new EventArgs());
        }
        private void plus1mil_Click(object sender, RoutedEventArgs e)
        {
            elapsedTicksSum = elapsedTicksSum - 10;
            elapsedTicksClockSum = elapsedTicksClockSum - 10;
            hcHomeTotalSum = hcHomeTotalSum - 10;
            hcAwayTotalSum = hcAwayTotalSum - 10;
            hcHomeNSCount = hcHomeNSCount - 10;
            hcAwayNSCount = hcAwayNSCount - 10;
            Homedispatchertimer_Tick(this, new EventArgs());
            Awaydispatchertimer_Tick(this, new EventArgs());
            var timespan = new TimeSpan();
            if(timerStart == true)
            {
                elapsedTicksCount1000 = elapsedTicksCount1000 + 100;
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
            }           
            if (timerStart == false)
            {
                elapsedTicksClock = (elapsedTicksClockSum * 10);
                if (periodstart == true)
                {
                    elapsedTicksCount1000 = FH0 - elapsedTicksClock;
                }
                if (periodstart == false)
                {
                    elapsedTicksCount1000 = elapsedTicksCountNew - elapsedTicksClock;
                }
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
                startButton.Focus();
            }
            if (elapsedTicksCount1000 > 1200000 && overtime == false || elapsedTicksCount1000 > 300000 && overtime == true)
            {
                timeHigh = true;
            }
            if (timeHigh == true)
            {
                elapsedTicksSum = elapsedTicksSum + 10;
                elapsedTicksClockSum = elapsedTicksClockSum + 10;
                elapsedTicksCount1000 = elapsedTicksCount1000 - 100;
                hcHomeTotalSum = hcHomeTotalSum + 10;
                hcAwayTotalSum = hcAwayTotalSum + 10;
                hcHomeNSCount = hcHomeNSCount + 10;
                hcAwayNSCount = hcAwayNSCount + 10;
                Homedispatchertimer_Tick(this, new EventArgs());
                Awaydispatchertimer_Tick(this, new EventArgs());
                if(overtime == false)
                {
                    messageLabel.Content = "You can't have more than 20 minutes";
                }
                if(overtime == true)
                {
                    messageLabel.Content = "You can't have more than 5 minutes in overtime";
                }
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
                animation(ref Animation);
                timeHigh = false;
            }
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            GraphUpdater(this, new EventArgs());
        }
        private void minus1min_Click(object sender, RoutedEventArgs e)
        {
            elapsedTicksSum = elapsedTicksSum + 6000;
            elapsedTicksClockSum = elapsedTicksClockSum + 6000;
            hcHomeTotalSum = hcHomeTotalSum + 6000;
            hcAwayTotalSum = hcAwayTotalSum + 6000;
            hcHomeNSCount = hcHomeNSCount + 6000;
            hcAwayNSCount = hcAwayNSCount + 6000;
            Homedispatchertimer_Tick(this, new EventArgs());
            Awaydispatchertimer_Tick(this, new EventArgs());
            var timespan = new TimeSpan();
            if (timerStart == true)
            {
                elapsedTicksCount1000 = elapsedTicksCount1000 - 60000;
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
            }
            if (timerStart == false)
            {
                elapsedTicksClock = (elapsedTicksClockSum * 10);
                if (periodstart == true)
                {
                    elapsedTicksCount1000 = FH0 - elapsedTicksClock;
                }
                if (periodstart == false)
                {
                    elapsedTicksCount1000 = elapsedTicksCountNew - elapsedTicksClock;
                }
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
                startButton.Focus();
            }
            if (elapsedTicksCount1000 < 0)
            {
                timeLow = true;
            }
            if (timeLow == true)
            {
                elapsedTicksSum = elapsedTicksSum - 6000;
                elapsedTicksClockSum = elapsedTicksClockSum - 6000;
                elapsedTicksCount1000 = elapsedTicksCount1000 + 60000;
                hcHomeTotalSum = hcHomeTotalSum - 6000;
                hcAwayTotalSum = hcAwayTotalSum - 6000;
                hcHomeNSCount = hcHomeNSCount - 6000;
                hcAwayNSCount = hcAwayNSCount - 6000;
                Homedispatchertimer_Tick(this, new EventArgs());
                Awaydispatchertimer_Tick(this, new EventArgs());
                messageLabel.Content = "You can't have negative time";
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
                animation(ref Animation);
                timeLow = false;
            }
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            GraphUpdater(this, new EventArgs());
        }
        private void minus1sec_Click(object sender, RoutedEventArgs e)
        {
            elapsedTicksSum = elapsedTicksSum + 100;
            elapsedTicksClockSum = elapsedTicksClockSum + 100;
            hcHomeTotalSum = hcHomeTotalSum + 100;
            hcAwayTotalSum = hcAwayTotalSum + 100;
            hcHomeNSCount = hcHomeNSCount + 100;
            hcAwayNSCount = hcAwayNSCount + 100;
            Homedispatchertimer_Tick(this, new EventArgs());
            Awaydispatchertimer_Tick(this, new EventArgs());
            var timespan = new TimeSpan();
            if (timerStart == true)
            {
                elapsedTicksCount1000 = elapsedTicksCount1000 - 1000;
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
            }
            if (timerStart == false)
            {
                elapsedTicksClock = (elapsedTicksClockSum * 10);
                if (periodstart == true)
                {
                    elapsedTicksCount1000 = FH0 - elapsedTicksClock;
                }
                if (periodstart == false)
                {
                    elapsedTicksCount1000 = elapsedTicksCountNew - elapsedTicksClock;
                }
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
                startButton.Focus();
            }
            if (elapsedTicksCount1000 < 0)
            {
                timeLow = true;
            }
            if (timeLow == true)
            {
                elapsedTicksSum = elapsedTicksSum - 100;
                elapsedTicksClockSum = elapsedTicksClockSum - 100;
                elapsedTicksCount1000 = elapsedTicksCount1000 + 1000;
                hcHomeTotalSum = hcHomeTotalSum - 100;
                hcAwayTotalSum = hcAwayTotalSum - 100;
                hcHomeNSCount = hcHomeNSCount - 100;
                hcAwayNSCount = hcAwayNSCount - 100;
                Homedispatchertimer_Tick(this, new EventArgs());
                Awaydispatchertimer_Tick(this, new EventArgs());
                messageLabel.Content = "You can't have negative time";
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
                animation(ref Animation);
                timeLow = false;
            }
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            GraphUpdater(this, new EventArgs());
        }
        private void minus1mil_Click(object sender, RoutedEventArgs e)
        {
            elapsedTicksSum = elapsedTicksSum + 10;
            elapsedTicksClockSum = elapsedTicksClockSum + 10;
            hcHomeTotalSum = hcHomeTotalSum + 10;
            hcAwayTotalSum = hcAwayTotalSum + 10;
            hcHomeNSCount = hcHomeNSCount + 10;
            hcAwayNSCount = hcAwayNSCount + 10;
            Homedispatchertimer_Tick(this, new EventArgs());
            Awaydispatchertimer_Tick(this, new EventArgs());
            var timespan = new TimeSpan();
            if (timerStart == true)
            {
                elapsedTicksCount1000 = elapsedTicksCount1000 - 100;
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
            }
            if (timerStart == false)
            {
                elapsedTicksClock = (elapsedTicksClockSum * 10);
                if (periodstart == true)
                {
                    elapsedTicksCount1000 = FH0 - elapsedTicksClock;
                }
                if (periodstart == false)
                {
                    elapsedTicksCount1000 = elapsedTicksCountNew - elapsedTicksClock;
                }
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
                startButton.Focus();
            }
            if (elapsedTicksCount1000 < 0)
            {
                timeLow = true;
            }
            if (timeLow == true)
            {
                elapsedTicksSum = elapsedTicksSum - 10;
                elapsedTicksClockSum = elapsedTicksClockSum - 10;
                elapsedTicksCount1000 = elapsedTicksCount1000 + 100;
                hcHomeTotalSum = hcHomeTotalSum - 10;
                hcAwayTotalSum = hcAwayTotalSum - 10;
                hcHomeNSCount = hcHomeNSCount - 10;
                hcAwayNSCount = hcAwayNSCount - 10;
                Homedispatchertimer_Tick(this, new EventArgs());
                Awaydispatchertimer_Tick(this, new EventArgs());
                messageLabel.Content = "You can't have negative time";
                timespan = TimeSpan.FromMilliseconds(elapsedTicksCount1000);
                time = string.Format("{0:D2}:{1:D2}.{2:D0}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 100);
                totalTimeLeft.Content = time;
                animation(ref Animation);
                timeLow = false;
            }
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            GraphUpdater(this, new EventArgs());
        }
        private void startSecHalfButton_Click(object sender, RoutedEventArgs e)
        {
            startSecHalfButton.IsEnabled = false;
            startSecHalfButton.Visibility = Visibility.Hidden;
            startButton.Visibility = Visibility.Visible;
            pauseButton.Visibility = Visibility.Visible;
            secHalf = true;
            periodover = false;
            EOR = false;
            buzzer = false;
            final = false;
            plus1min.IsEnabled = true;
            plus1sec.IsEnabled = true;
            plus1mil.IsEnabled = true;
            minus1min.IsEnabled = true;
            minus1sec.IsEnabled = true;
            minus1mil.IsEnabled = true;
            periodNum.Content = "2";
            totalTime = 0;
            totalTimeint10 = 0;
            totalTimeAdd = 0;
            totalTimeCount = 0;
            elapsedTicks = 0;
            elapsedTicksSum = 0;
            elapsedTicksNew = 0;
            elapsedTicksClock = 0;
            elapsedTicksClockSum = 0;
            elapsedTicksCount10 = 0;
            elapsedTicksCount1000 = 0;
            startButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
            pauseButton.Focus();
            HomeelapsedTicks = 0;
            AwayelapsedTicks = 0;
            hcHomeTime = 0;
            hcAwayTime = 0;
            hcHomeNSCountTicks = 0;
            hcAwayNSCountTicks = 0;
            hcHomeTotalTime = 0;
            hcAwayTotalTime = 0;
            hcHomeNoShoot = 0;
            hcAwayNoShoot = 0;

            if (overtime == true)
            {
                periodNum.Content = overtimeperiod.ToString();
            }
            if (overtime == true && overtimeperiod < 2 )
            {
                DoubleAnimation db = new DoubleAnimation();
                db.From = 0;
                db.To = 1;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                overtimeBack.BeginAnimation(OpacityProperty, db);
                overtimeName.BeginAnimation(OpacityProperty, db);
            }
            homeBonusClick = false;
            awayBonusClick = false;
        }
        private void switchButton_Click(object sender, RoutedEventArgs e)
        {
            switchTeams = !switchTeams;
            Duration duration = new Duration(TimeSpan.FromSeconds(0.30));

            // Create two DoubleAnimations and set their properbuzzers.
            ThicknessAnimation switchAnimation = new ThicknessAnimation();
            ThicknessAnimation switchAnimation2 = new ThicknessAnimation();

            //double posActualLeft = Convert.ToDouble(possessionPic.GetValue(Margin.Left).ToString());
            switchAnimation.From = new Thickness(76, 0, 0, 0);
            switchAnimation.To = new Thickness(0, 0, 76, 0);

            switchAnimation2.From = new Thickness(0, 0, 76, 0);
            switchAnimation2.To = new Thickness(76, 0, 0, 0);

            SineEase easingFunction = new SineEase();
            easingFunction.EasingMode = EasingMode.EaseInOut;
            switchAnimation.EasingFunction = easingFunction;
            switchAnimation2.EasingFunction = easingFunction;

            switchAnimation.Duration = duration;
            switchAnimation2.Duration = duration;

            Storyboard sb = new Storyboard();
            sb.Duration = duration;

            Storyboard sb2 = new Storyboard();
            sb2.Duration = duration;

            sb.Children.Add(switchAnimation);

            sb2.Children.Add(switchAnimation2);

            Storyboard.SetTarget(switchAnimation, possessionPic);
            Storyboard.SetTarget(switchAnimation2, possessionPic);

            // Set the attached properbuzzers of Canvas.Left and Canvas.Top
            // to be the target properbuzzers of the two respective DoubleAnimations.
            Storyboard.SetTargetProperty(switchAnimation, new PropertyPath(MarginProperty));
            Storyboard.SetTargetProperty(switchAnimation2, new PropertyPath(MarginProperty));

            // Begin the animation.
            if(switchTeams == false)
            {
                if (firstposs == true)
                {
                    if (poss == true)
                    {
                        sb.Begin();
                    }
                    if (poss == false)
                    {
                        sb2.Begin();
                    }
                }
                scoreboardGrid1.ColumnDefinitions.Clear();
                scoreboardGrid2.ColumnDefinitions.Clear();
                scoreboardGrid1.ColumnDefinitions.Add(columnLeft1);
                scoreboardGrid1.ColumnDefinitions.Add(columnLeft2);
                scoreboardGrid2.ColumnDefinitions.Add(columnRight1);
                scoreboardGrid2.ColumnDefinitions.Add(columnRight2);

                scoreboardGrid1.Margin = new Thickness(0, 0, 0, 0);
                scoreboardGrid2.Margin = new Thickness(390, 0, 0, 0);

                homeTeamPic.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeTeamPic.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeTeamPic.Margin = new Thickness(0, 18, 9, 0);

                awayTeamPic.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                awayTeamPic.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayTeamPic.Margin = new Thickness(9, 18, 0, 0);

                favoredHome.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                favoredHome.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                favoredHome.Margin = new Thickness(0, 165, 8, 0);

                favoredAway.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                favoredAway.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                favoredAway.Margin = new Thickness(8, 165, 0, 0);

                homeTeamName.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeTeamName.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homeTeamName.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeTeamName.Margin = new Thickness(0, 189, 72, 0);

                awayTeamName.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayTeamName.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayTeamName.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayTeamName.Margin = new Thickness(72, 189, 0, 0);

                homeTeamNameBackOdd.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeTeamNameBackOdd.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homeTeamNameBackOdd.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeTeamNameBackOdd.Margin = new Thickness(0, 189, 81, 0);

                awayTeamNameBackOdd.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayTeamNameBackOdd.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayTeamNameBackOdd.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayTeamNameBackOdd.Margin = new Thickness(81, 189, 0, 0);

                homeTeamNameBackEven.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeTeamNameBackEven.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homeTeamNameBackEven.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeTeamNameBackEven.Margin = new Thickness(0, 189, 72, 0);

                awayTeamNameBackEven.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayTeamNameBackEven.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayTeamNameBackEven.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayTeamNameBackEven.Margin = new Thickness(72, 189, 0, 0);

                homeTeamRecord.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeTeamRecord.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeTeamRecord.Margin = new Thickness(0, 216, 45, 0);

                awayTeamRecord.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                awayTeamRecord.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayTeamRecord.Margin = new Thickness(45, 216, 0, 0);

                homeTeamRecordBack.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeTeamRecordBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeTeamRecordBack.Margin = new Thickness(0, 216, 45, 0);

                awayTeamRecordBack.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                awayTeamRecordBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayTeamRecordBack.Margin = new Thickness(45, 216, 0, 0);

                homeScoreText.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeScoreText.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeScoreText.Margin = new Thickness(0, 242, 37, 0);

                awayScoreText.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                awayScoreText.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayScoreText.Margin = new Thickness(37, 242, 0, 0);

                homeScoreTextback.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeScoreTextback.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeScoreTextback.Margin = new Thickness(0, 241, 37, 0);

                awayScoreTextback.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                awayScoreTextback.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayScoreTextback.Margin = new Thickness(37, 241, 0, 0);

                streakHomeLabel.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                streakHomeLabel.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                streakHomeLabel.Margin = new Thickness(0, 298, 60, 0);

                streakAwayLabel.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                streakAwayLabel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                streakAwayLabel.Margin = new Thickness(60, 298, 0, 0);

                homeStreakBack.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeStreakBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeStreakBack.Margin = new Thickness(0, 298, 62, 0);

                awayStreakBack.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                awayStreakBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayStreakBack.Margin = new Thickness(62, 298, 0, 0);

                homeStreakPic.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeStreakPic.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeStreakPic.Margin = new Thickness(0, 214, 151, 0);

                awayStreakPic.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                awayStreakPic.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayStreakPic.Margin = new Thickness(151, 214, 0, 0);

                hcHomeNS.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                hcHomeNS.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                hcHomeNS.Margin = new Thickness(0, 401, 60, 0);

                hcAwayNS.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                hcAwayNS.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hcAwayNS.Margin = new Thickness(60, 401, 0, 0);

                hotcoldHome.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                hotcoldHome.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                hotcoldHome.Margin = new Thickness(0, 333, 15, 0);

                hotcoldAway.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                hotcoldAway.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hotcoldAway.Margin = new Thickness(15, 333, 0, 0);

                hotcoldHomebackimage.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                hotcoldHomebackimage.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                hotcoldHomebackimage.Margin = new Thickness(0, 333, 15, 0);

                hotcoldAwaybackimage.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                hotcoldAwaybackimage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hotcoldAwaybackimage.Margin = new Thickness(15, 333, 0, 0);

                homePercent.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homePercent.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homePercent.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homePercent.Margin = new Thickness(0, 259, 20, 0);

                awayPercent.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayPercent.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayPercent.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayPercent.Margin = new Thickness(20, 259, 0, 0);

                homeRect.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeRect.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homeRect.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeRect.Margin = new Thickness(0, 259, 19, 0);

                awayRect.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayRect.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayRect.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayRect.Margin = new Thickness(19, 259, 0, 0);

                homePercentBack.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homePercentBack.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homePercentBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homePercentBack.Margin = new Thickness(0, 259, 19, 0);

                awayPercentBack.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayPercentBack.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayPercentBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayPercentBack.Margin = new Thickness(19, 259, 0, 0);

                homeBonusMain.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeBonusMain.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homeBonusMain.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeBonusMain.Margin = new Thickness(0, 297, 26, 0);

                awayBonusMain.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayBonusMain.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayBonusMain.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayBonusMain.Margin = new Thickness(11, 297, 0, 0);

                homeBonusPlus.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                homeBonusPlus.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 1);
                homeBonusPlus.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeBonusPlus.Margin = new Thickness(0, 297, 15, 0);

                awayBonusPlus.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayBonusPlus.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayBonusPlus.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayBonusPlus.Margin = new Thickness(95, 297, 0, 0);

                homeBonus.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeBonus.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homeBonus.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeBonus.Margin = new Thickness(0, 297, 11, 0);

                awayBonus.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayBonus.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayBonus.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayBonus.Margin = new Thickness(11, 297, 0, 0);

                homeColdButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeColdButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeColdButton.Margin = new Thickness(17, 54, 0, 0);

                awayColdButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                awayColdButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayColdButton.Margin = new Thickness(0, 54, 17, 0);

                homeHotButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeHotButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeHotButton.Margin = new Thickness(17, 107, 0, 0);

                awayHotButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                awayHotButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayHotButton.Margin = new Thickness(0, 107, 17, 0);

                homeNeutralButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeNeutralButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeNeutralButton.Margin = new Thickness(17, 161, 0, 0);

                awayNeutralButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                awayNeutralButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayNeutralButton.Margin = new Thickness(0, 161, 17, 0);

                pgHomeButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                pgHomeButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                pgHomeButton.Margin = new Thickness(17, 215, 0, 0);

                pgAwayButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                pgAwayButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                pgAwayButton.Margin = new Thickness(0, 215, 17, 0);

                fgHomeButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                fgHomeButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                fgHomeButton.Margin = new Thickness(17, 269, 0, 0);

                fgAwayButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                fgAwayButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                fgAwayButton.Margin = new Thickness(0, 269, 17, 0);

                ftHomeButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                ftHomeButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                ftHomeButton.Margin = new Thickness(17, 323, 0, 0);

                ftAwayButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                ftAwayButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                ftAwayButton.Margin = new Thickness(0, 323, 17, 0);

                minus1Home.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                minus1Home.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                minus1Home.Margin = new Thickness(17, 377, 0, 0);

                minus1Away.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                minus1Away.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                minus1Away.Margin = new Thickness(0, 377, 17, 0);

                buzzerChoice1.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                buzzerChoice1.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                buzzerChoice1.Margin = new Thickness(10, 40, 10, 0);

                buzzerChoice3.SetValue(System.Windows.Controls.Grid.ColumnProperty, 3);
                buzzerChoice3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                buzzerChoice3.Margin = new Thickness(10, 40, 10, 0);
            }
            if(switchTeams == true)
            {
                if (firstposs == true)
                {
                    if (poss == true)
                    {
                        sb2.Begin();
                    }
                    if (poss == false)
                    {
                        sb.Begin();
                    }
                }
                scoreboardGrid1.ColumnDefinitions.Clear();
                scoreboardGrid2.ColumnDefinitions.Clear();
                scoreboardGrid2.ColumnDefinitions.Add(columnLeft1);
                scoreboardGrid2.ColumnDefinitions.Add(columnLeft2);
                scoreboardGrid1.ColumnDefinitions.Add(columnRight1);
                scoreboardGrid1.ColumnDefinitions.Add(columnRight2);

                scoreboardGrid1.Margin = new Thickness(390, 0, 0, 0);
                scoreboardGrid2.Margin = new Thickness(0, 0, 0, 0);
                
                homeTeamPic.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                homeTeamPic.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeTeamPic.Margin = new Thickness(9, 18, 0, 0);

                awayTeamPic.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayTeamPic.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayTeamPic.Margin = new Thickness(0, 18, 9, 0);

                favoredHome.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                favoredHome.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                favoredHome.Margin = new Thickness(8, 165, 0, 0);

                favoredAway.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                favoredAway.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                favoredAway.Margin = new Thickness(0, 165, 8, 0);

                homeTeamName.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeTeamName.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homeTeamName.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeTeamName.Margin = new Thickness(72, 189, 0, 0);

                awayTeamName.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayTeamName.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayTeamName.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayTeamName.Margin = new Thickness(0, 189, 72, 0);

                homeTeamNameBackOdd.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeTeamNameBackOdd.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homeTeamNameBackOdd.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeTeamNameBackOdd.Margin = new Thickness(81, 189, 0, 0);

                awayTeamNameBackOdd.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayTeamNameBackOdd.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayTeamNameBackOdd.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayTeamNameBackOdd.Margin = new Thickness(0, 189, 81, 0);

                homeTeamNameBackEven.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeTeamNameBackEven.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homeTeamNameBackEven.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeTeamNameBackEven.Margin = new Thickness(72, 189, 0, 0);

                awayTeamNameBackEven.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayTeamNameBackEven.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayTeamNameBackEven.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayTeamNameBackEven.Margin = new Thickness(0, 189, 72, 0);

                homeTeamRecord.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                homeTeamRecord.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeTeamRecord.Margin = new Thickness(45, 216, 0, 0);

                awayTeamRecord.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayTeamRecord.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayTeamRecord.Margin = new Thickness(0, 216, 45, 0);

                homeTeamRecordBack.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                homeTeamRecordBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeTeamRecordBack.Margin = new Thickness(45, 216, 0, 0);

                awayTeamRecordBack.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayTeamRecordBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayTeamRecordBack.Margin = new Thickness(0, 216, 45, 0);

                homeScoreText.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                homeScoreText.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeScoreText.Margin = new Thickness(37, 242, 0, 0);

                awayScoreText.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayScoreText.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayScoreText.Margin = new Thickness(0, 242, 37, 0);

                homeScoreTextback.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                homeScoreTextback.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeScoreTextback.Margin = new Thickness(37, 241, 0, 0);

                awayScoreTextback.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayScoreTextback.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayScoreTextback.Margin = new Thickness(0, 241, 37, 0);

                streakHomeLabel.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                streakHomeLabel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                streakHomeLabel.Margin = new Thickness(60, 298, 0, 0);

                streakAwayLabel.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                streakAwayLabel.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                streakAwayLabel.Margin = new Thickness(0, 298, 60, 0);

                homeStreakBack.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                homeStreakBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeStreakBack.Margin = new Thickness(62, 298, 0, 0);

                awayStreakBack.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayStreakBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayStreakBack.Margin = new Thickness(0, 298, 62, 0);

                homeStreakPic.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                homeStreakPic.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeStreakPic.Margin = new Thickness(151, 214, 0, 0);

                awayStreakPic.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayStreakPic.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayStreakPic.Margin = new Thickness(0, 214, 151, 0);

                hcHomeNS.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                hcHomeNS.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hcHomeNS.Margin = new Thickness(60, 401, 0, 0);

                hcAwayNS.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                hcAwayNS.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                hcAwayNS.Margin = new Thickness(0, 401, 60, 0);

                hotcoldHome.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                hotcoldHome.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hotcoldHome.Margin = new Thickness(15, 333, 0, 0);

                hotcoldAway.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                hotcoldAway.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                hotcoldAway.Margin = new Thickness(0, 333, 15, 0);

                hotcoldHomebackimage.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                hotcoldHomebackimage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hotcoldHomebackimage.Margin = new Thickness(15, 333, 0, 0);

                hotcoldAwaybackimage.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                hotcoldAwaybackimage.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                hotcoldAwaybackimage.Margin = new Thickness(0, 333, 15, 0);

                homePercent.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homePercent.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homePercent.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homePercent.Margin = new Thickness(20, 259, 0, 0);

                awayPercent.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayPercent.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayPercent.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayPercent.Margin = new Thickness(0, 259, 20, 0);

                homeRect.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeRect.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homeRect.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeRect.Margin = new Thickness(19, 259, 0, 0);

                awayRect.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayRect.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayRect.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayRect.Margin = new Thickness(0, 259, 19, 0);

                homePercentBack.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homePercentBack.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homePercentBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homePercentBack.Margin = new Thickness(19, 259, 0, 0);

                awayPercentBack.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayPercentBack.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayPercentBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayPercentBack.Margin = new Thickness(0, 259, 19, 0);

                homeBonusMain.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeBonusMain.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homeBonusMain.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeBonusMain.Margin = new Thickness(09, 297, 0, 0);

                awayBonusMain.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayBonusMain.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayBonusMain.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayBonusMain.Margin = new Thickness(0, 297, 24, 0);

                homeBonusPlus.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeBonusPlus.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homeBonusPlus.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeBonusPlus.Margin = new Thickness(95, 297, 0, 0);

                awayBonusPlus.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                awayBonusPlus.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 1);
                awayBonusPlus.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayBonusPlus.Margin = new Thickness(0, 297, 15, 0);

                homeBonus.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                homeBonus.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                homeBonus.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                homeBonus.Margin = new Thickness(11, 297, 0, 0);

                awayBonus.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayBonus.SetValue(System.Windows.Controls.Grid.ColumnSpanProperty, 2);
                awayBonus.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                awayBonus.Margin = new Thickness(0, 297, 11, 0);

                homeColdButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                homeColdButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeColdButton.Margin = new Thickness(0, 54, 17, 0);

                awayColdButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayColdButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayColdButton.Margin = new Thickness(17, 54, 0, 0);
                
                homeHotButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                homeHotButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeHotButton.Margin = new Thickness(0, 107, 17, 0);

                awayHotButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayHotButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayHotButton.Margin = new Thickness(17, 107, 0, 0);

                homeNeutralButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                homeNeutralButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                homeNeutralButton.Margin = new Thickness(0, 161, 17, 0);

                awayNeutralButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                awayNeutralButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                awayNeutralButton.Margin = new Thickness(17, 161, 0, 0);

                pgHomeButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                pgHomeButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                pgHomeButton.Margin = new Thickness(0, 215, 17, 0);

                pgAwayButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                pgAwayButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                pgAwayButton.Margin = new Thickness(17, 215, 0, 0);

                fgHomeButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                fgHomeButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                fgHomeButton.Margin = new Thickness(0, 269, 17, 0);

                fgAwayButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                fgAwayButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                fgAwayButton.Margin = new Thickness(17, 269, 0, 0);

                ftHomeButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                ftHomeButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                ftHomeButton.Margin = new Thickness(0, 323, 17, 0);

                ftAwayButton.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                ftAwayButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                ftAwayButton.Margin = new Thickness(17, 323, 0, 0);

                minus1Home.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                minus1Home.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                minus1Home.Margin = new Thickness(0, 377, 17, 0);

                minus1Away.SetValue(System.Windows.Controls.Grid.ColumnProperty, 0);
                minus1Away.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                minus1Away.Margin = new Thickness(17, 377, 0, 0);

                buzzerChoice1.SetValue(System.Windows.Controls.Grid.ColumnProperty, 3);
                buzzerChoice1.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                buzzerChoice1.Margin = new Thickness(10, 40, 10, 0);

                buzzerChoice3.SetValue(System.Windows.Controls.Grid.ColumnProperty, 1);
                buzzerChoice3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                buzzerChoice3.Margin = new Thickness(10, 40, 10, 0);
            }
            if (timerStart == false)
            {
                startButton.Focus();
            }
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
        }
        #endregion       
        #region animations
        //ANIMATION
        private void animation(ref bool Animation)
        {
            var windowlarge = new Storyboard();
            var windowsmall = new Storyboard();
            if(animdone == true)
            {
                if(buzzer == false && EOR == false)
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
                    incWindHeight.To = 600;

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
                        decHeight.To = 553.5;

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
            if(buzzer == true)
            {
                if(animdone == false)
                {
                    var delay = new DispatcherTimer { Interval = TimeSpan.FromSeconds(6) };
                    delay.Start();
                    delay.Tick += (parent, args) =>
                    {
                        delay.Stop();
                        Window mainWindow = System.Windows.Application.Current.MainWindow;
                        if (null == System.Windows.Application.Current.MainWindow)
                        {
                            new System.Windows.Application();
                        }
                        //var windowlarge = new Storyboard();
                        var incWindHeight = new DoubleAnimation();

                        windowlarge.Stop();

                        incWindHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));
                        incWindHeight.From = mainWindow.ActualHeight;
                        incWindHeight.To = 629;

                        Storyboard.SetTargetProperty(incWindHeight, new PropertyPath(MainWindow.HeightProperty));

                        SineEase easingFunction = new SineEase();
                        easingFunction.EasingMode = EasingMode.EaseInOut;
                        incWindHeight.EasingFunction = easingFunction;

                        windowlarge.Children.Add(incWindHeight);
                        
                        windowlarge.Begin(this);

                        if (homescore < awayscore)
                        {
                            messageLabel.Content = "Was there a buzzer beater by the " + homeTeamLower + " ?";
                        }
                        if (awayscore < homescore)
                        {
                            messageLabel.Content = "Was there a buzzer beater by the " + awayTeamLower + " ?";
                        }
                        if (awayscore == homescore)
                        {
                            messageLabel.Content = "Was there a buzzer beater by the " + homeTeamLower + " or the " + awayTeamLower + " ?";
                        }
                        buzzerChoice1.Content = homeTeam;
                        buzzerChoice2.Content = "Neither";
                        buzzerChoice3.Content = awayTeam;

                        messageGrid.Opacity = 0;
                        RadialGradientBrush messageBrush = new RadialGradientBrush();
                        messageBrush.GradientStops.Add(new GradientStop(Colors.White, 0.5));
                        messageBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
                        messageLabel.Background = messageBrush;

                        DoubleAnimation dc = new DoubleAnimation();
                        dc.From = 0;
                        dc.To = 1;
                        dc.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                        dc.BeginTime = TimeSpan.FromSeconds(.8);
                        dc.RepeatBehavior = new RepeatBehavior(1.0);
                        messageGrid.BeginAnimation(OpacityProperty, dc);
                        messageLabel.BeginAnimation(OpacityProperty, dc);
                        buzzerChoice1.BeginAnimation(OpacityProperty, dc);
                        buzzerChoice2.BeginAnimation(OpacityProperty, dc);
                        buzzerChoice3.BeginAnimation(OpacityProperty, dc);
                        animdone = true;
                    };
                }
                if(animdone == true)
                {
                    Window mainWindow = System.Windows.Application.Current.MainWindow;
                    if (null == System.Windows.Application.Current.MainWindow)
                    {
                        new System.Windows.Application();
                    } 
                    //var windowlarge = new Storyboard();
                    var incWindHeight = new DoubleAnimation();

                    windowlarge.Stop();

                    incWindHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));
                    incWindHeight.From = mainWindow.ActualHeight;
                    incWindHeight.To = 629;

                    Storyboard.SetTargetProperty(incWindHeight, new PropertyPath(MainWindow.HeightProperty));

                    SineEase easingFunction = new SineEase();
                    easingFunction.EasingMode = EasingMode.EaseInOut;
                    incWindHeight.EasingFunction = easingFunction;

                    windowlarge.Children.Add(incWindHeight);

                    windowlarge.Begin(this);

                    if (homescore < awayscore)
                    {
                        messageLabel.Content = "Was there a buzzer beater by the " + homeTeamLower + " ?";
                    }
                    if (awayscore < homescore)
                    {
                        messageLabel.Content = "Was there a buzzer beater by the " + awayTeamLower + " ?";
                    }
                    if (awayscore == homescore)
                    {
                        messageLabel.Content = "Was there a buzzer beater by the " + homeTeamLower + " or the " + awayTeamLower + " ?";
                    }
                    buzzerChoice1.Content = homeTeam;
                    buzzerChoice2.Content = "Neither";
                    buzzerChoice3.Content = awayTeam;

                    messageGrid.Opacity = 0;
                    RadialGradientBrush messageBrush = new RadialGradientBrush();
                    messageBrush.GradientStops.Add(new GradientStop(Colors.White, 0.5));
                    messageBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
                    messageLabel.Background = messageBrush;

                    DoubleAnimation dc = new DoubleAnimation();
                    dc.From = 0;
                    dc.To = 1;
                    dc.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                    dc.BeginTime = TimeSpan.FromSeconds(.8);
                    dc.RepeatBehavior = new RepeatBehavior(1.0);
                    messageGrid.BeginAnimation(OpacityProperty, dc);
                    messageLabel.BeginAnimation(OpacityProperty, dc);
                    buzzerChoice1.BeginAnimation(OpacityProperty, dc);
                    buzzerChoice2.BeginAnimation(OpacityProperty, dc);
                    buzzerChoice3.BeginAnimation(OpacityProperty, dc);
                }              
            }
            if (EOR == true)
            {
                if (animdone == false)
                {
                    var delay = new DispatcherTimer { Interval = TimeSpan.FromSeconds(6) };
                    delay.Start();
                    delay.Tick += (parent, args) =>
                    {
                        delay.Stop();
                        Window mainWindow = System.Windows.Application.Current.MainWindow;
                        if (null == System.Windows.Application.Current.MainWindow)
                        {
                            new System.Windows.Application();
                        }
                        //var windowlarge = new Storyboard();
                        var incWindHeight = new DoubleAnimation();

                        windowlarge.Stop();

                        incWindHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));
                        incWindHeight.From = mainWindow.ActualHeight;
                        incWindHeight.To = 629;

                        Storyboard.SetTargetProperty(incWindHeight, new PropertyPath(MainWindow.HeightProperty));

                        SineEase easingFunction = new SineEase();
                        easingFunction.EasingMode = EasingMode.EaseInOut;
                        incWindHeight.EasingFunction = easingFunction;

                        windowlarge.Children.Add(incWindHeight);

                        windowlarge.Begin(this);

                        messageLabel.Content = "Is this the final score? If so, hit ''Final''. You can still add or subtract points.";
                        buzzerChoice2.Content = "Final";
                        buzzerChoice2.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");

                        messageGrid.Opacity = 0;
                        RadialGradientBrush messageBrush = new RadialGradientBrush();
                        messageBrush.GradientStops.Add(new GradientStop(Colors.White, 0.5));
                        messageBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
                        messageLabel.Background = messageBrush;

                        DoubleAnimation db = new DoubleAnimation();
                        db.From = 0;
                        db.To = 1;
                        db.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                        db.BeginTime = TimeSpan.FromSeconds(.8);
                        db.RepeatBehavior = new RepeatBehavior(1.0);
                        messageGrid.BeginAnimation(OpacityProperty, db);
                        messageLabel.BeginAnimation(OpacityProperty, db);
                        buzzerChoice2.BeginAnimation(OpacityProperty, db);
                        final = true;
                        animdone = true;
                    };
                }
                if (animdone == true)
                {
                    Window mainWindow = System.Windows.Application.Current.MainWindow;
                    if (null == System.Windows.Application.Current.MainWindow)
                    {
                        new System.Windows.Application();
                    }
                    //var windowlarge = new Storyboard();
                    var incWindHeight = new DoubleAnimation();

                    windowlarge.Stop();

                    incWindHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));
                    incWindHeight.From = mainWindow.ActualHeight;
                    incWindHeight.To = 629;

                    Storyboard.SetTargetProperty(incWindHeight, new PropertyPath(MainWindow.HeightProperty));

                    SineEase easingFunction = new SineEase();
                    easingFunction.EasingMode = EasingMode.EaseInOut;
                    incWindHeight.EasingFunction = easingFunction;

                    windowlarge.Children.Add(incWindHeight);

                    windowlarge.Begin(this);

                    messageLabel.Content = "Is this the final score? If so, hit ''Final''. You can still add or subtract points.";
                    buzzerChoice2.Content = "Final";
                    buzzerChoice2.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");

                    messageGrid.Opacity = 0;
                    RadialGradientBrush messageBrush = new RadialGradientBrush();
                    messageBrush.GradientStops.Add(new GradientStop(Colors.White, 0.5));
                    messageBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
                    messageLabel.Background = messageBrush;

                    DoubleAnimation db = new DoubleAnimation();
                    db.From = 0;
                    db.To = 1;
                    db.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                    db.BeginTime = TimeSpan.FromSeconds(.8);
                    db.RepeatBehavior = new RepeatBehavior(1.0);
                    messageGrid.BeginAnimation(OpacityProperty, db);
                    messageLabel.BeginAnimation(OpacityProperty, db);
                    buzzerChoice2.BeginAnimation(OpacityProperty, db);
                    final = true;
                }
            }
            periodover = true;
        }
        private void ShowGraphAnimation(object sender, RoutedEventArgs e)
        {
            var windowlarge = new Storyboard();
            var windowsmall = new Storyboard();
            if (graphOpen == true)
            {
                Window mainWindow = System.Windows.Application.Current.MainWindow;
                if (null == System.Windows.Application.Current.MainWindow)
                {
                    new System.Windows.Application();
                }
                //var windowlarge = new Storyboard();
                var incWindWidth = new DoubleAnimation();

                incWindWidth.Duration = new Duration(TimeSpan.FromMilliseconds(800));
                incWindWidth.From = mainWindow.ActualWidth;
                incWindWidth.To = 1340;
                Storyboard.SetTargetProperty(incWindWidth, new PropertyPath(MainWindow.WidthProperty));

                SineEase easingFunction = new SineEase();
                easingFunction.EasingMode = EasingMode.EaseInOut;
                incWindWidth.EasingFunction = easingFunction;

                windowlarge.Children.Add(incWindWidth);

                windowlarge.Begin(this);
            }
        }
        private void Animation_Completed(object sender, EventArgs e)
        {
            animdone = true;
        }
        private void buzzerChoice1_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            da.RepeatBehavior = new RepeatBehavior(1.0);
            messageLabel.BeginAnimation(OpacityProperty, da);
            buzzerChoice1.BeginAnimation(OpacityProperty, da);
            buzzerChoice2.BeginAnimation(OpacityProperty, da);
            buzzerChoice3.BeginAnimation(OpacityProperty, da);

            var delay = new DispatcherTimer { Interval = TimeSpan.FromSeconds(.5) };
            delay.Start();
            delay.Tick += (parent, args) =>
            {
                delay.Stop();
                messageLabel.Content = "Is this the final score? If so, hit ''Final''. You can still add or subtract points.";
                buzzerChoice2.Content = "Final";
                buzzerChoice1.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
                buzzerChoice2.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
                buzzerChoice3.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
                DoubleAnimation db = new DoubleAnimation();
                db.From = 0;
                db.To = 1;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                messageLabel.BeginAnimation(OpacityProperty, db);
                buzzerChoice2.BeginAnimation(OpacityProperty, db);
                final = true;
                buzzHome = true;
            };
            if (gameover == true)
            {
                var Starting_Window = new Starting_Window();
                Starting_Window.Show();
                this.Close();
            }
        }
        private void buzzerChoice2_Click(object sender, RoutedEventArgs e)
        {
            if (final == true)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 1;
                da.To = 0;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                messageLabel.BeginAnimation(OpacityProperty, da);
                buzzerChoice2.BeginAnimation(OpacityProperty, da);

                var delay = new DispatcherTimer { Interval = TimeSpan.FromSeconds(.5) };
                delay.Start();
                delay.Tick += (parent, args) =>
                {
                    delay.Stop();
                    Final(ref final);
                };
            }
            if (final == false)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 1;
                da.To = 0;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                messageLabel.BeginAnimation(OpacityProperty, da);
                buzzerChoice1.BeginAnimation(OpacityProperty, da);
                buzzerChoice2.BeginAnimation(OpacityProperty, da);
                buzzerChoice3.BeginAnimation(OpacityProperty, da);

                var delay = new DispatcherTimer { Interval = TimeSpan.FromSeconds(.5) };
                delay.Start();
                delay.Tick += (parent, args) =>
                {
                    delay.Stop();
                    messageLabel.Content = "Is this the final score? If so, hit ''Final''. You can still add or subtract points.";
                    buzzerChoice2.Content = "Final";
                    buzzerChoice1.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
                    buzzerChoice2.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
                    buzzerChoice3.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
                    DoubleAnimation db = new DoubleAnimation();
                    db.From = 0;
                    db.To = 1;
                    db.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                    db.RepeatBehavior = new RepeatBehavior(1.0);
                    messageLabel.BeginAnimation(OpacityProperty, db);
                    buzzerChoice2.BeginAnimation(OpacityProperty, db);
                    final = true;
                };
            }
            if (gameover == true)
            {
                Window mainWindow = System.Windows.Application.Current.MainWindow;
                InitializeComponent();
            }
        }
        private void buzzerChoice3_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            da.RepeatBehavior = new RepeatBehavior(1.0);
            messageLabel.BeginAnimation(OpacityProperty, da);
            buzzerChoice1.BeginAnimation(OpacityProperty, da);
            buzzerChoice2.BeginAnimation(OpacityProperty, da);
            buzzerChoice3.BeginAnimation(OpacityProperty, da);

            var delay = new DispatcherTimer { Interval = TimeSpan.FromSeconds(.5) };
            delay.Start();
            delay.Tick += (parent, args) =>
            {
                delay.Stop();
                messageLabel.Content = "Is this the final score? If so, hit ''Final''. You can still add or subtract points.";
                buzzerChoice2.Content = "Final";
                buzzerChoice2.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
                DoubleAnimation db = new DoubleAnimation();
                db.From = 0;
                db.To = 1;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                messageLabel.BeginAnimation(OpacityProperty, db);
                buzzerChoice2.BeginAnimation(OpacityProperty, db);
                buzzAway = true;
                final = true;
            };
        }
        private void Final(ref bool final)
        {      
            if(scoreDif != 0)
            {
                gameover = true;
                Random buzzerRandom = new Random();
                
                buzzerChoice1.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
                buzzerChoice2.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
                buzzerChoice3.SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, "ButtonNormalBackground");
                string homefinalNum = string.Format("{000}", homeScoreText.Content);
                string awayfinalNum = string.Format("{000}", awayScoreText.Content);
                string winningteam = null;
                string losingteam = null;
                string winningscore = null;
                string losingscore = null;
                string winningsentence = null;
                string finalscore = null;
                periodNum.Content = "F";
                GraphUpdater(this, new EventArgs());
                ftHomeButton.IsEnabled = false;
                fgHomeButton.IsEnabled = false;
                pgHomeButton.IsEnabled = false;
                ftAwayButton.IsEnabled = false;
                fgAwayButton.IsEnabled = false;
                pgAwayButton.IsEnabled = false;
                minus1Home.IsEnabled = false;
                minus1Away.IsEnabled = false;
                if (homescore > awayscore)
                {
                    winningteam = homeTeamLower;
                    losingteam = awayTeamLower;
                    winningscore = homefinalNum;
                    losingscore = awayfinalNum;
                }
                if (homescore < awayscore)
                {
                    losingteam = homeTeamLower;
                    winningteam = awayTeamLower;
                    losingscore = homefinalNum;
                    winningscore = awayfinalNum;
                }
                if(buzzer == true)
                {
                    int buzzerRandomNum = buzzerRandom.Next(0, 5);
                    if (buzzerRandomNum == 0)
                    {
                        winningsentence = "The " + winningteam + " win it at the buzzer! ";
                    }
                    if (buzzerRandomNum == 1)
                    {
                        winningsentence = "BUZZER BEATER! The " + winningteam + " win! ";
                    }
                    if (buzzerRandomNum == 2)
                    {
                        winningsentence = "The " + winningteam + " pull it off at the buzzer! ";
                    }
                    if (buzzerRandomNum == 3)
                    {
                        winningsentence = "UNBELIEVABLE! The " + winningteam + " sink it at the buzzer! ";
                    }
                    if (buzzerRandomNum == 4)
                    {
                        winningsentence = "The " + winningteam + " hit it at the buzzer! ";
                    }
                    if (buzzerRandomNum == 5)
                    {
                        winningsentence = "The " + winningteam + " drain it at the buzzer! ";
                    }
                }
                if (buzzer == false && scoreDif <= 5)
                {
                    int buzzerRandomNum = buzzerRandom.Next(0, 4);
                    if (buzzerRandomNum == 0)
                    {
                        winningsentence = "The " + winningteam + " narrowly take this one. ";
                    }
                    if (buzzerRandomNum == 1)
                    {
                        winningsentence = "The " + winningteam + " win the nail biter. ";
                    }
                    if (buzzerRandomNum == 2)
                    {
                        winningsentence = "Close game as the " + winningteam + " prevail in the end. ";
                    }
                    if (buzzerRandomNum == 3)
                    {
                        winningsentence = "The " + winningteam + " keep it close and win. ";
                    }
                    if (buzzerRandomNum == 4)
                    {
                        winningsentence = "The " + winningteam + " pull out in the end. ";
                    }
                }
                if (buzzer == false && scoreDif > 5 && scoreDif <= 10)
                {
                    int buzzerRandomNum = buzzerRandom.Next(0, 3);
                    if (buzzerRandomNum == 0)
                    {
                        winningsentence = "The " + winningteam + " win by a fair margin. ";
                    }
                    if (buzzerRandomNum == 1)
                    {
                        winningsentence = "The " + winningteam + " are victorious. ";
                    }
                    if (buzzerRandomNum == 2)
                    {
                        winningsentence = "The " + winningteam + " take this game. ";
                    }
                    if (buzzerRandomNum == 3)
                    {
                        winningsentence = "The " + losingteam + " tried to keep it close. ";
                    }
                }
                if (buzzer == false && scoreDif > 10 && scoreDif <= 25)
                {
                    int buzzerRandomNum = buzzerRandom.Next(0, 4);
                    if (buzzerRandomNum == 0)
                    {
                        winningsentence = "The " + winningteam + " let loose on the " + losingteam + ". ";
                    }
                    if (buzzerRandomNum == 1)
                    {
                        winningsentence = "The " + winningteam + " sealed the deal with the " + losingteam + ". ";
                    }
                    if (buzzerRandomNum == 2)
                    {
                        winningsentence = "The " + winningteam + " took this one away. ";
                    }
                    if (buzzerRandomNum == 3)
                    {
                        winningsentence = "Not the best day for the " + losingteam + ". ";
                    }
                    if (buzzerRandomNum == 4)
                    {
                        winningsentence = "The " + winningteam + " never looked back. ";
                    }
                }
                if (buzzer == false && scoreDif >= 25)
                {
                    int buzzerRandomNum = buzzerRandom.Next(0, 6);
                    if (buzzerRandomNum == 0)
                    {
                        winningsentence = "The " + winningteam + " embarrassed the " + losingteam + ". ";
                    }
                    if (buzzerRandomNum == 1)
                    {
                        winningsentence = "The " + winningteam + " made that look easy. ";
                    }
                    if (buzzerRandomNum == 2)
                    {
                        winningsentence = "A one sided game. ";
                    }
                    if (buzzerRandomNum == 3)
                    {
                        winningsentence = "The " + winningteam + " completely annihilated the " + losingteam + ". ";
                    }
                    if (buzzerRandomNum == 4)
                    {
                        winningsentence = "Not much to say other than total domination. ";
                    }
                    if (buzzerRandomNum == 5)
                    {
                        winningsentence = losingteam + "... What was that?!? ";
                    }
                    if (buzzerRandomNum == 6)
                    {
                        winningsentence = "Not a good day to be a " + losingteam + " fan. ";
                    }
                }
                if (overtime == true && overtimeperiod == 1)
                {
                    winningsentence = "The " + winningteam + " win it in overtime! ";
                }
                if (overtime == true && overtimeperiod == 2)
                {
                    winningsentence = "The " + winningteam + " win it in double overtime! ";
                }
                if (overtime == true && overtimeperiod == 3)
                {
                    winningsentence = "The " + winningteam + " win it in triple overtime! ";
                }
                if (overtime == true && overtimeperiod == 4)
                {
                    winningsentence = "The " + winningteam + " win it in quadruple overtime! ";
                }
                if (overtime == true && overtimeperiod > 4)
                {
                    winningsentence = "The " + winningteam + " win it in the " + overtimeperiod + "th overtime period! ";
                }
                finalscore = "Final score: " + winningteam + " " + winningscore + ", " + losingteam + " " + losingscore + ".";
                messageLabel.Content = winningsentence + finalscore;
                buzzerChoice1.Content = "Choose Two New Teams";
                buzzerChoice2.Content = "Restart This Game";
                buzzerChoice3.Content = "Create Scenario";

                DoubleAnimation db = new DoubleAnimation();
                db.From = 0;
                db.To = 1;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                messageLabel.BeginAnimation(OpacityProperty, db);
                buzzerChoice1.BeginAnimation(OpacityProperty, db);
                buzzerChoice2.BeginAnimation(OpacityProperty, db);
                buzzerChoice3.BeginAnimation(OpacityProperty, db);
            }
            if (scoreDif == 0)
            {
                overtime = true;
                overtimeperiod ++;
                startSecHalfButton.IsEnabled = true;
                startSecHalfButton.FontSize = 15;
                startSecHalfButton.Content = "Start Overtime Period " + overtimeperiod;
 
                startSecHalfButton.Visibility = Visibility.Visible;
                startButton.Visibility = Visibility.Hidden;
                pauseButton.Visibility = Visibility.Hidden;

                Window mainWindow = System.Windows.Application.Current.MainWindow;

                var windowsmall = new Storyboard();
                var decHeight = new DoubleAnimation();

                decHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));

                decHeight.From = mainWindow.ActualHeight;
                decHeight.To = 553.5;

                Storyboard.SetTargetProperty(decHeight, new PropertyPath(MainWindow.HeightProperty));

                SineEase easingFunction = new SineEase();
                easingFunction.EasingMode = EasingMode.EaseInOut;
                decHeight.EasingFunction = easingFunction;

                windowsmall.Children.Add(decHeight);
                windowsmall.Begin(this);

                buzzerChoice1.Background = buzzerHomebrush;
                buzzerChoice3.Background = buzzerAwaybrush;
            }
        }
        private void endAnimation (ref bool endanimation)
        {
            Window mainWindow = System.Windows.Application.Current.MainWindow;
            if (null == System.Windows.Application.Current.MainWindow)
            {
                new System.Windows.Application();
            }
            messageLabel.Content = "";
            DoubleAnimation db = new DoubleAnimation();
            db.From = 1;
            db.To = 0;
            db.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            db.BeginTime = TimeSpan.FromMilliseconds(1);
            db.RepeatBehavior = new RepeatBehavior(1.0);
            messageGrid.BeginAnimation(OpacityProperty, db);

            var windowsmall = new Storyboard();
            var decHeight = new DoubleAnimation();

            decHeight.Duration = new Duration(TimeSpan.FromMilliseconds(800));

            decHeight.From = mainWindow.ActualHeight;
            decHeight.To = 553.5;

            Storyboard.SetTargetProperty(decHeight, new PropertyPath(MainWindow.HeightProperty));

            SineEase easingFunction = new SineEase();
            easingFunction.EasingMode = EasingMode.EaseInOut;
            decHeight.EasingFunction = easingFunction;

            windowsmall.Children.Add(decHeight);
            windowsmall.Begin(this);              
        }       
        private void Possession(ref bool possession)
        {           
            if (firstposs == true)
            {
                if(switchTeams == false)
                {
                    if (poss == true && lastPoint == false)
                    {
                        Duration duration = new Duration(TimeSpan.FromSeconds(0.30));

                        // Create two DoubleAnimations and set their properbuzzers.
                        ThicknessAnimation myDoubleAnimation1 = new ThicknessAnimation();

                        //double posActualLeft = Convert.ToDouble(possessionPic.GetValue(Margin.Left).ToString());
                        myDoubleAnimation1.From = new Thickness(76, 0, 0, 0);
                        myDoubleAnimation1.To = new Thickness(0, 0, 76, 0);

                        SineEase easingFunction = new SineEase();
                        easingFunction.EasingMode = EasingMode.EaseInOut;
                        myDoubleAnimation1.EasingFunction = easingFunction;

                        myDoubleAnimation1.Duration = duration;
                        Storyboard sb = new Storyboard();
                        sb.Duration = duration;

                        sb.Children.Add(myDoubleAnimation1);

                        Storyboard.SetTarget(myDoubleAnimation1, possessionPic);

                        // Set the attached properbuzzers of Canvas.Left and Canvas.Top
                        // to be the target properbuzzers of the two respective DoubleAnimations.
                        Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(MarginProperty));

                        // Begin the animation.
                        sb.Begin();
                        lastPoint = true;
                    }
                    if (poss == false && lastPoint == true)
                    {
                        Duration duration = new Duration(TimeSpan.FromSeconds(0.30));

                        // Create two DoubleAnimations and set their properbuzzers.
                        ThicknessAnimation myDoubleAnimation1 = new ThicknessAnimation();

                        //double posActualLeft = Convert.ToDouble(possessionPic.GetValue(Margin.Left).ToString());
                        myDoubleAnimation1.From = new Thickness(0, 0, 76, 0);
                        myDoubleAnimation1.To = new Thickness(76, 0, 0, 0);

                        SineEase easingFunction = new SineEase();
                        easingFunction.EasingMode = EasingMode.EaseInOut;
                        myDoubleAnimation1.EasingFunction = easingFunction;

                        myDoubleAnimation1.Duration = duration;
                        Storyboard sb = new Storyboard();
                        sb.Duration = duration;

                        sb.Children.Add(myDoubleAnimation1);

                        Storyboard.SetTarget(myDoubleAnimation1, possessionPic);

                        // Set the attached properbuzzers of Canvas.Left and Canvas.Top
                        // to be the target properbuzzers of the two respective DoubleAnimations.
                        Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(MarginProperty));

                        // Begin the animation.
                        sb.Begin();
                        lastPoint = false;
                    }
                }
                if (switchTeams == true)
                {
                    if (poss == true && lastPoint == false)
                    {
                        Duration duration = new Duration(TimeSpan.FromSeconds(0.30));

                        // Create two DoubleAnimations and set their properbuzzers.
                        ThicknessAnimation myDoubleAnimation1 = new ThicknessAnimation();

                        //double posActualLeft = Convert.ToDouble(possessionPic.GetValue(Margin.Left).ToString());
                        myDoubleAnimation1.From = new Thickness(0, 0, 76, 0);
                        myDoubleAnimation1.To = new Thickness(76, 0, 0, 0);

                        SineEase easingFunction = new SineEase();
                        easingFunction.EasingMode = EasingMode.EaseInOut;
                        myDoubleAnimation1.EasingFunction = easingFunction;

                        myDoubleAnimation1.Duration = duration;
                        Storyboard sb = new Storyboard();
                        sb.Duration = duration;

                        sb.Children.Add(myDoubleAnimation1);

                        Storyboard.SetTarget(myDoubleAnimation1, possessionPic);

                        // Set the attached properbuzzers of Canvas.Left and Canvas.Top
                        // to be the target properbuzzers of the two respective DoubleAnimations.
                        Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(MarginProperty));

                        // Begin the animation.
                        sb.Begin();
                        lastPoint = true;
                    }
                    if (poss == false && lastPoint == true)
                    {
                        Duration duration = new Duration(TimeSpan.FromSeconds(0.30));

                        // Create two DoubleAnimations and set their properbuzzers.
                        ThicknessAnimation myDoubleAnimation1 = new ThicknessAnimation();

                        //double posActualLeft = Convert.ToDouble(possessionPic.GetValue(Margin.Left).ToString());
                        myDoubleAnimation1.From = new Thickness(76, 0, 0, 0);
                        myDoubleAnimation1.To = new Thickness(0, 0, 76, 0);

                        SineEase easingFunction = new SineEase();
                        easingFunction.EasingMode = EasingMode.EaseInOut;
                        myDoubleAnimation1.EasingFunction = easingFunction;

                        myDoubleAnimation1.Duration = duration;
                        Storyboard sb = new Storyboard();
                        sb.Duration = duration;

                        sb.Children.Add(myDoubleAnimation1);

                        Storyboard.SetTarget(myDoubleAnimation1, possessionPic);

                        // Set the attached properbuzzers of Canvas.Left and Canvas.Top
                        // to be the target properbuzzers of the two respective DoubleAnimations.
                        Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(MarginProperty));

                        // Begin the animation.
                        sb.Begin();
                        lastPoint = false;
                    }
                }
            }
            if (firstposs == false)
            {
                if(switchTeams == false)
                {
                    if (poss == true)
                    {
                        Duration duration = new Duration(TimeSpan.FromSeconds(0.30));

                        // Create two DoubleAnimations and set their properbuzzers.
                        ThicknessAnimation myDoubleAnimation1 = new ThicknessAnimation();

                        //double posActualLeft = Convert.ToDouble(possessionPic.GetValue(Margin.Left).ToString());
                        myDoubleAnimation1.From = new Thickness(38, 0, 38, 0);
                        myDoubleAnimation1.To = new Thickness(0, 0, 76, 0);

                        SineEase easingFunction = new SineEase();
                        easingFunction.EasingMode = EasingMode.EaseInOut;
                        myDoubleAnimation1.EasingFunction = easingFunction;

                        myDoubleAnimation1.Duration = duration;
                        Storyboard sb = new Storyboard();
                        sb.Duration = duration;

                        sb.Children.Add(myDoubleAnimation1);

                        Storyboard.SetTarget(myDoubleAnimation1, possessionPic);

                        // Set the attached properbuzzers of Canvas.Left and Canvas.Top
                        // to be the target properbuzzers of the two respective DoubleAnimations.
                        Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(MarginProperty));

                        // Begin the animation.
                        sb.Begin();
                        lastPoint = true;
                    }
                    if (poss == false)
                    {
                        Duration duration = new Duration(TimeSpan.FromSeconds(0.30));

                        // Create two DoubleAnimations and set their properbuzzers.
                        ThicknessAnimation myDoubleAnimation1 = new ThicknessAnimation();

                        //double posActualLeft = Convert.ToDouble(possessionPic.GetValue(Margin.Left).ToString());
                        myDoubleAnimation1.From = new Thickness(38, 0, 38, 0);
                        myDoubleAnimation1.To = new Thickness(76, 0, 0, 0);

                        SineEase easingFunction = new SineEase();
                        easingFunction.EasingMode = EasingMode.EaseInOut;
                        myDoubleAnimation1.EasingFunction = easingFunction;

                        myDoubleAnimation1.Duration = duration;
                        Storyboard sb = new Storyboard();
                        sb.Duration = duration;

                        sb.Children.Add(myDoubleAnimation1);

                        Storyboard.SetTarget(myDoubleAnimation1, possessionPic);

                        // Set the attached properbuzzers of Canvas.Left and Canvas.Top
                        // to be the target properbuzzers of the two respective DoubleAnimations.
                        Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(MarginProperty));

                        // Begin the animation.
                        sb.Begin();
                        lastPoint = false;
                    }
                    firstposs = true;
                }
                if (switchTeams == true)
                {
                    if (poss == true)
                    {
                        Duration duration = new Duration(TimeSpan.FromSeconds(0.30));

                        // Create two DoubleAnimations and set their properbuzzers.
                        ThicknessAnimation myDoubleAnimation1 = new ThicknessAnimation();

                        //double posActualLeft = Convert.ToDouble(possessionPic.GetValue(Margin.Left).ToString());
                        myDoubleAnimation1.From = new Thickness(38, 0, 38, 0);
                        myDoubleAnimation1.To = new Thickness(76, 0, 0, 0);

                        SineEase easingFunction = new SineEase();
                        easingFunction.EasingMode = EasingMode.EaseInOut;
                        myDoubleAnimation1.EasingFunction = easingFunction;

                        myDoubleAnimation1.Duration = duration;
                        Storyboard sb = new Storyboard();
                        sb.Duration = duration;

                        sb.Children.Add(myDoubleAnimation1);

                        Storyboard.SetTarget(myDoubleAnimation1, possessionPic);

                        // Set the attached properbuzzers of Canvas.Left and Canvas.Top
                        // to be the target properbuzzers of the two respective DoubleAnimations.
                        Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(MarginProperty));

                        // Begin the animation.
                        sb.Begin();
                        lastPoint = true;
                    }
                    if (poss == false)
                    {
                        Duration duration = new Duration(TimeSpan.FromSeconds(0.30));

                        // Create two DoubleAnimations and set their properbuzzers.
                        ThicknessAnimation myDoubleAnimation1 = new ThicknessAnimation();

                        //double posActualLeft = Convert.ToDouble(possessionPic.GetValue(Margin.Left).ToString());
                        myDoubleAnimation1.From = new Thickness(38, 0, 38, 0);
                        myDoubleAnimation1.To = new Thickness(0, 0, 76, 0);

                        SineEase easingFunction = new SineEase();
                        easingFunction.EasingMode = EasingMode.EaseInOut;
                        myDoubleAnimation1.EasingFunction = easingFunction;

                        myDoubleAnimation1.Duration = duration;
                        Storyboard sb = new Storyboard();
                        sb.Duration = duration;

                        sb.Children.Add(myDoubleAnimation1);

                        Storyboard.SetTarget(myDoubleAnimation1, possessionPic);

                        // Set the attached properbuzzers of Canvas.Left and Canvas.Top
                        // to be the target properbuzzers of the two respective DoubleAnimations.
                        Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath(MarginProperty));

                        // Begin the animation.
                        sb.Begin();
                        lastPoint = false;
                    }
                    firstposs = true;
                }
            }
            if (timerStart == false)
            {
                WinningPercentValue(ref winpercent);
            }
        }           
        private void WinningPercentAnimation(ref bool wpAnimation)
        {
            if (winningPercent <= 75)
            {
                homecaution = false;
                homewarning = false;
                homegood1 = false;
                homegood = false;
                awaycaution = false;
                awaywarning = false;
                awaygood1 = false;
                awaygood = false;
                inconsistent = false;
                BlinkingAnimation(ref blinking);
            }            
            if (winningPercent > 75 && winningPercent <= 90 && homescore < awayscore)
            {
                homecaution = true;
                homewarning = false;
                homegood1 = false;
                homegood = false;
                awaycaution = false;
                awaywarning = false;
                awaygood1 = true;
                awaygood = false;
                inconsistent = false;
                BlinkingAnimation(ref blinking);
            }
            if (winningPercent > 90 && homescore < awayscore)
            {
                homecaution = false;
                homewarning = true;
                homegood1 = false;
                homegood = false;
                awaycaution = false;
                awaywarning = false;
                awaygood1 = false;
                awaygood = true;
                inconsistent = false;
                BlinkingAnimation(ref blinking);
            }
            if (winningPercent > 75 && winningPercent <= 90 && homescore > awayscore)
            {
                homecaution = false;
                homewarning = false;
                homegood1 = true;
                homegood = false;
                awaycaution = true;
                awaywarning = false;
                awaygood1 = false;
                awaygood = false;
                inconsistent = false;
                BlinkingAnimation(ref blinking);
            }
            if (winningPercent > 90 && homescore > awayscore)
            {
                homecaution = false;
                homewarning = false;
                homegood1 = false;
                homegood = true;
                awaycaution = false;
                awaywarning = true;
                awaygood1 = false;
                awaygood = false;
                inconsistent = false;
                BlinkingAnimation(ref blinking);
            }
            if (elapsedTicksCount10 < 6000 && elapsedTicksCount10 > 3000 && scoreDif <= 5 && timerStart == true)
            {
                homecaution = false;
                homewarning = false;
                homegood1 = false;
                homegood = false;
                awaycaution = false;
                awaywarning = false;
                awaygood1 = false;
                awaygood = false;
                inconsistent = true;
                BlinkingAnimation(ref blinking);
            }
            if (elapsedTicksCount10 < 3000 && scoreDif <= 3 && timerStart == true)
            {
                homecaution = false;
                homewarning = false;
                homegood1 = false;
                homegood = false;
                awaycaution = false;
                awaywarning = false;
                awaygood1 = false;
                awaygood = false;
                inconsistent = true;
                BlinkingAnimation(ref blinking);
            }
            if (periodNum.Content == "2" && regulation == true && scoreDif <= 3 && final == false)
            {
                buzzer = true;
                animation(ref Animation);
            }
            if (periodNum.Content == "2" && regulation == true && scoreDif > 3 && final == false)
            {
                EOR = true;
                buzzer = false;
                animation(ref Animation);
            }
            if (overtime == true && elapsedTicksCount1000 <= 0 && scoreDif <= 3 && periodover == false)
            {
                EOR = false;
                buzzer = true;
                animation(ref Animation);
            }
            if (overtime == true && elapsedTicksCount1000 <= 0 && scoreDif > 3 && periodover == false)
            {
                EOR = true;
                buzzer = false;
                animation(ref Animation);
            }
        }   
        private void BlinkingAnimation (ref bool blinking)
        {
            SolidColorBrush solidbrush = new SolidColorBrush();
            SolidColorBrush good = new SolidColorBrush();

            good.Color = System.Windows.Media.Color.FromArgb(255, 0, 255, 0);
            NameScope.SetNameScope(this, new NameScope());
            this.RegisterName("brush", solidbrush);

            Storyboard Hsolidboard = new Storyboard();
            Storyboard Asolidboard = new Storyboard();

            ColorAnimation homeanimation = new ColorAnimation();
            homeanimation.From = Colors.Transparent;
            homeanimation.Duration = TimeSpan.FromSeconds(.4);
            homeanimation.AutoReverse = true;
            homeanimation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTargetName(homeanimation, "brush");
            Storyboard.SetTargetProperty(homeanimation,
                new PropertyPath(SolidColorBrush.ColorProperty));
            Hsolidboard.Children.Add(homeanimation);

            ColorAnimation awayanimation = new ColorAnimation();
            awayanimation.From = Colors.Transparent;
            awayanimation.Duration = TimeSpan.FromSeconds(.4);
            awayanimation.AutoReverse = true;
            awayanimation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTargetName(awayanimation, "brush");
            Storyboard.SetTargetProperty(awayanimation,
                new PropertyPath(SolidColorBrush.ColorProperty));
            Asolidboard.Children.Add(awayanimation);

            homeRect.Stroke = solidbrush;
            awayRect.Stroke = solidbrush;
           
            if (homecaution == false && homewarning == false && homegood1 == false && homegood == false && inconsistent == false)
            {
                homeanimation.To = Colors.Transparent;
                homeRect.Opacity = 1;
            }
            if (homecaution == true && homewarning == false && homegood1 == false && homegood == false && inconsistent == false)
            {
                homeanimation.To = Colors.Orange;
                homeRect.Opacity = 1;
                Hsolidboard.Begin(this);
            }
            if (homecaution == false && homewarning == true && homegood1 == false && homegood == false && inconsistent == false)
            {
                homeanimation.To = Colors.Red;
                homeRect.Opacity = 1;
                Hsolidboard.Begin(this);
            }
            if (homecaution == false && homewarning == false && homegood1 == true && homegood == false && inconsistent == false)
            {
                homeRect.Stroke = good;
                homeRect.Opacity = .4;
            }
            if (homecaution == false && homewarning == false && homegood1 == false && homegood == true && inconsistent == false)
            {
                homeRect.Stroke = good;
                homeRect.Opacity = .7;
            }
            if (awaycaution == false && awaywarning == false && awaygood1 == false && awaygood == false && inconsistent == false)
            {
                awayanimation.To = Colors.Transparent;
                awayRect.Opacity = 1;
            }
            if (awaycaution == true && awaywarning == false && awaygood1 == false && awaygood == false && inconsistent == false)
            {
                awayanimation.To = Colors.Orange;
                awayRect.Opacity = 1;
                Asolidboard.Begin(this);
            }
            if (awaycaution == false && awaywarning == true && awaygood1 == false && awaygood == false && inconsistent == false)
            {
                awayanimation.To = Colors.Red;
                awayRect.Opacity = 1;
                Asolidboard.Begin(this);
            }
            if (awaycaution == false && awaywarning == false && awaygood1 == true && awaygood == false && inconsistent == false)
            {
                awayRect.Stroke = good;
                awayRect.Opacity = .4;
            }
            if (awaycaution == false && awaywarning == false && awaygood1 == false && awaygood == true && inconsistent == false)
            {
                awayRect.Stroke = good;
                awayRect.Opacity = .7;
            }
            if (inconsistent == true)
            {
                homeanimation.To = Colors.Yellow;
                awayanimation.To = Colors.Yellow;
                homeRect.Opacity = 1;
                awayRect.Opacity = 1;
                Hsolidboard.Begin(this);
                Asolidboard.Begin(this);
            }
        }
        #endregion
        #region scoring
        //SCORING
        private void ftHomeButton_Click(object sender, RoutedEventArgs e)
        {
            homescore = homescore + 1;
            homeScoreText.Content = homescore;
            hcHomeSum = 0 - HomeelapsedTicks;
            hcHomeNSCount = 0 - hcHomeNSCountTicks;
            if(hcHomeTotalTime >= 750)
            {
                hcHomeTotalSum = 0 - HomeelapsedTicks;
            }
            if (hcHomeTotalTime < 750)
            {
                hcHomeTotalSum -= 750;
            }
            homeStreak += 1;
            awayStreak = 0;
            Streaks(ref streaks);
            poss = false;
            Possession(ref possession);
            WinningTeam(ref winteam);
            WinningPercentAnimation(ref wpAnimation);
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            if (timerStart == false)
            {
                startButton.Focus();
            }
            Homedispatchertimer_Tick(this, new EventArgs());
            Awaydispatchertimer_Tick(this, new EventArgs());
            GraphUpdater(this, new EventArgs());
            HomePointAdd = true;
        }
        private void fgHomeButton_Click(object sender, RoutedEventArgs e)
        {
            homescore = homescore + 2;
            homeScoreText.Content = homescore;
            hcHomeSum = 0 - HomeelapsedTicks;
            hcHomeNSCount = 0 - hcHomeNSCountTicks;
            if (hcHomeTotalTime >= 3000)
            {
                hcHomeTotalSum = 0 - HomeelapsedTicks;
            }
            if (hcHomeTotalTime < 3000)
            {
                hcHomeTotalSum -= 3000;
            }
            homeStreak += 2;
            awayStreak = 0;
            Streaks(ref streaks);
            poss = false;
            Possession(ref possession);
            WinningTeam(ref winteam);
            WinningPercentAnimation(ref wpAnimation);
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            if (timerStart == false)
            {
                startButton.Focus();
            }
            Homedispatchertimer_Tick(this, new EventArgs());
            Awaydispatchertimer_Tick(this, new EventArgs());
            GraphUpdater(this, new EventArgs());
            HomePointAdd = true;
        }
        private void pgHomeButton_Click(object sender, RoutedEventArgs e)
        {
            homescore = homescore + 3;
            homeScoreText.Content = homescore;
            hcHomeSum = 0 - HomeelapsedTicks;
            hcHomeNSCount = 0 - hcHomeNSCountTicks;
            if (hcHomeTotalTime >= 4500)
            {
                hcHomeTotalSum = 0 - HomeelapsedTicks;
            }
            if (hcHomeTotalTime < 4500)
            {
                hcHomeTotalSum -= 4500;
            }
            homeStreak += 3;
            awayStreak = 0;
            Streaks(ref streaks);
            poss = false;
            Possession(ref possession);
            WinningTeam(ref winteam);
            WinningPercentAnimation(ref wpAnimation);
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            if (timerStart == false)
            {
                startButton.Focus();
            }
            Homedispatchertimer_Tick(this, new EventArgs());
            Awaydispatchertimer_Tick(this, new EventArgs());
            GraphUpdater(this, new EventArgs());
            HomePointAdd = true;
        }
        private void ftAwayButton_Click(object sender, RoutedEventArgs e)
        {
            awayscore = awayscore + 1;
            awayScoreText.Content = awayscore;
            hcAwaySum = 0 - AwayelapsedTicks;
            hcAwayNSCount = 0 - hcAwayNSCountTicks;
            if (hcAwayTotalTime >= 750)
            {
                hcAwayTotalSum = 0 - AwayelapsedTicks;
            }
            if (hcAwayTotalTime < 750)
            {
                hcAwayTotalSum -= 750;
            }
            homeStreak = 0;
            awayStreak += 1;
            Streaks(ref streaks);
            poss = true;
            Possession(ref possession);
            WinningTeam(ref winteam);
            WinningPercentAnimation(ref wpAnimation);
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            if (timerStart == false)
            {
                startButton.Focus();
            }
            Homedispatchertimer_Tick(this, new EventArgs());
            Awaydispatchertimer_Tick(this, new EventArgs());
            GraphUpdater(this, new EventArgs());
            AwayPointAdd = true;
        }
        private void fgAwayButton_Click(object sender, RoutedEventArgs e)
        {
            awayscore = awayscore + 2;
            awayScoreText.Content = awayscore;
            hcAwaySum = 0 - AwayelapsedTicks;
            hcAwayNSCount = 0 - hcAwayNSCountTicks;
            if (hcAwayTotalTime >= 3000)
            {
                hcAwayTotalSum = 0 - AwayelapsedTicks;
            }
            if (hcAwayTotalTime < 3000)
            {
                hcAwayTotalSum -= 3000;
            }
            homeStreak = 0;
            awayStreak += 2;
            Streaks(ref streaks);
            poss = true;
            Possession(ref possession);
            WinningTeam(ref winteam);
            WinningPercentAnimation(ref wpAnimation);
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            if (timerStart == false)
            {
                startButton.Focus();
            }
            Homedispatchertimer_Tick(this, new EventArgs());
            Awaydispatchertimer_Tick(this, new EventArgs());
            GraphUpdater(this, new EventArgs());
            AwayPointAdd = true;
        }
        private void pgAwayButton_Click(object sender, RoutedEventArgs e)
        {
            awayscore = awayscore + 3;
            awayScoreText.Content = awayscore;
            hcAwaySum = 0 - AwayelapsedTicks;
            hcAwayNSCount = 0 - hcAwayNSCountTicks;
            if (hcAwayTotalTime >= 4500)
            {
                hcAwayTotalSum = 0 - AwayelapsedTicks;
            }
            if (hcAwayTotalTime < 4500)
            {
                hcAwayTotalSum -= 4500;
            }
            homeStreak = 0;
            awayStreak += 3;
            Streaks(ref streaks);
            poss = true;
            Possession(ref possession);
            WinningTeam(ref winteam);
            WinningPercentAnimation(ref wpAnimation);
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            if (timerStart == false)
            {
                startButton.Focus();
            }
            Homedispatchertimer_Tick(this, new EventArgs());
            Awaydispatchertimer_Tick(this, new EventArgs());
            GraphUpdater(this, new EventArgs());
            AwayPointAdd = true;
        }
        private void minus1Home_Click(object sender, RoutedEventArgs e)
        {
            homescore = homescore - 1;
            homeScoreText.Content = homescore;
            if(homeStreak > 0)
            {
                homeStreak -= 1;
            }
            Streaks(ref streaks);
            WinningPercentAnimation(ref wpAnimation);
            if (timerStart == false)
            {
                WinningPercentValue(ref winpercent);
            }
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            if (timerStart == false)
            {
                startButton.Focus();
            }
            if (homescore < 0)
            {
                homescore = 0;
                homeScoreText.Content = homescore;
                messageLabel.Content = "You can't have negative points!";
                animation(ref Animation);
            }
            GraphUpdater(this, new EventArgs());
        }
        private void minus1Away_Click(object sender, RoutedEventArgs e)
        {
            awayscore = awayscore - 1;
            awayScoreText.Content = awayscore;
            if(awayStreak > 0)
            {
                awayStreak -= 1;
            }
            Streaks(ref streaks);
            WinningPercentAnimation(ref wpAnimation);
            if (timerStart == false)
            {
                WinningPercentValue(ref winpercent);
            }
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
            if (timerStart == false)
            {
                startButton.Focus();
            }
            if (awayscore < 0)
            {
                awayscore = 0;
                awayScoreText.Content = awayscore;
                messageLabel.Content = "You can't have negative points!";
                animation(ref Animation);
            }
            GraphUpdater(this, new EventArgs());
        }
        private void Streaks(ref bool streaks)
        {
            if (homeStreak < 5 || awayStreak < 5)
            {
                streak = false;
                homeStreakPic.Source = new BitmapImage(new Uri("Data Elements/arrowsneutral.png", UriKind.Relative));
                awayStreakPic.Source = new BitmapImage(new Uri("Data Elements/arrowsneutral.png", UriKind.Relative));
                homeStreakPic.Opacity = .50;
                awayStreakPic.Opacity = .50;
            }
            if (homeStreak >= 5 || awayStreak >= 5)
            {
                streak = true;
            }
            if (homeStreak >= 5 && homeStreak < 10)
            {
                homeStreakPic.Source = new BitmapImage(new Uri("Data Elements/uparrow.png", UriKind.Relative));
                awayStreakPic.Source = new BitmapImage(new Uri("Data Elements/downarrow.png", UriKind.Relative));
                homeStreakPic.Opacity = 1;
                awayStreakPic.Opacity = 1;
            }
            if (homeStreak >= 10 && homeStreak < 15)
            {
                homeStreakPic.Source = new BitmapImage(new Uri("Data Elements/up1arrow.png", UriKind.Relative));
                awayStreakPic.Source = new BitmapImage(new Uri("Data Elements/down1arrow.png", UriKind.Relative));
                homeStreakPic.Opacity = 1;
                awayStreakPic.Opacity = 1;
            }
            if (homeStreak >= 15 && homeStreak < 20)
            {
                homeStreakPic.Source = new BitmapImage(new Uri("Data Elements/up2arrows.png", UriKind.Relative));
                awayStreakPic.Source = new BitmapImage(new Uri("Data Elements/down2arrows.png", UriKind.Relative));
                homeStreakPic.Opacity = 1;
                awayStreakPic.Opacity = 1;
            }
            if (homeStreak >= 20)
            {
                homeStreakPic.Source = new BitmapImage(new Uri("Data Elements/up3arrows.png", UriKind.Relative));
                awayStreakPic.Source = new BitmapImage(new Uri("Data Elements/down3arrows.png", UriKind.Relative));
                homeStreakPic.Opacity = 1;
                awayStreakPic.Opacity = 1;
            }
            if (awayStreak >= 5 && awayStreak < 10)
            {
                awayStreakPic.Source = new BitmapImage(new Uri("Data Elements/uparrow.png", UriKind.Relative));
                homeStreakPic.Source = new BitmapImage(new Uri("Data Elements/downarrow.png", UriKind.Relative));
                homeStreakPic.Opacity = 1;
                awayStreakPic.Opacity = 1;
            }
            if (awayStreak >= 10 && awayStreak < 15)
            {
                awayStreakPic.Source = new BitmapImage(new Uri("Data Elements/up1arrow.png", UriKind.Relative));
                homeStreakPic.Source = new BitmapImage(new Uri("Data Elements/down1arrow.png", UriKind.Relative));
                homeStreakPic.Opacity = 1;
                awayStreakPic.Opacity = 1;
            }
            if (awayStreak >= 15 && awayStreak < 20)
            {
                awayStreakPic.Source = new BitmapImage(new Uri("Data Elements/up2arrows.png", UriKind.Relative));
                homeStreakPic.Source = new BitmapImage(new Uri("Data Elements/down2arrows.png", UriKind.Relative));
                homeStreakPic.Opacity = 1;
                awayStreakPic.Opacity = 1;
            }
            if (awayStreak >= 20)
            {
                awayStreakPic.Source = new BitmapImage(new Uri("Data Elements/up3arrows.png", UriKind.Relative));
                homeStreakPic.Source = new BitmapImage(new Uri("Data Elements/down3arrows.png", UriKind.Relative));
                homeStreakPic.Opacity = 1;
                awayStreakPic.Opacity = 1;
            }
            if(streak == true)
            {
                streakHomeLabel.Visibility = Visibility.Visible;
                streakAwayLabel.Visibility = Visibility.Visible;
                homeStreakBack.Visibility = Visibility.Visible;
                awayStreakBack.Visibility = Visibility.Visible;
            }
            if (streak == false)
            {
                streakHomeLabel.Visibility = Visibility.Hidden;
                streakAwayLabel.Visibility = Visibility.Hidden;
                homeStreakBack.Visibility = Visibility.Hidden;
                awayStreakBack.Visibility = Visibility.Hidden;
            }
            if(homeStreak < 10)
            {
                homeStreakAdj = "0" + homeStreak.ToString();
            }
            else { homeStreakAdj = homeStreak.ToString(); }
            if (awayStreak < 10)
            {
                awayStreakAdj = "0" + awayStreak.ToString();
            }
            else { awayStreakAdj = awayStreak.ToString(); }
            streakHomeLabel.Content = homeStreakAdj + "-" + awayStreakAdj;
            streakAwayLabel.Content = awayStreakAdj + "-" + homeStreakAdj;
        }

        private void homeHotButton_Click(object sender, RoutedEventArgs e)
        {
            hcHomeTotalSum = hcHomeTotalSum - 3000;
            hcHomeNSCount = hcHomeNSCount - 3000;
            Homedispatchertimer_Tick(this, new EventArgs());
            if (timerStart == false)
            {
                startButton.Focus();
            }
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
        }

        private void awayHotButton_Click(object sender, RoutedEventArgs e)
        {
            hcAwayTotalSum = hcAwayTotalSum - 3000;
            hcAwayNSCount = hcAwayNSCount - 3000;
            Awaydispatchertimer_Tick(this, new EventArgs());
            if (timerStart == false)
            {
                startButton.Focus();
            }
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
        }
        private void homeNeutralButton_Click(object sender, RoutedEventArgs e)
        {
            hcHomeSum = 0 - HomeelapsedTicks;
            hcHomeNSCount = 0 - hcHomeNSCountTicks;
            hcHomeTotalSum = 0 - HomeelapsedTicks;
            Homedispatchertimer_Tick(this, new EventArgs());
            if(timerStart == false)
            {
                startButton.Focus();
            }
            if(timerStart == true)
            {
                pauseButton.Focus();
            }
        }
        private void homeColdButton_Click(object sender, RoutedEventArgs e)
        {
            hcHomeTotalSum = hcHomeTotalSum + 3000;
            hcHomeNSCount = hcHomeNSCount + 3000;
            Homedispatchertimer_Tick(this, new EventArgs());
            if (timerStart == false)
            {
                startButton.Focus();
            }
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
        }
        private void awayColdButton_Click(object sender, RoutedEventArgs e)
        {
            hcAwayTotalSum = hcAwayTotalSum + 3000;
            hcAwayNSCount = hcAwayNSCount + 3000;
            Awaydispatchertimer_Tick(this, new EventArgs());
            if (timerStart == false)
            {
                startButton.Focus();
            }
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
        }
        private void awayNeutralButton_Click(object sender, RoutedEventArgs e)
        {
            hcAwaySum = 0 - AwayelapsedTicks;
            hcAwayNSCount = 0 - hcAwayNSCountTicks;
            hcAwayTotalSum = 0 - AwayelapsedTicks;
            Awaydispatchertimer_Tick(this, new EventArgs());
            if (timerStart == false)
            {
                startButton.Focus();
            }
            if (timerStart == true)
            {
                pauseButton.Focus();
            }
        }
#endregion
        #region shortcuts
        //KEYBOARD SHORTCUTS                     
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (gameover == false)
            {
                if (e.Key == Key.Space)
                {
                    if (timerStart == true)
                    {
                        pauseButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent, pauseButton));
                    }
                    else if (timerStart == false)
                    {
                        startButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent, startButton));
                    }
                    WinningPercentAnimation(ref wpAnimation);
                }
                if (e.Key == Key.NumPad3)
                {
                    if(switchTeams == false)
                    {
                        ftAwayButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                    if(switchTeams == true)
                    {
                        ftHomeButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                }
                if (e.Key == Key.NumPad6)
                {
                    if (switchTeams == false)
                    {
                        fgAwayButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                    if (switchTeams == true)
                    {
                        fgHomeButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                }
                if (e.Key == Key.NumPad9)
                {
                    if (switchTeams == false)
                    {
                        pgAwayButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                    if (switchTeams == true)
                    {
                        pgHomeButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                }
                if (e.Key == Key.NumPad1)
                {
                    if (switchTeams == false)
                    {
                        ftHomeButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                    if (switchTeams == true)
                    {
                        ftAwayButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                }
                if (e.Key == Key.NumPad4)
                {
                    if (switchTeams == false)
                    {
                        fgHomeButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                    if (switchTeams == true)
                    {
                        fgAwayButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                }
                if (e.Key == Key.NumPad7)
                {
                    if (switchTeams == false)
                    {
                        pgHomeButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                    if (switchTeams == true)
                    {
                        pgAwayButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                }
                if (e.Key == Key.NumPad0)
                {
                    if (switchTeams == false)
                    {
                        minus1Home.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                    if (switchTeams == true)
                    {
                        minus1Away.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                }
                if (e.Key == Key.Decimal)
                {
                    if (switchTeams == false)
                    {
                        minus1Away.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                    if (switchTeams == true)
                    {
                        minus1Home.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                    }
                }
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    shiftdown = true;
                }
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)))
                {
                    shiftdown = false;
                }
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    controldown = true;
                }
                if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                {
                    controldown = false;
                }
                if (shiftdown == true && controldown == true && (e.Key == Key.Add || e.Key == Key.OemPlus)) 
                {
                    plus1min.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                }
                if (shiftdown == true && controldown == true && (e.Key == Key.Subtract || e.Key == Key.OemMinus))
                {
                    minus1min.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                }
                if (controldown == true && shiftdown == false && (e.Key == Key.Add || e.Key == Key.OemPlus))
                {
                    plus1sec.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                }
                if (controldown == true && shiftdown == false && (e.Key == Key.Subtract || e.Key == Key.OemMinus))
                {
                    minus1sec.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                }
                if (shiftdown == false && controldown == false && (e.Key == Key.Add || e.Key == Key.OemPlus))
                {
                    plus1mil.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                }
                if (shiftdown == false && controldown == false && (e.Key == Key.Subtract || e.Key == Key.OemMinus))
                {
                    minus1mil.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                }
                if (e.Key == Key.Left)
                {
                    if(switchTeams == false)
                    {
                        poss = true;
                        Possession(ref possession);
                        e.Handled = true;
                    }
                    if (switchTeams == true)
                    {
                        poss = false;
                        Possession(ref possession);
                        e.Handled = true;
                    }
                }
                if (e.Key == Key.Right)
                {
                    if (switchTeams == true)
                    {
                        poss = true;
                        Possession(ref possession);
                        e.Handled = true;
                    }
                    if (switchTeams == false)
                    {
                        poss = false;
                        Possession(ref possession);
                        e.Handled = true;
                    }
                }
            }      
        }
        #endregion
        #region gradients and effects
        //GRADIENTS AND EFFECTS
        private void HomeNameBackdrop (ref int homeLetters)
        {
            if (homeLetters == 1 || homeLetters == 3 || homeLetters == 5 || homeLetters == 7 || homeLetters == 9 || homeLetters == 11 || homeLetters == 13)
            {
                homeTeamNameBackOdd.Visibility = Visibility.Visible;
            }
            else { homeTeamNameBackOdd.Visibility = Visibility.Hidden;}
            if (homeLetters == 2 || homeLetters == 4 || homeLetters == 6 || homeLetters == 8 || homeLetters == 10 || homeLetters == 12 || homeLetters == 14)
            {
                homeTeamNameBackEven.Visibility = Visibility.Visible;
            }
            else { homeTeamNameBackEven.Visibility = Visibility.Hidden; }
        }
        private void AwayNameBackdrop(ref int awayLetters)
        {
            if (awayLetters == 1 || awayLetters == 3 || awayLetters == 5 || awayLetters == 7 || awayLetters == 9 || awayLetters == 11 || awayLetters == 13)
            {
                awayTeamNameBackOdd.Visibility = Visibility.Visible;
            }
            else { awayTeamNameBackOdd.Visibility = Visibility.Hidden; }
            if (awayLetters == 2 || awayLetters == 4 || awayLetters == 6 || awayLetters == 8 || awayLetters == 10 || awayLetters == 12 || awayLetters == 14)
            {
                awayTeamNameBackEven.Visibility = Visibility.Visible;
            }
            else { awayTeamNameBackEven.Visibility = Visibility.Hidden; }
        }
        private void HomeBackColor (ref System.Windows.Media.Color homecolor)
        {
            LinearGradientBrush homefade = new LinearGradientBrush();
            homefade.StartPoint = new System.Windows.Point(0, 0);
            homefade.EndPoint = new System.Windows.Point(1, 1);
            homefade.GradientStops.Add(new GradientStop(homecolor, 0.0));
            homefade.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
            System.Windows.Application.Current.Resources["homebackplate"] = homefade;

            buzzerHomebrush.GradientStops.Add(new GradientStop(Colors.WhiteSmoke, 0.2));
            buzzerHomebrush.GradientStops.Add(new GradientStop(homecolor, 1.0));
            buzzerChoice1.Background = buzzerHomebrush;
        }
        private void AwayBackColor(ref System.Windows.Media.Color awaycolor)
        {
            LinearGradientBrush awayfade = new LinearGradientBrush();
            awayfade.StartPoint = new System.Windows.Point(1, 0);
            awayfade.EndPoint = new System.Windows.Point(0, 1);
            awayfade.GradientStops.Add(new GradientStop(awaycolor, 0.0));
            awayfade.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
            System.Windows.Application.Current.Resources["awaybackplate"] = awayfade;

            buzzerAwaybrush.GradientStops.Add(new GradientStop(Colors.WhiteSmoke, 0.2));
            buzzerAwaybrush.GradientStops.Add(new GradientStop(awaycolor, 1.0));
            buzzerChoice3.Background = buzzerAwaybrush;
        }
        private void homeBonusMain_MLBD(object sender, MouseButtonEventArgs e)
        {
            homeBonusClick = !homeBonusClick;
            if(homeBonusPlusClick == true)
            {
                homeBonusPlusClick = false;
            }
            homeBonuses(ref bonus);
            if (timerStart == false)
            {
                WinningPercentValue(ref winpercent);
            }
        }
        private void homeBonusMain_MLBU(object sender, MouseButtonEventArgs e)
        {
            SolidColorBrush bonusClick = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 4, 168, 4));
            homeBonusMain.Foreground = bonusClick;
            homeBonusMain.Opacity = 50;
            if(homeBonusClick == true)
            {
                SolidColorBrush bonusClickGlow = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 255, 0));
                homeBonusMain.Foreground = bonusClickGlow;
                homeBonusMain.Opacity = 100;
            }
        }
        private void homeBonusMain_Enter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(homeBonusClick == false)
            {
                homeBonusMain.Opacity = .4;
                DoubleAnimation da = new DoubleAnimation();
                da.From = .4;
                da.To = 1;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                homeBonusMain.BeginAnimation(OpacityProperty, da);
            }
        }
        private void homeBonusMain_Leave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (homeBonusClick == false && homeBonusPlusClick == false)
            {
                homeBonusMain.Opacity = 1;
                DoubleAnimation da = new DoubleAnimation();
                da.From = 1;
                da.To = .4;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                homeBonusMain.BeginAnimation(OpacityProperty, da);
                if(homeBonusPlus.Opacity == 1)
                {
                    DoubleAnimation db = new DoubleAnimation();
                    db.From = 1;
                    db.To = .4;
                    db.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                    db.RepeatBehavior = new RepeatBehavior(1.0);
                    homeBonusPlus.BeginAnimation(OpacityProperty, db);
                }
            }           
        }
        private void homeBonusPlus_MLBD(object sender, MouseButtonEventArgs e)
        {
            if(homeBonusClick == false)
            {
                homeBonusClick = true;
            }           
            homeBonusPlusClick = !homeBonusPlusClick;
            homeBonuses(ref bonus);
            if (timerStart == false)
            {
                WinningPercentValue(ref winpercent);
            }
        }
        private void homeBonusPlus_MLBU(object sender, MouseButtonEventArgs e)
        {
            if (homeBonusPlusClick == true)
            {
                SolidColorBrush bonusClickGlow = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 255, 0));
                homeBonusMain.Foreground = bonusClickGlow;
                homeBonusPlus.Foreground = bonusClickGlow;
                homeBonusMain.Opacity = 100;
                homeBonusPlus.Opacity = 100;
            }
        }
        private void homeBonusPlus_Enter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (homeBonusClick == false && homeBonusPlusClick == false)
            {
                homeBonusMain.Opacity = .4;
                DoubleAnimation da = new DoubleAnimation();
                da.From = .4;
                da.To = 1;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                homeBonusMain.BeginAnimation(OpacityProperty, da);

                homeBonusPlus.Opacity = .4;
                DoubleAnimation db = new DoubleAnimation();
                db.From = .4;
                db.To = 1;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                homeBonusPlus.BeginAnimation(OpacityProperty, db);
            }
            if (homeBonusClick == true && homeBonusPlusClick == false)
            {
                homeBonusPlus.Opacity = .4;
                DoubleAnimation db = new DoubleAnimation();
                db.From = .4;
                db.To = 1;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                homeBonusPlus.BeginAnimation(OpacityProperty, db);
            }
        }
        private void homeBonusPlus_Leave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (homeBonusClick == false && homeBonusPlusClick == false)
            {
                homeBonusMain.Opacity = 1;
                DoubleAnimation da = new DoubleAnimation();
                da.From = 1;
                da.To = .4;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                homeBonusMain.BeginAnimation(OpacityProperty, da);
                
                homeBonusPlus.Opacity = 1;
                DoubleAnimation db = new DoubleAnimation();
                db.From = 1;
                db.To = .4;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                homeBonusPlus.BeginAnimation(OpacityProperty, db);
            }
            if (homeBonusClick == true && homeBonusPlusClick == false)
            {
                homeBonusPlus.Opacity = 1;
                DoubleAnimation db = new DoubleAnimation();
                db.From = 1;
                db.To = .4;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                homeBonusPlus.BeginAnimation(OpacityProperty, db);
            }
        }
        private void homeBonuses(ref bool bonus)
        {
            SolidColorBrush bonusClickGlow = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 255, 0));
            SolidColorBrush bonusClick = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 4, 168, 4));
            if (homeBonusClick == false && homeBonusPlusClick == false)
            {
                homeBonusMain.Foreground = bonusClick;
                homeBonusPlus.Foreground = bonusClick;
            }
            if (homeBonusClick == true && homeBonusPlusClick == false)
            {
                homeBonusMain.Foreground = bonusClickGlow;
                homeBonusPlus.Foreground = bonusClick;
            }
            if (homeBonusClick == true && homeBonusPlusClick == true)
            {
                homeBonusMain.Foreground = bonusClickGlow;
                homeBonusPlus.Foreground = bonusClickGlow;
            }
        }
        private void awayBonusMain_MLBD(object sender, MouseButtonEventArgs e)
        {
            awayBonusClick = !awayBonusClick;
            if (awayBonusPlusClick == true)
            {
                awayBonusPlusClick = false;
            }
            awayBonuses(ref bonus);
            if (timerStart == false)
            {
                WinningPercentValue(ref winpercent);
            }
        }
        private void awayBonusMain_MLBU(object sender, MouseButtonEventArgs e)
        {
            SolidColorBrush bonusClick = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 4, 168, 4));
            awayBonusMain.Foreground = bonusClick;
            awayBonusMain.Opacity = 50;
            if (awayBonusClick == true)
            {
                SolidColorBrush bonusClickGlow = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 255, 0));
                awayBonusMain.Foreground = bonusClickGlow;
                awayBonusMain.Opacity = 100;
            }
        }
        private void awayBonusMain_Enter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (awayBonusClick == false)
            {
                awayBonusMain.Opacity = .4;
                DoubleAnimation da = new DoubleAnimation();
                da.From = .4;
                da.To = 1;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                awayBonusMain.BeginAnimation(OpacityProperty, da);
            }
        }
        private void awayBonusMain_Leave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (awayBonusClick == false && awayBonusPlusClick == false)
            {
                awayBonusMain.Opacity = 1;
                DoubleAnimation da = new DoubleAnimation();
                da.From = 1;
                da.To = .4;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                awayBonusMain.BeginAnimation(OpacityProperty, da);
                if (awayBonusPlus.Opacity == 1)
                {
                    DoubleAnimation db = new DoubleAnimation();
                    db.From = 1;
                    db.To = .4;
                    db.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                    db.RepeatBehavior = new RepeatBehavior(1.0);
                    awayBonusPlus.BeginAnimation(OpacityProperty, db);
                }
            }
        }
        private void awayBonusPlus_MLBD(object sender, MouseButtonEventArgs e)
        {
            if (awayBonusClick == false)
            {
                awayBonusClick = true;
            }
            awayBonusPlusClick = !awayBonusPlusClick;
            awayBonuses(ref bonus);
            if (timerStart == false)
            {
                WinningPercentValue(ref winpercent);
            }
        }
        private void awayBonusPlus_MLBU(object sender, MouseButtonEventArgs e)
        {
            if (awayBonusPlusClick == true)
            {
                SolidColorBrush bonusClickGlow = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 255, 0));
                awayBonusMain.Foreground = bonusClickGlow;
                awayBonusPlus.Foreground = bonusClickGlow;
                awayBonusMain.Opacity = 100;
                awayBonusPlus.Opacity = 100;
            }
        }
        private void awayBonusPlus_Enter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (awayBonusClick == false && awayBonusPlusClick == false)
            {
                awayBonusMain.Opacity = .4;
                DoubleAnimation da = new DoubleAnimation();
                da.From = .4;
                da.To = 1;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                awayBonusMain.BeginAnimation(OpacityProperty, da);

                awayBonusPlus.Opacity = .4;
                DoubleAnimation db = new DoubleAnimation();
                db.From = .4;
                db.To = 1;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                awayBonusPlus.BeginAnimation(OpacityProperty, db);
            }
            if (awayBonusClick == true && awayBonusPlusClick == false)
            {
                awayBonusPlus.Opacity = .4;
                DoubleAnimation db = new DoubleAnimation();
                db.From = .4;
                db.To = 1;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                awayBonusPlus.BeginAnimation(OpacityProperty, db);
            }
        }
        private void awayBonusPlus_Leave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (awayBonusClick == false && awayBonusPlusClick == false)
            {
                awayBonusMain.Opacity = 1;
                DoubleAnimation da = new DoubleAnimation();
                da.From = 1;
                da.To = .4;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                da.RepeatBehavior = new RepeatBehavior(1.0);
                awayBonusMain.BeginAnimation(OpacityProperty, da);

                awayBonusPlus.Opacity = 1;
                DoubleAnimation db = new DoubleAnimation();
                db.From = 1;
                db.To = .4;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                awayBonusPlus.BeginAnimation(OpacityProperty, db);
            }
            if (awayBonusClick == true && awayBonusPlusClick == false)
            {
                awayBonusPlus.Opacity = 1;
                DoubleAnimation db = new DoubleAnimation();
                db.From = 1;
                db.To = .4;
                db.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                db.RepeatBehavior = new RepeatBehavior(1.0);
                awayBonusPlus.BeginAnimation(OpacityProperty, db);
            }
        }
        private void awayBonuses(ref bool bonus)
        {
            SolidColorBrush bonusClickGlow = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 255, 0));
            SolidColorBrush bonusClick = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 4, 168, 4));
            if (awayBonusClick == false && awayBonusPlusClick == false)
            {
                awayBonusMain.Foreground = bonusClick;
                awayBonusPlus.Foreground = bonusClick;
            }
            if (awayBonusClick == true && awayBonusPlusClick == false)
            {
                awayBonusMain.Foreground = bonusClickGlow;
                awayBonusPlus.Foreground = bonusClick;
            }
            if (awayBonusClick == true && awayBonusPlusClick == true)
            {
                awayBonusMain.Foreground = bonusClickGlow;
                awayBonusPlus.Foreground = bonusClickGlow;
            }
        }
        public SolidColorBrush HomePointBrush
        {
            get { return homebrush; }
            set { 
                    HomePointBrush = value;
                }
        }
        #endregion
        #region percentage values
        //Percentage Values
        private void WinningTeam(ref bool winteam)
        {
            scoreDif = Math.Abs(homescore - awayscore);
            WinningPercentValue(ref winpercent);
        }
        public void WinningPercentValue(ref bool winpercent)
        {
            SolidColorBrush win = new SolidColorBrush();
            SolidColorBrush lose = new SolidColorBrush();
            win.Color = Colors.Gold;
            lose.Color = Colors.Red;
            homeTotal = homeWins + homeLosses;
            homeRecord = homeWins / homeTotal;
            awayTotal = awayWins + awayLosses;
            awayRecord = awayWins / awayTotal;
            AVGtotal = (homeTotal + awayTotal) / 2;
            homeRecordEqu = homeRecord / awayRecord;
            awayRecordEqu = awayRecord / homeRecord;
            if(homeRecord > awayRecord)
            {
                recordValue = (homeRecord - awayRecord) * AVGtotal;
                if (homeWins - awayWins >= 4 && homeWins - awayWins <= 8)
                {
                    favoredHome.Visibility = Visibility.Visible;
                    favoredHome.Source = new BitmapImage(new Uri("Data Elements/favored.png", UriKind.Relative));
                }
                if (homeWins - awayWins >= 8)
                {
                    favoredHome.Visibility = Visibility.Visible;
                    favoredHome.Source = new BitmapImage(new Uri("Data Elements/favored heavy.png", UriKind.Relative));
                }
            }
            if (homeRecord < awayRecord)
            {
                recordValue = (awayRecord - homeRecord) * AVGtotal;
                if (awayWins - homeWins >= 4 && awayWins - homeWins <= 8)
                {
                    favoredAway.Visibility = Visibility.Visible;
                    favoredAway.Source = new BitmapImage(new Uri("Data Elements/favored.png", UriKind.Relative));
                }
                if (awayWins - homeWins >= 8)
                {
                    favoredAway.Visibility = Visibility.Visible;
                    favoredAway.Source = new BitmapImage(new Uri("Data Elements/favored_heavy.png", UriKind.Relative));
                }
            }
            if (homeRecord == awayRecord)
            {
                recordValue = 0;
            }
            elapsedTicksAdj = elapsedTicksCount10;
            timeLeftValue = elapsedTicksAdj / 6000;
            minutesRemaining = elapsedTicksAdj / 6000;
            if(homescore == awayscore)
            {
                if (homeStreak > 0)
                {
                    swingvalue = (homeStreak * .05);
                }
                if (awayStreak > 0)
                {
                    swingvalue = (awayStreak * .05);
                }
            }
            if(homescore > awayscore)
            {
                if(homeStreak > 0)
                {
                    swingvalue = (homeStreak * .05);
                }
                if (awayStreak > 0)
                {
                    swingvalue = -(awayStreak * .05);
                }
                if (awayRecord > homeRecord)
                {
                    recordValue = -recordValue;
                }
                if (poss == true)
                {
                    timeLeftValue = timeLeftValue - .5;
                }
                if (poss == false)
                {
                    timeLeftValue = timeLeftValue + .5;
                }
                timeLeftValue = timeLeftValue - .2;
                if (homeBonusClick == true && homeBonusPlusClick == false && awayBonusClick == false)
                {
                    bonusvalue = 1;
                }
                if (homeBonusPlusClick == true && awayBonusClick == true && awayBonusPlusClick == false)
                {
                    bonusvalue = 1;
                }
                if (homeBonusPlusClick == true && awayBonusClick == false)
                {
                    bonusvalue = 2;
                }
                if (homeBonusClick == false && awayBonusClick == true && awayBonusPlusClick == false)
                {
                    bonusvalue = -1;
                }
                if (homeBonusClick == true && homeBonusPlusClick == false && awayBonusPlusClick == true)
                {
                    bonusvalue = -1;
                }
                if (homeBonusClick == false && awayBonusPlusClick == true)
                {
                    bonusvalue = -2;
                }
                if (homeBonusClick == true && awayBonusClick == true && homeBonusPlusClick == false && awayBonusPlusClick == false)
                {
                    bonusvalue = 0;
                }
                if (homeBonusPlusClick == true && awayBonusPlusClick == true)
                {
                    bonusvalue = 0;
                }
                if (awayBonusClick == false && awayBonusPlusClick == false && homeBonusClick == false && homeBonusPlusClick == false)
                {
                    bonusvalue = 0;
                }
                //Hot-Cold Numbers
                if (hcHomeTotalSum > -3000 && hcHomeTotalSum < 3000)
                {
                    hcValue = 0;
                }
                if (hcHomeTotalSum <= -3000 && hcHomeTotalSum > -6000)
                {
                    hcValue = 1;
                }
                if (hcHomeTotalSum <= -6000 && hcHomeTotalSum > -9000)
                {
                    hcValue = 2;
                }
                if (hcHomeTotalSum <= -9000 && hcHomeTotalSum > -12000)
                {
                    hcValue = 3;
                }
                if (hcHomeTotalSum <= -12000 && hcHomeTotalSum > -15000)
                {
                    hcValue = 4;
                }
                if (hcHomeTotalSum <= -15000)
                {
                    hcValue = 5;
                }
                if (hcHomeTotalSum >= 3000 && hcHomeTotalSum < 6000)
                {
                    hcValue = -1;
                }
                if (hcHomeTotalSum >= 6000 && hcHomeTotalSum < 9000)
                {
                    hcValue = -2;
                }
                if (hcHomeTotalSum >= 9000 && hcHomeTotalSum < 12000)
                {
                    hcValue = -3;
                }
                if (hcHomeTotalSum >= 12000 && hcHomeTotalSum < 15000)
                {
                    hcValue = -4;
                }
                if (hcHomeTotalSum >= 15000)
                {
                    hcValue = -5;
                }
                if (hcAwayTotalSum > -3000 && hcAwayTotalSum < 3000)
                {
                    hcValue = hcValue + 0;
                }
                if (hcAwayTotalSum <= -3000 && hcAwayTotalSum > -6000)
                {
                    hcValue = hcValue - 1;
                }
                if (hcAwayTotalSum <= -6000 && hcAwayTotalSum > -9000)
                {
                    hcValue = hcValue - 2;
                }
                if (hcAwayTotalSum <= -9000 && hcAwayTotalSum > -12000)
                {
                    hcValue = hcValue - 3;
                }
                if (hcAwayTotalSum <= -12000 && hcAwayTotalSum > -15000)
                {
                    hcValue = hcValue - 4;
                }
                if (hcAwayTotalSum <= -15000)
                {
                    hcValue = hcValue - 5;
                }
                if (hcAwayTotalSum >= 3000 && hcAwayTotalSum < 6000)
                {
                    hcValue = hcValue + 1;
                }
                if (hcAwayTotalSum >= 6000 && hcAwayTotalSum < 9000)
                {
                    hcValue = hcValue + 2;
                }
                if (hcAwayTotalSum >= 9000 && hcAwayTotalSum < 12000)
                {
                    hcValue = hcValue + 3;
                }
                if (hcAwayTotalSum >= 12000 && hcAwayTotalSum < 15000)
                {
                    hcValue = hcValue + 4;
                }
                if (hcAwayTotalSum >= 15000)
                {
                    hcValue = hcValue + 5;
                }
            }
            if (awayscore > homescore)
            {
                if (awayStreak > 0)
                {
                    swingvalue = (awayStreak * .05);
                }
                if (homeStreak > 0)
                {
                    swingvalue = -(homeStreak * .05);
                }
                if (homeRecord > awayRecord)
                {
                    recordValue = -recordValue;
                }
                if (poss == true)
                {
                    timeLeftValue = timeLeftValue + .5;
                }
                if (poss == false)
                {
                    timeLeftValue = timeLeftValue - .5;
                }
                timeLeftValue = timeLeftValue + .2;
                if (awayBonusClick == true && awayBonusPlusClick == false && homeBonusClick == false)
                {
                    bonusvalue = 1;
                }
                if (awayBonusPlusClick == true && homeBonusClick == true && homeBonusPlusClick == false)
                {
                    bonusvalue = 1;
                }
                if (awayBonusPlusClick == true && homeBonusClick == false)
                {
                    bonusvalue = 2;
                }
                if (awayBonusClick == false && homeBonusClick == true && homeBonusPlusClick == false)
                {
                    bonusvalue = -1;
                }
                if (awayBonusClick == true && awayBonusPlusClick == false && homeBonusPlusClick == true)
                {
                    bonusvalue = -1;
                }
                if (awayBonusClick == false && homeBonusPlusClick == true)
                {
                    bonusvalue = -2;
                }
                if (awayBonusClick == true && homeBonusClick == true && awayBonusPlusClick == false && homeBonusPlusClick == false)
                {
                    bonusvalue = 0;
                }
                if (awayBonusPlusClick == true && homeBonusPlusClick == true)
                {
                    bonusvalue = 0;
                }
                if (homeBonusClick == false && homeBonusPlusClick == false && awayBonusClick == false && awayBonusPlusClick == false)
                {
                    bonusvalue = 0;
                }
                //Hot-Cold Numbers
                if (hcAwayTotalSum > -3000 && hcAwayTotalSum < 3000)
                {
                    hcValue = 0;
                }
                if (hcAwayTotalSum <= -3000 && hcAwayTotalSum > -6000)
                {
                    hcValue = 1;
                }
                if (hcAwayTotalSum <= -6000 && hcAwayTotalSum > -9000)
                {
                    hcValue = 2;
                }
                if (hcAwayTotalSum <= -9000 && hcAwayTotalSum > -12000)
                {
                    hcValue = 3;
                }
                if (hcAwayTotalSum <= -12000 && hcAwayTotalSum > -15000)
                {
                    hcValue = 4;
                }
                if (hcAwayTotalSum <= -15000)
                {
                    hcValue = 5;
                }
                if (hcAwayTotalSum >= 3000 && hcAwayTotalSum < 6000)
                {
                    hcValue = -1;
                }
                if (hcAwayTotalSum >= 6000 && hcAwayTotalSum < 9000)
                {
                    hcValue = -2;
                }
                if (hcAwayTotalSum >= 9000 && hcAwayTotalSum < 12000)
                {
                    hcValue = -3;
                }
                if (hcAwayTotalSum >= 12000 && hcAwayTotalSum < 15000)
                {
                    hcValue = -4;
                }
                if (hcAwayTotalSum >= 15000)
                {
                    hcValue = -5;
                }
                if (hcHomeTotalSum > -3000 && hcHomeTotalSum < 3000)
                {
                    hcValue = hcValue + 0;
                }
                if (hcHomeTotalSum <= -3000 && hcHomeTotalSum > -6000)
                {
                    hcValue = hcValue - 1;
                }
                if (hcHomeTotalSum <= -6000 && hcHomeTotalSum > -9000)
                {
                    hcValue = hcValue - 2;
                }
                if (hcHomeTotalSum <= -9000 && hcHomeTotalSum > -12000)
                {
                    hcValue = hcValue - 3;
                }
                if (hcHomeTotalSum <= -12000 && hcHomeTotalSum > -15000)
                {
                    hcValue = hcValue - 4;
                }
                if (hcHomeTotalSum <= -15000)
                {
                    hcValue = hcValue - 5;
                }
                if (hcHomeTotalSum >= 3000 && hcHomeTotalSum < 6000)
                {
                    hcValue = hcValue + 1;
                }
                if (hcHomeTotalSum >= 6000 && hcHomeTotalSum < 9000)
                {
                    hcValue = hcValue + 2;
                }
                if (hcHomeTotalSum >= 9000 && hcHomeTotalSum < 12000)
                {
                    hcValue = hcValue + 3;
                }
                if (hcHomeTotalSum >= 12000 && hcHomeTotalSum < 15000)
                {
                    hcValue = hcValue + 4;
                }
                if (hcHomeTotalSum >= 15000)
                {
                    hcValue = hcValue + 5;
                }
            }
            if (scoreDif == 0)
            {
                startingvalue = 50.00 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = 0;
            }
            if (scoreDif == 1)
            {
                startingvalue = 68.658 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.087;
            }
            if (scoreDif == 2)
            {
                startingvalue = 71.71 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.081;
            }
            if (scoreDif == 3)
            {
                startingvalue = 74.551 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.076;
            }
            if (scoreDif == 4)
            {
                startingvalue = 77.078 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.07;
            }
            if (scoreDif == 5)
            {
                startingvalue = 79.448 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.065;
            }
            if (scoreDif == 6)
            {
                startingvalue = 81.554 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.061;
            }
            if (scoreDif == 7)
            {
                startingvalue = 83.436 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.056;
            }
            if (scoreDif == 8)
            {
                startingvalue = 85.137 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.052;
            }
            if (scoreDif == 9)
            {
                startingvalue = 86.639 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.048;
            }
            if (scoreDif == 10)
            {
                startingvalue = 87.972 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.045;
            }
            if (scoreDif == 11)
            {
                startingvalue = 89.177 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.041;
            }
            if (scoreDif == 12)
            {
                startingvalue = 90.229 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.038;
            }
            if (scoreDif == 13)
            {
                startingvalue = 91.174 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.035;
            }
            if (scoreDif == 14)
            {
                startingvalue = 92.019 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.033;
            }
            if (scoreDif == 15)
            {
                startingvalue = 92.774 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.03;
            }
            if (scoreDif == 16)
            {
                startingvalue = 93.446 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.028;
            }
            if (scoreDif == 17)
            {
                startingvalue = 94.026 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.026;
            }
            if (scoreDif == 18)
            {
                startingvalue = 94.565 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.024;
            }
            if (scoreDif == 19)
            {
                startingvalue = 95.053 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.022;
            }
            if (scoreDif == 20)
            {
                startingvalue = 95.469 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.021;
            }
            if (scoreDif == 21)
            {
                startingvalue = 95.861 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.019;
            }
            if (scoreDif == 22)
            {
                startingvalue = 96.195 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.018;
            }
            if (scoreDif == 23)
            {
                startingvalue = 96.531 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.017;
            }
            if (scoreDif == 24)
            {
                startingvalue = 96.825 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.015;
            }
            if (scoreDif == 25)
            {
                startingvalue = 97.079 + swingvalue + recordValue + bonusvalue + hcValue;
                curvevaluefix = -.014;
            }
            winningPercent = startingvalue * Math.Pow(System.Convert.ToDouble(timeLeftValue), curvevaluefix);
            if (homescore > awayscore)
            {
                if(winningPercent <= 100)
                {
                    homePercent.Content = (winningPercent).ToString("00.00");
                    awayPercent.Content = (100 - winningPercent).ToString("00.00");
                    HomePercentValue = Convert.ToInt32(winningPercent);
                    AwayPercentValue = Convert.ToInt32(100 - winningPercent);
                }
                if (winningPercent > 100)
                {
                    homePercent.Content = (100).ToString("00.00");
                    awayPercent.Content = (0).ToString("00.00");
                    HomePercentValue = Convert.ToInt32(100);
                    AwayPercentValue = Convert.ToInt32(0);
                }
            }
            if (awayscore > homescore)
            {
                if(winningPercent <= 100)
                {
                    awayPercent.Content = (winningPercent).ToString("00.00");
                    homePercent.Content = (100 - winningPercent).ToString("00.00");
                    AwayPercentValue = Convert.ToInt32(winningPercent);
                    HomePercentValue = Convert.ToInt32(100 - winningPercent);
                }
                if (winningPercent > 100)
                {
                    awayPercent.Content = (100).ToString("00.00");
                    homePercent.Content = (0).ToString("00.00");
                    AwayPercentValue = Convert.ToInt32(100);
                    HomePercentValue = Convert.ToInt32(0);
                }
            }
            if (homescore == awayscore)
            {
                if(homeRecord > awayRecord)
                {
                    if (homeStreak > 0)
                    {
                        swingvalue = homeStreak;
                    }
                    if (awayStreak > 0)
                    {
                        swingvalue = -awayStreak;
                    }
                    startingvalue = 50.00 + swingvalue + recordValue + bonusvalue + hcValue;
                    homePercent.Content = (startingvalue).ToString("00.00");
                    awayPercent.Content = (100 - (startingvalue)).ToString("00.00");
                    HomePercentValue = Convert.ToInt32(startingvalue);
                    AwayPercentValue = Convert.ToInt32(100 - startingvalue);
                }
                if (homeRecord < awayRecord)
                {
                    if (homeStreak > 0)
                    {
                        swingvalue = -homeStreak;
                    }
                    if (awayStreak > 0)
                    {
                        swingvalue = awayStreak;
                    }
                    startingvalue = 50.00 + swingvalue + recordValue + bonusvalue + hcValue;
                    awayPercent.Content = (startingvalue).ToString("00.00");
                    homePercent.Content = (100 - (startingvalue)).ToString("00.00");
                    AwayPercentValue = Convert.ToInt32(startingvalue);
                    HomePercentValue = Convert.ToInt32(100 - startingvalue);
                }
                if (homeRecord == awayRecord)
                {
                    awayPercent.Content = (50.00).ToString("00.00");
                    homePercent.Content = (50.00).ToString("00.00");
                    HomePercentValue = 50;
                    AwayPercentValue = 50;
                }
            }
            if ((winningPercent >= 100 || double.IsNaN(winningPercent)) && homescore > awayscore)
            {
                winningPercent = 100;
                homePercent.Content = "WIN!";
                awayPercent.Content = "LO,SE";
                homeRect.Stroke = win;
                awayRect.Stroke = lose;
            }
            if ((winningPercent >= 100 || double.IsNaN(winningPercent)) && homescore < awayscore)
            {
                winningPercent = 100;
                awayPercent.Content = "WIN!";
                homePercent.Content = "LO,SE";
                homeRect.Stroke = lose;
                awayRect.Stroke = win;
            }
            if(minutesRemaining <= 0 && homescore > awayscore)
            {
                winningPercent = 100;
                homePercent.Content = "WIN!";
                awayPercent.Content = "LO,SE!";
                homeRect.Stroke = win;
                awayRect.Stroke = lose;
                HomePercentValue = 100;
                AwayPercentValue = 0;
            }
            if (minutesRemaining <= 0 && homescore < awayscore)
            {
                winningPercent = 100;
                awayPercent.Content = "WIN!";
                homePercent.Content = "LO,SE";
                homeRect.Stroke = lose;
                awayRect.Stroke = win;
                HomePercentValue = 0;
                AwayPercentValue = 100;
            }
        }
        #endregion
        #region Graph and Data
        public void GraphMaker(ref bool graphmaker)
        {
            if(HomeGraphMaker == true && AwayGraphMaker == true)
            {
                //HOMEPOINTS
                sliderImages.HomeBrush = homebrush;
                hbc.HomeBrush = homecolor;
                //Ellipse ToolEllipse = new Ellipse();
                //ToolEllipse = (Ellipse)FindResource("ToolEllipse");
                //ToolEllipse.DataContext = hbc;

                //Ellipse ellipseTest = new Ellipse();
                //ellipseTest = (Ellipse)FindResource("ellipseTest");
                //ellipseTest.DataContext = hbc;
                //ToolEllipse.DataContext = hbc;
                //System.Windows.Data.Binding = new System.Windows.Data.Binding() { Path = new PropertyPath("HomeBrush") }; 
                
                HomeLineStyle.Setters.Add(NullMarker);
                HomeLineStyle.Setters.Add(HomeLineColor);
                HomeLine.DataPointStyle = HomeLineStyle;

                HomePointGrid.AppendChild(HomePointEllipse);
                HomePointTemp.VisualTree = HomePointGrid;
                HomePointSetter = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.TemplateProperty, HomePointTemp);
                HomeLinePointStyle.Setters.Add(HomePointSetter);
                //HomeLinePoints.DataPointStyle = HomeLinePointStyle;

                HomeChartLogoGrid.AppendChild(HomeChartLogo);
                HomeChartLogoTemp.VisualTree = HomeChartLogoGrid;
                HomeLogoSetter = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.TemplateProperty, HomeChartLogoTemp);
                XAxisHomePointStyle.Setters.Add(HomeLogoSetter);
                HomeLineMarker.DataPointStyle = XAxisHomePointStyle;

                //AWAYPOINTS
                
                
                FrameworkElementFactory recttest = new FrameworkElementFactory(typeof(System.Windows.Shapes.Rectangle));
                recttest.SetValue(System.Windows.Shapes.Rectangle.WidthProperty, 50d);
                recttest.SetValue(System.Windows.Shapes.Rectangle.HeightProperty, 30d);
                recttest.SetValue(System.Windows.Shapes.Rectangle.StrokeProperty, System.Windows.Media.Brushes.Black);
                recttest.SetValue(System.Windows.Shapes.Rectangle.StrokeThicknessProperty, 1d);

                AwayLineStyle.Setters.Add(NullMarker);

                AwayLineStyle.Setters.Add(AwayLineColor);
                AwayLine.DataPointStyle = AwayLineStyle;

                AwayChartLogoGrid.AppendChild(AwayChartLogo);
                AwayChartLogoTemp.VisualTree = AwayChartLogoGrid;

                AwayLogoSetter = new Setter(System.Windows.Controls.DataVisualization.Charting.DataPoint.TemplateProperty, AwayChartLogoTemp);
                XAxisAwayPointStyle.Setters.Add(AwayLogoSetter);

                AwayLineMarker.DataPointStyle = XAxisAwayPointStyle;

                //--- APPEND CHART ELEMENTS ---
                //Legend
                XAxisLegendGrid.AppendChild(XAxisHomeLegendImage);
                XAxisLegendGrid.AppendChild(XAxisHomeLegendLabel1);
                XAxisLegendGrid.AppendChild(XAxisHomeLegendLabel2);
                XAxisLegendGrid.AppendChild(XAxisAwayLegendImage);
                XAxisLegendGrid.AppendChild(XAxisAwayLegendLabel1);
                XAxisLegendGrid.AppendChild(XAxisAwayLegendLabel2);

                //Background
                DividerBrush.GradientStops.Add(HomeBackStopFade);
                DividerBrush.GradientStops.Add(HomeBackStopFull);
                DividerBrush.GradientStops.Add(AwayBackStopFull);
                DividerBrush.GradientStops.Add(AwayBackStopFade);

                ChartHomeRGB.GradientStops.Add(HomeRGB1);
                ChartHomeRGB.GradientStops.Add(HomeRGB2);
                ChartBackgroundDivider.SetValue(Line.StrokeProperty, DividerBrush);
                ChartHomePoly.SetValue(Polygon.FillProperty, ChartHomeRGB);

                ChartAwayRGB.GradientStops.Add(AwayRGB1);
                ChartAwayRGB.GradientStops.Add(AwayRGB2);
                ChartBackgroundDivider.SetValue(Line.StrokeProperty, DividerBrush);
                ChartAwayPoly.SetValue(Polygon.FillProperty, ChartAwayRGB);

                ChartBackgroundGrid.AppendChild(ChartHomePoly);
                ChartBackgroundGrid.AppendChild(ChartAwayPoly);
                ChartBackgroundGrid.AppendChild(ChartBackgroundDivider);
                ChartBackgroundGrid.AppendChild(ChartBackgroundHomeImage);
                ChartBackgroundGrid.AppendChild(ChartBackgroundAwayImage);
                ChartBackgroundBorder.AppendChild(ChartBackgroundGrid);

                ChartBackgroundTemp.VisualTree = ChartBackgroundBorder;
                ChartBackgroundSetter = new Setter(LinearAxis.TemplateProperty, ChartBackgroundTemp);
                ChartBackgroundStyle.Setters.Add(ChartBackgroundSetter);
                BackgroundAxis.Style = ChartBackgroundStyle;

                HChartHomeRGB.GradientStops.Add(HomeRGB1);
                HChartHomeRGB.GradientStops.Add(HomeRGB2);
                HChartHomePoly.SetValue(Polygon.FillProperty, HChartHomeRGB);
                HChartBackgroundGrid.AppendChild(HChartHomePoly);
                HChartBackgroundGrid.AppendChild(HChartBackgroundHomeImage);
                HChartBackgroundBorder.AppendChild(HChartBackgroundGrid);
                HChartBackgroundTemp.VisualTree = HChartBackgroundBorder;
                HChartBackgroundSetter = new Setter(LinearAxis.TemplateProperty, HChartBackgroundTemp);
                HChartBackgroundStyle.Setters.Add(HChartBackgroundSetter);

                AChartAwayRGB.GradientStops.Add(AwayRGB1);
                AChartAwayRGB.GradientStops.Add(AwayRGB2);
                AChartAwayPoly.SetValue(Polygon.FillProperty, AChartAwayRGB);
                AChartBackgroundGrid.AppendChild(AChartAwayPoly);
                AChartBackgroundGrid.AppendChild(AChartBackgroundAwayImage);
                AChartBackgroundBorder.AppendChild(AChartBackgroundGrid);
                AChartBackgroundTemp.VisualTree = AChartBackgroundBorder;
                AChartBackgroundSetter = new Setter(LinearAxis.TemplateProperty, AChartBackgroundTemp);
                AChartBackgroundStyle.Setters.Add(AChartBackgroundSetter);

                //CREATE CHART
                XAxisLegend.Setters.Add(XAxisLegendSetter);
                LegendAxis.Style = XAxisLegend;

                percentChart.Axes.Add(xaxis);
                percentChart.Axes.Add(yaxis);
                percentChart.Axes.Add(yaxis2);
                percentChart.Axes.Add(XAxisZone);
                percentChart.Axes.Add(LegendAxis);
                percentChart.Axes.Add(BackgroundAxis);
                
                percentChart.Series.Add(HomeLine);
                percentChart.Series.Add(HomeLinePoints);
                percentChart.Series.Add(HomeLineMarker);
                percentChart.Series.Add(AwayLine);
                percentChart.Series.Add(AwayLinePoints);
                percentChart.Series.Add(AwayLineMarker);
            }    
        }
        public void GraphUpdater(object sender, EventArgs e)
        {
            KeyValuePair<double, double> HomeMarkerPair = new KeyValuePair<double, double>(minutesRemaining, HomePercentValue);
           // KeyValuePair<double, double>[] HomePointsPair = new KeyValuePair<double,double>[100];// = new KeyValuePair<double, double>(minutesRemaining, HomePercentValue);
            KeyValuePair<double, double> HomePair = new KeyValuePair<double, double>(minutesRemaining, HomePercentValue);
            KeyValuePair<double, double> AwayMarkerPair = new KeyValuePair<double, double>(minutesRemaining, AwayPercentValue);
            //KeyValuePair<double, double> AwayPointsPair = new KeyValuePair<double, double>(minutesRemaining, AwayPercentValue);
            KeyValuePair<double, double> AwayPair = new KeyValuePair<double, double>(minutesRemaining, AwayPercentValue);

            if(AwayPointsPair.First == null)
            {
                AwayPointsPair.AddFirst(new KeyValuePair<double, double>(minutesRemaining, AwayPercentValue));
               // AwayPointsPair.AddLast(new KeyValuePair<double, double>(minutesRemaining, AwayPercentValue));
            }
            else
            {
                AwayPointsPair.AddLast(new KeyValuePair<double, double>(minutesRemaining, AwayPercentValue));
            }

            for (int i = 0; i < HomeSeriesMarker.Count; i++)
            {
                HomeSeriesMarker.RemoveAt(i);
            }
            for (int i = 0; i < AwaySeriesMarker.Count; i++)
            {
                AwaySeriesMarker.RemoveAt(i);
            }
            if(HomePercentValue <= 100)
            {
                HomeSeriesMarker.Add(HomeMarkerPair);
                HomeLineMarker.DataContext = HomeSeriesMarker;
                HomeSeries.Add(HomePair);
                HomeLine.DataContext = HomeSeries;
                if(HomePointAdd == true)
                {
                    //HomeSeriesPoints.Add(HomePointsPair);
                    HomeSeriesPoints.Add(HomePointsPair.Last<KeyValuePair<double, double>>());
                    //HomeSeriesPoints[HomeSeriesPoints.Count];
                    HomeLinePoints.DataContext = HomeSeriesPoints;
                    HomePointAdd = false;
                }
            }
            if (HomePercentValue > 100)
            {
                HomeSeriesMarker.Add(new KeyValuePair<double, double>(minutesRemaining, 100));
                HomeLineMarker.DataContext = HomeSeriesMarker;
                HomeSeries.Add(new KeyValuePair<double, double>(minutesRemaining, 100));
                HomeLine.DataContext = HomeSeries;
            }
            if (AwayPercentValue <= 100)
            {
                AwaySeriesMarker.Add(new KeyValuePair<double, double>(minutesRemaining, AwayPercentValue));
                AwayLineMarker.DataContext = AwaySeriesMarker;
                AwaySeries.Add(new KeyValuePair<double, double>(minutesRemaining, AwayPercentValue));
                AwayLine.DataContext = AwaySeries;
                if (AwayPointAdd == true)
                {
                    AwaySeriesPoints.Add(AwayPointsPair.Last<KeyValuePair<double, double>>());
                    AwayCCPerc.Content = AwayPointsPair.Last<KeyValuePair<double, double>>().ToString();
                    //AwaySeriesPoints.Add(AwayPointsPair);
                    AwayLinePoints.DataContext = AwaySeriesPoints;
                    AwayPointAdd = false;
                }
            }
            if (AwayPercentValue > 100)
            {
                AwaySeriesMarker.Add(new KeyValuePair<double, double>(minutesRemaining, 100));
                AwayLineMarker.DataContext = AwaySeriesMarker;
                AwaySeries.Add(new KeyValuePair<double, double>(minutesRemaining, 100));
                AwayLine.DataContext = AwaySeries;
            }

            graphUpdateTimer.Stop();
        }
        public void AwayPointsTT_Enter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DoubleAnimation AwayTTAnim = new DoubleAnimation();
            AwayTTAnim.From = 0;
            AwayTTAnim.To = 1;
            AwayTTAnim.Duration = TimeSpan.FromMilliseconds(400);
            Storyboard.SetTarget(AwayTTAnim, AwayTTGrid);
            Storyboard.SetTargetProperty(AwayTTAnim, new PropertyPath(OpacityProperty));
            Storyboard 
        }
        public void GraphMarkerRemoval(object sender, EventArgs e)
        {
            //percentChart.Series["HomeSeriesMarker"].Points.Clear();
            //percentChart.Series["AwaySeriesMarker"].Points.Clear();
            graphMarkerRemove.Stop();
        }
        private void graphButton_Click(object sender, RoutedEventArgs e)
        {
            graphOpen = true;
            ShowGraphAnimation(this, new RoutedEventArgs());
        }
        private void GameStartClosed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            
        }
        private void OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            DoubleAnimation HomeGrow = new DoubleAnimation();
            DoubleAnimation HomeFade = new DoubleAnimation();
            DoubleAnimation AwayGrow = new DoubleAnimation();
            DoubleAnimation AwayFade = new DoubleAnimation();
            DoubleAnimation HomeGrowMarker = new DoubleAnimation();
            DoubleAnimation HomeFadeMarker = new DoubleAnimation();
            DoubleAnimation AwayGrowMarker = new DoubleAnimation();
            DoubleAnimation AwayFadeMarker = new DoubleAnimation();

            HomeGrow.Duration = TimeSpan.FromMilliseconds(300);
            HomeFade.Duration = TimeSpan.FromMilliseconds(300);
            AwayGrow.Duration = TimeSpan.FromMilliseconds(300);
            AwayFade.Duration = TimeSpan.FromMilliseconds(300);
            HomeGrowMarker.Duration = TimeSpan.FromMilliseconds(300);
            HomeFadeMarker.Duration = TimeSpan.FromMilliseconds(300);
            AwayGrowMarker.Duration = TimeSpan.FromMilliseconds(300);
            AwayFadeMarker.Duration = TimeSpan.FromMilliseconds(300);

            HomeGrow.From = 0;
            HomeGrow.To = 1;
            HomeFade.From = 1;
            HomeFade.To = 0;
            AwayGrow.From = 0;
            AwayGrow.To = 1;
            AwayFade.From = 1;
            AwayFade.To = 0;
            HomeGrowMarker.From = 0;
            HomeGrowMarker.To = 1;
            HomeFadeMarker.From = 1;
            HomeFadeMarker.To = 0;
            AwayGrowMarker.From = 0;
            AwayGrowMarker.To = 1;
            AwayFadeMarker.From = 1;
            AwayFadeMarker.To = 0;

            Storyboard HGBoard = new Storyboard();
            Storyboard HFBoard = new Storyboard();
            Storyboard AGBoard = new Storyboard();
            Storyboard AFBoard = new Storyboard();

            Storyboard.SetTarget(HomeGrow, HomeLine);
            Storyboard.SetTarget(HomeGrowMarker, HomeLineMarker);
            Storyboard.SetTarget(HomeFade, HomeLine);
            Storyboard.SetTarget(HomeFadeMarker, HomeLineMarker);
            Storyboard.SetTarget(AwayGrow, AwayLine);
            Storyboard.SetTarget(AwayGrowMarker, AwayLineMarker);
            Storyboard.SetTarget(AwayFade, AwayLine);
            Storyboard.SetTarget(AwayFadeMarker, AwayLineMarker);

            Storyboard.SetTargetProperty(HomeGrow, new PropertyPath(OpacityProperty));
            Storyboard.SetTargetProperty(HomeFade, new PropertyPath(OpacityProperty));
            Storyboard.SetTargetProperty(AwayGrow, new PropertyPath(OpacityProperty));
            Storyboard.SetTargetProperty(AwayFade, new PropertyPath(OpacityProperty));

            Storyboard.SetTargetProperty(HomeGrowMarker, new PropertyPath(OpacityProperty));
            Storyboard.SetTargetProperty(HomeFadeMarker, new PropertyPath(OpacityProperty));
            Storyboard.SetTargetProperty(AwayGrowMarker, new PropertyPath(OpacityProperty));
            Storyboard.SetTargetProperty(AwayFadeMarker, new PropertyPath(OpacityProperty));

            HGBoard.Children.Add(HomeGrow);
            HFBoard.Children.Add(HomeFade);
            AGBoard.Children.Add(AwayGrow);
            AFBoard.Children.Add(AwayFade);
            HGBoard.Children.Add(HomeGrowMarker);
            HFBoard.Children.Add(HomeFadeMarker);
            AGBoard.Children.Add(AwayGrowMarker);
            AFBoard.Children.Add(AwayFadeMarker);

            if (CustomSlider.Value <= 1 && SliderHome == true && SliderAway == false)
            {
                AFBoard.Completed += new EventHandler(SliderAnimation_Completed);
                AFBoard.Begin(this);
                HGBoard.Begin(this);
                CustomSlider.IsEnabled = false;
                BackgroundAxis.Style = HChartBackgroundStyle;
                CustomSlider.Opacity = .5;
                SliderHome = false;
                SliderAway = true;
            }
            if (CustomSlider.Value <= 1 && SliderHome == false && SliderAway == false)
            {
                AFBoard.Completed += new EventHandler(SliderAnimation_Completed);
                AFBoard.Begin(this);
                CustomSlider.IsEnabled = false;
                BackgroundAxis.Style = HChartBackgroundStyle;
                CustomSlider.Opacity = .5;
                SliderHome = false;
                SliderAway = true;
            }
            if (CustomSlider.Value == 2 && SliderAway == true)
            {
                AGBoard.Completed += new EventHandler(SliderAnimation_Completed);
                AGBoard.Begin(this);
                CustomSlider.IsEnabled = false;
                BackgroundAxis.Style = ChartBackgroundStyle;
                CustomSlider.Opacity = .5;
                SliderHome = false;
                SliderAway = false;
            }
            if (CustomSlider.Value == 2 && SliderHome == true)
            {
                HGBoard.Completed += new EventHandler(SliderAnimation_Completed);
                HGBoard.Begin(this);
                CustomSlider.IsEnabled = false;
                BackgroundAxis.Style = ChartBackgroundStyle;
                CustomSlider.Opacity = .5;
                SliderHome = false;
                SliderAway = false;
            }
            if (CustomSlider.Value >= 3 && SliderHome == false && SliderAway == true)
            {
                HFBoard.Completed += new EventHandler(SliderAnimation_Completed);
                HFBoard.Begin(this);
                AGBoard.Begin(this);
                CustomSlider.IsEnabled = false;
                BackgroundAxis.Style = AChartBackgroundStyle;
                CustomSlider.Opacity = .5;
                SliderHome = true;
                SliderAway = false;
            }
            if (CustomSlider.Value >=3 && SliderHome == false && SliderAway == false)
            {
                HFBoard.Completed += new EventHandler(SliderAnimation_Completed);
                HFBoard.Begin(this);
                CustomSlider.IsEnabled = false;
                BackgroundAxis.Style = AChartBackgroundStyle;
                CustomSlider.Opacity = .5;
                SliderHome = true;
                SliderAway = false;
            }
        }
        public void SliderAnimation_Completed (object sender, EventArgs e)
        {
            CustomSlider.IsEnabled = true;
            CustomSlider.Opacity = 1;
        }
        public void ToolTip_Enter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //for(int i=0; i<HomeLinePoints.Points.Count; i++)
            //{
                //HomeLinePoints.ToolTip = string.Format("X = {00.00}, Y = {00.00}", HomeLinePoints.Points[i].X.ToString(), HomeLinePoints.Points[i].Y.ToString());
            //}
        }
        #endregion
    }
}
