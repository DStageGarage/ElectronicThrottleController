namespace DStage_ETC_v2
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxCOM = new System.Windows.Forms.ComboBox();
            this.buttonSerialOpen = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_read = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxData = new System.Windows.Forms.GroupBox();
            this.button_write = new System.Windows.Forms.Button();
            this.button_burn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.groupBoxCalibration = new System.Windows.Forms.GroupBox();
            this.buttonCaliClosed = new System.Windows.Forms.Button();
            this.buttonCaliOpen = new System.Windows.Forms.Button();
            this.buttonCaliBurn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBoxData.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxCalibration.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(599, 244);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(54, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "% / RPM";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 57600;
            this.serialPort1.PortName = "COM6";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Port";
            // 
            // comboBoxCOM
            // 
            this.comboBoxCOM.FormattingEnabled = true;
            this.comboBoxCOM.Location = new System.Drawing.Point(43, 21);
            this.comboBoxCOM.Name = "comboBoxCOM";
            this.comboBoxCOM.Size = new System.Drawing.Size(75, 21);
            this.comboBoxCOM.TabIndex = 3;
            // 
            // buttonSerialOpen
            // 
            this.buttonSerialOpen.Location = new System.Drawing.Point(14, 48);
            this.buttonSerialOpen.Name = "buttonSerialOpen";
            this.buttonSerialOpen.Size = new System.Drawing.Size(104, 23);
            this.buttonSerialOpen.TabIndex = 0;
            this.buttonSerialOpen.Text = "Connect";
            this.buttonSerialOpen.UseVisualStyleBackColor = true;
            this.buttonSerialOpen.Click += new System.EventHandler(this.buttonSerialOpen_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(366, 91);
            this.textBox1.TabIndex = 6;
            // 
            // button_read
            // 
            this.button_read.Location = new System.Drawing.Point(13, 19);
            this.button_read.Name = "button_read";
            this.button_read.Size = new System.Drawing.Size(104, 32);
            this.button_read.TabIndex = 6;
            this.button_read.Text = "Read from ETC";
            this.button_read.UseVisualStyleBackColor = true;
            this.button_read.Click += new System.EventHandler(this.button_read_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.buttonSerialOpen);
            this.groupBox1.Controls.Add(this.comboBoxCOM);
            this.groupBox1.Location = new System.Drawing.Point(617, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 82);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // groupBoxData
            // 
            this.groupBoxData.Controls.Add(this.button_write);
            this.groupBoxData.Controls.Add(this.button_burn);
            this.groupBoxData.Controls.Add(this.button_read);
            this.groupBoxData.Enabled = false;
            this.groupBoxData.Location = new System.Drawing.Point(618, 101);
            this.groupBoxData.Name = "groupBoxData";
            this.groupBoxData.Size = new System.Drawing.Size(132, 155);
            this.groupBoxData.TabIndex = 8;
            this.groupBoxData.TabStop = false;
            this.groupBoxData.Text = "Data";
            // 
            // button_write
            // 
            this.button_write.Location = new System.Drawing.Point(13, 95);
            this.button_write.Name = "button_write";
            this.button_write.Size = new System.Drawing.Size(104, 54);
            this.button_write.TabIndex = 8;
            this.button_write.Text = "Write to ETC";
            this.button_write.UseVisualStyleBackColor = true;
            this.button_write.Click += new System.EventHandler(this.button_write_Click);
            // 
            // button_burn
            // 
            this.button_burn.Location = new System.Drawing.Point(13, 57);
            this.button_burn.Name = "button_burn";
            this.button_burn.Size = new System.Drawing.Size(104, 32);
            this.button_burn.TabIndex = 7;
            this.button_burn.Text = "Burn to EEPROM";
            this.button_burn.UseVisualStyleBackColor = true;
            this.button_burn.Click += new System.EventHandler(this.button_burn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(12, 262);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(399, 125);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Info";
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Enabled = false;
            this.groupBoxFile.Location = new System.Drawing.Point(618, 262);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(132, 125);
            this.groupBoxFile.TabIndex = 10;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "File";
            // 
            // groupBoxCalibration
            // 
            this.groupBoxCalibration.Controls.Add(this.label3);
            this.groupBoxCalibration.Controls.Add(this.label2);
            this.groupBoxCalibration.Controls.Add(this.buttonCaliBurn);
            this.groupBoxCalibration.Controls.Add(this.buttonCaliOpen);
            this.groupBoxCalibration.Controls.Add(this.buttonCaliClosed);
            this.groupBoxCalibration.Enabled = false;
            this.groupBoxCalibration.Location = new System.Drawing.Point(417, 262);
            this.groupBoxCalibration.Name = "groupBoxCalibration";
            this.groupBoxCalibration.Size = new System.Drawing.Size(194, 125);
            this.groupBoxCalibration.TabIndex = 11;
            this.groupBoxCalibration.TabStop = false;
            this.groupBoxCalibration.Text = "Calibration";
            // 
            // buttonCaliClosed
            // 
            this.buttonCaliClosed.Location = new System.Drawing.Point(71, 19);
            this.buttonCaliClosed.Name = "buttonCaliClosed";
            this.buttonCaliClosed.Size = new System.Drawing.Size(116, 29);
            this.buttonCaliClosed.TabIndex = 0;
            this.buttonCaliClosed.Text = "Save closed position";
            this.buttonCaliClosed.UseVisualStyleBackColor = true;
            this.buttonCaliClosed.Click += new System.EventHandler(this.buttonCaliClosed_Click);
            // 
            // buttonCaliOpen
            // 
            this.buttonCaliOpen.Location = new System.Drawing.Point(71, 54);
            this.buttonCaliOpen.Name = "buttonCaliOpen";
            this.buttonCaliOpen.Size = new System.Drawing.Size(116, 29);
            this.buttonCaliOpen.TabIndex = 1;
            this.buttonCaliOpen.Text = "Save opend position";
            this.buttonCaliOpen.UseVisualStyleBackColor = true;
            this.buttonCaliOpen.Click += new System.EventHandler(this.buttonCaliOpen_Click);
            // 
            // buttonCaliBurn
            // 
            this.buttonCaliBurn.Location = new System.Drawing.Point(9, 89);
            this.buttonCaliBurn.Name = "buttonCaliBurn";
            this.buttonCaliBurn.Size = new System.Drawing.Size(178, 29);
            this.buttonCaliBurn.TabIndex = 2;
            this.buttonCaliBurn.Text = "Burn to EEPROM";
            this.buttonCaliBurn.UseVisualStyleBackColor = true;
            this.buttonCaliBurn.Click += new System.EventHandler(this.buttonCaliBurn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "De-press ->";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Press ->";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 396);
            this.Controls.Add(this.groupBoxCalibration);
            this.Controls.Add(this.groupBoxFile);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Location = new System.Drawing.Point(200, 200);
            this.Name = "Form1";
            this.Text = "DStage ETC v2.x APP v0.1 beta";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxData.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxCalibration.ResumeLayout(false);
            this.groupBoxCalibration.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_read;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxCOM;
        private System.Windows.Forms.Button buttonSerialOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxData;
        private System.Windows.Forms.Button button_write;
        private System.Windows.Forms.Button button_burn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.GroupBox groupBoxCalibration;
        private System.Windows.Forms.Button buttonCaliOpen;
        private System.Windows.Forms.Button buttonCaliClosed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCaliBurn;
    }
}

