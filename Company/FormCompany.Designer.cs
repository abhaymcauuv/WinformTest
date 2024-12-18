﻿namespace Company
{
    partial class FormCompany
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

        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.TextBox txtAddressLine1;
        private System.Windows.Forms.TextBox txtAddressLine2;
        private System.Windows.Forms.TextBox txtZipCode;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        private void InitializeComponent()
        {
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.txtAddressLine1 = new System.Windows.Forms.TextBox();
            this.txtAddressLine2 = new System.Windows.Forms.TextBox();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.ForeColor = System.Drawing.Color.Gray;
            this.txtCompanyName.Location = new System.Drawing.Point(20, 39);
            this.txtCompanyName.Multiline = true;
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(898, 54);
            this.txtCompanyName.TabIndex = 0;
            //this.txtCompanyName.Text = "Enter Company Name";
            // 
            // txtAddressLine1
            // 
            this.txtAddressLine1.ForeColor = System.Drawing.Color.Gray;
            this.txtAddressLine1.Location = new System.Drawing.Point(20, 152);
            this.txtAddressLine1.Multiline = true;
            this.txtAddressLine1.Name = "txtAddressLine1";
            this.txtAddressLine1.Size = new System.Drawing.Size(898, 57);
            this.txtAddressLine1.TabIndex = 1;
            //this.txtAddressLine1.Text = "Enter Address Line 1";
            // 
            // txtAddressLine2
            // 
            this.txtAddressLine2.ForeColor = System.Drawing.Color.Gray;
            this.txtAddressLine2.Location = new System.Drawing.Point(20, 276);
            this.txtAddressLine2.Multiline = true;
            this.txtAddressLine2.Name = "txtAddressLine2";
            this.txtAddressLine2.Size = new System.Drawing.Size(898, 56);
            this.txtAddressLine2.TabIndex = 2;
            //this.txtAddressLine2.Text = "Enter Address Line 2";
            // 
            // txtZipCode
            // 
            this.txtZipCode.ForeColor = System.Drawing.Color.Gray;
            this.txtZipCode.Location = new System.Drawing.Point(20, 391);
            this.txtZipCode.Multiline = true;
            this.txtZipCode.Name = "txtZipCode";
            this.txtZipCode.Size = new System.Drawing.Size(428, 49);
            this.txtZipCode.TabIndex = 3;
            //this.txtZipCode.Text = "Enter Zip Code";
            // 
            // txtTelephone
            // 
            this.txtTelephone.ForeColor = System.Drawing.Color.Gray;
            this.txtTelephone.Location = new System.Drawing.Point(479, 391);
            this.txtTelephone.Multiline = true;
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(439, 49);
            this.txtTelephone.TabIndex = 4;
            //this.txtTelephone.Text = "Enter Telephone";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(20, 467);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(165, 44);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(202, 467);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(161, 44);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(17, 8);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(111, 16);
            this.lblFirstName.TabIndex = 13;
            this.lblFirstName.Text = "Company Name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Address Line 1 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Address Line 2 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 361);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Zip Code :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(476, 361);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Telephone :";
            // 
            // FormCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 523);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtCompanyName);
            this.Controls.Add(this.txtAddressLine1);
            this.Controls.Add(this.txtAddressLine2);
            this.Controls.Add(this.txtZipCode);
            this.Controls.Add(this.txtTelephone);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "FormCompany";
            this.Text = "Add/Edit Company";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public FormCompany()
        {
            InitializeComponent();
            InitializeEventHandlers(); // Call a method to initialize the event handlers
        }

        private void InitializeEventHandlers()
        {
            // Company Name
            this.txtCompanyName.GotFocus += (sender, e) =>
            {
                if (txtCompanyName.Text == "Enter Company Name")
                {
                    txtCompanyName.Text = "";
                    txtCompanyName.ForeColor = System.Drawing.Color.Gray;
                }
            };
            this.txtCompanyName.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
                {
                    txtCompanyName.Text = "Enter Company Name";
                    txtCompanyName.ForeColor = System.Drawing.Color.Gray;
                }
            };

            // Address Line 1
            this.txtAddressLine1.GotFocus += (sender, e) =>
            {
                if (txtAddressLine1.Text == "Enter Address Line 1")
                {
                    txtAddressLine1.Text = "";
                    txtAddressLine1.ForeColor = System.Drawing.Color.Gray;
                }
            };
            this.txtAddressLine1.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtAddressLine1.Text))
                {
                    txtAddressLine1.Text = "Enter Address Line 1";
                    txtAddressLine1.ForeColor = System.Drawing.Color.Gray;
                }
            };

            // Address Line 2
            this.txtAddressLine2.GotFocus += (sender, e) =>
            {
                if (txtAddressLine2.Text == "Enter Address Line 2")
                {
                    txtAddressLine2.Text = "";
                    txtAddressLine2.ForeColor = System.Drawing.Color.Gray;
                }
            };
            this.txtAddressLine2.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtAddressLine2.Text))
                {
                    txtAddressLine2.Text = "Enter Address Line 2";
                    txtAddressLine2.ForeColor = System.Drawing.Color.Gray;
                }
            };

            // Zip Code
            this.txtZipCode.GotFocus += (sender, e) =>
            {
                if (txtZipCode.Text == "Enter Zip Code")
                {
                    txtZipCode.Text = "";
                    txtZipCode.ForeColor = System.Drawing.Color.Gray;
                }
            };
            this.txtZipCode.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtZipCode.Text))
                {
                    txtZipCode.Text = "Enter Zip Code";
                    txtZipCode.ForeColor = System.Drawing.Color.Gray;
                }
            };

            // Telephone
            this.txtTelephone.GotFocus += (sender, e) =>
            {
                if (txtTelephone.Text == "Enter Telephone")
                {
                    txtTelephone.Text = "";
                    txtTelephone.ForeColor = System.Drawing.Color.Gray;
                }
            };
            this.txtTelephone.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTelephone.Text))
                {
                    txtTelephone.Text = "Enter Telephone";
                    txtTelephone.ForeColor = System.Drawing.Color.Gray;
                }
            };
        }

        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
