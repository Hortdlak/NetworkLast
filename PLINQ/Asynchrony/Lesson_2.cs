using System.Net;
using System.Net.NetworkInformation;



namespace Asynchrony
{
    internal class Lesson_2
    {
        public static async Task Task3()
        {
            const string website = "yandex.ru";

            // Получаем IP-адреса сайта
            IPAddress[] iPAddresses = Dns.GetHostAddresses(website).Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToArray();

            foreach (var ip in iPAddresses)
            {
                Console.WriteLine(ip);
            }

            Dictionary<IPAddress, long> pings = new Dictionary<IPAddress, long>();

            List<Task> tasks = new List<Task>();
            object lockObj = new object();

            foreach (var ip in iPAddresses)
            {
                var task = Task.Run(async () =>
                {
                    Ping p = new Ping();
                    PingReply pingReply = await p.SendPingAsync(ip);

                    lock (lockObj)
                    {
                        pings[ip] = pingReply.RoundtripTime;
                    }
                    Console.WriteLine($"{ip}: {pingReply.RoundtripTime}");
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            if (pings.Count > 0)
            {
                long minPing = pings.Min(x => x.Value);
                Console.WriteLine($"Минимальное время пинга: {minPing} мс");
            }
            else
            {
                Console.WriteLine("Не удалось получить пинг для ни одного IP-адреса.");
            }
        }
    }
}
