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
        enum State
        {
            BLANK, ADD, DISPLAY, EDIT
        }

        SqlConnection conn = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        State state;
        int rowIndex = -1;

        public Form1()
        {
            InitializeComponent();
        }

        private void getData()
        {
            //string connStr = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\PROG37721\\ReceiptTracker\\ReceiptTracker\\receiptDB.mdf;Integrated Security=True;User Instance=True";
            string connStr = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\dilip\\Desktop\\ReceiptTracker\\ReceiptTracker\\receiptDB.mdf;Integrated Security=True;User Instance=True";
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                string sql = "SELECT * FROM [tReceipt] ORDER BY [date]";

                da = new SqlDataAdapter(sql, conn);
                createCommands();
                ds = new DataSet();
                da.Fill(ds, "tReceipt");
                conn.Close();
                //bind and display
                bindingSource1.DataSource = ds;
                bindingSource1.DataMember = "tReceipt";
                dg1.DataSource = bindingSource1;
                dg1.ClearSelection();
                //hide the receiptID column, unneccessary for viewing
                dg1.Columns["receiptID"].Visible = false;
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
            dg1.Click += new EventHandler(dg1_Click);
            getData();
            // Disable column sorting!
            dg1.Columns["date"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dg1.Columns["description"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dg1.Columns["amount"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dg1.Columns["category"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dg1.Columns["tags"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dg1.Columns["type"].SortMode = DataGridViewColumnSortMode.NotSortable;
            setControlState(State.BLANK);
        }

        void dg1_Click(object sender, EventArgs e)
        {
            rowIndex = dg1.CurrentRow.Index;
            /*// align rowIndex with index of selection in DataSet (very important!)
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i].RowState != DataRowState.Deleted)
                {
                    if (dg1.CurrentRow.Cells[0].Value.ToString().Equals(ds.Tables[0].Rows[i][0].ToString()))
                    {
                        rowIndex = i;
                        break;
                    }
                }
            }*/
            txtDate.Value = DateTime.Parse(dg1.CurrentRow.Cells[1].Value.ToString());
            txtDescription.Text = dg1.CurrentRow.Cells[2].Value.ToString();
            txtAmount.Text = dg1.CurrentRow.Cells[3].Value.ToString();
            txtCategory.Text = dg1.CurrentRow.Cells[4].Value.ToString();
            txtTags.Text = dg1.CurrentRow.Cells[5].Value.ToString();
            txtType.Text = dg1.CurrentRow.Cells[6].Value.ToString();
            setControlState(State.DISPLAY);
        }

        private void cmdAddReceipt_Click(object sender, EventArgs e)
        {
            setControlState(State.ADD);
        }

        private void cmdDeleteReceipt_Click(object sender, EventArgs e)
        {
            ds.Tables[0].Rows[rowIndex].Delete();
            da.Update(ds, "tReceipt");
            getData();
            setControlState(State.BLANK);
        }

        private void monthSelector_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (isDataGood())
            {
                DataRow dr = null;
                int primaryKey = 0;
                if (state == State.ADD)
                {
                    dr = ds.Tables["tReceipt"].NewRow();
                }
                else if (state == State.EDIT)
                {
                    dr = ds.Tables[0].Rows[rowIndex];
                    primaryKey = (int)(dr["receiptID"]);
                }
                dr["Date"] = txtDate.Value.ToString("yyyy-MM-dd");
                dr["Description"] = txtDescription.Text;
                dr["Amount"] = Convert.ToDouble(txtAmount.Text);
                dr["Category"] = txtCategory.Text;
                dr["Tags"] = txtTags.Text;
                dr["Type"] = txtType.Text;
                if (state == State.ADD)
                {
                    ds.Tables["tReceipt"].Rows.Add(dr);
                }
                da.Update(ds, "tReceipt");
                getData();
                if (state == State.ADD)
                {
                    dg1.Rows[IndexOfRowWithLargestPrimaryKey()].Selected = true;
                }
                else if (state == State.EDIT)
                {
                    dg1.Rows[rowIndex = IndexOfRowWithPrimaryKey(primaryKey)].Selected = true;

                }
                setControlState(State.DISPLAY);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (state == State.EDIT)
            {
                setControlState(State.DISPLAY);
            }
            else
            {
                setControlState(State.BLANK);
            }
            
        }

        private void cmdEditReceipt_Click(object sender, EventArgs e)
        {
            setControlState(State.EDIT);
        }

        private void cmdEditReceiptItems_Click(object sender, EventArgs e)
        {

        }

        private void clearText()
        {
            txtDate.Value = DateTime.Now;
            txtDescription.Text = "";
            txtAmount.Text = "";
            txtCategory.Text = "";
            txtType.Text = "Cash";
            txtTags.Text = "";
            dg1.ClearSelection();
            txtDescription.Focus();
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
            string sql = "INSERT INTO [tReceipt] ([date], [description],[amount],[category],[tags],[type]) VALUES (@date, @desc, @amount, @category, @tags, @type)";
            cmd.Connection = conn;
            cmd.CommandText = sql;
            //create parameters
            cmd.Parameters.Add("@date", SqlDbType.DateTime, 3, "Date");
            cmd.Parameters.Add("@desc", SqlDbType.VarChar, 63, "Description");
            cmd.Parameters.Add("@amount", SqlDbType.Decimal, 9, "Amount");
            cmd.Parameters.Add("@category", SqlDbType.VarChar, 32, "Category");
            cmd.Parameters.Add("@tags", SqlDbType.VarChar, 63, "Tags");
            cmd.Parameters.Add("@type", SqlDbType.VarChar, 15, "Type");
            // add to DataAdapter
            da.InsertCommand = cmd;

            //create  UPDATE command for DataAdapter
            cmd = new SqlCommand();
            sql = "UPDATE [tReceipt] SET [date] = @date, [description] = @desc, [amount] = @amount, [category] = @category, [tags] = @tags, [type] = @type WHERE [receiptID] = @receiptID";
            cmd.Connection = conn;
            cmd.CommandText = sql;
            //create parameters
            cmd.Parameters.Add("@receiptID", SqlDbType.Int, 4, "ReceiptID");
            cmd.Parameters.Add("@date", SqlDbType.DateTime, 3, "Date");
            cmd.Parameters.Add("@desc", SqlDbType.VarChar, 63, "Description");
            cmd.Parameters.Add("@amount", SqlDbType.Decimal, 9, "Amount");
            cmd.Parameters.Add("@category", SqlDbType.VarChar, 32, "Category");
            cmd.Parameters.Add("@tags", SqlDbType.VarChar, 63, "Tags");
            cmd.Parameters.Add("@type", SqlDbType.VarChar, 15, "Type");
            // add to DataAdapter
            da.UpdateCommand = cmd;

            //create Delete command
            cmd = new SqlCommand();
            sql = "DELETE FROM [tReceipt] WHERE [receiptID] = @receiptID";
            cmd.Connection = conn;
            cmd.CommandText = sql;
            //create parameter
            cmd.Parameters.Add("@receiptID", SqlDbType.Int, 4, "ReceiptID");//.SourceVersion = DataRowVersion.Original;
            //add to DataAdapter
            da.DeleteCommand = cmd;
        }

        private int IndexOfRowWithLargestPrimaryKey()
        {
            // At this point, the data set should be updated with the latest row
            int largestPrimaryKey = 0;
            int index = 0;
            int currentPrimaryKey = 0;
            DataRow dr = null;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                currentPrimaryKey = (int)dr["receiptID"];
                if (currentPrimaryKey > largestPrimaryKey)
                {
                    largestPrimaryKey = currentPrimaryKey;
                    index = i;
                }
            }
            return index;
        }

        private int IndexOfRowWithPrimaryKey(int primaryKey)
        {
            DataRow dr = null;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                if ((int)(dr["receiptID"]) == primaryKey)
                {
                    return i;
                }
            }
            return 0;
        }

        private void setControlState(State st)
        {
            switch (st)
            {
                case State.BLANK:
                    clearText();
                    txtDate.Enabled = false;
                    txtAmount.Enabled = false;
                    txtDescription.Enabled = false;
                    txtCategory.Enabled = false;
                    txtType.Enabled = false;
                    txtTags.Enabled = false;
                    cmdAddReceipt.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdCancel.Enabled = false;
                    cmdDeleteReceipt.Enabled = false;
                    cmdEditReceipt.Enabled = false;
                    cmdEditReceiptItems.Enabled = false;
                    monthSelector.Enabled = false;
                    cmdExportToFile.Enabled = false;
                    break;
                case State.ADD:
                    txtDate.Enabled = true;
                    txtAmount.Enabled = true;
                    txtDescription.Enabled = true;
                    txtCategory.Enabled = true;
                    txtType.Enabled = true;
                    txtTags.Enabled = true;
                    cmdAddReceipt.Enabled = true;
                    cmdSave.Enabled = true;
                    cmdCancel.Enabled = true;
                    cmdDeleteReceipt.Enabled = false;
                    cmdEditReceipt.Enabled = false;
                    cmdEditReceiptItems.Enabled = false;
                    monthSelector.Enabled = false;
                    cmdExportToFile.Enabled = false;
                    //dg1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    if (state == State.DISPLAY)
                    {
                        dg1.ClearSelection();
                    }
                    if (state == State.ADD)
                    {
                        clearText();
                    }
                    break;
                case State.DISPLAY:
                    txtDate.Enabled = false;
                    txtAmount.Enabled = false;
                    txtDescription.Enabled = false;
                    txtCategory.Enabled = false;
                    txtType.Enabled = false;
                    txtTags.Enabled = false;
                    cmdAddReceipt.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdCancel.Enabled = false;
                    cmdDeleteReceipt.Enabled = true;
                    cmdEditReceipt.Enabled = true;
                    cmdEditReceiptItems.Enabled = false;
                    monthSelector.Enabled = false;
                    cmdExportToFile.Enabled = false;
                    break;
                case State.EDIT:
                    txtDate.Enabled = true;
                    txtAmount.Enabled = true;
                    txtDescription.Enabled = true;
                    txtCategory.Enabled = true;
                    txtType.Enabled = true;
                    txtTags.Enabled = true;
                    cmdAddReceipt.Enabled = false;
                    cmdSave.Enabled = true;
                    cmdCancel.Enabled = true;
                    cmdDeleteReceipt.Enabled = true;
                    cmdEditReceipt.Enabled = false;
                    cmdEditReceiptItems.Enabled = false;
                    monthSelector.Enabled = false;
                    cmdExportToFile.Enabled = false;
                    break;
                default:
                    break;
            }
            state = st;
        }

        private void dg1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*DataGridViewCell cell = dg1.CurrentRow.Cells[e.ColumnIndex];
            DataGridViewColumn column = cell.OwningColumn;
            Console.WriteLine(cell.Value.ToString());*/
        }

    }
}
