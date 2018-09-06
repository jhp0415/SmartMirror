using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Threading;

namespace SmartMirror
{
    public class OnKeyBoard
    {
        public string path = AppDomain.CurrentDomain.BaseDirectory + "../../clickey32b/Clickey.exe";
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        public int processstate = 100;
        DispatcherTimer th = new DispatcherTimer();

        
        public void practiceKeyBoard()
        {
            p.StartInfo.FileName = path;
            p.EnableRaisingEvents = true;
            p.Exited += Return_ExitCode;
            p.Start();
        }
        public void Return_ExitCode(object sender, EventArgs e)
        {
            processstate = p.ExitCode;
            th.Stop();
        }
    }
}
