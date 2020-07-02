using System;
using System.Windows.Forms;
using Parcial_3.Controlador;


namespace Parcial_3.Vista
{
    public partial class Log : Form
    {

        Manager mg = new Manager();
        Usuarios us = new Usuarios();
        Vigilant vg = new Vigilant();
        public String Name, Password;
        CProxy.Proxy Myproxy = new CProxy.Proxy();
        public static int ID;



        public Log()
        {
            InitializeComponent();
            DoubleBuffered = true;
            
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

        private void LoginButton_Click(object sender, EventArgs e)
        {
            
            //opc1 = ExecuteQuery
            //opc2 = NonExecuteQuery (Password Required)
            
            Name = textBox1.Text;
            Password = textBox2.Text;

            try
            {
                if (textBox1.Text.Equals("") || textBox2.Text.Equals(""))
                {
                    MessageBox.Show("No pueden dejarse campos vacios!");
                }
                else
                {
                    string query = $"SELECT * FROM usuario " +
                                   $"WHERE nombreusuario = '{Name}' AND contrasenia = '{Password}'";

                    var lg = Myproxy.query(query);
                    
                   
                    
                    if (lg.Rows[0][5].ToString().Equals("1"))
                    {
                        MessageBox.Show("Se ha logeado como administrador");
                        Manager Current = new Manager(Password);
                        
                        
                        mg.Show();
                        


                    }
                    else if (lg.Rows[0][5].ToString().Equals("2"))
                    {
                        MessageBox.Show("Se ha logeado como vigilante");
                        vg.Show();
                        
                       
                    }
                    else if (lg.Rows[0][5].ToString().Equals("3"))
                    {
                        MessageBox.Show("Se ha logeado como usuario");


                        ID = Convert.ToInt32(lg.Rows[0][0].ToString());
                        
                        
                        us.Show();
                     
                      
                    }
                    else
                    {
                        throw new NotDepartmentFoundException(
                            "Usted no se encuentra registrado en uno de nuestros 3 departamentos!");
                    }

                }
            }
            catch (NotDepartmentFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}