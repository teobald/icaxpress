using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.SignalR.Client;

namespace XPress.QueueService.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:8088/notificationhub")
                .Build();

            connection.On<string, string>("Connect", (user, message) =>
            {
                var messageFromServer = message;
            });

            try
            {
                await connection.StartAsync();

                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
            }

        }
    }
}
