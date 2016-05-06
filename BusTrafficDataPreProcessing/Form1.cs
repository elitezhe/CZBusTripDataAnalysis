using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BusTrafficDataPreProcessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ListToCsv(List<BusData> theList)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"BusDataList.csv", false, Encoding.UTF8);
            foreach (BusData bd in theList)
            {
                string line = bd.ToCsvString();
                file.Write(line);
            }
            file.Flush();
            file.Close();
        }

        private void ListToCsv(List<BusTripTime> theList)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"TripTimeList.csv", false, Encoding.UTF8);
            foreach (BusTripTime bd in theList)
            {
                string line = bd.ToCsvString();
                file.Write(line);
            }
            file.Flush();
            file.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cmdString = @"SELECT * FROM [CZ].[dbo].[BusData] WHERE XL=25 AND CL=18138 AND (ZDM='红梅新村' OR ZDM='白云公交站')  ORDER BY SJ";

            SqlClient myClient = new SqlClient();
            var BusDataList = myClient.ReadBusData(cmdString);
            BusDataList.Sort((x, y) => { return x.DateAndTime.CompareTo(y.DateAndTime)*2 + x.DLZ.CompareTo(y.DLZ); });

            ListToCsv(BusDataList);

            List<BusTripTime> tripTimeList = new List<BusTripTime>();
            BusData lastBusData = new BusData();
            foreach (BusData bd in BusDataList)
            {
                if (bd.DLZ == "到站" && lastBusData.DLZ == "离站")
                {
                    if (bd.ZDM != lastBusData.ZDM && bd.YYLX == lastBusData.YYLX)
                    {
                        BusTripTime aTripTime = new BusTripTime();
                        aTripTime.StartStation = lastBusData.ZDM;
                        aTripTime.FinishStation = bd.ZDM;
                        aTripTime.StartTime = lastBusData.DateAndTime;
                        aTripTime.FinishTime = bd.DateAndTime;

                        tripTimeList.Add(aTripTime);
                    }
                }

                lastBusData = bd;
            }

            ListToCsv(tripTimeList);
        }
    }
}
