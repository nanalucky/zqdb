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
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing;

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
       
        void SetHttpRequestHeader(DxWinHttp _http)
        {
            if(AllPlayers.bSetProxy)
                _http.SetProxy(2, "127.0.0.1:8888", "0");

            //_http.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            _http.SetRequestHeader("Accept-Encoding", "identity");
            _http.SetRequestHeader("User-Agent", "Dalvik/2.1.0 (Linux; U; Android 7.1.1; vivo Xplay6 Build/NMF26F)");
            _http.SetRequestHeader("Host", "gapi.expop.com.cn");
        }


        public void Run()
        {
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
                    string szUrl = string.Format("http://gapi.expop.com.cn/Game/CreateSet?UserID={0}&&Diffcult=3", AllPlayers.strUserId);
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
                    int nPlayActionTimes = 0;
                    while (nPlayActionTimes < 3)
                    {
                        DxWinHttp httpPlayAction = new DxWinHttp();
                        string szUrlPlayAction = string.Format("http://gapi.expop.com.cn/Game/PlayAction?UserID={0}&now={1}", AllPlayers.strUserId, nQlist);
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
                                while (nAnswerTimes < 3)
                                {
                                    DxWinHttp httpAnswer = new DxWinHttp();
                                    string szUrlAnswer = string.Format("http://gapi.expop.com.cn/Game/Answer?UserID={0}&now={1}&AnswerString={2}", AllPlayers.strUserId, nQlist, Uri.EscapeDataString(szAnswer));
                                    httpAnswer.Open("GET", szUrlAnswer, false);
                                    SetHttpRequestHeader(httpAnswer);
                                    httpAnswer.Send("");
                                    if (httpAnswer.ResponseBody.Length > 0 && httpAnswer.ResponseBody.IndexOf(@"""Code"":") >= 0)
                                    {
                                        JObject joAnswer = (JObject)JsonConvert.DeserializeObject(httpAnswer.ResponseBody);
                                        if ((string)joAnswer["Code"] == "0")
                                        {
                                            Program.form1.richTextBoxStatus_AddString(string.Format("答题成功{0}\n", httpAnswer.ResponseBody));
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
                                while (nAnswerTimes < 3)
                                {
                                    DxWinHttp httpAnswer = new DxWinHttp();
                                    string szUrlAnswer = string.Format("http://gapi.expop.com.cn/Game/Answer?UserID={0}&now={1}&AnswerString={2}", AllPlayers.strUserId, nQlist, Uri.EscapeDataString(szAnswer));
                                    httpAnswer.Open("GET", szUrlAnswer, false);
                                    SetHttpRequestHeader(httpAnswer);
                                    httpAnswer.Send("");
                                    if (httpAnswer.ResponseBody.Length > 0 && httpAnswer.ResponseBody.IndexOf(@"""Code"":") >= 0)
                                    {
                                        JObject joAnswer = (JObject)JsonConvert.DeserializeObject(httpAnswer.ResponseBody);
                                        if ((string)joAnswer["Code"] == "0")
                                        {
                                            Program.form1.richTextBoxStatus_AddString(string.Format("答题成功{0}\n", httpAnswer.ResponseBody));
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
                                while (nAnswerTimes < 3)
                                {
                                    DxWinHttp httpAnswer = new DxWinHttp();
                                    string szUrlAnswer = string.Format("http://gapi.expop.com.cn/Game/Answer?UserID={0}&now={1}&AnswerString={2}", AllPlayers.strUserId, nQlist, Uri.EscapeDataString(szAnswer));
                                    httpAnswer.Open("GET", szUrlAnswer, false);
                                    SetHttpRequestHeader(httpAnswer);
                                    httpAnswer.Send("");
                                    if (httpAnswer.ResponseBody.Length > 0 && httpAnswer.ResponseBody.IndexOf(@"""Code"":") >= 0)
                                    {
                                        JObject joAnswer = (JObject)JsonConvert.DeserializeObject(httpAnswer.ResponseBody);
                                        if ((string)joAnswer["Code"] == "0")
                                        {
                                            Program.form1.richTextBoxStatus_AddString(string.Format("答题成功{0}\n", httpAnswer.ResponseBody));
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
                            }
                            break;
                        default:
                            {
                                Program.form1.richTextBoxStatus_AddString(string.Format("遇到新类型：Type{0}\n", nType));
                                Program.form1.richTextBoxStatus_AddString(string.Format("答题是：error:{0},fileName:{1},stem:{2}\n", szError, szFileName, szStem));
                            }
                            break;
                    }
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
                    string szUrl = string.Format("http://gapi.expop.com.cn/Game/CreateOne?UserID={0}", AllPlayers.strUserId);
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

                int nPlayActionTimes = 0;
                while (nPlayActionTimes < 3)
                {
                    DxWinHttp httpPlayAction = new DxWinHttp();
                    string szUrlPlayAction = string.Format("http://gapi.expop.com.cn/Game/PlayAction?UserID={0}&now=0", AllPlayers.strUserId);
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
                            JArray jaQlist = (JArray)joQlistResult["question"];
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
                            while (nAnswerTimes < 3)
                            {
                                DxWinHttp httpAnswer = new DxWinHttp();
                                string szUrlAnswer = string.Format("http://gapi.expop.com.cn/Game/AnswerOne?UserID={0}&now=0&AnswerString={1}", AllPlayers.strUserId, Uri.EscapeDataString(szAnswer));
                                httpAnswer.Open("GET", szUrlAnswer, false);
                                SetHttpRequestHeader(httpAnswer);
                                httpAnswer.Send("");
                                if (httpAnswer.ResponseBody.Length > 0 && httpAnswer.ResponseBody.IndexOf(@"""Code"":") >= 0)
                                {
                                    JObject joAnswer = (JObject)JsonConvert.DeserializeObject(httpAnswer.ResponseBody);
                                    if ((string)joAnswer["Code"] == "0")
                                    {
                                        Program.form1.richTextBoxStatus_AddString(string.Format("答题成功{0}\n", httpAnswer.ResponseBody));
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
                            JArray jaQlist = (JArray)joQlistResult["question"];
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
                            while (nAnswerTimes < 3)
                            {
                                DxWinHttp httpAnswer = new DxWinHttp();
                                string szUrlAnswer = string.Format("http://gapi.expop.com.cn/Game/AnswerOne?UserID={0}&now=0&AnswerString={1}", AllPlayers.strUserId, Uri.EscapeDataString(szAnswer));
                                httpAnswer.Open("GET", szUrlAnswer, false);
                                SetHttpRequestHeader(httpAnswer);
                                httpAnswer.Send("");
                                if (httpAnswer.ResponseBody.Length > 0 && httpAnswer.ResponseBody.IndexOf(@"""Code"":") >= 0)
                                {
                                    JObject joAnswer = (JObject)JsonConvert.DeserializeObject(httpAnswer.ResponseBody);
                                    if ((string)joAnswer["Code"] == "0")
                                    {
                                        Program.form1.richTextBoxStatus_AddString(string.Format("答题成功{0}\n", httpAnswer.ResponseBody));
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
        public static string strUserId = "";
        public static Dictionary<string, string> dic_Lyric_Music = new Dictionary<string, string>();
        public static Dictionary<string, List<string>> dic_FileName_Music = new Dictionary<string, List<string>>();
        public static Dictionary<string, string> dic_QuestionFileName_Answer = new Dictionary<string, string>();
        public static Dictionary<string, string> dic_Lyric_FileName = new Dictionary<string, string>();
        public static Dictionary<string, string> dic_FileName_Lyric = new Dictionary<string, string>();
        public static string szConfigError = "";

        public static bool bInit = false;

        public static int index = -1;

        [DllImport("OCR.dll")]
        public static extern StringBuilder CNN_OCR(int index, byte[] FileBuffer, int imglen, int zxd);
        [DllImport("OCR.dll")]
        public static extern int LCNN_INIT(string path, string ps, int threads);

        public static byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public static string CNN(string imgPath)
        {
            Image img1 = Image.FromFile(imgPath);
            byte[] img = ImageToBytes(img1);
            StringBuilder sb = CNN_OCR(1, img, img.Length, 0);
            return sb.ToString();
        }

        public void CNN1()
        {
            Thread.Sleep(0);
            string Result = CNN(System.Environment.CurrentDirectory + "\\pic.bmp");
            Console.WriteLine(string.Format("result {0}:{1}", Thread.CurrentThread.GetHashCode(), Result));
        }

        public string GetCodeFromOcr()
        {
            try
            {
                if (index == -1)
                {
                    index = LCNN_INIT(System.Environment.CurrentDirectory + "\\Newibailian.cnn", "", 100);
                    //MessageBox.Show("初始化失败！");
                    //return "";
                }

                for (int i = 0; i < 30; i++)
                {
                    Thread thread = new Thread(CNN1);
                    thread.Start();
                }

                Thread.Sleep(10000);

                for (int i = 0; i < 30; i++)
                {
                    Thread thread = new Thread(CNN1);
                    thread.Start();
                }

                Thread.Sleep(10000);

                for (int i = 0; i < 30; i++)
                {
                    Thread thread = new Thread(CNN1);
                    thread.Start();
                }

                Thread.Sleep(10000);
                
                //string Result = CNN_OCR(data, data.Length, index);
                string Result = CNN(Application.StartupPath + "\\securityCode.jpg");
                return Result;
            }
            catch
            {
                return string.Empty;
            }
        }
        
        
        
        public void Init()
        {
            if (bInit)
                return;

            bInit = true;

            GetCodeFromOcr();
            return;
            
            string szConfigLyricMusic = System.Environment.CurrentDirectory + @"\" + @"config_lyric_music.csv";
            string szConfigFileNameMusic = System.Environment.CurrentDirectory + @"\" + @"config_filename_music.csv";
            string szConfigYesNo = System.Environment.CurrentDirectory + @"\" + @"config_yesno.csv";
            string szConfigLyricFileName = System.Environment.CurrentDirectory + @"\" + @"config_lyric_filename.csv";
            szConfigError = System.Environment.CurrentDirectory + @"\" + @"config_error.csv";

            string[] arrayText = File.ReadAllLines(szConfigYesNo);
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
                string[] arrayParam = arrayText[i].Split(new char[] { ',' });
                if (arrayParam.Length >= 2)
                    dic_Lyric_Music.Add(arrayParam[0], arrayParam[1]);
            }

            arrayText = File.ReadAllLines(szConfigLyricFileName);
            for (int i = 0; i < arrayText.Length; ++i)
            {
                string[] arrayParam = arrayText[i].Split(new char[] { ',' });
                if (arrayParam.Length >= 2)
                {
                    dic_Lyric_FileName.Add(arrayParam[0], arrayParam[1]);
                    dic_FileName_Lyric.Add(arrayParam[1], arrayParam[0]);
                }
            }

            arrayText = File.ReadAllLines(szConfigFileNameMusic);
            for (int i = 0; i < arrayText.Length; ++i)
            {
                string[] arrayParam = arrayText[i].Split(new char[] { ',' });
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

        public void Run()
        {
            if (dic_QuestionFileName_Answer.Count() == 0 || dic_FileName_Music.Count() == 0 || dic_Lyric_Music.Count() == 0)
                return;

            if (Program.form1.textBoxSetProxy_GetProxy().Equals("0"))
                bSetProxy = false;
            else
                bSetProxy = true;

            strUserId = Program.form1.textBoxUserId_GetUserId();
            
            Player player = new Player();
            Thread thread = new Thread(player.Run);
            thread.Start();
        }
   
    };
     
}
