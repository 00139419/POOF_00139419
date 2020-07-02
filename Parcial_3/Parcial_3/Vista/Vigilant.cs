using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Parcial_3.Controlador;

namespace Parcial_3.Vista
{
    public partial class Vigilant : Form
    {
        CProxy.Proxy Myproxy = new CProxy.Proxy();
        public bool entrar;
        public int iduser, temperatura;
        public string nombre, date;

        public Vigilant()
        {
            InitializeComponent();
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
        
        private void AddRegister_Click(object sender, EventArgs e)
        {

            try
            {
                if (radioButton1.Equals(null) || radioButton2.Equals(null) || textBox1.Text.Equals("") ||
                textBox2.Text.Equals(""))
                {
                    throw new blankSpaceException("No se pueden dejar campos vacios!");
                }

            }
            catch (blankSpaceException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (radioButton1.Checked)
            {
                    entrar = true;
            }
            else if (radioButton2.Checked)
            {
                entrar = false;
            }


            nombre = comboBox1.SelectedItem.ToString();
            temperatura = Convert.ToInt32(textBox1.Text);
            date = textBox2.Text;
            
            string query = "select idusuario from usuario " +
                           $"where nombreusuario = '{comboBox1.SelectedItem.ToString()}'";
            var n = Myproxy.query(query);
            
            foreach (DataRow i in n.Rows)
            {
                iduser = Convert.ToInt32(i[0].ToString());
            }

           
            
            string noQuery = "insert into registro (entrada,temperatura,idusuario,fecha) "+
            $"values ({entrar},{temperatura},{iduser},'{date}')";
            
            
            MessageBox.Show("La Clave es: 123");
                
            string Password = Microsoft.VisualBasic.Interaction.InputBox("Ingrese La clave: ", "CONFIRMACION",
                "", this.Width / 2 - 170, this.Height / 2 - 50);
            
            Myproxy.nonQuery(Password,noQuery);

        }
        

        private void Vigilant_Load(object sender, EventArgs e)
        {
            string query1 = $"SELECT nombreusuario FROM usuario";  
            var dt1 = Myproxy.query(query1);
            
            var lista1 = new List<string>();
            foreach (DataRow i in dt1.Rows)
            {
                lista1.Add(i[0].ToString());
            }
            comboBox1.DataSource = lista1;
            
            
        }
    }
}