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
    public partial class FrmListaCreditoCompras : Form
    {
        public Compra _Compra { get; set; }

        public FrmListaCreditoCompras()
        {
            InitializeComponent();
        }

        private void FrmListaCreditoCompras_Load(object sender, EventArgs e)
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

            List<Compra> Lista = new CN_Compra().listar();

            foreach (Compra item in Lista)
            {
                if ((double)item.oCredito.Deuda > 0.00)
                    dgvData.Rows.Add(new object[] {
                        item.IdCompra,
                        item.OUsuario.IdUsuario,
                        item.OUsuario.oDatosPersona.Nombre + " " + item.OUsuario.oDatosPersona.Apellido,
                        item.oCredito.IdCredito,
                        item.oCredito.Deuda,
                        item.OProvedor.IdProveedor,
                        item.OProvedor.oDatosPersona.CI,
                        item.OProvedor.oDatosPersona.Nombre,
                        item.OProvedor.oDatosPersona.Apellido,
                        item.OProvedor.oCasaProveedora.RazonSocial,
                        item.TipoDocumento,
                        item.NumeroDocumento,
                        item.MontoTotal,
                        item.MontoBs,
                        item.MetodoPago,
                        item.FechaRegistro
                    });

            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColum = e.ColumnIndex;

            if (iRow >= 0 && iColum >= 0)
            {
                _Compra = new Compra()
                {
                    NumeroDocumento = dgvData.Rows[iRow].Cells["NumeroDocumento"].Value.ToString(),
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
