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
        HttpParam pmLogin;
        public JObject joLoginParam;
        public int nIndex;
        
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
            // login.action
            Program.form1.richTextBoxStatus_AddString("开始登录...");
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
                    Program.form1.richTextBoxStatus_AddString("登录失败！");
                    return;
                }
            }
            pmLogin.strUserId = (string)joLoginReturn["data"][HttpParam.USERID];
            pmLogin.strUserToken = (string)joLoginReturn["data"][HttpParam.USERTOKEN];
            Program.form1.richTextBoxStatus_AddString("登录成功！");

            // signIn.action
            Program.form1.richTextBoxStatus_AddString("开始签到...");
            HttpParam pmSignIn = new HttpParam(pmLogin);
            JObject joSignIn = new JObject();
            while (true)
            {
                DxWinHttp httpSignIn = new DxWinHttp();
                httpSignIn.Open("POST", HttpParam.URL + @"integral/signIn.action", true);
                SetHttpRequestHeader(httpSignIn, pmSignIn.GetSign());
                httpSignIn.Send(pmSignIn.GetParam());

                WaitForResponse(httpSignIn);
                if (httpSignIn.ResponseBody.Length > 0 && httpSignIn.ResponseBody.IndexOf(@"""code"":") >= 0)
                {
                    joSignIn = (JObject)JsonConvert.DeserializeObject(httpSignIn.ResponseBody);
                    if ((string)joSignIn["code"] == @"0")
                    {
                        Program.form1.richTextBoxStatus_AddString("签到成功!");
                    }
                    else
                    {
                        JToken outMsg;
                        if (joSignIn.TryGetValue("msg", out outMsg) && outMsg.Type != JTokenType.Null)
                        {
                            Program.form1.richTextBoxStatus_AddString(string.Format("签到失败：{0}", (string)joSignIn["msg"]));
                        }
                        else 
                        {
                            Program.form1.richTextBoxStatus_AddString("签到失败!");
                        }
                    }
                    break;
                }
            }
        }
    };

    class PostInfo
    {
        public string title;
        public string content;

        public PostInfo(JObject joPost)
        {
            title = (string)joPost["title"];
            content = (string)joPost["content"];
        }
    };

    class AllPlayers
    {
        public static bool bSetProxy = false;
        public static string strApiVer = @"";
        public static string strClientVer = @"";
        public static string strClientType = @"";
        public static string strConfigFileName = @"";
        public static string strAccountFileName = @"";
        public static List<PostInfo> listPostInfo = new List<PostInfo>();
        
        Player player = new Player();

        public void Init()
        {
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

            strConfigFileName = System.Environment.CurrentDirectory + @"\" + @"config.txt";
            string strConfigFileName = System.Environment.CurrentDirectory + @"\" + @"config.txt";
            string[] arrayText = File.ReadAllLines(strConfigFileName); 
            
            string[] arrayConfig = File.ReadAllLines(strConfigFileName);
            string[] arrayText = File.ReadAllLines(strAccountFileName);

            JObject joInfo = (JObject)JsonConvert.DeserializeObject(arrayConfig[0]);
            JArray jaPost = (JArray)joInfo["post"];
            strApiVer = (string)joInfo[HttpParam.APIVER];
            strClientVer = (string)joInfo[HttpParam.CLIENTVER];
            strClientType = (string)joInfo[HttpParam.CLIENTTYPE];
            if ((string)joInfo["SetProxy"] == @"0")
                bSetProxy = false;
            else
                bSetProxy = true;
            foreach (JObject joPost in jaPost)
            {
                PostInfo cinfo = new PostInfo(joPost);
                listPostInfo.Add(cinfo);
            }

            Program.form1.Form1_Init();

            int nInit = 1;
            for (int i = nInit; i < arrayText.Length; ++i)
            {
                string[] arrayParam = arrayText[i].Split(new char[] { ',' }); 

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

                player.nIndex = i - nInit;
                player.joLoginParam = joParam;
                player.strTelephone = (string)joParam[HttpParam.BODY]["phone"];
                break;
            }
        }


        public void Run()
        {
            player.Run();
        }
    };
     
}
