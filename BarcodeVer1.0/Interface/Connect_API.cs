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

        //private string urlAddress = "https://www.google.com";
        //private string urlAddress = "https://entool.azurewebsites.net/SEP21";
        private string urlAddress;
        private string urlConnect;
        private string data;

        private string Get()
        {
            return urlAddress;
        }

        public void Set(string newLink)
        {
            urlAddress = newLink;
        }

        //read data from html
        private string Url(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response;
            //kiem tra coi co ket noi duoc voi duong dan do khong
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }

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
        public Logins Check_Login(string email, string pass)
        {
            //url address
            urlConnect =Get() + "/Login?Username={0}&Password={1}";
            urlConnect = string.Format(urlConnect, email, pass);

            data = Url(urlConnect);
            if (data != "")
            {
                //parse data json
                Logins stuff = JsonConvert.DeserializeObject<Logins>(data);
                //get data json
                //compare login success or not
                return stuff;
            }
            urlConnect = "";            
            return null;
        }

        public List<Student> GetMember (string id)
        {
            urlConnect = Get() + "/GetMembers?courseID={0}";
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
            urlConnect = Get() + "/GetCourses?lecturerID={0}";
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

        public GetStudent Getstudent(string mssv)
        {
            //url address
            urlConnect = Get() + "/GetStudent?code={0}";
            urlConnect = string.Format(urlConnect, mssv);

            data = Url(urlConnect);
            if (data != "")
            {
                //parse data json
                GetStudent st = JsonConvert.DeserializeObject<GetStudent>(data);
                //get data json
                return st;
            }
            urlConnect = "";
            return null;
        }

        //syss member to server
        public ResponseData SyncMembers(string MaKH,string uid,string secret)
        {
            HttpClient client = new HttpClient();
            
            client.BaseAddress = new Uri(Get());
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
                uid = uid,
                secret = secret,
                data = mydata
            };

            //post data to server
            var result = client.PostAsJsonAsync(Get() + "/SyncMembers/", dataForm);
            //get response data
            var response = result.Result.Content.ReadAsStringAsync().Result.ToString();
            ResponseData nResDa = JsonConvert.DeserializeObject<ResponseData>(response);

            return nResDa;
        }

        public ResponseData SyncAttendance(string MaKH, string prUid, string prSecret)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Get());
            //chuan bi du lieu cho truong data
            var value = new
            {
                course = MaKH,
                sessions = db.Lessons.Where(x => x.MaKH == MaKH).AsEnumerable().Select(b => new
                {
                    id = b.Count,
                    date = DateTime.Parse(b.Day.ToString()).ToString("yyyy-MM-ddThh:mm:ss"),
                    info = string.Empty
                }).ToArray(),
                attendance = db.Members.AsEnumerable().Where(x => x.MaKH == MaKH).Select(m => new
                {
                    student = m.MaSV,
                    checklist = db.Attendances.Where(x => x.Status == true & x.Member.MaSV == m.MaSV).Select(z => z.Lesson.Count).ToArray(),
                    info = string.Empty
                })

            };
            var predata = JsonConvert.SerializeObject(value);
            //chuan bi du lieu de post
            var dataform = new
            {
                uid = prUid,
                secret = prSecret,
                data = predata
            };
            //post data to server
            var result = client.PostAsJsonAsync(Get() + "/SyncAttendance/", dataform);
            //get response data
            var response = result.Result.Content.ReadAsStringAsync().Result.ToString();
            ResponseData nResDa = JsonConvert.DeserializeObject<ResponseData>(response);

            return nResDa;
        }


    }
}