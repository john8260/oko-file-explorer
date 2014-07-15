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
            RemoteUpload uploadClient = null;
            //directory controller instance
            DirectoryController controller = new DirectoryController();
            //getting the directories (the key antique in web.config is defined in months)
            List<Directorio> directories = controller.getDirectories();
            foreach (Directorio dir in directories)
            {
                //getting the directory files
                List<Archivo> files = dir.Files;
                foreach (Archivo fil in files)
                {
                    //getting the byte array of the file
                    byte[] fileData = File.ReadAllBytes(fil.Path);
                    //uploading the file to remote server
                    uploadClient = new FtpRemoteUpload(fileData,fil.Name,fil.Path);

                    if (uploadClient.UploadFile())
                    {
                        Response.Write("Upload is complete");
                    }
                    else
                    {
                        Response.Write(" Failed to upload");
                    }
                }
            }
        }
    }
}