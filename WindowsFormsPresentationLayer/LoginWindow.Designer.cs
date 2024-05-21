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
			pictureBox2 = new PictureBox();
			pictureBox3 = new PictureBox();
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			button1 = new Button();
			label2 = new Label();
			linkLabel1 = new LinkLabel();
			textBox1 = new TextBox();
			textBox2 = new TextBox();
			panel1 = new Panel();
			panel2 = new Panel();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
			SuspendLayout();
			// 
			// pictureBox1
			// 
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new Point(303, 12);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(97, 78);
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			pictureBox1.Click += pictureBox1_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
			label1.Location = new Point(286, 108);
			label1.Name = "label1";
			label1.Size = new Size(139, 54);
			label1.TabIndex = 1;
			label1.Text = "LOGIN";
			label1.Click += label1_Click;
			// 
			// pictureBox2
			// 
			pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
			pictureBox2.Location = new Point(189, 168);
			pictureBox2.Name = "pictureBox2";
			pictureBox2.Size = new Size(76, 49);
			pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox2.TabIndex = 2;
			pictureBox2.TabStop = false;
			pictureBox2.Click += pictureBox2_Click;
			// 
			// pictureBox3
			// 
			pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
			pictureBox3.Location = new Point(189, 263);
			pictureBox3.Name = "pictureBox3";
			pictureBox3.Size = new Size(76, 47);
			pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox3.TabIndex = 3;
			pictureBox3.TabStop = false;
			// 
			// button1
			// 
			button1.Location = new Point(303, 335);
			button1.Name = "button1";
			button1.Size = new Size(94, 29);
			button1.TabIndex = 4;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(286, 388);
			label2.Name = "label2";
			label2.Size = new Size(50, 20);
			label2.TabIndex = 5;
			label2.Text = "label2";
			// 
			// linkLabel1
			// 
			linkLabel1.AutoSize = true;
			linkLabel1.Location = new Point(371, 388);
			linkLabel1.Name = "linkLabel1";
			linkLabel1.Size = new Size(76, 20);
			linkLabel1.TabIndex = 6;
			linkLabel1.TabStop = true;
			linkLabel1.Text = "linkLabel1";
			linkLabel1.LinkClicked += linkLabel1_LinkClicked;
			// 
			// textBox1
			// 
			textBox1.Location = new Point(271, 180);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(273, 27);
			textBox1.TabIndex = 7;
			// 
			// textBox2
			// 
			textBox2.Location = new Point(271, 283);
			textBox2.Name = "textBox2";
			textBox2.Size = new Size(273, 27);
			textBox2.TabIndex = 8;
			textBox2.TextChanged += textBox2_TextChanged;
			// 
			// panel1
			// 
			panel1.BackColor = Color.Black;
			panel1.Location = new Point(189, 219);
			panel1.Name = "panel1";
			panel1.Size = new Size(355, 1);
			panel1.TabIndex = 9;
			// 
			// panel2
			// 
			panel2.BackColor = Color.Black;
			panel2.Location = new Point(189, 316);
			panel2.Name = "panel2";
			panel2.Size = new Size(355, 1);
			panel2.TabIndex = 10;
			// 
			// LoginWindow
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(panel2);
			Controls.Add(panel1);
			Controls.Add(textBox2);
			Controls.Add(textBox1);
			Controls.Add(linkLabel1);
			Controls.Add(label2);
			Controls.Add(button1);
			Controls.Add(pictureBox3);
			Controls.Add(pictureBox2);
			Controls.Add(label1);
			Controls.Add(pictureBox1);
			Name = "LoginWindow";
			Text = "Log In";
			Load += LoginWindow_Load;
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private PictureBox pictureBox1;
		private Label label1;
		internal PictureBox pictureBox2;
		private PictureBox pictureBox3;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private Button button1;
		private Label label2;
		private LinkLabel linkLabel1;
		private TextBox textBox1;
		private TextBox textBox2;
		private Panel panel1;
		private Panel panel2;
	}
}
