using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using FileExplorer.Model;
using System.Collections.Generic;
using System.IO;

namespace FileExplorer.Controller
{
    public class DirectoryController
    {
        /// <summary>
        /// This method retrieves the directories to upload with its files
        /// </summary>
        public List<Directorio> getDirectories(){
            //setting the paths
            string fromPath = WebConfigurationManager.AppSettings["from"];
            string toPath = WebConfigurationManager.AppSettings["to"];
            string antique = WebConfigurationManager.AppSettings["antique"]; //this number is defined as months
            //Parsing variables
            int months = 0;
            Directorio directorio;
            Archivo archivo;
            List<Directorio> directorios = new List<Directorio>();
            List<Archivo> archivos = new List<Archivo>();
            //getting the directories from local path
            string[] dir = Directory.GetDirectories(fromPath);
            //getting the current dateTime
            string currentTime = DateTime.Now.ToString();
            //getting the specific directories to upload
            foreach (string directory in dir)
            {
                DateTime creationTime = Directory.GetCreationTime(directory);
                months = calculateMonths(creationTime);
                if (months <= Convert.ToInt32(antique))
                {
                    //getting the files from the directory
                    string[] arch = Directory.GetFiles(directory);
                    foreach (string auxArch in arch)
                    {
                        archivo = new Archivo(auxArch.Substring(auxArch.Length - 10), auxArch);
                        archivos.Add(archivo);
                    }
                    directorio = new Directorio(directory, creationTime.ToString(),archivos);
                    //adding to the directory list
                    directorios.Add(directorio);
                }

            }
            return directorios;
        }
        
        /// <summary>
        /// This method calculates the month difference between two dates.
        /// </summary>
        private int calculateMonths(DateTime Creationtime)
        {
            DateTime fi = Creationtime;
            DateTime ft = DateTime.Now;

            int AntMonths = 0, AntYears = 0;

            int monthFI = fi.Month, yearFI = fi.Year;
            int monthFT = ft.Month, yearFT = ft.Year;


            if (monthFT < monthFI)
            {
                monthFT += 12;
                yearFT -= 1;
            }

            // calculates months old
            AntMonths = monthFT - monthFI;

            // calculates years old
            AntYears = yearFT - yearFI;
            AntMonths = AntMonths + (AntYears * 12);

            return AntMonths;
        }
    }
}