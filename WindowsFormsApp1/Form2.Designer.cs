namespace WindowsFormsApp1
{
    partial class Form2
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.AxisName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Acc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Current_Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BusyStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.ErrorStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.MinusLimit = new System.Windows.Forms.DataGridViewImageColumn();
            this.HomeSensor = new System.Windows.Forms.DataGridViewImageColumn();
            this.PlusLimit = new System.Windows.Forms.DataGridViewImageColumn();
            this.DrivingSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HomeSpeedFirst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HomeSpeedSecond = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HomeSpeedThird = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HomeOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AxisName,
            this.StartSpeed,
            this.Speed,
            this.Acc,
            this.Dec,
            this.Current_Position,
            this.BusyStatus,
            this.ErrorStatus,
            this.MinusLimit,
            this.HomeSensor,
            this.PlusLimit,
            this.DrivingSpeed,
            this.HomeSpeedFirst,
            this.HomeSpeedSecond,
            this.HomeSpeedThird,
            this.HomeOffset});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(641, 204);
            this.dataGridView1.TabIndex = 0;
            // 
            // AxisName
            // 
            this.AxisName.HeaderText = "AxisName";
            this.AxisName.Name = "AxisName";
            this.AxisName.ReadOnly = true;
            // 
            // StartSpeed
            // 
            this.StartSpeed.HeaderText = "StartSpeed";
            this.StartSpeed.Name = "StartSpeed";
            this.StartSpeed.ReadOnly = true;
            // 
            // Speed
            // 
            this.Speed.HeaderText = "speed";
            this.Speed.Name = "Speed";
            this.Speed.ReadOnly = true;
            // 
            // Acc
            // 
            this.Acc.HeaderText = "acc";
            this.Acc.Name = "Acc";
            this.Acc.ReadOnly = true;
            // 
            // Dec
            // 
            this.Dec.HeaderText = "dec";
            this.Dec.Name = "Dec";
            this.Dec.ReadOnly = true;
            // 
            // Current_Position
            // 
            this.Current_Position.HeaderText = "current position";
            this.Current_Position.Name = "Current_Position";
            this.Current_Position.ReadOnly = true;
            // 
            // BusyStatus
            // 
            this.BusyStatus.HeaderText = "BusyStatus";
            this.BusyStatus.Name = "BusyStatus";
            this.BusyStatus.ReadOnly = true;
            this.BusyStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BusyStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ErrorStatus
            // 
            this.ErrorStatus.HeaderText = "ErrorStatus";
            this.ErrorStatus.Name = "ErrorStatus";
            this.ErrorStatus.ReadOnly = true;
            this.ErrorStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MinusLimit
            // 
            this.MinusLimit.HeaderText = "MinusLimit";
            this.MinusLimit.Name = "MinusLimit";
            this.MinusLimit.ReadOnly = true;
            this.MinusLimit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // HomeSensor
            // 
            this.HomeSensor.HeaderText = "HomeSensor";
            this.HomeSensor.Name = "HomeSensor";
            this.HomeSensor.ReadOnly = true;
            this.HomeSensor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PlusLimit
            // 
            this.PlusLimit.HeaderText = "PlusLimit";
            this.PlusLimit.Name = "PlusLimit";
            this.PlusLimit.ReadOnly = true;
            this.PlusLimit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PlusLimit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DrivingSpeed
            // 
            this.DrivingSpeed.HeaderText = "DrivingSpeed";
            this.DrivingSpeed.Name = "DrivingSpeed";
            this.DrivingSpeed.ReadOnly = true;
            // 
            // HomeSpeedFirst
            // 
            this.HomeSpeedFirst.HeaderText = "HomeSpeed 1차";
            this.HomeSpeedFirst.Name = "HomeSpeedFirst";
            this.HomeSpeedFirst.ReadOnly = true;
            this.HomeSpeedFirst.Width = 110;
            // 
            // HomeSpeedSecond
            // 
            this.HomeSpeedSecond.HeaderText = "HomeSpeed 2차";
            this.HomeSpeedSecond.Name = "HomeSpeedSecond";
            this.HomeSpeedSecond.ReadOnly = true;
            this.HomeSpeedSecond.Width = 110;
            // 
            // HomeSpeedThird
            // 
            this.HomeSpeedThird.HeaderText = "HomeSpeed 3차";
            this.HomeSpeedThird.Name = "HomeSpeedThird";
            this.HomeSpeedThird.ReadOnly = true;
            this.HomeSpeedThird.Width = 110;
            // 
            // HomeOffset
            // 
            this.HomeOffset.HeaderText = "HomeOffset";
            this.HomeOffset.Name = "HomeOffset";
            this.HomeOffset.ReadOnly = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(95, 304);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Axis";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 342);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Parameter";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(95, 339);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(95, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 53);
            this.button1.TabIndex = 5;
            this.button1.Text = "Set Value";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(95, 375);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 21);
            this.textBox1.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(449, 327);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 69);
            this.button2.TabIndex = 8;
            this.button2.Text = "JOG_CCW";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button2_MouseDown);
            this.button2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button2_MouseUp);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(528, 327);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(73, 69);
            this.button3.TabIndex = 9;
            this.button3.Text = "JOG_CW";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button3_MouseDown);
            this.button3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button3_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(437, 304);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "Axis";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(477, 300);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 20);
            this.comboBox3.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(16, 259);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 234);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter Setting";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(408, 259);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 234);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "JOG";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 560);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "Axis";
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(124, 556);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(121, 20);
            this.comboBox4.TabIndex = 15;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(125, 585);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(120, 21);
            this.textBox2.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 589);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "Target Position";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(61, 620);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(101, 53);
            this.button4.TabIndex = 19;
            this.button4.Text = "PTP MOVE";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(180, 620);
            this.button5.Name = "button5";
            this.button5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button5.Size = new System.Drawing.Size(101, 53);
            this.button5.TabIndex = 20;
            this.button5.Text = "STOP";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(437, 561);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "Axis";
            // 
            // comboBox5
            // 
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(477, 557);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(121, 20);
            this.comboBox5.TabIndex = 21;
            // 
            // comboBox6
            // 
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(477, 586);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(121, 20);
            this.comboBox6.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(405, 589);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "Home방법";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(477, 620);
            this.button6.Name = "button6";
            this.button6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button6.Size = new System.Drawing.Size(101, 53);
            this.button6.TabIndex = 25;
            this.button6.Text = "HOME";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(12, 521);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(278, 161);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PTP";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(408, 521);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(252, 161);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "HOME";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(16, 760);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(390, 106);
            this.textBox3.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 745);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 12);
            this.label8.TabIndex = 29;
            this.label8.Text = "보낸 패킷";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 894);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBox6);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox5);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn AxisName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Speed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Acc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Current_Position;
        private System.Windows.Forms.DataGridViewImageColumn BusyStatus;
        private System.Windows.Forms.DataGridViewImageColumn ErrorStatus;
        private System.Windows.Forms.DataGridViewImageColumn MinusLimit;
        private System.Windows.Forms.DataGridViewImageColumn HomeSensor;
        private System.Windows.Forms.DataGridViewImageColumn PlusLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrivingSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn HomeSpeedFirst;
        private System.Windows.Forms.DataGridViewTextBoxColumn HomeSpeedSecond;
        private System.Windows.Forms.DataGridViewTextBoxColumn HomeSpeedThird;
        private System.Windows.Forms.DataGridViewTextBoxColumn HomeOffset;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label8;
    }
}