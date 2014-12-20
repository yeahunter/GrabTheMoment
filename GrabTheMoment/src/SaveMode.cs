using System;
using System.Drawing;
using System.Drawing.Imaging;
using GrabTheMoment.Forms;
using GrabTheMoment.Properties;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using System.Xml;

namespace GrabTheMoment
{
    static class SaveMode
    {
        public static void Local(Bitmap bmpScreenShot, string neve)
        {
            try
            {
                string path = Path.Combine(Settings.Default.MLocal_path, neve + ".png");
                bmpScreenShot.Save(path, ImageFormat.Png);
                if (Settings.Default.CopyLink == (int)Main.CopyType.Local)
                {
                    Log.WriteEvent(string.Format("path: {0}", path));
                    InterceptKeys.Klipbood(path);
                }
            }
            catch (Exception e)
            {
                Log.WriteEvent(String.Empty, e);
            }
        }

        public static void Dropbox(Bitmap bmpScreenShot, string neve)
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

                if (Settings.Default.CopyLink == (int)Main.CopyType.Dropbox)
                    InterceptKeys.Klipbood(API.Dropbox_oauth1.Share(neve));
            }
            catch (Exception e)
            {
                Log.WriteEvent(String.Empty, e);
            }
            //if (!File.Exists(Settings.Default.MDropbox_path))
            //    System.IO.Directory.CreateDirectory(Settings.Default.MDropbox_path);
            //bmpScreenShot.Save(Settings.Default.MDropbox_path + "\\" + neve + ".png", ImageFormat.Png);
        }

        public static void FTP(Bitmap bmpScreenShot, string neve)
        {
            try
            {
                neve = neve + ".png";
                Uri HttpLink = new Uri(String.Format("{0}/{1}", Settings.Default.MFtp_path, neve));
                Uri FtpLink = new Uri(String.Format("ftp://{0}/{1}/{2}", Settings.Default.MFtp_address, Settings.Default.MFtp_remotedir, neve));
                FtpWebRequest req = (FtpWebRequest)WebRequest.Create(FtpLink);
                req.UseBinary = true;
                //req.UsePassive = true;
                req.KeepAlive = false;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.Credentials = new NetworkCredential(Settings.Default.MFtp_user, Settings.Default.MFtp_password);
                byte[] filedata = new byte[0];
                req.ContentLength = filedata.Length;

                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(filedata, 0, filedata.Length);
                    reqStream.Close();
                }

                using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
                {
                    Log.WriteEvent("Upload File Complete, status " + resp.StatusDescription);
                    resp.Close();
                }

                if (Settings.Default.CopyLink == (int)Main.CopyType.FTP)
                    InterceptKeys.Klipbood(HttpLink.OriginalString);
            }
            catch (Exception e)
            {
                Log.WriteEvent(String.Empty, e);
            }
        }

        public static void ImgurAnon(Bitmap bmpScreenShot, string neve)
        {
            try
            {
                neve = neve + ".png";

                string holakep = string.Empty;

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
                    Log.WriteEvent("Rossz response!");
                }

                if (Settings.Default.CopyLink == (int)Main.CopyType.ImgurAnon)
                    InterceptKeys.Klipbood(holakep);
            }
            catch (Exception e)
            {
                Log.WriteEvent(String.Empty, e);
            }
        }
    }
}
