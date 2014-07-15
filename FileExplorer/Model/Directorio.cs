using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileExplorer.Model
{
    public class Directorio
    {
        private string path;
        private string creationTime;
        private List<Archivo> files;

        public Directorio(string path, string creationTime, List<Archivo> files)
        {
            this.path = path;
            this.creationTime = creationTime;
            this.files = files;
        }


        public List<Archivo> Files
        {
            get { return files; }
            set { files = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }        

        public string CreationTime
        {
            get { return creationTime; }
            set { creationTime = value; }
        }
    }
}