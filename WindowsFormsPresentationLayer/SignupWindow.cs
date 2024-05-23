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
			//UserService.LoadDb();
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
		/*
		private void SignupWindow_Shown(object sender, EventArgs e)
		{
			UserService.LoadDb();
		}
		*/
	}
}
