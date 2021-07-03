using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HamaraBasket.Com
{
  public interface IDataRetriever<T>
    {
        List<T> Retriever();
    }
}
