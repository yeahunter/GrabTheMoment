using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing.Imaging;
using GrabTheMoment.Properties;
using System.IO;
using System.Net;
using System.Web;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Linq;

namespace GrabTheMoment.Savemode
{
    class allmode
    {

        public void MLocal_SavePS(Bitmap bmpScreenShot, string neve)
        {
            try
            {
                bmpScreenShot.Save(Settings.Default.MLocal_path + "\\" + neve + ".png", ImageFormat.Png);
                if (Settings.Default.CopyLink == 1)
                {
                    Log.WriteEvent("Form1/MLocal_SavePS: ertek: " + Settings.Default.MLocal_path + "\\" + neve + ".png");
                    InterceptKeys.Klipbood(Settings.Default.MLocal_path + "\\" + neve + ".png");
                }
            }
            catch (Exception e)
            {
                Log.WriteEvent("Form1/MLocal_SavePS: ", e);
            }
        }

        public void MDropbox_SavePS(Bitmap bmpScreenShot, string neve)
        {
            try
            {
                neve = neve + ".png";
                byte[] filedata = new byte[0];
                using (MemoryStream stream = new MemoryStream())
                {
                    bmpScreenShot.Save(stream, ImageFormat.Png);
                    stream.Close();

                    filedata = stream.ToArray();
                }
                API.Dropbox_oauth1.Upload(filedata, neve);

                if (Settings.Default.CopyLink == 3)
                    InterceptKeys.Klipbood(API.Dropbox_oauth1.Share(neve));
            }
            catch (Exception e)
            {
                Log.WriteEvent("Form1/MDropbox_SavePS: ", e);
            }
            //if (!File.Exists(Settings.Default.MDropbox_path))
            //    System.IO.Directory.CreateDirectory(Settings.Default.MDropbox_path);
            //bmpScreenShot.Save(Settings.Default.MDropbox_path + "\\" + neve + ".png", ImageFormat.Png);
        }

        public void MFtp_SavePS(Bitmap bmpScreenShot, string neve)
        {
            try
            {
                neve = neve + ".png";
                string ezittapath = Settings.Default.MFtp_path + "/" + neve;
                FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://" + Settings.Default.MFtp_address + "/" + Settings.Default.MFtp_remotedir + "/" + neve);
                req.UseBinary = true;
                //req.UsePassive = true;
                req.KeepAlive = false;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.Credentials = new NetworkCredential(Settings.Default.MFtp_user, Settings.Default.MFtp_password);
                byte[] filedata = new byte[0];
                req.ContentLength = filedata.Length;
                Stream reqStream = req.GetRequestStream();
                reqStream.Write(filedata, 0, filedata.Length);
                reqStream.Close();
                reqStream.Dispose();

                FtpWebResponse resp = (FtpWebResponse)req.GetResponse();
                Log.WriteEvent("Upload File Complete, status " + resp.StatusDescription);

                resp.Close();
                resp.Dispose();

                if (Settings.Default.CopyLink == 2)
                    InterceptKeys.Klipbood(ezittapath);
            }
            catch (Exception e)
            {
                Log.WriteEvent("Form1/MFtp_SavePS: ",e);
            }
        }

        public void MImgur_SavePS(Bitmap bmpScreenShot, string neve)
        {
            try
            {
                neve = neve + ".png";

                string holakep = "";

                byte[] filedata = new byte[0];
                using (MemoryStream stream = new MemoryStream())
                {
                    bmpScreenShot.Save(stream, ImageFormat.Png);
                    stream.Close();

                    filedata = stream.ToArray();
                }

                byte[] response;
                using (var w = new WebClient())
                {
                    w.Headers.Add("Authorization", "Client-ID ac06aa80956fe83");
                    var values = new NameValueCollection
                    {
                        { "image", Convert.ToBase64String(filedata) },
                        { "type", "base64" },
                        { "name", neve },
                        { "title", "GrabTheMoment - " + neve }
                    };

                    response = w.UploadValues("https://api.imgur.com/3/upload.xml", values);
                }

                XmlDocument xdoc = new XmlDocument();
                try
                {
                    xdoc.Load(new MemoryStream(response));
                    //string stat = xdoc.GetElementsByTagName("data")[0].Attributes.GetNamedItem("status").Value;
                    //string odeletehash = xdoc.GetElementsByTagName("deletehash")[0].InnerText;
                    holakep = xdoc.GetElementsByTagName("link")[0].InnerText;
                }
                catch
                {
                    Log.WriteEvent("Form1/MImgur_SavePS: Rossz response!");
                }

                if (Settings.Default.CopyLink == 4)
                    InterceptKeys.Klipbood(holakep);
            }
            catch (Exception e)
            {
                Log.WriteEvent("Form1/MImgur_SavePS: ", e);
            }
        }
    }
}
