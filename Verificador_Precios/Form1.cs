using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using  System.Threading;

namespace Verificador_Precios
{
    public partial class Form1 : Form
    {
        private int segundos = 0;

        private String codigo = "";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Principal();
        }
        private void Principal()
        {
            pictureBox3.Visible = false;
            pictureBox2.Visible = false;
            label2.Visible = false;
            this.BackColor = Color.FromArgb(76,218,74);
            pictureBox1.Visible = true;
            label1.Visible = true;
            label3.Visible = true;
            pictureBox3.Location = new Point(this.Width / 2 - pictureBox3.Width/2-pictureBox3.Width, this.Height / 2 - pictureBox3.Height/2);
            pictureBox5.Location = new Point(this.Width - pictureBox5.Width, 0);
            pictureBox6.Location = new Point(0, this.Height - pictureBox6.Height);
            pictureBox7.Location = new Point(this.Width - pictureBox5.Width, this.Height - pictureBox6.Height);
            pictureBox1.Location = new Point(this.Width / 2 - pictureBox1.Width / 2, this.Height / 5);
            label2.Location = new Point(((this.Width/2)-(label2.Width/2))+pictureBox3.Width,this.Height/2-pictureBox3.Height/2);
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, this.Height / 5 + pictureBox1.Height + label3.Height*2);          
            label3.Location = new Point(this.Width / 2 - label3.Width / 2, this.Height / 5 + pictureBox1.Height);
            label4.Visible = false;
            label4.Location = new Point(this.Width / 2 - label4.Width / 2, this.Height / 2 - label4.Height / 2);

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                    try
                    {
                        MySqlConnection servidor;
                        servidor = new MySqlConnection("server='localhost';port='3306';user id='root'; password=''; database='productos'; SslMode=none");
                        servidor.Open();
                        string query = "SELECT producto_nombre, producto_precio, producto_stock, producto_imagen FROM productos WHERE producto_codigo =" + codigo + ";";
                        MySqlCommand consulta;




                        consulta = new MySqlCommand(query, servidor);
                        MySqlDataReader resultado = consulta.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            resultado.Read();
                            pictureBox1.Visible = false;
                            pictureBox2.Visible = false;

                            label3.Visible = false;
                            label1.Visible = false;
                            label4.Visible = false;

                            label2.Visible = true;
                            pictureBox3.Visible = true;
                            this.BackColor = Color.White;
                            label2.Text = resultado.GetString(0) + Environment.NewLine + "Precio:" + resultado.GetString(1) +
                                Environment.NewLine + "Stock:" + resultado.GetString(2);
                            pictureBox3.ImageLocation = resultado.GetString(3);
                            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                            segundos = 0;
                            timer1.Enabled = true;
                        }
                        else
                        {

                        pictureBox1.Visible = false;
                        pictureBox3.Visible = false;
                        pictureBox2.Visible = false;
                        label2.Visible = false;
                        label1.Visible = false;
                        label3.Visible = false;
                        this.BackColor = Color.FromArgb(76, 218, 74);

                        label4.Visible = true;
                        segundos = 0;
                        timer1.Enabled = true;
                         
                        }
                    }
                    catch (Exception x)
                    {
                        MessageBox.Show(x.ToString(), "Titulo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    codigo = "";
                }
                else
                {
                    codigo += e.KeyChar;
                }
        }

            private void timer1_Tick(object sender, EventArgs e)
		{
            segundos++;

            if (segundos == 6)
            {
                Principal();
                timer1.Enabled = false;
                label2.Text = "";
            }
		}

    }
}