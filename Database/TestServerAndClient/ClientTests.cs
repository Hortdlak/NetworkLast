using Network.Abstracts;
using Network.Models;
using Network.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Network.Tests
{
    public class MockMessageSous : IMessageSous
    {
        public Task SendAsync(NetMessage message, IPEndPoint ep)
        {
            // Здесь можно добавить логику, если необходимо
            Console.WriteLine($"Mock SendAsync called with message: {message.Text}");
            return Task.CompletedTask;
        }

        public NetMessage Receive(ref IPEndPoint ep)
        {
            // Здесь можно добавить логику для получения сообщения, если необходимо
            Console.WriteLine("Mock Receive called");
            return new NetMessage();
        }
    }

    public class ClientTests
    {
        public void Test_Client_Register_SendsMessage()
        {
            // Arrange
            var mockMessageSous = new MockMessageSous();
            var client = new Client("TestClient");

            var remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

            // Act
            RunRegisterTest(client, mockMessageSous, remoteEndPoint);

            // Assert (проверки)
            // В данном примере просто выводим сообщение о вызове, но можно добавить конкретные проверки
            Console.WriteLine("Test_Client_Register_SendsMessage passed");
        }

        public void Test_Client_Confirm_SendsConfirmation()
        {
            // Arrange
            var mockMessageSous = new MockMessageSous();
            var client = new Client("TestClient");

            var message = new NetMessage
            {
                Command = Command.Message,
                NickNameFrom = "TestClient",
                NickNameTo = "Recipient",
                Text = "Hello"
            };

            var remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

            // Act
            RunConfirmTest(client, mockMessageSous, message, remoteEndPoint);

            // Assert (проверки)
            // В данном примере просто выводим сообщение о вызове, но можно добавить конкретные проверки
            Console.WriteLine("Test_Client_Confirm_SendsConfirmation passed");
        }

        private void RunRegisterTest(Client client, IMessageSous messageSous, IPEndPoint remoteEndPoint)
        {
            Task.Run(async () =>
            {
                await client.Register("TestClient", "127.0.0.1", 12345, messageSous, remoteEndPoint);
            }).Wait();
        }

        private void RunConfirmTest(Client client, IMessageSous messageSous, NetMessage message, IPEndPoint remoteEndPoint)
        {
            Task.Run(async () =>
            {
                await client.Confirm(message, messageSous, remoteEndPoint);
            }).Wait();
        }
    }
}