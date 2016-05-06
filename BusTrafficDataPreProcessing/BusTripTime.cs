using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTrafficDataPreProcessing
{
    class BusTripTime
    {
        public string StartStation;
        public string FinishStation;
        public DateTime StartTime;
        public DateTime FinishTime;

        public int GetTripTime()
        {
            var dt = FinishTime - StartTime;
            int timeMinutes = dt.Hours * 60 + dt.Minutes;
            return timeMinutes;
        }

        public string ToCsvString()
        {
            return StartTime +","+ StartStation + "," + FinishTime + "," + FinishStation + "," + GetTripTime()+"\n";
        }
    }
}
