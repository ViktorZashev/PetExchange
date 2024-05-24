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
    public partial class RegisterPetForm : Form
    {
        private static User LoggedUser;
        public RegisterPetForm(User user)
        {
            InitializeComponent();
            LoggedUser = user;
        }

        private void RegisterPetForm_Load(object sender, EventArgs e)
        {

        }
        private void registerButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string animalType = animalTypeTextBox.Text;
            int age = (int)ageNumericUpDown.Value;
            bool includesCage = cageCheckBox.Checked;
            string description = descriptionTextBox.Text;

  

            // Creating a new Pet object with the provided values.
            var NewPet = new Pet
            {
                Name = name,
                AnimalType = animalType,
                Age = age,
                IncludesCage = includesCage,
                Description = description,
                User = LoggedUser,
                UserId = LoggedUser.Id
            };

            PetService.Create(NewPet);
            // Close the form and set the DialogResult to OK to indicate successful registration.
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
