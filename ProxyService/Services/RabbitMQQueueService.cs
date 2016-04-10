using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProxyService.Services
{
    public class RabbitMQQueueService : IQueueService
    {
        private bool disposed = false;

        private IModel _channel;
        private String _consumerTag;
        private IConnection _connection;
        private readonly IConnectionFactory _factory;

        private string WorkQueue { get; set; }

        private string RequestUri { get; set; }
        private AuthenticationHeaderValue authenticationHeader;

        public RabbitMQQueueService(IConnectionFactory factory)
        {
            this._factory = factory;
            this._connection = _factory.CreateConnection();
            this._channel = _connection.CreateModel();
            InitializeService();
        }

        private void InitializeService()
        {
            WorkQueue = AppSettings.Get<string>("WorkQueue");
            RequestUri = AppSettings.Get<string>("RequestUri");

            authenticationHeader = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(
                            Encoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", AppSettings.Get<string>("UserName"),
                                                         AppSettings.Get<string>("Password")))));
        }

        public void Subscribe()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = authenticationHeader;
                    try
                    {
                        using (HttpResponseMessage response = client.PostAsync(RequestUri, new StringContent(message)).Result)
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                // Log error 
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        // Log exception
                    }
                }
            };

            _consumerTag = _channel.BasicConsume(queue: WorkQueue,
                                noAck: true,
                                consumer: consumer);
        }

        public void UnSubscribe()
        {
            _channel.BasicCancel(_consumerTag);
        }

        ~RabbitMQQueueService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                _channel?.Dispose();
                _connection?.Dispose();
            }

            disposed = true;
        }
    }
}