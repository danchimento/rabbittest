using System;
using System.Threading.Tasks;

namespace Orchestrator.Communication
{
    public interface ICommunicator
    {
        Task Send<T>(T request);
    }
}
