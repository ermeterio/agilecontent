
using System.Collections.Generic;

namespace CandidateTesting.DanielCarvalho.Domain.Interface
{
    public interface ILogFactory
    {
        List<Log> LogFactory(string log);
    }
}
