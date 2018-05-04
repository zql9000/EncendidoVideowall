using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace EncendidoVideowall
{
    public partial class Form1 : Form
    {
        SerialPort ComPort;

        public Form1()
        {
            InitializeComponent();
        }

        private void CrearPuerto()
        {
            ComPort = new SerialPort();

            ComPort.BaudRate = 9600;
            ComPort.Parity = Parity.None;
            ComPort.DataBits = 8;
            ComPort.StopBits = StopBits.One;
            ComPort.Handshake = Handshake.None;
            ComPort.ReadTimeout = 500;
            ComPort.WriteTimeout = 500;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CrearPuerto();

            foreach (var item in SerialPort.GetPortNames())
            {
                cmbPuerto.Items.Add(item);
            }

            if (cmbPuerto.Items.Count > 0)
                cmbPuerto.SelectedItem = cmbPuerto.Items[0];
        }

        private void btnEncender_Click(object sender, EventArgs e)
        {
            if (cmbPuerto.SelectedItem == null)
                MessageBox.Show("Debe seleccionar el puerto donde se encuantra conectado el Videowall","Puerto");
            else
                EnviarComando(cmbPuerto.Text, "K:000PON.K:016PON.K:001PON.K:017PON.");
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (cmbPuerto.SelectedItem == null)
                MessageBox.Show("Debe seleccionar el puerto donde se encuantra conectado el Videowall", "Puerto");
            else
                EnviarComando(cmbPuerto.Text, "K:000POF.K:016POF.K:001POF.K:017POF.");
        }

        private void EnviarComando(string Puerto, string Comando)
        {
            try
            {
                ComPort.PortName = Puerto;
                ComPort.Open();
                ComPort.Write(Comando);
                ComPort.Close();
            }
            catch (Exception)
            {
                CrearPuerto();
            }
        }
    }
}
