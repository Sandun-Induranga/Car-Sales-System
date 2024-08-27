namespace AD_Coursework_01
{
    partial class GenerateReportForm
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
            this.btnGenerateIncomeReport = new System.Windows.Forms.Button();
            this.btnGenerateMonthlyReport = new System.Windows.Forms.Button();
            this.btnGenerateAnnualReport = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btnGenerateIncomeReport
            // 
            this.btnGenerateIncomeReport.Location = new System.Drawing.Point(350, 162);
            this.btnGenerateIncomeReport.Name = "btnGenerateIncomeReport";
            this.btnGenerateIncomeReport.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateIncomeReport.TabIndex = 0;
            this.btnGenerateIncomeReport.Text = "button1";
            this.btnGenerateIncomeReport.UseVisualStyleBackColor = true;
            // 
            // btnGenerateMonthlyReport
            // 
            this.btnGenerateMonthlyReport.Location = new System.Drawing.Point(350, 233);
            this.btnGenerateMonthlyReport.Name = "btnGenerateMonthlyReport";
            this.btnGenerateMonthlyReport.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateMonthlyReport.TabIndex = 0;
            this.btnGenerateMonthlyReport.Text = "button1";
            this.btnGenerateMonthlyReport.UseVisualStyleBackColor = true;
            // 
            // btnGenerateAnnualReport
            // 
            this.btnGenerateAnnualReport.Location = new System.Drawing.Point(350, 313);
            this.btnGenerateAnnualReport.Name = "btnGenerateAnnualReport";
            this.btnGenerateAnnualReport.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateAnnualReport.TabIndex = 0;
            this.btnGenerateAnnualReport.Text = "button1";
            this.btnGenerateAnnualReport.UseVisualStyleBackColor = true;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(180, 83);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 1;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(502, 83);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 2;
            // 
            // GenerateReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.btnGenerateAnnualReport);
            this.Controls.Add(this.btnGenerateMonthlyReport);
            this.Controls.Add(this.btnGenerateIncomeReport);
            this.Name = "GenerateReportForm";
            this.Size = new System.Drawing.Size(823, 610);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateIncomeReport;
        private System.Windows.Forms.Button btnGenerateMonthlyReport;
        private System.Windows.Forms.Button btnGenerateAnnualReport;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
    }
}
