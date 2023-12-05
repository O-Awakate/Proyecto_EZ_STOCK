using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Modales
{
    public partial class FrmListaProveedores : Form
    {
        public Proveedor _Provedor { get; set; }

        public FrmListaProveedores()
        {
            InitializeComponent();
        }


        private void FrmListaProveedores_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true)
                {
                    cboBuscar.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBuscar.DisplayMember = "Texto";
            cboBuscar.ValueMember = "Valor";
            cboBuscar.SelectedIndex = 0;

            //Mostrando proveedores (todos)

            List<Proveedor> Lista = new CN_Proveedor().listar();

            foreach (Proveedor item in Lista)
            {
                if (item.Estado)
                    dgvData.Rows.Add(new object[] { item.IdProveedor, item.oDatosPersona.CI, item.oDatosPersona.Nombre, item.oDatosPersona.Apellido, item.oCasaProveedora.RazonSocial });

            } 
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColum = e.ColumnIndex;

            if (iRow >= 0 && iColum > 0)
            {
                _Provedor = new Proveedor()
                {
                    IdProveedor = Convert.ToInt32(dgvData.Rows[iRow].Cells["Id"].Value.ToString()),
                    oDatosPersona = new Datos_Persona
                    {
                        CI= dgvData.Rows[iRow].Cells["Documento"].Value.ToString(),
                       Nombre = dgvData.Rows[iRow].Cells["NombreProvedor"].Value.ToString(),
                     Apellido = dgvData.Rows[iRow].Cells["ApellidoProvedor"].Value.ToString(),
                    },
                    oCasaProveedora = new Casa_Proveedora
                    {
                        RazonSocial = dgvData.Rows[iRow].Cells["RazonSocial"].Value.ToString()
                    }
                     
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBuscar.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;

                    else
                        row.Visible = false;

                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {

                row.Visible = true;
            }
        }
    }
}
