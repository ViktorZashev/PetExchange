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
    public partial class DeletePublicOfferForm : Form
    {
        private List<PublicOffer> publicOffers;
        private User currentUser;

        public DeletePublicOfferForm(User currentUser)
        {
            InitializeComponent();
            this.publicOffers = PublicOfferService.ReadAll().ToList();
            this.currentUser = currentUser;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string petName = petNameTextBox.Text.Trim();
            // Check if the pet is registered as a public offer
            var foundOffer = publicOffers.Find(offer => offer.Pet.Name == petName);

            if (foundOffer == null)
            {
                MessageBox.Show("No such pet is registered as a public offer!");
                return;
            }

            // Check if the pet is registered to the current user
            if (foundOffer.UserId != currentUser.Id)
            {
                MessageBox.Show("This pet is not registered to you!");
                return;
            }

            // Delete the public offer
            try
            {
                PublicOfferService.DeleteByPetName(petName, currentUser);
                MessageBox.Show("Public offer deleted successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
