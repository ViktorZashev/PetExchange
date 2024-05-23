using BusinessLayer.Functions;
using DataLayer;
using DataLayer.ProjectDbContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsPresentationLayer
{
	public partial class SignupWindow : Form
	{
		public SignupWindow()
		{
			InitializeComponent();
		}

		private void SignupWindow_Load(object sender, EventArgs e)
		{
			// Loading the countries in the dropdownList
			var countryNames = CountryService.ReadAll().Select(x => x.Name);
			foreach (string name in countryNames)
			{
				CountryDropDownMenu.Items.Add(name);
			}
		}

		private void UsernameTextBox_Leave(object sender, EventArgs e)// Verifies that it is unique
		{
			UsernameErrorMessage.Visible = false;
			Cursor = Cursors.WaitCursor;
			var userName = UsernameTextBox.Text;
			var code = UserService.AuthenticateUserReturnsCode(userName, "");
			if (code != 0) // Such username is found
			{
				UsernameErrorMessage.Text = "Username already exists";
				UsernameErrorMessage.ForeColor = Color.Red;
				UsernameErrorMessage.Visible = true;
			}
			else // Valid Username
			{
				UsernameErrorMessage.Text = "Valid username";
				UsernameErrorMessage.ForeColor = Color.Green;
				UsernameErrorMessage.Visible = true;
			}
			Cursor = Cursors.Default;
		}
		private void ConfirmPasswordTextBox_Leave(object sender, EventArgs e)
		{
			if (PasswordTextBox.Text == ConfirmPasswordTextBox.Text)
			{
				PasswordsDifferentErrorMessage.Visible = false;
			}
			if (PasswordTextBox.Text != ConfirmPasswordTextBox.Text)
			{
				PasswordsDifferentErrorMessage.Visible = true;
			}
		}

		private void TownTextBox_Leave(object sender, EventArgs e) // Completed the input of a country
		{
			var inputedTown = TownTextBox.Text;
			if (TownService.CheckIfExists(inputedTown)) // The town is already registered in system
			{
				CountryLabel.Visible = false;
				CountryUnderscore.Visible = false;
				CountryInstructionsLabel.Visible = false;
				CountryDropDownMenu.Visible = false;
			}
			else // The town is unregistered, enter country also
			{
				CountryLabel.Visible = true;
				CountryUnderscore.Visible = true;
				CountryInstructionsLabel.Visible = true;
				CountryDropDownMenu.Visible = true;
			}
		}
	}
}
