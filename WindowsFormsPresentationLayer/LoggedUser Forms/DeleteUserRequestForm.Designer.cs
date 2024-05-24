namespace WindowsFormsPresentationLayer.LoggedUser_Forms
{
    partial class DeleteUserRequestForm
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
        /// 
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox petNameTextBox;
        private System.Windows.Forms.Label petNameLabel;
        private System.Windows.Forms.Button deleteButton;
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.petNameTextBox = new System.Windows.Forms.TextBox();
            this.petNameLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(800, 300);
            this.dataGridView1.TabIndex = 0;
            // 
            // petNameTextBox
            // 
            this.petNameTextBox.Location = new System.Drawing.Point(150, 320);
            this.petNameTextBox.Name = "petNameTextBox";
            this.petNameTextBox.Size = new System.Drawing.Size(200, 20);
            this.petNameTextBox.TabIndex = 1;
            // 
            // petNameLabel
            // 
            this.petNameLabel.AutoSize = true;
            this.petNameLabel.Location = new System.Drawing.Point(50, 323);
            this.petNameLabel.Name = "petNameLabel";
            this.petNameLabel.Size = new System.Drawing.Size(57, 13);
            this.petNameLabel.TabIndex = 2;
            this.petNameLabel.Text = "Pet Name:";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(370, 318);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // DeleteUserRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.petNameLabel);
            this.Controls.Add(this.petNameTextBox);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DeleteUserRequestForm";
            this.Text = "Delete User Request";
            this.Load += new System.EventHandler(this.DeleteUserRequestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}