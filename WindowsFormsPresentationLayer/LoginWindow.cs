using BusinessLayer.Functions;
using BusinessLayer.Models;

namespace WindowsFormsPresentationLayer
{
	public partial class LoginWindow : Form
	{
		public LoginWindow()
		{
			InitializeComponent();
		}

		private void LoginWindow_Load(object sender, EventArgs e) // To correctly load the loginWindow
		{
			UsernameErrorBox.Visible = false;
			PasswordErrorBox.Visible = false;
		}

		void LoginWindow_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13) // The Enter key is Pressend
			{
				LoginButton_Click(sender, new EventArgs());
			}
		}

		private void SignUpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // When the link is clicked
		{
			GoToSignUpPage();
		}

		private void LoginButton_Click(object sender, EventArgs e) // Login button has been clicked
		{
			LoadingImage.Visible = true;
			// Setting the Error Boxes Hiden
			UsernameErrorBox.Visible = false;
			PasswordErrorBox.Visible = false;
			//

			string username = UsernameTestBox.Text;
			string password = PasswordTextBox.Text;
			#region FrontEnd Validation
			bool isNotValid = false;
			if (username == string.Empty)
			{
				UsernameErrorBox.Text = "Required";
				UsernameErrorBox.Visible = true;
				isNotValid = true;
			}
			if (password == string.Empty)
			{
				PasswordErrorBox.Text = "Required";
				PasswordErrorBox.Visible = true;
				isNotValid = true;
			}
			if (isNotValid)
			{
				LoadingImage.Visible = false;
				return;
			}
			
			#endregion

			// Authentication
			int authenticationCode = UserService.AuthenticateUserReturnsCode(username, password);
			//LoadingImage.Visible = false;
			if (authenticationCode == 2)
			{
				var authenticatedUser = UserService.ReturnUser(username, password);
				LoadingImage.Visible = false;
				GoToLoggedUserPage(authenticatedUser);
			}
			// Displaying error message
			switch (authenticationCode)
			{
				case 0: // No found Username
					UsernameErrorBox.Text = "No such username exists";
					UsernameErrorBox.Visible = true;
					break;
				case 1: // Username found, but incorrect Password
					PasswordErrorBox.Text = "Incorrect password";
					PasswordErrorBox.Visible = true;
					break;
			}
			LoadingImage.Visible = false;
		}
		private void GoToLoggedUserPage(User loggedUser)
		{
			LoggedUserWindow obj = new LoggedUserWindow(loggedUser);
			obj.Show();
			this.Hide();
		}
		private void GoToSignUpPage()
		{
			SignupWindow obj = new SignupWindow();
			obj.Show();
			this.Hide();
		}
	}
}
