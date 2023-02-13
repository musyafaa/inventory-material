using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace inventory_materila
{
    public partial class Form2 : Form
    {
        private Color borderColor = Color.MediumSlateBlue;
        private int borderSize = 2;
        private bool underlinedStyle = false;

        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate();
        }
        } 
        public int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }
        public bool UnderlinedStyle
        {
            get
            {
               return underlinedStyle;
            }
            set
            {
                underlinedStyle = value;
                this.Invalidate();
            }
         }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            //Draw border
            using (Pen penBorder = new Pen(borderColor, borderSize)) ;
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string connection = "server=localhost;user id=root;password=;database=computech";
            string query = "INSERT INTO tbl_material(Nama_Material,Jumlah,Tanggal_Masuk,Tanggal_Dipinjam,Status,Image)VALUES('"+ this.NAMA.Text +"','" + this.JUMLAH.Text + "','" + this.MASUK.Text + "','" + this.DIPINJAM.Text + "','" + this.STATUS.Text + "','" + Path.GetFileName(pictureBox1.ImageLocation) + "')";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            MessageBox.Show("Material Berhasil Ditambahkan!");
            conn.Close();
            File.Copy(ImageText.Text, Application.StartupPath + @"\Image\" + Path.GetFileName(pictureBox1.ImageLocation));
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connection = "server=localhost;user id=root;password=;database=computech";
            string query = "UPDATE tbl_material SET Nama_Material='"+ this.NAMA.Text + "',Jumlah='" + this.JUMLAH.Text + "',Tanggal_Masuk='" + this.MASUK.Text + "',Tanggal_Dipinjam='" + this.DIPINJAM.Text + "',Status='" + this.STATUS.Text + "' WHERE ID='"+ this.ID.Text +"'";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            MessageBox.Show("Material Berhasil Diupdate");
            conn.Close();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            string connection = "server=localhost;user id=root;password=;database=computech";
            string query = "SELECT * FROM tbl_material";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.Add("PICTURE", Type.GetType("System.Byte[]"));

            foreach (DataRow row in dt.Rows)
            {
                row["PICTURE"] = File.ReadAllBytes(Application.StartupPath + @"\Image\" + Path.GetFileName(row["Image"].ToString()));
            }
            dataGridView1.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connection = "server=localhost;user id=root;password=;database=computech";
            string query = "DELETE FROM tbl_material WHERE ID='"+ this.ID.Text +"'";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            MessageBox.Show("Material Berhasil Dihapus");
            conn.Close();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string connection = "server=localhost;user id=root;password=;database=computech";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlDataAdapter da;
            DataTable dt;
            conn.Open();
            da = new MySqlDataAdapter("SELECT * FROM tbl_material WHERE Nama_Material LIKE'" + this.textBox1.Text + "%'", conn);
            dt = new DataTable();
            da.Fill(dt);
            dt.Columns.Add("PICTURE", Type.GetType("System.Byte[]"));

            foreach (DataRow row in dt.Rows)
            {
                row["PICTURE"] = File.ReadAllBytes(Application.StartupPath + @"\Image\" + Path.GetFileName(row["Image"].ToString()));
            }
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfd = new OpenFileDialog();
            openfd.Filter = "Image Files(*.jpg;*.jpeg;*.gif;) | *.jpg;*.jpeg;*.gif; ";
            if (openfd.ShowDialog() == DialogResult.OK)
            {
                ImageText.Text = openfd.FileName;
                pictureBox1.Image = new Bitmap(openfd.FileName);
                pictureBox1.ImageLocation = openfd.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
