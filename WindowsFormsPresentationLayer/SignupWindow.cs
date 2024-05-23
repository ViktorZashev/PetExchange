using BusinessLayer.Database_Functions;
using BusinessLayer.Functions;
using BusinessLayer.Models;
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
		private static string Username;
		private static string Password;
		private static string Name;
		private static string TownName;
		private static string CountryName;
		private static string ContactInfo;
		private static bool isAdmin;
		public SignupWindow()
		{
			InitializeComponent();
		}
		private static void ResetVariables()
		{
			Username = "";
			Password = "";
			Name = "";
			TownName = "";
			CountryName = "";
			ContactInfo = "";
			isAdmin = false;
		}
		private void SignupWindow_Load(object sender, EventArgs e)
		{
			// Loading the countries in the dropdownList
			var countryNames = CountryService.ReadAll().Select(x => x.Name);
			foreach (string name in countryNames)
			{
				CountryDropDownMenu.Items.Add(name);
			}
			ResetVariables();
		}

		private void UsernameTextBox_Leave(object sender, EventArgs e)// Verifies that it is unique
		{
			UsernameErrorMessage.Visible = false;
			Cursor = Cursors.WaitCursor;
			var userName = UsernameTextBox.Text;
			if (userName == "")
			{
				UsernameErrorMessage.Text = "Username can't be empty";
				UsernameErrorMessage.ForeColor = Color.Red;
				UsernameErrorMessage.Visible = true;
				return;
			}
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
				Username = UsernameTextBox.Text;
			}
			Cursor = Cursors.Default;
		}
		private void ConfirmPasswordTextBox_Leave(object sender, EventArgs e)
		{
			if (PasswordTextBox.Text == ConfirmPasswordTextBox.Text)
			{
				PasswordsDifferentErrorMessage.Visible = false;
				Password = PasswordTextBox.Text;
			}
			if (PasswordTextBox.Text != ConfirmPasswordTextBox.Text)
			{
				PasswordsDifferentErrorMessage.Visible = true;
			}
		}

		private void TownTextBox_Leave(object sender, EventArgs e) // Completed the input of a country
		{
			var inputedTown = TownTextBox.Text;
			TownName = inputedTown;
			if (TownService.CheckIfExists(inputedTown)) // The town is already registered in system
			{
				CountryLabel.Visible = false;
				CountryUnderscore.Visible = false;
				CountryInstructionsLabel.Visible = false;
				CountryDropDownMenu.Visible = false;
				TownName = inputedTown;
				Town foundTown = TownService.RetrieveTown(inputedTown);
				Guid countryId = foundTown.CountryId;
				CountryName = CountryService.Read(countryId).Name;
			}
			else // The town is unregistered, enter country also
			{
				CountryLabel.Visible = true;
				CountryUnderscore.Visible = true;
				CountryInstructionsLabel.Visible = true;
				CountryDropDownMenu.Visible = true;
			}
		}
		private void CountryDropDownMenu_Leave(object sender, EventArgs e) // Triggers when the user enters and leaves the dropdown menu
		{
			CountryName = CountryDropDownMenu.Text;
		}

		private void AdminBox_CheckedChanged(object sender, EventArgs e)
		{
			if (AdminBox.Checked == true)
			{
				AdminPasswordTextBox.Text = "";
				AdminPasswordError.Visible = false;
				AdminPasswordLabel.Visible = true;
				AdminPasswordTextBox.Visible = true;
				AdminUnderscore.Visible = true;
			}
			else
			{
				AdminPasswordError.Visible = false;
				AdminPasswordLabel.Visible = false;
				AdminPasswordTextBox.Visible = false;
				AdminUnderscore.Visible = false;
				isAdmin = false;
			}
		}

		private void AdminPasswordTextBox_TextChanged(object sender, EventArgs e) // Admin password has been entered
		{
			var inputedText = AdminPasswordTextBox.Text;
			if (inputedText == DatabaseFunctions.adminPassword)
			{
				AdminPasswordError.Visible = false;
				isAdmin = true;
			}
			else
			{
				AdminPasswordError.Visible = true;
				isAdmin = false;
			}
		}

		private void ContactInfoTextBox_TextChanged(object sender, EventArgs e)
		{
			ContactInfo = ContactInfoTextBox.Text;
		}

		private void NameTextBox_TextChanged(object sender, EventArgs e)
		{
			Name = NameTextBox.Text;
		}

		private void RegisterButton_Click(object sender, EventArgs e)
		{
			SuccessMessageLabel.Visible = false;
			/*
			MessageBox.Show("Name " + Name +
			"Username " + Username +
			"Password " + Password +
			"Town Name " + TownName +
			"Country Name" + CountryName +
			"Contact Info " + ContactInfo +
			"Admin" + isAdmin);
			*/
			// Checking all the validation criteria are met
			if ((UsernameErrorMessage.ForeColor == Color.Green)
			&& PasswordsDifferentErrorMessage.Visible == false
			&& AdminPasswordError.Visible == false
			&& Name != ""
			&& Username != ""
			&& Password != ""
			&& TownName != ""
			&& CountryName != ""
			&& ContactInfo != ""
			)
			{
				try
				{
					if (CountryService.RetrieveCountry(CountryName) == null)
					{
						CountryService.Create(new Country(CountryName));
					}
					Town inputedTown = new Town();
					if (!TownService.CheckIfExists(TownName))
					{
						inputedTown.Country = CountryService.RetrieveCountry(CountryName);
						inputedTown.CountryId = inputedTown.Country.Id;
						inputedTown.Name = TownName;
						TownService.Create(inputedTown);
					}
					inputedTown = TownService.RetrieveTown(TownName);
					var newUser = new User(inputedTown, new List<Pet>(), Name, "photo_path", isAdmin, ContactInfo, Username, Password);
					UserService.Create(newUser);
					SuccessMessageLabel.Visible = true;
				}
				catch
				{
					MessageBox.Show("A system error occured when trying to register user data!");
				}
			}
			else
			{
				MessageBox.Show("Please fill in all fields!" + "\n" +
				"Resolve all errors!");

			}
		}

		private void LogInLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			GoToLogInPage();
		}
		private void GoToLogInPage()
		{
			LoginWindow obj = new LoginWindow();
			obj.Show();
			this.Hide();
		}
	}
}
