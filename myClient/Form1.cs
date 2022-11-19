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
                MessageBox.Show("usage : /connect 서버IP 서버PORT");
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
            msg = addFunc.SplitMsg(ID, name.Text, msg); // 변경(영주): 채팅창 별로 나눠서 메세지 보여주기
            CheckForIllegalCrossThreadCalls = false;
            
            if (msg.Contains("!!Notification!!"))
            { // 알림이 온 경우
                string[] strlist = msg.Split(" ");
                //Thread thread = new Thread(Alert);
                //thread.IsBackground = true;
                Alert(strlist[1]);
                // 알림을 보내고 종료
                // 알림을 보낸 후 추가 DB에 적응 시키는 작업을 추가해야함
                return;
            }

            if (msg != "")// 변경(영주): NULL이 아니라면 ChatLog.AppendText
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
                MessageBox.Show("서버에 먼저 연결해주세요.");
                InputField.Text = string.Empty;
                InputField.Focus();
            }
            else if (s.Equals("/exit"))
            {
                Disconnect();
            }
            else
            {
                string str = name.Text + " " + s; // 변경(영주): 받는 사람 이름 포함
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
        private void ConnBtn_Click(object sender, EventArgs e)// 변경 중(영주): 서버 Mysql 저장 예정
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
            // 변경(영주): TreeView Load
            ArrayList department = new ArrayList(DBManager.GetInstance().Select("select * from 부서;", "부서명"));
            for (int i = 0; i < department.Count; i++)
            {
                treeView1.Nodes.Add(addFunc.UserLoad(i, department[i].ToString())); 
            }
            // 변경(영주): 
            listBox1.DataSource = addFunc.ChatListLoad(ID); 
        }
        // 여기부터 코드 작성, 윗 부분에 변경 내용과 변경 중인 코드 표시되어 있습니다. (영주)
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 변경(영주): 채팅 상대의 이름 표시
            name.Text = listBox1.Text;
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 변경 중(영주): TreeView 클릭시 잘 나오는지 테스트, 추후 재변경
            MessageBox.Show(e.Node.Text);
        }

        private void textBoxSearchMember_KeyDown(object sender, KeyEventArgs e)  //추가 승환
        {
            if (e.KeyCode == Keys.Return) //이름 찾기
            {
                treeView1.Nodes.Clear();
                string query = string.Format("select * from 회원정보 where 이름 = '{0}';", textBoxSearchMember.Text);
                ArrayList user = new ArrayList(DBManager.GetInstance().Select(query,"이름"));
                ArrayList userTeamID = new ArrayList(DBManager.GetInstance().Select(query, "팀명ID"));
                for (int i = 0; i<user.Count;i++)  //동명이인일수도 ;;
                {
                    treeView1.Nodes.Add(AddFunc.UserSearch(user[i].ToString(), Convert.ToInt32(userTeamID[i])));
                }
               
            }
            /*
            if (e.KeyCode == Keys.Return)  //부서 찾기
            {
                treeView1.Nodes.Clear();
                string query1 = string.Format("select 부서명 from 부서 where 부서명 = '{0}';", textBoxSearchMember.Text);
                ArrayList department = new ArrayList(DBManager.GetInstance().Select(query1, "부서명")); 
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
            frm.showAlert(msg + "님에게서 메세지가 도착했습니다.");
        }
    }
}