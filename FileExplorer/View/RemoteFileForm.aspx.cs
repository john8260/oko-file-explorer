using System;
using System.IO;
using System.Web.Configuration;
using FileExplorer.Controller;
using System.Collections.Generic;
using FileExplorer.Model;

namespace CSRemoteUploadAndDownload
{
    public partial class RemoteFileForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //directory controller instance
            DirectoryController controller = new DirectoryController();
            //getting the directories (the key antique in web.config is defined in months)
            if (Directory.Exists(WebConfigurationManager.AppSettings["from"]))
            {
                List<Directorio> directories = controller.getDirectories();
                Label1.Text = uploadFiles(directories);                                
            }
            else
            {
                Label1.Text = "Invalid Directory";
            }
        }

        private string uploadFiles(List<Directorio> directories)
        {
            //Delcaring parsing variables
            RemoteUpload uploadClient = null;
            string response = "";

            foreach (Directorio dir in directories)
            {
                //getting the directory files
                List<Archivo> files = dir.Files;
                foreach (Archivo fil in files)
                {
                    //getting the byte array of the file
                    byte[] fileData = File.ReadAllBytes(fil.Path);
                    //uploading the file to remote server
                    try
                    {
                        uploadClient = new FtpRemoteUpload(fileData, fil.Name, fil.Path);
                        uploadClient.UploadFile();
                    }
                    catch (Exception ex)
                    {
                        response = ex.Message;
                        return response;
                    }
                }
            }
            response = "upload is complete";
            return response;
        }
    }
}