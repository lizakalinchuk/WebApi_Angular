using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerLib
{
    public class FileManager : IFileManager
    {
        private int counterLessThan10Mb;
        private int counterBetween10MbAnd50Mb;
        private int counterMoreThan100Mb;
        const int TRANSFORM_NUMBER = 1024;
        private readonly IFileCountModel model;

        public FileManager(IFileCountModel model)
        {
            this.model = model;
            counterLessThan10Mb = 0;
            counterBetween10MbAnd50Mb = 0;
            counterMoreThan100Mb = 0;
            model.DirectoryName = new List<string>() { };
        }

        public FileCountModel CountModel { get { return (FileCountModel)model; } }

        public void GetCountOfFile(string initialDirectory, bool isRootFileSystem)
        {
            if (new DirectoryInfo(initialDirectory).Exists)
            {
                DirectoryHandle(new DirectoryInfo(initialDirectory));
            }
            FillModel(initialDirectory, isRootFileSystem);
        }

        public string CreateCorrectPath(string path, string currentPath)
        {
            if (currentPath.Length == 3 && currentPath.Contains(":") && path == "_")
            {
                return "_";
            }
            if (path == "_")
            {
                if (currentPath != "_")
                {
                    path = currentPath.LastIndexOf('\\') > 2
                        // for drivers, path='_'
                        ? currentPath.Substring(0, currentPath.LastIndexOf('\\'))
                        // for directory, path='_'
                        : currentPath.Substring(0, currentPath.LastIndexOf('\\') + 1);
                }
            }
            else
            {
                path = Path.Combine(currentPath, path);
            }
            return path;
        }

        public List<string> GetDriveNames()
        {
            List<string> listOfDrive = new List<string>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                listOfDrive.Add(drive.Name);
            }
            return listOfDrive;
        }

        #region Private

        private void FillModel(string initialDirectory, bool isRoot)
        {
            model.CountLessThan10Mb = counterLessThan10Mb;
            model.CountBetween10MbAnd50Mb = counterBetween10MbAnd50Mb;
            model.CountMoreThan100Mb = counterMoreThan100Mb;
            model.CurrentPath = isRoot ? "_" : initialDirectory;
            var parentDirectory = new DirectoryInfo(initialDirectory);

            if (isRoot)
            {
                model.DirectoryName.Add(parentDirectory.Name);
            }
            else
            {
                if (parentDirectory.Exists)
                {
                    try
                    {
                        foreach (var directory in parentDirectory.GetDirectories())
                        {
                            model.DirectoryName.Add(directory.Name);
                        }
                        foreach (var file in parentDirectory.GetFiles())
                        {
                            model.DirectoryName.Add(file.Name);
                        }
                    }
                    catch (UnauthorizedAccessException) { /* skip */ }
                    catch (Exception) { /* skip */ }
                }
            }
        }

        private void DirectoryHandle(DirectoryInfo parentDirectory)
        {
            GetFileLength(parentDirectory);
            try
            {
                foreach (DirectoryInfo directory in parentDirectory.GetDirectories())
                {
                    DirectoryHandle(directory);
                }
            }
            catch (UnauthorizedAccessException) { /* skip */ }
            catch (Exception) { /* skip */ }
        }

        private void GetFileLength(DirectoryInfo directory)
        {
            try
            {
                foreach (FileInfo file in directory.GetFiles())
                {
                    // convert bytes into Mb
                    long size = file.Length / TRANSFORM_NUMBER / TRANSFORM_NUMBER;
                    if (size <= 10)
                    {
                        counterLessThan10Mb++;
                    }
                    if (size > 10 && size <= 50)
                    {
                        counterBetween10MbAnd50Mb++;
                    }
                    if (size >= 100)
                    {
                        counterMoreThan100Mb++;
                    }
                }
            }
            catch (UnauthorizedAccessException) { /* skip */ }
            catch (Exception) { /* skip */ }
        }

        #endregion
    }
}
