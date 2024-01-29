namespace CapaPresentacion
{
    partial class frmPanelGestion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.btn30Dias = new System.Windows.Forms.Button();
            this.btnEsteMes = new System.Windows.Forms.Button();
            this.btn7Dias = new System.Windows.Forms.Button();
            this.btnHoy = new System.Windows.Forms.Button();
            this.btnPersonalizado = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblNumeroVentas = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTotalIngresos = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTotalGanancia = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chartIngresosBrutos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTopProductos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblNumeroProductos = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblNumeroProveedores = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblNumeroClientes = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dgvBajoStock = new System.Windows.Forms.DataGridView();
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartIngresosBrutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopProductos)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBajoStock)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 30);
            this.label1.TabIndex = 76;
            this.label1.Text = "Panel Gerencial";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "MMM dd, yyyy";
            this.dtpFechaInicio.Enabled = false;
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(184, 20);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaInicio.TabIndex = 77;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "MMM dd, yyyy";
            this.dtpFechaFin.Enabled = false;
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(300, 21);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaFin.TabIndex = 78;
            // 
            // btn30Dias
            // 
            this.btn30Dias.Location = new System.Drawing.Point(691, 19);
            this.btn30Dias.Name = "btn30Dias";
            this.btn30Dias.Size = new System.Drawing.Size(90, 23);
            this.btn30Dias.TabIndex = 79;
            this.btn30Dias.Text = "Ultimos 30 Días";
            this.btn30Dias.UseVisualStyleBackColor = true;
            this.btn30Dias.Click += new System.EventHandler(this.btn30Dias_Click);
            // 
            // btnEsteMes
            // 
            this.btnEsteMes.Location = new System.Drawing.Point(787, 20);
            this.btnEsteMes.Name = "btnEsteMes";
            this.btnEsteMes.Size = new System.Drawing.Size(62, 22);
            this.btnEsteMes.TabIndex = 80;
            this.btnEsteMes.Text = "Este mes";
            this.btnEsteMes.UseVisualStyleBackColor = true;
            this.btnEsteMes.Click += new System.EventHandler(this.btnEsteMes_Click);
            // 
            // btn7Dias
            // 
            this.btn7Dias.Location = new System.Drawing.Point(600, 19);
            this.btn7Dias.Name = "btn7Dias";
            this.btn7Dias.Size = new System.Drawing.Size(85, 24);
            this.btn7Dias.TabIndex = 81;
            this.btn7Dias.Text = "Ultimos 7 Dias";
            this.btn7Dias.UseVisualStyleBackColor = true;
            this.btn7Dias.Click += new System.EventHandler(this.btn7Dias_Click);
            // 
            // btnHoy
            // 
            this.btnHoy.Location = new System.Drawing.Point(550, 19);
            this.btnHoy.Name = "btnHoy";
            this.btnHoy.Size = new System.Drawing.Size(44, 24);
            this.btnHoy.TabIndex = 82;
            this.btnHoy.Text = "Hoy";
            this.btnHoy.UseVisualStyleBackColor = true;
            this.btnHoy.Click += new System.EventHandler(this.btnHoy_Click);
            // 
            // btnPersonalizado
            // 
            this.btnPersonalizado.Location = new System.Drawing.Point(463, 20);
            this.btnPersonalizado.Name = "btnPersonalizado";
            this.btnPersonalizado.Size = new System.Drawing.Size(81, 23);
            this.btnPersonalizado.TabIndex = 83;
            this.btnPersonalizado.Text = "Personalizado";
            this.btnPersonalizado.UseVisualStyleBackColor = true;
            this.btnPersonalizado.Click += new System.EventHandler(this.btnPersonalizado_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(426, 20);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(31, 23);
            this.btnOk.TabIndex = 84;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Visible = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblNumeroVentas
            // 
            this.lblNumeroVentas.AutoSize = true;
            this.lblNumeroVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroVentas.Location = new System.Drawing.Point(3, 13);
            this.lblNumeroVentas.Name = "lblNumeroVentas";
            this.lblNumeroVentas.Size = new System.Drawing.Size(20, 22);
            this.lblNumeroVentas.TabIndex = 1;
            this.lblNumeroVentas.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Numero de Ventas";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblNumeroVentas);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(709, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(137, 40);
            this.panel1.TabIndex = 85;
            this.panel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lblTotalIngresos);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(17, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(289, 40);
            this.panel2.TabIndex = 86;
            // 
            // lblTotalIngresos
            // 
            this.lblTotalIngresos.AutoSize = true;
            this.lblTotalIngresos.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalIngresos.Location = new System.Drawing.Point(3, 13);
            this.lblTotalIngresos.Name = "lblTotalIngresos";
            this.lblTotalIngresos.Size = new System.Drawing.Size(20, 22);
            this.lblTotalIngresos.TabIndex = 1;
            this.lblTotalIngresos.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Total Ingresos";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.lblTotalGanancia);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(312, 51);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(391, 40);
            this.panel3.TabIndex = 87;
            // 
            // lblTotalGanancia
            // 
            this.lblTotalGanancia.AutoSize = true;
            this.lblTotalGanancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalGanancia.Location = new System.Drawing.Point(3, 13);
            this.lblTotalGanancia.Name = "lblTotalGanancia";
            this.lblTotalGanancia.Size = new System.Drawing.Size(20, 22);
            this.lblTotalGanancia.TabIndex = 1;
            this.lblTotalGanancia.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Total Ganancias";
            // 
            // chartIngresosBrutos
            // 
            chartArea3.Name = "ChartArea1";
            this.chartIngresosBrutos.ChartAreas.Add(chartArea3);
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend3.Name = "Legend1";
            this.chartIngresosBrutos.Legends.Add(legend3);
            this.chartIngresosBrutos.Location = new System.Drawing.Point(17, 97);
            this.chartIngresosBrutos.Name = "chartIngresosBrutos";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartIngresosBrutos.Series.Add(series3);
            this.chartIngresosBrutos.Size = new System.Drawing.Size(513, 258);
            this.chartIngresosBrutos.TabIndex = 88;
            this.chartIngresosBrutos.Text = "chart1";
            title3.Alignment = System.Drawing.ContentAlignment.TopLeft;
            title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            title3.Name = "Title1";
            title3.Text = "Ingresos Brutos";
            this.chartIngresosBrutos.Titles.Add(title3);
            // 
            // chartTopProductos
            // 
            chartArea4.Name = "ChartArea1";
            this.chartTopProductos.ChartAreas.Add(chartArea4);
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend4.Name = "Legend1";
            this.chartTopProductos.Legends.Add(legend4);
            this.chartTopProductos.Location = new System.Drawing.Point(536, 97);
            this.chartTopProductos.Name = "chartTopProductos";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            series4.IsValueShownAsLabel = true;
            series4.LabelForeColor = System.Drawing.Color.White;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartTopProductos.Series.Add(series4);
            this.chartTopProductos.Size = new System.Drawing.Size(310, 445);
            this.chartTopProductos.TabIndex = 89;
            this.chartTopProductos.Text = "chart2";
            title4.Alignment = System.Drawing.ContentAlignment.TopLeft;
            title4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            title4.Name = "Title1";
            title4.Text = "Top 5 productos";
            this.chartTopProductos.Titles.Add(title4);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.lblNumeroProductos);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.lblNumeroProveedores);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.lblNumeroClientes);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Location = new System.Drawing.Point(17, 361);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(152, 181);
            this.panel4.TabIndex = 90;
            // 
            // lblNumeroProductos
            // 
            this.lblNumeroProductos.AutoSize = true;
            this.lblNumeroProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroProductos.Location = new System.Drawing.Point(13, 105);
            this.lblNumeroProductos.Name = "lblNumeroProductos";
            this.lblNumeroProductos.Size = new System.Drawing.Size(20, 22);
            this.lblNumeroProductos.TabIndex = 6;
            this.lblNumeroProductos.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Numero de Productos";
            // 
            // lblNumeroProveedores
            // 
            this.lblNumeroProveedores.AutoSize = true;
            this.lblNumeroProveedores.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroProveedores.Location = new System.Drawing.Point(13, 70);
            this.lblNumeroProveedores.Name = "lblNumeroProveedores";
            this.lblNumeroProveedores.Size = new System.Drawing.Size(20, 22);
            this.lblNumeroProveedores.TabIndex = 4;
            this.lblNumeroProveedores.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Numero de Proveedores";
            // 
            // lblNumeroClientes
            // 
            this.lblNumeroClientes.AutoSize = true;
            this.lblNumeroClientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroClientes.Location = new System.Drawing.Point(12, 35);
            this.lblNumeroClientes.Name = "lblNumeroClientes";
            this.lblNumeroClientes.Size = new System.Drawing.Size(20, 22);
            this.lblNumeroClientes.TabIndex = 2;
            this.lblNumeroClientes.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 22);
            this.label3.TabIndex = 1;
            this.label3.Text = "Contador Total";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Numero de Clientes";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.dgvBajoStock);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Location = new System.Drawing.Point(181, 361);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(349, 181);
            this.panel5.TabIndex = 91;
            // 
            // dgvBajoStock
            // 
            this.dgvBajoStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBajoStock.Location = new System.Drawing.Point(3, 22);
            this.dgvBajoStock.Name = "dgvBajoStock";
            this.dgvBajoStock.Size = new System.Drawing.Size(343, 156);
            this.dgvBajoStock.TabIndex = 2;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(207, 22);
            this.label14.TabIndex = 1;
            this.label14.Text = "Productos de Bajo Stock";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(297, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Fecha Final";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(181, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 92;
            this.label9.Text = "Fecha Inicial";
            // 
            // frmPanelGestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(858, 566);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.chartTopProductos);
            this.Controls.Add(this.chartIngresosBrutos);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnPersonalizado);
            this.Controls.Add(this.btnHoy);
            this.Controls.Add(this.btn7Dias);
            this.Controls.Add(this.btnEsteMes);
            this.Controls.Add(this.btn30Dias);
            this.Controls.Add(this.dtpFechaFin);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.label1);
            this.Name = "frmPanelGestion";
            this.Text = "frmPanelGestion";
            this.Load += new System.EventHandler(this.frmPanelGestion_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartIngresosBrutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopProductos)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBajoStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Button btn30Dias;
        private System.Windows.Forms.Button btnEsteMes;
        private System.Windows.Forms.Button btn7Dias;
        private System.Windows.Forms.Button btnHoy;
        private System.Windows.Forms.Button btnPersonalizado;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblNumeroVentas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTotalIngresos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTotalGanancia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartIngresosBrutos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTopProductos;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblNumeroClientes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblNumeroProductos;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblNumeroProveedores;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView dgvBajoStock;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
    }
}