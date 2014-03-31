namespace ReceiptTracker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmdAddReceipt = new System.Windows.Forms.Button();
            this.cmdDeleteReceipt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtTags = new System.Windows.Forms.TextBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEditReceipt = new System.Windows.Forms.Button();
            this.cmdEditReceiptItems = new System.Windows.Forms.Button();
            this.cmdExportToFile = new System.Windows.Forms.Button();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.monthSelector = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.DateTimePicker();
            this.txtCategory = new System.Windows.Forms.ComboBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdAddReceipt
            // 
            this.cmdAddReceipt.Location = new System.Drawing.Point(13, 13);
            this.cmdAddReceipt.Name = "cmdAddReceipt";
            this.cmdAddReceipt.Size = new System.Drawing.Size(124, 23);
            this.cmdAddReceipt.TabIndex = 0;
            this.cmdAddReceipt.Text = "Add New Receipt";
            this.cmdAddReceipt.UseVisualStyleBackColor = true;
            this.cmdAddReceipt.Click += new System.EventHandler(this.cmdAddReceipt_Click);
            // 
            // cmdDeleteReceipt
            // 
            this.cmdDeleteReceipt.Location = new System.Drawing.Point(143, 13);
            this.cmdDeleteReceipt.Name = "cmdDeleteReceipt";
            this.cmdDeleteReceipt.Size = new System.Drawing.Size(118, 23);
            this.cmdDeleteReceipt.TabIndex = 1;
            this.cmdDeleteReceipt.Text = "Delete Receipt";
            this.cmdDeleteReceipt.UseVisualStyleBackColor = true;
            this.cmdDeleteReceipt.Click += new System.EventHandler(this.cmdDeleteReceipt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Date: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Amount: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Category: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Tags: ";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(75, 98);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(186, 20);
            this.txtDescription.TabIndex = 8;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(75, 123);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(186, 20);
            this.txtAmount.TabIndex = 9;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            // 
            // txtTags
            // 
            this.txtTags.Location = new System.Drawing.Point(75, 174);
            this.txtTags.Name = "txtTags";
            this.txtTags.Size = new System.Drawing.Size(186, 20);
            this.txtTags.TabIndex = 11;
            this.txtTags.TextChanged += new System.EventHandler(this.txtTags_TextChanged);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(13, 302);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 12;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(96, 302);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 13;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdEditReceipt
            // 
            this.cmdEditReceipt.Location = new System.Drawing.Point(177, 302);
            this.cmdEditReceipt.Name = "cmdEditReceipt";
            this.cmdEditReceipt.Size = new System.Drawing.Size(84, 23);
            this.cmdEditReceipt.TabIndex = 14;
            this.cmdEditReceipt.Text = "Edit Receipt";
            this.cmdEditReceipt.UseVisualStyleBackColor = true;
            this.cmdEditReceipt.Click += new System.EventHandler(this.cmdEditReceipt_Click);
            // 
            // cmdEditReceiptItems
            // 
            this.cmdEditReceiptItems.Location = new System.Drawing.Point(13, 341);
            this.cmdEditReceiptItems.Name = "cmdEditReceiptItems";
            this.cmdEditReceiptItems.Size = new System.Drawing.Size(248, 23);
            this.cmdEditReceiptItems.TabIndex = 15;
            this.cmdEditReceiptItems.Text = "Edit Receipt\'s Items";
            this.cmdEditReceiptItems.UseVisualStyleBackColor = true;
            this.cmdEditReceiptItems.Click += new System.EventHandler(this.cmdEditReceiptItems_Click);
            // 
            // cmdExportToFile
            // 
            this.cmdExportToFile.Location = new System.Drawing.Point(13, 409);
            this.cmdExportToFile.Name = "cmdExportToFile";
            this.cmdExportToFile.Size = new System.Drawing.Size(248, 23);
            this.cmdExportToFile.TabIndex = 16;
            this.cmdExportToFile.Text = "Export To File";
            this.cmdExportToFile.UseVisualStyleBackColor = true;
            // 
            // dg1
            // 
            this.dg1.AllowUserToAddRows = false;
            this.dg1.AllowUserToDeleteRows = false;
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Location = new System.Drawing.Point(277, 40);
            this.dg1.MultiSelect = false;
            this.dg1.Name = "dg1";
            this.dg1.ReadOnly = true;
            this.dg1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg1.Size = new System.Drawing.Size(744, 393);
            this.dg1.TabIndex = 17;
            // 
            // monthSelector
            // 
            this.monthSelector.FormattingEnabled = true;
            this.monthSelector.Location = new System.Drawing.Point(563, 13);
            this.monthSelector.Name = "monthSelector";
            this.monthSelector.Size = new System.Drawing.Size(154, 21);
            this.monthSelector.TabIndex = 18;
            this.monthSelector.SelectedIndexChanged += new System.EventHandler(this.monthSelector_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(273, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "Receipts";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(484, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Select Month:";
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(75, 73);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(186, 20);
            this.txtDate.TabIndex = 21;
            this.txtDate.ValueChanged += new System.EventHandler(this.txtDate_ValueChanged);
            // 
            // txtCategory
            // 
            this.txtCategory.FormattingEnabled = true;
            this.txtCategory.Location = new System.Drawing.Point(75, 148);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(186, 21);
            this.txtCategory.TabIndex = 22;
            this.txtCategory.SelectedIndexChanged += new System.EventHandler(this.txtCategory_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 207);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Type: ";
            // 
            // txtType
            // 
            this.txtType.FormattingEnabled = true;
            this.txtType.Items.AddRange(new object[] {
            "Cash",
            "Debit",
            "Credit",
            "Deposit",
            "Withdrawal",
            "Refund"});
            this.txtType.Location = new System.Drawing.Point(75, 204);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(186, 21);
            this.txtType.TabIndex = 24;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 445);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.monthSelector);
            this.Controls.Add(this.dg1);
            this.Controls.Add(this.cmdExportToFile);
            this.Controls.Add(this.cmdEditReceiptItems);
            this.Controls.Add(this.cmdEditReceipt);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.txtTags);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdDeleteReceipt);
            this.Controls.Add(this.cmdAddReceipt);
            this.Name = "Form1";
            this.Text = "Receipt Tracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdAddReceipt;
        private System.Windows.Forms.Button cmdDeleteReceipt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtTags;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdEditReceipt;
        private System.Windows.Forms.Button cmdEditReceiptItems;
        private System.Windows.Forms.Button cmdExportToFile;
        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.ComboBox monthSelector;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker txtDate;
        private System.Windows.Forms.ComboBox txtCategory;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox txtType;
    }
}

