using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MainForm
{
    public partial class Class_Ex2 : Form
    {
        SqlConnection conn;
        SqlDataAdapter dataAdapter;
        DataSet ds;

        public Class_Ex2()
        {
            InitializeComponent();
            conn = new SqlConnection("Data Source=ZOHANUBIS;Initial Catalog=QLSinhVien;Integrated Security=True");
            ds = new DataSet();
        }

        private void Load_GVSinhVien(string maLop = "")
        {
            string query = "SELECT * FROM SinhVien";

            if (!string.IsNullOrEmpty(maLop) && maLop != "Tất cả lớp")
            {
                query += $" WHERE MaLop = '{maLop}'";
            }

            dataAdapter = new SqlDataAdapter(query, conn);
            ds.Clear();
            dataAdapter.Fill(ds, "SinhVien");
            dataGridView.DataSource = ds.Tables["SinhVien"];
            DataBinding(ds.Tables["SinhVien"]);
        }

        private void LoadLop_ComboBox()
        {
            conn.Open();
            string slcString = "SELECT * FROM Lop";
            SqlCommand cmd = new SqlCommand(slcString, conn);
            SqlDataReader rd = cmd.ExecuteReader();

            comboBoxLop.Items.Add("Tất cả lớp"); // Dòng đầu tiên
            while (rd.Read())
            {
                comboBoxLop.Items.Add(rd["TenLop"].ToString());
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

        private void comboBoxLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLop = comboBoxLop.SelectedItem.ToString();

            if (selectedLop == "Tất cả lớp")
            {
                Load_GVSinhVien(); // Hiển thị toàn bộ sinh viên
            }
            else
            {
                // Hiển thị sinh viên theo mã lớp
                Load_GVSinhVien(GetMaLopFromTenLop(selectedLop));
            }
        }

        private string GetMaLopFromTenLop(string tenLop)
        {
            // Hàm này trả về mã lớp từ tên lớp
            string query = "SELECT MaLop FROM Lop WHERE TenLop = @TenLop";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenLop", tenLop);

            conn.Open();
            string maLop = cmd.ExecuteScalar()?.ToString() ?? "";
            conn.Close();

            return maLop;
        }

        private void DataBinding(DataTable dt)
        {
            txtMaSinhVien.DataBindings.Clear();
            txtTenSinhVien.DataBindings.Clear();
            maskedTextBoxNgaySinh.DataBindings.Clear();
            comboBoxLop.DataBindings.Clear();

            txtMaSinhVien.DataBindings.Add("Text", dt, "MaSinhVien");
            txtTenSinhVien.DataBindings.Add("Text", dt, "HoTen");
            maskedTextBoxNgaySinh.DataBindings.Add("Text", dt, "NgaySinh");
            comboBoxLop.DataBindings.Add("Text", dt, "MaLop");
        }

        private void Class_Ex2_Load(object sender, EventArgs e)
        {
            LoadLop_ComboBox();
            Load_GVSinhVien();
        }

        private void comboBoxLop_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
