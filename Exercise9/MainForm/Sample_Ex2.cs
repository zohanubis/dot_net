using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MainForm
{
    public partial class Sample_Ex2 : Form
    {

        SqlConnection conn;
        SqlDataAdapter dataAdapter;
        DataSet ds_Khoa;
        DataColumn[] key = new DataColumn[1];

        public Sample_Ex2()
        {
            InitializeComponent();
            conn = new SqlConnection("Data Source=ZOHANUBIS;Initial Catalog=QLSinhVien;Integrated Security=True");
            string strSelect = "Select * from Khoa";
            dataAdapter = new SqlDataAdapter(strSelect, conn);
            ds_Khoa = new DataSet();
            dataAdapter.Fill(ds_Khoa,"Khoa");
            key[0] = ds_Khoa.Tables["Khoa"].Columns[0];
            ds_Khoa.Tables["Khoa"].PrimaryKey = key;
        }
        void DataBindings(DataTable dt)
        {
            txtMaKhoa.DataBindings.Clear();
            txtTenKhoa.DataBindings.Clear();

            txtMaKhoa.DataBindings.Add("Text", dt, "MaKhoa");
            txtTenKhoa.DataBindings.Add("Text", dt, "TenKhoa");
        }
        public void Load_DGVLop()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            dataGridView1.DataSource = ds_Khoa.Tables[0];
            DataBindings(ds_Khoa.Tables[0]);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow newRow = ds_Khoa.Tables["Khoa"].NewRow();
            newRow["MaKhoa"] = txtMaKhoa.Text;
            newRow["TenKhoa"] = txtTenKhoa.Text;

            ds_Khoa.Tables["Khoa"].Rows.Add(newRow);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Update(ds_Khoa, "Khoa");
            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow selectedRow = ds_Khoa.Tables["Khoa"].Rows.Find(txtMaKhoa.Text);

            if (selectedRow != null)
            {
                selectedRow.Delete();
              
            }
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Update(ds_Khoa, "Khoa");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataRow selectedRow = ds_Khoa.Tables["Khoa"].Rows.Find(txtMaKhoa.Text);

            if (selectedRow != null)
            {
                selectedRow["TenKhoa"] = txtTenKhoa.Text;
                
            }
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Update(ds_Khoa, "Khoa");
        }

        private void Sample_Ex2_Load(object sender, EventArgs e)
        {
            Load_DGVLop();
        }
    }
}
