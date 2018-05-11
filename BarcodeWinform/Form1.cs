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
            if (mssv != "")
            {
                var date = DateTime.Now.Date;
                var day = db.Lessons.FirstOrDefault(x => x.Day == date).ID;
                var item = db.Attendances.FirstOrDefault(x => x.ID_Student == mssv && x.ID_Lesson == day);
                item.Status = true;
                db.SaveChanges();
                MessageBox.Show("Success");
                return;
            }
            MessageBox.Show("Ma so khong ton tai");
            return;
        }


    }
}
