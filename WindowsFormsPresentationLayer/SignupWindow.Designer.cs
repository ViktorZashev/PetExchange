namespace WindowsFormsPresentationLayer
{
	partial class SignupWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignupWindow));
			UsernameLabel = new Label();
			SignUpTitle = new Label();
			PasswordLabel = new Label();
			TownLabel = new Label();
			CountryLabel = new Label();
			ContactInfoLabel = new Label();
			AdminPasswordLabel = new Label();
			label1 = new Label();
			textBox1 = new TextBox();
			textBox3 = new TextBox();
			textBox2 = new TextBox();
			textBox4 = new TextBox();
			textBox5 = new TextBox();
			textBox7 = new TextBox();
			textBox8 = new TextBox();
			label2 = new Label();
			comboBox1 = new ComboBox();
			AdminBox = new CheckBox();
			panel3 = new Panel();
			panel1 = new Panel();
			panel2 = new Panel();
			panel4 = new Panel();
			TownUnderScore = new Panel();
			CountryUnderscore = new Panel();
			panel7 = new Panel();
			panel8 = new Panel();
			LoginButton = new Button();
			pictureBox1 = new PictureBox();
			pictureBox2 = new PictureBox();
			AdminPasswordError = new Label();
			UsernameErrorMessage = new Label();
			CountryInstructionsLabel = new Label();
			pictureBox4 = new PictureBox();
			SignUpLinkLabel = new LinkLabel();
			label3 = new Label();
			SuccessMessageLabel = new Label();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
			SuspendLayout();
			// 
			// UsernameLabel
			// 
			UsernameLabel.AutoSize = true;
			UsernameLabel.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			UsernameLabel.Location = new Point(60, 230);
			UsernameLabel.Name = "UsernameLabel";
			UsernameLabel.Size = new Size(102, 25);
			UsernameLabel.TabIndex = 0;
			UsernameLabel.Text = "Username:";
			// 
			// SignUpTitle
			// 
			SignUpTitle.AutoSize = true;
			SignUpTitle.Font = new Font("Segoe UI", 25.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			SignUpTitle.Location = new Point(298, 139);
			SignUpTitle.Name = "SignUpTitle";
			SignUpTitle.Size = new Size(193, 57);
			SignUpTitle.TabIndex = 1;
			SignUpTitle.Text = "SIGN UP";
			SignUpTitle.Click += SignUpTitle_Click;
			// 
			// PasswordLabel
			// 
			PasswordLabel.AutoSize = true;
			PasswordLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			PasswordLabel.Location = new Point(60, 283);
			PasswordLabel.Name = "PasswordLabel";
			PasswordLabel.Size = new Size(90, 23);
			PasswordLabel.TabIndex = 2;
			PasswordLabel.Text = "Password:";
			// 
			// TownLabel
			// 
			TownLabel.AutoSize = true;
			TownLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			TownLabel.Location = new Point(363, 287);
			TownLabel.Name = "TownLabel";
			TownLabel.Size = new Size(57, 23);
			TownLabel.TabIndex = 3;
			TownLabel.Text = "Town:";
			// 
			// CountryLabel
			// 
			CountryLabel.AutoSize = true;
			CountryLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			CountryLabel.Location = new Point(557, 286);
			CountryLabel.Name = "CountryLabel";
			CountryLabel.Size = new Size(80, 23);
			CountryLabel.TabIndex = 4;
			CountryLabel.Text = "Country:";
			CountryLabel.Visible = false;
			CountryLabel.Click += CountryLabel_Click;
			// 
			// ContactInfoLabel
			// 
			ContactInfoLabel.AutoSize = true;
			ContactInfoLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			ContactInfoLabel.Location = new Point(363, 326);
			ContactInfoLabel.Name = "ContactInfoLabel";
			ContactInfoLabel.Size = new Size(114, 23);
			ContactInfoLabel.TabIndex = 5;
			ContactInfoLabel.Text = "Contact Info:";
			// 
			// AdminPasswordLabel
			// 
			AdminPasswordLabel.AutoSize = true;
			AdminPasswordLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			AdminPasswordLabel.Location = new Point(253, 411);
			AdminPasswordLabel.Name = "AdminPasswordLabel";
			AdminPasswordLabel.Size = new Size(149, 23);
			AdminPasswordLabel.TabIndex = 7;
			AdminPasswordLabel.Text = "Admin Password:";
			AdminPasswordLabel.Visible = false;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label1.Location = new Point(363, 227);
			label1.Name = "label1";
			label1.Size = new Size(62, 23);
			label1.TabIndex = 8;
			label1.Text = "Name:";
			// 
			// textBox1
			// 
			textBox1.Location = new Point(185, 227);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(125, 27);
			textBox1.TabIndex = 9;
			// 
			// textBox3
			// 
			textBox3.Location = new Point(431, 227);
			textBox3.Name = "textBox3";
			textBox3.Size = new Size(156, 27);
			textBox3.TabIndex = 9;
			// 
			// textBox2
			// 
			textBox2.Location = new Point(185, 279);
			textBox2.Name = "textBox2";
			textBox2.Size = new Size(125, 27);
			textBox2.TabIndex = 14;
			// 
			// textBox4
			// 
			textBox4.Location = new Point(426, 283);
			textBox4.Name = "textBox4";
			textBox4.Size = new Size(125, 27);
			textBox4.TabIndex = 15;
			// 
			// textBox5
			// 
			textBox5.Location = new Point(483, 322);
			textBox5.Name = "textBox5";
			textBox5.Size = new Size(313, 27);
			textBox5.TabIndex = 16;
			// 
			// textBox7
			// 
			textBox7.Location = new Point(408, 407);
			textBox7.Name = "textBox7";
			textBox7.Size = new Size(125, 27);
			textBox7.TabIndex = 18;
			textBox7.Visible = false;
			// 
			// textBox8
			// 
			textBox8.Location = new Point(185, 325);
			textBox8.Name = "textBox8";
			textBox8.Size = new Size(125, 27);
			textBox8.TabIndex = 21;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label2.Location = new Point(18, 329);
			label2.Name = "label2";
			label2.Size = new Size(161, 23);
			label2.TabIndex = 19;
			label2.Text = "Confirm Password:";
			label2.Click += label2_Click;
			// 
			// comboBox1
			// 
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new Point(643, 281);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new Size(151, 28);
			comboBox1.TabIndex = 22;
			comboBox1.Visible = false;
			// 
			// AdminBox
			// 
			AdminBox.AutoSize = true;
			AdminBox.Location = new Point(254, 373);
			AdminBox.Name = "AdminBox";
			AdminBox.Size = new Size(153, 24);
			AdminBox.TabIndex = 23;
			AdminBox.Text = "Register As Admin";
			AdminBox.UseVisualStyleBackColor = true;
			AdminBox.CheckedChanged += AdminBox_CheckedChanged;
			// 
			// panel3
			// 
			panel3.BackColor = Color.Black;
			panel3.Location = new Point(60, 258);
			panel3.Name = "panel3";
			panel3.Size = new Size(256, 1);
			panel3.TabIndex = 24;
			// 
			// panel1
			// 
			panel1.BackColor = Color.Black;
			panel1.Location = new Point(60, 312);
			panel1.Name = "panel1";
			panel1.Size = new Size(256, 1);
			panel1.TabIndex = 25;
			// 
			// panel2
			// 
			panel2.BackColor = Color.Black;
			panel2.Location = new Point(18, 355);
			panel2.Name = "panel2";
			panel2.Size = new Size(294, 1);
			panel2.TabIndex = 26;
			// 
			// panel4
			// 
			panel4.BackColor = Color.Black;
			panel4.Location = new Point(363, 259);
			panel4.Name = "panel4";
			panel4.Size = new Size(214, 1);
			panel4.TabIndex = 27;
			// 
			// TownUnderScore
			// 
			TownUnderScore.BackColor = Color.Black;
			TownUnderScore.Location = new Point(363, 315);
			TownUnderScore.Name = "TownUnderScore";
			TownUnderScore.Size = new Size(193, 1);
			TownUnderScore.TabIndex = 26;
			// 
			// CountryUnderscore
			// 
			CountryUnderscore.BackColor = Color.Black;
			CountryUnderscore.Location = new Point(540, 315);
			CountryUnderscore.Name = "CountryUnderscore";
			CountryUnderscore.Size = new Size(256, 1);
			CountryUnderscore.TabIndex = 27;
			CountryUnderscore.Visible = false;
			// 
			// panel7
			// 
			panel7.BackColor = Color.Black;
			panel7.Location = new Point(363, 355);
			panel7.Name = "panel7";
			panel7.Size = new Size(434, 1);
			panel7.TabIndex = 28;
			// 
			// panel8
			// 
			panel8.BackColor = Color.Black;
			panel8.Location = new Point(253, 440);
			panel8.Name = "panel8";
			panel8.Size = new Size(294, 1);
			panel8.TabIndex = 27;
			panel8.Visible = false;
			// 
			// LoginButton
			// 
			LoginButton.BackColor = Color.FromArgb(0, 117, 214);
			LoginButton.FlatAppearance.BorderColor = Color.Black;
			LoginButton.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
			LoginButton.ForeColor = Color.White;
			LoginButton.Location = new Point(232, 461);
			LoginButton.Name = "LoginButton";
			LoginButton.Size = new Size(355, 55);
			LoginButton.TabIndex = 29;
			LoginButton.Text = "REGISTER";
			LoginButton.UseVisualStyleBackColor = false;
			// 
			// pictureBox1
			// 
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new Point(603, 401);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(198, 173);
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox1.TabIndex = 30;
			pictureBox1.TabStop = false;
			pictureBox1.Click += pictureBox1_Click;
			// 
			// pictureBox2
			// 
			pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
			pictureBox2.Location = new Point(10, 401);
			pictureBox2.Name = "pictureBox2";
			pictureBox2.Size = new Size(213, 173);
			pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox2.TabIndex = 31;
			pictureBox2.TabStop = false;
			// 
			// AdminPasswordError
			// 
			AdminPasswordError.AutoSize = true;
			AdminPasswordError.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
			AdminPasswordError.ForeColor = Color.Red;
			AdminPasswordError.Location = new Point(408, 373);
			AdminPasswordError.Name = "AdminPasswordError";
			AdminPasswordError.Size = new Size(163, 20);
			AdminPasswordError.TabIndex = 32;
			AdminPasswordError.Text = "Wrong Admin Password";
			AdminPasswordError.Visible = false;
			// 
			// UsernameErrorMessage
			// 
			UsernameErrorMessage.AutoSize = true;
			UsernameErrorMessage.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
			UsernameErrorMessage.ForeColor = Color.Red;
			UsernameErrorMessage.Location = new Point(60, 204);
			UsernameErrorMessage.Name = "UsernameErrorMessage";
			UsernameErrorMessage.Size = new Size(163, 20);
			UsernameErrorMessage.TabIndex = 33;
			UsernameErrorMessage.Text = "Username already exists";
			UsernameErrorMessage.Visible = false;
			// 
			// CountryInstructionsLabel
			// 
			CountryInstructionsLabel.AutoSize = true;
			CountryInstructionsLabel.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			CountryInstructionsLabel.ForeColor = SystemColors.MenuHighlight;
			CountryInstructionsLabel.Location = new Point(608, 226);
			CountryInstructionsLabel.Name = "CountryInstructionsLabel";
			CountryInstructionsLabel.Size = new Size(196, 34);
			CountryInstructionsLabel.TabIndex = 34;
			CountryInstructionsLabel.Text = "Choose Country From Options/\r\n Register new Country";
			CountryInstructionsLabel.Visible = false;
			// 
			// pictureBox4
			// 
			pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
			pictureBox4.Location = new Point(309, 3);
			pictureBox4.Name = "pictureBox4";
			pictureBox4.Size = new Size(168, 133);
			pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox4.TabIndex = 36;
			pictureBox4.TabStop = false;
			// 
			// SignUpLinkLabel
			// 
			SignUpLinkLabel.AutoSize = true;
			SignUpLinkLabel.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			SignUpLinkLabel.Location = new Point(478, 550);
			SignUpLinkLabel.Name = "SignUpLinkLabel";
			SignUpLinkLabel.Size = new Size(62, 25);
			SignUpLinkLabel.TabIndex = 38;
			SignUpLinkLabel.TabStop = true;
			SignUpLinkLabel.Text = "Log In";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
			label3.Location = new Point(232, 550);
			label3.Name = "label3";
			label3.Size = new Size(215, 25);
			label3.TabIndex = 37;
			label3.Text = "Already have an account?";
			// 
			// SuccessMessageLabel
			// 
			SuccessMessageLabel.AutoSize = true;
			SuccessMessageLabel.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
			SuccessMessageLabel.ForeColor = Color.Green;
			SuccessMessageLabel.Location = new Point(270, 519);
			SuccessMessageLabel.Name = "SuccessMessageLabel";
			SuccessMessageLabel.Size = new Size(270, 28);
			SuccessMessageLabel.TabIndex = 39;
			SuccessMessageLabel.Text = "Account Successfully Created!";
			SuccessMessageLabel.Visible = false;
			// 
			// SignupWindow
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(813, 584);
			Controls.Add(SuccessMessageLabel);
			Controls.Add(SignUpLinkLabel);
			Controls.Add(label3);
			Controls.Add(pictureBox4);
			Controls.Add(CountryInstructionsLabel);
			Controls.Add(UsernameErrorMessage);
			Controls.Add(AdminPasswordError);
			Controls.Add(pictureBox2);
			Controls.Add(pictureBox1);
			Controls.Add(LoginButton);
			Controls.Add(panel8);
			Controls.Add(panel7);
			Controls.Add(CountryUnderscore);
			Controls.Add(TownUnderScore);
			Controls.Add(panel4);
			Controls.Add(panel2);
			Controls.Add(panel1);
			Controls.Add(panel3);
			Controls.Add(AdminBox);
			Controls.Add(comboBox1);
			Controls.Add(textBox8);
			Controls.Add(label2);
			Controls.Add(textBox7);
			Controls.Add(textBox5);
			Controls.Add(textBox4);
			Controls.Add(textBox2);
			Controls.Add(textBox3);
			Controls.Add(textBox1);
			Controls.Add(label1);
			Controls.Add(AdminPasswordLabel);
			Controls.Add(ContactInfoLabel);
			Controls.Add(CountryLabel);
			Controls.Add(TownLabel);
			Controls.Add(PasswordLabel);
			Controls.Add(SignUpTitle);
			Controls.Add(UsernameLabel);
			Name = "SignupWindow";
			Text = "Sign Up";
			Load += SignupWindow_Load;
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label UsernameLabel;
		private Label SignUpTitle;
		private Label PasswordLabel;
		private Label TownLabel;
		private Label CountryLabel;
		private Label ContactInfoLabel;
		private Label AdminPasswordLabel;
		private Label label1;
		private TextBox textBox1;
		private TextBox textBox3;
		private TextBox textBox2;
		private TextBox textBox4;
		private TextBox textBox5;
		private TextBox textBox7;
		private TextBox textBox8;
		private Label label2;
		private ComboBox comboBox1;
		private CheckBox AdminBox;
		private Panel panel3;
		private Panel panel1;
		private Panel panel2;
		private Panel panel4;
		private Panel TownUnderScore;
		private Panel CountryUnderscore;
		private Panel panel7;
		private Panel panel8;
		private Button LoginButton;
		private PictureBox pictureBox1;
		private PictureBox pictureBox2;
		private Label AdminPasswordError;
		private Label UsernameErrorMessage;
		private Label CountryInstructionsLabel;
		private PictureBox pictureBox4;
		private LinkLabel SignUpLinkLabel;
		private Label label3;
		private Label SuccessMessageLabel;
	}
}