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

namespace WindowsFormsPresentationLayer
{
	public partial class LoggedUserWindow : Form
	{
		private static User LoggedUser;

		public LoggedUserWindow(User user)
		{
			InitializeComponent();
			if (user == null)
			{
				throw new NullReferenceException("Logged User");
			}
			LoggedUser = user;
		}

		private void LoggedUserWindow_Load(object sender, EventArgs e)
		{

		}
	}
}
