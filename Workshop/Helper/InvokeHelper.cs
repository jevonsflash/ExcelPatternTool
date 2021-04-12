using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Workshop.Control;

namespace Workshop.Helper
{
    public class InvokeHelper
    {
        /// <summary>
        /// UI线程调用方法
        /// </summary>
        /// <param name="title"></param>
        /// <param name="action"></param>
        /// <param name="thenAction"></param>
        /// <returns></returns>
        public static Task InvokeOnUi(string title, Action action, Action thenAction=null)
        {
            var task= Task.Factory.StartNew(() =>
            {

                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    if (string.IsNullOrEmpty(title))
                    {
                        title = "获取数据中";
                    }
                    ProgressWindow.StaticShowDialog(title);
                });
                action?.Invoke();

                Application.Current.Dispatcher.InvokeAsync(() =>
                {

                    ProgressWindow.StaticUnShowDialog();
                    thenAction?.Invoke();
                });

            });
            return task;
        }

        /// <summary>
        /// 带参UI线程调用方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="title"></param>
        /// <param name="action"></param>
        /// <param name="thenAction"></param>
        /// <returns></returns>
        public static Task<T> InvokeOnUi<T>(string title, Func<T> action, Action thenAction = null)
        {
            var task = Task.Factory.StartNew(() =>
            {

                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    if (string.IsNullOrEmpty(title))
                    {
                        title = "获取数据中";
                    }
                    ProgressWindow.StaticShowDialog(title);
                });
                var result= action.Invoke();

                Application.Current.Dispatcher.InvokeAsync(() =>
                {

                    ProgressWindow.StaticUnShowDialog();
                    thenAction?.Invoke();
                });
                return result;
            });
            return task;
        }
    }
}
