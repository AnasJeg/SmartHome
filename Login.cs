using MySqlConnector;
using SmartHome.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SmartHome
{
    public partial class Login : Form
    {
        private MySqlConnection con = new MySqlConnection("SERVER=127.0.0.1; DATABASE=smarthome; UID=root; PASSWORD=");
        HomeService homeService = new HomeService();
        public Login()
        {
            InitializeComponent();
            HideOp();
        }
        private void HideOp()
        {
            label1.Hide();
            label4.Hide();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            guna2TextBox2.UseSystemPasswordChar = true;
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open) { con.Open(); }
                Users us = new Users();
                us.Name = guna2TextBox1.Text;
                us.Password= guna2TextBox2.Text;
            //    Users u1 = homeService.AfficherUser(us);
                if (guna2TextBox1.Text == "" || guna2TextBox2.Text == "")
                {   
                    label4.Show();
                    guna2TextBox1.BorderColor = Color.Red;
                    guna2TextBox2.BorderColor = Color.Red;
                    label1.Hide();
                }
                else if (homeService.LogUser(us))
                {
                    Form1 ff = new Form1();
                    ff.Getus(us.Name);
                    Console.WriteLine(us.Name);
               //     Console.WriteLine(u1.Name+" Role :  "+ u1.Role);
                    ff.Show();
                    this.Hide();
                }

                else
                {
                    
                    guna2TextBox1.BorderColor = Color.Red;
                    guna2TextBox2.BorderColor = Color.Red;
                    guna2TextBox1.Clear();
                    guna2TextBox2.Clear();
                    label1.Show();
                    label4.Hide();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DialogResult dd = MessageBox.Show("sql login !!");
            }

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked)
            {
                guna2TextBox2.UseSystemPasswordChar = false;
            }
            else
            {
                guna2TextBox2.UseSystemPasswordChar = true;
            }
        }

      
    }
}
