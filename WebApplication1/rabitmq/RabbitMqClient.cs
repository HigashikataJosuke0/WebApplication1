// using RabbitMQ.Client;
//
//
// namespace WebApplication1.rabitmq;
//
// public sealed class RabbitMqClient
// {
//     
//     private readonly ConnectionFactory _connectionFactory;
//
//     public RabbitMqClient(RabbitMqOptions options)
//     {
//         if (string.IsNullOrEmpty(options.Uri))
//         {
//             throw new ArgumentException("RabbitMQ Uri is not configured.", nameof(options.Uri));
//         }
//
//         // Настройка фабрики подключений с использованием URI
//         _connectionFactory = new ConnectionFactory
//         {
//             Uri = new Uri(options.Uri) // URI из конфигурации
//         };
//         Console.WriteLine("Соединение с RabbitMQ успешно установлено!");
//     }
//
//     public IConnection CreateConnection()
//     {
//         try
//         {
//             // Создаем и возвращаем подключение
//             return _connectionFactory.CreateConnection();
//             
//         }
//         catch (Exception ex)    
//         {
//             throw new InvalidOperationException("Failed to create RabbitMQ connection.", ex);
//         }
//     }
// }