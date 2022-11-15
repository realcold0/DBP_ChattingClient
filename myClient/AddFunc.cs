using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace myClient
{
    internal class AddFunc
    {
        Form ClientForm;
        public AddFunc(Form clientForm)
        {
            ClientForm = clientForm;
        }
        // TreeView Load 함수
        public TreeNode UserLoad(int i, string department)// 추가(영주)
        {
            TreeNode treeNode = new TreeNode(department);
            int teamnum = 0; // TeamID 

            string teamquery = string.Format("select * from `부서-팀` where 부서ID = '{0}';", i);
            ArrayList team = new ArrayList(DBManager.GetInstance().Select(teamquery, "팀명"));
            ArrayList teamlist = new ArrayList(DBManager.GetInstance().Select(teamquery, "팀명ID"));
            // ex) 인사부서의 팀명ID(2(개발팀), 4(행정팀))
            for (int j = 0; j < team.Count; j++)
            {
                teamnum = Convert.ToInt32(teamlist[j]); // ex) teamnum = 2
                treeNode.Nodes.Add(team[j].ToString()); // ex) 개발팀 추가
                string userquery = string.Format("select * from 회원정보 where 팀명ID = '{0}';", teamnum);
                ArrayList user = new ArrayList(DBManager.GetInstance().Select(userquery, "이름"));
                // ex) 팀명ID가 2인 회원정보 추가
                for (int k = 0; k < user.Count; k++)
                {
                    treeNode.Nodes[j].Nodes.Add(user[k].ToString());
                }
            }

            return treeNode;
        }
        public static TreeNode UserSearch(string searchText)   //추가 승환
        {
            
            string searchInUser = string.Format("select * from 회원정보 where 이름 = '{0}';", searchText);
            ArrayList user = new ArrayList(DBManager.GetInstance().Select(searchInUser, "이름"));
            ArrayList userTeamID = new ArrayList(DBManager.GetInstance().Select(searchInUser, "팀명ID")); //팀 id 알아내기

            
            string searchDepart = string.Format("select 팀명, 부서.부서명 from 부서,`부서-팀` where 팀명ID = {0} and `부서-팀`.부서ID = 부서.부서ID", 
                Convert.ToInt32(userTeamID[0])); //부서 찾기
            ArrayList searchDepartOFUser = new ArrayList(DBManager.GetInstance().Select(searchDepart, "부서명"));
            ArrayList userTeam = new ArrayList(DBManager.GetInstance().Select(searchDepart, "팀명"));


            //유저 찾아서 팀명 확인 , 이팀이 어디 부서인지 확인 
            TreeNode treeNode = new TreeNode(searchDepartOFUser[0].ToString());
            treeNode.Nodes.Add(userTeam[0].ToString());
            treeNode.Nodes[0].Nodes.Add(user[0].ToString());

            return treeNode;
        }

        public static TreeNode DepartmentSearch(string searchText)
        {

        }

        // 채팅방 목록 추가
        // ex) "영주"가 대화를 했던 사람들의 목록
        public ArrayList ChatListLoad(string ID)// 추가(영주)
        {
            string name = ID;
            string chatlistquery = string.Format("select * from 채팅방목록 where 보낸유저 = '{0}';", name);
            ArrayList chatlist = new ArrayList(DBManager.GetInstance().Select(chatlistquery, "받는유저"));
            return chatlist;
        }
        // 채팅방 창 나눠주기
        /*
         * ex) 보낸사람: 영주, 받는사람: 승환
         *     현재 대화창: 승환
         *     보낸사람==영주 && 받는사람==승환 -> [나] msg
         *     보낸사람==승환 && 받는사람==영주 -> [승환] msg
         *     그 외 -> msg = string.empty
         */
        public string SplitMsg(string From, string To, string msg)// 추가(영주)
        {
            string[] strings = msg.Split(" ");
            if (strings[0] == From && strings[1] == To)
            {
                int len = strings[0].Length + strings[1].Length + 1;
                string subSTR = msg.Substring(0, len);
                msg = msg.Replace(subSTR, "[나]");
                return msg;
            }
            else if (strings[0] == To && strings[1] == From)
            {
                int len = strings[0].Length + strings[1].Length + 1;
                string subSTR = msg.Substring(0, len);
                msg = msg.Replace(subSTR, "[" + To + "]");
                return msg;
            }
            else
            {
                msg = string.Empty;
                return msg;
            }
        }
    }
}
