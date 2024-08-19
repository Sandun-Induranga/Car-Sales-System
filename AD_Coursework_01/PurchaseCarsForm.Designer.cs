namespace AD_Coursework_01
{
    partial class PurchaseCarsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tblCars = new System.Windows.Forms.DataGridView();
            this.abccartradersDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.abc_car_tradersDataSet = new AD_Coursework_01.abc_car_tradersDataSet();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtQtyOnHand = new System.Windows.Forms.TextBox();
            this.txtNumOfSeats = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tblCars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.abccartradersDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.abc_car_tradersDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tblCars
            // 
            this.tblCars.AutoGenerateColumns = false;
            this.tblCars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblCars.DataSource = this.abccartradersDataSetBindingSource;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tblCars.DefaultCellStyle = dataGridViewCellStyle1;
            this.tblCars.GridColor = System.Drawing.Color.White;
            this.tblCars.Location = new System.Drawing.Point(44, 233);
            this.tblCars.Name = "tblCars";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tblCars.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tblCars.Size = new System.Drawing.Size(733, 294);
            this.tblCars.TabIndex = 0;
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
            this.txtId.Location = new System.Drawing.Point(44, 47);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(197, 20);
            this.txtId.TabIndex = 1;
            // 
            // txtBrand
            // 
            this.txtBrand.Location = new System.Drawing.Point(44, 84);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(197, 20);
            this.txtBrand.TabIndex = 1;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(44, 124);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(197, 20);
            this.txtPrice.TabIndex = 1;
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(44, 168);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(197, 20);
            this.txtColor.TabIndex = 1;
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(331, 47);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(197, 20);
            this.txtModel.TabIndex = 1;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(331, 84);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(197, 20);
            this.txtYear.TabIndex = 1;
            // 
            // txtQtyOnHand
            // 
            this.txtQtyOnHand.Location = new System.Drawing.Point(331, 124);
            this.txtQtyOnHand.Name = "txtQtyOnHand";
            this.txtQtyOnHand.Size = new System.Drawing.Size(197, 20);
            this.txtQtyOnHand.TabIndex = 1;
            // 
            // txtNumOfSeats
            // 
            this.txtNumOfSeats.Location = new System.Drawing.Point(331, 168);
            this.txtNumOfSeats.Name = "txtNumOfSeats";
            this.txtNumOfSeats.Size = new System.Drawing.Size(197, 20);
            this.txtNumOfSeats.TabIndex = 1;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(614, 122);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(163, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(614, 165);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(163, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(614, 82);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(163, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // PurchaseCarsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtNumOfSeats);
            this.Controls.Add(this.txtQtyOnHand);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.tblCars);
            this.Name = "PurchaseCarsForm";
            this.Size = new System.Drawing.Size(823, 610);
            ((System.ComponentModel.ISupportInitialize)(this.tblCars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.abccartradersDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.abc_car_tradersDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView tblCars;
        private System.Windows.Forms.BindingSource abccartradersDataSetBindingSource;
        private abc_car_tradersDataSet abc_car_tradersDataSet;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtQtyOnHand;
        private System.Windows.Forms.TextBox txtNumOfSeats;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
    }
}
