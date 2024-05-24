namespace WindowsFormsPresentationLayer.LoggedUser_Forms
{
    partial class DeletePetForm
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
            label1 = new Label();
            label2 = new Label();
            textBoxPetName = new TextBox();
            buttonDeletePet = new Button();
            labelMessage = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 10);
            label1.Name = "label1";
            label1.Size = new Size(109, 15);
            label1.TabIndex = 0;
            label1.Text = "Delete a Pet Record";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 38);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 1;
            label2.Text = "Pet Name:";
            // 
            // textBoxPetName
            // 
            textBoxPetName.Location = new Point(90, 35);
            textBoxPetName.Margin = new Padding(3, 2, 3, 2);
            textBoxPetName.Name = "textBoxPetName";
            textBoxPetName.Size = new Size(176, 23);
            textBoxPetName.TabIndex = 2;
            // 
            // buttonDeletePet
            // 
            buttonDeletePet.Location = new Point(90, 68);
            buttonDeletePet.Margin = new Padding(3, 2, 3, 2);
            buttonDeletePet.Name = "buttonDeletePet";
            buttonDeletePet.Size = new Size(175, 22);
            buttonDeletePet.TabIndex = 3;
            buttonDeletePet.Text = "Delete Pet";
            buttonDeletePet.UseVisualStyleBackColor = true;
            buttonDeletePet.Click += buttonDeletePet_Click;
            // 
            // labelMessage
            // 
            labelMessage.AutoSize = true;
            labelMessage.Location = new Point(11, 98);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(0, 15);
            labelMessage.TabIndex = 4;
            // 
            // DeletePetForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(282, 125);
            Controls.Add(labelMessage);
            Controls.Add(buttonDeletePet);
            Controls.Add(textBoxPetName);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "DeletePetForm";
            Text = "Delete Pet";
            Load += DeletePetForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPetName;
        private System.Windows.Forms.Button buttonDeletePet;
        private System.Windows.Forms.Label labelMessage;

        #endregion
    }
}