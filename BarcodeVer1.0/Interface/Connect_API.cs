using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using BarcodeVer1._0.Models;

namespace BarcodeVer1._0.Interface
{
    public class Connect_API
    {
        private string urlAddress = "https://entool.azurewebsites.net/SEP21";
        private string data;

        //read data from html
        private string Url(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                return data;
            }
            return "";
        }


        //check and get data when login success
        public string Check_Login(string email, string pass)
        {
            //url address
            urlAddress =urlAddress + "/Login?Username={0}&Password={1}";
            urlAddress = string.Format(urlAddress, email, pass);

            data = Url(urlAddress);
            if (data != "")
            {
                //parse data json
                dynamic stuff = JsonConvert.DeserializeObject(data);
                //get data json
                string code = stuff.code;
                //compare login success or not
                if (int.Parse(code) == 0)
                {
                    string id = stuff.data.id;
                    //get id teacher
                    return id;
                }
            }            
            return "";
        }

        //not finish
        public string GetCourse(string id)
        {
            urlAddress = urlAddress + "GetCourses?lecturerID={0}";
            urlAddress = string.Format(urlAddress, id);
            data = Url(urlAddress);
            if (data != "")
            {
                //parse data json
                dynamic stuff = JsonConvert.DeserializeObject(data);
                //get data json
                Course Courses = JsonConvert.DeserializeObject<Course>(data);

                string ids = Courses.data.ToString();

                //string []ids = stuff.data[id];
                //string name = stuff.data.name;
                ////get id teacher
                return ids;
                
            }
            return "";

        }
    }
}