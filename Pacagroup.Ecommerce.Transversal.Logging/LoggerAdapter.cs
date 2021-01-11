using Microsoft.Extensions.Logging;
using Pacagroup.Ecommerce.Transversal.Common;
using System;

namespace Pacagroup.Ecommerce.Transversal.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        #region Fields
        private readonly ILogger<T> _logger;
        #endregion
        #region Ctor
        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }
        #endregion
        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }
    }
}
