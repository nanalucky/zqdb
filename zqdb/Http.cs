using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxComOperate;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Threading; 

namespace zqdb
{
    public class HttpParam
    {
        public const string URL = @"http://server.expop.com.cn/api_v2/app/";
        public static int NOTREADNUM_INTERVAL = 5;
        public static DateTime DATETIMERUN;
        public static int CONCERTID;
        public static string PRICES;

        public const string KEY = @"key";
        public const string DEVICETOKEN = @"deviceToken";
        public const string CLIENTTYPE = @"clientType";
        public const string USERID = @"userId";
        public const string CLIENTTOKEN = @"clientToken";
        public const string TIMESTAMP = @"timestamp";
        public const string USERTOKEN = @"userToken";
        public const string APIVER = @"apiVer";
        public const string CLIENTVER = @"clientVer";
        public const string SIGN = @"sign";
        public const string BODY = @"body";
        public const string PHONE = @"phone";
        public const string PASSWORD = @"password";
        
        string strKey = @"40389d43-5823-4e4d-be5f-d6e94e45d73f";
        string strAnd = @"&";
        string strEqual = @"=";

        public string strDeviceToken = @"";
        public string strClientType = @"";
        public string strUserId = @"";
        public string strClientToken = @"";
        public string strTimeStamp = @"";
        public string strUserToken = @"";
        public string strApiVer = @"";
        public string strClientVer = @"";
        public string strSign = @"";

        public JObject joBody;

        public HttpParam()
        { 
        }

        public HttpParam(string strParam)
        {
            int startIndex = strParam.IndexOf("=");
            string paramUri = strParam.Substring(startIndex + 1);
            string param = Uri.UnescapeDataString(paramUri);
            JObject paramJo = (JObject)JsonConvert.DeserializeObject(param);
            strDeviceToken = (string)paramJo[DEVICETOKEN];
            strClientType = (string)paramJo[CLIENTTYPE];
            strUserId = (string)paramJo[USERID];
            strClientToken = (string)paramJo[CLIENTTOKEN];
            strUserToken = (string)paramJo[USERTOKEN];
            strApiVer = (string)paramJo[APIVER];
            strClientVer = (string)paramJo[CLIENTVER];
            joBody = (JObject)paramJo[BODY];

            //Console.WriteLine("devicetoken:{0}", strDeviceToken);
            //Console.WriteLine("clienttype:{0}", strClientType);
            //Console.WriteLine("body:{0}", JsonConvert.SerializeObject(joBody));
        }

        public HttpParam(HttpParam _param, bool user = true)
        {
            strDeviceToken = _param.strDeviceToken;
            strClientType = _param.strClientType;
            if(user)
                strUserId = _param.strUserId;
            strClientToken = _param.strClientToken;
            if(user)
                strUserToken = _param.strUserToken;
            strApiVer = _param.strApiVer;
            strClientVer = _param.strClientVer;
            joBody = new JObject();
        }

        public string GetParam()
        {
            strTimeStamp = Timestamp();
            //strTimeStamp = @"1492703932428";
            strSign = GetMd5();

            JObject joParam = new JObject(
                new JProperty(DEVICETOKEN, strDeviceToken),
                new JProperty(CLIENTTYPE, strClientType),
                new JProperty(USERID, strUserId),
                new JProperty(CLIENTTOKEN, strClientToken),
                new JProperty(TIMESTAMP, strTimeStamp),
                new JProperty(USERTOKEN, strUserToken),
                new JProperty(APIVER, strApiVer),
                new JProperty(CLIENTVER, strClientVer),
                new JProperty(SIGN, strSign), 
                new JProperty(BODY, joBody)
                );

            string strParam = JsonConvert.SerializeObject(joParam);
            string strParamUri = Uri.EscapeDataString(strParam);
            strParamUri = @"params=" + strParamUri;
            //Console.WriteLine("new param:{0}", strParamUri);
            return strParamUri;
        }

        public static string Timestamp()  
        {  
            TimeSpan span = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());  
            return ((ulong)span.TotalMilliseconds).ToString();  
        }

        private static string UserMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }

        private string GetMd5()
        {
            string urlParam = APIVER + strEqual + strApiVer
                + strAnd + BODY + strEqual + JsonConvert.SerializeObject(joBody)
                + strAnd + CLIENTTOKEN + strEqual + strClientToken
                + strAnd + CLIENTTYPE + strEqual + strClientType
                + strAnd + CLIENTVER + strEqual + strClientVer
                + strAnd + DEVICETOKEN + strEqual + strDeviceToken
                + strAnd + TIMESTAMP + strEqual + strTimeStamp;

            if (strUserId.Length > 0)
                urlParam += strAnd + USERID + strEqual + strUserId;

            if (strUserToken.Length > 0)
                urlParam += strAnd + USERTOKEN + strEqual + strUserToken;

            urlParam += strAnd + KEY + strEqual + strKey;
            //Console.WriteLine("param:{0}", urlParam);

            return UserMd5(urlParam);
        }
    };

    public class Player
    {
        HttpParam pmNotReadNum;
        public static Dictionary<string, string> dcCityLibrary = new Dictionary<string,string>();
 
        static void SetHttpRequestHeader(DxWinHttp _http)
        {
            _http.SetProxy(2, "127.0.0.1:8888", "0");
            _http.ClearPostData();
            _http.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            _http.SetRequestHeader("Host", "server.expop.com.cn");
            _http.SetRequestHeader("Connection", "Keep-Alive");
            _http.SetRequestHeader("Accept-Encoding", "gzip");
            _http.SetRequestHeader("User-Agent", "okhttp/3.4.1");
        }

        public static void GetCityLibrary(string strLoginParam)
        {
            while (true)
            {
                HttpParam pmCityLibrary = new HttpParam(strLoginParam);
                pmCityLibrary.strUserId = @"";
                pmCityLibrary.strUserToken = @"";
                DxWinHttp http = new DxWinHttp();
                http.Open("POST", HttpParam.URL + @"address/cityLibrary.action", true);
                SetHttpRequestHeader(http);
                http.Send(pmCityLibrary.GetParam());

                bool succeeded = false;
                http.WaitForResponse(30, out succeeded);
                if (succeeded)
                {
                    JObject joCityLibraryResult = (JObject)JsonConvert.DeserializeObject(http.ResponseBody);
                    if ((string)joCityLibraryResult["code"] == @"0")
                    {
                        JArray jaCityLibrary = (JArray)joCityLibraryResult["data"]["list"];
                        foreach (JObject city in jaCityLibrary)
                        {
                            dcCityLibrary.Add((string)city["code"], (string)city["name"]);
                        }
                        break;
                    }
                }
            }
        }


        void SendNotReadNum()
        {
            while (true)
            {
                DxWinHttp http = new DxWinHttp();
                http.Open("POST", HttpParam.URL + @"news/notReadNum.action", true);
                SetHttpRequestHeader(http);
                http.Send(pmNotReadNum.GetParam());
                Thread.Sleep(HttpParam.NOTREADNUM_INTERVAL * 1000);
            }
        }

        void SendSectionOrder(string strParam)
        {
            while (true)
            {
                DxWinHttp http = new DxWinHttp();
                http.Open("POST", HttpParam.URL + @"ticket/sectionOrder.action", true);
                SetHttpRequestHeader(http);
                http.Send(strParam);

                bool succeeded = false;
                http.WaitForResponse(30, out succeeded);
                if (succeeded)
                {
                    Console.Write(http.ResponseBody);
                    JObject joSectionOrderReturn = (JObject)JsonConvert.DeserializeObject(http.ResponseBody);
                    if ((string)joSectionOrderReturn["code"] == @"0")
                    {
                        break;
                    }
                }
            }
        }


        public void Run(string strLoginParam)
        {
            //HttpParam param = new HttpParam(strLoginParam);
            //param.GetParam();

            // login.action
            HttpParam pmLogin = new HttpParam(strLoginParam);
            JObject joLoginReturn = new JObject();
            while (true)
            {
                DxWinHttp http = new DxWinHttp();
                http.Open("POST", HttpParam.URL + @"account/login.action", true);
                SetHttpRequestHeader(http);
                http.Send(pmLogin.GetParam());

                bool succeeded = false;
                http.WaitForResponse(30, out succeeded);
                if (succeeded)
                {
                    Console.Write(http.ResponseBody);
                    joLoginReturn = (JObject)JsonConvert.DeserializeObject(http.ResponseBody);
                    if ((string)joLoginReturn["code"] == @"0")
                    {
                        break;
                    }
                }
            }
            pmLogin.strUserId = (string)joLoginReturn["data"][HttpParam.USERID];
            pmLogin.strUserToken = (string)joLoginReturn["data"][HttpParam.USERTOKEN];

            // addressList.action
            HttpParam pmAddressList = new HttpParam(pmLogin);
            pmAddressList.joBody = new JObject();
            JObject joAddressListReturn = new JObject();
            while (true)
            {
                DxWinHttp http = new DxWinHttp();
                http.Open("POST", HttpParam.URL + @"address/addressList.action", true);
                SetHttpRequestHeader(http);
                http.Send(pmAddressList.GetParam());

                bool succeeded = false;
                http.WaitForResponse(30, out succeeded);
                if (succeeded)
                {
                    Console.Write(http.ResponseBody);
                    joAddressListReturn = (JObject)JsonConvert.DeserializeObject(http.ResponseBody);
                    if ((string)joAddressListReturn["code"] == @"0")
                    {
                        break;
                    }
                }
            }
            
            // prices.action
            HttpParam pmPrices = new HttpParam(pmLogin);
            pmPrices.joBody = new JObject(
                new JProperty("concertId", HttpParam.CONCERTID)
                );
            JObject joPricesReturn = new JObject();
            Dictionary<string, int> dcPriceGoodId = new Dictionary<string, int>(); 
            while (true)
            {
                DxWinHttp http = new DxWinHttp();
                http.Open("POST", HttpParam.URL + @"ticket/prices.action", true);
                SetHttpRequestHeader(http);
                http.Send(pmPrices.GetParam());

                bool succeeded = false;
                http.WaitForResponse(30, out succeeded);
                if (succeeded)
                {
                    Console.Write(http.ResponseBody);
                    joPricesReturn = (JObject)JsonConvert.DeserializeObject(http.ResponseBody);
                    if ((string)joPricesReturn["code"] == @"0")
                    {
                        JArray jaPrices = (JArray)joPricesReturn["data"]["prices"];
                        foreach(JObject joPrice in jaPrices)
                        {
                            if((string)joPrice["restStock"] != @"0")
                            {
                                dcPriceGoodId.Add((string)joPrice["title"], (int)joPrice["goodsId"]);
                            }
                        }
                        break;
                    }
                }
            }
            
            // notReadNum.action
            pmNotReadNum = new HttpParam(pmLogin);
            pmNotReadNum.joBody = new JObject(
                new JProperty("type", (int)16843169),
                new JProperty(HttpParam.USERID, pmLogin.strUserId)
                );
            Thread threadNotReadNum = new Thread(new ThreadStart(SendNotReadNum));
            threadNotReadNum.Start();

            // wait start time
            while ((DateTime.Now < HttpParam.DATETIMERUN))
            {
                Thread.Sleep(100);
            }
            threadNotReadNum.Abort();

            
            // sectionOrder.action
            HttpParam pmSectionOrder = new HttpParam(pmLogin);
            JObject joSectionOrder = new JObject();

            JArray jaAddressList = (JArray)joAddressListReturn["data"]["list"];
            JObject joAddress = new JObject();
            foreach(JObject addr in jaAddressList)
            {
                if((int)addr["isDefault"] == 1)
                {
                    joAddress = addr;
                    break;
                }
            }
            string strProvince = dcCityLibrary[(string)joAddress["provinceCode"]];
            string strCity = dcCityLibrary[(string)joAddress["cityCode"]];
            string strArea = dcCityLibrary[(string)joAddress["areaCode"]];
            string strAddress = strProvince + @" " + strCity + @" " + strArea + @" " + (string)joAddress["address"];

            pmSectionOrder.joBody = new JObject(
                new JProperty("address", strAddress),
                new JProperty("contact", (string)joAddress["name"]),
                new JProperty("provinceCode", (string)joAddress["provinceCode"]),
                new JProperty("addressId", (string)joAddress["addressId"]),
                new JProperty("phone", (string)joAddress["phone"]),
                new JProperty("goodsIds", @" "),
                new JProperty("concertId", Convert.ToString(HttpParam.CONCERTID))
                );

            string[] arrayPrices = HttpParam.PRICES.Split(new Char[] {',', ' ', ';'});
            List<string> listPrices = new List<string>();
            foreach(string price in arrayPrices)
            {
                if(dcPriceGoodId.ContainsKey(price))
                    listPrices.Add(price);
            }

            List<Thread> listThread = new List<Thread>();
            foreach(string price in listPrices)
            {
                pmSectionOrder.joBody["goodsIds"] = Convert.ToString(dcPriceGoodId[price]);
                Thread threadSectionOrder = new Thread(new ThreadStart(() => SendSectionOrder(pmSectionOrder.GetParam())));
                threadSectionOrder.Start();
                listThread.Add(threadSectionOrder);
            }

        }
    };
    
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        string strMyOrder = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\myorder.txt");
    //        Player player = new Player();
    //        player.Run(strMyOrder);
           

    //        Console.WriteLine("timestamp:{0}", HttpParam.Timestamp());

    //        Console.Read();
    //    }
    //}
}
