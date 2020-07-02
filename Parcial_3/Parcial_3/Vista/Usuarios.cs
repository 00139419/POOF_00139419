using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Parcial_3.Controlador;


namespace Parcial_3.Vista
{
    public partial class Usuarios : Form
    {
        CProxy.Proxy Myproxy = new CProxy.Proxy();
        
        
        public Usuarios()
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
        

        private void Usuarios_Load(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM registro WHERE idusuario = '{Log.ID}'";
            var dt1 = Myproxy.query(query);
            
            dataGridView1.DataSource = dt1;
           
        }
    }
}