using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art_Gallery.DAL.Email_Logs
{
    public class LogWriter
    {
        private string logFilePath;

        public LogWriter(string filePath)
        {
            logFilePath = filePath;
        }

        public void AppendLog(string action, string title)
        {
            try
            {
                string logMessage = $"'{action}': '{title}' has been added to the Art Gallery";
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.WriteLine($"{DateTime.Now} - {logMessage}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during logging
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
