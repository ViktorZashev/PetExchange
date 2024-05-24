using BusinessLayer.Functions;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsPresentationLayer.LoggedUser_Forms
{
    public partial class PublicOfferDisplayForm : Form
    {
        private List<PublicOffer> _publicOffers;

        public PublicOfferDisplayForm(List<PublicOffer> publicOffers)
        {
            InitializeComponent();
            _publicOffers = publicOffers;

        }

        private void PublicOfferDisplayForm_Load(object sender, EventArgs e)
        {
            if (_publicOffers.Count == 0)
            {
                MessageBox.Show("No public offers exist yet in your town!");
                this.Close();
            }
            SetupDataGridView();
            DisplayPublicOffers();
        }

        private void SetupDataGridView()
        {
            // Add columns to the DataGridView
            publicOfferDataGridView.ColumnCount = 7;
            publicOfferDataGridView.Columns[0].Name = "Pet Name";
            publicOfferDataGridView.Columns[1].Name = "Pet Age";
            publicOfferDataGridView.Columns[2].Name = "Animal Type";
            publicOfferDataGridView.Columns[3].Name = "Includes Cage";
            publicOfferDataGridView.Columns[4].Name = "Description";
            publicOfferDataGridView.Columns[5].Name = "Town Name";
        }

        private void DisplayPublicOffers()
        {
            foreach (var offer in _publicOffers)
            {
                // Add a new row to the DataGridView
                publicOfferDataGridView.Rows.Add(
                    offer.Pet.Name,
                    offer.Pet.Age,
                    offer.Pet.AnimalType,
                    offer.Pet.IncludesCage ? "Yes" : "No",
                    offer.Pet.Description,
                    TownService.Read(offer.TownId).Name
                );
            }
        }

        private void publicOfferDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
