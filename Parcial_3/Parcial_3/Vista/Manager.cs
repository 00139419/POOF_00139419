using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Parcial_3.Controlador;


namespace Parcial_3.Vista
{
    public partial class Manager : Form
    {
        
        CProxy.Proxy Myproxy = new CProxy.Proxy();
        public string Nombre, apellido, contrasenia, dui, FechadeNacimiento, Pass;
        public int idDep;
        public int iduser;
        
        
        public delegate void GetName(string text);
        public GetName get_nickName;
        
        public Manager()
        {
            InitializeComponent();
           
           
        }

        public Manager(string pass)
        {
            Pass = pass;
        }
        
        protected override CreateParams CreateParams
        {    
            get
            {    
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        private void Administrador_Load(object sender, EventArgs e)
        {

           
            // Para DataGridView
            string query = $"SELECT * FROM usuario";
            var dt1 = Myproxy.query(query);
            dataGridView1.DataSource = dt1;
            dataGridView1.Dock = DockStyle.Fill;
            
            // Para DataGridView
            string query6 = $"SELECT * FROM registro where entrada = true";
            var dt6 = Myproxy.query(query6);
            dataGridView2.DataSource = dt6;
            dataGridView2.Dock = DockStyle.Fill;
            
            
            
            //Llenando comboBox2
            string query2 = $"SELECT nombreusuario FROM usuario";  
            var dt2 = Myproxy.query(query2);
            
            var lista1 = new List<string>();
            foreach (DataRow i in dt2.Rows)
            {
                lista1.Add(i[0].ToString());
            }
            comboBox2.DataSource = lista1;
            
            
            //Llenando comboBox1
            string query3 = $"SELECT nombre FROM departamento";  
            var dt3 = Myproxy.query(query3);
            
            var lista2 = new List<string>();
            foreach (DataRow i in dt3.Rows)
            {
                lista2.Add(i[0].ToString());
            }
            comboBox1.DataSource = lista2;
            

        }

        private void AddUser_Click(object sender, EventArgs e)
        {
            Nombre = textBox1.Text;
            apellido = textBox2.Text;
            dui = textBox3.Text;
            FechadeNacimiento = textBox4.Text;
            contrasenia = textBox5.Text;


            try
            {
                if (textBox1.Text == "" ||
                    textBox2.Text == "" ||
                    textBox3.Text == "" ||
                    textBox4.Text == "" ||
                    textBox5.Text == "")
                {
                    throw new blankSpaceException("No pueden dejarse espacio en blancos");
                }
            }
            catch (blankSpaceException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (textBox1.Text != "" ||
                textBox2.Text != "" ||
                textBox3.Text != "" ||
                textBox4.Text != "" ||
                textBox5.Text != "")
            {
                string query = "select iddepartamento from departamento " +
                               $"where nombre = '{comboBox1.SelectedItem.ToString()}'";
                var n = Myproxy.query(query);

                foreach (DataRow i in n.Rows)
                {
                    idDep = Convert.ToInt32(i[0].ToString());
                }

                string query2 =
                    $"insert into usuario(nombreusuario,apellidousuario,contrasenia,dui,iddepto,nacimiento) " +
                    $"values ('{Nombre}','{apellido}','{contrasenia}','{dui}',{idDep},'{FechadeNacimiento}')";

                MessageBox.Show("La Clave es: 123");

                Pass = Microsoft.VisualBasic.Interaction.InputBox("Ingrese La clave: ", "CONFIRMACION",
                    "", this.Width / 2 - 170, this.Height / 2 - 50);

                Myproxy.nonQuery(Pass, query2);

            }
        }

        private void DeleteUser_Click(object sender, EventArgs e)
        {
           
            string query = "select idusuario from usuario " +
                           $"where nombreusuario = '{comboBox2.SelectedItem.ToString()}'";
            var n = Myproxy.query(query);
            
            foreach (DataRow i in n.Rows)
            {
                iduser = Convert.ToInt32(i[0].ToString());
            }

            string noQuery = $"delete from usuario where idusuario = '{iduser}'";
            
            MessageBox.Show("La Clave es: 123");
                
            Pass = Microsoft.VisualBasic.Interaction.InputBox("Ingrese La clave: ", "CONFIRMACION",
                "", this.Width / 2 - 170, this.Height / 2 - 50);
            
            Myproxy.nonQuery(Pass,noQuery);
            
        }
    }
    }
