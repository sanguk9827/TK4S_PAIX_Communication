using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static WindowsFormsApp1.Form1;

namespace WindowsFormsApp1
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        Form1 form1;
        DispatcherTimer timer; 
        public ObservableCollection<tempData> tempList { get; set; }
    = new ObservableCollection<tempData>();
        
        public UserControl1(Form1 tmpform)
        {
            InitializeComponent();
            timer = new DispatcherTimer();    //객체생성
            timer.Interval = TimeSpan.FromMilliseconds(10);    //시간간격 설정
            timer.Tick += new EventHandler(timer_Tick);          //이벤트 추가
            DataGrid1.ItemsSource = tempList;

            tempList.Add(new tempData
            {
                CurrentTmp = "0",
                TargetTemp = "0",
                P_Gain = "0",
                I_Gain = "0",
                D_Gain = "0"
            });                                   //  
            form1 = tmpform;
            try
            {
               if (form1.tmpSerialPort.IsOpen)
               {
                   timer.Start();                                       //타이머 시작. 종료는 timer.Stop(); 으로 한다
               }
            }
            catch(Exception ex)
            {

            }
            comboBox1.Items.Add("SV값");
            comboBox1.Items.Add("P게인");
            comboBox1.Items.Add("I게인");
            comboBox1.Items.Add("D게인");
        }
        
        
        private void timer_Tick(object sender, EventArgs e)
        {
            //form1.ReqData()
            tempList[0].TargetTemp = form1.settedTemp.ToString();
            tempList[0].CurrentTmp = form1.CurTmep.ToString();
            tempList[0].P_Gain = form1.temp_P_Gain.ToString();
            tempList[0].I_Gain = form1.temp_I_Gain.ToString();
            tempList[0].D_Gain = form1.temp_D_Gain.ToString();
            DataGrid1.Items.Refresh();
            ///int settedTemp;
            ///int CurTmep;
            ///int temp_P_Gain;
            ///int temp_I_Gain;
            ///int temp_D_Gain;
            ///bool temp_AutoTunning_Status;
        }

        private async void Btn_Set_Value_Click(object sender, RoutedEventArgs e)
        {
            int index=-1;
            if (comboBox1.SelectedItem?.ToString() == "SV값")
            {
                index = comboBox1.SelectedIndex;
                int WriteValue;
                int.TryParse(textBox1.Text, out WriteValue); 
                await form1.ReqData(1, TmpController__DataType.WriteSingleRegister, 0, WriteValue);
            }
            else if (comboBox1.SelectedItem?.ToString() == "P게인")
            {
                index = comboBox1.SelectedIndex;
                int WriteValue;
                int.TryParse(textBox1.Text, out WriteValue);
                await form1.ReqData(1, TmpController__DataType.WriteSingleRegister, 101, WriteValue);
            }
            else if (comboBox1.SelectedItem?.ToString() == "I게인")
            {
                index = comboBox1.SelectedIndex;
                int WriteValue;
                int.TryParse(textBox1.Text, out WriteValue);
                await form1.ReqData(1, TmpController__DataType.WriteSingleRegister, 103, WriteValue);
            }
            else if (comboBox1.SelectedItem?.ToString() == "D게인")
            {
                index = comboBox1.SelectedIndex;
                int WriteValue;
                int.TryParse(textBox1.Text, out WriteValue);
                await form1.ReqData(1, TmpController__DataType.WriteSingleRegister, 105, WriteValue);
            }
        }
    }
    public class MyRow : INotifyPropertyChanged
    {
        private string _currentTmp;
        private string _targetTemp;
        private string _p_Gain;
        private string _i_Gain;
        private string _d_Gain;

        public string CurrentTmp
        {
            get => _currentTmp;
            set
            {
                _currentTmp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTmp)));
            }
        }

        public string TargetTemp
        {
            get => _targetTemp;
            set
            {
                _targetTemp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TargetTemp)));
            }
        }

        public string P_Gain
        {
            get => _p_Gain;
            set
            {
                _p_Gain = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P_Gain)));
            }
        }

        public string I_Gain
        {
            get => _i_Gain;
            set
            {
                _i_Gain = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(I_Gain)));
            }
        }

        public string D_Gain
        {
            get => _d_Gain;
            set
            {
                _d_Gain = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(D_Gain)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
public class tempData : INotifyPropertyChanged
{
    private string _currentTmp;
    private string _targetTemp;
    private string _pGain;
    private string _iGain;
    private string _dGain;

    public string CurrentTmp
    {
        get => _currentTmp;
        set
        {
            if (_currentTmp != value)
            {
                _currentTmp = value;
                OnPropertyChanged();
            }
        }
    }

    public string TargetTemp
    {
        get => _targetTemp;
        set
        {
            if (_targetTemp != value)
            {
                _targetTemp = value;
                OnPropertyChanged();
            }
        }
    }

    public string P_Gain
    {
        get => _pGain;
        set
        {
            if (_pGain != value)
            {
                _pGain = value;
                OnPropertyChanged();
            }
        }
    }

    public string I_Gain
    {
        get => _iGain;
        set
        {
            if (_iGain != value)
            {
                _iGain = value;
                OnPropertyChanged();
            }
        }
    }

    public string D_Gain
    {
        get => _dGain;
        set
        {
            if (_dGain != value)
            {
                _dGain = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(
        [CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}
