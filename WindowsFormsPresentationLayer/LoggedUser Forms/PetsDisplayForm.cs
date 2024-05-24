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
    public partial class PetsDisplayForm : Form
    {
        private List<Pet> pets;
        private System.Windows.Forms.DataGridView dataGridView1;

        public PetsDisplayForm(List<Pet> pets)
        {
            InitializeComponent();
            this.pets = pets;
            LoadPets();
        }
        private void LoadPets()
        {
            dataGridView1.DataSource = pets;

            // Hide unwanted columns
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["UserId"].Visible = false;
            dataGridView1.Columns["User"].Visible = false;
            dataGridView1.Columns["PhotoPath"].Visible = false;
        }

        private void PetsDisplayForm_Load(object sender, EventArgs e)
        { 
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
