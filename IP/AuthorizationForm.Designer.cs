using System;
using System.Windows.Forms;

namespace IP
{
    partial class AuthorizationForm
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
            this.labelIPValue = new System.Windows.Forms.Label();
            this.labelDateTime = new System.Windows.Forms.Label();
            this.labelLineResultAuth = new System.Windows.Forms.Label();
            this.buttonLabelCard = new IP.RoundedButton();
            this.buttonLabelPassword = new IP.RoundedButton();
            this.SuspendLayout();
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
            // labelLineResultAuth
            // 
            this.labelLineResultAuth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLineResultAuth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLineResultAuth.Location = new System.Drawing.Point(216, 107);
            this.labelLineResultAuth.Name = "labelLineResultAuth";
            this.labelLineResultAuth.Size = new System.Drawing.Size(147, 20);
            this.labelLineResultAuth.TabIndex = 7;
            this.labelLineResultAuth.Text = "Line";
            this.labelLineResultAuth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonLabelCard
            // 
            this.buttonLabelCard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonLabelCard.BackColor = System.Drawing.Color.CadetBlue;
            this.buttonLabelCard.CornerRadius = 15;
            this.buttonLabelCard.FlatAppearance.BorderColor = System.Drawing.Color.CadetBlue;
            this.buttonLabelCard.FlatAppearance.BorderSize = 0;
            this.buttonLabelCard.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonLabelCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLabelCard.Location = new System.Drawing.Point(202, 197);
            this.buttonLabelCard.Name = "buttonLabelCard";
            this.buttonLabelCard.Size = new System.Drawing.Size(170, 35);
            this.buttonLabelCard.TabIndex = 8;
            this.buttonLabelCard.Text = "Вход по пропуску";
            this.buttonLabelCard.UseVisualStyleBackColor = true;
            this.buttonLabelCard.Click += new System.EventHandler(this.ButtonLabelCard_Click);
            // 
            // buttonLabelPassword
            // 
            this.buttonLabelPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonLabelPassword.BackColor = System.Drawing.Color.CadetBlue;
            this.buttonLabelPassword.CornerRadius = 15;
            this.buttonLabelPassword.FlatAppearance.BorderColor = System.Drawing.Color.CadetBlue;
            this.buttonLabelPassword.FlatAppearance.BorderSize = 0;
            this.buttonLabelPassword.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonLabelPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLabelPassword.Location = new System.Drawing.Point(202, 260);
            this.buttonLabelPassword.Name = "buttonLabelPassword";
            this.buttonLabelPassword.Size = new System.Drawing.Size(170, 35);
            this.buttonLabelPassword.TabIndex = 5;
            this.buttonLabelPassword.Text = "Вход по паролю";
            this.buttonLabelPassword.UseVisualStyleBackColor = false;
            this.buttonLabelPassword.Click += new System.EventHandler(this.ButtonLabelPassword_Click);
            // 
            // AuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 410);
            this.ControlBox = false;
            this.Controls.Add(this.buttonLabelCard);
            this.Controls.Add(this.labelLineResultAuth);
            this.Controls.Add(this.labelDateTime);
            this.Controls.Add(this.labelIPValue);
            this.Controls.Add(this.buttonLabelPassword);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "AuthorizationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeInfoForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RoundedButton buttonLabelPassword;
        private Label labelIPValue;
        private Label labelDateTime;
        private Label labelLineResultAuth;
        private RoundedButton buttonLabelCard;
    }
}