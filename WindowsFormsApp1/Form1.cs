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
using System.Threading;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Net;
using System.Windows.Controls;
using System.Windows.Interop;
using static WindowsFormsApp1.MotionTcpClient;
//using System.Windows;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using System.Windows.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly SemaphoreSlim requestLock = new SemaphoreSlim(1, 1);
        public SerialPort tmpSerialPort;
        public int length;
        public int settedTemp;
        public int CurTmep;
        public int temp_P_Gain;
        public int temp_I_Gain;
        public int temp_D_Gain;
        public bool temp_AutoTunning_Status;
        public enum TmpController__DataType { ReadCoilStatus = 1, ReadInputStatus = 2, ReadHoldingRegisters = 3, ReadInputRegisters = 4, WriteSingleCoil = 5, WriteSingleRegister = 6 };

        public Form2 newForm2;
        public Form3 newForm3;
        public MotionTcpClient tcp = new MotionTcpClient();
        byte[] tempController_ReceivedData= new byte[256];
        public Form1()
        {
            InitializeComponent();
            tmpSerialPort = new SerialPort();
            //tmpSerialPort.PortName = "COM4";
            //tmpSerialPort.Parity = Parity.None;
            //tmpSerialPort.DataBits = 8;

            //tmpSerialPort.StopBits = StopBits.Two;
            // tmpSerialPort.DataReceived += SerialPort_DataReceived;   // 시리얼 패킷 수신했을 때, SerialPort_DataReceived 메서드 실행
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            string[] Parity_Array = { "Parity.None", "Parity.Odd", "Parity.Even", "Parity.Mark", "Parity.Space" };
            string[] StopBits_Array = { "StopBits.None", "StopBits.One", "StopBits.Two", "StopBits.OnePointFive" };
            comboBox2.Items.AddRange(Parity_Array);
            comboBox3.Items.AddRange(StopBits_Array);
        }

        /* private async Task SerialPort_DataReceived()
         {
             try
             {

             }
             catch(Exception ex)
             {

             }
             int a = 1;
         }  */


        /* private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)                 //////////원본 메소드 보존 이벤트방식
         {
             //Thread.Sleep(200);
             // data = tmpSerialPort.ReadExisting();
             while (tmpSerialPort.Read(buffer, 0, 10) <= 5)   // 시리얼 패킷이 일정량 이상 수신되고 난 후 다음 동작 실행
             {
             };

             BeginInvoke(new Action(() => textBox1.Text = buffer[4].ToString()));

         }  */
        /*private void ReqData(byte stationNumber, TmpController__DataType DataType, int Address)       //////////////////  원본 매소드 보존
        {
            byte AddressHIgh = (byte)((0xFF00 & Address) >> 8);
            byte AddressLow = (byte)(0xFF & Address);
            byte[] data = { stationNumber, (byte)DataType, AddressHIgh, AddressLow, 0, 1 };
            byte crc1, crc2;
            //byte[] dataForValueToModify= { 1, 6, 0, 0, 0, 30, 9, 194 };
            byte[] dataForSendToRead = new byte[data.Length + 2];
            Calculate_CRC16(data, out crc1, out crc2);
            for (int i = 0; i < data.Length; i++)
            {
                dataForSendToRead[i] = data[i];
            }
            dataForSendToRead[data.Length] = crc1;
            dataForSendToRead[data.Length + 1] = crc2;
            tmpSerialPort.Write(dataForSendToRead, 0, dataForSendToRead.Length);
            byte[] newbte = new byte[256];








            length = 0;

        }*/
        public async Task ReqData(
    byte stationNumber,
    TmpController__DataType DataType,
    int Address,
    int WriteValue = 0)
        {
            await requestLock.WaitAsync();

            try
            {
                Console.WriteLine($"REQ START : {DataType}, Address={Address}");
                byte[] buffer = new byte[256];

                byte AddressHigh = (byte)((0xFF00 & Address) >> 8);
                byte AddressLow = (byte)(0xFF & Address);

                byte[] ValueConvertToByte = IntToTwoBytes(WriteValue);

                byte[] data;

                if ((byte)DataType >= 1 && (byte)DataType <= 4)
                {
                    data = new byte[]
                    {
                stationNumber,
                (byte)DataType,
                AddressHigh,
                AddressLow,
                0,
                1
                    };
                }
                else if ((byte)DataType >= 5 && (byte)DataType <= 6)
                {
                    data = new byte[]
                    {
                stationNumber,
                (byte)DataType,
                AddressHigh,
                AddressLow,
                ValueConvertToByte[0],
                ValueConvertToByte[1]
                    };
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(DataType));
                }

                Calculate_CRC16(data, out byte crc1, out byte crc2);

                byte[] sendData = new byte[data.Length + 2];

                Buffer.BlockCopy(data, 0, sendData, 0, data.Length);

                sendData[data.Length] = crc1;
                sendData[data.Length + 1] = crc2;

                await tmpSerialPort.BaseStream.WriteAsync(
                    sendData,
                    0,
                    sendData.Length);

               
                    int totalReceived = 0;

                using (CancellationTokenSource cts = new CancellationTokenSource(1000))
                {
                    
                        while (totalReceived < buffer.Length)
                        {

                            int count = await tmpSerialPort.BaseStream.ReadAsync(
                                buffer,
                                totalReceived,
                                buffer.Length - totalReceived,
                                cts.Token);

                            if (count == 0)
                                break;

                            totalReceived += count;

                            if (DataType == TmpController__DataType.ReadCoilStatus ||
                                DataType == TmpController__DataType.ReadInputStatus)
                            {
                                if (totalReceived >= 6 /*&& buffer[5] != 0*/)
                                    break;
                            }
                            else if (DataType == TmpController__DataType.ReadHoldingRegisters ||
                                     DataType == TmpController__DataType.ReadInputRegisters)
                            {
                                if (totalReceived >= 7 /*&& buffer[6] != 0*/)
                                    break;
                            }
                        else if (DataType == TmpController__DataType.WriteSingleCoil ||
                                 DataType == TmpController__DataType.WriteSingleRegister)
                        {
                            if (totalReceived >= 8 /*&& buffer[6] != 0*/)
                                break;
                        }
                    }
                    
                }
                

                switch (DataType)
                {
                    case TmpController__DataType.ReadHoldingRegisters:

                        if (Address == 0)
                        {
                            settedTemp = ConvertBinaryToWord(buffer, 3);
                        }
                        else if (Address == 101)
                        {
                            temp_P_Gain = ConvertBinaryToWord(buffer, 3);
                        }
                        else if (Address == 103)
                        {
                            temp_I_Gain = ConvertBinaryToWord(buffer, 3);
                        }
                        else if (Address == 105)
                        {
                            temp_D_Gain = ConvertBinaryToWord(buffer, 3);
                        }
                        else if (Address == 100)
                        {
                            temp_AutoTunning_Status =
                                ConvertBinaryToWord(buffer, 3) == 1;
                        }

                        break;

                    case TmpController__DataType.ReadInputRegisters:

                        if (Address == 1000)
                        {
                            CurTmep = ConvertBinaryToWord(buffer, 3);
                        }

                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                requestLock.Release();
            }
        }





        private void button1_Click(object sender, EventArgs e)
        {
              if (newForm3 == null)
              newForm3 = new Form3(this);
              newForm3.Show();
          // await ReqData(1, TmpController__DataType.WriteSingleRegister, 0, 28);

        }
        public  byte[] IntToTwoBytes(int value)
        {
            return new byte[]
            {
        (byte)((value >> 8) & 0xFF),  // 상위 바이트
        (byte)(value & 0xFF)          // 하위 바이트
            };
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
               // tmpSerialPort.Open();
            }
            catch(Exception ex)
            {

            }
        }
        private void Calculate_CRC16(byte[] data, out byte CRCLow, out byte CRCHIgh)
        {
            ushort crc = 0xFFFF;
            foreach (byte b in data)
            {
                crc ^= b;
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            CRCLow = (byte)(crc & 0xFF);
            CRCHIgh = (byte)((crc  & 0xFF00)>>8);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ReqData_NMC2(out buffer);
            if(newForm2==null)
            newForm2 = new Form2(this);
            newForm2.Show();
            // tcp.Main("1 QALLEXT");    // "1 GETCEPOS 1 0" //체크섬 low값이 매뉴얼 0x42(십진수 66)보다 22 작은 0x22(34)  //"15 SRV 0 1"
            //int a = 1;
            //  byte[] b= MotionTcpClient.tmp_data;
            //  tcp.JOG(0, MotionTcpClient.JOGDirection.CW);
            //tcp.PTP(0, 300);
        }

        private async void timer1_Tick(object sender, EventArgs e)    //async
        {
            try
            {
               //// //await tcp.RequestData("1 QALLEXT");   //await
                await tcp.RequestData("1 QDRVSPEED");  //
                await tcp.RequestData("1 QSTATUS");
                await tcp.RequestData("1 QPX 0 C 53");
                await tcp.RequestData("1 QPX 1 C 53");
                await tcp.RequestData("1 QPX 2 C 53");
                await tcp.RequestData("1 QPX 4 C 53");
                await tcp.RequestData("1 QPX 5 C 53");
                ///
               // await ReqData(1, Form1.TmpController__DataType.ReadInputRegisters, 1000);
               // await ReqData(1, Form1.TmpController__DataType.ReadHoldingRegisters, 0);
               // await ReqData(1, Form1.TmpController__DataType.ReadHoldingRegisters, 101);
               // await ReqData(1, Form1.TmpController__DataType.ReadHoldingRegisters, 103);
               // await ReqData(1, Form1.TmpController__DataType.ReadHoldingRegisters, 105);
               // await ReqData(1, Form1.TmpController__DataType.ReadHoldingRegisters, 100);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {

                tcp.ip_adresss = textBox1.Text;
                int TK4S_DataBits;
                int Baudrate;
                int.TryParse(textBox2.Text, out tcp.port_num);
                int.TryParse(textBox3.Text, out TK4S_DataBits);
                //if (tmpSerialPort.IsOpen == false)
                tmpSerialPort.PortName = comboBox1.Text;
                int.TryParse(textBox4.Text, out Baudrate);
                tmpSerialPort.BaudRate = Baudrate;
                tmpSerialPort.Parity = (Parity)comboBox2.SelectedIndex;
                tmpSerialPort.DataBits = TK4S_DataBits;
                tmpSerialPort.StopBits = (StopBits)comboBox3.SelectedIndex;
                tmpSerialPort.Open();
                await tcp.ConnectStart();
                while (tmpSerialPort.IsOpen == false)
                {

                }
                if (tcp.client.Connected == true && tmpSerialPort.IsOpen == true)
                {
                    timer1.Enabled = true;
                    timer2.Enabled = true;
                    MessageBox.Show("TK4S, PAIX 연결 성공");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("IP어드레스와 포트번호가 일치하지 않습니다");
            }
            finally
            {

            }
        }
        public int ConvertBinaryToWord(byte[] data, int StartIndex)  // 4byte Binary값에서 int값 추출
        {
         
                return (int)((0xFFFFFFFF & (data[StartIndex + 0] << 8) | (0xFFFFFFFF & (data[StartIndex + 1] << 0)) ));
            


        }

        private async void timer2_Tick(object sender, EventArgs e)
        {
            await ReqData(1, Form1.TmpController__DataType.ReadInputRegisters, 1000);
            await ReqData(1, Form1.TmpController__DataType.ReadHoldingRegisters, 0);
            await ReqData(1, Form1.TmpController__DataType.ReadHoldingRegisters, 101);
            await ReqData(1, Form1.TmpController__DataType.ReadHoldingRegisters, 103);
            await ReqData(1, Form1.TmpController__DataType.ReadHoldingRegisters, 105);
            await ReqData(1, Form1.TmpController__DataType.ReadHoldingRegisters, 100);
        }
    }
    public class MotionTcpClient
    {
         public TcpClient client = new TcpClient();
        NetworkStream stream;
        public  byte[] ReceivedData = new byte[1024];
        public byte[] ReceivedData2 = new byte[1024];
        private readonly SemaphoreSlim requestLock = new SemaphoreSlim(1, 1);
        public int[] ReceivedData2_Encoder_Position = new int[6];
        public bool[] ReceivedData2_BusyStatus = new bool[6];
        public bool[] ReceivedData2_ErrorStatus = new bool[6];

        public bool[] ReceivedData2_MinusLimitSensor = new bool[6];
        public bool[] ReceivedData2_PlusLimitSensor = new bool[6];

        public bool[] ReceivedData2_HomeSensor = new bool[6];
        public bool[] ReceivedData2_AlarmStatus = new bool[6];
        public bool[] ReceivedData2_HomeComplete = new bool[6];
        public bool[] ReceivedData2_PositioningComplete = new bool[6];
        public bool[] ReceivedData2_ZPhase= new bool[6];
        public byte[] ShowSendPacked_ForForm2 = new byte[1024];
        public bool[] ReceivedData2_ServoReadyStatus = new bool[6];
        public int[] ReceivedData2_DrivingSpeed = new int[6];
        public int[] ReceivedData2_SettingSpeed = new int[6];
        public int[] ReceivedData2_SettingAcc = new int[6];
        public int[] ReceivedData2_SettingDec = new int[6];
        public int[] ReceivedData2_SettingStartSpeed = new int[6];
        public int[] ReceivedData2_AccJerk = new int[6];
        public int[] ReceivedData2_DecJerk = new int[6];
        public int[] ReceivedData2_HomingFirstSpeed = new int[6];
        public int[] ReceivedData2_HomingSecondSpeed = new int[6];
        public int[] ReceivedData2_HomingThirdSpeed = new int[6];
        public int[] ReceivedData2_HomingOffset = new int[6];
        public string ip_adresss;
        public int port_num;
        public enum JOGDirection { CW, CCW }
        public enum Highbyte { HighLOW, LowHigh }  // 패킷받을때, 상위byte가 앞에있는지, 뒤에있는지

        public async Task ConnectStart()
        {
            await client.ConnectAsync(ip_adresss, port_num);
            stream = client.GetStream();
        }
         public MotionTcpClient()
        {
            
        }
         public async Task RequestData(string msg)
        {
            /*using (TcpClient client = new TcpClient())   임시주석
            {*/

            // 로컬 서버(127.0.0.1)의 5000번 포트로 비동기 접속 
            //   await client.ConnectAsync("192.168.0.11", 1000); 임시주석

            await requestLock.WaitAsync();    // 중복 접근 불가. 하나의 태스크가 권한 점령
            try
            {
                
                byte[] buffer = new byte[1024];

                byte[] data = ResultMessage_CheckSum(msg);

                await stream.WriteAsync(data, 0, data.Length);

                int totalBytesRead = 0;

                while (totalBytesRead < buffer.Length)     //현재 받은 패킷 길이 체크
                {
                    int bytesRead = await stream.ReadAsync(
                        buffer,
                        totalBytesRead,
                        buffer.Length - totalBytesRead);

                    if (bytesRead == 0)
                        break;

                    totalBytesRead += bytesRead;

                    if (CheckEnd(buffer, totalBytesRead))
                        break;
                }
                ShowSendPacked_ForForm2 = data;  /// 현재 보내는 패킷을 Form2에 표기
                if (msg.Contains("GETCEPOS") || msg.Contains("QALLEXT"))
                {
                    ReceivedData2 = buffer;
                    if (msg.Contains("GETCEPOS"))
                    {
                        for (int i = 0; i < 5; i++)     //0,1,2,4,5 좌표값 int로 변환
                        {
                            //3번축 없음
                            if (i != 3) ReceivedData2_Encoder_Position[i] = (int)((0xFFFFFFFF & (buffer[4 * i + 9] << 24) | (0xFFFFFFFF & (buffer[4 * i + 8] << 16)) | (0xFFFFFFFF & (buffer[4 * i + 7] << 8)) | (0xFFFFFFFF & buffer[4 * i + 6])));

                        }
                    }
                    else if (msg.Contains("QALLEXT"))
                    {
                        for (int i = 0; i < 3; i++)  // 0,1,2 축 정보값 int로 변환
                        {
                            //ReceivedData2_Encoder_Position[i] = (int)((0xFFFFFFFF & (buffer[4 * i + 17] << 24) | (0xFFFFFFFF & (buffer[4 * i + 16] << 16)) | (0xFFFFFFFF & (buffer[4 * i + 15] << 8)) | (0xFFFFFFFF & buffer[4 * i + 14])));

                            ReceivedData2_Encoder_Position[i] = ConvertBinaryToInt(buffer, (4 * i + 14), Highbyte.LowHigh);
                            ReceivedData2_BusyStatus[i] = ConvertBinaryToBool(buffer[6], i);
                            ReceivedData2_ErrorStatus[i] = ConvertBinaryToBool(buffer[6], (i + 4));
                            ReceivedData2_HomeSensor[i] = ConvertBinaryToBool(buffer[7], i);
                            ReceivedData2_HomeComplete[i] = ConvertBinaryToBool(buffer[10], i);    // 0: 원점이동 중, 1: 원점이동 완료
                            ReceivedData2_ZPhase[i] = ConvertBinaryToBool(buffer[11], i);
                            ReceivedData2_ServoReadyStatus[i] = ConvertBinaryToBool(buffer[12], i);


                        }
                        for (int i = 0; i < 2; i++)  //4,5축 정보값 int로 변환
                        {
                            // ReceivedData2_Encoder_Position[i+3] = (int)((0xFFFFFFFF & (buffer[4 * i + 57] << 24) | (0xFFFFFFFF & (buffer[4 * i + 56] << 16)) | (0xFFFFFFFF & (buffer[4 * i + 55] << 8)) | (0xFFFFFFFF & buffer[4 * i + 54])));
                            ReceivedData2_Encoder_Position[i + 4] = ConvertBinaryToInt(buffer, (byte)(4 * i + 54), Highbyte.LowHigh);
                            ReceivedData2_BusyStatus[i + 4] = ConvertBinaryToBool(buffer[46], (byte)i);
                            ReceivedData2_ErrorStatus[i + 4] = ConvertBinaryToBool(buffer[46], (byte)(i + 4));
                            ReceivedData2_HomeComplete[i + 4] = ConvertBinaryToBool(buffer[50], i);    // 0: 원점이동 중, 1: 원점이동 완료
                            ReceivedData2_PositioningComplete[i + 4] = ConvertBinaryToBool(buffer[50], i + 4);   //0: PTP 이동중, 1: PTP 이동완료
                            ReceivedData2_ZPhase[i + 4] = ConvertBinaryToBool(buffer[51], i);
                            ReceivedData2_ServoReadyStatus[i + 4] = ConvertBinaryToBool(buffer[52], i);

                        }

                        ReceivedData2_MinusLimitSensor[0] = ConvertBinaryToBool(buffer[7], 5);
                        ReceivedData2_MinusLimitSensor[1] = ConvertBinaryToBool(buffer[8], 0);
                        ReceivedData2_MinusLimitSensor[2] = ConvertBinaryToBool(buffer[8], 3);
                        ReceivedData2_PlusLimitSensor[0] = ConvertBinaryToBool(buffer[7], 4);
                        ReceivedData2_PlusLimitSensor[1] = ConvertBinaryToBool(buffer[7], 7);
                        ReceivedData2_PlusLimitSensor[2] = ConvertBinaryToBool(buffer[8], 2);
                        ReceivedData2_AlarmStatus[0] = ConvertBinaryToBool(buffer[7], 6);
                        ReceivedData2_AlarmStatus[1] = ConvertBinaryToBool(buffer[8], 1);
                        ReceivedData2_AlarmStatus[2] = ConvertBinaryToBool(buffer[8], 4);

                        ReceivedData2_MinusLimitSensor[4] = ConvertBinaryToBool(buffer[47], 5);
                        ReceivedData2_MinusLimitSensor[5] = ConvertBinaryToBool(buffer[48], 0);
                        ReceivedData2_PlusLimitSensor[4] = ConvertBinaryToBool(buffer[47], 4);
                        ReceivedData2_PlusLimitSensor[5] = ConvertBinaryToBool(buffer[47], 7);
                        ReceivedData2_AlarmStatus[4] = ConvertBinaryToBool(buffer[47], 6);
                        ReceivedData2_AlarmStatus[5] = ConvertBinaryToBool(buffer[48], 1);

                        //리밋센서, 홈센서
                    }
            }
            else if(msg.Contains("QSTATUS"))
            {
                    for (int i = 0; i < 3; i++)  // 0,1,2 축 정보값 int로 변환
                    {
                        //ReceivedData2_Encoder_Position[i] = (int)((0xFFFFFFFF & (buffer[4 * i + 17] << 24) | (0xFFFFFFFF & (buffer[4 * i + 16] << 16)) | (0xFFFFFFFF & (buffer[4 * i + 15] << 8)) | (0xFFFFFFFF & buffer[4 * i + 14])));

                        ReceivedData2_Encoder_Position[i] = ConvertBinaryToInt(buffer, (4 * i + 14), Highbyte.LowHigh);
                        ReceivedData2_BusyStatus[i] = ConvertBinaryToBool(buffer[6], i);
                        ReceivedData2_ErrorStatus[i] = ConvertBinaryToBool(buffer[6], (i + 4));
                        ReceivedData2_HomeSensor[i] = ConvertBinaryToBool(buffer[7], i);
                        ReceivedData2_HomeComplete[i] = ConvertBinaryToBool(buffer[10], i);    // 0: 원점이동 중, 1: 원점이동 완료
                        ReceivedData2_ZPhase[i] = ConvertBinaryToBool(buffer[11], i);
                        ReceivedData2_ServoReadyStatus[i] = ConvertBinaryToBool(buffer[12], i);


                    }
                    for (int i = 0; i < 2; i++)  //4,5축 정보값 int로 변환
                    {
                        // ReceivedData2_Encoder_Position[i+3] = (int)((0xFFFFFFFF & (buffer[4 * i + 57] << 24) | (0xFFFFFFFF & (buffer[4 * i + 56] << 16)) | (0xFFFFFFFF & (buffer[4 * i + 55] << 8)) | (0xFFFFFFFF & buffer[4 * i + 54])));
                        ReceivedData2_Encoder_Position[i + 4] = ConvertBinaryToInt(buffer, (byte)(4 * i + 54), Highbyte.LowHigh);
                        ReceivedData2_BusyStatus[i + 4] = ConvertBinaryToBool(buffer[46], (byte)i);
                        ReceivedData2_ErrorStatus[i + 4] = ConvertBinaryToBool(buffer[46], (byte)(i + 4));
                        ReceivedData2_HomeComplete[i + 4] = ConvertBinaryToBool(buffer[50], i);    // 0: 원점이동 중, 1: 원점이동 완료
                        ReceivedData2_PositioningComplete[i + 4] = ConvertBinaryToBool(buffer[50], i + 4);   //0: PTP 이동중, 1: PTP 이동완료
                        ReceivedData2_ZPhase[i + 4] = ConvertBinaryToBool(buffer[51], i);
                        ReceivedData2_ServoReadyStatus[i + 4] = ConvertBinaryToBool(buffer[52], i);

                    }

                    ReceivedData2_MinusLimitSensor[0] = ConvertBinaryToBool(buffer[7], 5);
                    ReceivedData2_MinusLimitSensor[1] = ConvertBinaryToBool(buffer[8], 0);
                    ReceivedData2_MinusLimitSensor[2] = ConvertBinaryToBool(buffer[8], 3);
                    ReceivedData2_PlusLimitSensor[0] = ConvertBinaryToBool(buffer[7], 4);
                    ReceivedData2_PlusLimitSensor[1] = ConvertBinaryToBool(buffer[7], 7);
                    ReceivedData2_PlusLimitSensor[2] = ConvertBinaryToBool(buffer[8], 2);
                    ReceivedData2_AlarmStatus[0] = ConvertBinaryToBool(buffer[7], 6);
                    ReceivedData2_AlarmStatus[1] = ConvertBinaryToBool(buffer[8], 1);
                    ReceivedData2_AlarmStatus[2] = ConvertBinaryToBool(buffer[8], 4);

                    ReceivedData2_MinusLimitSensor[4] = ConvertBinaryToBool(buffer[47], 5);
                    ReceivedData2_MinusLimitSensor[5] = ConvertBinaryToBool(buffer[48], 0);
                    ReceivedData2_PlusLimitSensor[4] = ConvertBinaryToBool(buffer[47], 4);
                    ReceivedData2_PlusLimitSensor[5] = ConvertBinaryToBool(buffer[47], 7);
                    ReceivedData2_AlarmStatus[4] = ConvertBinaryToBool(buffer[47], 6);
                    ReceivedData2_AlarmStatus[5] = ConvertBinaryToBool(buffer[48], 1);

                    //리밋센서, 홈센서
                }
            else if (msg.Contains("QDRVSPEED"))
            {
                ReceivedData2 = buffer;
                for (int i = 0; i < 3; i++)   /// 0,1,2 번 축 현재 움직이는 속도값 받아오기
                {
                    ReceivedData2_DrivingSpeed[i] = ConvertBinaryToInt(buffer, (4 * i + 6), Highbyte.LowHigh);

                }
                for (int i = 0; i < 2; i++)   /// 4,5 번 축 현재 움직이는 속도값 받아오기
                {
                    ReceivedData2_DrivingSpeed[i + 4] = ConvertBinaryToInt(buffer, (4 * i + 22), Highbyte.LowHigh);

                }
            }
            else if (msg.Contains("QPX"))   // 각 축의 세팅된 속도값, 가감속도값, Jerk값,Home속도 값 읽어오기
            {
                    //int axisNum=0;
                    int axisNum=0;


                    for (int i = 0; i < 6; i++)
                    {

                            string tmpstring = string.Format("QPX {0}", i);
                            if (msg.Contains(tmpstring) == true)
                            {
                                axisNum = i;
                            }

                    }

                    ReceivedData2_SettingStartSpeed[axisNum] = ConvertBinaryToInt(buffer, 6, Highbyte.LowHigh);
                    ReceivedData2_SettingAcc[axisNum] = ConvertBinaryToInt(buffer, 10, Highbyte.LowHigh);
                    ReceivedData2_SettingDec[axisNum] = ConvertBinaryToInt(buffer, 14, Highbyte.LowHigh);
                    ReceivedData2_SettingSpeed[axisNum] = ConvertBinaryToInt(buffer, 18, Highbyte.LowHigh);
                    ReceivedData2_AccJerk[axisNum] = ConvertBinaryToInt(buffer, 22, Highbyte.LowHigh);
                    ReceivedData2_DecJerk[axisNum] = ConvertBinaryToInt(buffer, 26, Highbyte.LowHigh);
                    ReceivedData2_HomingOffset[axisNum] = ConvertBinaryToInt(buffer, 74, Highbyte.LowHigh);
                    ReceivedData2_HomingFirstSpeed[axisNum] = ConvertBinaryToInt(buffer, 78, Highbyte.LowHigh);
                    ReceivedData2_HomingSecondSpeed[axisNum] = ConvertBinaryToInt(buffer, 82, Highbyte.LowHigh);
                    ReceivedData2_HomingThirdSpeed[axisNum] = ConvertBinaryToInt(buffer, 86, Highbyte.LowHigh);
                }
            else
            {
                ReceivedData = buffer;
            }

            }
            catch(Exception ex)
            {

            }
            finally
            {
                requestLock.Release();    // 하나의 태스크가 독점하던 권한 해제
            }

        }
        private bool CheckEnd(byte[] arr, int length)
        {
            for (int i = 0; i < length - 1; i++)
            {
                if (arr[i] == 10 && arr[i + 1] == 63)
                {
                    return true;
                }
            }

            return false;
        }
        public byte[] ResultMessage_CheckSum(string msg)   // 체크섬 계산 후 최종 메시지
        {
            byte[] tmparray = Encoding.ASCII.GetBytes(msg);

            ushort sum = 0;

            if (tmparray.Length % 2 == 0)    // 메세지 길이가 짝수
            {
                byte[] tmparray2 = new byte[tmparray.Length + 4];
                for (int i = 0; i < tmparray.Length; i++)
                {
                    tmparray2[i] = tmparray[i];
                }
                tmparray2[tmparray.Length] = 0x20;
                tmparray2[tmparray.Length + 1] = 0x0A;
                for (int i = 0; i < tmparray.Length + 1; i++)
                {
                    sum += tmparray2[i];
                }
                byte chksum_low = (byte)(sum & 0x00FF);
                byte chksum_high = (byte)((sum & 0xFF00) >> 8);
                tmparray2[tmparray.Length + 2] = chksum_low;
                tmparray2[tmparray.Length + 3] = chksum_high;
                return tmparray2;
            }
            else    // 메세지 길이가 홀수인 경우
            {
                byte[] tmparray2 = new byte[tmparray.Length + 5];
                for (int i = 0; i < tmparray.Length; i++)
                {
                    tmparray2[i] = tmparray[i];
                }
                tmparray2[tmparray.Length] = 0x20;
                tmparray2[tmparray.Length + 1] = 0x0A;
                tmparray2[tmparray.Length + 2] = 0x0A;
                for (int i = 0; i < tmparray.Length + 2; i++)
                {
                    sum += tmparray2[i];
                }
                byte chksum_low = (byte)(sum & 0x00FF);
                byte chksum_high = (byte)((sum & 0xFF00) >> 8);
                tmparray2[tmparray.Length + 3] = chksum_low;
                tmparray2[tmparray.Length + 4] = chksum_high;
                return tmparray2;
            }
        }            ////////// //    
        public async void JOG(byte axis, JOGDirection direction)
        {
            string msg = string.Format("1 INFMOV {0} {1}", axis.ToString("X"), (byte)direction);
            await RequestData(msg);
        }
        public async void PTP(byte axis, int position)
        {
            string position_hex = position.ToString("X");
            string msg = string.Format("1 APTPMOV {0} {1}", axis, position_hex);
            await RequestData(msg);
        }
        public async void STOP(byte axis)
        {
            string msg = string.Format("1 STPMTR {0}", axis.ToString("X"));
            await RequestData(msg);
        }
        public async void HOME(byte axis, byte HomingMethod)
        {
            string msg = string.Format("1 HOMEMOVE {0} {1} 3 0", axis.ToString("X"), HomingMethod.ToString("X"));
            await RequestData(msg);
        }
        public int ConvertBinaryToInt(byte[] data, int StartIndex,Highbyte w=Highbyte.LowHigh)  // 4byte Binary값에서 int값 추출
        {
            if (w == Highbyte.HighLOW)     // 수신 패킷에서 상위byte가 앞에 있을때
            {
                return (int)((0xFFFFFFFF & (data[StartIndex + 0] << 24) | (0xFFFFFFFF & (data[StartIndex + 1] << 16)) | (0xFFFFFFFF & (data[StartIndex + 2] << 8)) | (0xFFFFFFFF & data[StartIndex+3])));
            }
            else if (w == Highbyte.LowHigh)     // 수신 패킷에서 상위byte가 뒤에 있을때
            {
                try
                {
                    return (int)((0xFFFFFFFF & (data[StartIndex + 3] << 24) | (0xFFFFFFFF & (data[StartIndex + 2] << 16)) | (0xFFFFFFFF & (data[StartIndex + 1] << 8)) | (0xFFFFFFFF & data[StartIndex])));
                }
                catch
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public bool ConvertBinaryToBool(byte data, int bitNum)   //Binary값에서 bit값 추출
        {
            byte a = 1; 
            if(bitNum<=7)
            {
                for(int i=1; i<=bitNum;i++)
                {
                    a *= 2;
                }
               // a-= 1;
                
                try
                {
                    if (((a & data) / a) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show("dpfj");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

}

