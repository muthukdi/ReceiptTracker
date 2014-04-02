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
            Blank, Add, Display, Edit
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
            string connStr = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\PROG37721\\ReceiptTracker\\ReceiptTracker\\receiptDB.mdf;Integrated Security=True;User Instance=True";
            //string connStr = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\dilip\\Desktop\\ReceiptTracker\\ReceiptTracker\\receiptDB.mdf;Integrated Security=True;User Instance=True";
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
            getData();

            // Disable column sorting!
            dg1.Columns["date"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dg1.Columns["description"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dg1.Columns["amount"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dg1.Columns["category"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dg1.Columns["tags"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dg1.Columns["type"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dg1.Click += new EventHandler(dg1_Click);
            dg1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg1_CellClick);

            //initialze the state to Blank
            setState(State.Blank);
        }


        private void cmdAddReceipt_Click(object sender, EventArgs e)
        {
            setState(State.Add);
        }

        private void cmdDeleteReceipt_Click(object sender, EventArgs e)
        {
            ds.Tables["tReceipt"].Rows[rowIndex].Delete();
            da.Update(ds, "tReceipt");
            getData();
            setState(State.Blank);
        }

        private void monthSelector_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (isDataGood())
            {
                DataRow dr = null;
                if (state == State.Add)
                {
                    dr = ds.Tables["tReceipt"].NewRow();
                    dr["date"] = txtDate.Value.ToString("yyyy-MM-dd");
                    dr["description"] = txtDescription.Text;
                    dr["amount"] = Convert.ToDouble(txtAmount.Text);
                    dr["category"] = txtCategory.Text;
                    dr["tags"] = txtTags.Text;
                    dr["type"] = txtType.Text;
                    ds.Tables["tReceipt"].Rows.Add(dr);
                    da.Update(ds, "tReceipt");
                    getData();

                    rowIndex = IndexOfRowWithLargestPrimaryKey();
                }
                else if (state == State.Edit)
                {
                    dr = ds.Tables["tReceipt"].Rows[rowIndex];
                    //get the receiptID of the selected row
                    int primaryKey = (int)(dr["receiptID"]);

                    dr["date"] = txtDate.Value.ToString("yyyy-MM-dd");
                    dr["description"] = txtDescription.Text;
                    dr["amount"] = Convert.ToDouble(txtAmount.Text);
                    dr["category"] = txtCategory.Text;
                    dr["tags"] = txtTags.Text;
                    dr["type"] = txtType.Text;

                    da.Update(ds, "tReceipt");
                    getData();

                    //find the new row index of the selected row (after ordering by date)
                    rowIndex = IndexOfRowWithPrimaryKey(primaryKey);
                }
                setState(State.Display);
                //select row
                dg1.Rows[rowIndex].Selected = true;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (state == State.Edit)
            {
                CopyValuesFromSelectedRowToInputs();
                setState(State.Display);
            }
            else
            {
                setState(State.Blank);
            }

        }

        private void cmdEditReceipt_Click(object sender, EventArgs e)
        {
            setState(State.Edit);

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

        private void createCommands()
        {
            //Insert command
            SqlCommand cmd = new SqlCommand();
            string sql = "INSERT INTO [tReceipt] ([date], [description],[amount],[category],[tags],[type]) VALUES (@date, @desc, @amount, @category, @tags, @type)";
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@date", SqlDbType.DateTime, 3, "date");
            cmd.Parameters.Add("@desc", SqlDbType.VarChar, 63, "description");
            cmd.Parameters.Add("@amount", SqlDbType.Decimal, 9, "amount");
            cmd.Parameters.Add("@category", SqlDbType.VarChar, 32, "category");
            cmd.Parameters.Add("@tags", SqlDbType.VarChar, 63, "tags");
            cmd.Parameters.Add("@type", SqlDbType.VarChar, 15, "type");
            da.InsertCommand = cmd;

            //Update command
            cmd = new SqlCommand();
            sql = "UPDATE [tReceipt] SET [date] = @date, [description] = @desc, [amount] = @amount, [category] = @category, [tags] = @tags, [type] = @type WHERE [receiptID] = @receiptID";
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@receiptID", SqlDbType.Int, 4, "receiptID");
            cmd.Parameters.Add("@date", SqlDbType.DateTime, 3, "date");
            cmd.Parameters.Add("@desc", SqlDbType.VarChar, 63, "description");
            cmd.Parameters.Add("@amount", SqlDbType.Decimal, 9, "amount");
            cmd.Parameters.Add("@category", SqlDbType.VarChar, 32, "category");
            cmd.Parameters.Add("@tags", SqlDbType.VarChar, 63, "tags");
            cmd.Parameters.Add("@type", SqlDbType.VarChar, 15, "type");
            da.UpdateCommand = cmd;

            //Delete command
            cmd = new SqlCommand();
            sql = "DELETE FROM [tReceipt] WHERE [receiptID] = @receiptID";
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@receiptID", SqlDbType.Int, 4, "receiptID");
            da.DeleteCommand = cmd;
        }

        //
        //finds the newest added row by looking for the largest receiptID
        //
        private int IndexOfRowWithLargestPrimaryKey()
        {
            // At this point, the data set should be updated with the latest row
            int largestPrimaryKey = 0;
            int index = 0;
            DataRow dr = null;
            for (int i = 0; i < ds.Tables["tReceipt"].Rows.Count; i++)
            {
                dr = ds.Tables["tReceipt"].Rows[i];
                int currentPrimaryKey = (int)dr["receiptID"];
                if (currentPrimaryKey > largestPrimaryKey)
                {
                    largestPrimaryKey = currentPrimaryKey;
                    index = i;
                }
            }
            return index;
        }

        //
        //finds the row index with a desired primary key (receiptID)
        //
        private int IndexOfRowWithPrimaryKey(int primaryKey)
        {
            DataRow dr = null;
            int index = 0;
            for (int i = 0; i < ds.Tables["tReceipt"].Rows.Count; i++)
            {
                dr = ds.Tables["tReceipt"].Rows[i];
                if ((int)(dr["receiptID"]) == primaryKey)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private void setState(State toState)
        {
            switch (toState)
            {
                case State.Blank:
                    clearText();
                    //disabled
                    txtDate.Enabled = false;
                    txtAmount.Enabled = false;
                    txtDescription.Enabled = false;
                    txtCategory.Enabled = false;
                    txtType.Enabled = false;
                    txtTags.Enabled = false;
                    cmdSave.Enabled = false;
                    cmdCancel.Enabled = false;
                    cmdDeleteReceipt.Enabled = false;
                    cmdEditReceipt.Enabled = false;
                    cmdEditReceiptItems.Enabled = false;
                    monthSelector.Enabled = false;
                    cmdExportToFile.Enabled = false;
                    //enabled
                    cmdAddReceipt.Enabled = true;
                    dg1.Enabled = true;
                    //from Add to Blank
                    if (this.state == State.Add)
                    {
                        cmdAddReceipt.Text = "Add New Receipt";
                        dg1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    }
                    break;
                case State.Add:
                    //disabled
                    cmdDeleteReceipt.Enabled = false;
                    cmdEditReceipt.Enabled = false;
                    cmdEditReceiptItems.Enabled = false;
                    monthSelector.Enabled = false;
                    cmdExportToFile.Enabled = false;
                    //enabled
                    txtDate.Enabled = true;
                    txtAmount.Enabled = true;
                    txtDescription.Enabled = true;
                    txtCategory.Enabled = true;
                    txtType.Enabled = true;
                    txtTags.Enabled = true;
                    cmdAddReceipt.Enabled = true;
                    cmdSave.Enabled = true;
                    cmdCancel.Enabled = true;
                    dg1.Enabled = true;
                    //
                    dg1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    //from Display to Add
                    if (this.state == State.Display)
                    {
                        //rename Add New button to indicate a Clearing action
                        cmdAddReceipt.Text = "New Blank Receipt";
                        dg1.ClearSelection();
                    }
                    //from Add to Add
                    else if (this.state == State.Add)
                    {
                        //it means that the Clearing action was selected
                        //change back the button label and clear the inputs
                        cmdAddReceipt.Text = "Add New Receipt";
                        clearText();
                    }
                    break;
                case State.Display:
                    //disabled
                    txtDate.Enabled = false;
                    txtAmount.Enabled = false;
                    txtDescription.Enabled = false;
                    txtCategory.Enabled = false;
                    txtType.Enabled = false;
                    txtTags.Enabled = false;
                    cmdSave.Enabled = false;
                    cmdEditReceiptItems.Enabled = false;
                    monthSelector.Enabled = false;
                    cmdExportToFile.Enabled = false;
                    //enabled
                    cmdAddReceipt.Enabled = true;
                    cmdDeleteReceipt.Enabled = true;
                    cmdCancel.Enabled = true;
                    cmdEditReceipt.Enabled = true;
                    dg1.Enabled = true;
                    //from Add to Display
                    if (this.state == State.Add)
                    {
                        //change selection mode to FullRowSelect
                        dg1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        //relabel the Add New button
                        cmdAddReceipt.Text = "Add New Receipt";
                    }
                    break;
                case State.Edit:
                    //disabled
                    cmdAddReceipt.Enabled = false;
                    cmdEditReceipt.Enabled = false;
                    cmdEditReceiptItems.Enabled = false;
                    monthSelector.Enabled = false;
                    cmdExportToFile.Enabled = false;
                    dg1.Enabled = false;
                    //enabled
                    txtDate.Enabled = true;
                    txtAmount.Enabled = true;
                    txtDescription.Enabled = true;
                    txtCategory.Enabled = true;
                    txtType.Enabled = true;
                    txtTags.Enabled = true;
                    cmdSave.Enabled = true;
                    cmdCancel.Enabled = true;
                    cmdDeleteReceipt.Enabled = true;
                    break;
                default:
                    break;
            }
            state = toState;
        }

        //
        //clicking on a cell while in Add state
        //
        private void dg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //if in Add state and the cell selected was not a RowHeader
            if (this.state == State.Add && e.ColumnIndex != -1)
            {
                //get the cell and the column the cell belongs to
                DataGridViewCell cell = dg1.CurrentRow.Cells[e.ColumnIndex];
                DataGridViewColumn column = cell.OwningColumn;

                //make sure the selection mode in CellSelect
                //clear any selection and then select the cell
                dg1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dg1.ClearSelection();
                cell.Selected = true;

                //find out what column your cell belongs to,
                //then copy the value in that cell to the
                //corresponding input field
                string columnName = column.Name;
                if (columnName.Equals("date"))
                {
                    txtDate.Value = DateTime.Parse(cell.Value.ToString());
                }
                else if (columnName.Equals("description"))
                {
                    txtDescription.Text = cell.Value.ToString();
                }
                else if (columnName.Equals("amount"))
                {
                    txtAmount.Text = cell.Value.ToString();
                }
                else if (columnName.Equals("category"))
                {
                    txtCategory.Text = cell.Value.ToString();
                }
                else if (columnName.Equals("tags"))
                {
                    txtTags.Text = cell.Value.ToString();
                }
                else if (columnName.Equals("type"))
                {
                    txtType.Text = cell.Value.ToString();
                }
            }

            //if in Add state and RowHeaderCell was clicked
            else if (this.state == State.Add && e.ColumnIndex == -1 && e.RowIndex != -1)
            {
                //get the index of the row selected
                rowIndex = e.RowIndex;
                //grab the row
                DataGridViewRow dr = dg1.Rows[rowIndex];

                //switch to FullRowSelect mode and select the full row
                dg1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dr.Selected = true;

                CopyValuesFromSelectedRowToInputs();

            }
        }

        void dg1_Click(object sender, EventArgs e)
        {
            //if the current state is not Add nor Edit
            if (this.state != State.Add && this.state != State.Edit && dg1.CurrentRow != null)
            {
                //get the index of the row selected
                rowIndex = dg1.CurrentRow.Index;

                CopyValuesFromSelectedRowToInputs();
                //set to Display state
                setState(State.Display);
            }
            else if (this.state == State.Edit)
            {
                dg1.Rows[rowIndex].Selected = true;
            }
        }

        //
        //copy values from the row at rowIndex to the inputs
        //
        void CopyValuesFromSelectedRowToInputs()
        {
            //grab the row
            DataGridViewRow dr = dg1.Rows[rowIndex];
            //populate the inputs with data from selected row
            txtDate.Value = DateTime.Parse(dr.Cells["date"].Value.ToString());
            txtDescription.Text = dr.Cells["description"].Value.ToString();
            txtAmount.Text = dr.Cells["amount"].Value.ToString();
            txtCategory.Text = dr.Cells["category"].Value.ToString();
            txtTags.Text = dr.Cells["tags"].Value.ToString();
            txtType.Text = dr.Cells["type"].Value.ToString();
        }

    }
}
