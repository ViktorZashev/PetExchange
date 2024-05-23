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
			// To Load all the dbContexts to prevent the slow loading later
			UserService.LoadDb();
			CountryService.LoadDb();
			PetService.LoadDb();
			PublicOfferService.LoadDb();
			UserRequestsService.LoadDb();
			TownService.LoadDb();
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
			Cursor.Current = Cursors.WaitCursor;
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
				Cursor.Current = Cursors.Arrow;
				return;
			}
			
			#endregion

			// Authentication
			int authenticationCode = UserService.AuthenticateUserReturnsCode(username, password);
			if (authenticationCode == 2)
			{
				var authenticatedUser = UserService.ReturnUser(username, password);
				Cursor.Current = Cursors.Arrow;
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
			Cursor.Current = Cursors.Arrow;
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
