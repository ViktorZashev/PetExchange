namespace WindowsFormsPresentationLayer
{
	partial class LoginWindow
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginWindow));
			pictureBox1 = new PictureBox();
			label1 = new Label();
			pictureBox3 = new PictureBox();
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			LoginButton = new Button();
			label2 = new Label();
			SignUpLinkLabel = new LinkLabel();
			PasswordTextBox = new TextBox();
			panel2 = new Panel();
			pictureBox4 = new PictureBox();
			UsernameTestBox = new TextBox();
			panel3 = new Panel();
			UsernameErrorBox = new Label();
			PasswordErrorBox = new Label();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
			SuspendLayout();
			// 
			// pictureBox1
			// 
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new Point(274, 10);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(168, 133);
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 25.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			label1.ForeColor = Color.Black;
			label1.Location = new Point(281, 146);
			label1.Name = "label1";
			label1.Size = new Size(161, 60);
			label1.TabIndex = 1;
			label1.Text = "LOGIN";
			// 
			// pictureBox3
			// 
			pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
			pictureBox3.Location = new Point(192, 276);
			pictureBox3.Name = "pictureBox3";
			pictureBox3.Size = new Size(76, 47);
			pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox3.TabIndex = 3;
			pictureBox3.TabStop = false;
			// 
			// LoginButton
			// 
			LoginButton.BackColor = Color.FromArgb(0, 117, 214);
			LoginButton.FlatAppearance.BorderColor = Color.Black;
			LoginButton.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
			LoginButton.ForeColor = Color.White;
			LoginButton.Location = new Point(192, 349);
			LoginButton.Name = "LoginButton";
			LoginButton.Size = new Size(355, 55);
			LoginButton.TabIndex = 4;
			LoginButton.Text = "LOGIN";
			LoginButton.UseVisualStyleBackColor = false;
			LoginButton.Click += LoginButton_Click;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
			label2.Location = new Point(192, 416);
			label2.Name = "label2";
			label2.Size = new Size(198, 25);
			label2.TabIndex = 5;
			label2.Text = "Don't have an account?";
			// 
			// SignUpLinkLabel
			// 
			SignUpLinkLabel.AutoSize = true;
			SignUpLinkLabel.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			SignUpLinkLabel.Location = new Point(438, 416);
			SignUpLinkLabel.Name = "SignUpLinkLabel";
			SignUpLinkLabel.Size = new Size(75, 25);
			SignUpLinkLabel.TabIndex = 6;
			SignUpLinkLabel.TabStop = true;
			SignUpLinkLabel.Text = "Sign Up";
			SignUpLinkLabel.LinkClicked += SignUpLinkLabel_LinkClicked;
			// 
			// PasswordTextBox
			// 
			PasswordTextBox.Location = new Point(274, 296);
			PasswordTextBox.Name = "PasswordTextBox";
			PasswordTextBox.PasswordChar = '*';
			PasswordTextBox.PlaceholderText = "Enter password";
			PasswordTextBox.Size = new Size(273, 27);
			PasswordTextBox.TabIndex = 8;
			// 
			// panel2
			// 
			panel2.BackColor = Color.Black;
			panel2.Location = new Point(192, 329);
			panel2.Name = "panel2";
			panel2.Size = new Size(355, 1);
			panel2.TabIndex = 10;
			// 
			// pictureBox4
			// 
			pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
			pictureBox4.Location = new Point(192, 208);
			pictureBox4.Name = "pictureBox4";
			pictureBox4.Size = new Size(76, 49);
			pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox4.TabIndex = 2;
			pictureBox4.TabStop = false;
			// 
			// UsernameTestBox
			// 
			UsernameTestBox.Location = new Point(274, 224);
			UsernameTestBox.Name = "UsernameTestBox";
			UsernameTestBox.PlaceholderText = "Enter username";
			UsernameTestBox.Size = new Size(273, 27);
			UsernameTestBox.TabIndex = 7;
			// 
			// panel3
			// 
			panel3.BackColor = Color.Black;
			panel3.Location = new Point(192, 263);
			panel3.Name = "panel3";
			panel3.Size = new Size(355, 1);
			panel3.TabIndex = 9;
			// 
			// UsernameErrorBox
			// 
			UsernameErrorBox.AutoSize = true;
			UsernameErrorBox.ForeColor = Color.Red;
			UsernameErrorBox.Location = new Point(553, 227);
			UsernameErrorBox.Name = "UsernameErrorBox";
			UsernameErrorBox.Size = new Size(41, 20);
			UsernameErrorBox.TabIndex = 11;
			UsernameErrorBox.Text = "Error";
			// 
			// PasswordErrorBox
			// 
			PasswordErrorBox.AutoSize = true;
			PasswordErrorBox.ForeColor = Color.Red;
			PasswordErrorBox.Location = new Point(553, 303);
			PasswordErrorBox.Name = "PasswordErrorBox";
			PasswordErrorBox.Size = new Size(41, 20);
			PasswordErrorBox.TabIndex = 12;
			PasswordErrorBox.Text = "Error";
			// 
			// LoginWindow
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(PasswordErrorBox);
			Controls.Add(UsernameErrorBox);
			Controls.Add(panel2);
			Controls.Add(panel3);
			Controls.Add(PasswordTextBox);
			Controls.Add(UsernameTestBox);
			Controls.Add(SignUpLinkLabel);
			Controls.Add(label2);
			Controls.Add(LoginButton);
			Controls.Add(pictureBox4);
			Controls.Add(pictureBox3);
			Controls.Add(label1);
			Controls.Add(pictureBox1);
			KeyPreview = true;
			Name = "LoginWindow";
			Text = "Log In";
			Load += LoginWindow_Load;
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private PictureBox pictureBox1;
		private Label label1;
		private PictureBox pictureBox3;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private Button LoginButton;
		private Label label2;
		private LinkLabel SignUpLinkLabel;
		private TextBox PasswordTextBox;
		private Panel panel2;
		internal PictureBox pictureBox4;
		private TextBox UsernameTestBox;
		private Panel panel3;
		private Label UsernameErrorBox;
		private Label PasswordErrorBox;
	}
}
