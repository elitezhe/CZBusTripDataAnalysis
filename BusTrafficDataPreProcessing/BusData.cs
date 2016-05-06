using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTrafficDataPreProcessing
{
    class BusData
    {
        public string XL;    //
        public string CL;    //车辆
        public string SJ;    //时间
        public double JD;    //精度
        public double WD;    //纬度
        public double SD;    //
        public double FXJ;   //方向角
        public string DLZ;   //
        public string ZDM;   //站点名
        public string YYLX;  //

        public DateTime DateAndTime; //手动转换后的时间,要记得自己转换并写入该值


        /// <summary>
        /// 默认构造函数,什么也不做
        /// </summary>
        /// <returns></returns>
        public BusData() { }

        
        /// <summary>
        /// 清除对象的所有字段的值
        /// </summary>
        /// <returns></returns>
        public void Clean()
        {
            XL = null;
            CL = null;
            SJ = null;
            JD = 0;
            WD = 0;
            SD = 0;
            FXJ = 0;
            DLZ = null;
            ZDM = null;
            YYLX = null;
        }

        /// <summary>
        /// 生成对象的输出字符串,结尾包含\n,输出或写入文件时使用write,而不是writeline
        /// </summary>
        /// <returns></returns>
        public string ToCsvString()
        {
            string line = XL+"," + CL + "," + SJ + "," + JD + "," + WD + "," + SD + "," + FXJ + "," + DLZ + "," + ZDM + "," + YYLX + "\n";
            return line;
        }
    }
}
