using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using BarcodeVer1._0.Models;
using System.Web.Script.Serialization;
using System.Web;
using System.Net.Http.Headers;
using System;

namespace BarcodeVer1._0.Interface
{
    public class Connect_API
    {
        SEPEntities db = new SEPEntities();

        private string urlAddress = "https://entool.azurewebsites.net/SEP21";
        private string urlConnect;
        private string data;

        //private string get()
        //{
        //    return db.Link_API.FirstOrDefault(x => x.ID == 1).Link;
        //}

        //public void set(string newLink)
        //{
        //    db.Link_API.FirstOrDefault(x => x.ID == 1).Link = newLink;
        //    db.SaveChanges();
        //}

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

        //syss member to server
        public ResponseData SysnMember(string MaKH)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlAddress);
            //lay ra nhung sinh vien theo ma khoa hoc
            var sv = new
            {
                course = MaKH,
                members = db.Members.Where(x => x.MaKH == MaKH).Select(b => b.MaSV).ToArray()
            };
            //convert sv thanh chuoi json
            var mydata = JsonConvert.SerializeObject(sv);
            //chuan bi du lieu theo form can post
            var dataForm = new
            {
                uid = "TH",
                secret = "-1781996535",
                data = mydata
            };

            //post data to server
            var result = client.PostAsJsonAsync(urlAddress + "/SyncMembers/", dataForm);
            //get response data
            var a = result.Result.Content.ReadAsStringAsync().Result.ToString();
            dynamic f = JsonConvert.DeserializeObject(a);

            ResponseData nResDa = new ResponseData();


            nResDa.code = f.code;
            nResDa.message= f.message;

            return nResDa;
        }
            
    }
}