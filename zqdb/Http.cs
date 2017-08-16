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
        public const string URL = @"https://ypapi.expop.com.cn/v3/app/";

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
        public const string NONCESTR = @"nonceStr";

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
        public string strNonceStr = @"";

        public JObject joBody;

        public HttpParam()
        { 
        }

        public HttpParam(JObject paramJo)
        {
            strDeviceToken = (string)paramJo[DEVICETOKEN];
            strClientType = (string)paramJo[CLIENTTYPE];
            strUserId = (string)paramJo[USERID];
            strClientToken = (string)paramJo[CLIENTTOKEN];
            strUserToken = (string)paramJo[USERTOKEN];
            strApiVer = (string)paramJo[APIVER];
            strClientVer = (string)paramJo[CLIENTVER];
            strNonceStr = (string)paramJo[NONCESTR];
            joBody = (JObject)paramJo[BODY];

            //Console.WriteLine("devicetoken:{0}", strDeviceToken);
            //Console.WriteLine("clienttype:{0}", strClientType);
            //Console.WriteLine("body:{0}", JsonConvert.SerializeObject(joBody));
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
            strNonceStr = (string)paramJo[NONCESTR];
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
            strNonceStr = _param.strNonceStr;
            joBody = new JObject();
        }

        public string GetSign()
        {
            //strTimeStamp = @"1499351014715";
            //UpdateNonceStr(@"214748364749589");
            strTimeStamp = Timestamp();
            UpdateNonceStr();
            strSign = GetMd5();
            return strSign;
        }

        public string GetParam()
        {
            JObject joParam = new JObject(
                new JProperty(DEVICETOKEN, strDeviceToken),
                new JProperty(CLIENTTYPE, strClientType),
                new JProperty(USERID, strUserId),
                new JProperty(CLIENTTOKEN, strClientToken),
                new JProperty(TIMESTAMP, strTimeStamp),
                new JProperty(USERTOKEN, strUserToken),
                new JProperty(NONCESTR, strNonceStr),
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

        public void UpdateNonceStr(string strTest = "")
        {
            string strMd5 = strNonceStr.Substring(0, 32);
            //string strMd5 = UserMd5(strClientToken).ToLower();
            string nonceStr;
            if (strTest.Length > 0)
            {
                nonceStr = string.Format("{0}{1}", strMd5, strTest);
            }
            else 
            {
                TimeSpan span = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
                float fCurTimeMillis = (float)span.TotalMilliseconds * 2.6f;
                int nCurTimeMillis;
                if (fCurTimeMillis >= (float)int.MaxValue)
                    nCurTimeMillis = int.MaxValue;
                else
                    nCurTimeMillis = (int)fCurTimeMillis;
                int nRandom = (new Random()).Next(100000);
                nonceStr = string.Format("{0}{1}{2}", strMd5, nCurTimeMillis, nRandom);
            }
            strNonceStr = nonceStr;
        }

        public static string Timestamp()  
        {  
            TimeSpan span = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());  
            return ((ulong)span.TotalMilliseconds).ToString();  
        }

        public static string UserMd5(string str)
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
                pwd = pwd + s[i].ToString("X2");
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
                + strAnd + NONCESTR + strEqual + strNonceStr
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
        public static bool bCityLibraryApply = false;
        public static Dictionary<string, string> dcCityLibrary = new Dictionary<string, string>();
        public static bool bCityLibraryFinished = false;
        public static bool bPricesApply = false;
        public static List<Thread> listPricesThread = new List<Thread>();

        HttpParam pmLogin;
        JObject joAddress;
        string strAddress;
        public Thread thread;
        public JObject joLoginParam;
        public string strTelephone;
        public int nIndex;

        static Dictionary<int, bool> dc_ConcertId_Finished = new Dictionary<int, bool>();
        static Dictionary<int, Dictionary<string, int>> dc_ConcertId_dcPriceGoodId = new Dictionary<int, Dictionary<string, int>>();

        
        void SetHttpRequestHeader(DxWinHttp _http, string _sign)
        {
            if(AllPlayers.bSetProxy)
                _http.SetProxy(2, "127.0.0.1:8888", "0");

            _http.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            _http.SetRequestHeader("Host", "ypapi.expop.com.cn");
            //_http.SetRequestHeader("Connection", "Keep-Alive");
            _http.SetRequestHeader("Accept-Encoding", "gzip");
            _http.SetRequestHeader("User-Agent", "okhttp/3.4.1");
            _http.SetRequestHeader("Encrypt-Sign", _sign);
        }

        bool WaitForResponse(DxWinHttp _http)
        {
            DateTime timeStart = DateTime.Now;
            while(true)
            {
                bool succeeded = false;
                _http.WaitForResponse(1, out succeeded);
                if (_http.ResponseBody.Length > 0)
                    break;
                if ((int)((DateTime.Now - timeStart).TotalSeconds) > 30)
                    break;
            }
            return true;
        }

        public void GetCityLibrary()
        {
            if (bCityLibraryFinished)
                return;

            while (true)
            {
                HttpParam pmCityLibrary = new HttpParam(pmLogin, false);
                DxWinHttp http = new DxWinHttp();
                http.Open("POST", HttpParam.URL + @"address/cityLibrary.action", true);
                SetHttpRequestHeader(http, pmCityLibrary.GetSign());
                http.Send(pmCityLibrary.GetParam());

                WaitForResponse(http);
                if (http.ResponseBody.Length > 0 && http.ResponseBody.IndexOf(@"""code"":") >= 0)
                {
                    JObject joCityLibraryResult = (JObject)JsonConvert.DeserializeObject(http.ResponseBody);
                    if ((string)joCityLibraryResult["code"] == @"0")
                    {
                        JArray jaCityLibrary = (JArray)joCityLibraryResult["data"]["list"];
                        foreach (JObject city in jaCityLibrary)
                        {
                            dcCityLibrary.Add((string)city["code"], (string)city["name"]);
                        }
                        bCityLibraryFinished = true;
                        break;
                    }
                }
            }
        }


        void SendNotReadNum()
        {
            HttpParam pmNotReadNum = new HttpParam(pmLogin);
            pmNotReadNum.joBody = new JObject(
                new JProperty("type", (int)16843169),
                new JProperty(HttpParam.USERID, pmLogin.strUserId)
                );

            while (true)
            {
                DxWinHttp http = new DxWinHttp();
                http.Open("POST", HttpParam.URL + @"news/notReadNum.action", true);
                SetHttpRequestHeader(http, pmNotReadNum.GetSign());
                http.Send(pmNotReadNum.GetParam());

                if (DateTime.Now >= AllPlayers.dtStartTime)
                    return;

                Thread.Sleep(AllPlayers.nNotReadNumInterval * 1000);
            }
        }

        public static void ClearConcertIdFinished()
        { 
            dc_ConcertId_Finished.Clear();
            dc_ConcertId_dcPriceGoodId.Clear();
            foreach (ConsertInfo info in AllPlayers.listConcertInfo)
            {
                int nConcertId = info.nConcertId;
                dc_ConcertId_Finished[nConcertId] = false;
                dc_ConcertId_dcPriceGoodId[nConcertId] = new Dictionary<string, int>();
            }        
        }

        public void SendPricesWithThread()
        {
            foreach (Thread thread in listPricesThread)
                thread.Abort();
            listPricesThread.Clear();

            ClearConcertIdFinished();
            dc_ConcertId_dcPriceGoodId.Clear();

            foreach (ConsertInfo info in AllPlayers.listConcertInfo)
            {
                Thread thread = new Thread(new ThreadStart(() => SendPrices(info.nConcertId)));
                thread.Start();
                listPricesThread.Add(thread);
            }
        }

        public void SendPrices(int concertId)
        {
            WaitToStart();

            int nNum = 50;
            HttpParam pmPrices = new HttpParam(pmLogin);
            pmPrices.joBody = new JObject(
                new JProperty("concertId", Convert.ToString(concertId))
                );
            List<DxWinHttp> listHttp = new List<DxWinHttp>();
            for (int i = 0; i < nNum; ++i)
            {
                DxWinHttp http = new DxWinHttp();
                http.Open("POST", HttpParam.URL + @"ticket/prices.action", true);
                SetHttpRequestHeader(http, pmPrices.GetSign());
                http.Send(pmPrices.GetParam());
                listHttp.Add(http);
                Thread.Sleep(1);
            }

            JObject joPricesReturn = new JObject();
            Dictionary<string, int> dcPriceGoodId = new Dictionary<string, int>();
            while (true)
            {
                for (int i = 0; i < nNum; ++i)
                {
                    if (listHttp[i].ResponseBody.Length > 0 && listHttp[i].ResponseBody.IndexOf(@"""code"":") >= 0)
                    {
                        joPricesReturn = (JObject)JsonConvert.DeserializeObject(listHttp[i].ResponseBody);
                        if ((string)joPricesReturn["code"] == @"0")
                        {
                            JArray jaPrices = (JArray)joPricesReturn["data"]["prices"];
                            foreach (JObject joPrice in jaPrices)
                            {
                                //if ((string)joPrice["restStock"] != @"0")
                                {
                                    dcPriceGoodId.Add((string)joPrice["title"], (int)joPrice["goodsId"]);
                                }
                            }
                            dc_ConcertId_dcPriceGoodId[concertId] = dcPriceGoodId;
                            dc_ConcertId_Finished[concertId] = true;
                            return;
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }

        void SendOrderInfo(ConsertInfo info)
        {
            WaitToStart();

            HttpParam pmOrderInfo = new HttpParam(pmLogin);
            pmOrderInfo.joBody = new JObject(
                new JProperty("goodsIds", @" "),
                new JProperty("concertId", Convert.ToString(info.nConcertId))
                );

            DateTime timeStart = DateTime.Now;
            DateTime timeLast = DateTime.Now;
            bool bValid = false;
            if (info.listGoodsId.Count > 0)
            {
                pmOrderInfo.joBody["goodsIds"] = string.Format(info.strGoodsIds, info.listGoodsId[(new Random()).Next(info.listGoodsId.Count)]);
                bValid = true;                
            }
            while (true)
            {
                if ((int)((DateTime.Now - timeLast).TotalMilliseconds) >= 50)
                {
                    timeLast = DateTime.Now;
                    if (dc_ConcertId_Finished[info.nConcertId])
                    {
                        pmOrderInfo.joBody["goodsIds"] = string.Format(info.strGoodsIds, dc_ConcertId_dcPriceGoodId[info.nConcertId][info.arrayPrices[(new Random()).Next(info.arrayPrices.Length)]]);
                        bValid = true;
                    }
                    else 
                    {
                        if (info.listGoodsId.Count > 0)
                        {
                            pmOrderInfo.joBody["goodsIds"] = string.Format(info.strGoodsIds, info.listGoodsId[(new Random()).Next(info.listGoodsId.Count)]);
                            bValid = true;
                        }
                    }
                }

                if (bValid)
                {
                    DxWinHttp http = new DxWinHttp();
                    http.Open("POST", HttpParam.URL + @"ticket/orderInfo.action", true);
                    SetHttpRequestHeader(http, pmOrderInfo.GetSign());
                    http.Send(pmOrderInfo.GetParam());
                }
                Thread.Sleep(1);

                if (AllPlayers.nLoginTimes == 1)
                {
                    if ((int)((DateTime.Now - timeStart).TotalSeconds) > 60)
                        break;
                }
                else
                {
                    if ((int)((DateTime.Now - timeStart).TotalSeconds) > 300)
                        break;
                } 
            }
        }

        void SendSectionOrder(ConsertInfo info)
        {
            WaitToStart();

            HttpParam pmSectionOrder = new HttpParam(pmLogin);
            pmSectionOrder.joBody = new JObject(
                new JProperty("addressId", (string)joAddress["addressId"]),
                new JProperty("address", strAddress),
                new JProperty("phone", (string)joAddress["phone"]),
                new JProperty("goodsIds", @" "),
                new JProperty("concertId", Convert.ToString(info.nConcertId)),
                new JProperty("contact", (string)joAddress["name"]),
                new JProperty("provinceCode", (string)joAddress["provinceCode"])
                );

            List<DxWinHttp> listHttp = new List<DxWinHttp>(); 
            DateTime timeStart = DateTime.Now;
            bool bValid = false;
            int nStart = 0;
            int nIndex = 0;
            if (info.listGoodsId.Count > 0)
            {
                nStart = nIndex % info.listGoodsId.Count;
                pmSectionOrder.joBody["goodsIds"] = string.Format(info.strGoodsIds, info.listGoodsId[(nIndex + nStart) % info.listGoodsId.Count]);
                nIndex++;
                bValid = true;
            }

            while(true)
            {
                while (true)
                {
                    if (bValid)
                    {
                        DxWinHttp http = new DxWinHttp();
                        http.Open("POST", HttpParam.URL + @"ticket/sectionOrder.action", true);
                        SetHttpRequestHeader(http, pmSectionOrder.GetSign());
                        http.Send(pmSectionOrder.GetParam());
                        listHttp.Add(http);
                    }

                    Thread.Sleep(200);
                    if (dc_ConcertId_Finished[info.nConcertId])
                    {
                        pmSectionOrder.joBody["goodsIds"] = string.Format(info.strGoodsIds, dc_ConcertId_dcPriceGoodId[info.nConcertId][info.arrayPrices[(nIndex + nStart) % info.arrayPrices.Length]]);
                        nIndex++;
                        bValid = true;
                    }
                    else
                    {
                        if (info.listGoodsId.Count > 0)
                        {
                            pmSectionOrder.joBody["goodsIds"] = string.Format(info.strGoodsIds, info.listGoodsId[(nIndex + nStart) % info.listGoodsId.Count]);
                            nIndex++;
                            bValid = true;
                        }
                    }

                    if (nIndex > 25)
                        break;
                }

                if (AllPlayers.nLoginTimes == 1)
                {
                    break;
                }
                else
                {
                    if ((int)((DateTime.Now - timeStart).TotalSeconds) > 300)
                        break;
                    else
                        Thread.Sleep(60000);
                }
            }

            if (listHttp.Count > 0)
            {
                for (int i = 0; i < listHttp.Count; i++)
                {
                    WaitForResponse(listHttp[i]);
                    if (listHttp[i].ResponseBody.Length > 0 && listHttp[i].ResponseBody.IndexOf(@"""code"":") >= 0)
                    {
                        JObject joSectionOrderReturn = (JObject)JsonConvert.DeserializeObject(listHttp[i].ResponseBody);
                        if ((string)joSectionOrderReturn["code"] == @"0")
                        {
                            Program.form1.UpdateDataGridView(strTelephone, Column.OrderInfo, (string)joSectionOrderReturn["msg"]);
                            break;
                        }
                    }                    
                }
            }
        }


        void WaitToStart()
        {
            // wait start time
            while ((DateTime.Now < AllPlayers.dtStartTime))
            {
                if ((AllPlayers.dtStartTime - DateTime.Now).TotalMilliseconds > 60000)
                    Thread.Sleep(60000);
                else if ((AllPlayers.dtStartTime - DateTime.Now).TotalMilliseconds > 1000)
                    Thread.Sleep(1000);
                else if ((AllPlayers.dtStartTime - DateTime.Now).TotalMilliseconds > 50)
                    Thread.Sleep(50);
                else
                    Thread.Sleep(1);
            }           
        }


        public void Run()
        {
            Program.form1.UpdateDataGridView(strTelephone, Column.Login, "false");
            Program.form1.UpdateDataGridView(strTelephone, Column.OrderInfo, @"");

            // login.action
            pmLogin = new HttpParam(joLoginParam);
            JObject joLoginReturn = new JObject();
            int nTimes = 0;
            while (true)
            {
                DxWinHttp http = new DxWinHttp();
                http.Open("POST", HttpParam.URL + @"account/login.action", true);
                SetHttpRequestHeader(http, pmLogin.GetSign());
                http.Send(pmLogin.GetParam());

                WaitForResponse(http);
                if (http.ResponseBody.Length > 0 && http.ResponseBody.IndexOf(@"""code"":") >= 0)
                {
                    joLoginReturn = (JObject)JsonConvert.DeserializeObject(http.ResponseBody);
                    if ((string)joLoginReturn["code"] == @"0")
                    {
                        break;
                    }
                }
                nTimes++;
                if (nTimes >= 10)
                {
                    Program.form1.UpdateDataGridView(strTelephone, Column.Login, "放弃");
                    return;
                }
            }
            pmLogin.strUserId = (string)joLoginReturn["data"][HttpParam.USERID];
            pmLogin.strUserToken = (string)joLoginReturn["data"][HttpParam.USERTOKEN];
            Program.form1.UpdateDataGridView(strTelephone, Column.Login, "true");
            if (AllPlayers.nLoginTimes == 1)
                Program.form1.UpdateDataGridView(strTelephone, Column.Name, (string)joLoginReturn["data"]["realName"]);

            // addressList.action
            if (AllPlayers.nLoginTimes == 1)
            {
                // CityLibrary.action
                bool bNeedGetCityLibrary = false;
                lock (this)
                {
                    if (!bCityLibraryApply)
                    {
                        bNeedGetCityLibrary = true;
                        bCityLibraryApply = true;
                    }
                }

                if (bNeedGetCityLibrary)
                {
                    GetCityLibrary();
                } 
                
                HttpParam pmAddressList = new HttpParam(pmLogin);
                pmAddressList.joBody = new JObject();
                JObject joAddressListReturn = new JObject();
                while (true)
                {
                    DxWinHttp http = new DxWinHttp();
                    http.Open("POST", HttpParam.URL + @"address/addressList.action", true);
                    SetHttpRequestHeader(http, pmAddressList.GetSign());
                    http.Send(pmAddressList.GetParam());

                    WaitForResponse(http);
                    if (http.ResponseBody.Length > 0 && http.ResponseBody.IndexOf(@"""code"":") >= 0)
                    {
                        joAddressListReturn = (JObject)JsonConvert.DeserializeObject(http.ResponseBody);
                        if ((string)joAddressListReturn["code"] == @"0")
                        {
                            JArray jaAddressList = (JArray)joAddressListReturn["data"]["list"];
                            if (jaAddressList.Count <= 0)
                            {
                                Program.form1.UpdateDataGridView(strTelephone, Column.Address, @"null");
                                return;
                            }
                            joAddress = new JObject();
                            foreach (JObject addr in jaAddressList)
                            {
                                if ((int)addr["isDefault"] == 1)
                                {
                                    joAddress = addr;
                                    break;
                                }
                            }

                            while (!bCityLibraryFinished)
                                Thread.Sleep(1);

                            string strProvince = dcCityLibrary[(string)joAddress["provinceCode"]];
                            string strCity = dcCityLibrary[(string)joAddress["cityCode"]];
                            string strArea = dcCityLibrary[(string)joAddress["areaCode"]];
                            strAddress = strProvince + @" " + strCity + @" " + strArea + @" " + (string)joAddress["address"];
                            Program.form1.UpdateDataGridView(strTelephone, Column.Address, strAddress);
                            break;
                        }
                    }
                }
            }

            // myOrders.action & cancel.action
            //if (AllPlayers.nLoginTimes > 1)
            {
                HttpParam pmMyOrder = new HttpParam(pmLogin);
                pmMyOrder.joBody = new JObject(
                    new JProperty("time", ulong.Parse(HttpParam.Timestamp())),
                    new JProperty("orderStatus", 0),
                    new JProperty("startIndex", 0),
                    new JProperty("pageSize", 20)
                    );
                JObject joMyOrderReturn = new JObject();
                while (true)
                {
                    DxWinHttp http = new DxWinHttp();
                    http.Open("POST", HttpParam.URL + @"user/myOrders.action", true);
                    SetHttpRequestHeader(http, pmMyOrder.GetSign());
                    http.Send(pmMyOrder.GetParam());

                    WaitForResponse(http);
                    if (http.ResponseBody.Length > 0 && http.ResponseBody.IndexOf(@"""code"":") >= 0)
                    {
                        joMyOrderReturn = (JObject)JsonConvert.DeserializeObject(http.ResponseBody);
                        if ((string)joMyOrderReturn["code"] == @"0")
                        {
                            JArray jaOrder = (JArray)joMyOrderReturn["data"]["list"];
                            foreach (JObject order in jaOrder)
                            {
                                if ((string)order["orderType"] == "3" && (string)order["orderStatus"] == "0")
                                {
                                    HttpParam pmCancel = new HttpParam(pmLogin);
                                    pmCancel.joBody = new JObject(
                                        new JProperty("orderId", (string)order["orderId"])
                                        );
                                    JObject joCancel = new JObject();
                                    while (true)
                                    {
                                        DxWinHttp httpCancel = new DxWinHttp();
                                        httpCancel.Open("POST", HttpParam.URL + @"order/cancel.action", true);
                                        SetHttpRequestHeader(httpCancel, pmCancel.GetSign());
                                        httpCancel.Send(pmCancel.GetParam());

                                        WaitForResponse(httpCancel);
                                        if (httpCancel.ResponseBody.Length > 0 && httpCancel.ResponseBody.IndexOf(@"""code"":") >= 0)
                                        {
                                            joCancel = (JObject)JsonConvert.DeserializeObject(httpCancel.ResponseBody);
                                            JToken outMsg;
                                            if ((string)joCancel["code"] == @"0" || (joCancel.TryGetValue("msg", out outMsg) && outMsg.Type != JTokenType.Null))
                                            {
                                                break;
                                            }
                                        }                                    
                                    }
                                }
                            }
                            break;
                        }
                    }
                }              
            }

            // Prices.action
            bool bNeedGetPrices = false;
            lock (this)
            {
                if (!bPricesApply)
                {
                    bNeedGetPrices = true;
                    bPricesApply = true;
                }
            }
            if (bNeedGetPrices)
            {
                SendPricesWithThread();
            }

            // notReadNum.action
            if (AllPlayers.nLoginTimes == 1)
            {
                Thread threadNotReadNum = new Thread(new ThreadStart(SendNotReadNum));
                threadNotReadNum.Start();
            }

            // OrderInfo.action
            foreach (ConsertInfo info in AllPlayers.listConcertInfo)
            {
                Thread threadOrderInfo = new Thread(new ThreadStart(() => SendOrderInfo(info)));
                threadOrderInfo.Start();
            }

            // SectionOrder.action
            foreach (ConsertInfo info in AllPlayers.listConcertInfo)
            {
                Thread threadSectionOrder = new Thread(new ThreadStart(() => SendSectionOrder(info)));
                threadSectionOrder.Start();
            }
        }
    };

    class ConsertInfo
    {
        public int nConcertId;
        public int nGoodsIdNum;
        public string strGoodsIds;
        public string[] arrayPrices;
        public List<string> listGoodsId;

        public ConsertInfo(JObject joPrice)
        {
            nConcertId = (int)joPrice["ConcertId"];
            nGoodsIdNum = (int)joPrice["GoodsIdNum"];
            strGoodsIds = "{0}";
            for (int i = 1; i < nGoodsIdNum; i++)
                strGoodsIds = strGoodsIds + ",{0}";
            string strPrices = (string)joPrice["Prices"];
            arrayPrices = strPrices.Split(new Char[] { ',', ' ', ';' });
            listGoodsId = new List<string>();
            JToken outGoodsId;
            if (joPrice.TryGetValue("GoodsId", out outGoodsId))
            {
                string[] arrayGoodsId = ((string)joPrice["GoodsId"]).Split(new Char[] { ',', ' ', ';' });
                for (int i = 0; i < arrayGoodsId.Length; i++)
                    listGoodsId.Add(arrayGoodsId[i]);
            }            
        }
    };

    class AllPlayers
    {
        public static JArray jaConcert;
        public static DateTime dtStartTime;
        public static int nNotReadNumInterval;
        public static int nReloginInterval;
        public static int nLoginTimes = 1;
        public static bool bSetProxy = false;
        public static string strApiVer = @"";
        public static string strClientVer = @"";
        public static string strClientType = @"";
        public static string strConfigFileName = @"";
        public static string strAccountFileName = @"";
        public static List<ConsertInfo> listConcertInfo = new List<ConsertInfo>();
        
        List<Player> listPlayer = new List<Player>();

        public void Init()
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DirectoryInfo folderDesktop = new DirectoryInfo(desktop);
            foreach(FileInfo NextFile in folderDesktop.GetFiles())
            {
                if (string.Equals(NextFile.Extension, ".txt", StringComparison.OrdinalIgnoreCase))
                    strConfigFileName = NextFile.DirectoryName + @"\" + NextFile.Name;

                if (string.Equals(NextFile.Extension, ".csv", StringComparison.OrdinalIgnoreCase))
                    strAccountFileName = NextFile.DirectoryName + @"\" + NextFile.Name;
            }

            string[] arrayConfig = File.ReadAllLines(strConfigFileName);
            string[] arrayText = File.ReadAllLines(strAccountFileName);

            JObject joInfo = (JObject)JsonConvert.DeserializeObject(arrayConfig[0]);
            jaConcert = (JArray)joInfo["list"];
            dtStartTime = DateTime.Parse((string)joInfo["StartTime"]);
            nNotReadNumInterval = (int)joInfo["NotReadNumInterval"];
            nReloginInterval = (int)joInfo["ReLoginInterval"];
            strApiVer = (string)joInfo[HttpParam.APIVER];
            strClientVer = (string)joInfo[HttpParam.CLIENTVER];
            strClientType = (string)joInfo[HttpParam.CLIENTTYPE];
            if ((string)joInfo["SetProxy"] == @"0")
                bSetProxy = false;
            else
                bSetProxy = true;
            foreach (JObject joPrice in jaConcert)
            {
                ConsertInfo cinfo = new ConsertInfo(joPrice);
                listConcertInfo.Add(cinfo);
            }

            Program.form1.Form1_Init();

/* 
            for (int i = 1; i < arrayText.Length; ++i)
            {
                int startIndex = arrayText[i].IndexOf("=");
                string paramUri = arrayText[i].Substring(startIndex + 1);
                string param = Uri.UnescapeDataString(paramUri);
                JObject joParam = (JObject)JsonConvert.DeserializeObject(param);
                Program.form1.dataGridViewInfo_AddRow(joParam);

                Player player = new Player();
                player.nIndex = i - 1;
                player.joLoginParam = joParam;
                player.strTelephone = (string)joParam[HttpParam.BODY]["phone"];
                player.thread = new Thread(new ThreadStart(player.Run));
                //player.thread.Start();
                listPlayer.Add(player);
            }
*/

            int nInit = 1;
            for (int i = nInit; i < arrayText.Length; ++i)
            {
                string[] arrayParam = arrayText[i].Split(new char[] { ',' }); 
                Program.form1.dataGridViewInfo_AddRow(arrayParam[3]);

                JObject joParam = new JObject(
                    new JProperty(HttpParam.DEVICETOKEN, arrayParam[1]),
                    new JProperty(HttpParam.CLIENTTYPE, AllPlayers.strClientType),
                    new JProperty(HttpParam.USERID, @""),
                    new JProperty(HttpParam.CLIENTTOKEN, arrayParam[2]),
                    new JProperty(HttpParam.TIMESTAMP, @""),
                    new JProperty(HttpParam.USERTOKEN, @""),
                    new JProperty(HttpParam.NONCESTR, HttpParam.UserMd5(arrayParam[2]).ToLower()),
                    new JProperty(HttpParam.APIVER, AllPlayers.strApiVer),
                    new JProperty(HttpParam.CLIENTVER, AllPlayers.strClientVer),
                    new JProperty(HttpParam.SIGN, @""),
                    new JProperty(HttpParam.BODY, new JObject(
                            new JProperty(HttpParam.PHONE, arrayParam[3]),
                            new JProperty(HttpParam.PASSWORD, arrayParam[4])
                        ))
                    );

                Player player = new Player();
                player.nIndex = i - nInit;
                player.joLoginParam = joParam;
                player.strTelephone = (string)joParam[HttpParam.BODY]["phone"];
                player.thread = new Thread(new ThreadStart(player.Run));
                //player.thread.Start();
                listPlayer.Add(player);
            }

            Player.ClearConcertIdFinished();
        }


        public void Run()
        {
            Program.form1.UpdateLoginTimes(); 

            // first login
            foreach(Player player in listPlayer)
            {
                player.thread.Start();
            }

            // relogin
            Thread threadRelogin = new Thread(new ThreadStart(this.WaitForRelogin));
            threadRelogin.Start();
        }

        void Relogin()
        {
            nLoginTimes = nLoginTimes + 1;
            Program.form1.UpdateLoginTimes();
            //Player.bPricesApply = false;

            foreach (Player player in listPlayer)
            {
                player.thread.Abort();
                player.thread = new Thread(new ThreadStart(player.Run));
                player.thread.Start();
            }
        }

        void WaitForRelogin()
        {
            int nBaseTimes = 0;
            if(DateTime.Now > AllPlayers.dtStartTime)
            {
                TimeSpan span = DateTime.Now - AllPlayers.dtStartTime;
                int nSpanSeconds = (int)span.TotalSeconds;
                nBaseTimes = (int)nSpanSeconds / AllPlayers.nReloginInterval;
            }

            while(true)
            {
                if(DateTime.Now < AllPlayers.dtStartTime)
                {
                    TimeSpan span = (AllPlayers.dtStartTime - DateTime.Now);
                    Thread.Sleep((int)span.TotalMilliseconds + (AllPlayers.nReloginInterval - 1) * 1000);
                }
                else
                {
                    TimeSpan span = DateTime.Now - AllPlayers.dtStartTime;
                    if ((int)span.TotalSeconds > (nBaseTimes + nLoginTimes) * AllPlayers.nReloginInterval)
                    {
                        Relogin();
                        Thread.Sleep(AllPlayers.nReloginInterval * 1000);
                    }
                }
            }
        }
    };
     
}
