using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;

namespace SmartMirror
{
    /// <summary>
    /// News_Item.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class News_Item : UserControl
    {
        #region field
        Stopwatch stopwatch = new Stopwatch();
        private string uri { get; set; }
        public static bool news_available = true;
        #endregion

        public News_Item()
        {
            InitializeComponent();
        }

        public News_Item(string uri)
        {
            InitializeComponent();

            this.uri = uri;
        }

        private void news_link(object sender, MouseButtonEventArgs e)
        {
            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds < 200)
            {
                try
                {
                    //System.Diagnostics.Process.Start(url);
                    if (news_available)
                    {
                        News_Web web = new News_Web();
                        web.Width = 800;
                        web.Height = 600;
                        web.navigate_service(uri);
                        web.Show();

                        news_available = false;
                    }
                }
                catch (Exception exception)
                {

                }
            }
            else { }
        }

        private void news_title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            stopwatch.Reset();
            stopwatch.Start();
        }
    }
}
