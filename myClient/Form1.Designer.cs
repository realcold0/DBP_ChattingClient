namespace myClient
{
    partial class ClientForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChatLog = new System.Windows.Forms.RichTextBox();
            this.ChatterList = new System.Windows.Forms.ListBox();
            this.InputField = new System.Windows.Forms.TextBox();
            this.EnterBtn = new System.Windows.Forms.Button();
            this.QuitBtn = new System.Windows.Forms.Button();
            this.ConnBtn = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.Label();
            this.textBoxSearchMember = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ChatLog
            // 
            this.ChatLog.Location = new System.Drawing.Point(420, 36);
            this.ChatLog.Margin = new System.Windows.Forms.Padding(4);
            this.ChatLog.Name = "ChatLog";
            this.ChatLog.Size = new System.Drawing.Size(250, 289);
            this.ChatLog.TabIndex = 0;
            this.ChatLog.Text = "";
            // 
            // ChatterList
            // 
            this.ChatterList.FormattingEnabled = true;
            this.ChatterList.ItemHeight = 20;
            this.ChatterList.Location = new System.Drawing.Point(212, 269);
            this.ChatterList.Margin = new System.Windows.Forms.Padding(4);
            this.ChatterList.Name = "ChatterList";
            this.ChatterList.Size = new System.Drawing.Size(199, 104);
            this.ChatterList.TabIndex = 1;
            // 
            // InputField
            // 
            this.InputField.Location = new System.Drawing.Point(420, 344);
            this.InputField.Margin = new System.Windows.Forms.Padding(4);
            this.InputField.Name = "InputField";
            this.InputField.Size = new System.Drawing.Size(250, 27);
            this.InputField.TabIndex = 2;
            this.InputField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputField_KeyDown);
            // 
            // EnterBtn
            // 
            this.EnterBtn.Location = new System.Drawing.Point(679, 343);
            this.EnterBtn.Margin = new System.Windows.Forms.Padding(4);
            this.EnterBtn.Name = "EnterBtn";
            this.EnterBtn.Size = new System.Drawing.Size(96, 31);
            this.EnterBtn.TabIndex = 3;
            this.EnterBtn.Text = "전송";
            this.EnterBtn.UseVisualStyleBackColor = true;
            this.EnterBtn.Click += new System.EventHandler(this.EnterBtn_Click);
            // 
            // QuitBtn
            // 
            this.QuitBtn.Location = new System.Drawing.Point(679, 296);
            this.QuitBtn.Margin = new System.Windows.Forms.Padding(4);
            this.QuitBtn.Name = "QuitBtn";
            this.QuitBtn.Size = new System.Drawing.Size(96, 31);
            this.QuitBtn.TabIndex = 4;
            this.QuitBtn.Text = "나가기";
            this.QuitBtn.UseVisualStyleBackColor = true;
            this.QuitBtn.Click += new System.EventHandler(this.QuitBtn_Click);
            // 
            // ConnBtn
            // 
            this.ConnBtn.Location = new System.Drawing.Point(679, 35);
            this.ConnBtn.Margin = new System.Windows.Forms.Padding(4);
            this.ConnBtn.Name = "ConnBtn";
            this.ConnBtn.Size = new System.Drawing.Size(96, 31);
            this.ConnBtn.TabIndex = 5;
            this.ConnBtn.Text = "서버연결";
            this.ConnBtn.UseVisualStyleBackColor = true;
            this.ConnBtn.Click += new System.EventHandler(this.ConnBtn_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(15, 75);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(188, 300);
            this.treeView1.TabIndex = 6;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "대화상대";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(212, 36);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(199, 204);
            this.listBox1.TabIndex = 8;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "대화리스트";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 245);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "참여자리스트";
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(420, 12);
            this.name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(54, 20);
            this.name.TabIndex = 11;
            this.name.Text = "상대방";
            // 
            // textBoxSearchMember
            // 
            this.textBoxSearchMember.Location = new System.Drawing.Point(15, 36);
            this.textBoxSearchMember.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSearchMember.Name = "textBoxSearchMember";
            this.textBoxSearchMember.Size = new System.Drawing.Size(188, 27);
            this.textBoxSearchMember.TabIndex = 12;
            this.textBoxSearchMember.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearchMember_KeyDown);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 395);
            this.Controls.Add(this.textBoxSearchMember);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.ConnBtn);
            this.Controls.Add(this.QuitBtn);
            this.Controls.Add(this.EnterBtn);
            this.Controls.Add(this.InputField);
            this.Controls.Add(this.ChatterList);
            this.Controls.Add(this.ChatLog);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ClientForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox ChatLog;
        private ListBox ChatterList;
        private TextBox InputField;
        private Button EnterBtn;
        private Button QuitBtn;
        private Button ConnBtn;
        private TreeView treeView1;
        private Label label1;
        private ListBox listBox1;
        private Label label2;
        private Label label3;
        private Label name;
        private TextBox textBoxSearchMember;
    }
}