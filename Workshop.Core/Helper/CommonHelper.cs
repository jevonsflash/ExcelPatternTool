using System;
using System.IO;
using System.Reflection;

namespace Workshop.Core.Helper
{
    public class CommonHelper
    {
        public static string DesktopPath => Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        public static string ExePath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string AppBasePath => AppDomain.CurrentDomain.BaseDirectory;

    }
}
