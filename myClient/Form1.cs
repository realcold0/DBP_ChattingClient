using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Data;
using System.Collections;

namespace myClient
{
    public partial class ClientForm : Form
    {
        string ID;
        NetworkStream stream = default(NetworkStream);
        TcpClient client = new TcpClient();

        public ClientForm(string ID)
        {
            this.ID = ID;
            InitializeComponent();
        }
        void Connect(string s)
        {
            char split = ' ';
            string[] parsedIP = s.Split(split); // 1 : IP, 2 : server, 3 : server Port

            try
            {
                IPEndPoint clientAddr = new IPEndPoint(IPAddress.Parse(parsedIP[1]), 0);
                IPEndPoint serverAddr = new IPEndPoint(IPAddress.Parse(parsedIP[2]), Int32.Parse(parsedIP[3]));
                client = new TcpClient(clientAddr);
                client.Connect(serverAddr);
                stream = client.GetStream();
                Display("Chat Server Connected...");
            }
            catch (Exception e)
            {
                Display("Failed to Connect...");
            }
            if (!client.Connected)
            {
                MessageBox.Show("usage : /connect ����IP ����PORT");
                InputField.Text = string.Empty;
                InputField.Focus();
            }
            else
            {
                byte[] buffer = Encoding.Default.GetBytes(ID);
                stream.Write(buffer, 0, buffer.Length);

                Thread clientThread = new Thread(Receiver);
                clientThread.IsBackground = true;
                clientThread.Start();
            }

        }
        void Display(string msg)
        {
            AddFunc addFunc = new AddFunc(this);
            msg = addFunc.SplitMsg(ID, name.Text, msg); // ����(����): ä��â ���� ������ �޼��� �����ֱ�
            CheckForIllegalCrossThreadCalls = false;
            
            if (msg.Contains("!!Notification!!"))
            { // �˸��� �� ���
                string[] strlist = msg.Split(" ");
                //Thread thread = new Thread(Alert);
                //thread.IsBackground = true;
                Alert(strlist[1]);
                // �˸��� ������ ����
                // �˸��� ���� �� �߰� DB�� ���� ��Ű�� �۾��� �߰��ؾ���
                return;
            }

            if (msg != "")// ����(����): NULL�� �ƴ϶�� ChatLog.AppendText
            {
                ChatLog.AppendText(msg + "\r\n");
                ChatLog.Select(ChatLog.Text.Length, 0);
                ChatLog.ScrollToCaret();
            }
        }
        void Receiver()
        {
            try
            {
                while (true)
                {
                    int bufferSize = client.ReceiveBufferSize;
                    byte[] buffer = new byte[bufferSize];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string msg = Encoding.Default.GetString(buffer, 0, bytes);
                    if (!isControlMsg(msg)) Display(msg);
                }
            }
            catch (IOException e)
            {
                //
            }
        }
        bool isControlMsg(string msg)
        {
            bool isCtrl = true;
            if (msg.StartsWith("**userlist** "))
            {
                ChatterList.Items.Clear();
                char split = ' ';
                string[] userlist = msg.Split(split);
                foreach (string s in userlist)
                {
                    if (s.StartsWith("**")) continue;
                    ChatterList.Items.Add(s);
                }
            }
            else isCtrl = false;
            return isCtrl;
        }
        void RefreshUserlist(string[] ul)
        {

        }
        void Sender(string s)
        {
            byte[] buffer = Encoding.Default.GetBytes(s);
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
        }
        void Controller(string s)
        {
            if (s.StartsWith("/connect "))
            {
                Connect(s);
            }
            else if (!client.Connected)
            {
                MessageBox.Show("������ ���� �������ּ���.");
                InputField.Text = string.Empty;
                InputField.Focus();
            }
            else if (s.Equals("/exit"))
            {
                Disconnect();
            }
            else
            {
                string str = name.Text + " " + s; // ����(����): �޴� ��� �̸� ����
                Sender(str);
            }
        }
        void Disconnect()
        {
            byte[] buffer = Encoding.Default.GetBytes("/exit");
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
            stream.Close();
            client.Close();
            Application.ExitThread();
            this.Close();
        }
        private void InputField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) EnterBtn_Click(sender, e);
        }
        private void EnterBtn_Click(object sender, EventArgs e)
        {
            string input = InputField.Text;
            Controller(input);
            InputField.Text = string.Empty;
            InputField.Focus();
        }
        private void ConnBtn_Click(object sender, EventArgs e)// ���� ��(����): ���� Mysql ���� ����
        {
            ArrayList serverIP = new ArrayList();
            serverIP = DBManager.GetInstance().Select("select * from Server;", "IP");
            string input = string.Format("/connect  {0}", serverIP[0]);
            Controller(input);
        }
        private void QuitBtn_Click(object sender, EventArgs e)
        {
            string input = "/exit";
            Controller(input);
        }
        private void ClientForm_Load(object sender, EventArgs e)
        {
            AddFunc addFunc = new AddFunc(this);
            // ����(����): TreeView Load
            ArrayList department = new ArrayList(DBManager.GetInstance().Select("select * from �μ�;", "�μ���"));
            for (int i = 0; i < department.Count; i++)
            {
                treeView1.Nodes.Add(addFunc.UserLoad(i, department[i].ToString())); 
            }
            // ����(����): 
            listBox1.DataSource = addFunc.ChatListLoad(ID); 
        }
        // ������� �ڵ� �ۼ�, �� �κп� ���� ����� ���� ���� �ڵ� ǥ�õǾ� �ֽ��ϴ�. (����)
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ����(����): ä�� ����� �̸� ǥ��
            name.Text = listBox1.Text;
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // ���� ��(����): TreeView Ŭ���� �� �������� �׽�Ʈ, ���� �纯��
            MessageBox.Show(e.Node.Text);
        }

        private void textBoxSearchMember_KeyDown(object sender, KeyEventArgs e)  //�߰� ��ȯ
        {
            if (e.KeyCode == Keys.Return) //�̸� ã��
            {
                treeView1.Nodes.Clear();
                string query = string.Format("select * from ȸ������ where �̸� = '{0}';", textBoxSearchMember.Text);
                ArrayList user = new ArrayList(DBManager.GetInstance().Select(query,"�̸�"));
                ArrayList userTeamID = new ArrayList(DBManager.GetInstance().Select(query, "����ID"));
                for (int i = 0; i<user.Count;i++)  //���������ϼ��� ;;
                {
                    treeView1.Nodes.Add(AddFunc.UserSearch(user[i].ToString(), Convert.ToInt32(userTeamID[i])));
                }
               
            }
            /*
            if (e.KeyCode == Keys.Return)  //�μ� ã��
            {
                treeView1.Nodes.Clear();
                string query1 = string.Format("select �μ��� from �μ� where �μ��� = '{0}';", textBoxSearchMember.Text);
                ArrayList department = new ArrayList(DBManager.GetInstance().Select(query1, "�μ���")); 
                for(int i = 0; i < department.Count; i++)
                {
                    treeView1.Nodes.Add(AddFunc.DepartmentSearch(department[i].ToString()));
                }
            }
            if (e.KeyCode == Keys.Return)
            {
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(AddFunc.UserIDSearch(textBoxSearchMember.Text));
            }
            */
        }

        public void Alert(string msg)
        {
            notify frm = new notify();
            frm.showAlert(msg + "�Կ��Լ� �޼����� �����߽��ϴ�.");
        }
    }
}