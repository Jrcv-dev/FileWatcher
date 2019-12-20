using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Producto
    {
        public int? Id;
        public int? idType;
        public int? idColor;
        public int? idBrand;
        public int? idProvider;
        public int? idCatalog;
        public string title;
        public string nombre;
        public string description;
        public string observations;
        public decimal? priceDistributor;
        public decimal priceClient;
        public decimal priceMember;
        public bool isEnabled;
        public string keywords;
        public DateTime? dateUpdate;
    }
}
