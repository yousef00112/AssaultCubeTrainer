using Memory;
using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using telchid.ac_src;



namespace telchid
{
    public partial class AcForm : Form
    {
        methods? APIMethods;
        Entity localPlayer = new Entity();
        List<Entity> entities = new List<Entity>();
        Point lastPoint;
        Mem m = new Mem();
        private volatile bool shouldStopThread = false;
        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        public AcForm()
        {
            InitializeComponent();
        }

        private void AcForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Add("Right Mouse");
            comboBox1.Items.Add("Left Mouse");
            comboBox1.Items.Add("LControl");
            comboBox1.SelectedIndex = 0;
        }

        private byte[] ConvertHexStringToByteArray(string hexString)
        {
            string[] hexValuesSplit = hexString.Split(' ');
            byte[] bytes = new byte[hexValuesSplit.Length];
            for (int i = 0; i < hexValuesSplit.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexValuesSplit[i], 16);
            }
            return bytes;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            shouldStopThread = true;
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        public int GetNumericUpDownValue()
        {
            return (int)numericUpDown1.Value;
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private void wm()
        {
            while (!shouldStopThread)
            {
                if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                {
                    injection.ForeColor = Color.Red;
                    injection.Text = "False";
                    shouldStopThread = true;
                }
                localPlayer = APIMethods.ReadLocalPlayer();
                entities = APIMethods.ReadEntities(localPlayer);

                entities = entities.OrderBy(o => o.mag).ToList();



                int arammovalue = m.ReadInt("base+0x0017E0A8,140");
                int grenadevalue = m.ReadInt("base+0x0017E0A8,144");
                int pistolammovalue = m.ReadInt("base+0x0017E0A8,12C");
                int sniperammovalue = m.ReadInt("base+0x0017E0A8,13C");
                int shotgunammovalue = m.ReadInt("base+0x0017E0A8,134");
                int armorvalue = m.ReadInt("base+0x0017E0A8,F0");
                int healthvalue = m.ReadInt("base+0x0017E0A8,EC");

                string playerNameValue = m.ReadString("base+0x0017E0A8,205");

                float xvalue = m.ReadFloat("base+0x0017E0A8,2C");
                float yvalue = m.ReadFloat("base+0x0017E0A8,30");
                float zvalue = m.ReadFloat("base+0x0017E0A8,28");
                label9.Text = arammovalue.ToString();
                label10.Text = grenadevalue.ToString();
                label11.Text = pistolammovalue.ToString();
                label12.Text = armorvalue.ToString();
                label13.Text = healthvalue.ToString();
                label18.Text = xvalue.ToString();
                label19.Text = yvalue.ToString();
                label20.Text = zvalue.ToString();
                label28.Text = sniperammovalue.ToString();
                label30.Text = shotgunammovalue.ToString();
                label33.Text = playerNameValue;
                if (this.checkBox1.Checked)
                {
                    if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                    {
                        this.checkBox1.Checked = false;
                    }
                    else
                    {
                        m.WriteMemory("base+0x0017E0A8,140", "int", "999"); //AR AMMO
                        m.WriteMemory("base+0x0017E0A8,12C", "int", "999"); //PISTOL AMMO
                        m.WriteMemory("base+0x0017E0A8,148", "int", "999"); //DOUBLE PISTOL AMMO
                        m.WriteMemory("base+0x0017E0A8,13C", "int", "999"); //SNIPER AMMO
                        m.WriteMemory("base+0x0017E0A8,134", "int", "999"); //SHOTGUN AMMO

                    }
                }
                if (this.checkBox5.Checked)
                {
                    if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                    {
                        this.checkBox5.Checked = false;
                    }
                    else
                    {
                        string address = "004C66A5";
                        string bytesString = "0F BF 70 08";
                        byte[] bytes = ConvertHexStringToByteArray(bytesString);
                        m.WriteBytes(address, bytes);

                    }
                }
                if (!this.checkBox5.Checked)
                {
                    if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                    {
                        this.checkBox5.Checked = false;
                    }
                    else
                    {
                        string address = "004C66A5";
                        string bytesString = "0F BF 70 54";
                        byte[] bytes = ConvertHexStringToByteArray(bytesString);
                        m.WriteBytes(address, bytes);

                    }
                }
                if (this.checkBox7.Checked)
                {
                    if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                    {
                        this.checkBox7.Checked = false;
                    }
                    else
                    {
                        string address = "004CA3A4";
                        string bytesString = "90 90";
                        byte[] bytes = ConvertHexStringToByteArray(bytesString);
                        m.WriteBytes(address, bytes);

                    }
                }
                if (!this.checkBox7.Checked)
                {
                    if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                    {
                        this.checkBox7.Checked = false;
                    }
                    else
                    {
                        string address = "004CA3A4";
                        string bytesString = "74 12";
                        byte[] bytes = ConvertHexStringToByteArray(bytesString);
                        m.WriteBytes(address, bytes);

                    }
                }
                if (this.checkBox9.Checked)
                {
                    if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                    {
                        this.checkBox9.Checked = false;
                    }
                    else
                    {
                        string address = "004C7311";
                        string bytesString = "80 78 68 00";
                        byte[] bytes = ConvertHexStringToByteArray(bytesString);
                        m.WriteBytes(address, bytes);

                    }
                }
                if (!this.checkBox9.Checked)
                {
                    if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                    {
                        this.checkBox9.Checked = false;
                    }
                    else
                    {
                        string address = "004C7311";
                        string bytesString = "80 78 66 00";
                        byte[] bytes = ConvertHexStringToByteArray(bytesString);
                        m.WriteBytes(address, bytes);

                    }
                }
                if (this.checkBox8.Checked)
                {
                    if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                    {
                        this.checkBox8.Checked = false;
                    }
                    else
                    {
                        string address = "004C2233";
                        string bytesString = "90 90";
                        byte[] bytes = ConvertHexStringToByteArray(bytesString);
                        m.WriteBytes(address, bytes);

                    }
                }
                if (!this.checkBox8.Checked)
                {
                    if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                    {
                        this.checkBox8.Checked = false;
                    }
                    else
                    {
                        string address = "004C2233";
                        string bytesString = "75 69";
                        byte[] bytes = ConvertHexStringToByteArray(bytesString);
                        m.WriteBytes(address, bytes);

                    }
                }
                if (this.checkBox6.Checked)
                {
                    if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                    {
                        this.checkBox6.Checked = false;
                    }
                    else
                    {
                        m.WriteMemory("base+0x0017E0A8,144", "int", "999"); //GRENADES

                    }
                }
                if (this.checkBox2.Checked)
                {
                    if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                    {
                        this.checkBox2.Checked = false;
                    }
                    else
                    {
                        m.WriteMemory("base+0x0017E0A8,F0", "int", "999");
                        m.WriteMemory("base+0x0017E0A8,EC", "int", "999");
                    }
                }
                if (this.checkBox3.Checked)
                {
                    if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                    {
                        this.checkBox3.Checked = false;
                    }
                    else
                    {
                        string address = "004C2EC3";
                        string bytesString = "90 90 90 90 90";
                        byte[] bytes = ConvertHexStringToByteArray(bytesString);
                        m.WriteBytes(address, bytes);
                    }
                }
                if (!this.checkBox3.Checked)
                {
                    if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0")
                    {
                        this.checkBox5.Checked = false;
                    }
                    else
                    {
                        string address = "004C2EC3";
                        string bytesString = "F3 0F 11 56 38";
                        byte[] bytes = ConvertHexStringToByteArray(bytesString);
                        m.WriteBytes(address, bytes);
                    }
                }
                if (this.checkBox4.Checked)
                {
                    string key = comboBox1.SelectedItem.ToString();
                    if (key == "Left Mouse")
                    {
                        if (GetAsyncKeyState(Keys.LButton) < 0)
                        {
                            if (entities.Count > 0)
                            {
                                foreach (var ent in entities)
                                {
                                    if (ent.team != localPlayer.team)
                                    {
                                        var angles = APIMethods.CalculateAngles(localPlayer, ent);
                                        APIMethods.Aim(localPlayer, angles.X, angles.Y);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (key == "Right Mouse")
                    {
                        if (GetAsyncKeyState(Keys.RButton) < 0)
                        {
                            if (entities.Count > 0)
                            {
                                foreach (var ent in entities)
                                {
                                    if (ent.team != localPlayer.team)
                                    {
                                        var angles = APIMethods.CalculateAngles(localPlayer, ent);
                                        APIMethods.Aim(localPlayer, angles.X, angles.Y);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (key == "LControl")
                    {
                        if (GetAsyncKeyState(Keys.LControlKey) < 0)
                        {
                            if (entities.Count > 0)
                            {
                                foreach (var ent in entities)
                                {
                                    if (ent.team != localPlayer.team)
                                    {
                                        var angles = APIMethods.CalculateAngles(localPlayer, ent);
                                        APIMethods.Aim(localPlayer, angles.X, angles.Y);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }

        private void button6_Click(object sender, EventArgs e)

        {
            int id = m.GetProcIdFromName("ac_client");
            if (id > 0)
            {
                m.OpenProcess(id);
                injection.Text = "True";
                injection.ForeColor = Color.Green;

                APIMethods = new methods();
                if (APIMethods != null)
                {
                    Thread thread = new Thread(wm) { IsBackground = true };
                    thread.Start();

                }

            }
            else
            {
                MessageBox.Show("game not found", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                injection.Text = "False";
                injection.ForeColor = Color.Red;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            shouldStopThread = true;
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.injection.Text == "True")
            {
                int xpos = (int)numericUpDown1.Value;
                string xPosString = xpos.ToString();
                m.WriteMemory("base+0x0017E0A8,2C", "float", xPosString);
                int ypos = (int)numericUpDown2.Value;
                string yPosString = ypos.ToString();
                m.WriteMemory("base+0x0017E0A8,30", "float", yPosString);
                int zpos = (int)numericUpDown3.Value;
                string zPosString = zpos.ToString();
                m.WriteMemory("base+0x0017E0A8,28", "float", zPosString);
            }
            else
            {
                MessageBox.Show("inject first!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.injection.Text == "True")
            {
                float xvalue = m.ReadFloat("base+0x0017E0A8,2C");
                float yvalue = m.ReadFloat("base+0x0017E0A8,30");
                float zvalue = m.ReadFloat("base+0x0017E0A8,28");
                numericUpDown1.Value = (decimal)xvalue;
                numericUpDown2.Value = (decimal)yvalue;
                numericUpDown3.Value = (decimal)zvalue;
            }
            else
            {
                MessageBox.Show("inject first!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            string cmdCommand = "taskkill /f /im ac_client.exe";

            Process cmdProcess = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "cmd.exe";
            processStartInfo.Arguments = $"/C {cmdCommand}";
            processStartInfo.CreateNoWindow = true;
            cmdProcess.StartInfo = processStartInfo;
            cmdProcess.Start();
        }

        private void button8_Click(object sender, EventArgs e) //Rapid fire
        {
            if (label18.Text == "0" && label19.Text == "0" && label20.Text == "0" && this.injection.Text == "False")
            {
                MessageBox.Show("Inject First!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string address = "004C73E3";
                string oldBytesString = "0F BF 48 54";
                string newBytesString = "0F BF 48 08";
                byte[] oldBytes = ConvertHexStringToByteArray(oldBytesString);
                byte[] newBytes = ConvertHexStringToByteArray(newBytesString);
                m.WriteBytes(address, newBytes);
                m.WriteBytes(address, oldBytes);
            }
        }
    }
}
