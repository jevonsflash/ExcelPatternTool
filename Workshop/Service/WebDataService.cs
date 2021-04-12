using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workshop.Helper;
using Workshop.Model;
using Workshop.Model.Apis;

namespace Workshop.Service
{
    public class WebDataService
    {
        static private string baseAddr = LocalDataService.ReadObjectLocal<SettingInfo>().Addr;
        static WebDataService()
        {

        }
        /// <summary>
        /// 查询类别
        /// </summary>
        /// <returns></returns>
        public static IList<CategoryInfo> GetCategory()
        {
            var result = new List<CategoryInfo>();
           

            return result;
        }
        /// <summary>
        /// 修改类别
        /// </summary>
        /// <param name="categoryInfo"></param>
        /// <returns></returns>
        public static bool EditCategory(CategoryInfo categoryInfo)
        {
            var name = categoryInfo.Name;
            var code = categoryInfo.Code;
            var desc = categoryInfo.Description;
            var result = new List<CategoryInfo>();
            Dictionary<string, string> paramsDic = new Dictionary<string, string>();
            paramsDic.Add("name", name);
            paramsDic.Add("code", code);
            paramsDic.Add("desc", desc);
            var data = WebHelper.Current.Getdata(string.Format("http://{0}:8118/scada/api/loc/editDept", baseAddr), Method.POST, paramsDic);
            var obj = JsonConvert.DeserializeObject<CategoryInfoApi>(data);
            if (obj.Errorno != 0)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="categoryInfo"></param>
        /// <returns></returns>
        public static bool DelCategory(CategoryInfo categoryInfo)
        {
            var code = categoryInfo.Code;
            Dictionary<string, string> paramsDic = new Dictionary<string, string>();
            paramsDic.Add("code", code);
            var data = WebHelper.Current.Getdata(string.Format("http://{0}:8118/scada/api/loc/delDept", baseAddr), Method.POST, paramsDic);
            var obj = JsonConvert.DeserializeObject<CategoryInfoApi>(data);
            if (obj.Errorno != 0)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 查询产品类型
        /// </summary>
        /// <returns></returns>
        public static IList<ProductTypeInfo> GetProductType(CategoryInfo category)
        {
            var result = new List<ProductTypeInfo>();

            Dictionary<string, string> paramsDic = new Dictionary<string, string>();
            paramsDic.Add("dept", category.Code);
            paramsDic.Add("session", App.Session);
            var data = WebHelper.Current.Getdata(string.Format("http://{0}:8115/scada/api/job/getSapProcess", baseAddr), Method.GET, paramsDic);
            try
            {
                var obj = JsonConvert.DeserializeObject<ProductTypeInfoApi>(data);
                if (obj.Errorno != 0)
                {
                    return result;
                }
                foreach (var item in obj.SapList)
                {
                    result.Add(item.ProductTypeInfo);
                }
            }
            catch (Exception)
            {
                return result;
            }

            return result;
        }

        public static IList<ProductTypeInfo> GetProductType()
        {
            var result = new List<ProductTypeInfo>();

            Dictionary<string, string> paramsDic = new Dictionary<string, string>();
            paramsDic.Add("session", App.Session);
            var data = WebHelper.Current.Getdata(string.Format("http://{0}:8115/scada/api/job/getSapProcess", baseAddr), Method.GET, paramsDic);
            try
            {
                var obj = JsonConvert.DeserializeObject<ProductTypeInfoApi>(data);
                if (obj.Errorno != 0)
                {
                    return result;
                }
                foreach (var item in obj.SapList)
                {
                    result.Add(item.ProductTypeInfo);
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }

        /// <summary>
        /// 修改产品类型
        /// </summary>
        /// <param name="productTypeInfo"></param>
        /// <returns></returns>
        public static bool EditProductType(ProductTypeInfo productTypeInfo)
        {
            //dept: WY
            //sapId: 123456
            //processIdList: WYTS01,WYCE01,WYCE02,WYCE05,WYCE06
            //processNameList: 调试,测试,TD检测,LTE检测,WCDMA检测
            //groupList: 1,2,2,2,2
            //session: 5b20d1296f58965fac5658b3
            var dept = productTypeInfo.Category.Code;
            var sapId = productTypeInfo.Name;
            var productName = productTypeInfo.Description;
            string processIdList = string.Empty;
            string processNameList = string.Empty;
            string groupList = string.Empty;
            string priceList = string.Empty;
            string manhourList = string.Empty;
            if (productTypeInfo.ProcedureInfos != null)
            {
                foreach (var item in productTypeInfo.ProcedureInfos)
                {
                    processNameList += "," + item.ProcedureTypeInfo.Name;
                    processIdList += "," + item.ProcedureTypeInfo.Id;
                    groupList += "," + item.Group;
                    priceList += "," + item.Price;
                    manhourList += "," + item.Duration;
                }

                processNameList.TrimStart(',');
                processIdList.TrimStart(',');
                groupList.TrimStart(',');
                priceList.TrimStart(',');
                manhourList.TrimStart(',');
            }
            Dictionary<string, string> paramsDic = new Dictionary<string, string>();
            paramsDic.Add("dept", dept);
            paramsDic.Add("sapId", sapId);
            paramsDic.Add("productName", productName);
            paramsDic.Add("processIdList", processIdList);
            paramsDic.Add("processNameList", processNameList);
            paramsDic.Add("groupList", groupList);
            paramsDic.Add("priceList", priceList);
            paramsDic.Add("manhourList", manhourList);
            paramsDic.Add("session",  App.Session);

            var data = WebHelper.Current.Getdata(string.Format("http://{0}:8115/scada/api/job/editSapProcess", baseAddr), Method.POST, paramsDic);
            var obj = JsonConvert.DeserializeObject<CategoryInfoApi>(data);
            if (obj.Errorno != 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 删除产品类型
        /// </summary>
        /// <param name="productTypeInfo"></param>
        public static bool DelProductType(ProductTypeInfo productTypeInfo)
        {
            var code = productTypeInfo.Name;
            var dept = productTypeInfo.Category.Code;
            Dictionary<string, string> paramsDic = new Dictionary<string, string>();
            paramsDic.Add("sapId", code);
            paramsDic.Add("dept", dept);
            var data = WebHelper.Current.Getdata(string.Format("http://{0}:8115/scada/api/job/delSapProcess", baseAddr), Method.POST, paramsDic);
            var obj = JsonConvert.DeserializeObject<CategoryInfoApi>(data);
            if (obj.Errorno != 0)
            {
                return false;
            }

            return true;
        }


        public static IList<ProcedureTypeInfo> GetProcedureType(CategoryInfo category)
        {
            var result = new List<ProcedureTypeInfo>();

            Dictionary<string, string> paramsDic = new Dictionary<string, string>();
            paramsDic.Add("dept", category.Code);

            paramsDic.Add("session", App.Session);
            var data = WebHelper.Current.Getdata(string.Format("http://{0}:8115/scada/api/job/getProcess", baseAddr), Method.GET, paramsDic);
            try
            {
                var obj = JsonConvert.DeserializeObject<ProcedureTypeInfoApi>(data);
                if (obj.Errorno != 0)
                {
                    return result;
                }
                foreach (var item in obj.ProcessList)
                {
                    result.Add(item.ProcedureTypeInfo);
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }

        public static IList<ProcedureTypeInfo> GetProcedureType()
        {
            var result = new List<ProcedureTypeInfo>();

            Dictionary<string, string> paramsDic = new Dictionary<string, string>();
            paramsDic.Add("session", App.Session);
            var data = WebHelper.Current.Getdata(string.Format("http://{0}:8115/scada/api/job/getProcess", baseAddr), Method.GET, paramsDic);
            try
            {
                var obj = JsonConvert.DeserializeObject<ProcedureTypeInfoApi>(data);
                if (obj.Errorno != 0)
                {
                    return result;
                }
                foreach (var item in obj.ProcessList)
                {
                    result.Add(item.ProcedureTypeInfo);
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }

        /// <summary>
        /// 修改工序类型
        /// </summary>
        /// <param name="productTypeInfo"></param>
        /// <returns></returns>
        public static bool EditProcedureType(ProcedureTypeInfo productTypeInfo)
        {
            //"dept", "processId", "processName", "instrumentFunc"
            var dept = productTypeInfo.Category.Code;
            var processId = productTypeInfo.Id;
            var func = productTypeInfo.Func;
            var processName = productTypeInfo.Name;
            Dictionary<string, string> paramsDic = new Dictionary<string, string>();
            paramsDic.Add("dept", dept);
            paramsDic.Add("processId", processId);
            paramsDic.Add("processName", processName);
            paramsDic.Add("instrumentFunc", func);
            paramsDic.Add("session", App.Session);

            var data = WebHelper.Current.Getdata(string.Format("http://{0}:8115/scada/api/job/addProcess", baseAddr), Method.POST, paramsDic);
            var obj = JsonConvert.DeserializeObject<CategoryInfoApi>(data);
            if (obj.Errorno != 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 删除工序类型
        /// </summary>
        /// <param name="productTypeInfo"></param>
        public static bool DelProcedureType(ProcedureTypeInfo productTypeInfo)
        {
            var processId = productTypeInfo.Id;
            var dept = productTypeInfo.Category.Code;
            Dictionary<string, string> paramsDic = new Dictionary<string, string>();
            paramsDic.Add("processId", processId);
            paramsDic.Add("dept", dept);
            var data = WebHelper.Current.Getdata(string.Format("http://{0}:8115/scada/api/job/delProcess", baseAddr), Method.POST, paramsDic);
            var obj = JsonConvert.DeserializeObject<CategoryInfoApi>(data);
            if (obj.Errorno != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取座位信息
        /// </summary>
        /// <returns></returns>
        public static IList<SeatList> GetSeatList()
        {
            var result = new List<SeatList>();

            Dictionary<string, string> paramsDic = new Dictionary<string, string>();

            paramsDic.Add("session", App.Session);
            var data = WebHelper.Current.Getdata(string.Format("http://{0}:8115/scada/api/job/getSeats", baseAddr), Method.GET, paramsDic);
            try
            {
                var obj = JsonConvert.DeserializeObject<SeatInfoApi>(data);
                if (obj.Errorno != 0)
                {
                    return result;
                }
                foreach (var item in obj.SeatList)
                {
                    result.Add(item);
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }

        public static IList<SeatList> GetProductInfo()
        {

            //session: 5b2ca396e1f3da2e109c47e3
            //seatId: seat03
            //productList: FA1830007644,FA1830007645,AA1830017665,AA1830017669,AA1830017670,AA1830017689,AA1830017690,AA1830017694,AA1830017707,AA1840000236,AA1840000239,AA1840000278

            var result = new List<SeatList>();

            Dictionary<string, string> paramsDic = new Dictionary<string, string>();

            paramsDic.Add("session", App.Session);
            var data = WebHelper.Current.Getdata(string.Format("http://{0}:8115/scada/api/job/getProductInfo", baseAddr), Method.GET, paramsDic);
            try
            {
                var obj = JsonConvert.DeserializeObject<SeatInfoApi>(data);
                if (obj.Errorno != 0)
                {
                    return result;
                }
                foreach (var item in obj.SeatList)
                {
                    result.Add(item);
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public static LoginResultInfoApi Login(string uid, string pwd, string seat)
        {
            var result = new LoginResultInfoApi();

            Dictionary<string, string> paramsDic = new Dictionary<string, string>();
            //userId=tony&password=123456&seatId=
            paramsDic.Add("userId", uid);
            paramsDic.Add("password", pwd);
            paramsDic.Add("seatId", seat);
            var data = WebHelper.Current.Getdata(string.Format("http://{0}:8112/scada/api/auth/login", baseAddr), Method.POST, paramsDic);
            try
            {
                var obj = JsonConvert.DeserializeObject<LoginResultInfoApi>(data);
                result = obj;
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }



    }
}
