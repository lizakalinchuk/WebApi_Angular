using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerLib
{
    public class FileCountModel : IFileCountModel
    {
        public int CountLessThan10Mb { get; set; }
        public int CountBetween10MbAnd50Mb { get; set; }
        public int CountMoreThan100Mb { get; set; }
        public string CurrentPath { get; set; }
        public List<string> DirectoryName { get; set; }
    }
}
