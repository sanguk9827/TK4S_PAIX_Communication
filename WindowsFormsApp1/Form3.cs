using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace WindowsFormsApp1
{
    
    public partial class Form3 : Form
    {
        Form1 form1;
        UserControl1 wpfControl;
        public Form3(Form1 refform1)
        {
            InitializeComponent();
            form1 = refform1;
            LoadWpfControl();




            
           // comboBox1.Items.Add("SV");
           // comboBox1.Items.Add("P_GAIN");
           // comboBox1.Items.Add("I_GAIN");
           // comboBox1.Items.Add("D_GAIN");
            


        }
        private void LoadWpfControl()
        {
            // 1. WPF 컨트롤을 감싸줄 호스트 객체 생성
            ElementHost host = new ElementHost();
            host.Dock = DockStyle.Fill; // 윈폼 화면 전체 채우기

            // 2. 작성하신 WPF 사용자 정의 컨트롤 객체 생성
            // (기존에 생성자 인자로 this를 받도록 만드셨다면 this를 전달)
            wpfControl = new UserControl1(form1);

            // 3. 호스트에 WPF 컨트롤 삽입
            host.Child = wpfControl;
            host.Dock = DockStyle.Fill;
            // 4. 새 윈폼의 컨트롤 컬렉션에 추가
            panel1.Controls.Add(host);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // dataGridView1.Rows[0].Cells[dataGridView1.Columns["PV"].Index].Value = form1.CurTmep;
           // dataGridView1.Rows[0].Cells[dataGridView1.Columns["SV"].Index].Value = form1.settedTemp;
           // dataGridView1.Rows[0].Cells[dataGridView1.Columns["P_GAIN"].Index].Value = form1.temp_P_Gain;
           // dataGridView1.Rows[0].Cells[dataGridView1.Columns["I_GAIN"].Index].Value = form1.temp_I_Gain;
           // dataGridView1.Rows[0].Cells[dataGridView1.Columns["I_GAIN"].Index].Value = form1.temp_D_Gain;
            wpfControl.DataGrid1.Items.Refresh();                                                    
        }

        private async void button1_Click(object sender, EventArgs e)
        {
          //  int Value;
          //  int.TryParse(textBox1.Text, out Value);
          //  switch (comboBox1.SelectedIndex)
          //  {
          //      
          //      
          //      case 0:
          //          await form1.ReqData(1, Form1.TmpController__DataType.WriteSingleRegister, 0, Value);
          //          break;
          //      case 1:
          //          await form1.ReqData(1, Form1.TmpController__DataType.WriteSingleRegister, 101, Value);
          //          break;
          //      case 2:
          //          await form1.ReqData(1, Form1.TmpController__DataType.WriteSingleRegister, 103, Value);
          //          break;
          //      case 3:
          //          await form1.ReqData(1, Form1.TmpController__DataType.WriteSingleRegister, 105, Value);
          //          break;
          //      default:
          //          break;
          //  }
            
        }

        
    }                                                                                                
}
