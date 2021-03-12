using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UIdESIGN.Models;
using UIdESIGN.Repository;

namespace UIdESIGN.Class
{
    public class APIclass : IApiRepository
    {
        private readonly string _baseurl;
        public APIclass(string baseurl)
        {
            _baseurl = baseurl;
        }

        public string Details(Inquiry InQui)
        {
            string val = string.Empty;
            try
            {
                Uri uri = new Uri(string.Format(_baseurl + "Inquiry?startDate={0}&endDate={1}", InQui.startDate.Trim(), InQui.endDate.Trim()));

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

                request.Method = "GET";
                request.ProtocolVersion = HttpVersion.Version11;
                request.ContentType = "application/json";
                request.ServerCertificateValidationCallback = delegate { return true; };

                StreamReader rsps = new StreamReader(request.GetResponse().GetResponseStream());

                val = rsps.ReadToEnd();
            }
            catch (Exception ex)
            {
                val = "ERROR404";
            }
            return val;
        }
        public string RemoveItem(int id)
        {
            string val = string.Empty;
            try
            {
                Uri uri = new Uri(string.Format(_baseurl + "Remove?id={0}", id));

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

                request.Method = "GET";
                request.ProtocolVersion = HttpVersion.Version11;
                //request.Host = uri.ToString();
                request.ContentType = "application/json";
                request.ServerCertificateValidationCallback = delegate { return true; };

                StreamReader rsps = new StreamReader(request.GetResponse().GetResponseStream());

                val = rsps.ReadToEnd();
            }
            catch (Exception)
            {
                val = "Error Response!";
            }
            return val;
        }
        public string PostData()
        {
            string val = string.Empty;
            try
            {
                Uri uri = new Uri(string.Format(_baseurl + "SelectAll"));

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

                request.Method = "GET";
                request.ProtocolVersion = HttpVersion.Version11;
                request.ContentType = "application/json";
                request.ServerCertificateValidationCallback = delegate { return true; };

                StreamReader rsps = new StreamReader(request.GetResponse().GetResponseStream());

                val = rsps.ReadToEnd();
            }
            catch (Exception)
            {
                val = "Error Response!";
            }
            return val;
        }
        public string UpdateData(int id, Update up)
        {
            string val = string.Empty;
            try
            {
                Uri uri = new Uri(string.Format(_baseurl + "Update?id={0}", id));
                string jsonData = JsonConvert.SerializeObject(up);
                string response = string.Empty;
                using (var client = new WebClient())
                {
                    client.Headers.Add("content-type", "application/json");
                    response = Encoding.ASCII.GetString(client.UploadData(uri, "POST", Encoding.UTF8.GetBytes(jsonData)));
                }
                return response;
            }
            catch (Exception)
            {
                val = "ERROR404";
            }
            return val;
        }
        public string addProduct(Add itmadd)
        {
            var val = string.Empty;
            try
            {
                Uri uri = new Uri(string.Format(_baseurl + "Add"));
                string jsonData = JsonConvert.SerializeObject(itmadd);
                string response = string.Empty;
                using (var client = new WebClient())
                {
                    client.Headers.Add("content-type", "application/json");
                    response = Encoding.ASCII.GetString(client.UploadData(uri, "POST", Encoding.UTF8.GetBytes(jsonData)));
                }
                return response;
            }
            catch (Exception)
            {
                val = "ERROR404";
            }
            return val;
        }
    }
}
