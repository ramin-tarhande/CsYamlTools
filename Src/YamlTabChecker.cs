using System;
using System.IO;

namespace CsYamlTools
{
    public class YamlTabChecker
    {
        private readonly string fileName;
        public YamlTabChecker(string fileName)
        {
            this.fileName = fileName;
        }

        public void Check()
        {
            using (var streamReader = new StreamReader(this.fileName))
            {
                Check(streamReader);
            }
        }

        void Check(StreamReader streamReader)
        {
            for (int i = 1; ; i++)
            {
                var line = streamReader.ReadLine();
                if (line == null)
                    break;

                if (line.Contains("\t"))
                {
                    throw new Exception(
                        ErrorMessage(i, line));
                }
            };
        }

        private string ErrorMessage(int i, string line)
        {
            return string.Format("Yaml file should not contain tabs\r\nfile:'{0}'\r\nline {1}:\r\n{2}", this.fileName, i, MarkTab(line));
        }

        private static string MarkTab(string line)
        {
            return line.Replace("\t", "<TAB>");
        }
    }
}
