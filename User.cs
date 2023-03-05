using MySqlConnector;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace SmartHome
{
    public partial class User : Form
    {
  
        public User()
        {
            InitializeComponent();

        }
        private MySqlConnection con = new MySqlConnection("SERVER=localhost; DATABASE=smarthome; UID=root; PASSWORD=");
        private static int id = 0;

        public void GetList()
        {
            con.Open();
            MySqlDataAdapter req = new MySqlDataAdapter("SELECT id, name, password, role FROM users", con);
            DataTable dt = new DataTable();
            req.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        void ChampsVide()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
            {
                DialogResult dialog = MessageBox.Show("veuillez remplir tous les champs", "champs !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    Users us = new Users(textBox1.Text, textBox2.Text, comboBox1.Text);
                    MySqlCommand req = new MySqlCommand("INSERT INTO users (name, password,role) VALUES(@name, @password,@role)", con);
                    req.Parameters.AddWithValue("@name", us.Name);
                    req.Parameters.AddWithValue("@password", us.Password);
                    req.Parameters.AddWithValue("@role", us.Role);
                    req.ExecuteNonQuery();
                    con.Close();
                    ChampsVide();
                    GetList();

                }
                catch (Exception ex)
                {
                    DialogResult dd = MessageBox.Show("sql !!");
                }


            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("voulez-vous vraiment modifier ce user !!", "update user", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Veuillez renseigner tous les champs!", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    try
                    {
                        if (con.State != ConnectionState.Open) { con.Open(); }
                        String name = textBox1.Text;
                        String pass = textBox2.Text;
                        String role = comboBox1.Text;

                        MySqlCommand req = new MySqlCommand("UPDATE users SET name=@name, password=@password ,role=@role  WHERE id=@id", con);
                        req.Parameters.AddWithValue("@name", name);
                        req.Parameters.AddWithValue("@password", pass);
                        req.Parameters.AddWithValue("@role", role);
                        req.Parameters.AddWithValue("@id", id);
                        req.ExecuteNonQuery();
                        con.Close();
                        ChampsVide();
                        GetList();



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("err !!!");
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open) { con.Open(); }

                MySqlCommand req = new MySqlCommand("DELETE FROM users WHERE id=@id", con);
                req.Parameters.AddWithValue("@id", id);
                req.ExecuteNonQuery();
                con.Close();
                GetList();
                ChampsVide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("err supp !!");
            }
            }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void button8_Click_1(object sender, EventArgs e)
        {
            GetList();
        }

        private void dataGridView1__CellMouseClick(object sender, DataGridViewCellEventArgs e)
        {

            int i = dataGridView1.SelectedCells[0].RowIndex;

            DataGridViewRow selR = dataGridView1.Rows[i];
            id = int.Parse(selR.Cells[0].Value.ToString());

            textBox1.Text = selR.Cells[1].Value.ToString();
            textBox2.Text = selR.Cells[2].Value.ToString();
            comboBox1.Text = selR.Cells[3].Value.ToString();

        }
    }
    }
