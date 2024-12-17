using Microsoft.Data.SqlClient;
using System.Data;

namespace DBMS.SQL.EmployeeSales
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection;
        SqlCommand cmd;
        SqlDataAdapter adapter;

        private void connectDB()
        {
            string server = @".\sqlexpress";
            string db = "northwind";
            string strCon = string.Format(@"Data Source={0};initial catalog={1};" + "Integrated security = true; Encrypt = false", server, db);
            connection = new SqlConnection(strCon);
            connection.Open();
        }

        private void disconnectDB()
        {
            connection.Close();
        }

        private void showData(string sql, DataGridView dgv)
        {
            adapter = new SqlDataAdapter(sql, connection);
            DataSet ds = new DataSet();
            //สั่งให้เอาข้อมูลไปใส่ไว่ใน Data set
            adapter.Fill(ds);
            dgv.DataSource = ds.Tables[0];
            dgv.Columns["Count Order"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["sales Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectDB();
            string sqlQuery = "select * from EmployeeList order by EmployeeID asc "; 

            showData(sqlQuery, dgvEmployee);

        }

        private void dgvEmployee_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int id = Convert.ToInt32(dgvEmployee.CurrentRow.Cells[0].Value);
                string sqlQuery = "SELECT * FROM Orderlist2 WHERE EmployeeID = @id";
                cmd = new SqlCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgvOrderList.DataSource = ds.Tables[0];

            }
        }
    }
}
