using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace FileWatcher
{
    public class Observer
    {
        private FileSystemWatcher watcher;
        private Data.CapaData D;
        public Observer(string path)
        {
            watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.Deleted += DeletedFile;
            watcher.Changed += SavedFile;
            watcher.EnableRaisingEvents = true;
            var common = new Common.Common();
            D = new Data.CapaData();
        }
        public void DeletedFile(object source, FileSystemEventArgs e)
        {
            if (!Program.ObserverWatcher)
            {
                Program.Changeslocked = true;
                D.DeletedProduct(int.Parse(e.Name.Split('.')[0]));
                Console.WriteLine("Eliminado");
                Program.Changeslocked = false;
            }
        }
        public void SavedFile(object source, FileSystemEventArgs e)
        {
            if (!Program.ObserverWatcher)
            {
                Program.Changeslocked = true;
                if (e.Name.Split('.')[0] == "0")
                {
                    D.InsertProduct(Common.Common.DeserializeList<Producto>(e.Name));
                    Program.Logs.Add("Se realizo un insert en el folder" + DateTime.Now);
                }
                else
                    D.UpdateProduct(Common.Common.DeserializeList<Producto>(e.Name));
                Console.WriteLine("Archivo Guardado");
                Program.Changeslocked = false;
            }
        }
    }
}
