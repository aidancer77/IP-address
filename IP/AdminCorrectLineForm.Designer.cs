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
            this.comboBoxLine = new System.Windows.Forms.ComboBox();
            this.comboBoxCOMPort = new System.Windows.Forms.ComboBox();
            this.labelDoubleDot = new System.Windows.Forms.Label();
            this.textBoxServerPort = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxTimer = new System.Windows.Forms.TextBox();
            this.labelTimer = new System.Windows.Forms.Label();
            this.labelMinute = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonOne = new System.Windows.Forms.Button();
            this.buttonTwo = new System.Windows.Forms.Button();
            this.buttonThree = new System.Windows.Forms.Button();
            this.buttonFour = new System.Windows.Forms.Button();
            this.buttonFive = new System.Windows.Forms.Button();
            this.buttonSix = new System.Windows.Forms.Button();
            this.buttonSeven = new System.Windows.Forms.Button();
            this.buttonEight = new System.Windows.Forms.Button();
            this.buttonNine = new System.Windows.Forms.Button();
            this.buttonZero = new System.Windows.Forms.Button();
            this.buttonBackspace = new System.Windows.Forms.Button();
            this.buttonDot = new System.Windows.Forms.Button();
            this.textBoxServerIP = new System.Windows.Forms.TextBox();
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
            this.buttonChooseLine.Location = new System.Drawing.Point(152, 300);
            this.buttonChooseLine.Name = "buttonChooseLine";
            this.buttonChooseLine.Size = new System.Drawing.Size(170, 40);
            this.buttonChooseLine.TabIndex = 5;
            this.buttonChooseLine.Text = "Выбрать";
            this.buttonChooseLine.UseVisualStyleBackColor = false;
            this.buttonChooseLine.Click += new System.EventHandler(this.ButtonChooseLine_Click);
            // 
            // comboBoxLine
            // 
            this.comboBoxLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxLine.FormattingEnabled = true;
            this.comboBoxLine.Location = new System.Drawing.Point(97, 206);
            this.comboBoxLine.Name = "comboBoxLine";
            this.comboBoxLine.Size = new System.Drawing.Size(130, 26);
            this.comboBoxLine.TabIndex = 9;
            // 
            // comboBoxCOMPort
            // 
            this.comboBoxCOMPort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxCOMPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCOMPort.FormattingEnabled = true;
            this.comboBoxCOMPort.Location = new System.Drawing.Point(97, 249);
            this.comboBoxCOMPort.Name = "comboBoxCOMPort";
            this.comboBoxCOMPort.Size = new System.Drawing.Size(130, 26);
            this.comboBoxCOMPort.TabIndex = 9;
            // 
            // labelDoubleDot
            // 
            this.labelDoubleDot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelDoubleDot.BackColor = System.Drawing.Color.Transparent;
            this.labelDoubleDot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDoubleDot.Location = new System.Drawing.Point(275, 163);
            this.labelDoubleDot.Name = "labelDoubleDot";
            this.labelDoubleDot.Size = new System.Drawing.Size(12, 23);
            this.labelDoubleDot.TabIndex = 12;
            this.labelDoubleDot.Text = ":";
            // 
            // textBoxServerPort
            // 
            this.textBoxServerPort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxServerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxServerPort.Location = new System.Drawing.Point(293, 163);
            this.textBoxServerPort.Name = "textBoxServerPort";
            this.textBoxServerPort.Size = new System.Drawing.Size(51, 26);
            this.textBoxServerPort.TabIndex = 13;
            this.textBoxServerPort.Text = "8181";
            this.textBoxServerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPassword.Location = new System.Drawing.Point(310, 207);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(68, 24);
            this.textBoxPassword.TabIndex = 14;
            this.textBoxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxTimer
            // 
            this.textBoxTimer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxTimer.Location = new System.Drawing.Point(310, 250);
            this.textBoxTimer.Name = "textBoxTimer";
            this.textBoxTimer.Size = new System.Drawing.Size(34, 24);
            this.textBoxTimer.TabIndex = 16;
            this.textBoxTimer.Text = "5";
            this.textBoxTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTimer
            // 
            this.labelTimer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTimer.BackColor = System.Drawing.Color.Transparent;
            this.labelTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTimer.Location = new System.Drawing.Point(240, 252);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(65, 23);
            this.labelTimer.TabIndex = 15;
            this.labelTimer.Text = "Таймер";
            // 
            // labelMinute
            // 
            this.labelMinute.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelMinute.BackColor = System.Drawing.Color.Transparent;
            this.labelMinute.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMinute.Location = new System.Drawing.Point(346, 251);
            this.labelMinute.Name = "labelMinute";
            this.labelMinute.Size = new System.Drawing.Size(42, 23);
            this.labelMinute.TabIndex = 17;
            this.labelMinute.Text = "мин";
            // 
            // labelPassword
            // 
            this.labelPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPassword.Location = new System.Drawing.Point(241, 209);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(65, 23);
            this.labelPassword.TabIndex = 18;
            this.labelPassword.Text = "Пароль";
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
            this.buttonOne.TabIndex = 19;
            this.buttonOne.Text = "1";
            this.buttonOne.UseVisualStyleBackColor = false;
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
            this.buttonTwo.TabIndex = 19;
            this.buttonTwo.Text = "2";
            this.buttonTwo.UseVisualStyleBackColor = false;
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
            this.buttonThree.TabIndex = 19;
            this.buttonThree.Text = "3";
            this.buttonThree.UseVisualStyleBackColor = false;
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
            this.buttonFour.TabIndex = 19;
            this.buttonFour.Text = "4";
            this.buttonFour.UseVisualStyleBackColor = false;
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
            this.buttonFive.TabIndex = 19;
            this.buttonFive.Text = "5";
            this.buttonFive.UseVisualStyleBackColor = false;
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
            this.buttonSix.TabIndex = 19;
            this.buttonSix.Text = "6";
            this.buttonSix.UseVisualStyleBackColor = false;
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
            this.buttonSeven.TabIndex = 19;
            this.buttonSeven.Text = "7";
            this.buttonSeven.UseVisualStyleBackColor = false;
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
            this.buttonEight.TabIndex = 19;
            this.buttonEight.Text = "8";
            this.buttonEight.UseVisualStyleBackColor = false;
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
            this.buttonNine.TabIndex = 19;
            this.buttonNine.Text = "9";
            this.buttonNine.UseVisualStyleBackColor = false;
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
            this.buttonZero.TabIndex = 19;
            this.buttonZero.Text = "0";
            this.buttonZero.UseVisualStyleBackColor = false;
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
            this.buttonBackspace.TabIndex = 19;
            this.buttonBackspace.UseVisualStyleBackColor = false;
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
            this.buttonDot.TabIndex = 19;
            this.buttonDot.Text = ".";
            this.buttonDot.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonDot.UseVisualStyleBackColor = false;
            // 
            // textBoxServerIP
            // 
            this.textBoxServerIP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxServerIP.Location = new System.Drawing.Point(148, 163);
            this.textBoxServerIP.Name = "textBoxServerIP";
            this.textBoxServerIP.Size = new System.Drawing.Size(121, 26);
            this.textBoxServerIP.TabIndex = 20;
            this.textBoxServerIP.Text = "192.168.77.74";
            this.textBoxServerIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.Controls.Add(this.textBoxServerIP);
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
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelMinute);
            this.Controls.Add(this.labelTimer);
            this.Controls.Add(this.textBoxTimer);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxServerPort);
            this.Controls.Add(this.labelDoubleDot);
            this.Controls.Add(this.comboBoxCOMPort);
            this.Controls.Add(this.comboBoxLine);
            this.Controls.Add(this.labelDateTime);
            this.Controls.Add(this.labelIPValue);
            this.Controls.Add(this.buttonChooseLine);
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
        private ComboBox comboBoxLine;
        private DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn checklineDataGridViewTextBoxColumn;
        private ComboBox comboBoxCOMPort;
        private Label labelDoubleDot;
        private TextBox textBoxServerPort;
        private TextBox textBoxPassword;
        private TextBox textBoxTimer;
        private Label labelTimer;
        private Label labelMinute;
        private Label labelPassword;
        private Button buttonOne;
        private Button buttonTwo;
        private Button buttonThree;
        private Button buttonFour;
        private Button buttonFive;
        private Button buttonSix;
        private Button buttonSeven;
        private Button buttonEight;
        private Button buttonNine;
        private Button buttonZero;
        private Button buttonBackspace;
        private Button buttonDot;
        private TextBox textBoxServerIP;
        private BindingSource lineInfoBindingSource;
    }
}