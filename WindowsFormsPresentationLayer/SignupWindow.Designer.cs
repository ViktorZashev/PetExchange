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
			UsernameTextBox = new TextBox();
			NameTextBox = new TextBox();
			PasswordTextBox = new TextBox();
			TownTextBox = new TextBox();
			ContactInfoTextBox = new TextBox();
			AdminPasswordTextBox = new TextBox();
			ConfirmPasswordTextBox = new TextBox();
			label2 = new Label();
			CountryDropDownMenu = new ComboBox();
			AdminBox = new CheckBox();
			panel3 = new Panel();
			panel1 = new Panel();
			panel2 = new Panel();
			panel4 = new Panel();
			TownUnderScore = new Panel();
			CountryUnderscore = new Panel();
			panel7 = new Panel();
			panel8 = new Panel();
			RegisterButton = new Button();
			pictureBox1 = new PictureBox();
			pictureBox2 = new PictureBox();
			AdminPasswordError = new Label();
			UsernameErrorMessage = new Label();
			CountryInstructionsLabel = new Label();
			pictureBox4 = new PictureBox();
			LogInLinkLabel = new LinkLabel();
			label3 = new Label();
			SuccessMessageLabel = new Label();
			label4 = new Label();
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
			// UsernameTextBox
			// 
			UsernameTextBox.Location = new Point(185, 227);
			UsernameTextBox.Name = "UsernameTextBox";
			UsernameTextBox.Size = new Size(125, 27);
			UsernameTextBox.TabIndex = 9;
			UsernameTextBox.Leave += UsernameTextBox_Leave;
			// 
			// NameTextBox
			// 
			NameTextBox.Location = new Point(431, 227);
			NameTextBox.Name = "NameTextBox";
			NameTextBox.Size = new Size(156, 27);
			NameTextBox.TabIndex = 9;
			// 
			// PasswordTextBox
			// 
			PasswordTextBox.Location = new Point(185, 279);
			PasswordTextBox.Name = "PasswordTextBox";
			PasswordTextBox.Size = new Size(125, 27);
			PasswordTextBox.TabIndex = 14;
			// 
			// TownTextBox
			// 
			TownTextBox.Location = new Point(426, 283);
			TownTextBox.Name = "TownTextBox";
			TownTextBox.Size = new Size(125, 27);
			TownTextBox.TabIndex = 15;
			// 
			// ContactInfoTextBox
			// 
			ContactInfoTextBox.Location = new Point(483, 322);
			ContactInfoTextBox.Name = "ContactInfoTextBox";
			ContactInfoTextBox.Size = new Size(313, 27);
			ContactInfoTextBox.TabIndex = 16;
			// 
			// AdminPasswordTextBox
			// 
			AdminPasswordTextBox.Location = new Point(408, 407);
			AdminPasswordTextBox.Name = "AdminPasswordTextBox";
			AdminPasswordTextBox.Size = new Size(125, 27);
			AdminPasswordTextBox.TabIndex = 18;
			AdminPasswordTextBox.Visible = false;
			// 
			// ConfirmPasswordTextBox
			// 
			ConfirmPasswordTextBox.Location = new Point(185, 325);
			ConfirmPasswordTextBox.Name = "ConfirmPasswordTextBox";
			ConfirmPasswordTextBox.Size = new Size(125, 27);
			ConfirmPasswordTextBox.TabIndex = 21;
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
			// 
			// CountryDropDownMenu
			// 
			CountryDropDownMenu.FormattingEnabled = true;
			CountryDropDownMenu.Location = new Point(643, 281);
			CountryDropDownMenu.Name = "CountryDropDownMenu";
			CountryDropDownMenu.Size = new Size(151, 28);
			CountryDropDownMenu.TabIndex = 22;
			CountryDropDownMenu.Visible = false;
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
			// RegisterButton
			// 
			RegisterButton.BackColor = Color.FromArgb(0, 117, 214);
			RegisterButton.FlatAppearance.BorderColor = Color.Black;
			RegisterButton.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
			RegisterButton.ForeColor = Color.White;
			RegisterButton.Location = new Point(232, 461);
			RegisterButton.Name = "RegisterButton";
			RegisterButton.Size = new Size(355, 55);
			RegisterButton.TabIndex = 29;
			RegisterButton.Text = "REGISTER";
			RegisterButton.UseVisualStyleBackColor = false;
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
			// LogInLinkLabel
			// 
			LogInLinkLabel.AutoSize = true;
			LogInLinkLabel.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			LogInLinkLabel.Location = new Point(478, 550);
			LogInLinkLabel.Name = "LogInLinkLabel";
			LogInLinkLabel.Size = new Size(62, 25);
			LogInLinkLabel.TabIndex = 38;
			LogInLinkLabel.TabStop = true;
			LogInLinkLabel.Text = "Log In";
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
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Point, 0);
			label4.Location = new Point(298, 196);
			label4.Name = "label4";
			label4.Size = new Size(179, 25);
			label4.TabIndex = 40;
			label4.Text = "All fields are required";
			// 
			// SignupWindow
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(813, 584);
			Controls.Add(label4);
			Controls.Add(SuccessMessageLabel);
			Controls.Add(LogInLinkLabel);
			Controls.Add(label3);
			Controls.Add(pictureBox4);
			Controls.Add(CountryInstructionsLabel);
			Controls.Add(UsernameErrorMessage);
			Controls.Add(AdminPasswordError);
			Controls.Add(pictureBox2);
			Controls.Add(pictureBox1);
			Controls.Add(RegisterButton);
			Controls.Add(panel8);
			Controls.Add(panel7);
			Controls.Add(CountryUnderscore);
			Controls.Add(TownUnderScore);
			Controls.Add(panel4);
			Controls.Add(panel2);
			Controls.Add(panel1);
			Controls.Add(panel3);
			Controls.Add(AdminBox);
			Controls.Add(CountryDropDownMenu);
			Controls.Add(ConfirmPasswordTextBox);
			Controls.Add(label2);
			Controls.Add(AdminPasswordTextBox);
			Controls.Add(ContactInfoTextBox);
			Controls.Add(TownTextBox);
			Controls.Add(PasswordTextBox);
			Controls.Add(NameTextBox);
			Controls.Add(UsernameTextBox);
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
		private TextBox UsernameTextBox;
		private TextBox NameTextBox;
		private TextBox PasswordTextBox;
		private TextBox TownTextBox;
		private TextBox ContactInfoTextBox;
		private TextBox AdminPasswordTextBox;
		private TextBox ConfirmPasswordTextBox;
		private Label label2;
		private ComboBox CountryDropDownMenu;
		private CheckBox AdminBox;
		private Panel panel3;
		private Panel panel1;
		private Panel panel2;
		private Panel panel4;
		private Panel TownUnderScore;
		private Panel CountryUnderscore;
		private Panel panel7;
		private Panel panel8;
		private Button RegisterButton;
		private PictureBox pictureBox1;
		private PictureBox pictureBox2;
		private Label AdminPasswordError;
		private Label UsernameErrorMessage;
		private Label CountryInstructionsLabel;
		private PictureBox pictureBox4;
		private LinkLabel LogInLinkLabel;
		private Label label3;
		private Label SuccessMessageLabel;
		private Label label4;
	}
}