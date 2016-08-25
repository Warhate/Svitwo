using System;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;

namespace Svitwo
{
    public class DevLogHandler : ILogHandler
    {
        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {

        }

        public void LogException(Exception exception, Object context)
        {

        }
    }
}

