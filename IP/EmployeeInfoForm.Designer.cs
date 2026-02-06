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
            this.labelIPValue = new System.Windows.Forms.Label();
            this.labelDateTime = new System.Windows.Forms.Label();
            this.loginButton = new IP.RoundedButton();
            this.labelLineResultEmpl = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxDepartment = new System.Windows.Forms.TextBox();
            this.textBoxPosition = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelIPValue
            // 
            this.labelIPValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelIPValue.AutoSize = true;
            this.labelIPValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelIPValue.Location = new System.Drawing.Point(92, 105);
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
            // 
            // labelLineResultEmpl
            // 
            this.labelLineResultEmpl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLineResultEmpl.AutoSize = true;
            this.labelLineResultEmpl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLineResultEmpl.Location = new System.Drawing.Point(255, 107);
            this.labelLineResultEmpl.Name = "labelLineResultEmpl";
            this.labelLineResultEmpl.Size = new System.Drawing.Size(39, 20);
            this.labelLineResultEmpl.TabIndex = 7;
            this.labelLineResultEmpl.Text = "132";
            this.labelLineResultEmpl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxName.Location = new System.Drawing.Point(96, 145);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(405, 24);
            this.textBoxName.TabIndex = 8;
            // 
            // textBoxDepartment
            // 
            this.textBoxDepartment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDepartment.Location = new System.Drawing.Point(96, 191);
            this.textBoxDepartment.Name = "textBoxDepartment";
            this.textBoxDepartment.Size = new System.Drawing.Size(405, 24);
            this.textBoxDepartment.TabIndex = 8;
            // 
            // textBoxPosition
            // 
            this.textBoxPosition.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPosition.Location = new System.Drawing.Point(96, 236);
            this.textBoxPosition.Name = "textBoxPosition";
            this.textBoxPosition.Size = new System.Drawing.Size(405, 24);
            this.textBoxPosition.TabIndex = 8;
            // 
            // EmployeeInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 410);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxPosition);
            this.Controls.Add(this.textBoxDepartment);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelLineResultEmpl);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.labelDateTime);
            this.Controls.Add(this.labelIPValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EmployeeInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeInfoForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label labelIPValue;
        private Label labelDateTime;
        private RoundedButton loginButton;
        private Label labelLineResultEmpl;
        private TextBox textBoxName;
        private TextBox textBoxDepartment;
        private TextBox textBoxPosition;
    }
}

