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
    public partial class PetAsPublicOfferRegistrationForm : Form
    {
        private List<Pet> userPets;
        private User LoggedUser;
        public PetAsPublicOfferRegistrationForm(User user)
        {
            InitializeComponent();
            userPets = PetService.ReturnAllPets(user);
            LoggedUser = user;
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string petName = petNameTextBox.Text.Trim();

            if (!userPets.Exists(x => x.Name == petName))
            {
                MessageBox.Show("No such pet registered in your profile! Try again.");
                return;
            }

            // Check if the pet is already registered as a public offer
            try
            {
                PublicOfferService.RegisterPet(petName, LoggedUser);
                MessageBox.Show("Pet registered as a Public Offer successfully!");
            }
            catch
            {
                MessageBox.Show("This pet is already registered as a Public Offer! Try again.");
            }

        }

        private void PetAsPublicOfferRegistrationForm_Load(object sender, EventArgs e)
        {


        }
    }
}
