namespace AD_Coursework_01
{
    partial class AdminDashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAvailableCars = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAvailableCarParts = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTotalSalesToday = new System.Windows.Forms.Label();
            this.abc_car_tradersDataSet1 = new AD_Coursework_01.abc_car_tradersDataSet();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.abccartradersDataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblTodaySales = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.abc_car_tradersDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.abccartradersDataSet1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblAvailableCars);
            this.panel1.Location = new System.Drawing.Point(30, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 125);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(76, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Available Cars";
            // 
            // lblAvailableCars
            // 
            this.lblAvailableCars.AutoSize = true;
            this.lblAvailableCars.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableCars.ForeColor = System.Drawing.Color.White;
            this.lblAvailableCars.Location = new System.Drawing.Point(131, 31);
            this.lblAvailableCars.Name = "lblAvailableCars";
            this.lblAvailableCars.Size = new System.Drawing.Size(62, 42);
            this.lblAvailableCars.TabIndex = 0;
            this.lblAvailableCars.Text = "12";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblAvailableCarParts);
            this.panel2.Location = new System.Drawing.Point(290, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(238, 125);
            this.panel2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(63, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Available Car Parts";
            // 
            // lblAvailableCarParts
            // 
            this.lblAvailableCarParts.AutoSize = true;
            this.lblAvailableCarParts.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableCarParts.ForeColor = System.Drawing.Color.White;
            this.lblAvailableCarParts.Location = new System.Drawing.Point(149, 31);
            this.lblAvailableCarParts.Name = "lblAvailableCarParts";
            this.lblAvailableCarParts.Size = new System.Drawing.Size(62, 42);
            this.lblAvailableCarParts.TabIndex = 0;
            this.lblAvailableCarParts.Text = "12";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.lblTotalSalesToday);
            this.panel3.Location = new System.Drawing.Point(549, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(238, 125);
            this.panel3.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(78, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Total Sales Today";
            // 
            // lblTotalSalesToday
            // 
            this.lblTotalSalesToday.AutoSize = true;
            this.lblTotalSalesToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSalesToday.ForeColor = System.Drawing.Color.White;
            this.lblTotalSalesToday.Location = new System.Drawing.Point(156, 31);
            this.lblTotalSalesToday.Name = "lblTotalSalesToday";
            this.lblTotalSalesToday.Size = new System.Drawing.Size(62, 42);
            this.lblTotalSalesToday.TabIndex = 0;
            this.lblTotalSalesToday.Text = "12";
            // 
            // abc_car_tradersDataSet1
            // 
            this.abc_car_tradersDataSet1.DataSetName = "abc_car_tradersDataSet";
            this.abc_car_tradersDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.DataSource = this.abccartradersDataSet1BindingSource;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(30, 244);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(757, 300);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            // 
            // abccartradersDataSet1BindingSource
            // 
            this.abccartradersDataSet1BindingSource.DataSource = this.abc_car_tradersDataSet1;
            this.abccartradersDataSet1BindingSource.Position = 0;
            // 
            // lblTodaySales
            // 
            this.lblTodaySales.AutoSize = true;
            this.lblTodaySales.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTodaySales.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTodaySales.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTodaySales.Location = new System.Drawing.Point(366, 196);
            this.lblTodaySales.Name = "lblTodaySales";
            this.lblTodaySales.Size = new System.Drawing.Size(135, 20);
            this.lblTodaySales.TabIndex = 0;
            this.lblTodaySales.Text = "Total Sales Today";
            // 
            // AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTodaySales);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AdminDashboard";
            this.Size = new System.Drawing.Size(823, 610);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.abc_car_tradersDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.abccartradersDataSet1BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAvailableCars;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAvailableCarParts;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTotalSalesToday;
        private abc_car_tradersDataSet abc_car_tradersDataSet1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.BindingSource abccartradersDataSet1BindingSource;
        private System.Windows.Forms.Label lblTodaySales;
    }
}
