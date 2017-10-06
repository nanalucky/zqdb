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
using System.Windows.Forms;
using Fiddler;
using FiddlerCore;
using System.Diagnostics;

namespace zqdb
{
    public class HttpParam
    {
        public const string URL = @"https://api.expop.com.cn/v3/app/";

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

        string strKey = @"ae973b8d-64f3-42e4-8f3f-b93d76f5924f";
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
            //strTimeStamp = @"1496512106741";
            //UpdateNonceStr(@"214748364773698");
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
        public static Dictionary<string, Player> dicSignaturePlayer = new Dictionary<string, Player>();

        public string signature = @"";
        public int type = 0;
        public int score = 0;
        public int userTime = 0;
        public string commitGameResultResponse = @"";
       
        void SetHttpRequestHeader(DxWinHttp _http, string _sign)
        {
            _http.SetProxy(2, "127.0.0.1:8888", "0");

            _http.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            _http.SetRequestHeader("Host", "api.expop.com.cn");
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

        public void response_getGameSign(Fiddler.Session oS)
        {
            JObject paramSignature = (JObject)JsonConvert.DeserializeObject(oS.GetResponseBodyAsString());
            signature = (string)paramSignature["data"]["signature"];
            HttpParam pmGetGameSign = new HttpParam(oS.GetRequestBodyAsString());
            type = (int)pmGetGameSign.joBody["type"];
            userTime = (new Random()).Next(AllPlayers.dicTypeScore[type].minSecond * 1000, AllPlayers.dicTypeScore[type].maxSecond * 1000);
            score = AllPlayers.dicTypeScore[type].score;

            HttpParam pmCommitGameResult = new HttpParam(oS.GetRequestBodyAsString());
            pmCommitGameResult.joBody = new JObject(
                    new JProperty("type", type),
                    new JProperty("useTime", userTime),
                    new JProperty("signature", signature),
                    new JProperty("score", score)
                    );


            DxWinHttp http = new DxWinHttp();
            http.Open("POST", HttpParam.URL + @"game/commitGameResult.action", true);
            SetHttpRequestHeader(http, pmCommitGameResult.GetSign());
            http.Send(pmCommitGameResult.GetParam());

            Fiddler.FiddlerApplication.Log.LogFormat("{0}request: ClientToken:{1}, signature:{2}, type:{3}, score:{4}, useTime:{5}", zqdbFiddler.strPreNotify, pmCommitGameResult.strClientToken, signature, type, score, userTime);

            dicSignaturePlayer.Add(signature, this);
            return;           
        } 
    };

    class scoreItem
    {
        public int type;
        public int score;
        public int minSecond;
        public int maxSecond;
    }

    class AllPlayers
    {
        public static Dictionary<int, scoreItem> dicTypeScore = new Dictionary<int, scoreItem>();
        public static int curProcessId = 0;

        public void Init()
        {
            string strConfigFileName = System.Environment.CurrentDirectory + @"\" + @"config.csv";
            string[] arrayText = File.ReadAllLines(strConfigFileName);

            int nInit = 1;
            for (int i = nInit; i < arrayText.Length; ++i)
            {
                string[] arrayParam = arrayText[i].Split(new char[] { ',' });
                scoreItem item = new scoreItem();
                item.type = int.Parse(arrayParam[0]);
                item.score = int.Parse(arrayParam[1]);
                item.minSecond = int.Parse(arrayParam[2]);
                item.maxSecond = int.Parse(arrayParam[3]);
                dicTypeScore.Add(item.type, item);
            }
            
            curProcessId = Process.GetCurrentProcess().Id;
        }
  
    };

    public class zqdbFiddler
    {
        public static string strPreNotify = @"notify ";

        public static bool InstallCertificate()
        {
            if (!CertMaker.rootCertExists())
            {
                if (!CertMaker.createRootCert())
                    return false;

                if (!CertMaker.trustRootCert())
                    return false;
            }

            return true;
        }
        
        
        public static void Init()
        {
            // <-- Personalize for your Application, 64 chars or fewer
            Fiddler.FiddlerApplication.SetAppDisplayName("FiddlerCoreZQDB");
            Fiddler.FiddlerApplication.OnNotification += delegate(object sender, NotificationEventArgs oNEA) { Program.form1.richTextBoxFiddler_AddString(oNEA.NotifyString + "\n"); };
            Fiddler.FiddlerApplication.Log.OnLogString += delegate(object sender, LogEventArgs oLEA) 
            {
                if (oLEA.LogString.IndexOf(strPreNotify) == 0)
                    Program.form1.richTextBoxFiddler_AddString(oLEA.LogString.Substring(strPreNotify.Length) + "\n");
                else
                    Program.form1.richTextBoxFiddlerAll_AddString(oLEA.LogString + "\n");
            };

            Fiddler.FiddlerApplication.BeforeRequest += delegate(Fiddler.Session oS)
            {
                if (oS.fullUrl.Contains(HttpParam.URL))
                {
                    Fiddler.FiddlerApplication.Log.LogFormat("{0} Request {1}", oS.clientIP, oS.fullUrl);
                }

                // Console.WriteLine("Before request for:\t" + oS.fullUrl);
                // In order to enable response tampering, buffering mode MUST
                // be enabled; this allows FiddlerCore to permit modification of
                // the response in the BeforeResponse handler rather than streaming
                // the response to the client as the response comes in.
                if (oS.fullUrl.Contains(@"https://api.expop.com.cn/v3/app/game/getGameSign.action"))
                {
                    oS.bBufferResponse = true;
                    
                }

                if (oS.fullUrl.Contains(@"https://api.expop.com.cn/v3/app/game/commitGameResult.action"))
                {
                    if (oS.LocalProcessID != AllPlayers.curProcessId)
                    {
                        oS.utilCreateResponseAndBypassServer();
                        oS.LoadResponseFromFile(System.Environment.CurrentDirectory + @"\" + @"response.txt");

                        HttpParam param = new HttpParam(oS.GetRequestBodyAsString());
                        Fiddler.FiddlerApplication.Log.LogFormat("{0}auto: ClientToken:{1}, signature:{2}, result:{3}", zqdbFiddler.strPreNotify, param.strClientToken, (string)param.joBody["signature"], oS.GetResponseBodyAsString());
                    }
                    else
                    {
                        oS.bBufferResponse = true;
                    }
                }
            };

            Fiddler.FiddlerApplication.BeforeResponse += delegate(Fiddler.Session oS)
            {
                if (oS.fullUrl.Contains(@"https://api.expop.com.cn/v3/app/game/getGameSign.action"))
                {
                    Player player = new Player();
                    player.response_getGameSign(oS);
                }

                if (oS.fullUrl.Contains(@"https://api.expop.com.cn/v3/app/game/commitGameResult.action"))
                {
                    if (oS.LocalProcessID == AllPlayers.curProcessId)
                    {
                        HttpParam param = new HttpParam(oS.GetRequestBodyAsString());
                        Player player = Player.dicSignaturePlayer[(string)param.joBody["signature"]];

                        Fiddler.FiddlerApplication.Log.LogFormat("{0}response: ClientToken:{1}, signature:{2}, result:{3}", zqdbFiddler.strPreNotify, param.strClientToken, player.signature, oS.GetResponseBodyAsString());
                        Player.dicSignaturePlayer.Remove(player.signature);
                    }
                }
            };

            
            Fiddler.CONFIG.IgnoreServerCertErrors = false;
            FiddlerApplication.Prefs.SetBoolPref("fiddler.network.streaming.abortifclientaborts", true);
            FiddlerCoreStartupFlags oFCSF = FiddlerCoreStartupFlags.Default;
            oFCSF = (oFCSF | FiddlerCoreStartupFlags.AllowRemoteClients | FiddlerCoreStartupFlags.CaptureLocalhostTraffic | FiddlerCoreStartupFlags.CaptureFTP);

            CONFIG.bCaptureCONNECT = true;
            CONFIG.IgnoreServerCertErrors = true;
            var cert = InstallCertificate();// getting true

            int iPort = 8888;
            Fiddler.FiddlerApplication.Startup(iPort, oFCSF);
            FiddlerApplication.Log.LogFormat("Created endpoint listening on port {0}", iPort);

            FiddlerApplication.Log.LogFormat("Starting with settings: [{0}]", oFCSF);
            FiddlerApplication.Log.LogFormat("Gateway: {0}", CONFIG.UpstreamGateway.ToString());
        }


        public static void doQuit()
        {
            Fiddler.FiddlerApplication.Shutdown();        
        }
    };
     
}
