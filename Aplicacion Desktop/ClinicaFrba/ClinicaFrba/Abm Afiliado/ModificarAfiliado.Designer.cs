namespace ClinicaFrba.Abm_Afiliado
{
    partial class ModificarAfiliado
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
            this.groupBoxDatosPersonales = new System.Windows.Forms.GroupBox();
            this.comboBoxEstadoCivil = new System.Windows.Forms.ComboBox();
            this.comboBoxSexo = new System.Windows.Forms.ComboBox();
            this.textBoxMail = new System.Windows.Forms.TextBox();
            this.textBoxTelefono = new System.Windows.Forms.TextBox();
            this.textBoxDireccion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonBaja = new System.Windows.Forms.Button();
            this.groupBoxBaja = new System.Windows.Forms.GroupBox();
            this.groupBoxDatosAfiliado = new System.Windows.Forms.GroupBox();
            this.comboBoxPlan = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelUsuario = new System.Windows.Forms.Label();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.buttonModificar = new System.Windows.Forms.Button();
            this.labelNroAfil = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonGrupo = new System.Windows.Forms.Button();
            this.groupBoxDatosPersonales.SuspendLayout();
            this.groupBoxBaja.SuspendLayout();
            this.groupBoxDatosAfiliado.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxDatosPersonales
            // 
            this.groupBoxDatosPersonales.Controls.Add(this.comboBoxEstadoCivil);
            this.groupBoxDatosPersonales.Controls.Add(this.comboBoxSexo);
            this.groupBoxDatosPersonales.Controls.Add(this.textBoxMail);
            this.groupBoxDatosPersonales.Controls.Add(this.textBoxTelefono);
            this.groupBoxDatosPersonales.Controls.Add(this.textBoxDireccion);
            this.groupBoxDatosPersonales.Controls.Add(this.label8);
            this.groupBoxDatosPersonales.Controls.Add(this.label7);
            this.groupBoxDatosPersonales.Controls.Add(this.label6);
            this.groupBoxDatosPersonales.Controls.Add(this.label5);
            this.groupBoxDatosPersonales.Controls.Add(this.label4);
            this.groupBoxDatosPersonales.Location = new System.Drawing.Point(12, 129);
            this.groupBoxDatosPersonales.Name = "groupBoxDatosPersonales";
            this.groupBoxDatosPersonales.Size = new System.Drawing.Size(357, 175);
            this.groupBoxDatosPersonales.TabIndex = 1;
            this.groupBoxDatosPersonales.TabStop = false;
            this.groupBoxDatosPersonales.Text = "Datos Personales";
            // 
            // comboBoxEstadoCivil
            // 
            this.comboBoxEstadoCivil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEstadoCivil.FormattingEnabled = true;
            this.comboBoxEstadoCivil.Location = new System.Drawing.Point(107, 149);
            this.comboBoxEstadoCivil.Name = "comboBoxEstadoCivil";
            this.comboBoxEstadoCivil.Size = new System.Drawing.Size(244, 21);
            this.comboBoxEstadoCivil.TabIndex = 18;
            // 
            // comboBoxSexo
            // 
            this.comboBoxSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSexo.FormattingEnabled = true;
            this.comboBoxSexo.Items.AddRange(new object[] {
            "M",
            "F"});
            this.comboBoxSexo.Location = new System.Drawing.Point(107, 122);
            this.comboBoxSexo.Name = "comboBoxSexo";
            this.comboBoxSexo.Size = new System.Drawing.Size(70, 21);
            this.comboBoxSexo.TabIndex = 17;
            // 
            // textBoxMail
            // 
            this.textBoxMail.Location = new System.Drawing.Point(107, 90);
            this.textBoxMail.Name = "textBoxMail";
            this.textBoxMail.Size = new System.Drawing.Size(244, 20);
            this.textBoxMail.TabIndex = 16;
            // 
            // textBoxTelefono
            // 
            this.textBoxTelefono.Location = new System.Drawing.Point(107, 61);
            this.textBoxTelefono.Name = "textBoxTelefono";
            this.textBoxTelefono.Size = new System.Drawing.Size(244, 20);
            this.textBoxTelefono.TabIndex = 15;
            // 
            // textBoxDireccion
            // 
            this.textBoxDireccion.Location = new System.Drawing.Point(107, 31);
            this.textBoxDireccion.Name = "textBoxDireccion";
            this.textBoxDireccion.Size = new System.Drawing.Size(244, 20);
            this.textBoxDireccion.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Estado Civil:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Sexo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Mail:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Telefono:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Direccion: ";
            // 
            // buttonBaja
            // 
            this.buttonBaja.Location = new System.Drawing.Point(112, 29);
            this.buttonBaja.Name = "buttonBaja";
            this.buttonBaja.Size = new System.Drawing.Size(138, 34);
            this.buttonBaja.TabIndex = 0;
            this.buttonBaja.UseVisualStyleBackColor = true;
            this.buttonBaja.Click += new System.EventHandler(this.buttonBaja_Click);
            // 
            // groupBoxBaja
            // 
            this.groupBoxBaja.Controls.Add(this.buttonBaja);
            this.groupBoxBaja.Location = new System.Drawing.Point(12, 38);
            this.groupBoxBaja.Name = "groupBoxBaja";
            this.groupBoxBaja.Size = new System.Drawing.Size(357, 85);
            this.groupBoxBaja.TabIndex = 5;
            this.groupBoxBaja.TabStop = false;
            this.groupBoxBaja.Text = "Baja";
            // 
            // groupBoxDatosAfiliado
            // 
            this.groupBoxDatosAfiliado.Controls.Add(this.buttonGrupo);
            this.groupBoxDatosAfiliado.Controls.Add(this.label2);
            this.groupBoxDatosAfiliado.Controls.Add(this.richTextBox1);
            this.groupBoxDatosAfiliado.Controls.Add(this.comboBoxPlan);
            this.groupBoxDatosAfiliado.Controls.Add(this.label9);
            this.groupBoxDatosAfiliado.Controls.Add(this.label10);
            this.groupBoxDatosAfiliado.Location = new System.Drawing.Point(15, 310);
            this.groupBoxDatosAfiliado.Name = "groupBoxDatosAfiliado";
            this.groupBoxDatosAfiliado.Size = new System.Drawing.Size(354, 182);
            this.groupBoxDatosAfiliado.TabIndex = 2;
            this.groupBoxDatosAfiliado.TabStop = false;
            this.groupBoxDatosAfiliado.Text = "Datos como afiliado";
            // 
            // comboBoxPlan
            // 
            this.comboBoxPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPlan.FormattingEnabled = true;
            this.comboBoxPlan.Location = new System.Drawing.Point(107, 53);
            this.comboBoxPlan.Name = "comboBoxPlan";
            this.comboBoxPlan.Size = new System.Drawing.Size(244, 21);
            this.comboBoxPlan.TabIndex = 20;
            this.comboBoxPlan.SelectionChangeCommitted += new System.EventHandler(this.comboBoxPlan_SelectionChangeCommitted);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Grupo familiar: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Plan:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Usuario a modificar: ";
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsuario.Location = new System.Drawing.Point(121, 9);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(31, 13);
            this.labelUsuario.TabIndex = 7;
            this.labelUsuario.Text = "xxxx";
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Location = new System.Drawing.Point(213, 496);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelar.TabIndex = 1;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // buttonModificar
            // 
            this.buttonModificar.Location = new System.Drawing.Point(294, 497);
            this.buttonModificar.Name = "buttonModificar";
            this.buttonModificar.Size = new System.Drawing.Size(75, 23);
            this.buttonModificar.TabIndex = 2;
            this.buttonModificar.Text = "Modificar";
            this.buttonModificar.UseVisualStyleBackColor = true;
            this.buttonModificar.Click += new System.EventHandler(this.buttonModificar_Click);
            // 
            // labelNroAfil
            // 
            this.labelNroAfil.AutoSize = true;
            this.labelNroAfil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNroAfil.Location = new System.Drawing.Point(121, 22);
            this.labelNroAfil.Name = "labelNroAfil";
            this.labelNroAfil.Size = new System.Drawing.Size(31, 13);
            this.labelNroAfil.TabIndex = 9;
            this.labelNroAfil.Text = "xxxx";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Numero de afiliado: ";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(15, 498);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 10;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(9, 117);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(339, 48);
            this.richTextBox1.TabIndex = 22;
            this.richTextBox1.Text = "";
            this.richTextBox1.Click += new System.EventHandler(this.richTextBox1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Motivo de cambio de plan:";
            // 
            // buttonGrupo
            // 
            this.buttonGrupo.Location = new System.Drawing.Point(150, 24);
            this.buttonGrupo.Name = "buttonGrupo";
            this.buttonGrupo.Size = new System.Drawing.Size(152, 23);
            this.buttonGrupo.TabIndex = 24;
            this.buttonGrupo.Text = "Ingresar a grupo familiar";
            this.buttonGrupo.UseVisualStyleBackColor = true;
            this.buttonGrupo.Click += new System.EventHandler(this.buttonGrupo_Click);
            // 
            // ModificarAfiliado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 524);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.labelNroAfil);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonModificar);
            this.Controls.Add(this.labelUsuario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxDatosAfiliado);
            this.Controls.Add(this.groupBoxBaja);
            this.Controls.Add(this.groupBoxDatosPersonales);
            this.MaximizeBox = false;
            this.Name = "ModificarAfiliado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modificar afiliado";
            this.groupBoxDatosPersonales.ResumeLayout(false);
            this.groupBoxDatosPersonales.PerformLayout();
            this.groupBoxBaja.ResumeLayout(false);
            this.groupBoxDatosAfiliado.ResumeLayout(false);
            this.groupBoxDatosAfiliado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDatosPersonales;
        private System.Windows.Forms.Button buttonBaja;
        private System.Windows.Forms.GroupBox groupBoxBaja;
        private System.Windows.Forms.GroupBox groupBoxDatosAfiliado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelUsuario;
        private System.Windows.Forms.ComboBox comboBoxEstadoCivil;
        private System.Windows.Forms.ComboBox comboBoxSexo;
        private System.Windows.Forms.TextBox textBoxMail;
        private System.Windows.Forms.TextBox textBoxTelefono;
        private System.Windows.Forms.TextBox textBoxDireccion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxPlan;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.Button buttonModificar;
        private System.Windows.Forms.Label labelNroAfil;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonGrupo;
    }
}