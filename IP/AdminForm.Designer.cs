using System.Windows.Forms;

namespace IP
{
    partial class AdminForm
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
            this.labelAttachPass = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.ButtonLabelEnter = new IP.RoundedButton();
            this.buttonLabelBack = new IP.RoundedButton();
            this.SuspendLayout();
            // 
            // labelIPValue
            // 
            this.labelIPValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelIPValue.AutoSize = true;
            this.labelIPValue.BackColor = System.Drawing.Color.Transparent;
            this.labelIPValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelIPValue.ForeColor = System.Drawing.Color.White;
            this.labelIPValue.Location = new System.Drawing.Point(94, 105);
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
            this.labelDateTime.Location = new System.Drawing.Point(373, 105);
            this.labelDateTime.Name = "labelDateTime";
            this.labelDateTime.Size = new System.Drawing.Size(0, 20);
            this.labelDateTime.TabIndex = 4;
            this.labelDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAttachPass
            // 
            this.labelAttachPass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelAttachPass.AutoSize = true;
            this.labelAttachPass.BackColor = System.Drawing.Color.Transparent;
            this.labelAttachPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAttachPass.Location = new System.Drawing.Point(247, 165);
            this.labelAttachPass.Name = "labelAttachPass";
            this.labelAttachPass.Size = new System.Drawing.Size(120, 18);
            this.labelAttachPass.TabIndex = 4;
            this.labelAttachPass.Text = "Введите пароль";
            this.labelAttachPass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPassword.Location = new System.Drawing.Point(220, 200);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(170, 24);
            this.textBoxPassword.TabIndex = 8;
            // 
            // ButtonLabelEnter
            // 
            this.ButtonLabelEnter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ButtonLabelEnter.BackColor = System.Drawing.Color.ForestGreen;
            this.ButtonLabelEnter.CornerRadius = 30;
            this.ButtonLabelEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonLabelEnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonLabelEnter.ForeColor = System.Drawing.Color.White;
            this.ButtonLabelEnter.Location = new System.Drawing.Point(220, 242);
            this.ButtonLabelEnter.Name = "ButtonLabelEnter";
            this.ButtonLabelEnter.Size = new System.Drawing.Size(170, 40);
            this.ButtonLabelEnter.TabIndex = 5;
            this.ButtonLabelEnter.Text = "Войти";
            this.ButtonLabelEnter.UseVisualStyleBackColor = false;
            this.ButtonLabelEnter.Click += new System.EventHandler(this.ButtonLabelEnter_Click);
            // 
            // buttonLabelBack
            // 
            this.buttonLabelBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonLabelBack.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonLabelBack.CornerRadius = 30;
            this.buttonLabelBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLabelBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLabelBack.ForeColor = System.Drawing.Color.White;
            this.buttonLabelBack.Location = new System.Drawing.Point(220, 297);
            this.buttonLabelBack.Name = "buttonLabelBack";
            this.buttonLabelBack.Size = new System.Drawing.Size(170, 40);
            this.buttonLabelBack.TabIndex = 5;
            this.buttonLabelBack.Text = "Вернуться";
            this.buttonLabelBack.UseVisualStyleBackColor = false;
            this.buttonLabelBack.Click += new System.EventHandler(this.ButtonLabelBack_Click);
            // 
            // AdminForm
            // 
            this.AcceptButton = this.ButtonLabelEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(569, 410);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelDateTime);
            this.Controls.Add(this.labelIPValue);
            this.Controls.Add(this.buttonLabelBack);
            this.Controls.Add(this.ButtonLabelEnter);
            this.Controls.Add(this.labelAttachPass);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeInfoForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RoundedButton ButtonLabelEnter;
        private Label labelIPValue;
        private Label labelDateTime;
        private Label labelAttachPass;
        private TextBox textBoxPassword;
        private RoundedButton buttonLabelBack;
    }
}