using BusinessLayer.Functions;
using BusinessLayer.Models;
using BusinessLayer;
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
    public partial class PetDataUpdateForm : Form
    {
        private static User LoggedUser;
        public PetDataUpdateForm(User user)
        {
            InitializeComponent();
            LoggedUser = user;
        }
        private Label petNameLabel;
        private TextBox petNameTextBox;
        private Label ageLabel;
        private NumericUpDown ageNumericUpDown;
        private Label animalTypeLabel;
        private TextBox animalTypeTextBox;
        private Label includesCageLabel;
        private CheckBox includesCageCheckBox;
        private Label descriptionLabel;
        private TextBox descriptionTextBox;
        private Button updateButton;



        private void PetDataUpdateForm_Load(object sender, EventArgs e)
        {

        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            // Get data from form controls
            string petName = petNameTextBox.Text;
            int age = Convert.ToInt32(ageNumericUpDown.Value);
            string animalType = animalTypeTextBox.Text;
            bool includesCage = includesCageCheckBox.Checked;
            string description = descriptionTextBox.Text;

            if (!PetService.CheckPetExists(petName))
            {
                MessageBox.Show("Pet name doesn't match any saved pets!");
                return;
            }
            var oldPet = PetService.ReturnPetByname(petName);
            var updatedPet = new Pet(oldPet.Id, LoggedUser, petName, "photo path", age, animalType, description, includesCage);
            PetService.Update(updatedPet);
            // Show a message indicating success
            MessageBox.Show("Pet data updated successfully!");
        }
    }
}
