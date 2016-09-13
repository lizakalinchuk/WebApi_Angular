using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerLib
{
    public interface IFileManager
    {
        FileCountModel CountModel { get; }
        void GetCountOfFile(string initialDirectory, bool isInitialLoad);
        List<string> GetDriveNames();
        string CreateCorrectPath(string path, string currentPath);
    }
}
