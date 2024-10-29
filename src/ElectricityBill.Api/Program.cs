
using ElectricityBill.Api.Services;
using ElectricityBill.Application.Commands;
using ElectricityBill.Application.CQRS;
using ElectricityBill.Application.DTOs;
using ElectricityBill.Application.Handlers;
using ElectricityBill.Application.Queries;
using ElectricityBill.Domain.Repositories;
using ElectricityBill.Common;
using ElectricityBill.Domain.EventBus;
using ElectricityBill.Domain.Service;

using ElectricityBill.Infrastructure.Repositories;
using ElectricityBill.Infrastructure.Services;
using ElectricityBill.Application.EventHandlers;
using ElectricityBill.Application.Event;
using ElectricityBill.Common.Events;
using ElectricityBill.Domain.Events;
using ElectricityBill.Domain.Services;
using ElectricityBill.Application.Services;
using System.Net.Mail;
using MailKit.Net.Smtp;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace ElectricityBill.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            string host = builder.Configuration["Email:Smtp"];
            int port = int.Parse(builder.Configuration["Email:Port"]);
 

            builder.Services.AddSingleton<System.Net.Mail.SmtpClient>(provider =>
            {
               
                return new System.Net.Mail.SmtpClient(host, port);
            });
            // Add repositories
            builder.Services.AddSingleton<IBillRepository, InMemoryBillRepository>();
            builder.Services.AddSingleton<IWalletRepository, InMemoryWalletRepository>();

            // Add event bus
            builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();

            // Add electricity providers
            builder.Services.AddSingleton<TestProviderA>();
            builder.Services.AddSingleton<TestProviderB>();
            builder.Services.AddSingleton<SmtpClient>();
            builder.Services.AddSingleton<ElectricityProviderFactory>();
            builder.Services.AddSingleton<INotificationFactory, NotificationFactory>();

            // Add services
            builder.Services.AddSingleton<ISmsService, MockSmsService>();

            // Add command handlers
            builder.Services.AddScoped<ICommandHandler<VerifyBillCommand,string>, VerifyBillCommandHandler>();
            builder.Services.AddScoped<ICommandHandler<ProcessPaymentCommand,Unit>, ProcessPaymentCommandHandler>();
            builder.Services.AddScoped<ICommandHandler<AddWalletFundsCommand,Unit>, AddWalletFundsCommandHandler>();

            builder.Services.AddScoped<ICommandHandler<AddWalletCommand, Guid>, AddWalletCommandHandler>();
            builder.Services.AddScoped<IEventHandler<WalletCreatedEvent>, WalletCreatedEventHandler>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IMessageFormatter, MessageFormatter>();
         


            // Add query handlers
            builder.Services.AddScoped<IQueryHandler<GetBillQuery, BillDto>, GetBillQueryHandler>();
            builder.Services.AddScoped<IQueryHandler<GetWalletQuery, WalletDto>, GetWalletQueryHandler>();

            // Add event handlers
            builder.Services.AddScoped<BillCreatedEventHandler>();
            builder.Services.AddScoped<PaymentCompletedEventHandler>();

            // Configure event subscriptions
            builder.Services.AddHostedService<EventSubscriptionService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
