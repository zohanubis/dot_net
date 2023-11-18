using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MainForm
{
    public partial class Class_Ex1 : Form
    {
        SqlConnection conn;
        SqlDataAdapter dataAdapter;
        DataSet ds;

        public Class_Ex1()
        {
            InitializeComponent();
            conn = new SqlConnection("Data Source=ZOHANUBIS;Initial Catalog=QLSinhVien;Integrated Security=True");
            ds = new DataSet();
        }

        private void Load_GVLop(string maKhoa = "")
        {
            string query = "SELECT Lop.MaLop, Lop.TenLop, Lop.MaKhoa, Khoa.TenKhoa FROM Lop JOIN Khoa ON Lop.MaKhoa = Khoa.MaKhoa";

            if (!string.IsNullOrEmpty(maKhoa) && maKhoa != "Tất cả khoa")
            {
                query += $" WHERE Lop.TenKhoa = '{maKhoa}'";
            }

            dataAdapter = new SqlDataAdapter(query, conn);
            ds.Clear();
            dataAdapter.Fill(ds, "Lop");
            dataGridView.DataSource = ds.Tables["Lop"];
            DataBinding1(ds.Tables["Lop"]);
        }
        private void Load_GVLopByKhoa(string tenKhoa)
        {
            string query = $"SELECT Lop.MaLop, Lop.TenLop, Lop.MaKhoa, Khoa.TenKhoa FROM Lop JOIN Khoa ON Lop.MaKhoa = Khoa.MaKhoa WHERE Khoa.TenKhoa = '{tenKhoa}'";

            dataAdapter = new SqlDataAdapter(query, conn);
            ds.Clear();
            dataAdapter.Fill(ds, "Lop");
            dataGridView.DataSource = ds.Tables["Lop"];
            DataBinding1(ds.Tables["Lop"]);
        }
        private void LoadKhoa_ComboBox()
        {
            conn.Open();
            string slcString = "SELECT * FROM Khoa";
            SqlCommand cmd = new SqlCommand(slcString, conn);
            SqlDataReader rd = cmd.ExecuteReader();

            comboBoxKhoa.Items.Add("Tất cả khoa"); // Dòng đầu tiên
            while (rd.Read())
            {
                comboBoxKhoa.Items.Add(rd["TenKhoa"].ToString());
            }
            rd.Close();
            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Viết code thêm dữ liệu
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Viết code xóa dữ liệu
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Viết code sửa dữ liệu
        }



      

        private void DataBinding1(DataTable dt)
        {
            txtMaLop.DataBindings.Clear();
            txtTenLop.DataBindings.Clear();
            comboBoxKhoa.DataBindings.Clear();

            txtMaLop.DataBindings.Add("Text", dt, "MaLop");
            txtTenLop.DataBindings.Add("Text", dt, "TenLop");
            comboBoxKhoa.DataBindings.Add("Text", dt, "TenKhoa");
        }

        private void Class_Ex1_Load(object sender, EventArgs e)
        {
            LoadKhoa_ComboBox();
            Load_GVLop();
        }

        private void comboBoxKhoa_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedTenKhoa = comboBoxKhoa.SelectedItem.ToString();

            if (selectedTenKhoa == "Tất cả khoa")
            {
                // Nếu chọn "Tất cả khoa", hiển thị toàn bộ dan
                // h sách lớp
                Load_GVLop();
            }
            else
            {
                // Nếu chọn một khoa cụ thể, hiển thị danh sách lớp của khoa đó
                Load_GVLopByKhoa(selectedTenKhoa);
            }
        }
    }
}
