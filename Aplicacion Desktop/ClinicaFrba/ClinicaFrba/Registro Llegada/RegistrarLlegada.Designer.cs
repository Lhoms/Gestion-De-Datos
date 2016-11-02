namespace ClinicaFrba.Registro_Llegada
{
    partial class RegistrarLlegada
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Registrar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.buttonAceptar = new System.Windows.Forms.Button();
            this.comboBoxTipoEsp = new System.Windows.Forms.ComboBox();
            this.comboBoxEsp = new System.Windows.Forms.ComboBox();
            this.comboBoxProfesional = new System.Windows.Forms.ComboBox();
            this.textBoxNroAfiliado = new System.Windows.Forms.TextBox();
            this.buttonVolver = new System.Windows.Forms.Button();
            this.labelFechaActual = new System.Windows.Forms.Label();
            this.labelFecha = new System.Windows.Forms.Label();
            this.textBoxBono = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numero de afiliado:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tipo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Especialidad:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Profesional:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxBono);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.buttonAceptar);
            this.groupBox1.Controls.Add(this.comboBoxTipoEsp);
            this.groupBox1.Controls.Add(this.comboBoxEsp);
            this.groupBox1.Controls.Add(this.comboBoxProfesional);
            this.groupBox1.Controls.Add(this.textBoxNroAfiliado);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 418);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Registrar});
            this.dataGridView1.Location = new System.Drawing.Point(9, 241);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(359, 171);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Registrar
            // 
            this.Registrar.HeaderText = "Registrar";
            this.Registrar.Name = "Registrar";
            this.Registrar.Width = 55;
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Location = new System.Drawing.Point(260, 181);
            this.buttonAceptar.Name = "buttonAceptar";
            this.buttonAceptar.Size = new System.Drawing.Size(75, 23);
            this.buttonAceptar.TabIndex = 9;
            this.buttonAceptar.Text = "Aceptar";
            this.buttonAceptar.UseVisualStyleBackColor = true;
            this.buttonAceptar.Click += new System.EventHandler(this.buttonAceptar_Click);
            // 
            // comboBoxTipoEsp
            // 
            this.comboBoxTipoEsp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipoEsp.FormattingEnabled = true;
            this.comboBoxTipoEsp.Location = new System.Drawing.Point(139, 47);
            this.comboBoxTipoEsp.Name = "comboBoxTipoEsp";
            this.comboBoxTipoEsp.Size = new System.Drawing.Size(196, 21);
            this.comboBoxTipoEsp.TabIndex = 6;
            this.comboBoxTipoEsp.SelectionChangeCommitted += new System.EventHandler(this.comboBoxTipoEsp_SelectionChangeCommitted);
            // 
            // comboBoxEsp
            // 
            this.comboBoxEsp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEsp.FormattingEnabled = true;
            this.comboBoxEsp.Location = new System.Drawing.Point(139, 74);
            this.comboBoxEsp.Name = "comboBoxEsp";
            this.comboBoxEsp.Size = new System.Drawing.Size(196, 21);
            this.comboBoxEsp.TabIndex = 7;
            this.comboBoxEsp.SelectionChangeCommitted += new System.EventHandler(this.comboBoxEsp_SelectionChangeCommitted);
            // 
            // comboBoxProfesional
            // 
            this.comboBoxProfesional.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfesional.FormattingEnabled = true;
            this.comboBoxProfesional.Location = new System.Drawing.Point(139, 101);
            this.comboBoxProfesional.Name = "comboBoxProfesional";
            this.comboBoxProfesional.Size = new System.Drawing.Size(196, 21);
            this.comboBoxProfesional.TabIndex = 8;
            // 
            // textBoxNroAfiliado
            // 
            this.textBoxNroAfiliado.Location = new System.Drawing.Point(139, 21);
            this.textBoxNroAfiliado.Name = "textBoxNroAfiliado";
            this.textBoxNroAfiliado.Size = new System.Drawing.Size(196, 20);
            this.textBoxNroAfiliado.TabIndex = 5;
            // 
            // buttonVolver
            // 
            this.buttonVolver.Location = new System.Drawing.Point(311, 427);
            this.buttonVolver.Name = "buttonVolver";
            this.buttonVolver.Size = new System.Drawing.Size(75, 23);
            this.buttonVolver.TabIndex = 10;
            this.buttonVolver.Text = "Volver";
            this.buttonVolver.UseVisualStyleBackColor = true;
            this.buttonVolver.Click += new System.EventHandler(this.buttonVolver_Click);
            // 
            // labelFechaActual
            // 
            this.labelFechaActual.AutoSize = true;
            this.labelFechaActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFechaActual.Location = new System.Drawing.Point(50, 440);
            this.labelFechaActual.Name = "labelFechaActual";
            this.labelFechaActual.Size = new System.Drawing.Size(85, 13);
            this.labelFechaActual.TabIndex = 23;
            this.labelFechaActual.Text = "YYYY-MM-DD";
            // 
            // labelFecha
            // 
            this.labelFecha.AutoSize = true;
            this.labelFecha.Location = new System.Drawing.Point(4, 440);
            this.labelFecha.Name = "labelFecha";
            this.labelFecha.Size = new System.Drawing.Size(40, 13);
            this.labelFecha.TabIndex = 22;
            this.labelFecha.Text = "Fecha:";
            // 
            // textBoxBono
            // 
            this.textBoxBono.Location = new System.Drawing.Point(139, 136);
            this.textBoxBono.Name = "textBoxBono";
            this.textBoxBono.Size = new System.Drawing.Size(196, 20);
            this.textBoxBono.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Bono numero:";
            // 
            // RegistrarLlegada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 462);
            this.Controls.Add(this.labelFechaActual);
            this.Controls.Add(this.labelFecha);
            this.Controls.Add(this.buttonVolver);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "RegistrarLlegada";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar llegada para atencion medica";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxNroAfiliado;
        private System.Windows.Forms.Button buttonAceptar;
        private System.Windows.Forms.ComboBox comboBoxTipoEsp;
        private System.Windows.Forms.ComboBox comboBoxEsp;
        private System.Windows.Forms.ComboBox comboBoxProfesional;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonVolver;
        private System.Windows.Forms.DataGridViewButtonColumn Registrar;
        private System.Windows.Forms.Label labelFechaActual;
        private System.Windows.Forms.Label labelFecha;
        private System.Windows.Forms.TextBox textBoxBono;
        private System.Windows.Forms.Label label5;
    }
}