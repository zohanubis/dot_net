using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MainForm
{
    public partial class Sample_Ex1 : Form
    {
        private string connectionString = "Data Source=ZOHANUBIS;Initial Catalog=QLSinhVien;Integrated Security=True";

        // Tạo một DataSet để lưu trữ dữ liệu
        private DataSet ds_QLSinhVien = new DataSet();

        public Sample_Ex1()
        {
            InitializeComponent();
        }

        public void Load_ComboBox()
        {
            string strSelect = "SELECT MaKhoa, TenKhoa FROM Khoa";
            SqlDataAdapter daKhoa = new SqlDataAdapter(strSelect, connectionString);
            daKhoa.Fill(ds_QLSinhVien, "Khoa");

            comboBoxKhoa.DataSource = ds_QLSinhVien.Tables["Khoa"];
            comboBoxKhoa.DisplayMember = "TenKhoa";
            comboBoxKhoa.ValueMember = "MaKhoa";
        }
        public bool KT_KhoaChinh(string pMa)
        {
            using (SqlConnection conStr = new SqlConnection(connectionString))
            {
                conStr.Open();
                string slcString = "Select * from Lop where MaLop ='" + pMa + "'";
                SqlCommand cmd = new SqlCommand(slcString, conStr);
                SqlDataReader rd = cmd.ExecuteReader();
                bool result = rd.HasRows;
                rd.Close();
                return !result;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ giao diện
            string maLop = txtMaLop.Text;
            string tenLop = txtTenLop.Text;
            string maKhoa = comboBoxKhoa.SelectedValue.ToString();

            if (!KT_KhoaChinh(maLop))
            {
                MessageBox.Show("Mã lớp đã tồn tại. Vui lòng chọn mã lớp khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string insertQuery = "INSERT INTO Lop (MaLop, TenLop, MaKhoa) VALUES (@MaLop, @TenLop, @MaKhoa)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@MaLop", maLop);
                    cmd.Parameters.AddWithValue("@TenLop", tenLop);
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Thêm lớp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Load_ComboBox();
            ClearInputFields();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ giao diện
            string maLop = txtMaLop.Text;

            // Thực hiện xóa trong cơ sở dữ liệu
            string deleteQuery = "DELETE FROM Lop WHERE MaLop = @MaLop";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@MaLop", maLop);

                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Xóa lớp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Cập nhật lại ComboBox và xóa dữ liệu nhập trên giao diện
            Load_ComboBox();
            ClearInputFields();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string maLop = txtMaLop.Text;
            string tenLop = txtTenLop.Text;
            string maKhoa = comboBoxKhoa.SelectedValue.ToString();

            if (!KT_KhoaChinh(maLop))
            {
                MessageBox.Show("Mã lớp không tồn tại. Vui lòng chọn mã lớp khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string updateQuery = "UPDATE Lop SET TenLop = @TenLop, MaKhoa = @MaKhoa WHERE MaLop = @MaLop";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@MaLop", maLop);
                    cmd.Parameters.AddWithValue("@TenLop", tenLop);
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);

                    cmd.ExecuteNonQuery();
                }
            }

            // Thông báo thành công
            MessageBox.Show("Sửa lớp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Load_ComboBox();
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            txtMaLop.Text = string.Empty;
            txtTenLop.Text = string.Empty;
        }

        private void Sample_Ex1_Load(object sender, EventArgs e)
        {
            Load_ComboBox();
        }
    }
}
