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
using System.Reflection;

namespace SmartMirror
{
    /// <summary>
    /// News_Web.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class News_Web : Window
    {
        public News_Web()
        {
            InitializeComponent();
            
        }

        public News_Web(string uri)
        {
            InitializeComponent();
            try
            {
                wbSample.Navigate(uri);
            } catch (Exception exception)
            {

            }
        }

        private void txtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                    wbSample.Navigate(txtUrl.Text);
            }catch(Exception exception)
            {

            }
        }

        private void wbSample_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            txtUrl.Text = e.Uri.OriginalString;
            HideScriptErrors(wbSample, true);
        }

        private void BrowseBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((wbSample != null) && (wbSample.CanGoBack));
        }

        private void BrowseBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            wbSample.GoBack();
        }

        private void BrowseForward_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((wbSample != null) && (wbSample.CanGoForward));
        }

        private void BrowseForward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            wbSample.GoForward();
        }

        private void GoToPage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void GoToPage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                wbSample.Navigate(txtUrl.Text);
            }catch(Exception exception)
            {

            }
        }

        public void navigate_service(string uri)
        {
            wbSample.Navigate(uri);
        }

        public void HideScriptErrors(WebBrowser wb, bool Hide)
        {
            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            object objComWebBrowser = fiComWebBrowser.GetValue(wb);

            if (objComWebBrowser == null) return;
            objComWebBrowser.GetType().InvokeMember(
            "Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { Hide });
        }

        private void exit_click(object sender, RoutedEventArgs e)
        {
            this.Close();
            News_Item.news_available = true;
        }
    }
}
