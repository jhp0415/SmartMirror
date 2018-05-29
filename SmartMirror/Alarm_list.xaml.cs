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
    /// Alarm_list.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Alarm_list : Window
    {
        #region Field
        Queue<AlarmData> alarm_queue = new Queue<AlarmData>();
        AlarmData selected_alarm = new AlarmData();

        bool select_trigger = false;
        #endregion

        public Alarm_list()
        {
            InitializeComponent();

            startTimer();
        }

        private void startTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += tickEvent;
            timer.Start();
        }

        private void tickEvent(object sender, EventArgs e)
        {
            // Update Alarm List
            alarm_queue = AlarmPage_Add.loadAlarm();
            alarm_list.Items.Clear();
            for (int i = alarm_queue.Count; i > 0; i--)
            {
                AlarmData buffer = alarm_queue.Dequeue();
                string output_string = "";
                output_string += buffer.day_Week + " ";
                output_string += buffer.hour + "시 ";
                output_string += buffer.minute + "분 ";
                output_string += buffer.content;

                alarm_list.Items.Add(output_string);
            }
        }

        private void alarm_add(object sender, MouseButtonEventArgs e)
        {
            AlarmPage_Add window = new AlarmPage_Add();
            dragwindow.SetWindow(window);
            dragwindow.SetDrag(window.t_alarm_add, true);
            window.Show();
            this.Close();
            MainPage.alarm_window_trigger = true;
        }

        private void cancel(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            MainPage.alarm_window_trigger = true;
        }

        private void target_update(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string buffer = e.AddedItems[0] as string;
                string[] extendedbuffer;
                extendedbuffer = buffer.Split(new char[] { ' ' });

                selected_alarm.day_Week = extendedbuffer[0];
                selected_alarm.hour = Int32.Parse(extendedbuffer[1].Split(new char[] { '시' })[0]);
                selected_alarm.minute = Int32.Parse(extendedbuffer[2].Split(new char[] { '분' })[0]);
                selected_alarm.content = extendedbuffer[3];
                
                select_trigger = true;
            }
            catch(Exception exception)
            {

            }
        }

        private void alarm_delete(object sender, MouseButtonEventArgs e)
        {
            if(select_trigger) AlarmPage_Add.eraseAlarm(selected_alarm.day_Week, selected_alarm.hour, selected_alarm.minute, selected_alarm.content);
            select_trigger = false;
        }
    }
}