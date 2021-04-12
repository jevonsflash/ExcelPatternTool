using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace Workshop.Helper
{
    public class LogHelper
    {
        private static readonly Thread _writeThread;
        private static readonly Queue<string> MsgQueue;

        private static readonly string FilePath;

        private static Boolean _autoResetEventFlag = false;
        private static readonly AutoResetEvent _aEvent = new AutoResetEvent(false);
        private static bool _flag = true;
        public static bool LogFlag = true;

        static LogHelper()
        {
            FilePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Logs");
            _writeThread = new Thread(WriteMsg);
            MsgQueue = new Queue<string>();
            _writeThread.Start();
        }

        public static void LogInfo(string msg)
        {
            Monitor.Enter(MsgQueue);
            MsgQueue.Enqueue(string.Format("{0} {1} {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss"), "Info", msg));
            Monitor.Exit(MsgQueue);
            if (_autoResetEventFlag)
            {
                _aEvent.Set();
            }
        }
        public static void LogError(string msg)
        {
            Monitor.Enter(MsgQueue);
            MsgQueue.Enqueue(string.Format("{0} {1} {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss"), "Error", msg));
            Monitor.Exit(MsgQueue);
            if (_autoResetEventFlag)
            {
                _aEvent.Set();
            }
        }
        public static void LogWarn(string msg)
        {
            Monitor.Enter(MsgQueue);
            MsgQueue.Enqueue(string.Format("{0} {1} {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss"), "Warn", msg));
            Monitor.Exit(MsgQueue);
            if (_autoResetEventFlag)
            {
                _aEvent.Set();
            }
        }

        public static void ExitThread()
        {
            _flag = false;
            _aEvent.Set();//恢复线程执行
        }
        private static void WriteMsg()
        {
            while (_flag)
            {
                //进行记录
                if (LogFlag)
                {
                    _autoResetEventFlag = false;
                    if (!Directory.Exists(FilePath))
                    {
                        Directory.CreateDirectory(FilePath);
                    }
                    string singleName="log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                    string fileName = Path.Combine(FilePath, singleName);
                    var logStreamWriter = new StreamWriter(fileName, true);
                    while (MsgQueue.Count > 0)
                    {
                        Monitor.Enter(MsgQueue);
                        string msg = MsgQueue.Dequeue();
                        Monitor.Exit(MsgQueue);
                        logStreamWriter.WriteLine(msg);
                        if (GetFileSize(fileName) > 1024 * 5)
                        {
                            logStreamWriter.Flush();
                            logStreamWriter.Close();
                            CopyToBak(fileName);
                            logStreamWriter = new StreamWriter(fileName, false);
                            logStreamWriter.Write("");
                            logStreamWriter.Flush();
                            logStreamWriter.Close();
                            logStreamWriter = new StreamWriter(fileName, true);
                        }
                        //下面用于DbgView.exe工具进行在线调试
                        System.Diagnostics.Debug.WriteLine("BS_Debug:" + msg);
                        System.Diagnostics.Trace.WriteLine("BS_Release:" + msg);
                    }
                    logStreamWriter.Flush();
                    logStreamWriter.Close();
                    _autoResetEventFlag = true;
                    _aEvent.WaitOne();
                }
                else
                {
                    _autoResetEventFlag = true;
                    _aEvent.WaitOne();
                }
            }
        }
        private static long GetFileSize(string fileName)
        {
            long strRe = 0;
            if (File.Exists(fileName))
            {
                var myFs = new FileInfo(fileName);
                strRe = myFs.Length / 1024;
                //Console.WriteLine(strRe);
            }
            return strRe;
        }
        private static void CopyToBak(string sFileName)
        {
            int fileCount = 0;
            string sBakName = "";
            do
            {
                fileCount++;
                sBakName = sFileName + "." + fileCount + ".BAK";
            }
            while (File.Exists(sBakName));
            File.Copy(sFileName, sBakName);
        }
    }
}