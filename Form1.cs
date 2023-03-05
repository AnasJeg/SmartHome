using SmartHome.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartHome
{
    public partial class Form1 : Form
    {


        Panel myControle;
        Panel b;
        PictureBox pic;
        private Control activeControle;
        private Point previousPoint;
        List<Panel> panels = new List<Panel>();
        private static int panelN = 1;
        private CheckBox check;
        //galia aimrane andiz wahed private divece li ayakhon name dial picbox f event mousedown p had name howa li ander
        private string diveceName;
        private List<home> homeList;
        Users user;
        String rl;
        private int xH = 611;
        private int yW = 555;
        private static int xZ= 0;
        private static int yZ=0;
        private static int x=0;
        private static int y=0;
        HomeService homeService = new HomeService();
        PictureBox pzone = new PictureBox();
        private static String desc;
        public Form1()
        {
            InitializeComponent();
            //initFridge();
            initExitSwitch();
            initCapt();


        }

        public bool Getus(String u)
        {
            // this.rl = u;
            this.rl = "admin";
            if (rl == "admin")
            {
                return true;
            }
            else
                return false;


        }
        public void showZ()
        {
            xZ = Convert.ToInt16(textBox1.Text);
            yZ = Convert.ToInt16(textBox2.Text);
             desc = comboBox1.Text;
            Console.WriteLine("x : " + xZ + " y : " + yZ);
            if (comboBox1.Text != "" && xZ > 0 && yZ > 0 && yZ<yW && xZ<xH)
            {
                //  PictureBox pzone=new PictureBox();
                pzone.Padding = new Padding(3, 3, 3, 3);
                Console.WriteLine(desc);
                switch (desc)
                {
                    case "Chambre":
                        pzone.Image = Image.FromFile("C:/Users/hp/source/repos/SmartHome/icons/cuisine.png");
                        break;
                    case "Chambre2":
                        pzone.Image = Image.FromFile("C:/Users/hp/source/repos/SmartHome/icons/cuisine.png");
                        break;
                    case "Chambre3":
                        pzone.Image = Image.FromFile("C:/Users/hp/source/repos/SmartHome/icons/cuisine.png");
                        break;
                    case "Salon":
                        pzone.Image = Image.FromFile("C:/Users/hp/source/repos/SmartHome/icons/cuisine.png");
                        break;
                    case "Salon2":
                        pzone.Image = Image.FromFile("C:/Users/hp/source/repos/SmartHome/icons/cuisine.png");
                        break;
                    case "Cuisin":
                        pzone.Image = Image.FromFile("C:/Users/hp/source/repos/SmartHome/icons/cuisine.png");
                        break;
                    case "Toillete":
                        pzone.Image = Image.FromFile("C:/Users/hp/source/repos/SmartHome/icons/cuisine.png");
                        break;
                    case "Toillete2":
                        pzone.Image = Image.FromFile("C:/Users/hp/source/repos/SmartHome/icons/cuisine.png");
                        break;
                        //   case "Chambr   ":

                }
                
                pzone.SizeMode = PictureBoxSizeMode.StretchImage;
                pzone.Height = xZ;
                pzone.Width = yZ;
                // Cursor = Cursors.Hand;
                // var location = pzone.Location;
                // location.Offset(pzone.Height - previousPoint.X, pzone.Width - previousPoint.Y);
                // pzone.Location = location;
                //  pzone.MouseDown += new MouseEventHandler(pzone_MouseDown);

                if (xZ < xH && yZ < yW)
                {
                    if (x + pzone.Width > panel3.Width)
                    {
                        y += pzone.Height;
                        yZ += pzone.Height;
                        x = 0;
                    }
                    pzone.Location = new Point(x, y);
                    x += pzone.Width;
                    xZ += pzone.Width;
                }

                panel3.Controls.Add(pzone);
                panel3.AutoScroll = true;
                Zone zn = new Zone(desc, xZ, yZ);
                
                homeService.AddZone(zn);
            }
            else
            {
                DialogResult dialogClose = MessageBox.Show("add zone !!!!", "Attention!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            }
        }


        private void initCapt()
        {
            homeList = homeService.afficher();
            foreach (home item in homeList)
            {
                myControle = new Panel();
                myControle.Location = new Point(item.X, item.Y);
                myControle.Size = new Size(64, 64);
                myControle.Text = (panelN).ToString();
                myControle.Name = item.Name;
                myControle.BackColor = Color.Transparent;
                myControle.Click += b_Click;
                changeIcon(myControle, item.Status);
                myControle.BackgroundImageLayout = ImageLayout.Stretch;
                myControle.MouseDown += new MouseEventHandler(myContrl_MouseDown);
                myControle.MouseMove += new MouseEventHandler(myContrl_MouseMove);
                myControle.MouseUp += new MouseEventHandler(myContrl_MMouseUp);
                panel3.Controls.Add(myControle);
                panelN++;
            }
        }
        /*private void initFridge()
        {
            myControle = new Panel();
            myControle.Location = new Point(530, 500);
            myControle.Size = new Size(64, 64);
            myControle.Text = (panelN).ToString();
            myControle.Name = string.Format("Fridge1");
            myControle.BackColor = Color.Transparent;
            myControle.Click += b_Click;
            myControle.BackgroundImage = Properties.Resources.fridge;
            myControle.BackgroundImageLayout = ImageLayout.Stretch;
            myControle.MouseDown += new MouseEventHandler(myContrl_MouseDown);
            myControle.MouseMove += new MouseEventHandler(myContrl_MouseMove);
            myControle.MouseUp += new MouseEventHandler(myContrl_MMouseUp);
            panel3.Controls.Add(myControle);

        }*/
        private void initExitSwitch()
        {
            panel7.Click += ExitSwitch;
        }
        void ExitSwitch(object sender, EventArgs e)
        {
            Panel b = (Panel)sender;
            DialogResult dialogClose = MessageBox.Show("exit = vider la DB", "Attention!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                b.BackgroundImage = Properties.Resources.exit__1_;

                panel3.Controls.Clear();
                panels.Clear();
                if (homeService.killSwitch() && homeService.killSwitch2())
                    Application.Exit();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panelN++;
            myControle = new Panel();
            if (checkBox1.Checked)
            {
                check = checkBox1;
                checkBox2.Checked = false;
                checkBox4.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                myControle.Name = string.Format("door {0}", panelN);
            }
            else if (checkBox2.Checked)
            {
                check = checkBox2;
                checkBox1.Checked = false;
                checkBox4.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                myControle.Name = string.Format("frigde {0}", panelN);
            }
            else if (checkBox4.Checked)
            {
                check = checkBox4;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                myControle.Name = string.Format("lamp {0}", panelN);
            }
            else if (checkBox7.Checked)
            {
                check = checkBox7;
                checkBox1.Checked = false;
                checkBox4.Checked = false;
                checkBox2.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                myControle.Name = string.Format("ac {0}", panelN);
            }
            else if (checkBox8.Checked)
            {
                check = checkBox8;
                checkBox1.Checked = false;
                checkBox4.Checked = false;
                checkBox7.Checked = false;
                checkBox2.Checked = false;
                checkBox9.Checked = false;
                myControle.Name = string.Format("router {0}", panelN);
            }
            else if (checkBox9.Checked)
            {
                check = checkBox9;
                checkBox1.Checked = false;
                checkBox4.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                myControle.Name = string.Format("tv {0}", panelN);
            }
            myControle.Location = new Point(300, 200);
            myControle.Size = new Size(64, 64);
            myControle.Text = (panelN).ToString();
            myControle.BackColor = Color.Transparent;
            myControle.Click += b_Click;
            myControle.BackgroundImage = check.Image;
            myControle.BackgroundImageLayout = ImageLayout.Stretch;
            myControle.MouseDown += new MouseEventHandler(myContrl_MouseDown);
            myControle.MouseMove += new MouseEventHandler(myContrl_MouseMove);
            myControle.MouseUp += new MouseEventHandler(myContrl_MMouseUp);
            panel3.Controls.Add(myControle);
            panels.Add(myControle);
            removeButton.Enabled = true;

        }

        private void checkStatus(Panel b)
        {
            if (homeService.AfficherStatus(int.Parse(b.Text)).Contains("E"))
            {
                deconnect.Enabled = true;
                allumer.Enabled = true;
                Eteindre.Enabled = false;
                connect.Enabled = false;
            }
            else if (homeService.AfficherStatus(int.Parse(b.Text)).Contains("A"))
            {
                Eteindre.Enabled = true;
                deconnect.Enabled = true;
                allumer.Enabled = false;
                connect.Enabled = false;
            }
            else if (homeService.AfficherStatus(int.Parse(b.Text)).Contains("D"))
            {
                Eteindre.Enabled = false;
                allumer.Enabled = false;
                deconnect.Enabled = false;
                connect.Enabled = true;
            }
            else if (homeService.AfficherStatus(int.Parse(b.Text)).Contains("C"))
            {
                Eteindre.Enabled = false;
                allumer.Enabled = true;
                connect.Enabled = false;
                deconnect.Enabled = true;
            }
        }

        private void myContrl_MMouseUp(object sender, MouseEventArgs e)
        {
            activeControle = null;
            ActiveControl = null;
            Cursor = Cursors.Default;
        }

        private void myContrl_MouseMove(object sender, MouseEventArgs e)
        {
            if (activeControle == null || activeControle != sender)
            {
                return;
            }
            var location = activeControle.Location;
            location.Offset(e.Location.X - previousPoint.X, e.Location.Y - previousPoint.Y);
            activeControle.Location = location;
          
             var location2 = pzone.Location;
             location.Offset(pzone.Height - previousPoint.X, pzone.Width - previousPoint.Y);
             pzone.Location = location2;
        }

        private void myContrl_MouseDown(object sender, MouseEventArgs e)
        {
            activeControle = sender as Control;
            previousPoint = e.Location;
            Cursor = Cursors.Hand;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (homeService.AfficherParIndex(panelN) == true) homeService.delete(panelN);
            if (panel3.Controls.Count == (homeList.Count() + 3)) removeButton.Enabled = false;
            panel3.Controls.Remove(panels.Last());
            panels.RemoveAt(panelN - (homeList.Count() + 2));
            panelN--;
            panel6.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            String n = "";
            Point locationOnForm = myControle.FindForm().PointToClient(myControle.Parent.PointToScreen(myControle.Location));

            DialogResult dialogClose = MessageBox.Show("Add => " + myControle.Name +" IN "+desc, "Alert!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dialogClose == DialogResult.OK)
            {
                if (locationOnForm.X > 47 && locationOnForm.X < 250 && locationOnForm.Y < 290)
                    n = desc;
               else if (locationOnForm.X > 250 && locationOnForm.X < 410 && locationOnForm.Y < 219)
                    n = desc;
                else if (locationOnForm.X > 480 && locationOnForm.X < 650 && locationOnForm.Y < 380)
                    n = desc;
                else if (locationOnForm.X > 230 && locationOnForm.X < 470 && locationOnForm.Y > 500)
                    n = desc;
                else if (locationOnForm.X > 220 && locationOnForm.X < 260 && locationOnForm.Y > 370)
                    n = desc;
                else if (locationOnForm.X > 28 && locationOnForm.X < 210 && locationOnForm.Y > 370)
                    n = desc;
                homeService.Ajouter(new home(myControle.Name, n, "E", panelN, locationOnForm.X - 1, locationOnForm.Y - 1));
                panel6.Visible = false;
            }
            else if (dialogClose == DialogResult.Cancel)
            {
                panel3.Controls.Remove(panels.Last());
                panels.RemoveAt(panelN - 2);
                panelN--;
                panel6.Visible = false;
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            showZ();
        }

        private void allumer_Click(object sender, EventArgs e)
        {

            DialogResult dialogClose = MessageBox.Show("Turn On This !!", "Alert!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                Eteindre.Enabled = true;
                allumer.Enabled = false;
                homeService.Modifier(int.Parse(b.Text), "A");
                changeIcon(b, "A");
            }

        }

        private void Eteindre_Click(object sender, EventArgs e)
        {
            DialogResult dialogClose = MessageBox.Show("Turn Off This !!", "Alert!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                Eteindre.Enabled = false;
                allumer.Enabled = true;
                homeService.Modifier(int.Parse(b.Text), "E");
                changeIcon(b, "E");
            }
        }

        private void connect_Click(object sender, EventArgs e)
        {
            DialogResult dialogClose = MessageBox.Show("Connect This !!", "Alert!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                connect.Enabled = false;
                deconnect.Enabled = true;
                allumer.Enabled = true;
                Eteindre.Enabled = false;
                homeService.Modifier(int.Parse(b.Text), "C");
                changeIcon(b, "C");
            }
        }

        private void deconnect_Click(object sender, EventArgs e)
        {
            DialogResult dialogClose = MessageBox.Show("Disconnect This !!", "Alert!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                connect.Enabled = true;
                deconnect.Enabled = false;
                allumer.Enabled = false;
                Eteindre.Enabled = false;
                homeService.Modifier(int.Parse(b.Text), "D");
                changeIcon(b, "D");
            }
        }
        private void changeIcon(Panel b, String valeur)
        {
            if (valeur.Contains("A"))
            {
                if (b.Name.Contains("tv")) b.BackgroundImage = Properties.Resources.smart_tv;
                else if (b.Name.Contains("door")) b.BackgroundImage = Properties.Resources.door__1_;
                else if (b.Name.Contains("lamp")) b.BackgroundImage = Properties.Resources.light_bulb;
                else if (b.Name.Contains("router")) b.BackgroundImage = Properties.Resources.wireless_router;
                else if (b.Name.Contains("ac")) b.BackgroundImage = Properties.Resources.ac__1_;
            }
            else
            {

                if (b.Name.Contains("tv")) b.BackgroundImage = Properties.Resources.smart_tv__1_;
                else if (b.Name.Contains("door")) b.BackgroundImage = Properties.Resources.door;
                else if (b.Name.Contains("lamp")) b.BackgroundImage = Properties.Resources.light_bulb__2_;
                else if (b.Name.Contains("router")) b.BackgroundImage = Properties.Resources.wireless_router__1_;
                else if (b.Name.Contains("ac")) b.BackgroundImage = Properties.Resources.ac;
            }
        }

        void b_Click(object sender, EventArgs e)
        {
            b = sender as Panel;
            if (b != null)
            {
                bool exist = homeService.AfficherParIndex(int.Parse(b.Text));
                if (exist == false)
                    panel6.Visible = true;
                else
                    checkStatus(b);
            }
        }
        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox4.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox4.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox4.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox4.Checked = false;
            checkBox7.Checked = false;
            checkBox2.Checked = false;
            checkBox9.Checked = false;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox4.Checked = false;
            checkBox2.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*   if (homeService.checkZoneAllu(comboBox1.Text)==1) { 
                   connect.Enabled = false; deconnect.Enabled = true;
               Eteindre.Enabled = true; allumer.Enabled = true;
               }
           else if(homeService.checkZoneAllu(comboBox1.Text) == 0) {
               MessageBox.Show("Zone Vide : "+comboBox1.Text);
               connect.Enabled = false; deconnect.Enabled = false;
               Eteindre.Enabled = false; allumer.Enabled = false;
           }
           else if (homeService.checkZoneAllu(comboBox1.Text) ==-1)
           { 
               connect.Enabled = true; deconnect.Enabled = true;
               Eteindre.Enabled = true; allumer.Enabled = true;
           }
            */
        }


        private void button3_Click(object sender, EventArgs e)
        {
            User uss = new User();
            uss.Show();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }





        private void picMouseDw(object sender, MouseEventArgs e)
        {
            panelN++;
            myControle = new Panel();
            pic = sender as PictureBox;
            //  pic.BringToFront();
            Console.WriteLine("PIC : "+pic.ToString() );
            if (pic != null)
            {
                diveceName = pic.Name;
                // MessageBox.Show(diveceName);
            }
            ((PictureBox)sender).DoDragDrop(((PictureBox)sender).Image, DragDropEffects.Copy);

            if (pic.Equals(pictureBox2))
            {
                myControle.Name = string.Format("door {0}", panelN);
            }
            else if (pic.Equals(pictureBox1))
            {
                myControle.Name = string.Format("ac {0}", panelN);
            }
            else if (pic.Equals(pictureBox3))
            {
                myControle.Name = string.Format("router {0}", panelN);
            }
            else if (pic.Equals(pictureBox4))
            {
                myControle.Name = string.Format("lamp {0}", panelN);
            }
            else if (pic.Equals(pictureBox5))
            {
                myControle.Name = string.Format("tv {0}", panelN);
            }
            else if (pic.Equals(pictureBox6))
            {
                myControle.Name = string.Format("frigde {0}", panelN);
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel3.AllowDrop = true;
            panelDrag.Hide();
            panelBox.Hide();

            //pzone.AllowDrop = true;
            if (Getus(rl))
            {
                panel9.Show();
                Console.WriteLine(" >>>>> : " + rl);
            }
            else
            {
                panel9.Hide();
            }
        }




        private void panelDD(object sender, DragEventArgs e)
        {
            Image imgGetPic = (Bitmap)e.Data.GetData(DataFormats.Bitmap);

            myControle = new Panel();

            myControle.Location = new Point(300, 300);
            myControle.Size = new Size(64, 64);
            myControle.Text = (panelN).ToString();
            myControle.Name = diveceName;
            myControle.BackColor = Color.Transparent;
            myControle.Click += b_Click;
            myControle.BackgroundImage = imgGetPic;
            myControle.BackgroundImageLayout = ImageLayout.Stretch;
            myControle.MouseDown += new MouseEventHandler(myContrl_MouseDown);
            myControle.MouseMove += new MouseEventHandler(myContrl_MouseMove);
            myControle.MouseUp += new MouseEventHandler(myContrl_MMouseUp);
            panel3.Controls.Add(myControle);
            panels.Add(myControle);
            removeButton.Enabled = true;
            myControle.BringToFront();
        }

        private void panelDE(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Copy;

            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked)
            {
                guna2CheckBox2.Checked= false;
                panelBox.Show();
            }
            else
            {
                panelBox.Hide();
            }
        }

        private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(guna2CheckBox2.Checked)
            {
                guna2CheckBox1.Checked= false;
                panelDrag.Show();
            }
            else
            {
                panelDrag.Hide();
            }

        }
    }
}
