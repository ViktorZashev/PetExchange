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
    public partial class UserRequestsDisplayForm : Form
    {
        private User user;
        private List<UserRequests> userRequests;
        private List<PublicOffer> publicOffers;
        public UserRequestsDisplayForm(User user)
        {
            InitializeComponent();
            this.user = user;
            userRequests = GetUserRequestsForUser(user);
            publicOffers = PublicOfferService.ReadAll().Where(x => x.TownId == user.TownId).ToList();
        }

        private void UserRequestsDisplayForm_Load(object sender, EventArgs e)
        {
            LoadUserRequests();
        }

        private void LoadUserRequests()
        {

            // Prepare data to display in the DataGridView
            var dataToDisplay = userRequests.Select(ur => new
            {
                PetName = publicOffers.FirstOrDefault(po => po.Id == ur.PublicOfferId)?.Pet.Name,
                PetType = publicOffers.FirstOrDefault(po => po.Id == ur.PublicOfferId)?.Pet.AnimalType,
                ur.IsAccepted
            }).ToList();

            dataGridView1.DataSource = dataToDisplay;

            // Set column headers
            dataGridView1.Columns["PetName"].HeaderText = "Pet Name";
            dataGridView1.Columns["PetType"].HeaderText = "Pet Type";
            dataGridView1.Columns["IsAccepted"].HeaderText = "Is Accepted";
        }

        private List<UserRequests> GetUserRequestsForUser(User user)
        {
            return UserRequestsService.ReadAll(user);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
