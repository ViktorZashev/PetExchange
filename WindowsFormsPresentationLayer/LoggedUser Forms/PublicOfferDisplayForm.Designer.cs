namespace WindowsFormsPresentationLayer.LoggedUser_Forms
{
    partial class PublicOfferDisplayForm
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
            publicOfferDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)publicOfferDataGridView).BeginInit();
            SuspendLayout();
            // 
            // publicOfferDataGridView
            // 
            publicOfferDataGridView.AllowUserToAddRows = false;
            publicOfferDataGridView.AllowUserToDeleteRows = false;
            publicOfferDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            publicOfferDataGridView.Dock = DockStyle.Fill;
            publicOfferDataGridView.Location = new Point(0, 0);
            publicOfferDataGridView.Name = "publicOfferDataGridView";
            publicOfferDataGridView.ReadOnly = true;
            publicOfferDataGridView.RowHeadersVisible = false;
            publicOfferDataGridView.RowTemplate.Height = 24;
            publicOfferDataGridView.Size = new Size(511, 330);
            publicOfferDataGridView.TabIndex = 0;
            publicOfferDataGridView.CellContentClick += publicOfferDataGridView_CellContentClick;
            // 
            // PublicOfferDisplayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(511, 330);
            Controls.Add(publicOfferDataGridView);
            Name = "PublicOfferDisplayForm";
            Text = "Public Offer Display";
            Load += PublicOfferDisplayForm_Load;
            ((System.ComponentModel.ISupportInitialize)publicOfferDataGridView).EndInit();
            ResumeLayout(false);
        }

        private DataGridView publicOfferDataGridView;

        #endregion
    }
}