using BusinessLayer.Functions;
using BusinessLayer.Models;
using DataLayer;
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
    public partial class DeletePetForm : Form
    {
        private readonly User LoggedUser;

        public DeletePetForm(User loggedUser)
        {
            LoggedUser = loggedUser;
            InitializeComponent();
        }
        public void DeletePetForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonDeletePet_Click(object sender, EventArgs e)
        {
            string petName = textBoxPetName.Text.Trim();

            if (string.IsNullOrEmpty(petName))
            {
                labelMessage.Text = "Please enter a pet name.";
                return;
            }
            var pet = PetService.ReadAll().FirstOrDefault(p => p.Name == petName && p.UserId == LoggedUser.Id);
            if (pet != null)
            {
                PetService.Delete(petName, LoggedUser);
                labelMessage.Text = $"Pet '{petName}' has been deleted.";
            }
            else
            {
                labelMessage.Text = $"Pet '{petName}' not found.";
            }
        }
    }
}
