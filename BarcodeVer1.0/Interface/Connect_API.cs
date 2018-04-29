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
        private string thong_tin_khoa_hoc;
        private string urlConnect;
        private string data;

        //read data from html
        private string Url(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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
            urlConnect =urlAddress + "/Login?Username={0}&Password={1}";
            urlConnect = string.Format(urlConnect, email, pass);

            data = Url(urlConnect);
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
                    urlConnect = "";
                    //get id teacher
                    return id;
                }
            }
            urlConnect = "";            
            return "";
        }

        //not finish
        public string GetCourse(string id)
        {
            thong_tin_khoa_hoc = "";
            urlConnect = urlAddress + "/GetCourses?lecturerID={0}";
            urlConnect = string.Format(urlConnect, id);
            data = Url(urlConnect);
            if (data != "")
            {
                //parse data json
                //dynamic stuff = JsonConvert.DeserializeObject(data);

                //get data json type array
                Course Courses = JsonConvert.DeserializeObject<Course>(data);
                
                //int a = Courses.data.Count();
                foreach(var item in Courses.data)
                {
                    //thong_tin_khoa_hoc = thong_tin_khoa_hoc + item.id + "," + item.name + ".";
                    thong_tin_khoa_hoc = thong_tin_khoa_hoc + item.name + " (" + item.id + ")" + "/";
                }
                //
                return thong_tin_khoa_hoc;
                
            }
            return "";

        }
    }
}