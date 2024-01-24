using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmPanelGestion : Form
    {

        public frmPanelGestion()
        {
            InitializeComponent();

            dtpFechaInicio.Value = DateTime.Today.AddDays(-7);
            dtpFechaFin.Value = DateTime.Now;
            btn7Dias.Select();
            Datos();
           
        }

        private bool CargarDatos(DateTime FechaInicio, DateTime FechaFin)
        {
            CN_PanelGerencial oPanel = new CN_PanelGerencial();

            try
            {
                var RefrescarDatosContadores = oPanel.ObtenerContadores(FechaInicio, FechaFin);
                var RefrescarDatosVentas = oPanel.RedimientoVentas(FechaInicio, FechaFin);
                var RefrescarDatosProductos = oPanel.RendimientoProductos(FechaInicio, FechaFin);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private void Datos()
        {
            PaneldeGestion oContadores = new CN_PanelGerencial().ObtenerContadores(dtpFechaInicio.Value, dtpFechaFin.Value);
            PaneldeGestion oVentas = new CN_PanelGerencial().RedimientoVentas(dtpFechaInicio.Value, dtpFechaFin.Value);
            PaneldeGestion oProducto = new CN_PanelGerencial().RendimientoProductos(dtpFechaInicio.Value, dtpFechaFin.Value);

            var Refrescar = CargarDatos(dtpFechaInicio.Value, dtpFechaFin.Value);
            if (Refrescar)
            {
                lblNumeroVentas.Text = oContadores.NumeroVentas.ToString();
                lblTotalGanancia.Text = "$" + oVentas.TotalGanancia.ToString();
                lblTotalIngresos.Text = "$" + oVentas.TotalIngresos.ToString();

                lblNumeroClientes.Text = oContadores.NumeroCliente.ToString();
                lblNumeroProveedores.Text = oContadores.NumeroProveerdo.ToString();
                lblNumeroProductos.Text = oContadores.NumeroProductos.ToString();

                chartIngresosBrutos.DataSource = oVentas.IngresosBrutos;
                chartIngresosBrutos.Series[0].XValueMember = "Fecha";
                chartIngresosBrutos.Series[0].YValueMembers = "Montototal";
                chartIngresosBrutos.DataBind();

                chartTopProductos.DataSource = oProducto.ListaProductosTop;
                chartTopProductos.Series[0].XValueMember = "Key";
                chartTopProductos.Series[0].YValueMembers = "Value";
                chartTopProductos.DataBind();

                dgvBajoStock.DataSource = oProducto.ListaBajoStock;
                dgvBajoStock.Columns[0].HeaderText = "Producto";
                dgvBajoStock.Columns[1].HeaderText = "Unidad";

                Console.WriteLine("todo chil");
            }
            else Console.WriteLine("no cargo");
        }

        private void DesabilitarFechasPersonalisadas()
        {
            dtpFechaInicio.Enabled = false;
            dtpFechaFin.Enabled = false;
            btnOk.Visible = false;
        }

        private void btnHoy_Click(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Today;
            dtpFechaFin.Value = DateTime.Now;
            Datos();
            DesabilitarFechasPersonalisadas();
            
        }

        private void btn7Dias_Click(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Today.AddDays(-7);
            dtpFechaFin.Value = DateTime.Now;
            Datos();
            DesabilitarFechasPersonalisadas();
        }

        private void btn30Dias_Click(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Today.AddDays(-30);
            dtpFechaFin.Value = DateTime.Now;
            Datos();
            DesabilitarFechasPersonalisadas();
        }

        private void btnEsteMes_Click(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = new DateTime (DateTime.Today.Year,DateTime.Today.Month,1);
            dtpFechaFin.Value = DateTime.Now;
            Datos();
            DesabilitarFechasPersonalisadas();
        }

        private void btnPersonalizado_Click(object sender, EventArgs e)
        {
            dtpFechaInicio.Enabled = true;
            dtpFechaFin.Enabled = true;
            btnOk.Visible = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Datos();
        }
    }
}
