namespace AD_Coursework_01
{
    partial class OrderDetailsForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.abccartradersDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.abc_car_tradersDataSet = new AD_Coursework_01.abc_car_tradersDataSet();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.panelCustomer = new System.Windows.Forms.Panel();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblMobile = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblCusName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.abccartradersDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.abc_car_tradersDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.panelCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrders
            // 
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(112, 183);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.Size = new System.Drawing.Size(695, 141);
            this.dgvOrders.TabIndex = 0;
            // 
            // abccartradersDataSetBindingSource
            // 
            this.abccartradersDataSetBindingSource.DataSource = this.abc_car_tradersDataSet;
            this.abccartradersDataSetBindingSource.Position = 0;
            // 
            // abc_car_tradersDataSet
            // 
            this.abc_car_tradersDataSet.DataSetName = "abc_car_tradersDataSet";
            this.abc_car_tradersDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dgvOrderDetails
            // 
            this.dgvOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderDetails.Location = new System.Drawing.Point(112, 362);
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.Size = new System.Drawing.Size(695, 141);
            this.dgvOrderDetails.TabIndex = 0;
            // 
            // panelCustomer
            // 
            this.panelCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.panelCustomer.Controls.Add(this.pictureBox1);
            this.panelCustomer.Controls.Add(this.lblEmail);
            this.panelCustomer.Controls.Add(this.lblMobile);
            this.panelCustomer.Controls.Add(this.lblAddress);
            this.panelCustomer.Controls.Add(this.lblCusName);
            this.panelCustomer.Location = new System.Drawing.Point(427, 45);
            this.panelCustomer.Name = "panelCustomer";
            this.panelCustomer.Size = new System.Drawing.Size(380, 129);
            this.panelCustomer.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(124, 98);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(51, 16);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email: -";
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMobile.Location = new System.Drawing.Point(124, 72);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(58, 16);
            this.lblMobile.TabIndex = 0;
            this.lblMobile.Text = "Mobile: -";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(124, 50);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(68, 16);
            this.lblAddress.TabIndex = 0;
            this.lblAddress.Text = "Address: -";
            // 
            // lblCusName
            // 
            this.lblCusName.AutoSize = true;
            this.lblCusName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusName.Location = new System.Drawing.Point(124, 28);
            this.lblCusName.Name = "lblCusName";
            this.lblCusName.Size = new System.Drawing.Size(114, 16);
            this.lblCusName.TabIndex = 0;
            this.lblCusName.Text = "Customer Name: -";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AD_Coursework_01.Properties.Resources.icons8_customer_100;
            this.pictureBox1.Location = new System.Drawing.Point(13, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // OrderDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelCustomer);
            this.Controls.Add(this.dgvOrderDetails);
            this.Controls.Add(this.dgvOrders);
            this.Name = "OrderDetailsForm";
            this.Size = new System.Drawing.Size(929, 610);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.abccartradersDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.abc_car_tradersDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.panelCustomer.ResumeLayout(false);
            this.panelCustomer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.BindingSource abccartradersDataSetBindingSource;
        private abc_car_tradersDataSet abc_car_tradersDataSet;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
        private System.Windows.Forms.Panel panelCustomer;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblCusName;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
