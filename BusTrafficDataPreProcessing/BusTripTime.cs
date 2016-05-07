using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTrafficDataPreProcessing
{
    class BusTripTime
    {
        public string XL; //与数据库中对应
        public string CL; //与数据库中对应
        public string YYLX;
        public string StartStation;
        public string FinishStation;
        public DateTime StartTime;
        public DateTime FinishTime;


        /// <summary>
        /// 计算行程时间.
        /// </summary>
        /// <returns></returns>
        public int GetTripTime()
        {
            var dt = FinishTime - StartTime;
            //int timeMinutes = dt.Hours * 60 + dt.Minutes; //时间范围必须低于1天,因为此处仅使用了小时和分钟
            int timeMinutes = Convert.ToInt32(dt.TotalMinutes); //精确到分钟,不含秒
            return timeMinutes;
        }

        /// <summary>
        /// 生成对象的输出字符串,结尾包含\n,输出或写入文件时使用write,而不是writeline
        /// </summary>
        /// <returns></returns>
        public string ToCsvString()
        {
            return XL + "," + CL + ","+ YYLX + "," + StartTime +","+ StartStation + "," + FinishTime + "," + FinishStation + "," + GetTripTime()+"\n";
        }
    }
}
