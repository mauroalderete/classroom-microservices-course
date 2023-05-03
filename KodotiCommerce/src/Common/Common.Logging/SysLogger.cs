using System;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Common.Logging
{

    public class SysLogger : ILogger
    {
        private readonly string _name;
        private readonly string _host;
        private readonly int _port;
        private readonly Func<string, LogLevel, bool> _filter;

        public SysLogger(string name, string host, int port, Func<string, LogLevel, bool> filter)
        {
            _name = name;
            _host = host;
            _port = port;
            _filter = filter;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => _filter == null || _filter(_name, logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var message = formatter(state, exception);
            SendMessageToPapertrail(logLevel, eventId, message, exception);
        }

        private void SendMessageToPapertrail(LogLevel logLevel, EventId eventId, string message, Exception exception)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            var logMessage = $"{timestamp} {_name} {logLevel}: {message}";

            if (exception != null)
            {
                logMessage += Environment.NewLine + exception;
            }

            using var udpClient = new UdpClient();
            try
            {
                var bytes = Encoding.ASCII.GetBytes(logMessage);
                udpClient.SendAsync(bytes, bytes.Length, _host, _port).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar mensaje a Papertrail: {ex.Message}");
            }
        }
    }

}