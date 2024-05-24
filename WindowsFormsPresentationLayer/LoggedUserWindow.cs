using BusinessLayer;
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
using WindowsFormsPresentationLayer.LoggedUser_Forms;

namespace WindowsFormsPresentationLayer
{
    public partial class LoggedUserWindow : Form
    {
        private static User LoggedUser;

        public LoggedUserWindow(User user)
        {
            InitializeComponent();
            if (user == null)
            {
                throw new NullReferenceException("Logged User");
            }
            LoggedUser = user;
        }

        private void LoggedUserWindow_Load(object sender, EventArgs e)
        {

        }
        private void ShowAllPetsButton_Click(object sender, EventArgs e)
        {
            var pets = PetService.ReturnAllPets(LoggedUser);
            if (pets.Count == 0)
            {
                MessageBox.Show("No pets exist!");
                return; // Exit the method if there are no pets
            }
            PetsDisplayForm obj = new PetsDisplayForm(pets);
            obj.Show();
            //this.Hide();
        }

        private void RegisterNewPetButton_Click(object sender, EventArgs e)
        {
            RegisterPetForm obj = new RegisterPetForm(LoggedUser);
            obj.Show();
        }

        private void DeletePetButton_Click(object sender, EventArgs e)
        {
            DeletePetForm obj = new DeletePetForm(LoggedUser);
            obj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PetDataUpdateForm obj = new PetDataUpdateForm(LoggedUser);
            obj.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var town = TownService.Read(LoggedUser.TownId);
            var availableOffers = PublicOfferService.ReadAll().Where(x => x.TownId == town.Id).ToList();
            PublicOfferDisplayForm obj = new PublicOfferDisplayForm(availableOffers);
            obj.Show();
        }
    }
}
