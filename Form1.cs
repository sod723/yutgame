using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace yutgame
{
    public partial class Form1 : Form
    {

        public NetworkStream m_Stream;
        public NetworkStream m_Stream2;
        public StreamReader m_Read;
        public StreamWriter m_Write;

        public StreamReader m_Read2;
        public StreamWriter m_Write2;

        private Thread m_ThReader;
        private Thread m_ThReader2;

        public bool m_bStop = false;

        private TcpListener m_listener;
        private TcpListener m_listener2;
        private Thread m_thServer;
        private Thread m_thServer2;

        public bool m_bConnect = false;
        TcpClient m_Client;
        TcpClient m_Client2;

        static int num;
        int yutnum;
        static string oppyut;
        bool blue = false;
        bool red = false;
        static int[] yutinfo=new int[2];
        static int yutswitch=-1;
        static int bluehorse1=-1;
        static int bluehorse2=-1;
        static int redhorse1=-1;
        static int redhorse2=-1;
        static int opphorse2;
        static int oppyut2;

        int outlineCnt1 = 0;
        int outlineCnt2 = 0;
        int outlineCnt3 = 0;
        int outlineCnt4 = 0;


        int firstShortCutCnt1 = 0;
        int firstShortCutCnt2 = 0;
        int firstShortCutCnt3 = 0;
        int firstShortCutCnt4 = 0;

        int centerShortCutCnt1 = 0;
        int centerShortCutCnt2 = 0;
        int centerShortCutCnt3 = 0;
        int centerShortCutCnt4 = 0;

        int lastShortCutCnt1 = 0;
        int lastShortCutCnt2 = 0;
        int lastShortCutCnt3 = 0;
        int lastShortCutCnt4 = 0;

        int firstShortCutCheck1;
        int firstShortCutCheck2;
        int firstShortCutCheck3;
        int firstShortCutCheck4;

        int centerShortCutCheck1;
        int centerShortCutCheck2;
        int centerShortCutCheck3;
        int centerShortCutCheck4;

        int lastShortCutCheck1;
        int lastShortCutCheck2;
        int lastShortCutCheck3;
        int lastShortCutCheck4;

        Image blue1 = System.Drawing.Image.FromFile(@"..\..\Resources/img_character_01.png");
        Image blue2 = System.Drawing.Image.FromFile(@"..\..\Resources/img_character_02.png");
        Image red1 = System.Drawing.Image.FromFile(@"..\..\Resources/img_character_03.png");
        Image red2 = System.Drawing.Image.FromFile(@"..\..\Resources/img_character_04.png");
        Image blue_with = System.Drawing.Image.FromFile(@"..\..\Resources/blue_with.png");
        Image red_with = System.Drawing.Image.FromFile(@"..\..\Resources/red_with.png");

        public Form1()
        {
            InitializeComponent();
            btnBlue1.Enabled = false;
            btnBlue2.Enabled = false;
            btnRed1.Enabled = false;
            btnRed2.Enabled = false;
            IP_Address.Text = "127.0.0.1";
            PORT_Number.Text = "7777";
        }

        private void allClear()
        {
            blue = false;
            red = false;
            firstShortCutCnt1 = 0;
            firstShortCutCnt2 = 0;
            firstShortCutCnt3 = 0;
            firstShortCutCnt4 = 0;
            outlineCnt1 = 0;
            outlineCnt2 = 0;
            outlineCnt3 = 0;
            outlineCnt4 = 0;
            centerShortCutCnt1 = 0;
            centerShortCutCnt2 = 0;
            centerShortCutCnt3 = 0;
            centerShortCutCnt4 = 0;
            lastShortCutCnt1 = 0;
            lastShortCutCnt2 = 0;
            lastShortCutCnt3 = 0;
            lastShortCutCnt4 = 0;
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            firstShortCutCheck1 = 0;
            centerShortCutCheck1 = 0;
            lastShortCutCheck1 = 0;
            firstShortCutCheck3 = 0;
            centerShortCutCheck3 = 0;
            lastShortCutCheck3 = 0;
            firstShortCutCheck2 = 0;
            centerShortCutCheck2 = 0;
            lastShortCutCheck2 = 0;
            firstShortCutCheck4 = 0;
            centerShortCutCheck4 = 0;
            lastShortCutCheck4 = 0;
            btnBlue1.Visible = true;
            btnBlue2.Visible = true;
            btnRed1.Visible = true;
            btnRed2.Visible = true;

            //외곽
            PictureBox[] pb1 = new PictureBox[] { horsePos1, horsePos2, horsePos3, horsePos4, horsePos5, horsePos6, horsePos7, horsePos8, horsePos9, horsePos10, horsePos11, horsePos12, horsePos13, horsePos14, horsePos15, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //1st대각선
            PictureBox[] pb2 = new PictureBox[] { horsePos6, horsePos21, horsePos22, horsePos28, horsePos24, horsePos25, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //2nd대각선
            PictureBox[] pb3 = new PictureBox[] { horsePos11, horsePos26, horsePos27, horsePos28, horsePos29, horsePos30, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //윷놀이판 초기화
            for (int reset = 0; reset < 23; reset++)
                pb1[reset].Image = null;
            for (int reset = 0; reset < 12; reset++)
                pb2[reset].Image = null;
            for (int reset = 0; reset < 7; reset++)
                pb3[reset].Image = null; 
        }
        // 게임 끝
        private void finish()
        {
            bool check = false;
            if (label3.Text == "●" && label4.Text == "●")
            {
                MessageBox.Show("Red Team Win!!!\n새 게임을 시작합니다."); check = true;
            }
            else if (label1.Text == "●" && label2.Text == "●")
            {
                MessageBox.Show("Blue Team Win!!!\n새 게임을 시작합니다."); check = true;
            }

            if (check)
            {
                blue = false;
                red = false;
                firstShortCutCnt1 = 0;
                firstShortCutCnt2 = 0;
                firstShortCutCnt3 = 0;
                firstShortCutCnt4 = 0;
                outlineCnt1 = 0;
                outlineCnt2 = 0;
                outlineCnt3 = 0;
                outlineCnt4 = 0;
                centerShortCutCnt1 = 0;
                centerShortCutCnt2 = 0;
                centerShortCutCnt3 = 0;
                centerShortCutCnt4 = 0;
                lastShortCutCnt1 = 0;
                lastShortCutCnt2 = 0;
                lastShortCutCnt3 = 0;
                lastShortCutCnt4 = 0;
                label1.Text = "";
                label2.Text = "";
                label3.Text = "";
                label4.Text = "";
                firstShortCutCheck1 = 0;
                centerShortCutCheck1 = 0;
                lastShortCutCheck1 = 0;
                firstShortCutCheck3 = 0;
                centerShortCutCheck3 = 0;
                lastShortCutCheck3 = 0;
                firstShortCutCheck2 = 0;
                centerShortCutCheck2 = 0;
                lastShortCutCheck2 = 0;
                firstShortCutCheck4 = 0;
                centerShortCutCheck4 = 0;
                lastShortCutCheck4 = 0;
                btnBlue1.Visible = true;
                btnBlue2.Visible = true;
                btnRed1.Visible = true;
                btnRed2.Visible = true;

                //외곽
                PictureBox[] pb1 = new PictureBox[] { horsePos1, horsePos2, horsePos3, horsePos4, horsePos5, horsePos6, horsePos7, horsePos8, horsePos9, horsePos10, horsePos11, horsePos12, horsePos13, horsePos14, horsePos15, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
                //1st대각선
                PictureBox[] pb2 = new PictureBox[] { horsePos6, horsePos21, horsePos22, horsePos28, horsePos24, horsePos25, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
                //2nd대각선
                PictureBox[] pb3 = new PictureBox[] { horsePos11, horsePos26, horsePos27, horsePos28, horsePos29, horsePos30, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
                //윷놀이판 초기화
                for (int reset = 0; reset < 23; reset++)
                    pb1[reset].Image = null;
                for (int reset = 0; reset < 12; reset++)
                    pb2[reset].Image = null;
                for (int reset = 0; reset < 7; reset++)
                    pb3[reset].Image = null;
            }
        }
        // 말 잡혔을 때 clear
        public void clear(int dol)
        {
            switch (dol)
            {
                //blue 1
                case 1:
                    outlineCnt1 = 0;
                    firstShortCutCnt1 = 0;
                    centerShortCutCnt1 = 0;
                    lastShortCutCnt1 = 0;
                    firstShortCutCheck1 = 0;
                    centerShortCutCheck1 = 0;
                    lastShortCutCheck1 = 0;
                    blue = false;
                    break;
                //blue2
                case 2:
                    outlineCnt2 = 0;
                    firstShortCutCnt2 = 0;
                    centerShortCutCnt2 = 0;
                    lastShortCutCnt2 = 0;
                    firstShortCutCheck2 = 0;
                    centerShortCutCheck2 = 0;
                    lastShortCutCheck2 = 0;
                    blue = false;
                    break;
                //red1
                case 3:
                    outlineCnt3 = 0;
                    firstShortCutCnt3 = 0;
                    centerShortCutCnt3 = 0;
                    lastShortCutCnt3 = 0;
                    firstShortCutCheck3 = 0;
                    centerShortCutCheck3 = 0;
                    lastShortCutCheck3 = 0;
                    red = false;
                    break;
                //red2
                case 4:
                    outlineCnt4 = 0;
                    firstShortCutCnt4 = 0;
                    centerShortCutCnt4 = 0;
                    lastShortCutCnt4 = 0;
                    firstShortCutCheck4 = 0;
                    centerShortCutCheck4 = 0;
                    lastShortCutCheck4 = 0;
                    red = false;
                    break;
            }
        }

        private void btnThrow_Click(object sender, EventArgs e)
        {
            if(cmbYut.SelectedIndex == 1)
            {
                MessageBox.Show("빽도!!");
                num = -1;
                btnThrow.Enabled = false; 
            }
            else if(cmbYut.SelectedIndex == 2)
            {
                MessageBox.Show("도!!");
                num = 1;
                btnThrow.Enabled = false; 
            }
            else if( cmbYut.SelectedIndex == 3)
            {
                MessageBox.Show("개!!");
                num = 2;
                btnThrow.Enabled = false; 
            }
            else if (cmbYut.SelectedIndex == 4)
            {
                MessageBox.Show("걸!!");
                num = 3;
                btnThrow.Enabled = false; 
            }
            else if (cmbYut.SelectedIndex == 5)
            {
                MessageBox.Show("윷!! 한번 더!!!");
                num = 4;
                btnThrow.Enabled = true; 
            }
            else if (cmbYut.SelectedIndex == 6)
            {
                MessageBox.Show("모!! 한번 더!!!");
                btnThrow.Enabled = true;
                num = 5; 
            }
            else
            {
                Random rand = new Random();
                yutnum = rand.Next(16);

                if (yutnum == 1) // 빽도
                {
                    MessageBox.Show("빽도!!");
                    num = -1;
                    btnThrow.Enabled = false;
                }
                else if (1 < yutnum && yutnum <= 4) //도
                {
                    MessageBox.Show("도!!");
                    num = 1;
                    btnThrow.Enabled = false;
                }
                else if (4 < yutnum && yutnum <= 10) //개
                {
                    MessageBox.Show("개!!");
                    num = 2;
                    btnThrow.Enabled = false;
                }
                else if (10 < yutnum && yutnum <= 14) //걸
                {
                    MessageBox.Show("걸!!");
                    num = 3;
                    btnThrow.Enabled = false;
                }
                else if (14 < yutnum && yutnum <= 15) //윷
                {
                    MessageBox.Show("윷!! 한번 더!!!");
                    num = 4;
                    btnThrow.Enabled = true;
                }
                else if (yutnum == 16) //모
                {
                    MessageBox.Show("모!! 한번 더!!!");
                    btnThrow.Enabled = true;
                    num = 5;
                } 
            }
        bluehorse1=-1;
        bluehorse2=-1;
        redhorse1=-1;
        redhorse2=-1;

        }

        private void Blue1(int num) 
        {
            //외곽
            PictureBox[] pb1 = new PictureBox[] { horsePos1, horsePos2, horsePos3, horsePos4, horsePos5, horsePos6, horsePos7, horsePos8, horsePos9, horsePos10, horsePos11, horsePos12, horsePos13, horsePos14, horsePos15, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //1st대각선
            PictureBox[] pb2 = new PictureBox[] { horsePos6, horsePos21, horsePos22, horsePos28, horsePos24, horsePos25, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //2nd대각선
            PictureBox[] pb3 = new PictureBox[] { horsePos11, horsePos26, horsePos27, horsePos28, horsePos29, horsePos30, horsePos1, not1, not2, not3, not4, not5, not6, not7 };

            if (num == 0)
                return;

            //외각라인 
            if (outlineCnt1 < 22)
            {
                if (outlineCnt1 == 0)
                { 
                }
                else
                    pb1[outlineCnt1].Image = null;

                // 첫 지름길에서 NO를 선택한경우
                if (firstShortCutCheck1 == 0)
                {
                    //마지막 지름길에서 NO를 선택한경우
                    if (lastShortCutCheck1 == 0) {
                        if (outlineCnt1 < 22) {
                            //아웃라인 전진
                            outlineCnt1 += num;
                            if (outlineCnt1 == 0) outlineCnt1 = 20;

                            if (blue) outlineCnt2 += num;

                            //백도 처리
                            if (outlineCnt1 == -1)
                            {
                                //업기
                                if (horsePos1.Image == blue2)
                                    blue = true;
                                //잡기
                                else if (horsePos1.Image == red1)
                                {
                                    clear(3);
                                    Switchon();
                                }
                                else if (horsePos1.Image == red2)
                                {
                                    clear(4);
                                    Switchon();
                                }
                                else if (horsePos1.Image == red_with)
                                {
                                    clear(3);
                                    clear(4);
                                    Switchon();
                                }
                                if (blue)
                                    horsePos1.Image = blue_with;
                                else
                                    horsePos1.Image = blue1;
                                outlineCnt1 = 20;
                                if (blue)
                                    outlineCnt2 = 20;
                            }


                            //1P의 돌이 2P의 돌을 만낫을때
                            if (outlineCnt1 < 21)
                            {
                                if (pb1[outlineCnt1].Image == (Image)blue2)
                                {
                                    blue = true;
                                }
                                //1P의 돌이 2P 첫번째돌을 만낫을때
                                if (pb1[outlineCnt1].Image == (Image)red1) {
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                    Switchon();
                                    clear(3); 
                                }
                                // 1P의 돌이 2P의 두번째돌을 만났을때
                                if (pb1[outlineCnt1].Image == (Image)red2) {
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                    Switchon();
                                    clear(4);
                                }
                                if (pb1[outlineCnt1].Image == (Image)red_with) {
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                    Switchon();
                                    clear(3);
                                    clear(4);
                                    red = false;
                                }
                            }
                            if (blue) pb1[outlineCnt1].Image = (Image)blue_with;
                            else pb1[outlineCnt1].Image = (Image)blue1;
                        }
                    }
                }
                //첫 지름길에서 yes를 한경우
                if (firstShortCutCheck1 == 1)
                {
                    pb2[firstShortCutCnt1].Image = null;
                    if (outlineCnt1 == 5)
                    {
                        outlineCnt1 = 0;
                        if (blue)
                            outlineCnt2 = 0;
                    }
                    if (firstShortCutCnt1 < 13)
                    {
                        // 중앙 지름길로 가지않앗을때
                        if (centerShortCutCheck1 != 1)
                        {
                            firstShortCutCnt1 += num;

                            if (blue) firstShortCutCnt2 += num;

                            if (firstShortCutCnt1 < 12)
                            {
                                //대각선 백도
                                if (firstShortCutCnt1 == -1)
                                {
                                    firstShortCutCnt1 = 0;                                   
                                    outlineCnt1 = 5;
                                    pb1[outlineCnt1].Image = blue1;
                                    if (blue)
                                    {
                                        outlineCnt2 = 5;
                                        firstShortCutCnt2 = 0;
                                    }
                                }
                                if (pb2[firstShortCutCnt1].Image == (Image)blue2) blue = true;

                                if (pb2[firstShortCutCnt1].Image == (Image)red1) {
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(3);
                                }
                                if (pb2[firstShortCutCnt1].Image == (Image)red2) {
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(4);
                                }
                                if (pb2[firstShortCutCnt1].Image == (Image)red_with) {
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(3);
                                    clear(4);
                                    red = false;
                                }
                            }
                            if (blue) pb2[firstShortCutCnt1].Image = (Image)blue_with;
                            else pb2[firstShortCutCnt1].Image = (Image)blue1;
                        }
                    }
                }

                // 중앙 지름길에서 Yes를 한경우
                if (centerShortCutCheck1 == 1) { 
                    pb3[3 + centerShortCutCnt1].Image = null;
                    if (firstShortCutCnt1 == 3) {
                        firstShortCutCnt1 = 0;
                        if (blue) firstShortCutCnt2 = 0;
                    }
                    if (centerShortCutCnt1 < 5) { 
                        centerShortCutCnt1 += num;
                        if (blue)
                            centerShortCutCnt2 += num; 

                        if (centerShortCutCnt1 < 4)
                        {
                            if (pb3[3 + centerShortCutCnt1].Image == (Image)blue2) {
                                blue = true;
                            }
                            if (pb3[3 + centerShortCutCnt1].Image == (Image)red1) {
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                Switchon();
                                clear(3);
                            }
                            if (pb3[3 + centerShortCutCnt1].Image == (Image)red2) {
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                Switchon();
                                clear(4);
                            }
                            if (pb3[3 + centerShortCutCnt1].Image == (Image)red_with) {
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                Switchon();
                                clear(3);
                                clear(4);
                                red = false;
                            }
                        }
                        if (blue) pb3[3 + centerShortCutCnt1].Image = (Image)blue_with;
                        else pb3[3 + centerShortCutCnt1].Image = (Image)blue1;
                    }
                }

                // 2번째 지름길에서 Yes를 한경우
                if (lastShortCutCheck1 == 1) { 
                    pb3[lastShortCutCnt1].Image = null;
                    if (outlineCnt1 == 10) {
                        outlineCnt1 = 0;
                        if (blue)
                            outlineCnt2 = 0;
                    }
                    if (lastShortCutCnt1 < 8)
                    {
                        lastShortCutCnt1 = lastShortCutCnt1 + num;
                        if (blue)
                            lastShortCutCnt2 += num;

                        if (lastShortCutCnt1 < 7) {

                            if (lastShortCutCnt1 == -1)
                            {
                                lastShortCutCnt1 = 0;
                                outlineCnt1 = 10;
                                pb1[outlineCnt1].Image = blue1;
                                if (blue)
                                {
                                    outlineCnt2 = 10;
                                    lastShortCutCnt2 = 0;
                                }
                            }

                            if (pb3[lastShortCutCnt1].Image == (Image)blue2) {
                                blue = true;
                            }
                            if (pb3[lastShortCutCnt1].Image == (Image)red1) {
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                Switchon();
                                clear(3);
                            }
                            if (pb3[lastShortCutCnt1].Image == (Image)red2) {
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                Switchon();
                                clear(4);
                            }
                            if (pb3[lastShortCutCnt1].Image == (Image)red_with) {
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                Switchon();
                                clear(3);
                                clear(4);
                                red = false;
                            }
                        }
                        if (blue) pb3[lastShortCutCnt1].Image = (Image)blue_with;
                        else pb3[lastShortCutCnt1].Image = (Image)blue1;
                    }
                }


                //돌 도착 했을 때
                if (outlineCnt1 > 20) // 첫번째 돌이 도착했을때 
                {
                    label1.Text = "●"; //현황파 label에 색칠
                    label1.ForeColor = System.Drawing.Color.Blue;
                    btnBlue1.Visible = false; // 1P말이동 버튼을 안보이게만든다.
                    if (blue)
                    {
                        label2.Text = "●";
                        label2.ForeColor = System.Drawing.Color.Blue;
                        btnBlue2.Visible = false;
                    }
                }
                if (firstShortCutCnt1 > 11) // 지름길1로 도착햇을때
                {
                    label1.Text = "●";
                    label1.ForeColor = System.Drawing.Color.Blue;
                    btnBlue1.Visible = false;
                    if (blue)
                    {
                        label2.Text = "●";
                        label2.ForeColor = System.Drawing.Color.Blue;
                        btnBlue2.Visible = false;
                    } 
                }
                if (centerShortCutCnt1 > 3) // 중앙 지름길으로 도착햇을때
                {
                    label1.Text = "●";
                    label1.ForeColor = System.Drawing.Color.Blue;
                    btnBlue1.Visible = false;
                    if (blue)
                    {
                        label2.Text = "●";
                        label2.ForeColor = System.Drawing.Color.Blue;
                        btnBlue2.Visible = false;
                    } 
                }
                if (lastShortCutCnt1 > 6) //지름길 2로 도착햇을때
                {
                    label1.Text = "●";
                    label1.ForeColor = System.Drawing.Color.Blue;
                    btnBlue1.Visible = false;
                    if (blue)
                    {
                        label2.Text = "●";
                        label2.ForeColor = System.Drawing.Color.Blue;
                        btnBlue2.Visible = false;
                    } 
                }
            }


            bluehorse1=1; 
            finish();
        }
        private void Blue2(int num)
        {
            PictureBox[] pb0 = new PictureBox[] { horsePos20, horsePos1 };
            //외곽
            PictureBox[] pb1 = new PictureBox[] { horsePos1, horsePos2, horsePos3, horsePos4, horsePos5, horsePos6, horsePos7, horsePos8, horsePos9, horsePos10, horsePos11, horsePos12, horsePos13, horsePos14, horsePos15, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //1st대각선
            PictureBox[] pb2 = new PictureBox[] { horsePos6, horsePos21, horsePos22, horsePos28, horsePos24, horsePos25, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //2nd대각선
            PictureBox[] pb3 = new PictureBox[] { horsePos11, horsePos26, horsePos27, horsePos28, horsePos29, horsePos30, horsePos1, not1, not2, not3, not4, not5, not6, not7 };

            if (num == 0)
                return;
            if (outlineCnt2 < 22)
            {
                if (outlineCnt2 == 0)
                {
                }
                else
                    pb1[outlineCnt2].Image = null;
                if (firstShortCutCheck2 == 0)
                {
                    if (lastShortCutCheck2 == 0)
                    {
                        if (outlineCnt2 < 22)
                        {
                            outlineCnt2 += num;
                            if (outlineCnt2 == 0) outlineCnt2 = 20;
                            if (blue)
                                outlineCnt1 += num;

                            //백도 처리
                            if (outlineCnt2 == -1)
                            {
                                //업기
                                if (horsePos1.Image == blue1)
                                    blue = true;
                                //잡기
                                else if(horsePos1.Image == red1)
                                {
                                    clear(3);
                                    Switchon();
                                }
                                else if(horsePos1.Image == red2)
                                {
                                    clear(4);
                                    Switchon();
                                }
                                else if(horsePos1.Image == red_with)
                                {
                                    clear(3);
                                    clear(4);
                                    Switchon();
                                }
                                if (blue)
                                    horsePos1.Image = blue_with;
                                else
                                    horsePos1.Image = blue2;
                                outlineCnt2 = 20;
                                if (blue)
                                    outlineCnt1 = 20;
                            }

                            //1P의 돌이 2P의 돌을 만낫을때
                            if (outlineCnt2 < 21)
                            {
                                if (pb1[outlineCnt2].Image == (Image)blue1)
                                {
                                    blue = true;
                                }
                                if (pb1[outlineCnt2].Image == (Image)red1)
                                {
                                    pb1[outlineCnt2].Image = (Image)blue2;
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(3);

                                }
                                if (pb1[outlineCnt2].Image == (Image)red2) // 1P의 돌이 2P의 두번째돌을 만났을때
                                {
                                    pb1[outlineCnt2].Image = (Image)blue2;
                                    clear(4);
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                    Switchon();
                                }
                                if (pb1[outlineCnt2].Image == (Image)red_with)
                                {
                                    pb1[outlineCnt2].Image = (Image)blue2;
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(4);
                                    clear(3);
                                    red = false;

                                }
                            }
                            if (blue) pb1[outlineCnt2].Image = (Image)blue_with;
                            else pb1[outlineCnt2].Image = (Image)blue2;
                        }
                    }
                }
                //첫 지름길에서 yes를 한경우
                if (firstShortCutCheck2 == 1)
                {
                    pb2[firstShortCutCnt2].Image = null;
                    if (outlineCnt2 == 5)
                    {
                        outlineCnt2 = 0;
                        if (blue)
                            outlineCnt1 = 0;
                    }
                    if (firstShortCutCnt2 < 13)
                    {
                        // 중앙 지름길로 가지않앗을때
                        if (centerShortCutCheck2 != 1)
                        {
                            firstShortCutCnt2 += num;
                            if (blue) firstShortCutCnt1 += num;
                            
                            if (firstShortCutCnt2 < 12)
                            {
                                if (firstShortCutCnt2 == -1)
                                {
                                    firstShortCutCnt2 = 0;
                                    pb2[outlineCnt2].Image = null;
                                    outlineCnt2 = 5;
                                    pb1[outlineCnt2].Image = blue2;
                                    if (blue)
                                    {
                                        outlineCnt1 = 5;
                                        firstShortCutCnt1 = 0;
                                    }
                                }
                                if (pb2[firstShortCutCnt2].Image == (Image)blue1) blue = true;
                                
                                if (pb2[firstShortCutCnt2].Image == (Image)red1)
                                {
                                    pb2[firstShortCutCnt2].Image = (Image)blue2;
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(3);
                                }
                                if (pb2[firstShortCutCnt2].Image == (Image)red2)
                                {
                                    pb2[firstShortCutCnt2].Image = (Image)blue2;
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(4);
                                }
                                if (pb2[firstShortCutCnt2].Image == (Image)red_with)
                                {
                                    pb2[firstShortCutCnt2].Image = (Image)blue2;
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(3);
                                    clear(4);
                                    red = false;
                                }
                            }
                            if (blue) pb2[firstShortCutCnt2].Image = (Image)blue_with;
                            else pb2[firstShortCutCnt2].Image = (Image)blue2;
                        }
                    }
                }

                // 중앙 지름길에서 Yes를 한경우
                if (centerShortCutCheck2 == 1)
                {

                    pb3[3 + centerShortCutCnt2].Image = null;
                    if (firstShortCutCnt2 == 3)
                    {
                        firstShortCutCnt2 = 0;
                        if (blue)
                            firstShortCutCnt1 = 0;
                    }
                    if (centerShortCutCnt2 < 5)
                    {
                        centerShortCutCnt2 += num;
                        if (blue)
                            centerShortCutCnt1 += num;
                       
                        if (centerShortCutCnt2 < 4)
                        {
                            if (pb3[3 + centerShortCutCnt2].Image == (Image)blue1)
                            {
                                blue = true;
                            }
                            if (pb3[3 + centerShortCutCnt2].Image == (Image)red1)
                            {
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                Switchon();
                                clear(3);
                            }
                            if (pb3[3 + centerShortCutCnt2].Image == (Image)red2)
                            {
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                Switchon();
                                clear(4);
                            }
                            if (pb3[3 + centerShortCutCnt2].Image == (Image)red_with)
                            {
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                Switchon();
                                clear(3);
                                clear(4);
                                red = false;
                            }
                        }
                        if (blue) pb3[3 + centerShortCutCnt2].Image = (Image)blue_with;
                        else pb3[3 + centerShortCutCnt2].Image = (Image)blue2;


                    }
                }

                // 2번째 지름길에서 Yes를 한경우
                if (lastShortCutCheck2 == 1)
                {

                    pb3[lastShortCutCnt2].Image = null;
                    if (outlineCnt2 == 10)
                    {
                        outlineCnt2 = 0;
                        if (blue)
                            outlineCnt1 = 0;
                    }
                    if (lastShortCutCnt2 < 8)
                    {
                        lastShortCutCnt2 = lastShortCutCnt2 + num;
                        if (blue)
                            lastShortCutCnt1 += num;

                        if (lastShortCutCnt2 < 7)
                        {
                            if (lastShortCutCnt2 == -1)
                            {
                                lastShortCutCnt2 = 0;
                                outlineCnt2 = 10;
                                pb1[outlineCnt2].Image = blue2;
                                if (blue)
                                {
                                    outlineCnt1 = 10;
                                    lastShortCutCnt1 = 0;
                                }
                            }
                            if (pb3[lastShortCutCnt2].Image == (Image)blue1)
                            {
                                blue = true;
                            }
                            if (pb3[lastShortCutCnt2].Image == (Image)red1)
                            {
                                pb3[lastShortCutCnt2].Image = (Image)blue2;
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                Switchon();
                                clear(3);
                            }
                            if (pb3[lastShortCutCnt2].Image == (Image)red2)
                            {
                                pb3[lastShortCutCnt2].Image = (Image)blue2;
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                Switchon();
                                clear(4);
                            }
                            if (pb3[lastShortCutCnt2].Image == (Image)red_with)
                            {
                                pb3[lastShortCutCnt2].Image = (Image)blue2;
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                Switchon();
                                clear(3);
                                clear(4);
                                red = false;
                            }
                        }
                        if (blue)
                            pb3[lastShortCutCnt2].Image = (Image)blue_with;
                        else
                            pb3[lastShortCutCnt2].Image = (Image)blue2;
                    }
                }


                //돌 도착 했을 때
                if (outlineCnt2 > 20)
                {
                    label2.Text = "●";
                    label2.ForeColor = System.Drawing.Color.Blue;
                    btnBlue2.Visible = false;
                    if (blue)
                    {
                        label1.Text = "●";
                        label1.ForeColor = System.Drawing.Color.Blue;
                        btnBlue1.Visible = false;
                    }
                }
                if (firstShortCutCnt2 > 11)
                {
                    label2.Text = "●";
                    label2.ForeColor = System.Drawing.Color.Blue;
                    btnBlue2.Visible = false;
                    if (blue)
                    {
                        label1.Text = "●";
                        label1.ForeColor = System.Drawing.Color.Blue;
                        btnBlue1.Visible = false;
                    }

                }
                if (centerShortCutCnt2 > 3)
                {
                    label2.Text = "●";
                    label2.ForeColor = System.Drawing.Color.Blue;
                    btnBlue2.Visible = false;
                    if (blue)
                    {
                        label1.Text = "●";
                        label1.ForeColor = System.Drawing.Color.Blue;
                        btnBlue1.Visible = false;
                    }

                }
                if (lastShortCutCnt2 > 6)
                {
                    label2.Text = "●";
                    label2.ForeColor = System.Drawing.Color.Blue;
                    btnBlue2.Visible = false;
                    if (blue)
                    {
                        label1.Text = "●";
                        label1.ForeColor = System.Drawing.Color.Blue;
                        btnBlue1.Visible = false;
                    }
                }
            }
            bluehorse2=1;
            finish();
        }
        private void Red1(int num)
        {
            //외곽
            PictureBox[] pb1 = new PictureBox[] { horsePos1, horsePos2, horsePos3, horsePos4, horsePos5, horsePos6, horsePos7, horsePos8, horsePos9, horsePos10, horsePos11, horsePos12, horsePos13, horsePos14, horsePos15, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //1st대각선
            PictureBox[] pb2 = new PictureBox[] { horsePos6, horsePos21, horsePos22, horsePos28, horsePos24, horsePos25, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //2nd대각선
            PictureBox[] pb3 = new PictureBox[] { horsePos11, horsePos26, horsePos27, horsePos28, horsePos29, horsePos30, horsePos1, not1, not2, not3, not4, not5, not6, not7 };

            if (num == 0)
                return; 
            //외각라인 
            if (outlineCnt3 < 22) {
                if (outlineCnt3 == 0)
                { 
                }
                else pb1[outlineCnt3].Image = null;

                // 첫 지름길에서 NO를 선택한경우
                if (firstShortCutCheck3 == 0) {
                    //마지막 지름길에서 NO를 선택한경우
                    if (lastShortCutCheck3 == 0) {
                        if (outlineCnt3 < 22) {
                            //아웃라인 전진
                            outlineCnt3 += num;
                            if (outlineCnt3 == 0) outlineCnt3 = 20;
                            if (red) outlineCnt4 += num;

                            //백도 처리
                            if (outlineCnt3 == -1) {
                                //업기
                                if (horsePos1.Image == red2)
                                    red = true;
                                //잡기
                                else if(horsePos1.Image == blue1)
                                {
                                    clear(1);
                                    Switchon();
                                }
                                else if(horsePos1.Image == blue2)
                                {
                                    clear(2);
                                    Switchon();
                                }
                                else if(horsePos1.Image == blue_with)
                                {
                                    clear(1);
                                    clear(2);
                                    Switchon();
                                }
                                if (red)
                                    horsePos1.Image = red_with;
                                else
                                    horsePos1.Image = red1;
                                outlineCnt3 = 20;
                                if (red)
                                    outlineCnt4 = 20;
                            } 

                            //2P의 돌이 1P의 돌을 만낫을때
                            if (outlineCnt3 < 21) {
                                if (pb1[outlineCnt3].Image == (Image)red2) {
                                    red = true;
                                }
                                //2P의 돌이 1P 첫번째돌을 만낫을때
                                if (pb1[outlineCnt3].Image == (Image)blue1) {
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다");
                                    Switchon();
                                    clear(1);
                                }
                                // 2P의 돌이 1P의 두번째돌을 만났을때
                                if (pb1[outlineCnt3].Image == (Image)blue2) {
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다");
                                    Switchon();
                                    clear(2);
                                }
                                if (pb1[outlineCnt3].Image == (Image)blue_with) {
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다");
                                    Switchon();
                                    clear(1);
                                    clear(2);
                                    blue = false;
                                }
                            }
                            if (red) pb1[outlineCnt3].Image = (Image)red_with;
                            else pb1[outlineCnt3].Image = (Image)red1;
                        }
                    }
                }
                //첫 지름길에서 yes를 한경우
                if (firstShortCutCheck3 == 1) {
                    pb2[firstShortCutCnt3].Image = null;
                    if (outlineCnt3 == 5) {
                        outlineCnt3 = 0;
                        if (red) outlineCnt4 = 0;
                    }
                    if (firstShortCutCnt3 < 13) {
                        // 중앙 지름길로 가지않앗을때
                        if (centerShortCutCheck3 != 1) {

                            firstShortCutCnt3 += num;

                            if (red) firstShortCutCnt4 += num;

                            if (firstShortCutCnt3 < 12) {
                                if (firstShortCutCnt3 == -1)
                                {
                                    firstShortCutCnt3 = 0;
                                    outlineCnt3 = 5;
                                    pb1[outlineCnt3].Image = red1;
                                    if (red)
                                    {
                                        outlineCnt4 = 5;
                                        firstShortCutCnt4 = 0;
                                    }
                                }
                                if (pb2[firstShortCutCnt3].Image == (Image)red2) {
                                    red = true;
                                }
                                if (pb2[firstShortCutCnt3].Image == (Image)blue1) {
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(1);
                                }
                                if (pb2[firstShortCutCnt3].Image == (Image)blue2) {
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(2);
                                }
                                if (pb2[firstShortCutCnt3].Image == (Image)blue_with) {
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(1);
                                    clear(2);
                                    blue = false;
                                }
                            }
                            if (red) pb2[firstShortCutCnt3].Image = (Image)red_with;
                            else pb2[firstShortCutCnt3].Image = (Image)red1;
                        }
                    }
                }

                // 중앙 지름길에서 Yes를 한경우
                if (centerShortCutCheck3 == 1) { 
                    pb3[3 + centerShortCutCnt3].Image = null;
                    if (firstShortCutCnt3 == 3)
                    {
                        firstShortCutCnt3 = 0;
                        if (red) firstShortCutCnt4 = 0;
                    }
                    if (centerShortCutCnt3 < 5)
                    { 
                        centerShortCutCnt3 += num;
                        if (red) centerShortCutCnt4 += num;

                        if (centerShortCutCnt3 < 4) {
                            if (pb3[3 + centerShortCutCnt3].Image == (Image)red2) {
                                red = true;
                            }
                            if (pb3[3 + centerShortCutCnt3].Image == (Image)blue1) {
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                Switchon();
                                clear(1);
                            }
                            if (pb3[3 + centerShortCutCnt3].Image == (Image)blue2) {

                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                Switchon();
                                clear(2);
                            }
                            if (pb3[3 + centerShortCutCnt3].Image == (Image)blue_with) {
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                Switchon();
                                clear(1);
                                clear(2);
                                blue = false;
                            }
                        }
                        if (red) pb3[3 + centerShortCutCnt3].Image = (Image)red_with;
                        else pb3[3 + centerShortCutCnt3].Image = (Image)red1; 
                    }
                }

                // 2번째 지름길에서 Yes를 한경우
                if (lastShortCutCheck3 == 1) { 
                    pb3[lastShortCutCnt3].Image = null;
                    if (outlineCnt3 == 10) {
                        outlineCnt3 = 0;
                    }
                    if (lastShortCutCnt3 < 8) {
                        
                        lastShortCutCnt3 = lastShortCutCnt3 + num;
                        
                        if (red) lastShortCutCnt4 += num;

                        if (lastShortCutCnt3 < 7) {
                            if (lastShortCutCnt3 == -1)
                            {
                                lastShortCutCnt3 = 0;
                                outlineCnt3 = 10;
                                pb1[outlineCnt3].Image = red1;
                                if (red)
                                {
                                    outlineCnt4 = 10;
                                    lastShortCutCnt4 = 0;
                                }
                            }
                            if (pb3[lastShortCutCnt3].Image == (Image)red2)
                            {
                                red = true;
                            }
                            if (pb3[lastShortCutCnt3].Image == (Image)blue1) {
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                Switchon();
                                clear(1);
                            }
                            if (pb3[lastShortCutCnt3].Image == (Image)blue2) {
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                Switchon();
                                clear(2);
                            }
                            if (pb3[lastShortCutCnt3].Image == (Image)blue_with) {
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                btnThrow.Enabled = true;
                                clear(1);
                                clear(2);
                                blue = false;
                            }
                        }
                        if (red) pb3[lastShortCutCnt3].Image = (Image)red_with;
                        else pb3[lastShortCutCnt3].Image = (Image)red1;
                    }
                }


                //돌 도착 했을 때
                if (outlineCnt3 > 20) // 첫번째 돌이 도착했을때 
                {
                    label3.Text = "●"; //현황파 label에 색칠
                    label3.ForeColor = System.Drawing.Color.Red;
                    btnRed1.Visible = false; // 2P말이동 버튼을 안보이게만든다.
                    if (red)
                    {
                        label4.Text = "●";
                        label4.ForeColor = System.Drawing.Color.Red;
                        btnRed2.Visible = false;
                    }
                }
                if (firstShortCutCnt3 > 11) // 지름길1로 도착햇을때
                {
                    label3.Text = "●";
                    label3.ForeColor = System.Drawing.Color.Red;
                    btnRed1.Visible = false;
                    if (red)
                    {
                        label4.Text = "●";
                        label4.ForeColor = System.Drawing.Color.Red;
                        btnRed2.Visible = false;
                    }

                }
                if (centerShortCutCnt3 > 3) // 중앙 지름길으로 도착햇을때
                {
                    label3.Text = "●";
                    label3.ForeColor = System.Drawing.Color.Red;
                    btnRed1.Visible = false;
                    if (red)
                    {
                        label4.Text = "●";
                        label4.ForeColor = System.Drawing.Color.Red;
                        btnRed2.Visible = false;
                    }

                }
                if (lastShortCutCnt3 > 6) //지름길 2로 도착햇을때
                {
                    label3.Text = "●";
                    label3.ForeColor = System.Drawing.Color.Red;
                    btnRed1.Visible = false;
                    if (red)
                    {
                        label4.Text = "●";
                        label4.ForeColor = System.Drawing.Color.Red;
                        btnRed2.Visible = false;
                    }

                }
            }
            redhorse1=1;
            finish();
        }
        private void Red2(int num)
        {
            //외곽
            PictureBox[] pb1 = new PictureBox[] { horsePos1, horsePos2, horsePos3, horsePos4, horsePos5, horsePos6, horsePos7, horsePos8, horsePos9, horsePos10, horsePos11, horsePos12, horsePos13, horsePos14, horsePos15, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //1st대각선
            PictureBox[] pb2 = new PictureBox[] { horsePos6, horsePos21, horsePos22, horsePos28, horsePos24, horsePos25, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //2nd대각선
            PictureBox[] pb3 = new PictureBox[] { horsePos11, horsePos26, horsePos27, horsePos28, horsePos29, horsePos30, horsePos1, not1, not2, not3, not4, not5, not6, not7 };


            if (num == 0)
                return;
            //외각라인 
            if (outlineCnt4 < 22) {
                if (outlineCnt4 == 0)
                { 
                }
                else pb1[outlineCnt4].Image = null;

                // 첫 지름길에서 NO를 선택한경우
                if (firstShortCutCheck4 == 0) {
                    //마지막 지름길에서 NO를 선택한경우
                    if (lastShortCutCheck4 == 0) {
                        if (outlineCnt4 < 22) {
                            //아웃라인 전진
                            outlineCnt4 += num;
                            if (outlineCnt4 == 0) outlineCnt4 = 20;
                            if (red) outlineCnt3 += num;

                            //백도 처리
                            if (outlineCnt4 == -1) {
                                //업기
                                if (horsePos1.Image == red1)
                                    red = true;
                                //잡기
                                else if(horsePos1.Image == blue1)
                                {
                                    clear(1);
                                    Switchon();
                                }
                                else if(horsePos1.Image == blue2)
                                {
                                    clear(2);
                                    Switchon();
                                }
                                else if(horsePos1.Image == blue_with)
                                {
                                    clear(1);
                                    clear(2);
                                    Switchon();
                                }
                                if (red)
                                    horsePos1.Image = red_with;
                                else
                                    horsePos1.Image = red2;
                                outlineCnt4 = 20;
                                if (red)
                                    outlineCnt3 = 20;
                            }


                            //2P의 돌이 1P의 돌을 만낫을때
                            if (outlineCnt4 < 21) {
                                if (pb1[outlineCnt4].Image == (Image)red1) {
                                    red = true;
                                }
                                //2P의 돌이 1P 첫번째돌을 만낫을때
                                if (pb1[outlineCnt4].Image == (Image)blue1) {
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다");
                                    Switchon();
                                    clear(1);
                                }
                                // 2P의 돌이 1P의 두번째돌을 만났을때
                                if (pb1[outlineCnt4].Image == (Image)blue2) {
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다");
                                    Switchon();
                                    clear(2);
                                }
                                if (pb1[outlineCnt4].Image == (Image)blue_with) {
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다");
                                    Switchon();
                                    clear(1);
                                    clear(2);
                                    blue = false;
                                }
                            }
                            if (red) pb1[outlineCnt4].Image = (Image)red_with;
                            else pb1[outlineCnt4].Image = (Image)red2;
                        }
                    }
                }
                //첫 지름길에서 yes를 한경우
                if (firstShortCutCheck4 == 1) {
                    pb2[firstShortCutCnt4].Image = null;
                    if (outlineCnt4 == 5) {
                        outlineCnt4 = 0;
                        if (red) outlineCnt3 = 0;
                    }
                    if (firstShortCutCnt4 < 13)
                    {
                        // 중앙 지름길로 가지않앗을때
                        if (centerShortCutCheck4 != 1)
                        {
                            firstShortCutCnt4 += num;
                            if (red) firstShortCutCnt3 += num;

                            if (firstShortCutCnt4 < 12) {
                                if (firstShortCutCnt4 == -1)
                                {
                                    firstShortCutCnt4 = 0;
                                    outlineCnt4 = 5;
                                    pb1[outlineCnt4].Image = red2;
                                    if (red)
                                    {
                                        outlineCnt3 = 5;
                                        firstShortCutCnt3 = 0;
                                    }
                                }
                                if (pb2[outlineCnt4].Image == (Image)red1) {
                                    red = true;
                                }
                                if (pb2[firstShortCutCnt4].Image == (Image)blue1) {
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(1);
                                }
                                if (pb2[firstShortCutCnt4].Image == (Image)blue2) {
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(2);
                                }
                                if (pb2[firstShortCutCnt4].Image == (Image)blue_with) {
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                    Switchon();
                                    clear(1);
                                    clear(2);
                                    blue = false;
                                }
                            }
                            if (red) pb2[firstShortCutCnt4].Image = (Image)red_with;
                            else pb2[firstShortCutCnt4].Image = (Image)red2;
                        }
                    }
                }

                // 중앙 지름길에서 Yes를 한경우
                if (centerShortCutCheck4 == 1) { 
                    pb3[3 + centerShortCutCnt4].Image = null;
                    if (firstShortCutCnt4 == 3) {
                        firstShortCutCnt4 = 0;
                        
                        if (red) firstShortCutCnt3 = 0;
                    }
                    if (centerShortCutCnt4 < 5) { 
                        centerShortCutCnt4 += num;

                        if (red) centerShortCutCnt3 += num;
                        
                        if (centerShortCutCnt4 < 4)
                        {
                            if (pb3[3 + centerShortCutCnt4].Image == (Image)red1) {
                                red = true;
                            }
                            if (pb3[3 + centerShortCutCnt4].Image == (Image)blue1) {
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                Switchon();
                                clear(1);
                            }
                            if (pb3[3 + centerShortCutCnt4].Image == (Image)blue2) {
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                Switchon();
                                clear(2);
                            }
                            if (pb3[3 + centerShortCutCnt4].Image == (Image)blue_with) {
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                Switchon();
                                clear(1);
                                clear(2);
                                blue = false;
                            }
                        }
                        if (red) pb3[3 + centerShortCutCnt4].Image = (Image)red_with;
                        else pb3[3 + centerShortCutCnt4].Image = (Image)red2;


                    }
                }

                // 2번째 지름길에서 Yes를 한경우
                if (lastShortCutCheck4 == 1) { 
                    pb3[lastShortCutCnt4].Image = null;
                    if (outlineCnt4 == 10) {
                        outlineCnt4 = 0;
                        
                        if (red) outlineCnt3 = 0;
                    }
                    if (lastShortCutCnt4 < 8) {
                        lastShortCutCnt4 = lastShortCutCnt4 + num;

                        if (red) lastShortCutCnt3 += num;

                        if (lastShortCutCnt4 < 7) {
                            if (lastShortCutCnt4 == -1)
                            {
                                lastShortCutCnt4 = 0;
                                outlineCnt4 = 10;
                                pb1[outlineCnt4].Image = red2;
                                if (red)
                                {
                                    outlineCnt3 = 10;
                                    lastShortCutCnt3 = 0;
                                }
                            }
                            if (pb3[lastShortCutCnt4].Image == (Image)red1)
                            {
                                red = true;
                            }
                            if (pb3[lastShortCutCnt4].Image == (Image)blue1) {
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                Switchon();
                                clear(1);
                            }
                            if (pb3[lastShortCutCnt4].Image == (Image)blue2) {
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                Switchon();
                                clear(2);
                            }
                            if (pb3[lastShortCutCnt4].Image == (Image)blue_with) {
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                Switchon();
                                clear(1);
                                clear(2);
                                blue = false;
                            }
                        }
                        if (red) pb3[lastShortCutCnt4].Image = (Image)red_with;
                        else pb3[lastShortCutCnt4].Image = (Image)red2;
                    }
                }


                //돌 도착 했을 때
                if (outlineCnt4 > 20) // 첫번째 돌이 도착했을때 
                {
                    label4.Text = "●"; //현황파 label에 색칠
                    label4.ForeColor = System.Drawing.Color.Red;
                    btnRed2.Visible = false; // 2P말이동 버튼을 안보이게만든다.
                    if (red)
                    {
                        label3.Text = "●";
                        label3.ForeColor = System.Drawing.Color.Red;
                        btnRed1.Visible = false;
                    }
                }
                if (firstShortCutCnt4 > 11) // 지름길1로 도착햇을때
                {
                    label4.Text = "●";
                    label4.ForeColor = System.Drawing.Color.Red;
                    btnRed2.Visible = false;
                    if (red)
                    {
                        label3.Text = "●";
                        label3.ForeColor = System.Drawing.Color.Red;
                        btnRed1.Visible = false;
                    }
                }
                if (centerShortCutCnt4 > 3) // 중앙 지름길으로 도착햇을때
                {
                    label4.Text = "●";
                    label4.ForeColor = System.Drawing.Color.Red;
                    btnRed2.Visible = false;
                    if (red)
                    {
                        label3.Text = "●";
                        label3.ForeColor = System.Drawing.Color.Red;
                        btnRed1.Visible = false;
                    }
                }
                if (lastShortCutCnt4 > 6) //지름길 2로 도착햇을때
                {
                    label4.Text = "●";
                    label4.ForeColor = System.Drawing.Color.Red;
                    btnRed2.Visible = false;
                    if (red)
                    {
                        label3.Text = "●";
                        label3.ForeColor = System.Drawing.Color.Red;
                        btnRed1.Visible = false;
                    }
                }
            }
            redhorse2=1;
            finish();
        }
        private void btnGo_Click(object sender,EventArgs e)
        {
            var btn = sender as Button;
            if( btn != null)
            {
                if (btn == btnBlue1)
                {
                    // 지름길 선택 여부
                    // 1번째지름길에 도착했을경우
                    if (num != -1)
                    {
                        if (outlineCnt1 == 5)
                        {
                            if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes) //지름길로 갈것인지 메세지를 띄움
                            {
                                firstShortCutCheck1 = 1;
                                if (blue) firstShortCutCheck2 = 1;
                            }
                            else
                            {
                                firstShortCutCheck1 = 0;
                                if (blue) firstShortCutCheck2 = 0;
                            }
                        }
                        // 중앙 지름길에 도착했을경우
                        if (firstShortCutCnt1 == 3)
                        {
                            if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                centerShortCutCheck1 = 1;
                                firstShortCutCheck1 = -1;
                                if (blue)
                                {
                                    centerShortCutCheck2 = 1;
                                    firstShortCutCheck2 = -1;
                                }
                            }
                            else
                            {
                                centerShortCutCheck1 = 0;
                                if (blue) centerShortCutCheck2 = 0;
                            }
                        }
                        // 2번째 지름길에 도착했을경우
                        if (outlineCnt1 == 10)
                        {
                            if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                lastShortCutCheck1 = 1;
                                if (blue) lastShortCutCheck2 = 1;
                            }
                            else
                            {
                                lastShortCutCheck1 = 0;
                                if (blue) lastShortCutCheck2 = 0;
                            }
                        }
                    }
                    Blue1(num);
                }
                if (btn == btnBlue2)
                {
                    //지름길 선택 여부
                    if (num != -1)
                    {
                        if (outlineCnt2 == 5)
                        {
                            if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes) //지름길로 갈것인지 메세지를 띄움
                            {
                                firstShortCutCheck2 = 1;
                                if (blue)
                                    firstShortCutCheck1 = 1;
                            }
                            else
                            {
                                firstShortCutCheck2 = 0;
                                if (blue)
                                    firstShortCutCheck1 = 0;
                            }
                        }
                        if (firstShortCutCnt2 == 3)
                        {
                            if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                centerShortCutCheck2 = 1;
                                firstShortCutCheck2 = -1;
                                if (blue)
                                {
                                    centerShortCutCheck1 = 1;
                                    firstShortCutCheck1 = -1;
                                }
                            }
                            else
                            {
                                centerShortCutCheck2 = 0;
                                if (blue)
                                    centerShortCutCheck1 = 0;
                            }
                        }
                        if (outlineCnt2 == 10)
                        {
                            if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                lastShortCutCheck2 = 1;
                                if (blue)
                                    lastShortCutCheck1 = 1;
                            }
                            else
                            {
                                lastShortCutCheck2 = 0;
                                if (blue)
                                    lastShortCutCheck1 = 0;
                            }
                        }
                    }
                    Blue2(num);
                }
                if (btn == btnRed1)
                {
                    // 지름길 선택 여부
                    // 1번째지름길에 도착했을경우
                    if (num != -1)
                    {
                        if (outlineCnt3 == 5)
                        {
                            if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes) //지름길로 갈것인지 메세지를 띄움
                            {
                                firstShortCutCheck3 = 1;
                                if (red) firstShortCutCheck4 = 1;
                            }
                            else
                            {
                                firstShortCutCheck3 = 0;
                                if (red) firstShortCutCheck4 = 0;
                            }
                        }
                        // 중앙 지름길에 도착했을경우
                        if (firstShortCutCnt3 == 3)
                        {
                            if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                centerShortCutCheck3 = 1;
                                firstShortCutCheck3 = -1;
                                if (red)
                                {
                                    centerShortCutCheck4 = 1;
                                    firstShortCutCheck4 = -1;
                                }
                            }
                            else
                            {
                                centerShortCutCheck3 = 0;
                                if (red) centerShortCutCheck4 = 0;
                            }
                        }
                        // 2번째 지름길에 도착했을경우
                        if (outlineCnt3 == 10)
                        {
                            if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                lastShortCutCheck3 = 1;
                                if (red) lastShortCutCheck4 = 1;
                            }
                            else
                            {
                                lastShortCutCheck3 = 0;
                                if (red) lastShortCutCheck4 = 0;
                            }
                        }
                    }
                    Red1(num);
                }
                if (btn == btnRed2)
                {
                    // 지름길 선택 여부
                    // 1번째지름길에 도착했을경우
                    if (num != -1)
                    {
                        if (outlineCnt4 == 5)
                        {
                            if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes) //지름길로 갈것인지 메세지를 띄움
                            {
                                firstShortCutCheck4 = 1;
                                if (red) firstShortCutCheck3 = 1;
                            }
                            else
                            {
                                firstShortCutCheck4 = 0;
                                if (red) firstShortCutCheck3 = 0;
                            }
                        }
                        // 중앙 지름길에 도착했을경우
                        if (firstShortCutCnt4 == 3)
                        {
                            if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                centerShortCutCheck4 = 1;
                                firstShortCutCheck4 = -1;
                                if (red)
                                {
                                    centerShortCutCheck3 = 1;
                                    firstShortCutCheck3 = -1;
                                }
                            }
                            else
                            {
                                centerShortCutCheck4 = 0;
                                if (red) centerShortCutCheck3 = 0;
                            }
                        }
                        // 2번째 지름길에 도착했을경우
                        if (outlineCnt4 == 10)
                        {
                            if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                lastShortCutCheck4 = 1;
                                if (red) lastShortCutCheck3 = 1;
                            }
                            else
                            {
                                lastShortCutCheck4 = 0;
                                if (red) lastShortCutCheck3 = 0;
                            }
                        }
                    }
                    Red2(num); 
                }
                if(num != 0)
                    Sendyut();
                num = 0;
            }
        }

        public void Message(string msg)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    txtChat.AppendText(msg + "\r\n");
                    txtChat.Focus();
                    txtChat.ScrollToCaret();
                    txtSend.Focus();
                }));

            }
        public void ServerStart()
        {
            try
            {
                int PORT = Convert.ToInt32(PORT_Number.Text);
                m_listener = new TcpListener(PORT);
                m_listener.Start();

                m_bStop = true;
                while (m_bStop)
                {
                    TcpClient hClient = m_listener.AcceptTcpClient();
                    if (hClient.Connected)
                    {
                        Message("상대방이 들어왔습니다.");
                        m_bConnect = true;
                        m_Stream = hClient.GetStream();

                        m_Read = new StreamReader(m_Stream);

                        m_Write = new StreamWriter(m_Stream);
                        m_ThReader = new Thread(new ThreadStart(Receive));
                        m_ThReader.Start();
                    }
                }
            }
            catch
            {
                return;
            }
        }
        public void ServerStart2()
    {
        try
        {
            int PORT2 = Convert.ToInt32(PORT_Number.Text)+1;
            m_listener2 = new TcpListener(PORT2);
            m_listener2.Start();

            m_bStop = true;
            while (m_bStop)
            {
                TcpClient hClient2 = m_listener2.AcceptTcpClient();
                if (hClient2.Connected)
                {
                    m_bConnect = true;
                    m_Stream2 = hClient2.GetStream();

                    m_Read2 = new StreamReader(m_Stream2);

                    m_Write2 = new StreamWriter(m_Stream2);
                    m_ThReader2 = new Thread(new ThreadStart(Receiveyut));
                    m_ThReader2.Start();
                }
            }
        }
        catch
        {
            return;
        }
    }
        public void ServerStop()
        {
            if (!m_bStop)
                return;
            m_listener.Stop();
            m_listener2.Stop();

            if (m_Read != null)
                m_Read.Close();
            if(m_Write != null)
                m_Write.Close();
            if(m_Read2 !=null)
                m_Read2.Close();
            if( m_Write2 != null)
                m_Write2.Close();

            if(m_Stream !=null)
                m_Stream.Close();
            if(m_Stream2 != null)
                m_Stream2.Close();
            if(m_ThReader != null)
                m_ThReader.Abort();
            if(m_ThReader2 != null)
                m_ThReader2.Abort();
            if(m_thServer != null)
                m_thServer.Abort();
            if(m_thServer2 != null)
                m_thServer2.Abort();
            Message("서비스 종료");
        }
        public void Disconnect()
        {
            if (!m_bConnect)
                return;
            m_bConnect = false;
            if(m_Read !=null)
                m_Read.Close();
            if(m_Write !=null)
                m_Write.Close();
            if(m_Read2!=null)
                m_Read2.Close();
            if(m_Write2!=null)
                m_Write2.Close();

            if(m_Stream !=null)
                m_Stream.Close();
            if(m_ThReader !=null)
                m_ThReader.Abort();
            if(m_Stream2 !=null)
                m_Stream2.Close();
            if(m_ThReader2 !=null)
                m_ThReader2.Abort();
            Message("상대방과 연결 중단");
        }
        public void Connect()
        {
            m_Client = new TcpClient();
            m_Client2 = new TcpClient();

            try
            {
                int PORT = Convert.ToInt32(PORT_Number.Text);
                int PORT2 = Convert.ToInt32(PORT_Number.Text)+1;

                m_Client.Connect(IP_Address.Text, PORT);
                m_Client2.Connect(IP_Address.Text, PORT2);

            }
            catch
            {
                m_bConnect = false;
                return;
            }
            m_bConnect = true;
            Message("서버에 연결");

            m_Stream = m_Client.GetStream();
            m_Stream2 = m_Client2.GetStream();
            m_Read = new StreamReader(m_Stream);
            m_Read2 = new StreamReader(m_Stream2);    
        
            m_Write = new StreamWriter(m_Stream);
            m_Write2 = new StreamWriter(m_Stream2);

            m_ThReader = new Thread(new ThreadStart(Receive));
            m_ThReader2 = new Thread(new ThreadStart(Receiveyut));
            m_ThReader.Start();
            m_ThReader2.Start();
        }
        public void Receive()
        {
            try
            {
                while (m_bConnect)
                {
                    
                  string szMessage=m_Read.ReadLine();


                    if (szMessage != null)
                        Message("상대방 >>> :" + szMessage);

                }}
            catch
            {
                Message("데이터를 읽는 과정에서 오류가 발생");

            }
            Disconnect();
        }
        void Send()
        {
            try
            {
                if(m_Write != null)
                {
                    m_Write.WriteLine(txtSend.Text);
                    m_Write.Flush();

                    Message(">>>: " + txtSend.Text);
                    txtSend.Text = ""; 
                }
            }
            catch
            {
                Message("데이터 전송 실패");
            }
        }

        private void btn_Server_Click(object sender, EventArgs e)
        {
            if (btn_Server.Text == "생성")
            {
                btnBlue1.Enabled = true;
                btnBlue2.Enabled = true;

                Message("참가자 접속 대기중...");
                m_thServer = new Thread(new ThreadStart(ServerStart));
                this.Invoke(new MethodInvoker(delegate () { 
                m_thServer2 = new Thread(new ThreadStart(ServerStart2));
                m_thServer2.Start(); }));

                m_thServer.Start();
                
                btn_Server.Text = "멈춤";
            }
            else
            {
                btnBlue1.Enabled = false;
                btnBlue2.Enabled = false; 
                ServerStop();
                btn_Server.Text = "생성";
            }
            allClear();
        }
        void Sendyut()
        {
            try { 
                int i;
                int ShortCheck;
                int TurnCheck;
                if (btnThrow.Enabled == true)
                    TurnCheck = 1;
                else
                    TurnCheck = 0;
                if (bluehorse1 > 0)
                {
                    i = 1;
                    if (firstShortCutCheck1 > 0)
                        ShortCheck = 1;
                    else if (centerShortCutCheck1 > 0)
                        ShortCheck = 2;
                    else if (lastShortCutCheck1 > 0)
                        ShortCheck = 3;
                    else
                        ShortCheck = 0;
                }
                else if (bluehorse2 > 0)
                {
                    i = 2;
                    if (firstShortCutCheck2 > 0)
                        ShortCheck = 1;
                    else if (centerShortCutCheck2 > 0)
                        ShortCheck = 2;
                    else if (lastShortCutCheck2 > 0)
                        ShortCheck = 3;
                    else
                        ShortCheck = 0;
                }
                else if (redhorse1 > 0)
                {
                    i = 3;
                    if (firstShortCutCheck3 > 0)
                        ShortCheck = 1;
                    else if (centerShortCutCheck3 > 0)
                        ShortCheck = 2;
                    else if (lastShortCutCheck3 > 0)
                        ShortCheck = 3;
                    else
                        ShortCheck = 0;
                }
                else
                {
                    i = 4;
                    if (firstShortCutCheck4 > 0)
                        ShortCheck = 1;
                    else if (centerShortCutCheck4 > 0)
                        ShortCheck = 2;
                    else if (lastShortCutCheck4 > 0)
                        ShortCheck = 3;
                    else
                        ShortCheck = 0;
                }
                if (m_Write2 != null) {
                    m_Write2.WriteLine(num);
                    m_Write2.WriteLine(i);
                    m_Write2.WriteLine(ShortCheck);
                    m_Write2.WriteLine(TurnCheck);
                    m_Write2.Flush();
                } 
            }
            catch
            {
                Message("윷 전송 실패");
            }

        }

        void Switchon()
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                btnThrow.Enabled = true;
                return;
            }));
        }

        void Switchoff()
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                btnThrow.Enabled = false;
                return;
            }));
        }

        void Receiveyut()
        {
            try
            {
                while (m_bConnect)
                {
                    // 윷정보 : -1, 1,2,3,4,5
                    string a = m_Read2.ReadLine();
                    // 말정보 : 1,2,3,4,
                    string b = m_Read2.ReadLine();
                    string c = m_Read2.ReadLine();
                    //턴 정보 : 0,1
                    string t = m_Read2.ReadLine();
                    int aa = Int32.Parse(a);
                    int bb = Int32.Parse(b);
                    int cc = Int32.Parse(c);
                    int tt = Int32.Parse(t);

                    string yutInfo=null;
                    switch (aa)
                    {
                        case -1:
                            yutInfo = "빽도";
                            break;
                        case 1:
                            yutInfo = "도";
                            break;
                        case 2:
                            yutInfo = "개";
                            break;
                        case 3:
                            yutInfo = "걸";
                            break;
                        case 4:
                            yutInfo = "윷";
                            break;
                        case 5:
                            yutInfo = "모";
                            break;
                    }

                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        switch (bb)
                        {
                            case 1:
                                if (cc == 1)
                                    firstShortCutCheck1 = 1;
                                else if (cc == 2)
                                    centerShortCutCheck1 = 1;
                                else if (cc == 3)
                                    lastShortCutCheck1 = 1;
                                Message("상대방이 " + yutInfo + "이(가) 나왔습니다!");
                                Blue1(aa);
                                if (tt == 0)
                                    Switchon();
                                else
                                    Switchoff();
                                break;
                            case 2:
                                if (cc == 1)
                                    firstShortCutCheck2 = 1;
                                else if (cc == 2)
                                    centerShortCutCheck2 = 1;
                                else if (cc == 3)
                                    lastShortCutCheck2 = 1;
                                Message("상대방이 " + yutInfo + "이(가) 나왔습니다!");
                                Blue2(aa);
                                if (tt == 0)
                                    Switchon();
                                else
                                    Switchoff();
                                break;
                            case 3:
                                if (cc == 1)
                                    firstShortCutCheck3 = 1;
                                else if (cc == 2)
                                    centerShortCutCheck3 = 1;
                                else if (cc == 3)
                                    lastShortCutCheck3 = 1;
                                Message("상대방이 " + yutInfo + "이(가) 나왔습니다!");
                                Red1(aa);
                                if (tt == 0)
                                    Switchon();
                                else
                                    Switchoff();
                                break;
                            case 4:
                                if (cc == 1)
                                    firstShortCutCheck4 = 1;
                                else if (cc == 2)
                                    centerShortCutCheck4 = 1;
                                else if (cc == 3)
                                    lastShortCutCheck4 = 1;
                                Message("상대방이 " + yutInfo + "이(가) 나왔습니다!");
                                Red2(aa);
                                if (tt == 0)
                                    Switchon();
                                else
                                    Switchoff();
                                break;
                        }
                        return;
                    }));
                }
            }
            catch
            {
                Message("데이터를 읽는 과정에서 오류가 발생1");

            }
            

        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (btn_Connect.Text == "연결")
            {
                Connect();
                if (m_bConnect)
                {
                    btnRed1.Enabled = true;
                    btnRed2.Enabled = true;
                    btnThrow.Enabled = false;

                    btn_Connect.Text = "끊기";
                }
            }
            else
            {
                btnRed1.Enabled = false;
                btnRed2.Enabled = false;
                btnThrow.Enabled = true;
                Disconnect();
                btn_Connect.Text = "연결";
            }
            allClear();
        }

        private void Send_Message_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void txtSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Send();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ServerStop();
            Disconnect();
        }

        private void txtChat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

    }
}



