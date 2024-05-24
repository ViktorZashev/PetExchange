using BusinessLayer.Models;

namespace WindowsFormsPresentationLayer.LoggedUser_Forms
{
    partial class RegisterPetForm
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
            nameLabel = new Label();
            nameTextBox = new TextBox();
            animalTypeLabel = new Label();
            animalTypeTextBox = new TextBox();
            ageLabel = new Label();
            ageNumericUpDown = new NumericUpDown();
            cageLabel = new Label();
            cageCheckBox = new CheckBox();
            descriptionLabel = new Label();
            descriptionTextBox = new TextBox();
            registerButton = new Button();
            ((System.ComponentModel.ISupportInitialize)ageNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(10, 14);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(42, 15);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Name:";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(131, 11);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(219, 23);
            nameTextBox.TabIndex = 1;
            // 
            // animalTypeLabel
            // 
            animalTypeLabel.AutoSize = true;
            animalTypeLabel.Location = new Point(10, 44);
            animalTypeLabel.Name = "animalTypeLabel";
            animalTypeLabel.Size = new Size(75, 15);
            animalTypeLabel.TabIndex = 2;
            animalTypeLabel.Text = "Animal Type:";
            // 
            // animalTypeTextBox
            // 
            animalTypeTextBox.Location = new Point(131, 41);
            animalTypeTextBox.Name = "animalTypeTextBox";
            animalTypeTextBox.Size = new Size(219, 23);
            animalTypeTextBox.TabIndex = 3;
            // 
            // ageLabel
            // 
            ageLabel.AutoSize = true;
            ageLabel.Location = new Point(10, 74);
            ageLabel.Name = "ageLabel";
            ageLabel.Size = new Size(31, 15);
            ageLabel.TabIndex = 4;
            ageLabel.Text = "Age:";
            // 
            // ageNumericUpDown
            // 
            ageNumericUpDown.Location = new Point(131, 72);
            ageNumericUpDown.Name = "ageNumericUpDown";
            ageNumericUpDown.Size = new Size(219, 23);
            ageNumericUpDown.TabIndex = 5;
            // 
            // cageLabel
            // 
            cageLabel.AutoSize = true;
            cageLabel.Location = new Point(10, 104);
            cageLabel.Name = "cageLabel";
            cageLabel.Size = new Size(103, 15);
            cageLabel.TabIndex = 6;
            cageLabel.Text = "Comes with Cage:";
            // 
            // cageCheckBox
            // 
            cageCheckBox.AutoSize = true;
            cageCheckBox.Location = new Point(131, 103);
            cageCheckBox.Name = "cageCheckBox";
            cageCheckBox.Size = new Size(15, 14);
            cageCheckBox.TabIndex = 7;
            cageCheckBox.UseVisualStyleBackColor = true;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(10, 134);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(70, 15);
            descriptionLabel.TabIndex = 8;
            descriptionLabel.Text = "Description:";
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Location = new Point(131, 131);
            descriptionTextBox.Multiline = true;
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.Size = new Size(219, 56);
            descriptionTextBox.TabIndex = 9;
            // 
            // registerButton
            // 
            registerButton.Location = new Point(131, 202);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(219, 22);
            registerButton.TabIndex = 10;
            registerButton.Text = "Register Pet";
            registerButton.UseVisualStyleBackColor = true;
            registerButton.Click += registerButton_Click;
            // 
            // RegisterPetForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(360, 235);
            Controls.Add(registerButton);
            Controls.Add(descriptionTextBox);
            Controls.Add(descriptionLabel);
            Controls.Add(cageCheckBox);
            Controls.Add(cageLabel);
            Controls.Add(ageNumericUpDown);
            Controls.Add(ageLabel);
            Controls.Add(animalTypeTextBox);
            Controls.Add(animalTypeLabel);
            Controls.Add(nameTextBox);
            Controls.Add(nameLabel);
            Name = "RegisterPetForm";
            Text = "Register Pet";
            Load += RegisterPetForm_Load;
            ((System.ComponentModel.ISupportInitialize)ageNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
      

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label animalTypeLabel;
        private System.Windows.Forms.TextBox animalTypeTextBox;
        private System.Windows.Forms.Label ageLabel;
        private System.Windows.Forms.NumericUpDown ageNumericUpDown;
        private System.Windows.Forms.Label cageLabel;
        private System.Windows.Forms.CheckBox cageCheckBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Button registerButton;
    }
}