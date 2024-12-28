// using FluentValidation;
// using RabbitMQ.Client;
//
//
// namespace WebApplication1.rabitmq;
//
// public sealed record class RabbitMqOptions
// {
//     public const string SectionName = "RabbitMq";
//
//     /// <summary>
//     /// Uri брокера с информацией по подключению (пример amqp://guest:guest@localhost)
//     /// </summary>
//     public required string Uri { get; init; }
// 	
//     public sealed class Validator : AbstractValidator<RabbitMqOptions>
//     {
//         public Validator()
//         {
//             RuleFor(options => options.Uri)
//                 .NotEmpty()
//                 .Must
//                 (
//                     connectionString =>
//                     {
//                         try
//                         {
//                             var connectionFactory = new ConnectionFactory()
//                             {
//                                 Uri = new Uri(connectionString)
//                             };
//
//                             return true;
//                         }
//                         catch
//                         {
//                             return false;
//                         }
//                     }
//                 )
//                 .WithMessage("'{{PropertyName}}' содержит некорректное значение для строки подключения. Введённое значение: {{PropertyValue}}.");
//         }
//     }
// }