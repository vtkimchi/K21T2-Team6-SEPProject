using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarcodeVer1._0.Models;
using BarcodeVer1._0.Interface;

namespace BarcodeWinform
{
    public partial class Form1 : Form
    {

        SEP_DBEntities1 db;
        Connect_API connect = new Connect_API();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db = new SEP_DBEntities1(); 
            string mssv = textBox1.Text;
            var item = connect.GetMember("TH2");
            if (mssv != "")
            {
                Attendance nAtten = new Attendance();
                //chua bat truong hop neu khong dung mssv
                var std = item.FirstOrDefault(x => x.id == mssv);
                if (std != null)
                {
                    nAtten.ID_Student = std.id;
                    //luu lai id ngay hoc
                    var date = DateTime.Now.Date;
                    var day = db.Lessons.FirstOrDefault(x => x.Day == date);
                    if(day != null)
                    {
                        nAtten.ID_Lesson = day.ID;

                        //dem xem buoi hoc do la buoi hoc thu may
                        var nLesson = db.Lessons.Where(x => x.MaKH == "TH2").Count();
                        nAtten.Count_Lesson = nLesson;
                        //luu lai trang thai la sv do co di hoc
                        nAtten.Status = true;
                        db.Attendances.Add(nAtten);
                        db.SaveChanges();
                        MessageBox.Show("success");
                        return;
                    }
                    MessageBox.Show("Khong can diem danh");
                    return;

                }
                MessageBox.Show("Ma so khong ton tai");

            }
        }

    }
}
