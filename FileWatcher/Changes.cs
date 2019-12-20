using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace FileWatcher
{
    class Changes
    {
        private string table;
        private Data.CapaData D;
        private int CurrentID;
        private int newID;

        public Changes(string table)
        {
            this.table = table;
            D = new Data.CapaData();
            CurrentID = D.GetLastRegister(Common.Strings.TableChanges).idLog;
            Start();
        }
        private async Task Start()
        {
            while (true)
            {
                await startAsync();
            }
        }

        private async Task startAsync()
        {
            //indica el tiempo determinado de espera en milisegundos
            await Task.Delay(500);
            if (!Program.Changeslocked)
            {
                newID = D.GetLastRegister(Common.Strings.TableChanges).idLog;
                if (CurrentID != newID)
                {
                    List<ChangeOnProduct> newRows = D.ReturnChangesOnTable(table, CurrentID + 1);
                    foreach (ChangeOnProduct row in newRows)
                    {
                        switch (row.ActionMode)
                        {
                            case 1:
                                InsertWasMade(row.idProduct);
                                Console.WriteLine("Se realizo un insert.");
                                Program.Logs.Add("Se realizo un insert."+DateTime.Now);
                                break;
                            case 2:
                                DeleteWasMade(row.idProduct);
                                Console.WriteLine("Se realizo un Delete.");
                                break;
                            case 3:
                                UpdateWasMade(row.idProduct);
                                Console.WriteLine("Se realizo una Actualizacion en un Registro.");
                                break;
                            default:
                                Console.WriteLine("Error");
                                break;

                        }
                    }
                }
                else
                {
                    Console.WriteLine("NO CHANGES ON DB");
                   // Console.Clear();
                }
            }
        }
        private void DeleteWasMade(int Idp)
        {
            Program.ObserverWatcher = true;
            Common.Common.Delete(Idp + ".xml");
            CurrentID = D.GetLastRegister(table).idLog;
            Program.ObserverWatcher = false;
        }
        private void UpdateWasMade(int Idp)
        {
            Program.ObserverWatcher = true;
            var UpdateProduct = D.GetProductByID(Idp);
            Common.Common.Delete(Idp + ".xml");
            Common.Common.SerializeList(UpdateProduct, Idp + ".xml");
            CurrentID = D.GetLastRegister(table).idLog;
            Program.ObserverWatcher = false;
        }
        private void InsertWasMade(int Idp)
        {
            //blocked the process of the filewatcher
            Program.ObserverWatcher = true;

            //Process of the insert product
            Producto p = D.GetProductByID(Idp);
            Common.Common.SerializeList(p, p.Id + ".xml");

            //we get the new current id and we unblocked the filewatcher
            CurrentID = D.GetLastRegister(table).idLog;
            Program.ObserverWatcher = false;
        }
    }
}
