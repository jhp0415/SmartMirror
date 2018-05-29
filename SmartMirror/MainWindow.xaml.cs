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
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public static MainWindow a;
        #region Constructor
        public MainWindow()
        {
            
        }
        #endregion

        #region LibraryClass
        public static class FileIO
        {
            #region Field
            public static Queue<String> dataStack;
            #endregion

            #region Method
            /* erase content on file named '_name' & write a line of text on it 
                <input>
                    - _name: Name of file to be written on
                    - _data: String to be written on file
             */
            public static void writeText(String _name, String _data) // _name: 저장 파일명, _data: 파일에 들어갈 문장
            {
                FileStream fileStream;
                StreamWriter streamWriter;

                try
                {
                    fileStream = new FileStream(_name, FileMode.Create);
                    streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8);

                    streamWriter.WriteLine(_data);
                    streamWriter.Flush();

                    streamWriter.Close();
                    fileStream.Close();
                }
                catch (Exception fileException_Write)
                {
                    //MessageBox.Show("파일을 쓰는데 문제가 발생하였습니다.\n" + fileException_Write.Message);
                    return;
                }
            }

            /* write a line of text on file named '_name' at end of it
                <input>
                    - _name: Name of file to be written on
                    - _data: String to be written on file
             */
            public static void appendText(String _name, String _data) // _name: 저장 파일명, _data: 파일에 들어갈 문장
            {
                FileStream fileStream;
                StreamWriter streamWriter;

                try
                {
                    fileStream = new FileStream(_name, FileMode.Append);
                    streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8);

                    streamWriter.WriteLine(_data);
                    streamWriter.Flush();

                    streamWriter.Close();
                    fileStream.Close();
                }
                catch (Exception fileException_append)
                {
                    //MessageBox.Show("파일을 쓰는데 문제가 발생하였습니다.\n" + fileException_append.Message);
                    return;
                }
            }

            /* read each lines of file named '_name' and store them in Queue
                <Input>
                    - String _name: Name of file to be read
                <output>
                    - Queue<String> dataStack: Queue storing a set of text line
            */
            public static Queue<String> readData(String _name) // _name: 읽을 파일명
            {
                FileStream fileStream;
                StreamReader streamReader;

                dataStack = new Queue<String>();
                try
                {
                    fileStream = new FileStream(_name, FileMode.OpenOrCreate);
                    streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8);

                    streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    while (streamReader.Peek() > -1)
                        dataStack.Enqueue(streamReader.ReadLine());

                    fileStream.Close();
                    streamReader.Close();
                }
                catch (Exception fileException_Read)
                {
                    //MessageBox.Show("파일을 읽는데 문제가 발생하였습니다.\n" + fileException_Read.Message);
                    return null;
                }

                return dataStack;
            }

            public static void eraseData(String _name, int _line)
            {
                Queue<string> data_queue = readData(_name);
                File.Delete(_name);

                int count = 1;
                for (int i = data_queue.Count; i > 0; i--, count++)
                {
                    string buffer = data_queue.Dequeue();
                    if (count != _line) appendText(_name, buffer);
                }
            }
            #endregion
        }
        public static class TimeManager
        {
            #region Field
            public static string day_Week;
            public static int year, month, day;
            public static int hour, minute, second;
            public static int hour_meridiem;
            public static string meridiem;
            #endregion

            #region Method
            public static void updateTime()
            {
                DateTime dateTime;
                try
                {
                    currentDate(out year, out month, out day);
                    currentTime(out hour, out minute, out second, out hour_meridiem, out meridiem);
                    dateTime = new DateTime(year, month, day);
                    day_Week = dateTime.ToString("dddd");
                }catch(Exception exception)
                {

                }
            }

            public static void currentDate(out int _year, out int _month, out int _day)
            {
                _year = Int32.Parse(DateTime.Now.ToString("yyyy"));
                _month = Int32.Parse(DateTime.Now.ToString("MM"));
                _day = Int32.Parse(DateTime.Now.ToString("dd"));
            }
            public static void currentTime(out int _hour, out int _minute, out int _second)
            {
                _hour = Int32.Parse(DateTime.Now.ToString("HH"));
                _minute = Int32.Parse(DateTime.Now.ToString("mm"));
                _second = Int32.Parse(DateTime.Now.ToString("ss"));
            }
            public static void currentTime(out int _hour_meridiem, out int _minute, out int _second, out string _meridiem)
            {
                _hour_meridiem = Int32.Parse(DateTime.Now.ToString("hh"));
                _minute = Int32.Parse(DateTime.Now.ToString("mm"));
                _second = Int32.Parse(DateTime.Now.ToString("ss"));

                int hour_meridiem = Int32.Parse(DateTime.Now.ToString("HH"));
                if (hour < 12) _meridiem = "am";
                else _meridiem = "pm";
            }
            public static void currentTime(out int _hour, out int _minute, out int _second, out int _hour_meridiem, out string _meridiem)
            {
                _hour = Int32.Parse(DateTime.Now.ToString("hh"));
                _minute = Int32.Parse(DateTime.Now.ToString("mm"));
                _second = Int32.Parse(DateTime.Now.ToString("ss"));

                _hour_meridiem = Int32.Parse(DateTime.Now.ToString("HH"));
                if (_hour_meridiem < 12) _meridiem = "am";
                else _meridiem = "pm";
            }
            #endregion
        }
        public static class ScheduleManager
        {
            #region DataType
            public struct ScheduleUnit
            {
                public int year, month, day;
                //public int hour, minute;
                public string schedule;
            };
            #endregion

            #region Field
            public static ScheduleUnit scheduleUnit;
            #endregion

            #region Method
            public static void storeSchedule(int _year, int _month, int _day, string _schedule)
            {
                try
                {
                    FileIO.appendText("Schedule List.txt", _year.ToString() + '.' + _month.ToString() + '.' + _day.ToString() + ' ' + _schedule);
                }
                catch (Exception scheduleException_Store)
                {
                    MessageBox.Show("일정을 저장하는데 문제가 발생하였습니다.\n" + scheduleException_Store.Message);
                }
            }
            public static bool eraseSchedule(int _year, int _month, int _day, string _schedule)
            {
                Queue<String> scheduleQueue = FileIO.readData("Schedule List.txt");
                string buffer = "";
                string[] bufferToken;
                string[] extendedBuffer;

                int index = 1;
                for (int i = scheduleQueue.Count; i > 0; i--)
                {
                    try
                    {
                        buffer = scheduleQueue.Dequeue(); // yyyy.MM.dd HH:mm:tt [Work to do]
                        bufferToken = buffer.Split(new char[] { ' ' });

                        // 날짜 저장
                        extendedBuffer = bufferToken[0].Split(new char[] { '.' });
                        scheduleUnit.year = Int32.Parse(extendedBuffer[0]);
                        scheduleUnit.month = Int32.Parse(extendedBuffer[1]);
                        scheduleUnit.day = Int32.Parse(extendedBuffer[2]);

                        scheduleUnit.schedule = buffer.Remove(0, bufferToken[0].Length + 1);
                        if (scheduleUnit.year == _year && scheduleUnit.month == _month && scheduleUnit.day == _day && scheduleUnit.schedule == _schedule)
                        {
                            FileIO.eraseData("Schedule List.txt", index);
                        }
                        index++;
                    }
                    catch (Exception scheduleException_Load)
                    {
                        //MessageBox.Show("일정을 가져오는데 문제가 발생하였습니다.\n" + scheduleException_Load.Message);
                        return false;
                    }
                }

                return true;
            }

            public static Queue<ScheduleUnit> loadSchedule()
            {
                Queue<String> scheduleQueue = FileIO.readData("Schedule List.txt");
                Queue<ScheduleUnit> scheduleUnitQueue_Return = new Queue<ScheduleUnit>();
                string buffer = "";
                string[] bufferToken;
                string[] extendedBuffer;
                try
                {
                    for (int i = scheduleQueue.Count; i > 0; i--)
                    {

                        buffer = scheduleQueue.Dequeue(); // yyyy.MM.dd HH:mm:tt [Work to do]
                        bufferToken = buffer.Split(new char[] { ' ' });

                        // 날짜 저장
                        extendedBuffer = bufferToken[0].Split(new char[] { '.' });
                        scheduleUnit.year = Int32.Parse(extendedBuffer[0]);
                        scheduleUnit.month = Int32.Parse(extendedBuffer[1]);
                        scheduleUnit.day = Int32.Parse(extendedBuffer[2]);

                        scheduleUnit.schedule = buffer.Remove(0, bufferToken[0].Length + 1);
                        scheduleUnitQueue_Return.Enqueue(scheduleUnit);
                    }
                }
                catch (Exception scheduleException_Load)
                {
                    //MessageBox.Show("일정을 가져오는데 문제가 발생하였습니다.\n" + scheduleException_Load.Message);
                    return null;
                }

                return scheduleUnitQueue_Return;
            }
            public static Queue<ScheduleUnit> loadSchedule(int _year) //<'_year'년도>에 있는 일정들을 반환한다.
            {
                Queue<String> scheduleQueue = FileIO.readData("Schedule List.txt");
                Queue<ScheduleUnit> scheduleUnitQueue_Return = new Queue<ScheduleUnit>();
                string buffer = "";
                string[] bufferToken;
                string[] extendedBuffer;

                for (int i = scheduleQueue.Count; i > 0; i--)
                {
                    try
                    {
                        buffer = scheduleQueue.Dequeue(); // yyyy.MM.dd HH:mm:tt [Work to do]
                        bufferToken = buffer.Split(new char[] { ' ' });

                        // 날짜 저장
                        extendedBuffer = bufferToken[0].Split(new char[] { '.' });
                        scheduleUnit.year = Int32.Parse(extendedBuffer[0]);
                        scheduleUnit.month = Int32.Parse(extendedBuffer[1]);
                        scheduleUnit.day = Int32.Parse(extendedBuffer[2]);

                        scheduleUnit.schedule = buffer.Remove(0, bufferToken[0].Length + 1);
                        if (scheduleUnit.year == _year)
                            scheduleUnitQueue_Return.Enqueue(scheduleUnit);
                    }
                    catch (Exception scheduleException_Load)
                    {
                        //MessageBox.Show("일정을 가져오는데 문제가 발생하였습니다.\n" + scheduleException_Load.Message);
                        return null;
                    }
                }

                return scheduleUnitQueue_Return;
            }
            public static Queue<ScheduleUnit> loadSchedule(int _year, int _month) //<'_year'년도 '_month'월>에 있는 일정들을 반환한다.
            {
                Queue<String> scheduleQueue = FileIO.readData("Schedule List.txt");
                Queue<ScheduleUnit> scheduleUnitQueue_Return = new Queue<ScheduleUnit>();
                string buffer = "";
                string[] bufferToken;
                string[] extendedBuffer;

                for (int i = scheduleQueue.Count; i > 0; i--)
                {
                    try
                    {
                        buffer = scheduleQueue.Dequeue(); // yyyy.MM.dd HH:mm:tt [Work to do]
                        bufferToken = buffer.Split(new char[] { ' ' });

                        // 날짜 저장
                        extendedBuffer = bufferToken[0].Split(new char[] { '.' });
                        scheduleUnit.year = Int32.Parse(extendedBuffer[0]);
                        scheduleUnit.month = Int32.Parse(extendedBuffer[1]);
                        scheduleUnit.day = Int32.Parse(extendedBuffer[2]);

                        scheduleUnit.schedule = buffer.Remove(0, bufferToken[0].Length + 2);
                        if (scheduleUnit.year == _year && scheduleUnit.month == _month)
                            scheduleUnitQueue_Return.Enqueue(scheduleUnit);
                    }
                    catch (Exception scheduleException_Load)
                    {
                        MessageBox.Show("일정을 가져오는데 문제가 발생하였습니다.\n" + scheduleException_Load.Message);
                        return null;
                    }
                }

                return scheduleUnitQueue_Return;
            }
            public static Queue<ScheduleUnit> loadSchedule(int _year, int _month, int _day) //<'_year'년도 '_month'월 '_day'일>에 있는 일정들을 반환한다.
            {
                Queue<String> scheduleQueue = FileIO.readData("Schedule List.txt");
                Queue<ScheduleUnit> scheduleUnitQueue_Return = new Queue<ScheduleUnit>();
                string buffer = "";
                string[] bufferToken;
                string[] extendedBuffer;

                for (int i = scheduleQueue.Count; i > 0; i--)
                {
                    try
                    {
                        buffer = scheduleQueue.Dequeue(); // yyyy.MM.dd HH:mm:tt [Work to do]
                        bufferToken = buffer.Split(new char[] { ' ' });

                        // 날짜 저장
                        extendedBuffer = bufferToken[0].Split(new char[] { '.' });
                        scheduleUnit.year = Int32.Parse(extendedBuffer[0]);
                        scheduleUnit.month = Int32.Parse(extendedBuffer[1]);
                        scheduleUnit.day = Int32.Parse(extendedBuffer[2]);

                        scheduleUnit.schedule = buffer.Remove(0, bufferToken[0].Length + 1);
                        if (scheduleUnit.year == _year && scheduleUnit.month == _month && scheduleUnit.day == _day)
                            scheduleUnitQueue_Return.Enqueue(scheduleUnit);
                    }
                    catch (Exception scheduleException_Load)
                    {
                        MessageBox.Show("일정을 가져오는데 문제가 발생하였습니다.\n" + scheduleException_Load.Message);
                        return null;
                    }
                }

                return scheduleUnitQueue_Return;
            }
            //public static Queue<ScheduleUnit> loadSchedule(int _year, int _month, int _day, int _hour) //<'_year'년 '_month'월 '_day'일 '_hour'시>에 있는 일정들을 반환한다.
            //{
            //    Queue<String> scheduleQueue = FileIO.readData("Schedule List.txt");
            //    Queue<ScheduleUnit> scheduleUnitQueue_Return = new Queue<ScheduleUnit>();
            //    string buffer = "";
            //    string[] bufferToken;
            //    string[] extendedBuffer;

            //    for (int i = scheduleQueue.Count; i > 0; i--)
            //    {
            //        try
            //        {
            //            buffer = scheduleQueue.Dequeue(); // yyyy.MM.dd HH:mm:tt [Work to do]
            //            bufferToken = buffer.Split(new char[] { ' ' });

            //            // 날짜 저장
            //            extendedBuffer = bufferToken[0].Split(new char[] { '.' });
            //            scheduleUnit.year = Int32.Parse(extendedBuffer[0]);
            //            scheduleUnit.month = Int32.Parse(extendedBuffer[1]);
            //            scheduleUnit.day = Int32.Parse(extendedBuffer[2]);

            //            extendedBuffer = bufferToken[1].Split(new char[] { ':' });
            //            scheduleUnit.hour = Int32.Parse(extendedBuffer[0]);
            //            scheduleUnit.minute = Int32.Parse(extendedBuffer[1]);

            //            scheduleUnit.schedule = buffer.Remove(0, bufferToken[0].Length + bufferToken[1].Length + 2);
            //            if (scheduleUnit.year == _year && scheduleUnit.month == _month && scheduleUnit.day == _day && scheduleUnit.hour == _hour)
            //                scheduleUnitQueue_Return.Enqueue(scheduleUnit);
            //        }
            //        catch (Exception scheduleException_Load)
            //        {
            //            MessageBox.Show("일정을 가져오는데 문제가 발생하였습니다.\n" + scheduleException_Load.Message);
            //            return null;
            //        }
            //    }

            //    return scheduleUnitQueue_Return;
            //}
            //public static Queue<ScheduleUnit> loadSchedule(int _year, int _month, int _day, int _hour, int _minute) //<'_year'년 '_month'월 '_day'일 '_hour'시>에 있는 일정들을 반환한다.
            //{
            //    Queue<String> scheduleQueue = FileIO.readData("Schedule List.txt");
            //    Queue<ScheduleUnit> scheduleUnitQueue_Return = new Queue<ScheduleUnit>();
            //    string buffer = "";
            //    string[] bufferToken;
            //    string[] extendedBuffer;

            //    for (int i = scheduleQueue.Count; i > 0; i--)
            //    {
            //        try
            //        {
            //            buffer = scheduleQueue.Dequeue(); // yyyy.MM.dd HH:mm:tt [Work to do]
            //            bufferToken = buffer.Split(new char[] { ' ' });

            //            // 날짜 저장
            //            extendedBuffer = bufferToken[0].Split(new char[] { '.' });
            //            scheduleUnit.year = Int32.Parse(extendedBuffer[0]);
            //            scheduleUnit.month = Int32.Parse(extendedBuffer[1]);
            //            scheduleUnit.day = Int32.Parse(extendedBuffer[2]);

            //            extendedBuffer = bufferToken[1].Split(new char[] { ':' });
            //            scheduleUnit.hour = Int32.Parse(extendedBuffer[0]);
            //            scheduleUnit.minute = Int32.Parse(extendedBuffer[1]);

            //            scheduleUnit.schedule = buffer.Remove(0, bufferToken[0].Length + bufferToken[1].Length + 2);
            //            if (scheduleUnit.year == _year && scheduleUnit.month == _month && scheduleUnit.day == _day && scheduleUnit.hour == _hour && scheduleUnit.minute == _minute)
            //                scheduleUnitQueue_Return.Enqueue(scheduleUnit);
            //        }
            //        catch (Exception scheduleException_Load)
            //        {
            //            MessageBox.Show("일정을 가져오는데 문제가 발생하였습니다.\n" + scheduleException_Load.Message);
            //            return null;
            //        }
            //    }

            //    return scheduleUnitQueue_Return;
            //}
            #endregion
        }
        #endregion
    }
}
