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

namespace SmartMirror
{
    /// <summary>
    /// AdjustmentBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AdjustmentBox : UserControl
    {
        #region Field
        List<string> contentList;
        int index;
        #endregion

        #region Constructor
        public AdjustmentBox()
        {
            InitializeComponent();
            contentList = new List<string>();
            index = 0;

            contentLabel.Text = "";
        }
        public AdjustmentBox(string content)
        {
            InitializeComponent();

            contentLabel.Text = content;
        }
        public AdjustmentBox(List<string> _content, int _index = 0)
        {
            InitializeComponent();

            contentList = _content;
            index = _index;

            contentLabel.Text = contentList[index];
        }
        #endregion

        #region Method
        public void SetContentList(List<string> _content)
        {
            contentList = _content;
            index = 0;

            this.contentLabel.Text = contentList[index];
        }
        #endregion

        #region Event
        private void upButtonEvent_LeftClickDown(object sender, MouseButtonEventArgs e)
        {
            if (contentList.Count > 0)
            {
                if (index < contentList.Count - 1) index++;
                else index = 0;

                contentLabel.Text = contentList[index];
            }
            else
                return;
        }

        private void downButtonEvent_LeftClickDown(object sender, MouseButtonEventArgs e)
        {
            if (contentList.Count > 0)
            {
                if (index > 0) index--;
                else index = contentList.Count - 1;

                contentLabel.Text = contentList[index];
            }
            else
                return;
        }

        private void displayClickEvent_LeftClickDown(object sender, MouseButtonEventArgs e)
        {
            valueList_AdjustmentBox window = new valueList_AdjustmentBox();
            window.ShowDialog();
        }
        #endregion
    }
}
