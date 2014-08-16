﻿using System;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Specialized;
using GrabTheMoment.Properties;

namespace GrabTheMoment.Savemode
{
    class allmode
    {
        public void MLocal_SavePS(Bitmap bmpScreenShot, string neve)
        {
            try
            {
                string path = Path.Combine(Settings.Default.MLocal_path, neve + ".png");
                bmpScreenShot.Save(path, ImageFormat.Png);

                if (Settings.Default.CopyLink == 1)
                {
                    Log.WriteEvent("Form1/MLocal_SavePS: ertek: " + path);
                    InterceptKeys.Klipbood(path);
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
                string ezittapath = Path.Combine(Settings.Default.MFtp_path, neve);
                var req = (FtpWebRequest)WebRequest.Create("ftp://" + Path.Combine(Settings.Default.MFtp_address, Settings.Default.MFtp_remotedir, neve));
                req.UseBinary = true;
                //req.UsePassive = true;
                req.KeepAlive = false;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.Credentials = new NetworkCredential(Settings.Default.MFtp_user, Settings.Default.MFtp_password);
                byte[] filedata = new byte[0];
                req.ContentLength = filedata.Length;

                using (var reqStream = req.GetRequestStream())
                {
                    reqStream.Write(filedata, 0, filedata.Length);
                    reqStream.Close();
                }

                using (var resp = (FtpWebResponse)req.GetResponse())
                {
                    Log.WriteEvent("Upload File Complete, status " + resp.StatusDescription);
                    resp.Close();
                }

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

                try
                {
                    var xdoc = new XmlDocument();
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
