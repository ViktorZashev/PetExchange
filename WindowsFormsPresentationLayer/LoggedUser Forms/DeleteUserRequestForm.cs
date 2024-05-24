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
    public partial class DeleteUserRequestForm : Form
    {
        private List<UserRequests> userRequests;
        private List<PublicOffer> publicOffers;
        private User LoggedUser;

        public DeleteUserRequestForm(User users)
        {
            InitializeComponent();
            LoggedUser = users;
            userRequests = UserRequestsService.ReadAll(LoggedUser);
            publicOffers = PublicOfferService.ReadAll().Where(x => x.TownId == LoggedUser.TownId).ToList();
        }

        private void DeleteUserRequestForm_Load(object sender, EventArgs e)
        {
            LoadUserRequests();
        }

        private void LoadUserRequests()
        {
            // Display all user requests in the DataGridView
            var dataToDisplay = userRequests.Select(ur => new
            {
                PetName = publicOffers.FirstOrDefault(po => po.Id == ur.PublicOfferId)?.Pet.Name,
                PetType = publicOffers.FirstOrDefault(po => po.Id == ur.PublicOfferId)?.Pet.AnimalType,
                ur.IsAccepted
            }).ToList();

            dataGridView1.DataSource = dataToDisplay;
            dataGridView1.Columns["PetName"].HeaderText = "Pet Name";
            dataGridView1.Columns["PetType"].HeaderText = "Pet Type";
            dataGridView1.Columns["IsAccepted"].HeaderText = "Is Accepted";
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string petName = petNameTextBox.Text;
            var requestToDelete = userRequests.FirstOrDefault(ur => publicOffers.FirstOrDefault(po => po.Id == ur.PublicOfferId)?.Pet.Name == petName);

            if (requestToDelete != null)
            {
                userRequests.Remove(requestToDelete);
                UserRequestsService.DeleteRequest(LoggedUser, petName);
                MessageBox.Show($"User request for pet '{petName}' has been successfully deleted.");
                LoadUserRequests(); // Refresh the data grid view
            }
            else
            {
                MessageBox.Show($"No user request found for pet '{petName}'.");
            }
        }
    }
}
