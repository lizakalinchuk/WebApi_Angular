using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerLib
{
    public interface IFileCountModel
    {
        int CountLessThan10Mb { get; set; }
        int CountBetween10MbAnd50Mb { get; set; }
        int CountMoreThan100Mb { get; set; }
        string CurrentPath { get; set; }
        List<string> DirectoryName { get; set; }
    }
}
