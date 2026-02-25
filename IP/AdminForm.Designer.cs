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
            this.buttonLabelEnter = new IP.RoundedButton();
            this.buttonLabelBack = new IP.RoundedButton();
            this.buttonBackspace = new System.Windows.Forms.Button();
            this.buttonNine = new System.Windows.Forms.Button();
            this.buttonSix = new System.Windows.Forms.Button();
            this.buttonDot = new System.Windows.Forms.Button();
            this.buttonZero = new System.Windows.Forms.Button();
            this.buttonThree = new System.Windows.Forms.Button();
            this.buttonEight = new System.Windows.Forms.Button();
            this.buttonFive = new System.Windows.Forms.Button();
            this.buttonTwo = new System.Windows.Forms.Button();
            this.buttonSeven = new System.Windows.Forms.Button();
            this.buttonFour = new System.Windows.Forms.Button();
            this.buttonOne = new System.Windows.Forms.Button();
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
            this.labelAttachPass.Location = new System.Drawing.Point(132, 165);
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
            this.textBoxPassword.Location = new System.Drawing.Point(105, 200);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(170, 24);
            this.textBoxPassword.TabIndex = 8;
            // 
            // buttonLabelEnter
            // 
            this.buttonLabelEnter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonLabelEnter.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonLabelEnter.CornerRadius = 30;
            this.buttonLabelEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLabelEnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLabelEnter.ForeColor = System.Drawing.Color.White;
            this.buttonLabelEnter.Location = new System.Drawing.Point(105, 245);
            this.buttonLabelEnter.Name = "buttonLabelEnter";
            this.buttonLabelEnter.Size = new System.Drawing.Size(170, 40);
            this.buttonLabelEnter.TabIndex = 5;
            this.buttonLabelEnter.Text = "Войти";
            this.buttonLabelEnter.UseVisualStyleBackColor = false;
            this.buttonLabelEnter.Click += new System.EventHandler(this.ButtonLabelEnter_Click);
            // 
            // buttonLabelBack
            // 
            this.buttonLabelBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonLabelBack.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonLabelBack.CornerRadius = 30;
            this.buttonLabelBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLabelBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLabelBack.ForeColor = System.Drawing.Color.White;
            this.buttonLabelBack.Location = new System.Drawing.Point(105, 300);
            this.buttonLabelBack.Name = "buttonLabelBack";
            this.buttonLabelBack.Size = new System.Drawing.Size(170, 40);
            this.buttonLabelBack.TabIndex = 5;
            this.buttonLabelBack.Text = "Вернуться";
            this.buttonLabelBack.UseVisualStyleBackColor = false;
            this.buttonLabelBack.Click += new System.EventHandler(this.ButtonLabelBack_Click);
            // 
            // buttonBackspace
            // 
            this.buttonBackspace.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonBackspace.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonBackspace.BackgroundImage = global::Autorization.Properties.Resources.icons_18;
            this.buttonBackspace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonBackspace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBackspace.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBackspace.ForeColor = System.Drawing.Color.White;
            this.buttonBackspace.Location = new System.Drawing.Point(476, 278);
            this.buttonBackspace.Name = "buttonBackspace";
            this.buttonBackspace.Size = new System.Drawing.Size(30, 30);
            this.buttonBackspace.TabIndex = 20;
            this.buttonBackspace.UseVisualStyleBackColor = false;
            // 
            // buttonNine
            // 
            this.buttonNine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonNine.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonNine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNine.ForeColor = System.Drawing.Color.White;
            this.buttonNine.Location = new System.Drawing.Point(476, 240);
            this.buttonNine.Name = "buttonNine";
            this.buttonNine.Size = new System.Drawing.Size(30, 30);
            this.buttonNine.TabIndex = 21;
            this.buttonNine.Text = "9";
            this.buttonNine.UseVisualStyleBackColor = false;
            // 
            // buttonSix
            // 
            this.buttonSix.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSix.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonSix.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSix.ForeColor = System.Drawing.Color.White;
            this.buttonSix.Location = new System.Drawing.Point(476, 202);
            this.buttonSix.Name = "buttonSix";
            this.buttonSix.Size = new System.Drawing.Size(30, 30);
            this.buttonSix.TabIndex = 22;
            this.buttonSix.Text = "6";
            this.buttonSix.UseVisualStyleBackColor = false;
            // 
            // buttonDot
            // 
            this.buttonDot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonDot.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonDot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDot.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDot.ForeColor = System.Drawing.Color.White;
            this.buttonDot.Location = new System.Drawing.Point(400, 278);
            this.buttonDot.Name = "buttonDot";
            this.buttonDot.Size = new System.Drawing.Size(30, 30);
            this.buttonDot.TabIndex = 23;
            this.buttonDot.Text = ".";
            this.buttonDot.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonDot.UseVisualStyleBackColor = false;
            // 
            // buttonZero
            // 
            this.buttonZero.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonZero.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonZero.ForeColor = System.Drawing.Color.White;
            this.buttonZero.Location = new System.Drawing.Point(438, 278);
            this.buttonZero.Name = "buttonZero";
            this.buttonZero.Size = new System.Drawing.Size(30, 30);
            this.buttonZero.TabIndex = 24;
            this.buttonZero.Text = "0";
            this.buttonZero.UseVisualStyleBackColor = false;
            // 
            // buttonThree
            // 
            this.buttonThree.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonThree.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonThree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonThree.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonThree.ForeColor = System.Drawing.Color.White;
            this.buttonThree.Location = new System.Drawing.Point(476, 163);
            this.buttonThree.Name = "buttonThree";
            this.buttonThree.Size = new System.Drawing.Size(30, 30);
            this.buttonThree.TabIndex = 25;
            this.buttonThree.Text = "3";
            this.buttonThree.UseVisualStyleBackColor = false;
            // 
            // buttonEight
            // 
            this.buttonEight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonEight.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonEight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonEight.ForeColor = System.Drawing.Color.White;
            this.buttonEight.Location = new System.Drawing.Point(438, 240);
            this.buttonEight.Name = "buttonEight";
            this.buttonEight.Size = new System.Drawing.Size(30, 30);
            this.buttonEight.TabIndex = 26;
            this.buttonEight.Text = "8";
            this.buttonEight.UseVisualStyleBackColor = false;
            // 
            // buttonFive
            // 
            this.buttonFive.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonFive.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonFive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFive.ForeColor = System.Drawing.Color.White;
            this.buttonFive.Location = new System.Drawing.Point(438, 202);
            this.buttonFive.Name = "buttonFive";
            this.buttonFive.Size = new System.Drawing.Size(30, 30);
            this.buttonFive.TabIndex = 27;
            this.buttonFive.Text = "5";
            this.buttonFive.UseVisualStyleBackColor = false;
            // 
            // buttonTwo
            // 
            this.buttonTwo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonTwo.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonTwo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTwo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonTwo.ForeColor = System.Drawing.Color.White;
            this.buttonTwo.Location = new System.Drawing.Point(438, 163);
            this.buttonTwo.Name = "buttonTwo";
            this.buttonTwo.Size = new System.Drawing.Size(30, 30);
            this.buttonTwo.TabIndex = 28;
            this.buttonTwo.Text = "2";
            this.buttonTwo.UseVisualStyleBackColor = false;
            // 
            // buttonSeven
            // 
            this.buttonSeven.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSeven.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonSeven.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSeven.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSeven.ForeColor = System.Drawing.Color.White;
            this.buttonSeven.Location = new System.Drawing.Point(400, 240);
            this.buttonSeven.Name = "buttonSeven";
            this.buttonSeven.Size = new System.Drawing.Size(30, 30);
            this.buttonSeven.TabIndex = 29;
            this.buttonSeven.Text = "7";
            this.buttonSeven.UseVisualStyleBackColor = false;
            // 
            // buttonFour
            // 
            this.buttonFour.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonFour.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonFour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFour.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFour.ForeColor = System.Drawing.Color.White;
            this.buttonFour.Location = new System.Drawing.Point(400, 202);
            this.buttonFour.Name = "buttonFour";
            this.buttonFour.Size = new System.Drawing.Size(30, 30);
            this.buttonFour.TabIndex = 30;
            this.buttonFour.Text = "4";
            this.buttonFour.UseVisualStyleBackColor = false;
            // 
            // buttonOne
            // 
            this.buttonOne.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOne.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOne.ForeColor = System.Drawing.Color.White;
            this.buttonOne.Location = new System.Drawing.Point(400, 163);
            this.buttonOne.Name = "buttonOne";
            this.buttonOne.Size = new System.Drawing.Size(30, 30);
            this.buttonOne.TabIndex = 31;
            this.buttonOne.Text = "1";
            this.buttonOne.UseVisualStyleBackColor = false;
            // 
            // AdminForm
            // 
            this.AcceptButton = this.buttonLabelEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(569, 410);
            this.ControlBox = false;
            this.Controls.Add(this.buttonBackspace);
            this.Controls.Add(this.buttonNine);
            this.Controls.Add(this.buttonSix);
            this.Controls.Add(this.buttonDot);
            this.Controls.Add(this.buttonZero);
            this.Controls.Add(this.buttonThree);
            this.Controls.Add(this.buttonEight);
            this.Controls.Add(this.buttonFive);
            this.Controls.Add(this.buttonTwo);
            this.Controls.Add(this.buttonSeven);
            this.Controls.Add(this.buttonFour);
            this.Controls.Add(this.buttonOne);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelDateTime);
            this.Controls.Add(this.labelIPValue);
            this.Controls.Add(this.buttonLabelBack);
            this.Controls.Add(this.buttonLabelEnter);
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

        private RoundedButton buttonLabelEnter;
        private Label labelIPValue;
        private Label labelDateTime;
        private Label labelAttachPass;
        private TextBox textBoxPassword;
        private RoundedButton buttonLabelBack;
        private Button buttonBackspace;
        private Button buttonNine;
        private Button buttonSix;
        private Button buttonDot;
        private Button buttonZero;
        private Button buttonThree;
        private Button buttonEight;
        private Button buttonFive;
        private Button buttonTwo;
        private Button buttonSeven;
        private Button buttonFour;
        private Button buttonOne;
    }
}