using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Workshop.Model;
using Workshop.Service;

namespace Workshop.Helper
{
    public class CommonHelper
    {
        public static string DesktopPath => Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        public static string ExePath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    }
}
