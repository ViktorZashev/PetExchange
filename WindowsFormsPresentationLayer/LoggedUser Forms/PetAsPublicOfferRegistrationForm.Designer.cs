namespace WindowsFormsPresentationLayer.LoggedUser_Forms
{
    partial class PetAsPublicOfferRegistrationForm
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
            registerButton = new Button();
            SuspendLayout();
            // 
            // petNameLabel
            // 
            petNameLabel.AutoSize = true;
            petNameLabel.Location = new Point(35, 40);
            petNameLabel.Name = "petNameLabel";
            petNameLabel.Size = new Size(62, 15);
            petNameLabel.TabIndex = 0;
            petNameLabel.Text = "Pet Name:";
            // 
            // petNameTextBox
            // 
            petNameTextBox.Location = new Point(120, 37);
            petNameTextBox.Name = "petNameTextBox";
            petNameTextBox.Size = new Size(150, 23);
            petNameTextBox.TabIndex = 1;
            // 
            // registerButton
            // 
            registerButton.Location = new Point(120, 80);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(100, 30);
            registerButton.TabIndex = 2;
            registerButton.Text = "Register";
            registerButton.UseVisualStyleBackColor = true;
            registerButton.Click += registerButton_Click;
            // 
            // PetAsPublicOfferRegistrationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(300, 150);
            Controls.Add(registerButton);
            Controls.Add(petNameTextBox);
            Controls.Add(petNameLabel);
            Name = "PetAsPublicOfferRegistrationForm";
            Text = "Pet Registration Form";
            Load += PetAsPublicOfferRegistrationForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label petNameLabel;
        private System.Windows.Forms.TextBox petNameTextBox;
        private System.Windows.Forms.Button registerButton;
        #endregion
    }
}