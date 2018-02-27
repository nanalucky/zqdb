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
using System.Net;

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
        public JObject joLoginParam;
        public string strTelephone;
        public int nIndex;
        HttpParam pmLogin;
        string strAuthory;      

        void SetHttpRequestHeader(DxWinHttp _http)
        {
            if(AllPlayers.bSetProxy)
                _http.SetProxy(2, "127.0.0.1:8888", "0");

            //_http.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            _http.SetRequestHeader("Accept-Encoding", "identity");
            _http.SetRequestHeader("User-Agent", "Dalvik/2.1.0 (Linux; U; Android 7.1.1; vivo Xplay6 Build/NMF26F)");
            //_http.SetRequestHeader("Host", "gapi.expop.com.cn");
        }

        void SetHttpRequestHeader(DxWinHttp _http, string _sign)
        {
            if (AllPlayers.bSetProxy)
                _http.SetProxy(2, "127.0.0.1:8888", "0");

            _http.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            //_http.SetRequestHeader("Host", "api.expop.com.cn");
            //_http.SetRequestHeader("Connection", "Keep-Alive");
            _http.SetRequestHeader("Accept-Encoding", "gzip");
            _http.SetRequestHeader("User-Agent", "okhttp/3.4.1");
            _http.SetRequestHeader("Encrypt-Sign", _sign);
        }

        bool WaitForResponse(DxWinHttp _http)
        {
            DateTime timeStart = DateTime.Now;
            while (true)
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

        void DownloadMusic(string strFileName)
        {
            string path = AllPlayers.strMusicPath + @"\" + strFileName;
            if (File.Exists(path))
                return;

            // 设置参数
            string url = "http://gmp3.expop.com.cn/music/" + strFileName;
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            //创建本地文件写入流
            Stream stream = new FileStream(path, FileMode.Create);
            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            stream.Close();
            responseStream.Close();
        }

        public void Run()
        {
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
                    Program.form1.richTextBoxStatus_AddString(string.Format("{0}登录失败，放弃\n", strTelephone));
                    return;
                }
            }
            pmLogin.strUserId = (string)joLoginReturn["data"][HttpParam.USERID];
            pmLogin.strUserToken = (string)joLoginReturn["data"][HttpParam.USERTOKEN];
            Program.form1.richTextBoxStatus_AddString(string.Format("{0}登录成功\n", strTelephone));

            //getAuthory
            HttpParam pmGetAuthory = new HttpParam(pmLogin);
            pmGetAuthory.joBody = new JObject();
            JObject joGetAuthoryReturn = new JObject();
            nTimes = 0;
            while (true)
            {
                DxWinHttp http = new DxWinHttp();
                http.Open("POST", "https://api.expop.com.cn:443/v3/app/game/getAuthory.action", true);
                SetHttpRequestHeader(http, pmGetAuthory.GetSign());
                http.Send(pmGetAuthory.GetParam());

                WaitForResponse(http);
                if (http.ResponseBody.Length > 0 && http.ResponseBody.IndexOf(@"""code"":") >= 0)
                {
                    joGetAuthoryReturn = (JObject)JsonConvert.DeserializeObject(http.ResponseBody);
                    if ((string)joGetAuthoryReturn["code"] == @"0")
                    {
                        strAuthory = (string)joGetAuthoryReturn["data"]["authory"];
                        break;
                    }
                }

                nTimes++;
                if (nTimes >= 10)
                {
                    Program.form1.richTextBoxStatus_AddString(string.Format("{0}GetAuthory失败，放弃\n", strTelephone));
                    return;
                }
            }

            
            int nTime = 0;
            while(nTime < 3)
            {
                Program.form1.richTextBoxStatus_AddString(string.Format("最强听音第{0}次\n", nTime));
                nTime++;

                JObject joQlistResult = new JObject();
                int nCreateSetTime = 0;
                while(nCreateSetTime < 3)
                {
                    DxWinHttp http = new DxWinHttp();
                    string szUrl = string.Format("http://gapi.expop.com.cn/Game/CreateSet?UserID={0}&&Diffcult=3&authory={1}", pmLogin.strUserId, strAuthory);
                    http.Open("GET", szUrl, false);
                    SetHttpRequestHeader(http);
                    http.Send("");
                    if (http.ResponseBody.Length > 0 && http.ResponseBody.IndexOf(@"""Qlist"":") >= 0)
                    {
                        joQlistResult = (JObject)JsonConvert.DeserializeObject(http.ResponseBody);
                        Program.form1.richTextBoxStatus_AddString(string.Format("获取问题列表成功\n"));
                        break;
                    }

                    Program.form1.richTextBoxStatus_AddString(string.Format("获取问题列表出错：{0}\n", http.ResponseBody));
                    nCreateSetTime++;
                }

                if (nCreateSetTime >= 3)
                {
                    Program.form1.richTextBoxStatus_AddString(string.Format("获取问题列表出错，放弃\n"));
                    continue;
                }

                
                JArray jaQlist = (JArray)joQlistResult["Qlist"];
                for (int nQlist = 0; nQlist < jaQlist.Count(); ++nQlist)
                {
                    JObject joQuestion = (JObject)jaQlist[nQlist];
                    string szFileName = (string)joQuestion["fileName"];
                    DownloadMusic(szFileName);
                }

                for (int nQlist = 0; nQlist < jaQlist.Count(); ++nQlist)
                {
                    string strQuestion = JsonConvert.SerializeObject((JObject)jaQlist[nQlist]);
                    AllPlayers.RecordQuestion(strQuestion);

                    int nPlayActionTimes = 0;
                    while (nPlayActionTimes < 3)
                    {
                        DxWinHttp httpPlayAction = new DxWinHttp();
                        string szUrlPlayAction = string.Format("http://gapi.expop.com.cn/Game/PlayAction?UserID={0}&now={1}&authory={2}", pmLogin.strUserId, nQlist, strAuthory);
                        httpPlayAction.Open("GET", szUrlPlayAction, false);
                        SetHttpRequestHeader(httpPlayAction);
                        httpPlayAction.Send("");
                        if (httpPlayAction.ResponseBody.Length > 0 && httpPlayAction.ResponseBody.IndexOf(@"""Code"":") >= 0)
                        {
                            Program.form1.richTextBoxStatus_AddString(string.Format("第{0}题\n", nQlist));
                            break;
                        }
                        Program.form1.richTextBoxStatus_AddString(string.Format("答题出错：{0}\n", httpPlayAction.ResponseBody));
                        nPlayActionTimes++;
                    }
                    
                    JObject joQuestion = (JObject)jaQlist[nQlist];
                    string szError = (string)joQuestion["error"];
                    string szFileName = (string)joQuestion["fileName"];
                    string szFileSize = (string)joQuestion["fileSize"];
                    string szStem = (string)joQuestion["stem"];
                    int nType = (int)joQuestion["Type"];

                    switch(nType)
                    {
                        case 1:
                            {
                                int nIndex = szStem.IndexOf("，对吗");
                                if (nIndex < 0)
                                    nIndex = szStem.IndexOf("。对吗");
                                string szSubStem = szStem.Substring(0, nIndex);
                                nIndex = szFileName.IndexOf(".");
                                string szSubFileName = szFileName.Substring(0, nIndex);
                                string szQuestion = szSubStem+szSubFileName;
                                string szAnswer = "";
                                AllPlayers.dic_QuestionFileName_Answer.TryGetValue(szQuestion, out szAnswer);
                                if (szAnswer == null || szAnswer == "")
                                {
                                    Program.form1.richTextBoxStatus_AddString(string.Format("是非题查找出错:{0}\n", szQuestion));
                                    AllPlayers.RecordError(szQuestion);
                                    szAnswer = "不对";
                                }

                                Program.form1.richTextBoxStatus_AddString(string.Format("Type1:{0},{1},{2}\n", szSubStem, szSubFileName, szAnswer));

                                int nAnswerTimes = 0;
                                bool bSuccess = false;
                                while (nAnswerTimes < 3)
                                {
                                    DxWinHttp httpAnswer = new DxWinHttp();
                                    string szUrlAnswer = string.Format("http://gapi.expop.com.cn/Game/Answer?UserID={0}&now={1}&AnswerString={2}&authory={3}", pmLogin.strUserId, nQlist, Uri.EscapeDataString(szAnswer), strAuthory);
                                    httpAnswer.Open("GET", szUrlAnswer, false);
                                    SetHttpRequestHeader(httpAnswer);
                                    httpAnswer.Send("");
                                    if (httpAnswer.ResponseBody.Length > 0 && httpAnswer.ResponseBody.IndexOf(@"""Code"":") >= 0)
                                    {
                                        JObject joAnswer = (JObject)JsonConvert.DeserializeObject(httpAnswer.ResponseBody);
                                        if ((string)joAnswer["Code"] == "0")
                                        {
                                            Program.form1.richTextBoxStatus_AddString(string.Format("答题成功{0}\n", httpAnswer.ResponseBody));
                                            bSuccess = true;
                                        }
                                        else 
                                        {
                                            Program.form1.richTextBoxStatus_AddString(string.Format("答题出错：{0}\n", httpAnswer.ResponseBody));
                                            Program.form1.richTextBoxStatus_AddString(string.Format("答题是：error:{0},fileName:{1},stem:{2}\n", szError, szFileName, szStem));
                                        }
                                        break;
                                    }

                                    Program.form1.richTextBoxStatus_AddString(string.Format("答题出错：{0}\n", httpAnswer.ResponseBody));
                                    nAnswerTimes++;
                                }

                                if (bSuccess)
                                    AllPlayers.RecordQuestionRight(strQuestion);
                                else
                                    AllPlayers.RecordQuestionWrong(strQuestion);
                            }
                            break;
                        case 2:
                            {
                                int nIndex = szFileName.IndexOf(".");
                                string szSubFileName = szFileName.Substring(0, nIndex);
                                string szAnswer = "";
                                List<string> listMusic = new List<string>();
                                AllPlayers.dic_FileName_Music.TryGetValue(szSubFileName, out listMusic);
                                if (listMusic == null || listMusic.Count() == 0)
                                {
                                    Program.form1.richTextBoxStatus_AddString(string.Format("文件名查音乐出错:{0}\n", szSubFileName));
                                    AllPlayers.RecordError(szSubFileName);

                                    string[] arrayError = szError.Split(new string[] { "^^^" }, System.StringSplitOptions.None);
                                    szAnswer = arrayError[0];
                                }
                                else 
                                {
                                    string[] arrayError = szError.Split(new string[] { "^^^" }, System.StringSplitOptions.None);
                                    foreach (string szError1 in arrayError)
                                    {
                                        foreach (string szMusic in listMusic)
                                        {
                                            if (szMusic.Equals(szError1, StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                szAnswer = szError1;
                                                break;
                                            }
                                        }

                                        if (szAnswer != "")
                                            break;
                                    }
                                    if (szAnswer == "")
                                    {
                                        Program.form1.richTextBoxStatus_AddString(string.Format("音乐名不在题目选项中:{0}\n", szSubFileName));
                                        szAnswer = arrayError[0];
                                    }
                                }

                                Program.form1.richTextBoxStatus_AddString(string.Format("Type2:{0},{1}\n", szSubFileName, szAnswer));

                                int nAnswerTimes = 0;
                                bool bSuccess = false;
                                while (nAnswerTimes < 3)
                                {
                                    DxWinHttp httpAnswer = new DxWinHttp();
                                    string szUrlAnswer = string.Format("http://gapi.expop.com.cn/Game/Answer?UserID={0}&now={1}&AnswerString={2}&authory={3}", pmLogin.strUserId, nQlist, Uri.EscapeDataString(szAnswer), strAuthory);
                                    httpAnswer.Open("GET", szUrlAnswer, false);
                                    SetHttpRequestHeader(httpAnswer);
                                    httpAnswer.Send("");
                                    if (httpAnswer.ResponseBody.Length > 0 && httpAnswer.ResponseBody.IndexOf(@"""Code"":") >= 0)
                                    {
                                        JObject joAnswer = (JObject)JsonConvert.DeserializeObject(httpAnswer.ResponseBody);
                                        if ((string)joAnswer["Code"] == "0")
                                        {
                                            Program.form1.richTextBoxStatus_AddString(string.Format("答题成功{0}\n", httpAnswer.ResponseBody));
                                            bSuccess = true;
                                        }
                                        else
                                        {
                                            Program.form1.richTextBoxStatus_AddString(string.Format("答题出错：{0}\n", httpAnswer.ResponseBody));
                                            Program.form1.richTextBoxStatus_AddString(string.Format("答题是：error:{0},fileName:{1},stem:{2}\n", szError, szFileName, szStem));
                                        }
                                        break;
                                    }

                                    Program.form1.richTextBoxStatus_AddString(string.Format("答题出错：{0}\n", httpAnswer.ResponseBody));
                                    nAnswerTimes++;
                                }

                                if (bSuccess)
                                    AllPlayers.RecordQuestionRight(strQuestion);
                                else
                                    AllPlayers.RecordQuestionWrong(strQuestion);
                            }
                            break;
                        case 3:
                            {
                                int nIndex = szFileName.IndexOf(".");
                                string szSubFileName = szFileName.Substring(0, nIndex);
                                string szAnswer = "";
                                string[] arrayLyric = szError.Split(new string[] { "^^^" }, System.StringSplitOptions.None);

                                List<string> listMusic = new List<string>();
                                AllPlayers.dic_FileName_Music.TryGetValue(szSubFileName, out listMusic);
                                if (listMusic == null || listMusic.Count() == 0)
                                {
                                    Program.form1.richTextBoxStatus_AddString(string.Format("文件名查音乐出错:{0}\n", szSubFileName));
                                    AllPlayers.RecordError(szSubFileName);
                                }
                                else
                                {
                                    for (int nLyric = 0; nLyric < arrayLyric.Count(); nLyric++)
                                    {
                                        string szTmpMusic = "";
                                        AllPlayers.dic_Lyric_Music.TryGetValue(arrayLyric[nLyric], out szTmpMusic);
                                        if (szTmpMusic == null || szTmpMusic == "")
                                        {
                                            Program.form1.richTextBoxStatus_AddString(string.Format("歌词查音乐出错:{0}\n", arrayLyric[nLyric]));
                                            AllPlayers.RecordError(arrayLyric[nLyric]);
                                        }
                                        else
                                        {
                                            foreach (string szMusic in listMusic)
                                            {
                                                if (szTmpMusic.Equals(szMusic, StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    szAnswer = arrayLyric[nLyric];
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (szAnswer == "")
                                {
                                    Program.form1.richTextBoxStatus_AddString(string.Format("自动获取答案失败：Type2:{0},{1}\n", szError, szFileName));
                                    szAnswer = arrayLyric[0];
                                }

                                Program.form1.richTextBoxStatus_AddString(string.Format("Type3:{0},{1}\n", szSubFileName, szAnswer));

                                int nAnswerTimes = 0;
                                bool bSuccess = false;
                                while (nAnswerTimes < 3)
                                {
                                    DxWinHttp httpAnswer = new DxWinHttp();
                                    string szUrlAnswer = string.Format("http://gapi.expop.com.cn/Game/Answer?UserID={0}&now={1}&AnswerString={2}&authory={3}", pmLogin.strUserId, nQlist, Uri.EscapeDataString(szAnswer), strAuthory);
                                    httpAnswer.Open("GET", szUrlAnswer, false);
                                    SetHttpRequestHeader(httpAnswer);
                                    httpAnswer.Send("");
                                    if (httpAnswer.ResponseBody.Length > 0 && httpAnswer.ResponseBody.IndexOf(@"""Code"":") >= 0)
                                    {
                                        JObject joAnswer = (JObject)JsonConvert.DeserializeObject(httpAnswer.ResponseBody);
                                        if ((string)joAnswer["Code"] == "0")
                                        {
                                            Program.form1.richTextBoxStatus_AddString(string.Format("答题成功{0}\n", httpAnswer.ResponseBody));
                                            bSuccess = true;
                                        }
                                        else
                                        {
                                            Program.form1.richTextBoxStatus_AddString(string.Format("答题出错：{0}\n", httpAnswer.ResponseBody));
                                            Program.form1.richTextBoxStatus_AddString(string.Format("答题是：error:{0},fileName:{1},stem:{2}\n", szError, szFileName, szStem));
                                        }
                                        break;
                                    }

                                    Program.form1.richTextBoxStatus_AddString(string.Format("答题出错：{0}\n", httpAnswer.ResponseBody));
                                    nAnswerTimes++;
                                }

                                if (bSuccess)
                                    AllPlayers.RecordQuestionRight(strQuestion);
                                else
                                    AllPlayers.RecordQuestionWrong(strQuestion);
                            }
                            break;
                        default:
                            {
                                Program.form1.richTextBoxStatus_AddString(string.Format("遇到新类型：Type{0}\n", nType));
                                Program.form1.richTextBoxStatus_AddString(string.Format("答题是：error:{0},fileName:{1},stem:{2}\n", szError, szFileName, szStem));
                                AllPlayers.RecordQuestionWrong(strQuestion);
                            }
                            break;
                    }
                }
            }


            //getAuthory
            pmGetAuthory = new HttpParam(pmLogin);
            pmGetAuthory.joBody = new JObject();
            joGetAuthoryReturn = new JObject();
            nTimes = 0;
            while (true)
            {
                DxWinHttp http = new DxWinHttp();
                http.Open("POST", "https://api.expop.com.cn:443/v3/app/game/getAuthory.action", true);
                SetHttpRequestHeader(http, pmGetAuthory.GetSign());
                http.Send(pmGetAuthory.GetParam());

                WaitForResponse(http);
                if (http.ResponseBody.Length > 0 && http.ResponseBody.IndexOf(@"""code"":") >= 0)
                {
                    joGetAuthoryReturn = (JObject)JsonConvert.DeserializeObject(http.ResponseBody);
                    if ((string)joGetAuthoryReturn["code"] == @"0")
                    {
                        strAuthory = (string)joGetAuthoryReturn["data"]["authory"];
                        break;
                    }
                }

                nTimes++;
                if (nTimes >= 10)
                {
                    Program.form1.richTextBoxStatus_AddString(string.Format("{0}GetAuthory失败，放弃\n", strTelephone));
                    return;
                }
            }
            

            nTime = 0;
            while (nTime < 3)
            {
                Program.form1.richTextBoxStatus_AddString(string.Format("一题成名第{0}次\n", nTime));
                nTime++;

                JObject joQlistResult = new JObject();
                string szQlistResult = "";
                int nCreateOneTime = 0;
                while (nCreateOneTime < 3)
                {
                    DxWinHttp http = new DxWinHttp();
                    string szUrl = string.Format("http://gapi.expop.com.cn/Game/CreateOne?UserID={0}&authory={1}", pmLogin.strUserId, strAuthory);
                    http.Open("GET", szUrl, false);
                    SetHttpRequestHeader(http);
                    http.Send("");
                    if (http.ResponseBody.Length > 0 && http.ResponseBody.IndexOf(@"""Option"":") >= 0)
                    {
                        szQlistResult = http.ResponseBody;
                        joQlistResult = (JObject)JsonConvert.DeserializeObject(http.ResponseBody);
                        Program.form1.richTextBoxStatus_AddString(string.Format("获取问题列表成功\n"));
                        break;
                    }

                    Program.form1.richTextBoxStatus_AddString(string.Format("获取问题列表出错：{0}\n", http.ResponseBody));
                    nCreateOneTime++;
                }
                if (nCreateOneTime >= 3)
                {
                    Program.form1.richTextBoxStatus_AddString(string.Format("获取问题列表出错，放弃\n"));
                    continue;
                }

                string strQuestion = szQlistResult;
                AllPlayers.RecordQuestionOne(strQuestion);

                JArray jaQlist = (JArray)joQlistResult["question"];
                for (int nQlist = 0; nQlist < jaQlist.Count(); ++nQlist)
                {
                    string szFileName = (string)((JObject)jaQlist[nQlist])["fileName"];
                    DownloadMusic(szFileName);
                }

                int nPlayActionTimes = 0;
                while (nPlayActionTimes < 3)
                {
                    DxWinHttp httpPlayAction = new DxWinHttp();
                    string szUrlPlayAction = string.Format("http://gapi.expop.com.cn/Game/PlayAction?UserID={0}&now=0&authory={1}", pmLogin.strUserId, strAuthory);
                    httpPlayAction.Open("GET", szUrlPlayAction, false);
                    SetHttpRequestHeader(httpPlayAction);
                    httpPlayAction.Send("");
                    if (httpPlayAction.ResponseBody.Length > 0 && httpPlayAction.ResponseBody.IndexOf(@"""Code"":") >= 0)
                    {
                        Program.form1.richTextBoxStatus_AddString(string.Format("第0题\n"));
                        break;
                    }
                    Program.form1.richTextBoxStatus_AddString(string.Format("答题出错：{0}\n", httpPlayAction.ResponseBody));
                    nPlayActionTimes++;
                }

                int nFameType = (int)joQlistResult["fameType"];
                switch (nFameType)
                {
                    case 2:
                        {
                            List<string> listOption = new List<string>();
                            JArray jaOption = (JArray)joQlistResult["Option"];
                            foreach (string szValue in jaOption)
                            {
                                listOption.Add(szValue);
                            }

                            string szAnswer = "";
                            jaQlist = (JArray)joQlistResult["question"];
                            for (int nQlist = 0; nQlist < jaQlist.Count(); ++nQlist)
                            {
                                string szFileName = (string)((JObject)jaQlist[nQlist])["fileName"];
                                int nIndex = szFileName.IndexOf(".");
                                string szSubFileName = szFileName.Substring(0, nIndex);
                                string szMusic = "";
                                List<string> listMusic = new List<string>();
                                AllPlayers.dic_FileName_Music.TryGetValue(szSubFileName, out listMusic);
                                if (listMusic == null || listMusic.Count() == 0)
                                {
                                    Program.form1.richTextBoxStatus_AddString(string.Format("文件名查音乐出错:{0}\n", szSubFileName));
                                    AllPlayers.RecordError(szSubFileName);

                                    szMusic = listOption[0];
                                }
                                else
                                {
                                    foreach (string szOption in listOption)
                                    {
                                        foreach (string szMusic1 in listMusic)
                                        {
                                            if (szMusic1.Equals(szOption, StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                szMusic = szOption;
                                                break;
                                            }
                                        }

                                        if (szMusic != "")
                                            break;
                                    }
                                    if (szMusic == "")
                                    {
                                        Program.form1.richTextBoxStatus_AddString(string.Format("音乐名不在题目选项中:{0}\n", szSubFileName));
                                        szMusic = listOption[0];
                                    }

                                }

                                if (nQlist == 0)
                                    szAnswer = szMusic;
                                else
                                    szAnswer = szAnswer + "#" + szMusic;
                            }

                            Program.form1.richTextBoxStatus_AddString(string.Format("一题成名:{0}\n", szAnswer));

                            int nAnswerTimes = 0;
                            bool bSuccess = false;
                            while (nAnswerTimes < 3)
                            {
                                DxWinHttp httpAnswer = new DxWinHttp();
                                string szUrlAnswer = string.Format("http://gapi.expop.com.cn/Game/AnswerOne?UserID={0}&now=0&AnswerString={1}&authory={2}", pmLogin.strUserId, Uri.EscapeDataString(szAnswer), strAuthory);
                                httpAnswer.Open("GET", szUrlAnswer, false);
                                SetHttpRequestHeader(httpAnswer);
                                httpAnswer.Send("");
                                if (httpAnswer.ResponseBody.Length > 0 && httpAnswer.ResponseBody.IndexOf(@"""Code"":") >= 0)
                                {
                                    JObject joAnswer = (JObject)JsonConvert.DeserializeObject(httpAnswer.ResponseBody);
                                    if ((string)joAnswer["Code"] == "0")
                                    {
                                        Program.form1.richTextBoxStatus_AddString(string.Format("答题成功{0}\n", httpAnswer.ResponseBody));
                                        bSuccess = true;
                                    }
                                    else
                                    {
                                        Program.form1.richTextBoxStatus_AddString(string.Format("答题出错：{0}\n", httpAnswer.ResponseBody));
                                        Program.form1.richTextBoxStatus_AddString(string.Format("答题是：{0}\n", szQlistResult));
                                    }
                                    break;
                                }

                                Program.form1.richTextBoxStatus_AddString(string.Format("答题出错：{0}\n", httpAnswer.ResponseBody));
                                nAnswerTimes++;
                            }

                            if (bSuccess)
                                AllPlayers.RecordQuestionRightOne(strQuestion);
                            else
                                AllPlayers.RecordQuestionWrongOne(strQuestion);
                        }
                        break;
                    case 3:
                        {
                            List<string> listOption = new List<string>();
                            JArray jaOption = (JArray)joQlistResult["Option"];
                            foreach (string szValue in jaOption)
                            {
                                listOption.Add(szValue);
                            }

                            string szAnswer = "";
                            jaQlist = (JArray)joQlistResult["question"];
                            for (int nQlist = 0; nQlist < jaQlist.Count(); ++nQlist)
                            {
                                string szFileName = (string)((JObject)jaQlist[nQlist])["fileName"];
                                int nIndex = szFileName.IndexOf(".");
                                string szSubFileName = szFileName.Substring(0, nIndex);
                                string szLyric = "";
                                AllPlayers.dic_FileName_Lyric.TryGetValue(szSubFileName, out szLyric);
                                if (szLyric == null || szLyric.Count() == 0)
                                {
                                    Program.form1.richTextBoxStatus_AddString(string.Format("文件名查歌词出错:{0}\n", szSubFileName));
                                    AllPlayers.RecordError(szSubFileName);

                                    szLyric = listOption[0];
                                }
                                else
                                {
                                    bool bFound = false;
                                    foreach (string szOption in listOption)
                                    {
                                        if (szOption.Equals(szLyric, StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            bFound = true;
                                            szLyric = szOption;
                                            break;
                                        }
                                    }
                                    if (!bFound)
                                    {
                                        Program.form1.richTextBoxStatus_AddString(string.Format("歌词不在题目选项中:{0},{1}\n", szSubFileName, szLyric));
                                        szLyric = listOption[0];
                                    }

                                }

                                if (nQlist == 0)
                                    szAnswer = szLyric;
                                else
                                    szAnswer = szAnswer + "#" + szLyric;
                            }

                            Program.form1.richTextBoxStatus_AddString(string.Format("一题成名:{0}\n", szAnswer));

                            int nAnswerTimes = 0;
                            bool bSuccess = false;
                            while (nAnswerTimes < 3)
                            {
                                DxWinHttp httpAnswer = new DxWinHttp();
                                string szUrlAnswer = string.Format("http://gapi.expop.com.cn/Game/AnswerOne?UserID={0}&now=0&AnswerString={1}&authory={2}", pmLogin.strUserId, Uri.EscapeDataString(szAnswer), strAuthory);
                                httpAnswer.Open("GET", szUrlAnswer, false);
                                SetHttpRequestHeader(httpAnswer);
                                httpAnswer.Send("");
                                if (httpAnswer.ResponseBody.Length > 0 && httpAnswer.ResponseBody.IndexOf(@"""Code"":") >= 0)
                                {
                                    JObject joAnswer = (JObject)JsonConvert.DeserializeObject(httpAnswer.ResponseBody);
                                    if ((string)joAnswer["Code"] == "0")
                                    {
                                        Program.form1.richTextBoxStatus_AddString(string.Format("答题成功{0}\n", httpAnswer.ResponseBody));
                                        bSuccess = true;
                                    }
                                    else
                                    {
                                        Program.form1.richTextBoxStatus_AddString(string.Format("答题出错：{0}\n", httpAnswer.ResponseBody));
                                        Program.form1.richTextBoxStatus_AddString(string.Format("答题是：{0}\n", szQlistResult));
                                    }
                                    break;
                                }

                                Program.form1.richTextBoxStatus_AddString(string.Format("答题出错：{0}\n", httpAnswer.ResponseBody));
                                nAnswerTimes++;
                            }

                            if (bSuccess)
                                AllPlayers.RecordQuestionRightOne(strQuestion);
                            else
                                AllPlayers.RecordQuestionWrongOne(strQuestion);
                        }
                        break;
                }
            }

            Program.form1.richTextBoxStatus_AddString(string.Format("答题完成\n"));
            Program.form1.button1_Enabled();
            return;
        }
    };

 
    class AllPlayers
    {
        public static bool bSetProxy = false;
        //public static string strUserId = "";
        public static Dictionary<string, string> dic_Lyric_Music = new Dictionary<string, string>();
        public static Dictionary<string, List<string>> dic_FileName_Music = new Dictionary<string, List<string>>();
        public static Dictionary<string, string> dic_QuestionFileName_Answer = new Dictionary<string, string>();
        public static Dictionary<string, string> dic_Lyric_FileName = new Dictionary<string, string>();
        public static Dictionary<string, string> dic_FileName_Lyric = new Dictionary<string, string>();
        public static string szConfigError = "";

        public static string strApiVer = @"";
        public static string strClientVer = @"";
        public static string strClientType = @"";
        public static string strConfigFileName = @"";
        public static string strAccountFileName = @"";
        public static string strQuestionFileName = @"";
        public static string strQuestionWrongFileName = @"";
        public static string strQuestionRightFileName = @"";
        public static string strQuestionOneFileName = @"";
        public static string strQuestionWrongOneFileName = @"";
        public static string strQuestionRightOneFileName = @"";

        public static string strMusicPath;
        List<Player> listPlayer = new List<Player>();

        public static bool bInit = false;

        public void Init()
        {
            if (bInit)
                return;

            bInit = true;

            strMusicPath = System.Environment.CurrentDirectory + @"\music";
            if (!Directory.Exists(strMusicPath))
            {
                Directory.CreateDirectory(strMusicPath);
            }

            string szConfigLyricMusic = System.Environment.CurrentDirectory + @"\" + @"config_lyric_music.csv";
            string szConfigFileNameMusic = System.Environment.CurrentDirectory + @"\" + @"config_filename_music.csv";
            string szConfigYesNo = System.Environment.CurrentDirectory + @"\" + @"config_yesno.csv";
            string szConfigLyricFileName = System.Environment.CurrentDirectory + @"\" + @"config_lyric_filename.csv";
            szConfigError = System.Environment.CurrentDirectory + @"\" + @"config_error.csv";
            strConfigFileName = System.Environment.CurrentDirectory + @"\" + @"config_login.txt";
            strAccountFileName = System.Environment.CurrentDirectory + @"\" + @"config_account.csv";
            strQuestionFileName = System.Environment.CurrentDirectory + @"\" + @"out_set_all.csv";
            strQuestionWrongFileName = System.Environment.CurrentDirectory + @"\" + @"out_set_wrong.csv";
            strQuestionRightFileName = System.Environment.CurrentDirectory + @"\" + @"out_set_right.csv";
            strQuestionOneFileName = System.Environment.CurrentDirectory + @"\" + @"out_one_all.csv";
            strQuestionWrongOneFileName = System.Environment.CurrentDirectory + @"\" + @"out_one_wrong.csv";
            strQuestionRightOneFileName = System.Environment.CurrentDirectory + @"\" + @"out_one_right.csv";

            string[] arrayConfig = File.ReadAllLines(strConfigFileName);
            string[] arrayText = File.ReadAllLines(strAccountFileName);

            JObject joInfo = (JObject)JsonConvert.DeserializeObject(arrayConfig[0]);
            strApiVer = (string)joInfo[HttpParam.APIVER];
            strClientVer = (string)joInfo[HttpParam.CLIENTVER];
            strClientType = (string)joInfo[HttpParam.CLIENTTYPE];


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

                Player player = new Player();
                player.nIndex = i - nInit;
                player.joLoginParam = joParam;
                player.strTelephone = (string)joParam[HttpParam.BODY]["phone"];
                //player.thread.Start();
                listPlayer.Add(player);
            }

            arrayText = File.ReadAllLines(szConfigYesNo);
            for (int i = 0; i < arrayText.Length; ++i)
            {
                string[] arrayParam = arrayText[i].Split(new string[] { "####" }, System.StringSplitOptions.None);
                if (arrayParam.Length >= 3)
                {
                    dic_QuestionFileName_Answer.Add(arrayParam[0] + arrayParam[1], arrayParam[2]);                    
                }
            }

            arrayText = File.ReadAllLines(szConfigLyricMusic);
            for (int i = 0; i < arrayText.Length; ++i)
            {
                string[] arrayParam = arrayText[i].Split(new string[] { "####" }, System.StringSplitOptions.None);
                if (arrayParam.Length >= 2)
                    dic_Lyric_Music.Add(arrayParam[0], arrayParam[1]);
            }

            arrayText = File.ReadAllLines(szConfigLyricFileName);
            for (int i = 0; i < arrayText.Length; ++i)
            {
                string[] arrayParam = arrayText[i].Split(new string[] { "####" }, System.StringSplitOptions.None);
                if (arrayParam.Length >= 2)
                {
                    //dic_Lyric_FileName.Add(arrayParam[0], arrayParam[1]);
                    dic_FileName_Lyric.Add(arrayParam[1], arrayParam[0]);
                }
            }

            arrayText = File.ReadAllLines(szConfigFileNameMusic);
            for (int i = 0; i < arrayText.Length; ++i)
            {
                string[] arrayParam = arrayText[i].Split(new string[] { "####" }, System.StringSplitOptions.None);
                if (arrayParam.Length >= 3)
                {
                    List<string> listMusic = new List<string>();
                    for (int nMusic = 2; nMusic < arrayParam.Length; ++nMusic)
                        listMusic.Add(arrayParam[nMusic]);
                    dic_FileName_Music.Add(arrayParam[1], listMusic);
                }
            }

            Program.form1.Form1_Init();
        }

        public static void RecordError(string strError)
        {
            FileStream fs = File.Open(szConfigError, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(string.Format("{0}\r\n", strError));
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public static void RecordQuestion(string strQuestion)
        {
            FileStream fs = File.Open(strQuestionFileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(string.Format("{0}\r\n", strQuestion));
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public static void RecordQuestionWrong(string strQuestion)
        {
            FileStream fs = File.Open(strQuestionWrongFileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(string.Format("{0}\r\n", strQuestion));
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public static void RecordQuestionRight(string strQuestion)
        {
            FileStream fs = File.Open(strQuestionRightFileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(string.Format("{0}\r\n", strQuestion));
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public static void RecordQuestionOne(string strQuestion)
        {
            FileStream fs = File.Open(strQuestionOneFileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(string.Format("{0}\r\n", strQuestion));
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public static void RecordQuestionWrongOne(string strQuestion)
        {
            FileStream fs = File.Open(strQuestionWrongOneFileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(string.Format("{0}\r\n", strQuestion));
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public static void RecordQuestionRightOne(string strQuestion)
        {
            FileStream fs = File.Open(strQuestionRightOneFileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(string.Format("{0}\r\n", strQuestion));
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public void Run()
        {
            if (dic_QuestionFileName_Answer.Count() == 0 || dic_FileName_Music.Count() == 0 || dic_Lyric_Music.Count() == 0)
                return;

            if (Program.form1.textBoxSetProxy_GetProxy().Equals("0"))
                bSetProxy = false;
            else
                bSetProxy = true;
                        
            Thread thread = new Thread(RunPlayerOneByOne);
            thread.Start();
        }

        public void RunPlayerOneByOne()
        {
            foreach (Player player in listPlayer)
            {
                player.Run();
            }
        }
   
    };
     
}
