using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;

namespace BusTrafficDataPreProcessing
{
    class SqlClient
    {
        private SqlConnection myConnection;
        private SqlCommand myCommand;
        private string myCmdString;
        private SqlDataReader myReader;


        
        /// <summary>
        /// 构造函数，新建SQL链接并打开
        /// </summary>
        /// <returns></returns>
        public SqlClient()
        {
            string connString = @"Data Source=DESKTOP-6JRT7M3\SQLEXPRESS;Initial Catalog=CZ;
                                        Integrated Security=True;Connect Timeout=15;Encrypt=False;
                                        TrustServerCertificate=False;ApplicationIntent=ReadWrite;
                                        MultiSubnetFailover=False";
            myConnection = new SqlConnection(connString);
            try
            {
                myConnection.Open();
                Console.WriteLine("数据库链接并打开成功!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            myReader = null;
            myCommand = new SqlCommand();
            myCommand.Connection = myConnection;  // link myCommand to the database connection
        }

        /// <summary>
        /// 执行SQL语句从数据库中读取车辆行程信息
        /// <para>本方法不够稳健,需要SQL语句满足一定条件</para>
        /// <para>参考语句:SELECT * FROM [CZ].[dbo].[BusData] WHERE XL=25 AND CL=18138 AND (ZDM='红梅新村' OR ZDM='白云公交站')  ORDER BY SJ</para>
        /// </summary>
        /// <param name="SQLCmdString"></param>
        /// <returns></returns>
        public List<BusData> ReadBusData(string SQLCmdString)
        {
            
            List<BusData> BusDataList = new List<BusData>();
            myCommand.CommandText = SQLCmdString;
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                BusData aBusData = new BusData(); //必须新建对象,否则list里结果全一样
                aBusData.XL = myReader["XL"].ToString();
                aBusData.CL = myReader["CL"].ToString();
                aBusData.SJ = myReader["SJ"].ToString();
                aBusData.JD = myReader.GetDouble(3);
                aBusData.WD = myReader.GetDouble(4);
                aBusData.SD = myReader.GetDouble(5);
                aBusData.FXJ = myReader.GetDouble(6);
                aBusData.DLZ = myReader["DLZ"].ToString();
                aBusData.ZDM = myReader["ZDM"].ToString();
                aBusData.YYLX = myReader["YYLX"].ToString();

                DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
                dtFormat.ShortDatePattern = "yyyy/MM/dd HH:mm";
                aBusData.DateAndTime = Convert.ToDateTime(aBusData.SJ, dtFormat); 

                BusDataList.Add(aBusData);
                //aBusData.Clean();
            }

            return BusDataList;

        }

        public void Close()
        {
            try
            {
                myConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
