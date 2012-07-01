using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KcyDispatcher
{
    public class KeyWordExtractor
    {
        public string ExtractKeyword(string compartir)
        {
            int index = compartir.LastIndexOf("/");

            return compartir.Substring(index + 1);
        }
    }
}
