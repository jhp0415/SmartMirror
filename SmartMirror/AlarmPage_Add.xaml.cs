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

namespace SmartMirror
{
    /// <summary>
    /// AlarmPage_Setting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AlarmPage_Add : Window
    {
        #region Field
        Queue<AlarmData> alarmData;

        string date;
        #endregion

        #region Constructor
        public AlarmPage_Add()
        {
            InitializeComponent();

            alarmData = loadAlarm();

            MainWindow.TimeManager.updateTime();
            date = MainWindow.TimeManager.day_Week;
            hourInput.Content = MainWindow.TimeManager.hour.ToString();
            minuteInput.Content = (MainWindow.TimeManager.minute + 1).ToString();

            if (MainWindow.TimeManager.hour > 12) meridiemButton.Content = "A.M";
            else meridiemButton.Content = "P.M";

            alarm.Focus();
        }
        #endregion

        #region Method
        public static void eraseAlarm(string _day_week, int _hour, int _minute, string _content)
        {
            Queue<AlarmData> data_queue = loadAlarm();
            AlarmData data;

            int count = 1;
            for (int i = data_queue.Count; i > 0; i--, count++)
            {
                data = data_queue.Dequeue();
                if (data.day_Week == _day_week && data.hour == _hour && data.minute == _minute && data.content == _content)
                {
                    MainWindow.FileIO.eraseData("Alarm List.txt", count);
                }
            }
        }
        public static Queue<AlarmData> loadAlarm() // Alarm List.txt 파일에서 값 불러오기
        {
            Queue<AlarmData> dataQueue = new Queue<AlarmData>();
            Queue<String> lineQueue = new Queue<String>();
            try
            {
                lineQueue = MainWindow.FileIO.readData("Alarm List.txt");
                for (int i = lineQueue.Count; i > 0; i--)
                {
                    AlarmData alarmData_Part = new AlarmData();
                    string line = lineQueue.Dequeue();
                    string[] token = line.Split(new char[] { ':' });

                    alarmData_Part.day_Week = token[0];
                    alarmData_Part.hour = Int32.Parse(token[1]);
                    alarmData_Part.minute = Int32.Parse(token[2]);
                    alarmData_Part.content = token[3];
                    dataQueue.Enqueue(alarmData_Part);
                }
            }
            catch(Exception exception)
            {

            }

            return dataQueue;
        }
        public bool checkAlarm() // 설정된 시각과 현재 시각을 비교하는 함수
        {
            Queue<AlarmData> data = alarmData;
            AlarmData data_Part;

            for(int i = data.Count; i > 0; i--)
            {
                data_Part = data.Dequeue();
                if (data_Part.day_Week == MainWindow.TimeManager.day_Week && data_Part.hour == MainWindow.TimeManager.hour_meridiem && data_Part.minute == MainWindow.TimeManager.minute)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Event
        private void changeMeridiem(object sender, RoutedEventArgs e) // 오전,오후 설정을 바꿔주는 기능
        {
            if (Equals(meridiemButton.Content, "A.M"))
                meridiemButton.Content = "P.M";
            else
                meridiemButton.Content = "A.M";
        }

        private void confirm_Button_Click(object sender, RoutedEventArgs e) // 저장 버튼을 눌렀을때 실행되는 함수
        {
            string alarmDay = date;
            int alarmHour = Int32.Parse(this.hourInput.Content as string);
            int alarmMinute = Int32.Parse(this.minuteInput.Content as string);
            string alarmMeridiem = this.meridiemButton.Content.ToString();
            string content = alarm.Text;

            if (alarmMeridiem.Equals("P.M"))
                alarmHour += 12;

            MainWindow.FileIO.appendText("Alarm List.txt", alarmDay.ToString() + ':' + alarmHour.ToString() + ':' + alarmMinute.ToString() + ':' + content);

            this.Close();

            Alarm_list window = new Alarm_list();
            dragwindow.SetWindow(window);
            dragwindow.SetDrag(window.t_alarm_list, true);
            window.Show();
        }
        #endregion

        private void date_button_click(object sender, RoutedEventArgs e)
        {
            if (sender == sun) date = "일요일";
            else if (sender == mon) date = "월요일";
            else if (sender == tue) date = "화요일";
            else if (sender == wed) date = "수요일";
            else if (sender == thu) date = "목요일";
            else if (sender == fri) date = "금요일";
            else if (sender == sat) date = "토요일";
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            Alarm_list window = new Alarm_list();
            dragwindow.SetWindow(window);
            dragwindow.SetDrag(window.t_alarm_list, true);
            window.Show();
        }

        private void hour_up_button_click(object sender, RoutedEventArgs e)
        {
            int number = Int32.Parse(hourInput.Content as string);
            if (++number >= 12) number = 1;
            hourInput.Content = number.ToString();
        }

        private void hour_down_button_click(object sender, RoutedEventArgs e)
        {
            int number = Int32.Parse(hourInput.Content as string);
            if (--number <= 0) number = 12;
            hourInput.Content = number.ToString();
        }

        private void minute_up_button_click(object sender, RoutedEventArgs e)
        {
            int number = Int32.Parse(minuteInput.Content as string);
            if (++number >= 59) number = 0;
            minuteInput.Content = number.ToString();
        }

        private void minute_down_button_click(object sender, RoutedEventArgs e)
        {
            int number = Int32.Parse(minuteInput.Content as string);
            if (--number <= 0) number = 59;
            minuteInput.Content = number.ToString();
        }

        private void hour_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void minute_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void music_button_click(object sender, RoutedEventArgs e)
        {

        }

        #region Alarm KeyBoard
        OnKeyBoard kc = new OnKeyBoard();
        DispatcherTimer m;
        private void Click_TextBox(object sender, MouseButtonEventArgs e)
        {
            if (alarm.Focusable == false)
            {
                alarm.Focusable = true;
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
                alarm.Focusable = false;
                kc.processstate = 100;
                m.Stop();
            }
        }
        #endregion

    }

    public class AlarmData
    {
        public string day_Week;
        public int hour, minute;
        public string content;
        public string music;
        public int volume;
    }
}
