﻿/*
 * Created by SharpDevelop.
 * 作者: 不得闲
 * Date: 2009-12-2
 * Time: 23:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace DxComOperate
{
    /// <summary>
    /// COM对象的后期绑定调用类库	
    /// </summary>
    public class DxComObject
    {
        private System.Type _ObjType;
        private object ComInstance;
        /*public DxComObject()
        {
            throw new
        }*/
        public DxComObject(string ComName)
        {
            //根据COM对象的名称创建COM对象
            _ObjType = System.Type.GetTypeFromProgID(ComName);
            if (_ObjType == null)
                throw new Exception("指定的COM对象名称无效");
            ComInstance = System.Activator.CreateInstance(_ObjType);
        }
        public System.Type ComType
        {
            get { return _ObjType; }
        }
        //执行的函数
        public object DoMethod(string MethodName, object[] args)
        {
            return ComType.InvokeMember(MethodName, System.Reflection.BindingFlags.InvokeMethod, null, ComInstance, args);
        }

        public object DoMethod(string MethodName, object[] args, System.Reflection.ParameterModifier[] ParamMods)
        {
            return ComType.InvokeMember(MethodName, System.Reflection.BindingFlags.InvokeMethod, null, ComInstance, args, ParamMods, null, null);
        }
        //获得属性与设置属性
        public object this[string propName]
        {
            get
            {
                return _ObjType.InvokeMember(propName, System.Reflection.BindingFlags.GetProperty, null, ComInstance, null);
            }
            set
            {
                _ObjType.InvokeMember(propName, System.Reflection.BindingFlags.SetProperty, null, ComInstance, new object[] { value });
            }
        }
    }


    /// <summary>
    /// WinHttp对象库
    /// </summary>
    public class DxWinHttp
    {
        private DxComObject HttpObj;
        private string _ContentType;
        private int _ContentLength;
        private bool _Active;
        private System.Collections.ArrayList PostDataList;//提交的数据字段
        public DxWinHttp()
        {
            //构建WinHttp对象
            HttpObj = new DxComObject("WinHttp.WinHttpRequest.5.1");
            _ContentType = "application/x-www-form-urlencoded";
            _ContentLength = 0;
            PostDataList = new System.Collections.ArrayList();
        }

        //提交参数信息的个数
        public int PostDataCount
        {
            get { return PostDataList.Count; }
        }
        //设置Content-Type属性
        public string ContentType
        {
            get { return _ContentType; }
            set
            {
                if (!_Active) _ContentType = value;
                else if (_ContentType.CompareTo(value) != 0)
                {
                    _ContentType = value;
                    SetRequestHeader("Content-Type", _ContentType);
                }
            }
        }

        //对象是否是打开状态
        public bool Active
        {
            get { return _Active; }
        }

        //设置Send数据的长度 
        public int ContentLength
        {
            get { return _ContentLength; }
            set
            {
                if (!_Active) _ContentLength = value;
                else if (_ContentLength != value)
                {
                    _ContentLength = value;
                    HttpObj.DoMethod("SetRequestHeader", new object[2] { "Content-Length", value });
                }
            }
        }

        //执行之后返回的结果
        public string ResponseBody
        {
            get
            {
                if (_Active)
                {
                    DxComObject AdoStream = new DxComObject("Adodb.Stream");
                    AdoStream["Type"] = 1;
                    AdoStream["Mode"] = 3;
                    AdoStream.DoMethod("Open", new object[] { });
                    AdoStream.DoMethod("Write", new object[1] { HttpObj["ResponseBody"] });
                    AdoStream["Position"] = 0;
                    AdoStream["Type"] = 2;
                    AdoStream["Charset"] = "UTF-8";
                    return AdoStream["ReadText"].ToString();
                }
                else return "";
            }
        }

        //设定请求头
        public string SetRequestHeader(string Header, object Value)
        {
            object obj;
            obj = HttpObj.DoMethod("SetRequestHeader", new object[] { Header, Value });
            if (obj != null) return obj.ToString();
            else return "True";
        }

        //打开URL执行OpenMethod方法,Async指定是否采用异步方式调用,异步方式不会阻塞
        public string Open(string OpenMethod, string URL, bool Async)
        {
            object obj;
            obj = HttpObj.DoMethod("Open", new object[] { OpenMethod, URL, Async });
            if (obj != null)
            {
                _Active = false;
                return obj.ToString();
            }
            else
            {
                SetRequestHeader("Content-Type", _ContentType);
                SetRequestHeader("User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)");
                if (_ContentLength != 0) SetRequestHeader("Content-Length", _ContentLength);
                _Active = true;
                return "True";
            }
        }

        //发送数据
        public string Send(string body)
        {
            if (!_Active) return "False";
            object obj;
            obj = HttpObj.DoMethod("Send", new object[1] { body });
            if (obj != null) return obj.ToString();
            else return "True";
        }

        public void ClearPostData()
        {
            this.PostDataList.Clear();
        }
        //增加提交数据信息
        public void AddPostField(string FieldName, object Value)
        {
            this.PostDataList.Add(FieldName + "=" + Value.ToString());
        }

        //通过参数指定提交
        public string DxPost()
        {
            if (!_Active)
            {
                return "False";
            }
            string st = "";
            for (int i = 0; i < this.PostDataList.Count; i++)
            {
                if (st != "") st = st + "&" + PostDataList[i].ToString();
                else st = PostDataList[i].ToString();
            }
            this.ContentLength = st.Length;
            return Send(st);
        }

        //设置等待超时等
        public string SetTimeouts(long ResolveTimeout, long ConnectTimeout, long SendTimeout, long ReceiveTimeout)
        {
            object obj;
            obj = HttpObj.DoMethod("SetTimeouts", new object[4] { ResolveTimeout, ConnectTimeout, SendTimeout, ReceiveTimeout });
            if (obj != null) return obj.ToString();
            else return "True";
        }
        //等待数据提交完成
        public string WaitForResponse(object Timeout, out bool Succeeded)
        {
            if (!_Active) { Succeeded = false; return ""; }
            object obj;
            bool succ;
            succ = false;
            System.Reflection.ParameterModifier[] ParamesM;
            ParamesM = new System.Reflection.ParameterModifier[1];
            ParamesM[0] = new System.Reflection.ParameterModifier(2); // 初始化为接口参数的个数
            ParamesM[0][1] = true; // 设置第二个参数为返回参数

            //ParamesM[1] = true;
            object[] ParamArray = new object[2] { Timeout, succ };
            obj = HttpObj.DoMethod("WaitForResponse", ParamArray, ParamesM);
            //System.Windows.Forms.MessageBox.Show(ParamArray[1].ToString());
            Succeeded = bool.Parse(ParamArray[1].ToString());
            //Succeeded = bool.Parse(ParamArray[1].ToString);
            if (obj != null) { return obj.ToString(); }
            else return "True";
        }

        public string GetResponseHeader(string Header, ref string Value)
        {
            if (!_Active) { Value = ""; return ""; }
            object obj;
            /*string str;
            str = "";
            System.Reflection.ParameterModifier[] Parames;
            Parames = new System.Reflection.ParameterModifier[1];
            Parames[0] = new System.Reflection.ParameterModifier (2); // 初始化为接口参数的个数
            Parames[0][1] = true; */
            // 设置第二个参数为返回参数
            obj = HttpObj.DoMethod("GetResponseHeader", new object[2] { Header, Value });
            //Value = str;
            if (obj != null) { return obj.ToString(); }
            else return "True";
        }

        public string SetProxy(int ProxySetting, string ProxyServer, string BypassList)
        {
            if (!_Active) { return ""; }
            object obj;
            obj = HttpObj.DoMethod("SetProxy", new object[3] { ProxySetting, ProxyServer, BypassList });
            if (obj != null) { return obj.ToString(); }
            else return "True";
        }
    }
}
