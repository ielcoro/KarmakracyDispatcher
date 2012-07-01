using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KcyDispatcher
{
    public interface IKarmacracyConnector
    {
        string ShortLink(string url);
        void ShareKcy(string text, string kcy);
    }
}
