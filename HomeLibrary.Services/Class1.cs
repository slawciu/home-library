using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.Services
{
    public class Class1
    {
    }

    public interface IQueryHandler<in TQuery, out TResult>
    {
        TResult Handle(TQuery query);
    }

    public class GetLibraryStateQuery
    {
    }
}
