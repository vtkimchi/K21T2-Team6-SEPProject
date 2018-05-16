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

        public List<Student> GetMember (string id)
        {
            urlConnect = urlAddress + "/GetMembers?courseID={0}";
            urlConnect = string.Format(urlConnect, id);
            data = Url(urlConnect);
            if (data != "")
            {
                //tao noi luu tru vao model
                var Student = new List<Student>();
                //parse data json
                //get data json type array
                Info items = JsonConvert.DeserializeObject<Info>(data);
                foreach (var item in items.data)
                {
                    Student.Add(item);
                }
                return Student;

            }
            return null;
        }

        public List<Course.Datum> TestCourse(string id)
        {
            urlConnect = urlAddress + "/GetCourses?lecturerID={0}";
            urlConnect = string.Format(urlConnect, id);
            data = Url(urlConnect);
            if (data != "")
            {
                //parse data json
                var coure = new List<Course.Datum>();

                //get data json type array
                Course Courses = JsonConvert.DeserializeObject<Course>(data);
                foreach(var item in Courses.data)
                {
                    coure.Add(item);
                }
                
                //
                return coure;

            }
            return null;
        }

        public string Getstudent(string mssv)
        {
            //url address
            urlConnect = urlAddress + "/GetStudent?code={0}";
            urlConnect = string.Format(urlConnect, mssv);

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
                    string firstname = stuff.data.firstname;
                    string lastname = stuff.data.lastname;
                    urlConnect = "";
                    //get id teacher
                    return firstname +"/" + lastname;
                }
            }
            urlConnect = "";
            return "";
        }

    }
}