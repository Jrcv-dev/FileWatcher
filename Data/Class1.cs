using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Data
{
    public class CapaData
    {
        DataProductsEntities BD = new DataProductsEntities();

        public void InsertProduct(Producto p)
        {
            BD.Products.Add(
                new Products()
                {
                    IdType = p.idType,
                    IdColor = p.idColor,
                    IdBrand = p.idBrand,
                    IdProvider = p.idProvider,
                    IdCatalog = p.idCatalog,
                    Title = p.title,
                    Nombre = p.nombre,
                    Description = p.description,
                    Observations = p.observations,
                    PriceDistributor = p.priceDistributor,
                    PriceClient = p.priceClient,
                    PriceMember = p.priceMember,
                    IsEnabled = p.isEnabled,
                    DateUpdate = p.dateUpdate
                });
            BD.SaveChanges();
        }
        public void DeletedProduct(int id)
        {
            BD.Products.Remove(
                BD.Products.FirstOrDefault(p => p.Id == id)
                );
            BD.SaveChanges();
        }
        public void UpdateProduct(Producto p)
        {
            if (p.Id.HasValue)
            {
                var producto = (from o in BD.Products where o.Id == p.Id select o).FirstOrDefault();
                producto.IdBrand = p.idBrand;
                producto.IdCatalog = p.idCatalog;
                producto.IdProvider = p.idProvider;
                producto.IdType = p.idType;
                producto.Title = p.title;
                producto.Nombre = p.nombre;
                producto.PriceDistributor = p.priceDistributor;
                producto.PriceClient = p.priceClient;
                producto.PriceMember = p.priceMember;
                producto.Observations = p.observations;
                producto.Description = p.description;
                producto.IsEnabled = p.isEnabled;
                producto.DateUpdate = p.dateUpdate;
                BD.SaveChanges();
            }
            else
            {
                InsertProduct(p);
            }
        }
        //se obtendra el ultimo registro y se buscara en base al nombre de la tabla recibido
        public ChangeOnProduct GetLastRegister(string tableName)
        {
            /* if (tableName == Common.Strings.TableChanges)
             {
                 return new ChangeOnProduct()
                 {
                     idLog = BD.ChangesOnProduct.OrderBy(p=> p.IdLog).Select(p=>p.IdLog).First(),
                     idProduct = BD.ChangesOnProduct.OrderBy(p=>p.IdProduct).Select(,
                     ActionMode = BD.ChangesOnProduct.Last().ActionMade
                 };
             }
             return new ChangeOnProduct();*/
            if (tableName == Common.Strings.TableChanges)
            {
                int id = BD.ChangesOnProduct.OrderByDescending(p => p.IdLog).Select(p => p.IdLog).First();
                ChangesOnProduct last = BD.ChangesOnProduct.Where(p => p.IdLog == id).FirstOrDefault();
                return new ChangeOnProduct()
                {
                    idLog = last.IdLog,
                    idProduct = last.IdProduct,
                    ActionMode = last.ActionMade
                };
            }
            return new ChangeOnProduct();
        }
        public List<ChangeOnProduct> ReturnChangesOnTable(string tableName, int id)
        {
            if (tableName == Common.Strings.TableChanges)
            {
                List<ChangeOnProduct> listProduct = new List<ChangeOnProduct>();
                var list = (from o in BD.ChangesOnProduct where o.IdLog >= id select o).ToList();
                foreach (var item in list)
                {
                    listProduct.Add(new ChangeOnProduct
                    {
                        idLog = item.IdLog,
                        idProduct = item.IdProduct,
                        ActionMode = item.ActionMade
                    });
                }
                return listProduct;
            }
            return new List<ChangeOnProduct>();
        }
        public Producto GetProductByID(int id)
        {
            var producto = (from o in BD.Products where o.Id == id select o).ToList();

            return new Producto
            {
                Id = producto[0].Id,
                idType = producto[0].IdType,
                idBrand = producto[0].IdBrand,
                idCatalog = producto[0].IdCatalog,
                idColor = producto[0].IdColor,
                idProvider = producto[0].IdProvider,
                nombre = producto[0].Nombre,
                title = producto[0].Title,
                description = producto[0].Description,
                observations = producto[0].Observations,
                priceDistributor = producto[0].PriceDistributor,
                priceClient = producto[0].PriceClient,
                priceMember = producto[0].PriceMember,
                isEnabled = producto[0].IsEnabled,
                keywords = producto[0].Keywords,
                dateUpdate = producto[0].DateUpdate
            };
        }
    }
}
