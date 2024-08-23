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
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.abccartradersDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.abc_car_tradersDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrders
            // 
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(59, 183);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.Size = new System.Drawing.Size(693, 141);
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
            this.dgvOrderDetails.Location = new System.Drawing.Point(59, 362);
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.Size = new System.Drawing.Size(693, 141);
            this.dgvOrderDetails.TabIndex = 0;
            // 
            // OrderDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvOrderDetails);
            this.Controls.Add(this.dgvOrders);
            this.Name = "OrderDetailsForm";
            this.Size = new System.Drawing.Size(807, 571);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.abccartradersDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.abc_car_tradersDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.BindingSource abccartradersDataSetBindingSource;
        private abc_car_tradersDataSet abc_car_tradersDataSet;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
    }
}
