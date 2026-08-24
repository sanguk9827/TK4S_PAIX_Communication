
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        bool CCW_Status = false;
        bool CW_Status = false;
        private Form1 form1;
        Image ButtonOn;
        Image ButtonOff;
        public Form2(Form1 tmp_form)
        {
            InitializeComponent();
            form1 = tmp_form;
            dataGridView1.Columns[9].Width = 70;
            dataGridView1.Rows.Add("Axis0");
            dataGridView1.Rows.Add("Axis1");
            dataGridView1.Rows.Add("Axis2");
            dataGridView1.Rows.Add("Axis3");
            dataGridView1.Rows.Add("Axis4");
            dataGridView1.Rows.Add("Axis5");
            comboBox1.Items.Add("Axis0");
            comboBox1.Items.Add("Axis1");
            comboBox1.Items.Add("Axis2");
            comboBox1.Items.Add("Axis3");
            comboBox1.Items.Add("Axis4");
            comboBox1.Items.Add("Axis5");

            comboBox2.Items.Add("StartSpeed");
            comboBox2.Items.Add("speed");
            comboBox2.Items.Add("acc");
            comboBox2.Items.Add("dec");

            comboBox2.Items.Add("HomeSpeed 1차");
            comboBox2.Items.Add("HomeSpeed 2차");
            comboBox2.Items.Add("HomeSpeed 3차");

            comboBox3.Items.Add("Axis0");
            comboBox3.Items.Add("Axis1");
            comboBox3.Items.Add("Axis2");
            comboBox3.Items.Add("Axis3");
            comboBox3.Items.Add("Axis4");
            comboBox3.Items.Add("Axis5");

            comboBox4.Items.Add("Axis0");
            comboBox4.Items.Add("Axis1");
            comboBox4.Items.Add("Axis2");
            comboBox4.Items.Add("Axis3");
            comboBox4.Items.Add("Axis4");
            comboBox4.Items.Add("Axis5");

            comboBox5.Items.Add("Axis0");
            comboBox5.Items.Add("Axis1");
            comboBox5.Items.Add("Axis2");
            comboBox5.Items.Add("Axis3");
            comboBox5.Items.Add("Axis4");
            comboBox5.Items.Add("Axis5");

            comboBox6.Items.Add("+Limit");
            comboBox6.Items.Add("-Limit");
            comboBox6.Items.Add("+Home센서");
            comboBox6.Items.Add("-Home센서");
            comboBox6.Items.Add("Z-");
            comboBox6.Items.Add("Z+");


            //     comboBox2.Items.Add("HomeOffset");

            //((DataGridViewImageColumn)dataGridView1.Columns[9]).ImageLayout = DataGridViewImageCellLayout.Stretch;
            // ((DataGridViewImageColumn)dataGridView1.Columns[9]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            ((DataGridViewImageColumn)dataGridView1.Columns[9]).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string foldername = @"\Image\";
            string imagepath = AppDomain.CurrentDomain.BaseDirectory + foldername;
            string path_onbutton = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "ON_button_red.png");
            string path_offbutton = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "OFF_button_black.png");
            int a = dataGridView1.Columns["Speed"].Index;

            ButtonOn = Image.FromFile(path_onbutton);
            ButtonOff = Image.FromFile(path_offbutton);




            //////////////////////////
            ///
            // WPF UserControl 생성
            //   UserControl2 wpfControl = new UserControl2();
            //
            //   // WPF를 WinForms에 넣기 위한 컨테이너
            //   ElementHost host = new ElementHost();
            //
            //   host.Dock = DockStyle.Fill;
            //   host.Child = wpfControl;
            //   host.Location = new System.Drawing.Point(100, 100);
            //   host.Size = new System.Drawing.Size(300, 300);
            //
            //   host.Child = new UserControl2();
            //
            //   this.Controls.Add(host);
            /////////////////////////////
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < form1.tcp.ReceivedData2_Encoder_Position.Length; i++)
            {
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["Current_Position"].Index].Value = form1.tcp.ReceivedData2_Encoder_Position[i].ToString();
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["BusyStatus"].Index].Value = (form1.tcp.ReceivedData2_BusyStatus[i] == true) ? ButtonOn : ButtonOff;
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["ErrorStatus"].Index].Value = (form1.tcp.ReceivedData2_ErrorStatus[i] == true) ? ButtonOn : ButtonOff;
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["MinusLimit"].Index].Value = (form1.tcp.ReceivedData2_MinusLimitSensor[i] == true) ? ButtonOn : ButtonOff;
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["HomeSensor"].Index].Value = (form1.tcp.ReceivedData2_HomeSensor[i] == true) ? ButtonOn : ButtonOff;
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["PlusLimit"].Index].Value = (form1.tcp.ReceivedData2_PlusLimitSensor[i] == true) ? ButtonOn : ButtonOff;
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["DrivingSpeed"].Index].Value = form1.tcp.ReceivedData2_DrivingSpeed[i];
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["Speed"].Index].Value = form1.tcp.ReceivedData2_SettingSpeed[i];
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["Acc"].Index].Value = form1.tcp.ReceivedData2_SettingAcc[i];
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["Dec"].Index].Value = form1.tcp.ReceivedData2_SettingDec[i];
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["StartSpeed"].Index].Value = form1.tcp.ReceivedData2_SettingStartSpeed[i];
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["HomeSpeedFirst"].Index].Value = form1.tcp.ReceivedData2_HomingFirstSpeed[i];
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["HomeSpeedSecond"].Index].Value = form1.tcp.ReceivedData2_HomingSecondSpeed[i];
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["HomeSpeedThird"].Index].Value = form1.tcp.ReceivedData2_HomingThirdSpeed[i];
                dataGridView1.Rows[i].Cells[dataGridView1.Columns["HomeOffset"].Index].Value = form1.tcp.ReceivedData2_HomingOffset[i];



            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex >= 0 && comboBox2.SelectedIndex <= 3)
            {
                string axisNum_Hex = comboBox1.SelectedIndex.ToString("X");
                string Startspeed_Hex = form1.tcp.ReceivedData2_SettingStartSpeed[comboBox1.SelectedIndex].ToString("X");
                string speed_Hex = form1.tcp.ReceivedData2_SettingSpeed[comboBox1.SelectedIndex].ToString("X");
                string Acc_Hex = form1.tcp.ReceivedData2_SettingAcc[comboBox1.SelectedIndex].ToString("X");
                string Dec_Hex = form1.tcp.ReceivedData2_SettingDec[comboBox1.SelectedIndex].ToString("X");
                string message = string.Format("1 SPDPPS {0} {1} {2} {3} 0 0 {4} 0", axisNum_Hex, Startspeed_Hex, Acc_Hex, speed_Hex, Dec_Hex);
                int txtBoxValue;
                int.TryParse(textBox1.Text, out txtBoxValue);
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        message = string.Format("1 SPDPPS {0} {1} {2} {3} 0 0 {4} 0", axisNum_Hex, txtBoxValue.ToString("X"), Acc_Hex, speed_Hex, Dec_Hex);
                        if (form1.tcp.ReceivedData2_SettingSpeed[comboBox1.SelectedIndex] == 0)
                            message = string.Format("1 SPDPPS {0} {1} {2} {3} 0 0 {4} 0", axisNum_Hex, txtBoxValue.ToString("X"), 50.ToString("X"), 5.ToString("X"), 50.ToString("X"));

                        break;
                    case 1:
                        message = string.Format("1 SPDPPS {0} {1} {2} {3} 0 0 {4} 0", axisNum_Hex, Startspeed_Hex, Acc_Hex, txtBoxValue.ToString("X"), Dec_Hex);
                        if (form1.tcp.ReceivedData2_SettingSpeed[comboBox1.SelectedIndex] == 0)
                            message = string.Format("1 SPDPPS {0} {1} {2} {3} 0 0 {4} 0", axisNum_Hex, 5.ToString("X"), 50.ToString("X"), txtBoxValue.ToString("X"), 50.ToString("X"));
                        break;
                    case 2:
                        message = string.Format("1 SPDPPS {0} {1} {2} {3} 0 0 {4} 0", axisNum_Hex, Startspeed_Hex, txtBoxValue.ToString("X"), speed_Hex, Dec_Hex);
                        if (form1.tcp.ReceivedData2_SettingSpeed[comboBox1.SelectedIndex] == 0)
                            message = string.Format("1 SPDPPS {0} {1} {2} {3} 0 0 {4} 0", axisNum_Hex, txtBoxValue.ToString("X"), 50.ToString("X"), 5.ToString("X"), 50.ToString("X"));
                        break;
                    case 3:
                        message = string.Format("1 SPDPPS {0} {1} {2} {3} 0 0 {4} 0", axisNum_Hex, Startspeed_Hex, Acc_Hex, speed_Hex, txtBoxValue.ToString("X"));
                        if (form1.tcp.ReceivedData2_SettingSpeed[comboBox1.SelectedIndex] == 0)
                            message = string.Format("1 SPDPPS {0} {1} {2} {3} 0 0 {4} 0", axisNum_Hex, txtBoxValue.ToString("X"), 50.ToString("X"), 5.ToString("X"), 50.ToString("X"));
                        break;
                    default:
                        break;
                }
                await form1.tcp.RequestData(message);
            }
            else if (comboBox2.SelectedIndex >= 4 && comboBox2.SelectedIndex <= 7)
            {
                string FirstHomeSpd = form1.tcp.ReceivedData2_HomingFirstSpeed[comboBox1.SelectedIndex].ToString("X");
                string SecondHomeSpd = form1.tcp.ReceivedData2_HomingSecondSpeed[comboBox1.SelectedIndex].ToString("X");
                string ThirdHomeSpd = form1.tcp.ReceivedData2_HomingThirdSpeed[comboBox1.SelectedIndex].ToString("X");
                string message = string.Format("1 SHOMESPD {0} {1} {2} 0", FirstHomeSpd, SecondHomeSpd, ThirdHomeSpd);
                int txtBoxValue;
                int.TryParse(textBox1.Text, out txtBoxValue);
                switch (comboBox2.SelectedIndex)
                {
                    case 4:
                        message = string.Format("1 SHOMESPD {0} {1} {2} {3} 0", comboBox1.SelectedIndex.ToString("X"), txtBoxValue.ToString("X"), SecondHomeSpd, ThirdHomeSpd);
                        break;
                    case 5:
                        message = string.Format("1 SHOMESPD {0} {1} {2} {3} 0", comboBox1.SelectedIndex.ToString("X"), FirstHomeSpd, txtBoxValue.ToString("X"), ThirdHomeSpd);
                        break;
                    case 6:
                        message = string.Format("1 SHOMESPD {0} {1} {2} {3} 0", comboBox1.SelectedIndex.ToString("X"), FirstHomeSpd, SecondHomeSpd, txtBoxValue.ToString("X"));
                        break;
                    default:
                        break;
                }
                await form1.tcp.RequestData(message);
            }
            ShowSendPacket(form1.tcp.ShowSendPacked_ForForm2);
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            if (comboBox3.SelectedIndex != -1 && CCW_Status == false)
            {
                CCW_Status = true;
                form1.tcp.JOG((byte)comboBox3.SelectedIndex, MotionTcpClient.JOGDirection.CCW);
                ShowSendPacket(form1.tcp.ShowSendPacked_ForForm2);
            }
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            CCW_Status = false;
            form1.tcp.STOP((byte)comboBox3.SelectedIndex);
            ShowSendPacket(form1.tcp.ShowSendPacked_ForForm2);
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            CW_Status = false;
            form1.tcp.JOG((byte)comboBox3.SelectedIndex, MotionTcpClient.JOGDirection.CW);
            ShowSendPacket(form1.tcp.ShowSendPacked_ForForm2);
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            CW_Status = false;
            form1.tcp.STOP((byte)comboBox3.SelectedIndex);
            ShowSendPacket(form1.tcp.ShowSendPacked_ForForm2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex != -1)
            {
                int targetPosition;
                int.TryParse(textBox2.Text, out targetPosition);
                form1.tcp.PTP((byte)comboBox4.SelectedIndex, targetPosition);
                ShowSendPacket(form1.tcp.ShowSendPacked_ForForm2);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex != -1)
            {
                int targetPosition;

                form1.tcp.STOP((byte)comboBox4.SelectedIndex);
                ShowSendPacket(form1.tcp.ShowSendPacked_ForForm2);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex != -1 && comboBox6.SelectedIndex != -1)
            {
                form1.tcp.HOME((byte)comboBox5.SelectedIndex, (byte)comboBox6.SelectedIndex);
                ShowSendPacket(form1.tcp.ShowSendPacked_ForForm2);
            }
        }
        private void ShowSendPacket(byte[] packet)
        {
            // 실제 패킷 전송
            //  networkStream.Write(packet, 0, packet.Length);

            // TextBox에 전송 패킷 표시
            textBox3.AppendText(
                $"[{DateTime.Now:HH:mm:ss.fff}] TX : {BitConverter.ToString(packet)}"
                + Environment.NewLine
            );
        }
    }
}
