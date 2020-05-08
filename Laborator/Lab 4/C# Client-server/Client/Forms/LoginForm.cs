using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class LoginForm : Form
    {
        private MainForm child;
        private ClientController controller;

        public LoginForm(ClientController controller)
        {
            this.controller = controller;
            InitializeComponent();
            child = new MainForm(this, controller);
        }

        private void Login()
        {
            try
            {
                controller.Login(usernameTextBox.Text, passwordTextBox.Text);
                child.Show();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Login error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Login();
            }
        }
            
        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Login();
            }
        }
    }
}
