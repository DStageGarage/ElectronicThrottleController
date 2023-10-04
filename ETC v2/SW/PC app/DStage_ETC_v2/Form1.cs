using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;

namespace DStage_ETC_v2
{
    public partial class Form1 : Form
    {
        public char command_status;
        public delegate void serial_delegate(int indata);
        public int serial_count;
        int col_serial=0, row_serial=0, rpm_temp=0;

        readonly int[] convert_bit2pr = { 0,     0,  1,  1,  2,  2,  2,  3,  3,  4,  4,  4,  5,  5,  5,  6,  6,  7,  7,  7,
8,  8,  9,  9,  9,  10,     10,     11,     11,     11,     12,     12,     13,     13,     13,     14,     14,     15,     15,     15,
16,     16,     16,     17,     17,     18,     18,     18,     19,     19,     20,     20,     20,     21,     21,     22,     22,     22,     23,     23,
24,     24,     24,     25,     25,     25,     26,     26,     27,     27,
27,     28,     28,     29,     29,     29,     30,     30,     31,     31,     31,     32,     32,     33,     33,     33,     34,     34,     35,     35,
35,     36,     36,     36,     37,     37,     38,     38,     38,     39,     39,     40,     40,     40,     41,     41,     42,     42,     42,     43,
43,     44,     44,     44,     45,     45,     45,     46,     46,     47,     47,     47,     48,     48,     49,     49,     49,     50,     50,     51,
51,     51,     52,     52,     53,     53,     53,     54,     54,     55,     55,     55,     56,     56,     56,     57,     57,     58,     58,     58,
59,     59,     60,     60,     60,     61,     61,     62,     62,     62,     63,     63,     64,     64,     64,     65,     65,     65,     66,     66,
67,     67,     67,     68,     68,     69,     69,     69,     70,     70,     71,     71,     71,     72,     72,     73,     73,     73,     74,     74,
75,     75,     75,     76,     76,     76,     77,     77,     78,     78,     78,     79,     79,     80,     80,     80,     81,     81,     82,     82,
82,     83,     83,     84,     84,     84,     85,     85,     85,     86,     86,     87,     87,     87,     88,     88,     89,     89,     89,     90,
90,     91,     91,     91,     92,     92,     93,     93,     93,     94,     94,     95,     95,     95,     96,     96,     96,     97,     97,     98,
98,     98,     99,     99,     100,    100 };

        readonly int[] convert_pr2bit = {0,     3,  5,  8,  10,     13,     15,     18,     20,     23,     26,     28,     31,     33,     36,     38,     41,     43,     46,     48,
51,     54,     56,     59,     61,     64,     66,     69,     71,     74,     77,     79,     82,     84,     87,     89,     92,     94,     97,     99,
102,    105,    107,    110,    112,    115,    117,    120,    122,    125,    128,    130,    133,    135,    138,    140,    143,    145,    148,    150,
153,    156,    158,    161,    163,    166,    168,    171,    173,    176,    179,    181,    184,    186,    189,    191,    194,    196,    199,    201,
204,    207,    209,    212,    214,    217,    219,    222,    224,    227,    230,    232,    235,    237,    240,    242,    245,    247,    250,    252,
255 }; 																			


        // serial communication aliases
        char COM_FREE = '0';
        char COM_GET_FULL_T = 'f';
        char COM_GET_RPM_T = 'r';
        char COM_GET_POS_T = 'p';
        char COM_SEND_FULL_T = 'F';
        char COM_SEND_RPM_T = 'R';
        char COM_SEND_POS_T = 'P';

        public List<PositionColumn> Columns {  get; set; }

        public Form1()
        {
            // init the translation table
            Columns = GetColumns();

            InitializeComponent();

            // init list of available ports
            GetSerialPorts();
        }

        private void GetSerialPorts()
        {
            // Get a list of serial port names
            string[] ports = SerialPort.GetPortNames();

            // Add each port name to the list.
            foreach (string port in ports)
                comboBoxCOM.Items.Add(port);

            // set to first item from the list
            comboBoxCOM.SelectedIndex = 0;
        }

        private List<PositionColumn> GetColumns()
        {
            var columns = new List<PositionColumn>();

            columns.Add(new PositionColumn()
            {
                //PositionRange = 0,
                Rpm0 = 500,
                Rpm1 = 1000,
                Rpm2 = 2000,
                Rpm3 = 3000,
                Rpm4 = 4000,
                Rpm5 = 4500,
                Rpm6 = 5000,
                Rpm7 = 5500,
                Rpm8 = 6000,
                Rpm9 = 7000,
                Rpm10 = 8000,
                Rpm11 = 9000
            });

            columns.Add(new PositionColumn()
            {
                PositionRange = 100,
                Rpm11 = 100,
                Rpm10 = 100,
                Rpm9 = 100,
                Rpm8 = 100,
                Rpm7 = 100,
                Rpm6 = 100,
                Rpm5 = 100,
                Rpm4 = 100,
                Rpm3 = 100,
                Rpm2 = 100,
                Rpm1 = 100,
                Rpm0 = 100
            });

            columns.Add(new PositionColumn()
            {
                PositionRange = 90,
                Rpm11 = 90,
                Rpm10 = 90,
                Rpm9 = 90,
                Rpm8 = 90,
                Rpm7 = 90,
                Rpm6 = 90,
                Rpm5 = 90,
                Rpm4 = 90,
                Rpm3 = 90,
                Rpm2 = 90,
                Rpm1 = 90,
                Rpm0 = 90
            });

            columns.Add(new PositionColumn()
            {
                PositionRange = 80,
                Rpm11 = 80,
                Rpm10 = 80,
                Rpm9 = 80,
                Rpm8 = 80,
                Rpm7 = 80,
                Rpm6 = 80,
                Rpm5 = 80,
                Rpm4 = 80,
                Rpm3 = 80,
                Rpm2 = 80,
                Rpm1 = 80,
                Rpm0 = 80
            });

            columns.Add(new PositionColumn()
            {
                PositionRange = 70,
                Rpm11 = 70,
                Rpm10 = 70,
                Rpm9 = 70,
                Rpm8 = 70,
                Rpm7 = 70,
                Rpm6 = 70,
                Rpm5 = 70,
                Rpm4 = 70,
                Rpm3 = 70,
                Rpm2 = 70,
                Rpm1 = 70,
                Rpm0 = 70
            });

            columns.Add(new PositionColumn()
            {
                PositionRange = 60,
                Rpm11 = 60,
                Rpm10 = 60,
                Rpm9 = 60,
                Rpm8 = 60,
                Rpm7 = 60,
                Rpm6 = 60,
                Rpm5 = 60,
                Rpm4 = 60,
                Rpm3 = 60,
                Rpm2 = 60,
                Rpm1 = 60,
                Rpm0 = 60
            });

            columns.Add(new PositionColumn()
            {
                PositionRange = 50,
                Rpm11 = 50,
                Rpm10 = 50,
                Rpm9 = 50,
                Rpm8 = 50,
                Rpm7 = 50,
                Rpm6 = 50,
                Rpm5 = 50,
                Rpm4 = 50,
                Rpm3 = 50,
                Rpm2 = 50,
                Rpm1 = 50,
                Rpm0 = 50
            });

            columns.Add(new PositionColumn()
            {
                PositionRange = 40,
                Rpm11 = 40,
                Rpm10 = 40,
                Rpm9 = 40,
                Rpm8 = 40,
                Rpm7 = 40,
                Rpm6 = 40,
                Rpm5 = 40,
                Rpm4 = 40,
                Rpm3 = 40,
                Rpm2 = 40,
                Rpm1 = 40,
                Rpm0 = 40
            });

            columns.Add(new PositionColumn()
            {
                PositionRange = 30,
                Rpm11 = 30,
                Rpm10 = 30,
                Rpm9 = 30,
                Rpm8 = 30,
                Rpm7 = 30,
                Rpm6 = 30,
                Rpm5 = 30,
                Rpm4 = 30,
                Rpm3 = 30,
                Rpm2 = 30,
                Rpm1 = 30,
                Rpm0 = 30
            });

            columns.Add(new PositionColumn()
            {
                PositionRange = 20,
                Rpm11 = 20,
                Rpm10 = 20,
                Rpm9 = 20,
                Rpm8 = 20,
                Rpm7 = 20,
                Rpm6 = 20,
                Rpm5 = 20,
                Rpm4 = 20,
                Rpm3 = 20,
                Rpm2 = 20,
                Rpm1 = 20,
                Rpm0 = 20
            });

            columns.Add(new PositionColumn()
            {
                PositionRange = 10,
                Rpm11 = 10,
                Rpm10 = 10,
                Rpm9 = 10,
                Rpm8 = 10,
                Rpm7 = 10,
                Rpm6 = 10,
                Rpm5 = 10,
                Rpm4 = 10,
                Rpm3 = 10,
                Rpm2 = 10,
                Rpm1 = 10,
                Rpm0 = 10
            });

            columns.Add(new PositionColumn()
            {
                PositionRange = 5,
                Rpm11 = 5,
                Rpm10 = 5,
                Rpm9 = 5,
                Rpm8 = 5,
                Rpm7 = 5,
                Rpm6 = 5,
                Rpm5 = 5,
                Rpm4 = 5,
                Rpm3 = 5,
                Rpm2 = 5,
                Rpm1 = 5,
                Rpm0 = 5
            });

            columns.Add(new PositionColumn()
            {
                PositionRange = 0,
                Rpm11 = 0,
                Rpm10 = 0,
                Rpm9 = 0,
                Rpm8 = 0,
                Rpm7 = 0,
                Rpm6 = 0,
                Rpm5 = 0,
                Rpm4 = 0,
                Rpm3 = 0,
                Rpm2 = 0,
                Rpm1 = 0,
                Rpm0 = 0
            });

            return columns;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var columns = this.Columns;

            dataGridView1.DataSource = columns;

            dataGridView1.Columns[0].HeaderText = "         ";
            dataGridView1.Columns[1].HeaderText = "MIN";
            dataGridView1.Columns[2].HeaderText = "   ";
            dataGridView1.Columns[3].HeaderText = "   ";
            dataGridView1.Columns[4].HeaderText = "   ";
            dataGridView1.Columns[5].HeaderText = "   ";
            dataGridView1.Columns[6].HeaderText = "   ";
            dataGridView1.Columns[7].HeaderText = "   ";
            dataGridView1.Columns[8].HeaderText = "   ";
            dataGridView1.Columns[9].HeaderText = "   ";
            dataGridView1.Columns[10].HeaderText = "   ";
            dataGridView1.Columns[11].HeaderText = "   ";
            dataGridView1.Columns[12].HeaderText = "MAX";

            // load cell colours
            for (int r = 1; r < 13; r++)
                for (int c = 1; c < 13; c++)
                    this.cellColorRefresh(r, c);

            // top and bottom values of position have to stay 0 and 100
            dataGridView1.Rows[0].Cells[0].ReadOnly = true;
            dataGridView1.Rows[1].Cells[0].ReadOnly = true;
            dataGridView1.Rows[12].Cells[0].ReadOnly = true;
            dataGridView1.Rows[1].Cells[0].Style.ForeColor = Color.CornflowerBlue;
            dataGridView1.Rows[12].Cells[0].Style.ForeColor = Color.CornflowerBlue;

            // set background color for headers
            for(int i=1;i<13;i++)
            { 
                dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.LightGray;
                dataGridView1.Rows[0].Cells[i].Style.BackColor = Color.LightGray;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int r_i, c_i;

            c_i = e.ColumnIndex;
            r_i = e.RowIndex;
            
            // refresh color for inner part of the table
            if((c_i > 0) && (r_i > 0))
                this.cellColorRefresh(r_i, c_i);
        }


        private void cellColorRefresh(int r_i, int c_i)
        {
            int color_r, color_g, color_b;

            // multiply by 2.5 to go from % to 8bit value
            color_r = (int)this.dataGridView1.Rows[r_i].Cells[c_i].Value;
            color_r = (int)((float)color_r * 2.5);
            color_g = 100 - (int)this.dataGridView1.Rows[r_i].Cells[c_i].Value;
            color_g = (int)((float)color_g * 2.5);
            color_b = 0;

            this.dataGridView1[c_i, r_i].Style.BackColor = Color.FromArgb(color_r, color_g, color_b);
        }

        private void buttonSerialOpen_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                buttonSerialOpen.Text = "Connect";
                groupBoxData.Enabled = false;
                groupBoxCalibration.Enabled = false;
                comboBoxCOM.Enabled = true;
            }
            else
            {
                serialPort1.PortName = comboBoxCOM.Text;
                serialPort1.Open();
                buttonSerialOpen.Text = "Disconnect";
                groupBoxData.Enabled = true;
                groupBoxCalibration.Enabled = true;
                comboBoxCOM.Enabled = false;
            }
        }

        private void button_read_Click(object sender, EventArgs e)
        {
            groupBoxData.Enabled = false;
            groupBoxCalibration.Enabled = false;

            // command for geting whole translation table
            // TODO: how to put char here?
            serialPort1.Write("f");
            command_status = COM_GET_FULL_T;
            serial_count = 144;
        }

        private void button_write_Click(object sender, EventArgs e)
        {
            int r_i, c_i, byte_temp;

            groupBoxData.Enabled = false;
            groupBoxCalibration.Enabled = false;
            textBox1.Text += "\nwrite to RAM: ";

            // command for sending whole translation table
            serialPort1.Write("F");

            // send whole translation table
            for (c_i = 12; c_i > 0; c_i--)
                for (r_i = 12; r_i > 0; r_i--)
                {
                    while(serialPort1.BytesToWrite > 0)
                        button_write.Enabled = false;

                    var dataByte = new byte[1];
                    byte_temp = (int)dataGridView1.Rows[13-r_i].Cells[c_i].Value;
                    //byte_temp = (int)((float)byte_temp * 2.55);
                    byte_temp = convert_pr2bit[byte_temp];
                    dataByte[0] = (byte)byte_temp;
                    serialPort1.Write(dataByte, 0, 1);

                    textBox1.Text = textBox1.Text + r_i.ToString();
                }

            while (serialPort1.BytesToWrite > 0)
                button_write.Enabled = false;
          
            // command for sending position ranges table
            serialPort1.Write("P");

            for (r_i = 12; r_i > 0; r_i--)
            {
                while (serialPort1.BytesToWrite > 0)
                    button_write.Enabled = false;

                var dataByte = new byte[1];
                byte_temp = (int)dataGridView1.Rows[13 - r_i].Cells[0].Value;
                byte_temp = convert_pr2bit[byte_temp];
                dataByte[0] = (byte)byte_temp;
                serialPort1.Write(dataByte, 0, 1);

                textBox1.Text = textBox1.Text + r_i.ToString();
            }

            while (serialPort1.BytesToWrite > 0)
                button_write.Enabled = false;

            // command for sending RPM ranges table
            serialPort1.Write("R");

            for (c_i = 12; c_i > 0; c_i--)
            {
                while (serialPort1.BytesToWrite > 0)
                    button_write.Enabled = false;

                var dataByte = new byte[1];
                byte_temp = ((int)dataGridView1.Rows[0].Cells[c_i].Value) & 0xFF;
                //byte_temp = convert_pr2bit[byte_temp];
                dataByte[0] = (byte)byte_temp;
                serialPort1.Write(dataByte, 0, 1);

                while (serialPort1.BytesToWrite > 0)
                    button_write.Enabled = false;

                byte_temp = ((int)dataGridView1.Rows[0].Cells[c_i].Value) >> 8;
                //byte_temp = convert_pr2bit[byte_temp];
                dataByte[0] = (byte)byte_temp;
                serialPort1.Write(dataByte, 0, 1);

                textBox1.Text = textBox1.Text + c_i.ToString();
            }

            button_write.Enabled = true;
            textBox1.Text += " write done \n";
            groupBoxData.Enabled = true;
            groupBoxCalibration.Enabled = true;
        }

        private void button_burn_Click(object sender, EventArgs e)
        {
            // command for byrning data to EEPROM
            serialPort1.Write("B");

            textBox1.Text += "\n Burn to EEPROM done \n";
        }

        private void buttonCaliClosed_Click(object sender, EventArgs e)
        {
            // command for saving de-presed gas pedal position to RAM
            serialPort1.Write("C");

            textBox1.Text += "\n de-presed gas pedal position saved \n";
        }

        private void buttonCaliOpen_Click(object sender, EventArgs e)
        {
            // command for saving presed gas pedal position to RAM
            serialPort1.Write("O");

            textBox1.Text += "\n presed gas pedal position saved \n";
        }

        private void buttonCaliBurn_Click(object sender, EventArgs e)
        {
            // command for saving gas pedal calibration to EEPROM
            serialPort1.Write("E");

            textBox1.Text += "\n gas pedal calibration burned to EEPROM \n";
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //string indata = serialPort1.ReadLine();
            int indata = serialPort1.ReadByte();
            serial_delegate writeit = new serial_delegate(receive_from_serial);
            Invoke(writeit, indata);
        }

        public void receive_from_serial(int indata)
        {
            // receiving position ranges
            if (command_status == COM_GET_POS_T)
            {
                int norm_data;
                //norm_data = (int)((float)indata / 2.55);
                norm_data = convert_bit2pr[indata];
                dataGridView1.Rows[13-serial_count].Cells[0].Value = norm_data;

                serial_count--;

                // end of transmition
                if (serial_count == 0)
                {
                    // end of recieving
                    command_status = COM_FREE;

                    groupBoxData.Enabled = true;
                    groupBoxCalibration.Enabled = true;
                }
            }

            // receiving RPM ranges
            if (command_status == COM_GET_RPM_T)
            {

                if ((serial_count & 0x01) == 0)
                {
                    // even bytes = LSB
                    rpm_temp = indata;
                }
                else
                {
                    // odd bytes = MSB
                    rpm_temp = rpm_temp + (indata << 8);

                    dataGridView1.Rows[0].Cells[(serial_count >> 1) + 1].Value = rpm_temp;
                }

                serial_count--;

                // end of transmition
                if (serial_count == 0)
                {
                    // move to getting position ranges
                    command_status = COM_GET_POS_T;
                    serial_count = 12;

                    serialPort1.Write("p");
                    //return;
                }
            }

            // receiving translation table
            if (command_status == COM_GET_FULL_T) 
            {
                serial_count--;

                int r_i, c_i;
                r_i = 12 - (serial_count % 12);
                c_i = (serial_count / 12) + 1;

                int norm_data;
                //norm_data = (int)((float)indata / 2.55);
                norm_data = convert_bit2pr[indata];
                //norm_data = indata;

                dataGridView1.Rows[r_i].Cells[c_i].Value = norm_data;
                //dataGridView1[c_i, r_i].Style.BackColor = Color.FromArgb(indata, 255-indata, 0);
                //cellColorRefresh(r_i, c_i);

                // end of transmition
                if (serial_count == 0)
                {
                    //command_status = COM_FREE;

                    // refresh the color of all cells
                    for(r_i = 1; r_i<13;r_i++)
                        for(c_i = 1; c_i<13;c_i++)
                            cellColorRefresh(r_i, c_i);

                    // move to getting RPM ranges
                    command_status = COM_GET_RPM_T;
                    serial_count = 24;

                    serialPort1.Write("r");
                    //return;
                }
            }
            //textBox1.Text = textBox1.Text + indata.ToString();
        }
    }
}
