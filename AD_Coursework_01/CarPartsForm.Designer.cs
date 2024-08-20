namespace AD_Coursework_01
{
    partial class CarPartsForm
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
            this.tblCarParts = new System.Windows.Forms.DataGridView();
            this.abccartradersDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.abc_car_tradersDataSet = new AD_Coursework_01.abc_car_tradersDataSet();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.txtQtyOnHand = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tblCarParts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.abccartradersDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.abc_car_tradersDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tblCarParts
            // 
            this.tblCarParts.AutoGenerateColumns = false;
            this.tblCarParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblCarParts.DataSource = this.abccartradersDataSetBindingSource;
            this.tblCarParts.Location = new System.Drawing.Point(50, 266);
            this.tblCarParts.Name = "tblCarParts";
            this.tblCarParts.Size = new System.Drawing.Size(724, 259);
            this.tblCarParts.TabIndex = 0;
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
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(50, 62);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(199, 20);
            this.txtId.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(306, 62);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(199, 20);
            this.txtName.TabIndex = 1;
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Location = new System.Drawing.Point(571, 62);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.Size = new System.Drawing.Size(199, 20);
            this.txtUnitPrice.TabIndex = 1;
            // 
            // txtQtyOnHand
            // 
            this.txtQtyOnHand.Location = new System.Drawing.Point(50, 110);
            this.txtQtyOnHand.Name = "txtQtyOnHand";
            this.txtQtyOnHand.Size = new System.Drawing.Size(199, 20);
            this.txtQtyOnHand.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(571, 110);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(199, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(571, 152);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(199, 23);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(571, 197);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(199, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // CarPartsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtQtyOnHand);
            this.Controls.Add(this.txtUnitPrice);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.tblCarParts);
            this.Name = "CarPartsForm";
            this.Size = new System.Drawing.Size(823, 610);
            ((System.ComponentModel.ISupportInitialize)(this.tblCarParts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.abccartradersDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.abc_car_tradersDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView tblCarParts;
        private System.Windows.Forms.BindingSource abccartradersDataSetBindingSource;
        private abc_car_tradersDataSet abc_car_tradersDataSet;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.TextBox txtQtyOnHand;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
    }
}
