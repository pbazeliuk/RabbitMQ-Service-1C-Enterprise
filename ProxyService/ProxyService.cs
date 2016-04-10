using System.ServiceProcess;
using ProxyService.Services;

namespace ProxyService
{
    public partial class ProxyService : ServiceBase
    {
        private readonly IQueueService _queueService;

        public ProxyService(IQueueService queueService)
        {
            this._queueService = queueService;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _queueService.Subscribe();
        }

        protected override void OnStop()
        {
            _queueService.UnSubscribe();
        }
    }
}
