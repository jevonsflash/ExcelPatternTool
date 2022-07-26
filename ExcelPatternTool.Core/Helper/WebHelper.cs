using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace ExcelPatternTool.Core.Helper
{
    public enum Method
    {
        POST, GET
    }
    public class WebHelper
    {//实例化

        private static WebHelper webHelper;

        public static WebHelper Current
        {
            get
            {
                if (webHelper == null)
                {
                    webHelper = new WebHelper();

                }
                return webHelper;
            }
        }

        private WebHelper()
        {

        }

        public bool GetNetWorkSituation(string gatw = "10.10.39.30")
        {
            Ping p = new Ping();
            PingReply pr = p.Send(gatw);
            return pr != null && pr.Status == IPStatus.Success;
        }


        public string Getdata(string url, Method method = Method.GET, Dictionary<string, string> paramsData = null)
        {
            var result = string.Empty;

            try
            {
                if (method == Method.GET)
                {

                    result = GetResponseString(CreateGetHttpResponse(url, paramsData));
                }
                else
                {

                    result = GetResponseString(CreatePostHttpResponse(url, paramsData));


                }
            }
            catch (Exception e)
            {
                LogHelper.LogError(e.Message);
                throw e;
            }



            return result;
        }

        /// <summary>
        /// 发送http post请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="parameters">查询参数集合</param>
        /// <returns></returns>
        private HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;//创建请求对象
            request.Method = "POST";//请求方式
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";//链接类型
            request.Accept = "application/x-www-form-urlencoded";//构造查询字符串

            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                bool first = true;
                foreach (string key in parameters.Keys)
                {

                    if (!first)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        first = false;
                    }
                }
                byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
                //写入请求流
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }
        /// <summary>
        /// 发送http Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private HttpWebResponse CreateGetHttpResponse(string url, IDictionary<string, string> paramsData)
        {
            var paramsStr = string.Empty;
            if (paramsData != null && paramsData.Count != 0)
            {
                List<string> paramsStrList = new List<string>();
                //异步post提交，不用等待。
                foreach (var item in paramsData)
                {
                    paramsStrList.Add(item.Key + "=" + item.Value);
                }
                paramsStr = "?" + string.Join("&", paramsStrList);
            }
            var requestUrl = url + paramsStr;
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";//链接类型
            return request.GetResponse() as HttpWebResponse;
        }
        /// <summary>
        /// 从HttpWebResponse对象中提取响应的数据转换为字符串
        /// </summary>
        /// <param name="webresponse"></param>
        /// <returns></returns>
        private string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }
    }
}
