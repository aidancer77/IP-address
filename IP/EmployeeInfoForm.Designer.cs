using System;
using System.Drawing;
using System.Windows.Forms;

namespace IP
{
    partial class EmployeeInfoForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxName = new System.Windows.Forms.ListBox();
            this.textBoxDepartment = new System.Windows.Forms.ListBox();
            this.textBoxPosition = new System.Windows.Forms.ListBox();
            this.labelIPValue = new System.Windows.Forms.Label();
            this.labelDateTime = new System.Windows.Forms.Label();
            this.comboBoxLine = new System.Windows.Forms.ComboBox();
            this.loginButton = new IP.RoundedButton();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxName.FormattingEnabled = true;
            this.textBoxName.ItemHeight = 18;
            this.textBoxName.Location = new System.Drawing.Point(96, 147);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(405, 22);
            this.textBoxName.TabIndex = 3;
            // 
            // textBoxDepartment
            // 
            this.textBoxDepartment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDepartment.FormattingEnabled = true;
            this.textBoxDepartment.ItemHeight = 18;
            this.textBoxDepartment.Location = new System.Drawing.Point(96, 193);
            this.textBoxDepartment.Name = "textBoxDepartment";
            this.textBoxDepartment.Size = new System.Drawing.Size(405, 22);
            this.textBoxDepartment.TabIndex = 3;
            // 
            // textBoxPosition
            // 
            this.textBoxPosition.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPosition.FormattingEnabled = true;
            this.textBoxPosition.ItemHeight = 18;
            this.textBoxPosition.Location = new System.Drawing.Point(96, 238);
            this.textBoxPosition.Name = "textBoxPosition";
            this.textBoxPosition.Size = new System.Drawing.Size(405, 22);
            this.textBoxPosition.TabIndex = 3;
            // 
            // labelIPValue
            // 
            this.labelIPValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelIPValue.AutoSize = true;
            this.labelIPValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelIPValue.Location = new System.Drawing.Point(94, 105);
            this.labelIPValue.Name = "labelIPValue";
            this.labelIPValue.Size = new System.Drawing.Size(83, 20);
            this.labelIPValue.TabIndex = 4;
            this.labelIPValue.Text = "IPAddress";
            this.labelIPValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDateTime
            // 
            this.labelDateTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelDateTime.AutoSize = true;
            this.labelDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDateTime.Location = new System.Drawing.Point(373, 105);
            this.labelDateTime.Name = "labelDateTime";
            this.labelDateTime.Size = new System.Drawing.Size(78, 20);
            this.labelDateTime.TabIndex = 4;
            this.labelDateTime.Text = "Date&Time";
            this.labelDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLine
            // 
            this.comboBoxLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxLine.FormattingEnabled = true;
            this.comboBoxLine.Location = new System.Drawing.Point(214, 105);
            this.comboBoxLine.Name = "comboBoxLine";
            this.comboBoxLine.Size = new System.Drawing.Size(134, 24);
            this.comboBoxLine.TabIndex = 6;
            // 
            // loginButton
            // 
            this.loginButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loginButton.BackColor = System.Drawing.Color.CadetBlue;
            this.loginButton.CornerRadius = 15;
            this.loginButton.FlatAppearance.BorderColor = System.Drawing.Color.CadetBlue;
            this.loginButton.FlatAppearance.BorderSize = 0;
            this.loginButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.loginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginButton.Location = new System.Drawing.Point(201, 289);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(160, 35);
            this.loginButton.TabIndex = 5;
            this.loginButton.Text = "Авторизоваться";
            this.loginButton.UseVisualStyleBackColor = false;
            this.loginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // EmployeeInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 410);
            this.ControlBox = false;
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.comboBoxLine);
            this.Controls.Add(this.labelDateTime);
            this.Controls.Add(this.labelIPValue);
            this.Controls.Add(this.textBoxPosition);
            this.Controls.Add(this.textBoxDepartment);
            this.Controls.Add(this.textBoxName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EmployeeInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeInfoForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ListBox textBoxName;
        private ListBox textBoxDepartment;
        private ListBox textBoxPosition;
        private Label labelIPValue;
        private Label labelDateTime;
        private ComboBox comboBoxLine;
        private RoundedButton loginButton;
    }
}

