using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ReceiptTracker
{
    public partial class Form1 : Form
    {

        SqlConnection conn = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        int rowIndex = -1;

        public Form1()
        {
            InitializeComponent();
        }

        private void getData()
        {
            string connStr = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\PROG37721\\ReceiptTracker\\ReceiptTracker\\receiptDB.mdf;Integrated Security=True;User Instance=True";
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                string sql = "SELECT * FROM [tReceipt]";
                da = new SqlDataAdapter(sql, conn);
                //SqlCommandBuilder cb = new SqlCommandBuilder(da);
                //createCommands();
                ds = new DataSet();
                da.Fill(ds, "tReceipt");
                conn.Close();
                //bind and display
                bindingSource1.DataSource = ds;
                bindingSource1.DataMember = "tReceipt";
                dg1.DataSource = bindingSource1;
                dg1.ClearSelection();
            }
            catch (SqlException ex)
            {
                if (conn != null)
                {
                    conn.Close();
                }
                MessageBox.Show(ex.Message, "Error Reading Data");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getData();

            SqlCommand cmd = new SqlCommand();
            string sql = "INSERT INTO [tReceipt] ([date], [description],[amount]) VALUES (@date, @desc, @amount)";
            cmd.Connection = conn;
            cmd.CommandText = sql;
            //create parameters
            cmd.Parameters.Add("@date", SqlDbType.DateTime, 3, "Date");
            cmd.Parameters.Add("@desc", SqlDbType.VarChar, 63, "Desc");
            cmd.Parameters.Add("@amount", SqlDbType.Decimal, 9, "Amount");

            da.InsertCommand = cmd;

            DataRow dr = ds.Tables["tReceipt"].NewRow();
            dr["Date"] = txtDate;
            dr["Desc"] = "hello";
            dr["Amount"] = 121.43;
            ds.Tables["tReceipt"].Rows.Add(dr);
            da.Update(ds, "tReceipt");

            getData();


        }

        private void cmdAddReceipt_Click(object sender, EventArgs e)
        {

        }

        private void cmdDeleteReceipt_Click(object sender, EventArgs e)
        {

        }

        private void monthSelector_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdSave_Click(object sender, EventArgs e)
        {

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {

        }

        private void cmdEditReceipt_Click(object sender, EventArgs e)
        {

        }

        private void cmdEditReceiptItems_Click(object sender, EventArgs e)
        {

        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtTags_TextChanged(object sender, EventArgs e)
        {

        }

        private bool isDataGood()
        {
            return true;
        }

        private void clearInputs()
        {
            txtAmount.Text = "";
        }

        private void createCommands()
        {
            //create INSERT command for DataAdapter
            SqlCommand cmd = new SqlCommand();
            string sql = "INSERT INTO [tClients] ([Account],[FirstName],[LastName],[Balance]) VALUES (@account, @firstname, @lastname, @balance)";
            cmd.Connection = conn;
            cmd.CommandText = sql;
            //create parameters
            cmd.Parameters.Add("@account", SqlDbType.Int, 4, "Account");
            cmd.Parameters.Add("@firstname", SqlDbType.VarChar, 15, "FirstName");
            cmd.Parameters.Add("@lastname", SqlDbType.VarChar, 15, "LastName");
            cmd.Parameters.Add("@balance", SqlDbType.Float, 8, "Balance");
            //add to DataAdapter
            da.InsertCommand = cmd;

            //create UPDATE command
            cmd = new SqlCommand();
            sql = "UPDATE [tClients] SET [Account] = @account, [FirstName] = @firstnamt, [LastName] = @lastname, [Balance] = @balance WHERE [Account] = @origAccount";
            cmd.Connection = conn;
            cmd.CommandText = sql;
            //create parameters
            cmd.Parameters.Add("@account", SqlDbType.Int, 4, "Account");
            cmd.Parameters.Add("@firstname", SqlDbType.VarChar, 15, "FirstName");
            cmd.Parameters.Add("@lastname", SqlDbType.VarChar, 15, "LastName");
            cmd.Parameters.Add("@balance", SqlDbType.Float, 8, "Balance");
            SqlParameter param = cmd.Parameters.Add("@origAccount", SqlDbType.Int, 4, "Account");
            param.SourceVersion = DataRowVersion.Original;
            //add to DataAdapter
            da.UpdateCommand = cmd;

            //create Delete command
            cmd = new SqlCommand();
            sql = "DELETE FROM [tClients] WHERE [Account] = @origAccount";
            cmd.Connection = conn;
            cmd.CommandText = sql;
            //create parameter
            cmd.Parameters.Add("@origAccount", SqlDbType.Int, 4, "Account").SourceVersion = DataRowVersion.Original;
            //add to DataAdapter
            da.DeleteCommand = cmd;
        }

    }
}
