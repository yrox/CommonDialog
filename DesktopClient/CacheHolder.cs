using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace DesktopClient
{
    public class CacheHolder
    {
        public ObjectCache Cache = MemoryCache.Default;
    }
}
