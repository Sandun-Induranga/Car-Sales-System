namespace AD_Coursework_01
{
    partial class OrderForm
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
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.dgvAvailableCars = new System.Windows.Forms.DataGridView();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.btnFinalizePurchase = new System.Windows.Forms.Button();
            this.txtCarID = new System.Windows.Forms.TextBox();
            this.txtCarName = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.dgvAvailableParts = new System.Windows.Forms.DataGridView();
            this.lblItemType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableCars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableParts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCart
            // 
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Location = new System.Drawing.Point(65, 48);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.Size = new System.Drawing.Size(673, 105);
            this.dgvCart.TabIndex = 0;
            // 
            // dgvAvailableCars
            // 
            this.dgvAvailableCars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailableCars.Location = new System.Drawing.Point(65, 179);
            this.dgvAvailableCars.Name = "dgvAvailableCars";
            this.dgvAvailableCars.Size = new System.Drawing.Size(673, 86);
            this.dgvAvailableCars.TabIndex = 0;
            // 
            // nudQuantity
            // 
            this.nudQuantity.Location = new System.Drawing.Point(547, 22);
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(133, 20);
            this.nudQuantity.TabIndex = 1;
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.Location = new System.Drawing.Point(503, 440);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(75, 23);
            this.btnAddToCart.TabIndex = 2;
            this.btnAddToCart.Text = "button1";
            this.btnAddToCart.UseVisualStyleBackColor = true;
            // 
            // btnFinalizePurchase
            // 
            this.btnFinalizePurchase.Location = new System.Drawing.Point(605, 440);
            this.btnFinalizePurchase.Name = "btnFinalizePurchase";
            this.btnFinalizePurchase.Size = new System.Drawing.Size(75, 23);
            this.btnFinalizePurchase.TabIndex = 2;
            this.btnFinalizePurchase.Text = "button1";
            this.btnFinalizePurchase.UseVisualStyleBackColor = true;
            // 
            // txtCarID
            // 
            this.txtCarID.Location = new System.Drawing.Point(65, 511);
            this.txtCarID.Name = "txtCarID";
            this.txtCarID.Size = new System.Drawing.Size(215, 20);
            this.txtCarID.TabIndex = 3;
            // 
            // txtCarName
            // 
            this.txtCarName.Location = new System.Drawing.Point(310, 511);
            this.txtCarName.Name = "txtCarName";
            this.txtCarName.Size = new System.Drawing.Size(215, 20);
            this.txtCarName.TabIndex = 3;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(531, 511);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(215, 20);
            this.txtPrice.TabIndex = 3;
            // 
            // dgvAvailableParts
            // 
            this.dgvAvailableParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailableParts.Location = new System.Drawing.Point(65, 285);
            this.dgvAvailableParts.Name = "dgvAvailableParts";
            this.dgvAvailableParts.Size = new System.Drawing.Size(673, 86);
            this.dgvAvailableParts.TabIndex = 0;
            // 
            // lblItemType
            // 
            this.lblItemType.AutoSize = true;
            this.lblItemType.Location = new System.Drawing.Point(245, 450);
            this.lblItemType.Name = "lblItemType";
            this.lblItemType.Size = new System.Drawing.Size(35, 13);
            this.lblItemType.TabIndex = 4;
            this.lblItemType.Text = "label1";
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblItemType);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtCarName);
            this.Controls.Add(this.txtCarID);
            this.Controls.Add(this.btnFinalizePurchase);
            this.Controls.Add(this.btnAddToCart);
            this.Controls.Add(this.nudQuantity);
            this.Controls.Add(this.dgvAvailableParts);
            this.Controls.Add(this.dgvAvailableCars);
            this.Controls.Add(this.dgvCart);
            this.Name = "OrderForm";
            this.Size = new System.Drawing.Size(807, 571);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableCars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableParts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.DataGridView dgvAvailableCars;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Button btnAddToCart;
        private System.Windows.Forms.Button btnFinalizePurchase;
        private System.Windows.Forms.TextBox txtCarID;
        private System.Windows.Forms.TextBox txtCarName;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.DataGridView dgvAvailableParts;
        private System.Windows.Forms.Label lblItemType;
    }
}
