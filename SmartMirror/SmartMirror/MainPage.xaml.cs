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
    /// MainPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainPage : Page
    {
        #region Field
        Stopwatch stopwatch = new Stopwatch();
        music_player mp = new music_player();

        MainWindow.ScheduleManager.ScheduleUnit selected_schedule = new MainWindow.ScheduleManager.ScheduleUnit();
        bool schedule_select = false;

        public static bool alarm_window_trigger = true;
        public static bool news_trigger = true;
        private int last_minute;
        private bool alarm_trigger = true;
        public static int a = 0;
        #endregion

        #region Constructor
        public MainPage()
        {
            InitializeComponent();

            #region TimeManagement
            MainWindow.TimeManager.updateTime();
            last_minute = MainWindow.TimeManager.minute;
            startTimer();
            #endregion

            #region ScheduleManagement
            Queue<MainWindow.ScheduleManager.ScheduleUnit> scheduleUnitQueue;
            scheduleUnitQueue = MainWindow.ScheduleManager.loadSchedule(MainWindow.TimeManager.year, MainWindow.TimeManager.month, MainWindow.TimeManager.day);
            for (int i = scheduleUnitQueue.Count; i > 0; i--)
            {
                MainWindow.ScheduleManager.ScheduleUnit scheduleUnit = scheduleUnitQueue.Dequeue();

                //real_schedule_list.Items.Add(scheduleUnit.schedule);
                //ListBoxItem asd = real_schedule_list.Items[0] as ListBoxItem;
                //asd.BorderBrush = Brushes.Red;


            }
            #endregion
            Loaded += new RoutedEventHandler(window_load);
        }
        #endregion

        #region Method
        private void startTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += tickEvent;
            timer.Start();
        }
        #endregion // Method

        #region Event
        #region TimerEvent
        private void tickEvent(object sender, EventArgs e)
        {
            // Set current time
            MainWindow.TimeManager.updateTime();

            // Display current time on MainWindow
            dateDisplay.Text = MainWindow.TimeManager.year.ToString() + '-' + MainWindow.TimeManager.month.ToString() + '-' + MainWindow.TimeManager.day.ToString();
            timeDisplay.Text = MainWindow.TimeManager.meridiem.ToUpper() + ' ' + MainWindow.TimeManager.hour.ToString() + ':' + MainWindow.TimeManager.minute.ToString() + ':' + MainWindow.TimeManager.second.ToString();

            if(MainWindow.TimeManager.minute != last_minute)
            {
                last_minute = MainWindow.TimeManager.minute;
                alarm_trigger = true;
            }

            // AlarmPage_Setting
            AlarmPage_Add alarmPage = new AlarmPage_Add();
            if (alarmPage.checkAlarm() && alarm_trigger)
            {
                Alarm_confirm window = new Alarm_confirm();
                window.alarm_content.Text = MainWindow.TimeManager.meridiem.ToUpper()+"  "+ MainWindow.TimeManager.hour.ToString()+" 시 "+ MainWindow.TimeManager.minute.ToString()+" 분";
                dragwindow.SetWindow(window);
                dragwindow.SetDrag(window.t_alarm_confirm, true);
                window.Show();



                alarm_trigger = false;
            }

            // Schedule Event
            scheduleList.Items.Clear();
            Queue<MainWindow.ScheduleManager.ScheduleUnit> schedule_queue =  MainWindow.ScheduleManager.loadSchedule();
            for(int i = schedule_queue.Count; i > 0; i--)
            {
                MainWindow.ScheduleManager.ScheduleUnit data = schedule_queue.Dequeue();
                string output_string = "";
                output_string += data.year + "년 ";
                output_string += data.month + "월 ";
                output_string += data.day + "일\n";
                output_string += " " + data.schedule;
                scheduleList.Items.Add(output_string);
            }
            if(a==1)
            {
                Drag.SetWindow(this);
                Drag.SetDrag(music, true);
            }
            else if(a==0)
            {
                Drag.SetWindow(this);
                Drag.SetDrag(music, false);
            }
        }
        #endregion
        #region ApplicationEvent
        private void clockEvent_Up(object sender, MouseButtonEventArgs e)//마우스로 시계를 클릭하는경우 실행하는 함수
        {
            // 시계 설정 창으로 이동
        }
        // 달력의 날짜가 클릭되었을 때 <일정 추가 창>으로 이동
        private void clickEvent_Schedule(object sender, SelectionChangedEventArgs e)
        {
            String time = e.AddedItems[0].ToString().Remove(10, 12); // 클릭된 날짜 정보 받아오기

            int year = Int32.Parse(time.Remove(4, 6)); time = time.Remove(0, 5); // 년도
            int month = Int32.Parse(time.Remove(2, 3)); time = time.Remove(0, 3); // 월
            int day = Int32.Parse(time); // 일

            SchedulePage_Add window = new SchedulePage_Add(year, month, day);
            dragwindow.SetWindow(window);
            dragwindow.SetDrag(window.t_schedule, true);
            window.Show();
        }

        #region IconEvent
        private void clockIconEvent_Down(object sender, MouseButtonEventArgs e) // 마우스 눌렀을 때 실행되는 이벤트
        {
            if (alarm_window_trigger)
            {
                Alarm_list window = new Alarm_list();
                dragwindow.SetWindow(window);
                dragwindow.SetDrag(window.t_alarm_list, true);
                window.Show();
                alarm_window_trigger = false;
            }
        }
        //// 시계 세팅 창으로 이동
        //private void clockIconEvent_Up(object sender, MouseButtonEventArgs e) // 마우스 땠을 때 실행되는 이벤트
        //{
        //    stopwatch.Stop();
        //    if (stopwatch.ElapsedMilliseconds < 200)
        //    {
        //        if (dateDisplay.Visibility == (Visibility)1) dateDisplay.Visibility = (Visibility)0;
        //        else if (dateDisplay.Visibility == (Visibility)0) dateDisplay.Visibility = (Visibility)1;

        //        if (timeDisplay.Visibility == (Visibility)1) timeDisplay.Visibility = (Visibility)0;
        //        else if (timeDisplay.Visibility == (Visibility)0) timeDisplay.Visibility = (Visibility)1;
        //    }
        //    else
        //    {
        //        //Clock로 이동
        //        AlarmPage_Add page = new AlarmPage_Add();
        //        NavigationService.Navigate(page);
        //    }
        //}
        private void calendarIconEvent_Down(object sender, MouseButtonEventArgs e)
        {
            stopwatch.Reset();
            stopwatch.Start();
        }
        // 달력 세팅 창으로 이동
        private void calendarIconEvent_Up(object sender, MouseButtonEventArgs e)
        {
            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds < 200)
            {
                if (Calendar.Visibility == (Visibility)1) Calendar.Visibility = (Visibility)0;
                else if (Calendar.Visibility == (Visibility)0) Calendar.Visibility = (Visibility)1;
            }
            else
            {
                //CalendarPage_Setting page = new CalendarPage_Setting();
                //NavigationService.Navigate(page);
            }
        }
        private void musicIconEvent_Down(object sender, MouseButtonEventArgs e)
        {
            stopwatch.Reset();
            stopwatch.Start();
        }
        // 음악 세팅 창으로 이동
        private void musicIconEvent_Up(object sender, MouseButtonEventArgs e)
        {
            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds < 200)
            {
                if (music.Visibility == (Visibility)1)
                    music.Visibility = (Visibility)0;
                else if(music.Visibility == (Visibility)0)
                    music.Visibility = (Visibility)1;
            }
            else
            {
                //MessageBox.Show("제작 중");
                //MusicPage_Setting page = new CalendarPage_Setting();
                //NavigationService.Navigate(page);
            }
        }
        //음악 위젯 이동
        private void musicEvent_down(object sender, MouseButtonEventArgs e)
        {
            stopwatch.Reset();
            stopwatch.Start();
        }
        private void musicEvent_up(object sender, MouseButtonEventArgs e)
        {
            if (stopwatch.ElapsedMilliseconds < 200)
            {
                
            }
        }
        private void weatherIcon_EventUp(object sender, MouseButtonEventArgs e)
        {
            if (weather.Visibility == (Visibility)1)
                weather.Visibility = (Visibility)0;
            else if (weather.Visibility == (Visibility)0)
                weather.Visibility = (Visibility)1;
        }
        private void scheduleIcon_EventUp(object sender, MouseButtonEventArgs e)
        {
            if (schedule_list.Visibility == (Visibility)1)
                schedule_list.Visibility = (Visibility)0;
            else if (schedule_list.Visibility == (Visibility)0)
                schedule_list.Visibility = (Visibility)1;
        }

        #endregion // IconEvent

        #endregion // ApplicationEvent

        #endregion // TimerEvent

        private void click_schedule(object sender, SelectionChangedEventArgs e)
        {
            string buffer;
            string[] extended_buffer;
            try
            {
                buffer = e.AddedItems[0].ToString();
                extended_buffer = buffer.Split(new char[] { '\n', ' ' });
                selected_schedule.year = Int32.Parse(extended_buffer[0].Split(new char[] { '년' })[0]);
                selected_schedule.month = Int32.Parse(extended_buffer[1].Split(new char[] { '월' })[0]);
                selected_schedule.day = Int32.Parse(extended_buffer[2].Split(new char[] { '일' })[0]);
                selected_schedule.schedule = extended_buffer[4];
                
                schedule_select = true;
            }
            catch (Exception exception)
            {

            }
        }

        private void schedule_delete(object sender, RoutedEventArgs e)
        {
            if (schedule_select)
            {
                MainWindow.ScheduleManager.eraseSchedule(selected_schedule.year, selected_schedule.month, selected_schedule.day, selected_schedule.schedule);
            }
            schedule_select = false;
        }

        private void menu_icon_click(object sender, RoutedEventArgs e)
        {
            if (icon_menu.Visibility == (Visibility)1) icon_menu.Visibility = (Visibility)0;
            else if (icon_menu.Visibility == (Visibility)0) icon_menu.Visibility = (Visibility)1;
        }
        /*
        private void memo_icon_click(object sender, MouseButtonEventArgs e)
        {
            Memo m=new Memo(sender,e);
            m.Width = 300;
            m.Height = 300;
            m.Show();
           
        }
        */
        #region MemoManager
        public void window_load(object sender, RoutedEventArgs e)
        {
            Drag.SetWindow(this);
            Drag.SetDrag(mymemo, true);
   
        }
        private void memo_icon_click(object sender, MouseButtonEventArgs e)
        {
            if (mymemo.Visibility == (Visibility)0) mymemo.Visibility = (Visibility)1;
            else if (mymemo.Visibility == (Visibility)1) mymemo.Visibility = (Visibility)0;

        }
        #endregion
        #region 움직임고정
       

        private void weather_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            stopwatch.Reset();
            stopwatch.Start();
        }

        private void weather_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (stopwatch.ElapsedMilliseconds > 200)
            {
                Drag.SetWindow(this);
                Drag.SetDrag(weather, true);
            }
            else
            {
                Drag.SetWindow(this);
                Drag.SetDrag(weather, false);
            }
        }
        #endregion

        #region Memo KeyBoard
        OnKeyBoard kc = new OnKeyBoard();
        DispatcherTimer m;

        private void timer_memo(object sender, MouseButtonEventArgs e)
        {
            stopwatch.Reset();
            stopwatch.Start();
        }

        private void Click_TextBox(object sender, MouseButtonEventArgs e)
        {
            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds < 200)
            {
                if (mymemo.Focusable == false)
                {
                    mymemo.Focusable = true;
                    kc.practiceKeyBoard();
                }
                m = new DispatcherTimer();
                m.Interval = TimeSpan.FromMilliseconds(20);
                m.Start();
                m.Tick += Check_State;
            }
        }

        private void Check_State(object sender, EventArgs e)
        {
            if (kc.processstate != 100)
            {
                //mymemo.Text += kc.processstate.ToString();
                mymemo.Focusable = false;
                kc.processstate = 100;
                m.Stop();
            }   
        }
        #endregion


        int bg_tr = 0;//0이면배경,1이면투명
        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bg_tr == 0)
            {
                dd.Background.Opacity = 0;
                bg_tr = 1;
            }
            else if (bg_tr == 1)
            {
                dd.Background.Opacity = 255;
                bg_tr = 0;
            }
        }
        private static NewsWindow window = new NewsWindow();
        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            
            if (news_trigger)
            {
                window = new NewsWindow();
                dragwindow.SetWindow(window);
                dragwindow.SetDrag(window.t_news, true);
                window.Show();
                news_trigger = false;
            }
            else if(news_trigger==false)
            {
                window.Close();
                news_trigger=true;
            }
        }
    }
}
