using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieGame2
{
    public class LogFile
    {
        /// <summary>
        /// A file path
        /// </summary>
        private string filePath;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path"> the file path </param>
        public LogFile(string path)
        {
            filePath = path;
        }

        /// <summary>
        /// A method to log the message into the file
        /// </summary>
        /// <param name="message"> message </param>
        public void Log(string message)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true)) 
            {
                writer.WriteLine(message);
            }
        }
    }
}
