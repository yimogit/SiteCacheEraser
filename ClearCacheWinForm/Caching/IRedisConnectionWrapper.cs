using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace ClearCacheWinForm.Caching
{
    public interface IRedisConnectionWrapper : IDisposable
    {
        ConnectionMultiplexer GetConnection();
        IDatabase Database(int? db = null);
        IServer Server(EndPoint endPoint);
        EndPoint[] GetEndpoints();
        void FlushDb(int? db = null);
    }
}
