using FileManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi_Angular.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IFileManager fileManager;

        public ValuesController(IFileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        public FileCountModel Get([FromUri] string path, string currentPath)
        {
            path = fileManager.CreateCorrectPath(path, currentPath);
            if (path == "_")
            {
                foreach (var driveName in fileManager.GetDriveNames())
                {
                    fileManager.GetCountOfFile(driveName, true);
                }
            }
            else
            {
                fileManager.GetCountOfFile(path, false);
            }

            return fileManager.CountModel;
        }

        public FileCountModel Get()
        {
            foreach (var path in fileManager.GetDriveNames())
            {
                fileManager.GetCountOfFile(path, true);
            }
            return fileManager.CountModel;
        }
    }
}