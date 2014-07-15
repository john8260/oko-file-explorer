using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileExplorer.Model
{
    public class Archivo
    {
        private string name;
        private string path;

        public Archivo(string name, string path)
        {
            this.name = name;
            this.path = path;
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}