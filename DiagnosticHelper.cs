using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance
{
    public class DiagnosticHelper
    {        
        public Stopwatch watch { get; set; }

        public void StartWatch()
        {
            watch = Stopwatch.StartNew();
        }

        public void StoptWatch()
        {
            watch.Stop();
        }

        public string GetMessage()
        {
            long totalTime = watch.ElapsedMilliseconds;
            string message = string.Format("Total Execution Time: {0} ms" + Environment.NewLine, totalTime);
            int day = (int)(totalTime / (1000 * 60 * 60 * 24));
            totalTime -= day * 1000 * 60 * 60 * 24;
            int hour = (int)(totalTime / (1000 * 60 * 60));
            totalTime -= hour * 1000 * 60 * 60;

            int minutes = (int)(totalTime / (1000 * 60));
            totalTime -= minutes * 1000 * 60;

            int seconds = (int)(totalTime / 1000);
            totalTime -= seconds * 1000;

            int miliseconds = (int)totalTime;
            message += day > 0 ? string.Format("{0} d ", day) : "";
            message += hour > 0 ? string.Format("{0} h ", hour) : "";
            message += minutes > 0 ? string.Format("{0} m ", minutes) : "";
            message += seconds > 0 ? string.Format("{0} s ", seconds) : "";
            message += miliseconds > 0 ? string.Format("{0} ms ", miliseconds) : "";
            return message;
        }
    }
}
