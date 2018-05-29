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
using System.IO;

namespace SmartMirror
{
    /// <summary>
    /// SchedulePage_Add.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SchedulePage_Add : Window
    {
        #region Field
        int year, month, day;
        #endregion

        #region Constructor
        public SchedulePage_Add()
        {
            InitializeComponent();

            year = MainWindow.TimeManager.year;
            month = MainWindow.TimeManager.month;
            day = MainWindow.TimeManager.day;

            yearInput.Text = year.ToString();
            monthInput.Text = month.ToString();
            dayInput.Text = day.ToString();

            schedule.Focus();
        }

        public SchedulePage_Add(int _year, int _month, int _day)
        {
            InitializeComponent();

            year = _year;
            month = _month;
            day = _day;

            yearInput.Text = year.ToString();
            monthInput.Text = month.ToString();
            dayInput.Text = day.ToString();

            schedule.Focus();
        }
        #endregion

        // 확인
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ScheduleManager.storeSchedule(year, month, day, schedule.Text);

            this.Close();
        }
        // 취소
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region Schedule KeyBoard
        OnKeyBoard kc = new OnKeyBoard();
        DispatcherTimer m;
        private void Click_TextBox(object sender, MouseButtonEventArgs e)
        {
            if (schedule.Focusable == false)
            {
                schedule.Focusable = true;
                kc.practiceKeyBoard();
            }
            m = new DispatcherTimer();
            m.Interval = TimeSpan.FromMilliseconds(20);
            m.Start();
            m.Tick += Check_State;
        }

        private void Check_State(object sender, EventArgs e)
        {
            if (kc.processstate != 100)
            {
                //mymemo.Text += kc.processstate.ToString();
                schedule.Focusable = false;
                kc.processstate = 100;
                m.Stop();
            }
        }
        #endregion
    }
}