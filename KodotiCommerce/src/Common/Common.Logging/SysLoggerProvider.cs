using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;

namespace Common.Logging
{
    public class SysLoggerProvider : ILoggerProvider
    {
        private string _host;
        private int _port;
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly ConcurrentDictionary<string, SysLogger> _loggers = new ConcurrentDictionary<string, SysLogger>();

        public SysLoggerProvider(string host, int port, Func<string, LogLevel, bool> filter)
        {
            _host = host;
            _port = port;
            _filter = filter;
        }

        public ILogger CreateLogger(string categoryName)
        {
            // Retorna una instancia de SysLogger, si no existe, crea una nueva y la agrega al diccionario
            return _loggers.GetOrAdd(categoryName, name => new SysLogger(name, _host, _port, _filter));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
