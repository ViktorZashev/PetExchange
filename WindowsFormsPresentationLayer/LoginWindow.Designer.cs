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
			button1 = new Button();
			label2 = new Label();
			linkLabel1 = new LinkLabel();
			textBox2 = new TextBox();
			panel2 = new Panel();
			pictureBox4 = new PictureBox();
			textBox3 = new TextBox();
			panel3 = new Panel();
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
			pictureBox1.Click += pictureBox1_Click;
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
			label1.Click += label1_Click;
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
			// button1
			// 
			button1.BackColor = Color.FromArgb(0, 117, 214);
			button1.FlatAppearance.BorderColor = Color.Black;
			button1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
			button1.ForeColor = Color.White;
			button1.Location = new Point(192, 349);
			button1.Name = "button1";
			button1.Size = new Size(355, 55);
			button1.TabIndex = 4;
			button1.Text = "LOGIN";
			button1.UseVisualStyleBackColor = false;
			button1.Click += button1_Click;
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
			label2.Click += label2_Click;
			// 
			// linkLabel1
			// 
			linkLabel1.AutoSize = true;
			linkLabel1.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			linkLabel1.Location = new Point(438, 416);
			linkLabel1.Name = "linkLabel1";
			linkLabel1.Size = new Size(75, 25);
			linkLabel1.TabIndex = 6;
			linkLabel1.TabStop = true;
			linkLabel1.Text = "Sign Up";
			linkLabel1.LinkClicked += linkLabel1_LinkClicked;
			// 
			// textBox2
			// 
			textBox2.Location = new Point(274, 296);
			textBox2.Name = "textBox2";
			textBox2.Size = new Size(273, 27);
			textBox2.TabIndex = 8;
			textBox2.TextChanged += textBox2_TextChanged;
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
			pictureBox4.Click += pictureBox2_Click;
			// 
			// textBox3
			// 
			textBox3.Location = new Point(274, 224);
			textBox3.Name = "textBox3";
			textBox3.Size = new Size(273, 27);
			textBox3.TabIndex = 7;
			// 
			// panel3
			// 
			panel3.BackColor = Color.Black;
			panel3.Location = new Point(192, 263);
			panel3.Name = "panel3";
			panel3.Size = new Size(355, 1);
			panel3.TabIndex = 9;
			// 
			// LoginWindow
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(panel2);
			Controls.Add(panel3);
			Controls.Add(textBox2);
			Controls.Add(textBox3);
			Controls.Add(linkLabel1);
			Controls.Add(label2);
			Controls.Add(button1);
			Controls.Add(pictureBox4);
			Controls.Add(pictureBox3);
			Controls.Add(label1);
			Controls.Add(pictureBox1);
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
		private Button button1;
		private Label label2;
		private LinkLabel linkLabel1;
		private TextBox textBox2;
		private Panel panel2;
		internal PictureBox pictureBox4;
		private TextBox textBox3;
		private Panel panel3;
	}
}
