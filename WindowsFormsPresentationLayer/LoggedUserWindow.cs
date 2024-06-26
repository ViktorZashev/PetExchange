﻿using BusinessLayer;
using BusinessLayer.Database_Functions;
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
            UsernameTextBox.Text = user.Name;
        }

        private void LoggedUserWindow_Load(object sender, EventArgs e)
        {
            if(LoggedUser.IsAdmin == true)
            {
                AdminTitle.Visible = true;
                TruncateButton.Visible = true;
                SeedDatabaseButton.Visible = true;
            }
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

        private void button4_Click(object sender, EventArgs e)
        {
            PetAsPublicOfferRegistrationForm obj = new PetAsPublicOfferRegistrationForm(LoggedUser);
            obj.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DeletePublicOfferForm obj = new DeletePublicOfferForm(LoggedUser);
            obj.Show();
        }

        private void button8_Click(object sender, EventArgs e) // Truncate Button
        {
            DatabaseFunctions.DeleteAllEntries();
            MessageBox.Show("Deletion of all database entries successful");
        }

        private void button7_Click(object sender, EventArgs e) // Seed With Default Values Button
        {
            DatabaseFunctions.SeedDatabase();
            MessageBox.Show("Deletion of all database entries successful");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var userRequests = UserRequestsService.ReadAll(LoggedUser);
            if (userRequests.Count == 0)
            {
                MessageBox.Show("No user made requests  exist!");
                return;
            }
            UserRequestsDisplayForm obj = new UserRequestsDisplayForm(LoggedUser);
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteUserRequestForm obj = new DeleteUserRequestForm(LoggedUser);
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateUserRequestForm obj = new CreateUserRequestForm(LoggedUser);
            obj.Show();
        }

        private void LogInLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginWindow obj = new LoginWindow();
            obj.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
