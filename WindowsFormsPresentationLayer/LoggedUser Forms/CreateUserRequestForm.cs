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
    public partial class CreateUserRequestForm : Form
    {
        private List<PublicOffer> publicOffers;
        private User currentUser;

        public CreateUserRequestForm(User user)
        {
            InitializeComponent();
            publicOffers = PublicOfferService.ReadAll().Where(x => x.TownId == user.TownId).ToList();
            currentUser = user;
        }

        private void CreateUserRequestForm_Load(object sender, EventArgs e)
        {
            LoadPublicOffers();
        }

        private void LoadPublicOffers()
        {
            // Display all available public offers in the DataGridView
            var dataToDisplay = publicOffers.Where(x => x.UserId != currentUser.Id).Select(po => new
            {
                po.Pet.Name,
                po.Pet.AnimalType,
                po.Pet.Age,
                po.Pet.Description
            }).ToList();

            dataGridView1.DataSource = dataToDisplay;
            dataGridView1.Columns["Name"].HeaderText = "Pet Name";
            dataGridView1.Columns["AnimalType"].HeaderText = "Animal Type";
            dataGridView1.Columns["Age"].HeaderText = "Age";
            dataGridView1.Columns["Description"].HeaderText = "Description";
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var petName = selectedRow.Cells["Name"].Value.ToString();
                var selectedOffer = publicOffers.FirstOrDefault(po => po.Pet.Name == petName);
                if (selectedOffer != null)
                {
                    var newUserRequest = new UserRequests
                    {
                        PublicOfferId = selectedOffer.Id,
                        UserId = currentUser.Id,
                        IsAccepted = false
                    };

                    try
                    {
                        UserRequestsService.CreateRequest(currentUser, petName);
                        MessageBox.Show($"User request for pet '{petName}' has been successfully created.");
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show($"User request for pet '{petName}' already exists.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a public offer to create a request.");
            }
        }
    }
}
