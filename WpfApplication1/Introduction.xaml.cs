using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Introduction.xaml
    /// </summary>
    public partial class Introduction : Window
    {
        bool Internet;
        Storyboard ConnectionStoryboard = new Storyboard();
        DoubleAnimation ConnectionAnimation = new DoubleAnimation();
        public Introduction()
        {
            InitializeComponent();
            LiveButton.IsEnabled = false;
            RetryButton.IsEnabled = false;
            loadingLabel.Visibility = Visibility.Hidden;
            internet(ref Internet);
            
            this.RegisterName("loadingLabel", loadingLabel);

            ConnectionAnimation.From = 1;
            ConnectionAnimation.To = 0;
            ConnectionAnimation.Duration = TimeSpan.FromMilliseconds(400);
            Storyboard.SetTargetName(ConnectionAnimation, "loadingLabel");
            Storyboard.SetTargetProperty(ConnectionAnimation, new PropertyPath(Label.OpacityProperty));

            ConnectionStoryboard.Children.Add(ConnectionAnimation);
        }
        private void internet(ref bool Internet)
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
                request.Timeout = 100;
                request.KeepAlive = false;
                res = request.GetResponse() as HttpWebResponse;
                if (request.Method == "GET")
                {
                    RetryButton.IsEnabled = false;
                    LiveButton.IsEnabled = true;
                    loadingLabel.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                loadingLabel.Content = "Connection Not Found...";
                ConnectionStoryboard.Begin();
                RetryButton.IsEnabled = true;
            }
        }
        private void LiveButton_Click(object sender, RoutedEventArgs e)
        {
            Starting_Window StartingWindow = new Starting_Window();
            StartingWindow.Show();
            this.Close();
        }

        private void ManualButton_Click(object sender, RoutedEventArgs e)
        {
            Starting_Window StartingWindow = new Starting_Window();
            StartingWindow.Show();
            this.Close();
            StartingWindow.ManualMode = true;
        }
        private void RetryButton_Click(object sender, RoutedEventArgs e)
        {
            loadingLabel.Visibility = Visibility.Visible;
            RetryButton.IsEnabled = false;
            internet(ref Internet);
        }
    }
}
