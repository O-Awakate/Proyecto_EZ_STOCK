namespace CapaPresentacion
{
    partial class Inicio
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
            this.components = new System.ComponentModel.Container();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnBack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuacercade = new System.Windows.Forms.Button();
            this.menuproveedores = new System.Windows.Forms.Button();
            this.menuclientes = new System.Windows.Forms.Button();
            this.menureportes = new System.Windows.Forms.Button();
            this.menuadministracion = new System.Windows.Forms.Button();
            this.menudevolución = new System.Windows.Forms.Button();
            this.menucompras = new System.Windows.Forms.Button();
            this.menuventas = new System.Windows.Forms.Button();
            this.menuapartado = new System.Windows.Forms.Button();
            this.menuusuarios = new System.Windows.Forms.Button();
            this.contenedor = new System.Windows.Forms.Panel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.Panel();
            this.ddmAdministracion = new CapaPresentacion.Controls.DropdownMenu(this.components);
            this.SubMenuCategoria = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuProducto = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuNegocio = new System.Windows.Forms.ToolStripMenuItem();
            this.ddmVenta = new CapaPresentacion.Controls.DropdownMenu(this.components);
            this.SubMenuRegVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuDetVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuAbonoVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.ddmCompra = new CapaPresentacion.Controls.DropdownMenu(this.components);
            this.SubMenuRegIngreso = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuDetIngreso = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuAbonoIngreso = new System.Windows.Forms.ToolStripMenuItem();
            this.ddmReportes = new CapaPresentacion.Controls.DropdownMenu(this.components);
            this.SubMenuRepVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuRepIngreso = new System.Windows.Forms.ToolStripMenuItem();
            this.ddmDevoluciones = new CapaPresentacion.Controls.DropdownMenu(this.components);
            this.devolverVentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuDevlProv = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.ddmAdministracion.SuspendLayout();
            this.ddmVenta.SuspendLayout();
            this.ddmCompra.SuspendLayout();
            this.ddmReportes.SuspendLayout();
            this.ddmDevoluciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // btnBack
            // 
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Image = global::CapaPresentacion.Properties.Resources.arrow_back_circle_outline__1_;
            this.btnBack.Location = new System.Drawing.Point(10, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(35, 31);
            this.btnBack.TabIndex = 10;
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(133)))));
            this.label1.Location = new System.Drawing.Point(12, 566);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Usuario:";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(10, 558);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 5);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // menuacercade
            // 
            this.menuacercade.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.menuacercade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuacercade.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuacercade.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(163)))), ((int)(((byte)(17)))));
            this.menuacercade.Image = global::CapaPresentacion.Properties.Resources.information_outline__1_1;
            this.menuacercade.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuacercade.Location = new System.Drawing.Point(10, 583);
            this.menuacercade.Name = "menuacercade";
            this.menuacercade.Size = new System.Drawing.Size(168, 34);
            this.menuacercade.TabIndex = 11;
            this.menuacercade.Text = "Acerca De";
            this.menuacercade.UseVisualStyleBackColor = true;
            // 
            // menuproveedores
            // 
            this.menuproveedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuproveedores.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.menuproveedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuproveedores.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuproveedores.Image = global::CapaPresentacion.Properties.Resources.timeline_delivery__1_;
            this.menuproveedores.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuproveedores.Location = new System.Drawing.Point(10, 129);
            this.menuproveedores.Name = "menuproveedores";
            this.menuproveedores.Size = new System.Drawing.Size(168, 34);
            this.menuproveedores.TabIndex = 7;
            this.menuproveedores.Text = "Proveedores";
            this.menuproveedores.UseVisualStyleBackColor = true;
            this.menuproveedores.Click += new System.EventHandler(this.menuProveedores_Click);
            // 
            // menuclientes
            // 
            this.menuclientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuclientes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.menuclientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuclientes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuclientes.Image = global::CapaPresentacion.Properties.Resources.users__1_;
            this.menuclientes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuclientes.Location = new System.Drawing.Point(10, 89);
            this.menuclientes.Name = "menuclientes";
            this.menuclientes.Size = new System.Drawing.Size(168, 34);
            this.menuclientes.TabIndex = 6;
            this.menuclientes.Text = "Clientes";
            this.menuclientes.UseVisualStyleBackColor = true;
            this.menuclientes.Click += new System.EventHandler(this.menuClientes_Click);
            // 
            // menureportes
            // 
            this.menureportes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menureportes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.menureportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menureportes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menureportes.Image = global::CapaPresentacion.Properties.Resources.report_multiple__1_;
            this.menureportes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menureportes.Location = new System.Drawing.Point(10, 289);
            this.menureportes.Name = "menureportes";
            this.menureportes.Size = new System.Drawing.Size(168, 34);
            this.menureportes.TabIndex = 8;
            this.menureportes.Text = "    Generar Reportes";
            this.menureportes.UseVisualStyleBackColor = true;
            this.menureportes.Click += new System.EventHandler(this.btnGeneraReportes_Click);
            // 
            // menuadministracion
            // 
            this.menuadministracion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuadministracion.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.menuadministracion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuadministracion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuadministracion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.menuadministracion.Image = global::CapaPresentacion.Properties.Resources.result_draft__1_;
            this.menuadministracion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuadministracion.Location = new System.Drawing.Point(10, 249);
            this.menuadministracion.Name = "menuadministracion";
            this.menuadministracion.Size = new System.Drawing.Size(168, 34);
            this.menuadministracion.TabIndex = 5;
            this.menuadministracion.Text = "Administración";
            this.menuadministracion.UseVisualStyleBackColor = true;
            this.menuadministracion.Click += new System.EventHandler(this.btnAdministracion_Click);
            // 
            // menudevolución
            // 
            this.menudevolución.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menudevolución.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.menudevolución.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menudevolución.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menudevolución.Image = global::CapaPresentacion.Properties.Resources.product_release__1_;
            this.menudevolución.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menudevolución.Location = new System.Drawing.Point(10, 329);
            this.menudevolución.Name = "menudevolución";
            this.menudevolución.Size = new System.Drawing.Size(168, 34);
            this.menudevolución.TabIndex = 9;
            this.menudevolución.Text = "Devolución";
            this.menudevolución.UseVisualStyleBackColor = true;
            this.menudevolución.Click += new System.EventHandler(this.btnDevolucion_Click);
            // 
            // menucompras
            // 
            this.menucompras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menucompras.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.menucompras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menucompras.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menucompras.Image = global::CapaPresentacion.Properties.Resources.interface_download_box_1_arrow_box_down_download_internet_network_server_upload__1_;
            this.menucompras.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menucompras.Location = new System.Drawing.Point(10, 49);
            this.menucompras.Name = "menucompras";
            this.menucompras.Size = new System.Drawing.Size(168, 34);
            this.menucompras.TabIndex = 4;
            this.menucompras.Text = "Compra";
            this.menucompras.UseVisualStyleBackColor = true;
            this.menucompras.Click += new System.EventHandler(this.btnIngreso_Click);
            // 
            // menuventas
            // 
            this.menuventas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuventas.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.menuventas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuventas.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuventas.Image = global::CapaPresentacion.Properties.Resources.interface_upload_box_1_arrow_box_download_internet_network_server_up_upload__1_;
            this.menuventas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuventas.Location = new System.Drawing.Point(10, 9);
            this.menuventas.Name = "menuventas";
            this.menuventas.Size = new System.Drawing.Size(168, 34);
            this.menuventas.TabIndex = 3;
            this.menuventas.Text = "Venta";
            this.menuventas.UseVisualStyleBackColor = true;
            this.menuventas.Click += new System.EventHandler(this.btnVenta_Click);
            // 
            // menuapartado
            // 
            this.menuapartado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuapartado.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.menuapartado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuapartado.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuapartado.Image = global::CapaPresentacion.Properties.Resources.box_time_outline__1_;
            this.menuapartado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuapartado.Location = new System.Drawing.Point(10, 169);
            this.menuapartado.Name = "menuapartado";
            this.menuapartado.Size = new System.Drawing.Size(168, 34);
            this.menuapartado.TabIndex = 0;
            this.menuapartado.Text = "Servicio de Apartado";
            this.menuapartado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.menuapartado.UseVisualStyleBackColor = true;
            this.menuapartado.Click += new System.EventHandler(this.menuServicioApartado_Click);
            // 
            // menuusuarios
            // 
            this.menuusuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuusuarios.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.menuusuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuusuarios.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuusuarios.Image = global::CapaPresentacion.Properties.Resources.user__1_1;
            this.menuusuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuusuarios.Location = new System.Drawing.Point(10, 209);
            this.menuusuarios.Name = "menuusuarios";
            this.menuusuarios.Size = new System.Drawing.Size(168, 34);
            this.menuusuarios.TabIndex = 2;
            this.menuusuarios.Text = "Usuario";
            this.menuusuarios.UseVisualStyleBackColor = true;
            this.menuusuarios.Click += new System.EventHandler(this.menuUsuario_Click);
            // 
            // contenedor
            // 
            this.contenedor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.contenedor.Location = new System.Drawing.Point(186, 12);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(874, 605);
            this.contenedor.TabIndex = 1;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(133)))));
            this.lblUsuario.Location = new System.Drawing.Point(65, 566);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(81, 17);
            this.lblUsuario.TabIndex = 19;
            this.lblUsuario.Text = "lorem ipsum";
            // 
            // menu
            // 
            this.menu.Controls.Add(this.menudevolución);
            this.menu.Controls.Add(this.menureportes);
            this.menu.Controls.Add(this.menuadministracion);
            this.menu.Controls.Add(this.menuusuarios);
            this.menu.Controls.Add(this.menuventas);
            this.menu.Controls.Add(this.menucompras);
            this.menu.Controls.Add(this.menuproveedores);
            this.menu.Controls.Add(this.menuapartado);
            this.menu.Controls.Add(this.menuclientes);
            this.menu.Location = new System.Drawing.Point(0, 40);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(187, 443);
            this.menu.TabIndex = 10;
            // 
            // ddmAdministracion
            // 
            this.ddmAdministracion.IsMainMenu = false;
            this.ddmAdministracion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuCategoria,
            this.SubMenuProducto,
            this.SubMenuNegocio});
            this.ddmAdministracion.MenuItemHeight = 25;
            this.ddmAdministracion.MenuItemTextColor = System.Drawing.Color.DimGray;
            this.ddmAdministracion.Name = "dropdownMenu1";
            this.ddmAdministracion.PrimaryColor = System.Drawing.Color.MediumSlateBlue;
            this.ddmAdministracion.Size = new System.Drawing.Size(148, 70);
            // 
            // SubMenuCategoria
            // 
            this.SubMenuCategoria.Name = "SubMenuCategoria";
            this.SubMenuCategoria.Size = new System.Drawing.Size(147, 22);
            this.SubMenuCategoria.Text = "Categoria";
            this.SubMenuCategoria.Click += new System.EventHandler(this.SubMenuCategoria_Click);
            // 
            // SubMenuProducto
            // 
            this.SubMenuProducto.Name = "SubMenuProducto";
            this.SubMenuProducto.Size = new System.Drawing.Size(147, 22);
            this.SubMenuProducto.Text = "Producto";
            this.SubMenuProducto.Click += new System.EventHandler(this.SubMenuProducto_Click);
            // 
            // SubMenuNegocio
            // 
            this.SubMenuNegocio.Name = "SubMenuNegocio";
            this.SubMenuNegocio.Size = new System.Drawing.Size(147, 22);
            this.SubMenuNegocio.Text = "Otros Detalles";
            this.SubMenuNegocio.Click += new System.EventHandler(this.SubMenuNegocio_Click);
            // 
            // ddmVenta
            // 
            this.ddmVenta.IsMainMenu = false;
            this.ddmVenta.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuRegVenta,
            this.SubMenuDetVenta,
            this.SubMenuAbonoVenta});
            this.ddmVenta.MenuItemHeight = 25;
            this.ddmVenta.MenuItemTextColor = System.Drawing.Color.DimGray;
            this.ddmVenta.Name = "dropdownMenu2";
            this.ddmVenta.PrimaryColor = System.Drawing.Color.MediumSlateBlue;
            this.ddmVenta.Size = new System.Drawing.Size(130, 70);
            // 
            // SubMenuRegVenta
            // 
            this.SubMenuRegVenta.Name = "SubMenuRegVenta";
            this.SubMenuRegVenta.Size = new System.Drawing.Size(129, 22);
            this.SubMenuRegVenta.Text = "Registrar";
            this.SubMenuRegVenta.Click += new System.EventHandler(this.SubMenuRegVenta_Click);
            // 
            // SubMenuDetVenta
            // 
            this.SubMenuDetVenta.Name = "SubMenuDetVenta";
            this.SubMenuDetVenta.Size = new System.Drawing.Size(129, 22);
            this.SubMenuDetVenta.Text = "Ver Detalle";
            this.SubMenuDetVenta.Click += new System.EventHandler(this.SubMenuDetVenta_Click);
            // 
            // SubMenuAbonoVenta
            // 
            this.SubMenuAbonoVenta.Name = "SubMenuAbonoVenta";
            this.SubMenuAbonoVenta.Size = new System.Drawing.Size(129, 22);
            this.SubMenuAbonoVenta.Text = "Abono";
            this.SubMenuAbonoVenta.Click += new System.EventHandler(this.SubMenuAbonoVenta_Click);
            // 
            // ddmCompra
            // 
            this.ddmCompra.IsMainMenu = false;
            this.ddmCompra.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuRegIngreso,
            this.SubMenuDetIngreso,
            this.SubMenuAbonoIngreso});
            this.ddmCompra.MenuItemHeight = 25;
            this.ddmCompra.MenuItemTextColor = System.Drawing.Color.DimGray;
            this.ddmCompra.Name = "dropdownMenu3";
            this.ddmCompra.PrimaryColor = System.Drawing.Color.MediumSlateBlue;
            this.ddmCompra.Size = new System.Drawing.Size(135, 70);
            // 
            // SubMenuRegIngreso
            // 
            this.SubMenuRegIngreso.Name = "SubMenuRegIngreso";
            this.SubMenuRegIngreso.Size = new System.Drawing.Size(134, 22);
            this.SubMenuRegIngreso.Text = "Registrar";
            this.SubMenuRegIngreso.Click += new System.EventHandler(this.SubMenuRegIngreso_Click);
            // 
            // SubMenuDetIngreso
            // 
            this.SubMenuDetIngreso.Name = "SubMenuDetIngreso";
            this.SubMenuDetIngreso.Size = new System.Drawing.Size(134, 22);
            this.SubMenuDetIngreso.Text = "Ver Detalles";
            this.SubMenuDetIngreso.Click += new System.EventHandler(this.SubMenuDetIngreso_Click);
            // 
            // SubMenuAbonoIngreso
            // 
            this.SubMenuAbonoIngreso.Name = "SubMenuAbonoIngreso";
            this.SubMenuAbonoIngreso.Size = new System.Drawing.Size(134, 22);
            this.SubMenuAbonoIngreso.Text = "Abono";
            this.SubMenuAbonoIngreso.Click += new System.EventHandler(this.SubMenuAbonoIngreso_Click);
            // 
            // ddmReportes
            // 
            this.ddmReportes.IsMainMenu = false;
            this.ddmReportes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuRepVenta,
            this.SubMenuRepIngreso});
            this.ddmReportes.MenuItemHeight = 25;
            this.ddmReportes.MenuItemTextColor = System.Drawing.Color.DimGray;
            this.ddmReportes.Name = "dropdownMenu4";
            this.ddmReportes.PrimaryColor = System.Drawing.Color.MediumSlateBlue;
            this.ddmReportes.Size = new System.Drawing.Size(163, 48);
            // 
            // SubMenuRepVenta
            // 
            this.SubMenuRepVenta.Name = "SubMenuRepVenta";
            this.SubMenuRepVenta.Size = new System.Drawing.Size(162, 22);
            this.SubMenuRepVenta.Text = "Reporte Ventas";
            this.SubMenuRepVenta.Click += new System.EventHandler(this.SubMenuRepVenta_Click);
            // 
            // SubMenuRepIngreso
            // 
            this.SubMenuRepIngreso.Name = "SubMenuRepIngreso";
            this.SubMenuRepIngreso.Size = new System.Drawing.Size(162, 22);
            this.SubMenuRepIngreso.Text = "Reporte Ingresos";
            this.SubMenuRepIngreso.Click += new System.EventHandler(this.SubMenuRepIngreso_Click);
            // 
            // ddmDevoluciones
            // 
            this.ddmDevoluciones.IsMainMenu = false;
            this.ddmDevoluciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.devolverVentaToolStripMenuItem,
            this.SubMenuDevlProv});
            this.ddmDevoluciones.MenuItemHeight = 25;
            this.ddmDevoluciones.MenuItemTextColor = System.Drawing.Color.DimGray;
            this.ddmDevoluciones.Name = "dropdownMenu5";
            this.ddmDevoluciones.PrimaryColor = System.Drawing.Color.MediumSlateBlue;
            this.ddmDevoluciones.Size = new System.Drawing.Size(190, 48);
            // 
            // devolverVentaToolStripMenuItem
            // 
            this.devolverVentaToolStripMenuItem.Name = "devolverVentaToolStripMenuItem";
            this.devolverVentaToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.devolverVentaToolStripMenuItem.Text = "Devolver Venta";
            this.devolverVentaToolStripMenuItem.Click += new System.EventHandler(this.devolverVentaToolStripMenuItem_Click);
            // 
            // SubMenuDevlProv
            // 
            this.SubMenuDevlProv.Name = "SubMenuDevlProv";
            this.SubMenuDevlProv.Size = new System.Drawing.Size(189, 22);
            this.SubMenuDevlProv.Text = "Devolver al Proveedor";
            this.SubMenuDevlProv.Click += new System.EventHandler(this.SubMenuDevlProv_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1072, 633);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuacercade);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.contenedor);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.menu.ResumeLayout(false);
            this.ddmAdministracion.ResumeLayout(false);
            this.ddmVenta.ResumeLayout(false);
            this.ddmCompra.ResumeLayout(false);
            this.ddmReportes.ResumeLayout(false);
            this.ddmDevoluciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button menuacercade;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button menuproveedores;
        private System.Windows.Forms.Button menuclientes;
        private System.Windows.Forms.Button menureportes;
        private System.Windows.Forms.Button menudevolución;
        private System.Windows.Forms.Button menucompras;
        private System.Windows.Forms.Button menuventas;
        private System.Windows.Forms.Button menuapartado;
        private System.Windows.Forms.Button menuusuarios;
        private System.Windows.Forms.Panel contenedor;
        private Controls.DropdownMenu ddmAdministracion;
        private System.Windows.Forms.ToolStripMenuItem SubMenuCategoria;
        private System.Windows.Forms.ToolStripMenuItem SubMenuProducto;
        private System.Windows.Forms.ToolStripMenuItem SubMenuNegocio;
        private Controls.DropdownMenu ddmVenta;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRegVenta;
        private System.Windows.Forms.ToolStripMenuItem SubMenuDetVenta;
        private System.Windows.Forms.ToolStripMenuItem SubMenuAbonoVenta;
        private Controls.DropdownMenu ddmCompra;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRegIngreso;
        private System.Windows.Forms.ToolStripMenuItem SubMenuDetIngreso;
        private System.Windows.Forms.ToolStripMenuItem SubMenuAbonoIngreso;
        private Controls.DropdownMenu ddmReportes;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRepVenta;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRepIngreso;
        private Controls.DropdownMenu ddmDevoluciones;
        private System.Windows.Forms.ToolStripMenuItem devolverVentaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SubMenuDevlProv;
        private System.Windows.Forms.Button menuadministracion;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Panel menu;
    }
}