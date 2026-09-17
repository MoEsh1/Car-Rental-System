using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Car_Rental.Global_Classes
{
    public class clsEventLogger
    {
        const string SourceName = "CarRental";
        const string TargetName = "Application";

        public static void LogInfo(string message)
        {
            Log(message, EventLogEntryType.Information);
        }

        public static void LogWarning(string message)
        {
            Log(message, EventLogEntryType.Warning);
        }

        public static void LogError(string message)
        {
            Log(message, EventLogEntryType.Error);
        }

        private static void Log(string message, string sourceName, string targetName, EventLogEntryType type)
        {
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, targetName);
            }
            EventLog.WriteEntry(sourceName, message, type);
        }

        private static void Log(string message, EventLogEntryType type)
        {
            Log(message, SourceName, TargetName, type);
        }



    }
}
