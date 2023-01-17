using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_Project
{
    public class LoggingUtility
    {
        private ILog log;
        private RollingFileAppender roller;
        private string logFileName;
        private bool runAsConsole = false;
        private int logContinueDays = 30;

        public LoggingUtility(string loggerName, Level logLevel, int logDays)
        {
            logFileName = loggerName + ".log";
            logContinueDays = logDays;
            SetupLog4net(logLevel, loggerName);
        }

        /// <summary>
        /// Log4net사용을 위한 설정
        /// </summary>
        /// <param name="logLevel">로그 레벨 설정</param>
        public void SetupLog4net(Level logLevel, string loggerName)
        {
            CheckAndCreateLoggingFolder();
            //로그파일이 없을때만 생성하도록 변경해야 함 (ihlee)
            bool exists = false;
            ILoggerRepository repository = null;

            log4net.Repository.ILoggerRepository[] repositories = LogManager.GetAllRepositories();
            if (repositories != null)
            {
                foreach (log4net.Repository.ILoggerRepository r in repositories)
                {
                    if (r.Name == loggerName)
                    {
                        repository = r;
                        exists = true;
                        break;
                    }
                }
            }
            if (!exists)
            {
                repository = LogManager.CreateRepository(loggerName);                
            }

            Hierarchy hierarchy = (Hierarchy)repository;
            log4net.Repository.Hierarchy.Logger logger = hierarchy.LoggerFactory.CreateLogger((ILoggerRepository)hierarchy, loggerName);
            logger.Hierarchy = hierarchy;
            CreateFileAppender(loggerName);
            logger.AddAppender(this.roller);
            logger.Repository.Configured = true;

            hierarchy.Threshold = logLevel;
            logger.Level = logLevel;

            this.log = new LogImpl(logger);
        }

        private void CreateFileAppender(string loggerName)
        {
            if (roller != null)
            {
                this.CloseRoller();
            }

            //rollingAppender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date;
            //rollingAppender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
            //rollingAppender.DatePattern = "_yyyyMMdd\".log\""; // 날짜가 지나간 경우 이전 로그에 붙을 이름 구성
            //log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout("%date [%property{buildversion}] %-5level %logger - %message%newline");

            roller = new RollingDateAppender
            {
                Name = loggerName + "FileAppender",
                File = GetLoggingFilePath(),
                LockingModel = new FileAppender.MinimalLock(),
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Composite,
                DatePattern = "_yyyyMMdd",
                MaxAgeRollBackups = TimeSpan.FromDays(logContinueDays),
                MaxSizeRollBackups = -1,
                MaximumFileSize = "1MB",
                StaticLogFileName = true,
                //Layout = new log4net.Layout.PatternLayout("[%level] %date %-20logger %newline%message%newline") //[INFO] 2020-01-19 21:39:49,388 MyProject 
                Layout = new log4net.Layout.PatternLayout("[%level] %date %logger -> %message%newline") //[INFO] 2020-01-19 21:39:49,388 MyProject 

                //Name = loggerName + "FileAppender",
                //File = GetLoggingFilePath(),
                //LockingModel = new FileAppender.MinimalLock(),
                //AppendToFile = true,
                //RollingStyle = RollingFileAppender.RollingMode.Size,
                //MaxSizeRollBackups = -1,
                //MaximumFileSize = "1MB",
                //StaticLogFileName = true,
                //Layout = new log4net.Layout.PatternLayout("[%level] %date %-20logger %newline%message%newline") //[INFO] 2020-01-19 21:39:49,388 MyProject   
            };
            roller.ActivateOptions();
            BasicConfigurator.Configure(roller);
        }

        public void CloseRoller()
        {
            if (this != null && this.roller != null && !runAsConsole)
            {
                this.roller.Close();
            }
        }

        public void RemoveRepository(string loggerName)
        {
            log4net.Repository.ILoggerRepository[] repositories = LogManager.GetAllRepositories();
            if (repositories != null)
            {
                foreach (log4net.Repository.ILoggerRepository r in repositories)
                {
                    if (r.Name == loggerName)
                    {
                        LogManager.ShutdownRepository(loggerName);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 디버그 정보 쓰기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void WriteDebug(string message, Exception ex)
        {
            if (!runAsConsole)
                log.Debug(message, ex);
        }

        /// <summary>
        /// 디버그 정보 쓰기
        /// </summary>
        /// <param name="message"></param>
        public void WriteDebug(string message)
        {
            if (!runAsConsole)
                log.Debug(message);
        }

        /// <summary>
        /// 실행 정보 쓰기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void WriteInfo(string message, Exception ex)
        {
            if (!runAsConsole)
                log.Info(message, ex);
        }


        /// <summary>
        /// 실행 정보 쓰기
        /// </summary>
        /// <param name="message"></param>
        public void WriteInfo(string message)
        {
            if (!runAsConsole)
                log.Info(message);
        }

        /// <summary>
        /// 경고 로그 쓰기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void WriteWarn(string message, Exception ex)
        {
            if (!runAsConsole)
                log.Warn(message, ex);
        }

        /// <summary>
        /// 경고 로그 쓰기
        /// </summary>
        /// <param name="message"></param>
        public void WriteWarn(string message)
        {
            if (!runAsConsole)
                log.Warn(message);
        }

        /// <summary>
        /// 오류 로그 쓰기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void WriteError(string message, Exception ex)
        {
            if (!runAsConsole)
                log.Error(message, ex);
        }

        /// <summary>
        /// 오류 로그 쓰기
        /// </summary>
        /// <param name="message"></param>
        public void WriteError(string message)
        {
            if (!runAsConsole)
                log.Error(message);
        }

        /// <summary>
        /// 치명적인 오류 쓰기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void WriteFatal(string message, Exception ex)
        {
            if (!runAsConsole)
                log.Fatal(message, ex);
        }

        /// <summary>
        /// 치명적인 오류 쓰기
        /// </summary>
        /// <param name="message"></param>
        public void WriteFatal(string message)
        {
            if (!runAsConsole)
                log.Fatal(message);
        }


        /// <summary>
        /// Logs 폴더 존재 확인 후 생성
        /// </summary>
        private void CheckAndCreateLoggingFolder()
        {
            string tempFolder = GetLoggingFolder();

            if (!Directory.Exists(tempFolder))
            {
                Directory.CreateDirectory(tempFolder);
            }
        }

        /// <summary>
        /// .\Logs 폴더를 구함
        /// </summary>
        /// <returns></returns>
        private string GetLoggingFolder()
        {
            return @".\Logs"; // string.Format(@"\{0}\{1}\Logs", this.company, this.product);
        }

        /// <summary>
        /// 로깅 파일의 위치를 구함
        /// </summary>
        /// <returns></returns>
        private string GetLoggingFilePath()
        {
            return string.Format(@"{0}\{1}", GetLoggingFolder(), this.logFileName);
        }
    }

    public class RollingDateAppender : RollingFileAppender
    {
        public TimeSpan MaxAgeRollBackups { get; set; }

        public RollingDateAppender()
          : base()
        {
            PreserveLogFileNameExtension = true;
            StaticLogFileName = false;
        }

        protected override void AdjustFileBeforeAppend()
        {
            base.AdjustFileBeforeAppend();

            string LogFolder = Path.GetDirectoryName(File);
            var CheckTime = DateTime.Now.Subtract(MaxAgeRollBackups);
            foreach (string file in Directory.GetFiles(LogFolder, "*.log"))
            {
                if (System.IO.File.GetLastWriteTime(file) < CheckTime)
                    DeleteFile(file);
            }
        }

    }
}
