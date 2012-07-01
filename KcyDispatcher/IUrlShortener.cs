using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KcyDispatcher
{
    public interface IUrlShortener
    {
        string Short(string url);
    }
}
