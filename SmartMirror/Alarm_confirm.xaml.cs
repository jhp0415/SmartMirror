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
using System.Windows.Shapes;

namespace SmartMirror
{
    /// <summary>
    /// Alarm_confirm.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Alarm_confirm : Window
    {
        public Alarm_confirm()
        {
            InitializeComponent();

            music_player my = new music_player();

            mp.Source = new Uri(my.SearchFiles[0], UriKind.Relative);     //선택한 미디어로 소스변경하기
            mp.LoadedBehavior = MediaState.Manual;
            mp.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mp.Stop();
        }
    }
}
