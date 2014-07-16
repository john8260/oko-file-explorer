/****************************** Summary ******************************\
*
* This project is created by using WebClient and FtpWebRequest object. 
*
* WebClient and FtpWebRequest Class both provides common methods for sending data 
* to URI of server.
*
* These classes will do webrequest to the url which user types in.
*
* UploadData() method sends a data buffer(without encoding it) to a resource 
* using the HTTP or FTP method specified in the method parameter, and then returns 
* web response from the server. 
* 
\***************************************************************************/

using System;
using System.Net;
using System.IO;
using System.Web.Configuration;
using FileExplorer.Model;
using System.Collections.Generic;

namespace CSRemoteUploadAndDownload
{
    public abstract class RemoteUpload
    {

        public string FileName
        {
            get;
            set;
        }


        public string UrlString
        {
            get;
            set;
        }


        public string NewFileName
        {
            get;
            set;
        }


        public byte[] FileData
        {
            get;
            set;
        }

        public RemoteUpload(byte[] fileData, string fileName, string urlString)
        {
            this.FileData = fileData;
            this.FileName = fileName;
            this.UrlString = System.Web.Configuration.WebConfigurationManager.AppSettings["to"].ToString();
        }

        /// <summary>
        /// upload file to remote server
        /// </summary>
        /// <returns></returns>
        public virtual bool UploadFile()
        {
            return true;
        }

    }

    /// <summary>
    /// HttpUpload class
    /// </summary>
    public class HttpRemoteUpload : RemoteUpload
    {
        public HttpRemoteUpload(byte[] fileData, string fileNamePath, string urlString)
            : base(fileData, fileNamePath, urlString)
        {

        }

        public override bool UploadFile()
        {
            byte[] postData;
            try
            {
                postData = this.FileData;
                using (WebClient client = new WebClient())
                {
                    // Whether credentials are needed use this
                    //String username = WebConfigurationManager.AppSettings["UserName"];
                    //String password = WebConfigurationManager.AppSettings["pass"];
                    //client.Credentials = new System.Net.NetworkCredential(username, password);

                    //Default system credentials
                    client.Credentials = CredentialCache.DefaultCredentials;
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    client.UploadData(this.UrlString, "PUT", postData);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload", ex.InnerException);
            }

        }
    }

    /// <summary>
    /// FtpUpload class
    /// </summary>
    public class FtpRemoteUpload : RemoteUpload
    {
        public FtpRemoteUpload(byte[] fileData, string fileNamePath, string urlString)
            : base(fileData, fileNamePath, urlString)
        {

        }

        public override bool UploadFile()
        {
           
            // Whether credentials are needed use this
            string username = WebConfigurationManager.AppSettings["UserName"];
            string password = WebConfigurationManager.AppSettings["pass"];
            string FtpFilePath = this.UrlString + this.FileName;
            //setting the url
            Uri targetUri = new Uri(Uri.EscapeUriString(FtpFilePath));
            
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(targetUri);
            reqFTP.KeepAlive = true;
            reqFTP.Credentials = new NetworkCredential(username.Normalize(),password.Normalize());
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            //reqFTP.EnableSsl = true;
            reqFTP.ContentLength = this.FileData.Length;
            
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            MemoryStream ms = new MemoryStream(this.FileData);
            
            try
            {
                int contenctLength;
                using (Stream strm = reqFTP.GetRequestStream())
                {
                    contenctLength = ms.Read(buff, 0, buffLength);
                    while (contenctLength > 0)
                    {
                        strm.Write(buff, 0, contenctLength);
                        contenctLength = ms.Read(buff, 0, buffLength);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}