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
    public partial class FrmListaProductos : Form
    {
        public Producto _Producto { get; set; } 

        public FrmListaProductos()
        {
            InitializeComponent();
        }

        private void FrmListaProductos_Load(object sender, EventArgs e)
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

            //Mostrando Producto (todos)

            List<Producto> Lista = new CN_Producto().listar();

            foreach (Producto item in Lista)
            {
                dgvData.Rows.Add(new object[] {
                    item.IdProducto,
                    item.CodigoAvila,
                    item.CodigoFabrica,
                    item.MarcaProducto,
                    item.MarcaCarro,
                    item.DescripcionProducto,
                    item.oCategoria.NombreCategoria,
                    item.AplicaParaCarro,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta
                });

            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColum = e.ColumnIndex;

            if (iRow >= 0 && iColum > 0)
            {
                _Producto = new Producto()
                {
                    IdProducto = Convert.ToInt32(dgvData.Rows[iRow].Cells["Id"].Value.ToString()),
                    CodigoAvila = dgvData.Rows[iRow].Cells["CodigoAvila"].Value.ToString(),
                    CodigoFabrica = dgvData.Rows[iRow].Cells["CodigoFrabrica"].Value.ToString(),
                    MarcaProducto = dgvData.Rows[iRow].Cells["MarcaProducto"].Value.ToString(),
                    MarcaCarro = dgvData.Rows[iRow].Cells["MarcaCarro"].Value.ToString(),
                    DescripcionProducto = dgvData.Rows[iRow].Cells["Descripcion"].Value.ToString(),
                    oCategoria = new Categoria
                    {
                        NombreCategoria = dgvData.Rows[iRow].Cells["Categoria"].Value.ToString()
                    },
                    AplicaParaCarro = dgvData.Rows[iRow].Cells["AplicaParaCarro"].Value.ToString(),
                    Stock = Convert.ToInt32(dgvData.Rows[iRow].Cells["Stock"].Value.ToString()),
                    PrecioCompra = Convert.ToDecimal(dgvData.Rows[iRow].Cells["PrecioCompra"].Value.ToString()),
                    PrecioVenta = Convert.ToDecimal(dgvData.Rows[iRow].Cells["PrecioVenta"].Value.ToString()),
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
