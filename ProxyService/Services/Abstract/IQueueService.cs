using System;

namespace ProxyService.Services
{
    public interface IQueueService : IDisposable
    {
        void Subscribe();
        void UnSubscribe();
    }
}
