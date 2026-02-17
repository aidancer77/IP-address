using System.Windows.Forms;

namespace IP
{
    partial class AdminCorrectLineForm
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
            this.labelIPValue = new System.Windows.Forms.Label();
            this.labelDateTime = new System.Windows.Forms.Label();
            this.buttonChooseLine = new IP.RoundedButton();
            this.labelChooseLine = new System.Windows.Forms.Label();
            this.comboBoxLine = new System.Windows.Forms.ComboBox();
            this.lineInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.lineInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labelIPValue
            // 
            this.labelIPValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelIPValue.AutoSize = true;
            this.labelIPValue.BackColor = System.Drawing.Color.Transparent;
            this.labelIPValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelIPValue.ForeColor = System.Drawing.Color.White;
            this.labelIPValue.Location = new System.Drawing.Point(95, 105);
            this.labelIPValue.Name = "labelIPValue";
            this.labelIPValue.Size = new System.Drawing.Size(0, 20);
            this.labelIPValue.TabIndex = 4;
            this.labelIPValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDateTime
            // 
            this.labelDateTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelDateTime.AutoSize = true;
            this.labelDateTime.BackColor = System.Drawing.Color.Transparent;
            this.labelDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDateTime.ForeColor = System.Drawing.Color.White;
            this.labelDateTime.Location = new System.Drawing.Point(374, 105);
            this.labelDateTime.Name = "labelDateTime";
            this.labelDateTime.Size = new System.Drawing.Size(0, 20);
            this.labelDateTime.TabIndex = 4;
            this.labelDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonChooseLine
            // 
            this.buttonChooseLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonChooseLine.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonChooseLine.CornerRadius = 30;
            this.buttonChooseLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChooseLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonChooseLine.ForeColor = System.Drawing.Color.White;
            this.buttonChooseLine.Location = new System.Drawing.Point(220, 260);
            this.buttonChooseLine.Name = "buttonChooseLine";
            this.buttonChooseLine.Size = new System.Drawing.Size(170, 40);
            this.buttonChooseLine.TabIndex = 5;
            this.buttonChooseLine.Text = "Выбрать";
            this.buttonChooseLine.UseVisualStyleBackColor = false;
            this.buttonChooseLine.Click += new System.EventHandler(this.ButtonChooseLine_Click);
            // 
            // labelChooseLine
            // 
            this.labelChooseLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelChooseLine.AutoSize = true;
            this.labelChooseLine.BackColor = System.Drawing.Color.Transparent;
            this.labelChooseLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelChooseLine.Location = new System.Drawing.Point(245, 165);
            this.labelChooseLine.Name = "labelChooseLine";
            this.labelChooseLine.Size = new System.Drawing.Size(125, 18);
            this.labelChooseLine.TabIndex = 4;
            this.labelChooseLine.Text = "Выберите линию";
            this.labelChooseLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxLine
            // 
            this.comboBoxLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxLine.FormattingEnabled = true;
            this.comboBoxLine.Location = new System.Drawing.Point(220, 200);
            this.comboBoxLine.Name = "comboBoxLine";
            this.comboBoxLine.Size = new System.Drawing.Size(170, 26);
            this.comboBoxLine.TabIndex = 9;
            // 
            // lineInfoBindingSource
            // 
            this.lineInfoBindingSource.DataSource = typeof(IP.LineInfo);
            // 
            // AdminCorrectLineForm
            // 
            this.AcceptButton = this.buttonChooseLine;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(569, 410);
            this.ControlBox = false;
            this.Controls.Add(this.comboBoxLine);
            this.Controls.Add(this.labelDateTime);
            this.Controls.Add(this.labelIPValue);
            this.Controls.Add(this.buttonChooseLine);
            this.Controls.Add(this.labelChooseLine);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "AdminCorrectLineForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeInfoForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.lineInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RoundedButton buttonChooseLine;
        private Label labelIPValue;
        private Label labelDateTime;
        private Label labelChooseLine;
        private ComboBox comboBoxLine;
        private DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn checklineDataGridViewTextBoxColumn;
        private BindingSource lineInfoBindingSource;
    }
}