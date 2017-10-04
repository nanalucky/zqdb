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
       
        void SetHttpRequestHeader(DxWinHttp _http, string _sign)
        {
            if(AllPlayers.bSetProxy)
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

 
        public void Run()
        {
            JObject paramSignature = (JObject)JsonConvert.DeserializeObject(AllPlayers.strParamSignature);
            string signature = (string)paramSignature["data"]["signature"];
            HttpParam pmGetGameSign = new HttpParam(AllPlayers.strParam);
            int type = (int)pmGetGameSign.joBody["type"];
            int userTime = (new Random()).Next(14000, 15000);

            HttpParam pmCommitGameResult = new HttpParam(AllPlayers.strParam);
            pmCommitGameResult.joBody = new JObject(
                    new JProperty("type", type),
                    new JProperty("useTime", userTime),
                    new JProperty("signature", signature),
                    new JProperty("score", int.Parse(Program.form1.textBoxScore_GetScore()))
                    );

            // 检查signature
            bool bFound = false;
            bool bHasLine = false;
            if (File.Exists(AllPlayers.strConfigFileName))
            {
                string[] arrayText = File.ReadAllLines(AllPlayers.strConfigFileName);
                if (arrayText.Length > 0)
                {
                    bHasLine = true;
                    for (int i = 0; i < arrayText.Length; ++i)
                    {
                        if (arrayText[i].IndexOf(signature) >= 0)
                        {
                            bFound = true;
                            break;
                        }
                    }
                }
            }

            if (bFound)
            {
                System.Windows.Forms.MessageBox.Show(Program.form1, string.Format("重复{0}", signature));
                return;
            }

            FileStream fs = File.Open(AllPlayers.strConfigFileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            if (bHasLine)
            {
                sw.Write(string.Format("\r\n{0}", signature));
            }
            else
            {
                sw.Write(signature);
            }
            sw.Flush();
            sw.Close();
            fs.Close();

            DxWinHttp http = new DxWinHttp();
            http.Open("POST", HttpParam.URL + AllPlayers.strUrl, true);
            SetHttpRequestHeader(http, pmCommitGameResult.GetSign());
            http.Send(pmCommitGameResult.GetParam());

            System.Windows.Forms.MessageBox.Show(Program.form1, "消息发送完毕");
            return;
        }
    };

    class AllPlayers
    {
        public static bool bSetProxy = false;
        public static string strConfigFileName = @"";
        public static string strAccountFileName = @"";

        public static string strParam = @"";
        public static string strUrl = @"";
        public static string strParamSignature = @"";

        public void Init()
        {
            if (Program.form1.textBoxSetProxy_GetProxy().Equals("0"))
                bSetProxy = false;
            else
                bSetProxy = true;

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = desktop;
            openFileDialog.Filter = "txt File(*.txt)|*.txt";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                strAccountFileName = openFileDialog.FileName;
            }
            else
            {
                return;
            }
            strConfigFileName = System.Environment.CurrentDirectory + @"\" + @"config.csv";

            string[] arrayText = File.ReadAllLines(strAccountFileName);
            for (int i = 0; i < arrayText.Length; ++i)
            {
                if (arrayText[i].IndexOf("params=") == 0)
                {
                    strParam = arrayText[i];
                }

                if (arrayText[i].IndexOf("{\"code\":") == 0)
                {
                    strParamSignature = arrayText[i];
                }
            }
            strUrl = @"game/commitGameResult.action";

            Program.form1.Form1_Init();
        }


        public void Run()
        {
            if (strAccountFileName == "")
                return;

            Player player = new Player();
            player.Run();
        }
   
    };

    public class zqdbFiddler
    {
        static Proxy oSecureEndpoint;
        static string sSecureEndpointHostname = "localhost";
        static int iSecureEndpointPort = 7777;

        
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
            Fiddler.FiddlerApplication.OnNotification += delegate(object sender, NotificationEventArgs oNEA) { Program.form1.richTextBoxFiddler_AddString("** NotifyUser: " + oNEA.NotifyString + "\n"); };
            Fiddler.FiddlerApplication.Log.OnLogString += delegate(object sender, LogEventArgs oLEA) { Program.form1.richTextBoxFiddler_AddString("** LogString: " + oLEA.LogString + "\n"); };

            Fiddler.FiddlerApplication.BeforeRequest += delegate(Fiddler.Session oS)
            {
                // Console.WriteLine("Before request for:\t" + oS.fullUrl);
                // In order to enable response tampering, buffering mode MUST
                // be enabled; this allows FiddlerCore to permit modification of
                // the response in the BeforeResponse handler rather than streaming
                // the response to the client as the response comes in.
                oS.bBufferResponse = false;

                Fiddler.FiddlerApplication.Log.LogFormat("Request URL {0}", oS.fullUrl);// getting only http traffic details

                // Set this property if you want FiddlerCore to automatically authenticate by
                // answering Digest/Negotiate/NTLM/Kerberos challenges itself
                // oS["X-AutoAuth"] = "(default)";

                /* If the request is going to our secure endpoint, we'll echo back the response.
                
                Note: This BeforeRequest is getting called for both our main proxy tunnel AND our secure endpoint, 
                so we have to look at which Fiddler port the client connected to (pipeClient.LocalPort) to determine whether this request 
                was sent to secure endpoint, or was merely sent to the main proxy tunnel (e.g. a CONNECT) in order to *reach* the secure endpoint.

                As a result of this, if you run the demo and visit https://localhost:7777 in your browser, you'll see

                Session list contains...
                 
                    1 CONNECT http://localhost:7777
                    200                                         <-- CONNECT tunnel sent to the main proxy tunnel, port 8877

                    2 GET https://localhost:7777/
                    200 text/html                               <-- GET request decrypted on the main proxy tunnel, port 8877

                    3 GET https://localhost:7777/               
                    200 text/html                               <-- GET request received by the secure endpoint, port 7777
                */

                if ((oS.oRequest.pipeClient.LocalPort == iSecureEndpointPort) && (oS.hostname == sSecureEndpointHostname))
                {
                    oS.utilCreateResponseAndBypassServer();
                    oS.oResponse.headers.SetStatus(200, "Ok");
                    oS.oResponse["Content-Type"] = "text/html; charset=UTF-8";
                    oS.oResponse["Cache-Control"] = "private, max-age=0";
                    oS.utilSetResponseBody("<html><body>Request for httpS://" + sSecureEndpointHostname + ":" + iSecureEndpointPort.ToString() + " received. Your request was:<br /><plaintext>" + oS.oRequest.headers.ToString());
                }
            };

            Fiddler.CONFIG.IgnoreServerCertErrors = false;
            FiddlerApplication.Prefs.SetBoolPref("fiddler.network.streaming.abortifclientaborts", true);
            FiddlerCoreStartupFlags oFCSF = FiddlerCoreStartupFlags.Default;
            oFCSF = (oFCSF | FiddlerCoreStartupFlags.AllowRemoteClients | FiddlerCoreStartupFlags.CaptureLocalhostTraffic | FiddlerCoreStartupFlags.CaptureFTP);

            CONFIG.bCaptureCONNECT = true;
            CONFIG.IgnoreServerCertErrors = true;
            var cert = InstallCertificate();// getting true

            int iPort = 8877;
            Fiddler.FiddlerApplication.Startup(iPort, oFCSF);
            FiddlerApplication.Log.LogFormat("Created endpoint listening on port {0}", iPort);

            FiddlerApplication.Log.LogFormat("Starting with settings: [{0}]", oFCSF);
            FiddlerApplication.Log.LogFormat("Gateway: {0}", CONFIG.UpstreamGateway.ToString());

            oSecureEndpoint = FiddlerApplication.CreateProxyEndpoint(iSecureEndpointPort, true, sSecureEndpointHostname);
            if (null != oSecureEndpoint)
            {
                FiddlerApplication.Log.LogFormat("Created secure endpoint listening on port {0}, using a HTTPS certificate for '{1}'", iSecureEndpointPort, sSecureEndpointHostname);
            }
        }


        public static void doQuit()
        {
            if (null != oSecureEndpoint) oSecureEndpoint.Dispose();
            Fiddler.FiddlerApplication.Shutdown();        
        }
    };
     
}
