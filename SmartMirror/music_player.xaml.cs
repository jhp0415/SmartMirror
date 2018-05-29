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
using System.Diagnostics;

////////////추가
using System.IO;                            //모든 입출력 클래스들 포함되어 있음
using System.Windows.Threading;


namespace SmartMirror
{
    /// <summary>
    /// music_player.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class music_player : UserControl
    {
        public string path = @AppDomain.CurrentDomain.BaseDirectory + "../../MusicFiles/";

        public string[] SearchFiles = null;                    //음악경로 저장하는 string

        private bool btnsatate = true;      //버튼 클릭상태 저장
        private int currentindex = 0;
        private int pastindex;
        Stopwatch stopwatch = new Stopwatch();

        int cur_minute;
        int cur_second;

        DispatcherTimer mytimer;
        
        public music_player()
        {
            InitializeComponent();
            PlayList();
        }
        #region Media PlayList
        public void PlayList()
        {
            if (Directory.Exists(path))
            {
                SearchFiles = Directory.GetFiles(path, "*.mp3");  //파일 경로 디렉토리에서 "*.mp3"만 검사하여 가지고 온다.경로로 저장됨
                pastindex = SearchFiles.Length - 1;
                for (int i = 0; i < SearchFiles.Length; i++)//배열의 크기 : 배열이름.Length
                    myplaylist.Items.Add(System.IO.Path.GetFileNameWithoutExtension(SearchFiles[i]));//확장자 제외한 파일 이름추출
            }

            myplaylist.SelectionChanged += new SelectionChangedEventHandler(ListBoxItem_Selected);  //리스트박스 아이템 선택시 이벤트 발생하기
            myplaylist.SelectionChanged += new SelectionChangedEventHandler(Play_Pause);
            music_name.MouseLeftButtonDown += new MouseButtonEventHandler(music_name_MouseLeftButtonDown);
            music_name.MouseLeftButtonUp += new MouseButtonEventHandler(music_name_MouseLeftButtonUp);
        }

        private void ListBoxItem_Selected(object sender, EventArgs e)
        {
            if (myplaylist.SelectedItem != null)
            {
                music_name.Text = System.IO.Path.GetFileNameWithoutExtension(SearchFiles[myplaylist.SelectedIndex]);
                pastindex = currentindex;
                myMedia.Source = new Uri(@SearchFiles[myplaylist.SelectedIndex], UriKind.Relative);     //선택한 미디어로 소스변경하기
                myMedia.LoadedBehavior = MediaState.Manual;
                currentindex = myplaylist.SelectedIndex;
                btnsatate = true;
            }
        }
        #endregion

        #region Play or Pause
        private void Play_Pause(object sender, RoutedEventArgs e)
        {
            //플레이 버튼 클릭시 플레이
            //이미지를 멈춤버튼으로 변경
            //멈춤 이미지 클릭시 음악재생 멈추기 pause_clik실행
            if (btnsatate == true)
            {
                //음악재생
                myMedia.Play();
                Setting_Timer();

                BitmapImage change = new BitmapImage();
                change.BeginInit();
                change.UriSource = new Uri(@"Image\button\pause.png", UriKind.Relative);
                change.EndInit();
                playorpause.Source = change;
                btnsatate = false;
            }

            else if (btnsatate == false)
            {
                //일시정지
                myMedia.Pause();
                mytimer.Stop();     //타이머 정지

                BitmapImage change = new BitmapImage();
                change.BeginInit();
                change.UriSource = new Uri(@"Image\button\play.png", UriKind.Relative);
                change.EndInit();
                playorpause.Source = change;
                btnsatate = true;
            }
        }
        #endregion

        #region Past or Next
        private void Past_Media(object sender, RoutedEventArgs e)
        {
            if (pastindex < 0) pastindex = SearchFiles.Length - 1;         //첫번째곡 -> 마지막곡
            music_name.Text = System.IO.Path.GetFileNameWithoutExtension(SearchFiles[pastindex]);
            myMedia.Source = new Uri(@SearchFiles[pastindex], UriKind.Relative);     //이전에 선택했던 곡 Index
            myMedia.LoadedBehavior = MediaState.Manual;
            myMedia.Play();
            currentindex = pastindex;
            pastindex -= 1;                     //다음에 재생할 이전곡 Index
        }

        private void Next_Media(object sender, RoutedEventArgs e)
        {
            if (currentindex == SearchFiles.Length) currentindex = 0;          //마지막곡 -> 첫번째곡
            music_name.Text = System.IO.Path.GetFileNameWithoutExtension(SearchFiles[currentindex]);
            myMedia.Source = new Uri(@SearchFiles[currentindex], UriKind.Relative);     //플레이리스트 다음곡 Index Number 선택
            myMedia.LoadedBehavior = MediaState.Manual;
            myMedia.Play();
            pastindex = currentindex;
            currentindex += 1;
        }
        #endregion

        #region Volume
        // Change the volume of the media. 볼륨조절
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            myMedia.Volume = volumeSlider.Value;
            vol.Text = ((int)((volumeSlider.Value) * 30)).ToString();
            if (volumeSlider.Value == 0)
            {
                vol.Text = "음소거";
                BitmapImage change = new BitmapImage();
                change.BeginInit();
                change.UriSource = new Uri(@"Image\button\muted.png", UriKind.Relative);
                change.EndInit();
                volimage.Source = change;
            }
            else
            {
                myMedia.Volume = volumeSlider.Value;
                vol.Text = ((int)((volumeSlider.Value) * 30)).ToString();
                BitmapImage change = new BitmapImage();
                change.BeginInit();
                change.UriSource = new Uri(@"Image\button\volume.png", UriKind.Relative);
                change.EndInit();
                volimage.Source = change;
            }

        }

        void InitializePropertyValues()
        {
            // Set the media's starting Volume and SpeedRatio to the current value of the
            // their respective slider controls.
            myMedia.Volume = (int)volumeSlider.Value;
        }
        #endregion

        #region TimeSlider
        private void Element_MediaOpened(object sender, EventArgs e)
        {
            //음악의 전체 재생시간 나타내기
            string total_minute;      //분 시간
            string total_second;      //초 시간

            timelineSlider.Maximum = myMedia.NaturalDuration.TimeSpan.TotalMilliseconds;        //전체 재생시간
            total_minute = ((int)(myMedia.NaturalDuration.TimeSpan.TotalMinutes)).ToString();
            total_second = ((int)(myMedia.NaturalDuration.TimeSpan.TotalSeconds % 60)).ToString();
            totaltime.Text = total_minute + " : " + total_second;

        }
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            //재생바를 끌어서 현재재생위치 가져오기
            //ticktime = 0;
            int SliderValue = (int)timelineSlider.Value;

            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            myMedia.Position = ts;

            cur_minute = ((int)(myMedia.Position.Minutes));
            cur_second = ((int)(myMedia.Position.Seconds % 60));
            currenttime.Text = cur_minute.ToString() + " : " + cur_second.ToString();
           
        }

        private void Setting_Timer()
        {
            //재생바 시간맞추기
            mytimer = new DispatcherTimer();
            mytimer.Stop();
            mytimer.Interval = new TimeSpan(0, 0, 1);
            mytimer.Tick += Count_Timer;
            mytimer.Start();

            DispatcherTimer th = new DispatcherTimer();
            th.Stop();
            th.Interval = new TimeSpan(0, 0, 1);
            th.Tick += Auto_slider;
            th.Start();

        }

        private void Count_Timer(object sender, EventArgs e)
        {
            cur_minute = ((int)(myMedia.Position.Minutes));
            cur_second = ((int)(myMedia.Position.Seconds % 60));
            currenttime.Text = cur_minute.ToString() + " : " + cur_second.ToString(); 
        }

        private void Auto_slider(object sender, EventArgs e)
        {
            timelineSlider.Value = myMedia.Position.TotalMilliseconds;
        }
        #endregion


        #region Auto Play
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            //자동재생
            try {
                myMedia.Stop();

                if (currentindex == SearchFiles.Length - 1)
                    currentindex = -1;
                currentindex++;
                music_name.Text = System.IO.Path.GetFileNameWithoutExtension(SearchFiles[currentindex]);
                myMedia.Source = new Uri(@SearchFiles[currentindex], UriKind.Relative);
                myMedia.LoadedBehavior = MediaState.Manual;

                myMedia.Play();
            }
            catch (Exception)
            {

            }
        }
        #endregion
        
        /*
        #region Replay Media
        private bool Restate = false;
        private void ReplayBtn_Selected(object sender, RoutedEventArgs e)
        {
            //반복재생
            if (Restate == false)
            {
                Restate = true;
                myMedia.MediaEnded += Replay;
            }
            if (Restate == true)
            {
                Restate = false;
                myMedia.MediaEnded += Element_MediaEnded;
            }
            
        }
        private void Replay(object sender, EventArgs e)
        {
            //반복재생
            //버튼이 눌렸으면 반복재생 기능 
            myMedia.Stop();
            myMedia.Position = TimeSpan.FromSeconds(0);

            myMedia.Play();

            
        }
        #endregion
        */

        private void volum_click(object sender, EventArgs e)
        {
            if (volum_slider.Visibility == (Visibility)1) volum_slider.Visibility = (Visibility)0;
            else if (volum_slider.Visibility == (Visibility)0) volum_slider.Visibility = (Visibility)1;
        }

        private void list_click(object sender, RoutedEventArgs e)
        {
            if (playlist.Visibility == (Visibility)1) playlist.Visibility = (Visibility)0;
            else if (playlist.Visibility == (Visibility)0) playlist.Visibility = (Visibility)1;
        }

        private void music_name_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            stopwatch.Reset();
            stopwatch.Start();
        }

        private void music_name_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (stopwatch.ElapsedMilliseconds > 200)
            {
                MainPage.a = 1;//드래그활성화
            }
            else
            {
                MainPage.a = 0;//드래그비활성화
            }
        }
    }
}
