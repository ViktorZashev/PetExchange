namespace WindowsFormsPresentationLayer.LoggedUser_Forms
{
    partial class PetDataUpdateForm
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
            petNameLabel = new Label();
            petNameTextBox = new TextBox();
            ageLabel = new Label();
            ageNumericUpDown = new NumericUpDown();
            animalTypeLabel = new Label();
            animalTypeTextBox = new TextBox();
            includesCageLabel = new Label();
            includesCageCheckBox = new CheckBox();
            descriptionLabel = new Label();
            descriptionTextBox = new TextBox();
            updateButton = new Button();
            ((System.ComponentModel.ISupportInitialize)ageNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // petNameLabel
            // 
            petNameLabel.AutoSize = true;
            petNameLabel.Location = new Point(10, 14);
            petNameLabel.Name = "petNameLabel";
            petNameLabel.Size = new Size(62, 15);
            petNameLabel.TabIndex = 0;
            petNameLabel.Text = "Pet Name:";
            // 
            // petNameTextBox
            // 
            petNameTextBox.Location = new Point(92, 11);
            petNameTextBox.Name = "petNameTextBox";
            petNameTextBox.Size = new Size(176, 23);
            petNameTextBox.TabIndex = 1;
            // 
            // ageLabel
            // 
            ageLabel.AutoSize = true;
            ageLabel.Location = new Point(10, 46);
            ageLabel.Name = "ageLabel";
            ageLabel.Size = new Size(31, 15);
            ageLabel.TabIndex = 2;
            ageLabel.Text = "Age:";
            // 
            // ageNumericUpDown
            // 
            ageNumericUpDown.Location = new Point(92, 44);
            ageNumericUpDown.Name = "ageNumericUpDown";
            ageNumericUpDown.Size = new Size(61, 23);
            ageNumericUpDown.TabIndex = 3;
            // 
            // animalTypeLabel
            // 
            animalTypeLabel.AutoSize = true;
            animalTypeLabel.Location = new Point(10, 81);
            animalTypeLabel.Name = "animalTypeLabel";
            animalTypeLabel.Size = new Size(75, 15);
            animalTypeLabel.TabIndex = 4;
            animalTypeLabel.Text = "Animal Type:";
            // 
            // animalTypeTextBox
            // 
            animalTypeTextBox.Location = new Point(92, 78);
            animalTypeTextBox.Name = "animalTypeTextBox";
            animalTypeTextBox.Size = new Size(176, 23);
            animalTypeTextBox.TabIndex = 5;
            // 
            // includesCageLabel
            // 
            includesCageLabel.AutoSize = true;
            includesCageLabel.Location = new Point(10, 115);
            includesCageLabel.Name = "includesCageLabel";
            includesCageLabel.Size = new Size(84, 15);
            includesCageLabel.TabIndex = 6;
            includesCageLabel.Text = "Includes Cage:";
            // 
            // includesCageCheckBox
            // 
            includesCageCheckBox.AutoSize = true;
            includesCageCheckBox.Location = new Point(102, 115);
            includesCageCheckBox.Name = "includesCageCheckBox";
            includesCageCheckBox.Size = new Size(15, 14);
            includesCageCheckBox.TabIndex = 7;
            includesCageCheckBox.UseVisualStyleBackColor = true;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(10, 152);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(70, 15);
            descriptionLabel.TabIndex = 8;
            descriptionLabel.Text = "Description:";
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Location = new Point(92, 149);
            descriptionTextBox.Multiline = true;
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.Size = new Size(176, 94);
            descriptionTextBox.TabIndex = 9;
            // 
            // updateButton
            // 
            updateButton.Location = new Point(92, 259);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(66, 28);
            updateButton.TabIndex = 10;
            updateButton.Text = "Update";
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += updateButton_Click;
            // 
            // PetDataUpdateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(290, 301);
            Controls.Add(updateButton);
            Controls.Add(descriptionTextBox);
            Controls.Add(descriptionLabel);
            Controls.Add(includesCageCheckBox);
            Controls.Add(includesCageLabel);
            Controls.Add(animalTypeTextBox);
            Controls.Add(animalTypeLabel);
            Controls.Add(ageNumericUpDown);
            Controls.Add(ageLabel);
            Controls.Add(petNameTextBox);
            Controls.Add(petNameLabel);
            Name = "PetDataUpdateForm";
            Text = "Update Pet Data";
            Load += PetDataUpdateForm_Load;
            ((System.ComponentModel.ISupportInitialize)ageNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
    }
}