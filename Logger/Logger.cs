using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLoggers
{
    public class Logger

    {
        NLog.Logger _logger { get; set; }

        private Logger(NLog.Logger logger)
        {
            _logger = logger;
        }

        public Logger(string name) : this(LogManager.GetLogger(name))
        {

        }

        public static Logger Default { get; private set; }

        static Logger()
        {
            Default = new Logger(LogManager.GetCurrentClassLogger());
        }

        #region Dedub

        public void Debug(string msg, params object[] args)
        {
            _logger.Debug(msg, args);
        }

        public void Debug(string msg, Exception e)
        {
            _logger.Debug(e, msg);
        }

        #endregion

        #region Info

        public void Info(string msg, params object[] args)
        {
            _logger.Info(msg, args);
        }

        public void Info(string msg, Exception e)
        {
            _logger.Info(e, msg);
        }

        #endregion

        #region Trace

        public void Trace(string msg, params object[] args)
        {
            _logger.Trace(msg, args);
        }

        public void Trace(string msg, Exception e)
        {
            _logger.Trace(e, msg);
        }

        #endregion

        #region Warn

        public void Warn(string msg, params object[] args)
        {
            _logger.Warn(msg, args);
        }

        public void Warn(string msg, Exception e)
        {
            _logger.Warn(e, msg);
        }

        #endregion

        #region Error

        public void Error(string msg, params object[] args)
        {
            _logger.Error(msg, args);
        }

        public void Error(string msg, Exception e)
        {
            _logger.Error(e, msg);
        }

        #endregion
    }
}
