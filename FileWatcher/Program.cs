using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace FileWatcher
{
    class Program
    {
        public static bool Changeslocked, ObserverWatcher;
        public static List<string> Logs = new List<string>();
        static void Main(string[] args)
        {
            Observer ws = new Observer(Common.Common.PATH);
            Changes Cambios = new Changes(Strings.TableChanges);
            Console.ReadLine();
        }
    }
}
