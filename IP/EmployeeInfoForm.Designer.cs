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
            this.labelAttachPass = new System.Windows.Forms.Label();
            this.labelDateTime = new System.Windows.Forms.Label();
            this.comboBoxLine = new System.Windows.Forms.ComboBox();
            this.labelLine = new System.Windows.Forms.Label();
            this.buttonLabelPassword = new System.Windows.Forms.Button();
            this.roundedButton1 = new IP.RoundedButton();
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
            this.textBoxName.Visible = false;
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
            this.textBoxDepartment.Visible = false;
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
            this.textBoxPosition.Visible = false;
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
            // labelAttachPass
            // 
            this.labelAttachPass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelAttachPass.AutoSize = true;
            this.labelAttachPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAttachPass.Location = new System.Drawing.Point(143, 150);
            this.labelAttachPass.Name = "labelAttachPass";
            this.labelAttachPass.Size = new System.Drawing.Size(269, 18);
            this.labelAttachPass.TabIndex = 4;
            this.labelAttachPass.Text = "Для авторизации приложите пропуск";
            this.labelAttachPass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.comboBoxLine.Visible = false;
            // 
            // labelLine
            // 
            this.labelLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLine.AutoSize = true;
            this.labelLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLine.Location = new System.Drawing.Point(259, 109);
            this.labelLine.Name = "labelLine";
            this.labelLine.Size = new System.Drawing.Size(47, 16);
            this.labelLine.TabIndex = 7;
            this.labelLine.Text = "Линия";
            this.labelLine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelLine.Visible = false;
            // 
            // buttonLabelPassword
            // 
            this.buttonLabelPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonLabelPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLabelPassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonLabelPassword.Location = new System.Drawing.Point(201, 228);
            this.buttonLabelPassword.Name = "buttonLabelPassword";
            this.buttonLabelPassword.Size = new System.Drawing.Size(160, 34);
            this.buttonLabelPassword.TabIndex = 8;
            this.buttonLabelPassword.Text = "Вход по паролю";
            // 
            // roundedButton1
            // 
            this.roundedButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.roundedButton1.BackColor = System.Drawing.Color.CadetBlue;
            this.roundedButton1.CornerRadius = 15;
            this.roundedButton1.FlatAppearance.BorderColor = System.Drawing.Color.CadetBlue;
            this.roundedButton1.FlatAppearance.BorderSize = 0;
            this.roundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.roundedButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.roundedButton1.Location = new System.Drawing.Point(201, 230);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.Size = new System.Drawing.Size(160, 35);
            this.roundedButton1.TabIndex = 5;
            this.roundedButton1.Text = "Авторизоваться";
            this.roundedButton1.UseVisualStyleBackColor = false;
            this.roundedButton1.Visible = false;
            this.roundedButton1.Click += new System.EventHandler(this.LoginButton_Click);
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
            this.loginButton.Visible = false;
            this.loginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // EmployeeInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 410);
            this.Controls.Add(this.roundedButton1);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.buttonLabelPassword);
            this.Controls.Add(this.labelLine);
            this.Controls.Add(this.comboBoxLine);
            this.Controls.Add(this.labelAttachPass);
            this.Controls.Add(this.labelDateTime);
            this.Controls.Add(this.labelIPValue);
            this.Controls.Add(this.textBoxPosition);
            this.Controls.Add(this.textBoxDepartment);
            this.Controls.Add(this.textBoxName);
            this.Name = "EmployeeInfoForm";
            //this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeInfoForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox textBoxName;
        private System.Windows.Forms.ListBox textBoxDepartment;
        private System.Windows.Forms.ListBox textBoxPosition;
        private System.Windows.Forms.Label labelIPValue;
        private Label labelAttachPass;
        private Label labelDateTime;
        private ComboBox comboBoxLine;
        private Label labelLine;
        private Button buttonLabelPassword;
        private RoundedButton loginButton;
        private RoundedButton roundedButton1;
    }
}

