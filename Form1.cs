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



//업기랑 백도 먹힐때 다시해야함

namespace yutgame
{
    public partial class Form1 : Form
    {
        Socket client;
        IPEndPoint ipep;

        

        int num;
        int yutnum;
        
        // count
        int count1 = 0;
        int count2 = 0;
        int count3 = 0;
        int count4 = 0;
        
        int ccnt;
        int ccnt1;
        int ccnt2;
        int ccnt3;
        
        int outlineCnt1 = 0;
        int outlineCnt2 = 0;
        int outlineCnt3 = 0;
        int outlineCnt4 = 0;
        
        //cnt11
        int firstShortCutCnt1 = 0;
        int firstShortCutCnt2 = 0;
        int firstShortCutCnt3 = 0;
        int firstShortCutCnt4 = 0;
        
        //cnt_1
        int centerShortCutCnt1 = 0;
        int centerShortCutCnt2 = 0;
        int centerShortCutCnt3 = 0;
        int centerShortCutCnt4 = 0;
        
        //cnt__1
        int lastShortCutCnt1 = 0;
        int lastShortCutCnt2 = 0;
        int lastShortCutCnt3 = 0;
        int lastShortCutCnt4 = 0;
        
        //b1
        int firstShortCutCheck1;
        int firstShortCutCheck2;
        int firstShortCutCheck3;
        int firstShortCutCheck4;
        
        //b__1
        int centerShortCutCheck1;
        int centerShortCutCheck2;
        int centerShortCutCheck3;
        int centerShortCutCheck4;
        
        //b___1
        int lastShortCutCheck1;
        int lastShortCutCheck2;
        int lastShortCutCheck3;
        int lastShortCutCheck4;


        public Form1()
        {
            InitializeComponent();
        }


        // 게임 끝
        public void finish() {
            bool check = false;
            if (label3.Text == "●" && label4.Text == "●") {
                MessageBox.Show("Red Team Win!!!\n새 게임을 시작합니다."); check = true;
            }
            else if (label1.Text == "●" && label2.Text == "●") {
                MessageBox.Show("Blue Team Win!!!\n새 게임을 시작합니다."); check = true;
            }

            if (check) {
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

        private void btnThrow_Click(object sender, EventArgs e) {
            Random rand = new Random();
            yutnum = rand.Next(16);
            if(yutnum == 1) // 빽도
            {
                MessageBox.Show("빽도!!");
                num = -1;
            }
            else if (1 < yutnum && yutnum <= 4) //도
            {
                MessageBox.Show("도!!");
                num = 1;
            }
            else if (4 < yutnum && yutnum <= 10) //개
            {
                MessageBox.Show("개!!");
                num = 2;
            }
            else if (10 < yutnum && yutnum <= 14) //걸
            {
                MessageBox.Show("걸!!");
                num = 3;
            }
            else if (14 < yutnum && yutnum <= 15) //윷
            {
                MessageBox.Show("윷!! 한번 더!!!");
                num = 4;
            }
            else if (yutnum == 16) //모
            {
                MessageBox.Show("모!! 한번 더!!!");
                num = 5;
            }
        }

        private void btnBlue1_Click(object sender, EventArgs e)
        {
            //PictureBox[] pb0 = new PictureBox[] { horsePos20, horsePos1 };
            
            //외곽
            PictureBox[] pb1 = new PictureBox[] { horsePos1, horsePos2, horsePos3, horsePos4, horsePos5, horsePos6, horsePos7, horsePos8, horsePos9, horsePos10, horsePos11, horsePos12, horsePos13, horsePos14, horsePos15, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6,not7 };
            //1st대각선
            PictureBox[] pb2 = new PictureBox[] { horsePos6, horsePos21, horsePos22, horsePos28, horsePos24, horsePos25, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //2nd대각선
            PictureBox[] pb3 = new PictureBox[] { horsePos11, horsePos26, horsePos27, horsePos28, horsePos29, horsePos30, horsePos1, not1, not2, not3, not4, not5, not6, not7 };


            //외각라인 
            if (outlineCnt1 < 22) 
            {
                pb1[outlineCnt1].Image = null;
                
                // 첫 지름길에서 NO를 선택한경우
                if (firstShortCutCheck1 == 0) 
                {
                    //마지막 지름길에서 NO를 선택한경우
                    if (lastShortCutCheck1 == 0) 
                    {
                        if (outlineCnt1 < 22)
                        {
                            //아웃라인 전진
                            outlineCnt1 += num; 
                            
                            //백도 처리
                            /*if(outlineCnt1 == -1)
                            {
                               
                            }*/

                            // ccnt가 1일때 즉 1P의말이 겹쳣을때를 말하는 코드이다.
                            if (ccnt == 1)  
                            {
                                pb1[count1].Image = imgCharacter.Images[4];
                                ccnt = 0;
                            }

                            // 1P의 두개의돌이 만났을때
                            if (pb1[outlineCnt1].Image == imgCharacter.Images[1])
                            {
                                count1 = outlineCnt1; //count1에 outlineCnt1값을 넣어준다
                                ccnt = 1; // ccnt는 1로 변경해준다.
                                pb1[count1].Image = imgCharacter.Images[4];
                            }

                            //1P의 돌이 2P의 돌을 만낫을때
                            if (outlineCnt1 < 21)
                            {
                                //1P의 돌이 2P 첫번째돌을 만낫을때
                                if (pb1[outlineCnt1].Image == imgCharacter.Images[2]) 
                                {
                                    pb1[outlineCnt1].Image = imgCharacter.Images[0];
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다"); 

                                }
                                // 1P의 돌이 2P의 두번째돌을 만났을때
                                if (pb1[outlineCnt1].Image == imgCharacter.Images[3]) 
                                {
                                    pb1[outlineCnt1].Image = imgCharacter.Images[0];
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                }
                            }
                            pb1[outlineCnt1].Image = imgCharacter.Images[0];
                        }
                    }
                }
                //첫 지름길에서 yes를 한경우
                if (firstShortCutCheck1 == 1)
                {
                    pb2[firstShortCutCnt1].Image = null;
                    if (outlineCnt1 == 5) 
                    {
                        outlineCnt1++;
                    }
                    if (firstShortCutCnt1 < 13)
                    {
                        // 중앙 지름길로 가지않앗을때
                        if (centerShortCutCheck1 != 1) 
                        {
                            firstShortCutCnt1 += num;
                            if (ccnt1 == 1)
                            {
                                pb2[count1].Image = imgCharacter.Images[4];
                                ccnt1 = 0;
                            }                          
                            if (pb2[outlineCnt1].Image == imgCharacter.Images[0])
                            {
                                count1 = firstShortCutCnt1;
                                ccnt1 = 1;
                            }
                            if (firstShortCutCnt1 < 12)
                            {
                                if (pb2[firstShortCutCnt1].Image == imgCharacter.Images[2])
                                {
                                    pb2[firstShortCutCnt1].Image = imgCharacter.Images[0];                                  
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                    outlineCnt3 = 0;
                                    firstShortCutCnt3 = 0;
                                    centerShortCutCnt3 = 0;
                                    lastShortCutCnt3 = 0;
                                    firstShortCutCheck3 = 0;
                                    centerShortCutCheck3 = 0;
                                    lastShortCutCheck3 = 0;
                                }
                                if (pb2[firstShortCutCnt1].Image == imgCharacter.Images[3])
                                {
                                    pb2[firstShortCutCnt1].Image = imgCharacter.Images[0];                                   
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                    outlineCnt4 = 0;
                                    firstShortCutCnt4 = 0;
                                    centerShortCutCnt4 = 0;
                                    lastShortCutCnt4 = 0;
                                    firstShortCutCheck4 = 0;
                                    centerShortCutCheck4 = 0;
                                    lastShortCutCheck4 = 0;
                                }
                            }
                            pb2[firstShortCutCnt1].Image = imgCharacter.Images[0];
                        }
                    }
                }

                // 중앙 지름길에서 Yes를 한경우
                if (centerShortCutCheck1 == 1) 
                {

                    pb3[3 + centerShortCutCnt1].Image = null;
                    if (firstShortCutCnt1 == 3) 
                    {
                        firstShortCutCnt1++;
                    }
                    if (centerShortCutCnt1 < 5)
                    {

                        centerShortCutCnt1 += num;
                        if (ccnt1 == 1)
                        {
                            pb3[count1].Image = imgCharacter.Images[4];
                            ccnt1 = 0;
                        }
                        if (pb3[count1].Image == imgCharacter.Images[0]) 
                        {                                                            
                            count1 = 3 + centerShortCutCnt1;
                            ccnt1 = 1;
                        }
                  
                        if (centerShortCutCnt1 < 4)
                        {
                            if (pb3[3 + centerShortCutCnt1].Image == imgCharacter.Images[2])
                            {
                                pb3[count1].Image = imgCharacter.Images[0];
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                outlineCnt3 = 0;
                                firstShortCutCnt3 = 0;
                                centerShortCutCnt3 = 0;
                                lastShortCutCnt3 = 0;
                                firstShortCutCheck3 = 0;
                                centerShortCutCheck3 = 0;
                                lastShortCutCheck3 = 0;
                            }
                            if (pb3[3 + centerShortCutCnt1].Image == imgCharacter.Images[3])
                            {
                                pb3[count1].Image = imgCharacter.Images[1];
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                outlineCnt4 = 0;
                                firstShortCutCnt4 = 0;
                                centerShortCutCnt4 = 0;
                                lastShortCutCnt4 = 0;
                                firstShortCutCheck4 = 0;
                                centerShortCutCheck4 = 0;
                                lastShortCutCheck4 = 0;
                            }
                        }
                        pb3[3 + centerShortCutCnt1].Image = imgCharacter.Images[0];


                    }
                }

                // 2번째 지름길에서 Yes를 한경우
                if (lastShortCutCheck1 == 1) 
                {

                    pb3[lastShortCutCnt1].Image = null;
                    if (outlineCnt1 == 10)
                    {
                        outlineCnt1++;
                    }
                    if (lastShortCutCnt1 < 8)
                    {
                        lastShortCutCnt1 = lastShortCutCnt1 + num;

                        if (ccnt1 == 1)
                        {
                            pb3[count1].Image = imgCharacter.Images[4];
                            ccnt1 = 0;
                        }
                        if (pb3[lastShortCutCnt1].Image == imgCharacter.Images[0])
                        {
                            count1 = lastShortCutCnt1;
                            ccnt1 = 1;
                        }
                        if (lastShortCutCnt1 < 7)
                        {
                            if (pb3[lastShortCutCnt1].Image == imgCharacter.Images[2])
                            {
                                pb3[lastShortCutCnt1].Image = imgCharacter.Images[0];
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                outlineCnt3 = 0;
                                firstShortCutCnt3 = 0;
                                centerShortCutCnt3 = 0;
                                lastShortCutCnt3 = 0;
                                firstShortCutCheck3 = 0;
                                centerShortCutCheck3 = 0;
                                lastShortCutCheck3 = 0;
                            }
                            if (pb3[lastShortCutCnt1].Image == imgCharacter.Images[3])
                            {
                                pb3[lastShortCutCnt1].Image = imgCharacter.Images[0];
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                outlineCnt4 = 0;
                                firstShortCutCnt4 = 0;
                                centerShortCutCnt4 = 0;
                                lastShortCutCnt4 = 0;
                                firstShortCutCheck4 = 0;
                                centerShortCutCheck4 = 0;
                                lastShortCutCheck4 = 0;
                            }
                        }
                        pb3[lastShortCutCnt1].Image = imgCharacter.Images[0];
                    }
                }

                
                //돌 도착 했을 때
                if (outlineCnt1 > 20) // 첫번째 돌이 도착했을때 
                {
                    label1.Text = "●"; //현황파 label에 색칠
                    label1.ForeColor = System.Drawing.Color.Blue;                  
                    btnBlue1.Visible = false; // 1P말이동 버튼을 안보이게만든다.
                }
                if (firstShortCutCnt1 > 11) // 지름길1로 도착햇을때
                {
                    label1.Text = "●";
                    label1.ForeColor = System.Drawing.Color.Blue;                   
                    btnBlue1.Visible = false;

                }
                if (centerShortCutCnt1 > 3) // 중앙 지름길으로 도착햇을때
                {
                    label1.Text = "●";
                    label1.ForeColor = System.Drawing.Color.Blue;                   
                    btnBlue1.Visible = false;

                }
                if (lastShortCutCnt1 > 6) //지름길 2로 도착햇을때
                {
                    label1.Text = "●";
                    label1.ForeColor = System.Drawing.Color.Blue;                  
                    btnBlue1.Visible = false;

                }
            }


            // 지름길 선택 여부
            // 1번째지름길에 도착했을경우
            if (outlineCnt1 == 5) 
            {
                if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes) //지름길로 갈것인지 메세지를 띄움
                {
                    firstShortCutCheck1 = 1;
                }
                else
                {
                    firstShortCutCheck1 = 0;
                }
            }
            // 중앙 지름길에 도착했을경우
            if (firstShortCutCnt1 == 3) 
            {
                if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    centerShortCutCheck1 = 1;
                }
                else
                {
                    centerShortCutCheck1 = 0;
                }
            }
            // 2번째 지름길에 도착했을경우
            if (outlineCnt1 == 10) 
            {
                if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lastShortCutCheck1 = 1;
                }
                else
                {
                    lastShortCutCheck1 = 0;
                }
            }
            finish(); 
        }
        
        private void btnBlue2_Click(object sender, EventArgs e)
        {
            PictureBox[] pb0 = new PictureBox[] { horsePos20, horsePos1 };
            //외곽
            PictureBox[] pb1 = new PictureBox[] { horsePos1, horsePos2, horsePos3, horsePos4, horsePos5, horsePos6, horsePos7, horsePos8, horsePos9, horsePos10, horsePos11, horsePos12, horsePos13, horsePos14, horsePos15, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //1st대각선
            PictureBox[] pb2 = new PictureBox[] { horsePos6, horsePos21, horsePos22, horsePos28, horsePos24, horsePos25, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //2nd대각선
            PictureBox[] pb3 = new PictureBox[] { horsePos11, horsePos26, horsePos27, horsePos28, horsePos29, horsePos30, horsePos1, not1, not2, not3, not4, not5, not6, not7 };

            if (outlineCnt2 < 22) 
            {
                pb1[outlineCnt2].Image = null;
                if (firstShortCutCheck2 == 0) 
                {
                    if (lastShortCutCheck2 == 0)
                    {
                        if (outlineCnt2 < 22)
                        {
                            outlineCnt2 += num; 

                            //백도 처리
                            /*if (outlineCnt2 == -1)
                            {
                                
                            }*/
                           
                            if (ccnt == 1)
                            {
                                pb1[count2].Image = imgCharacter.Images[1];
                                ccnt = 0;
                            }
                           
                            if (pb1[outlineCnt2].Image == imgCharacter.Images[0])
                            {
                                count2 = outlineCnt2;
                                ccnt = 1; 
                                pb1[count2].Image = imgCharacter.Images[4];
                            }
                            //1P의 돌이 2P의 돌을 만낫을때
                            if (outlineCnt2 < 21)
                            {
                                if (pb1[outlineCnt2].Image == imgCharacter.Images[2])
                                {
                                    pb1[outlineCnt2].Image = imgCharacter.Images[1];
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다."); 

                                }
                                if (pb1[outlineCnt2].Image == imgCharacter.Images[3]) // 1P의 돌이 2P의 두번째돌을 만났을때
                                {
                                    pb1[outlineCnt2].Image = imgCharacter.Images[1];
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                }
                            }
                            pb1[outlineCnt2].Image = imgCharacter.Images[1];
                        }
                    }
                }
                //지름길에서 yes를 한경우
                if (firstShortCutCheck2 == 1)
                {
                    pb2[firstShortCutCnt2].Image = null;
                    if (outlineCnt2 == 5) 
                    {
                        outlineCnt2++;
                    }
                    if (firstShortCutCnt2 < 13)
                    {
                        if (centerShortCutCheck2 != 1) 
                        {
                            firstShortCutCnt2 += num;
                            if (ccnt1 == 1)
                            {
                                pb2[count2].Image = imgCharacter.Images[1];
                                ccnt1 = 0;
                            }
                            if (pb2[outlineCnt2].Image == imgCharacter.Images[1])
                            {
                                count2 = firstShortCutCnt2;
                                ccnt1 = 1;
                            }
                            if (firstShortCutCnt2 < 12)
                            {
                                if (pb2[firstShortCutCnt2].Image == imgCharacter.Images[2])
                                {
                                    pb2[firstShortCutCnt2].Image = imgCharacter.Images[1];
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                    outlineCnt3 = 0;
                                    firstShortCutCnt3 = 0;
                                    centerShortCutCnt3 = 0;
                                    lastShortCutCnt3 = 0;
                                    firstShortCutCheck3 = 0;
                                    centerShortCutCheck3 = 0;
                                    lastShortCutCheck3 = 0;
                                }
                                if (pb2[firstShortCutCnt2].Image == imgCharacter.Images[3])
                                {
                                    pb2[firstShortCutCnt2].Image = imgCharacter.Images[1];
                                    MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다.");
                                    outlineCnt4 = 0;
                                    firstShortCutCnt4 = 0;
                                    centerShortCutCnt4 = 0;
                                    lastShortCutCnt4 = 0;
                                    firstShortCutCheck4 = 0;
                                    centerShortCutCheck4 = 0;
                                    lastShortCutCheck4 = 0;
                                }
                            }
                            pb2[firstShortCutCnt2].Image = imgCharacter.Images[1];
                        }
                    }
                }

                // 중앙 지름길에서 Yes를 한경우
                if (centerShortCutCheck2 == 1)
                {

                    pb3[3 + centerShortCutCnt2].Image = null;
                    if (firstShortCutCnt2 == 3) 
                    {
                        firstShortCutCnt2++;
                    }
                    if (centerShortCutCnt2 < 5)
                    {

                        centerShortCutCnt2 += num;
                        if (ccnt1 == 1)
                        {
                            pb3[count1].Image = imgCharacter.Images[0];
                            ccnt1 = 0;
                        }
                        if (pb3[count1].Image == imgCharacter.Images[0]) 
                        {                                                             
                            ccnt1 = 1;
                        }
                        if (centerShortCutCnt2 < 4)
                        {
                            if (pb3[3 + centerShortCutCnt2].Image == imgCharacter.Images[2])
                            {
                                pb3[count1].Image = imgCharacter.Images[1];
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                outlineCnt3 = 0;
                                firstShortCutCnt3 = 0;
                                centerShortCutCnt3 = 0;
                                lastShortCutCnt3 = 0;
                                firstShortCutCheck3 = 0;
                                centerShortCutCheck3 = 0;
                                lastShortCutCheck3 = 0;
                            }
                            if (pb3[3 + centerShortCutCnt2].Image == imgCharacter.Images[3])
                            {
                                pb3[count1].Image = imgCharacter.Images[1];
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                outlineCnt4 = 0;
                                firstShortCutCnt4 = 0;
                                centerShortCutCnt4 = 0;
                                lastShortCutCnt4 = 0;
                                firstShortCutCheck4 = 0;
                                centerShortCutCheck4 = 0;
                                lastShortCutCheck4 = 0;
                            }
                        }
                        pb3[3 + centerShortCutCnt2].Image = imgCharacter.Images[1];


                    }
                }

                // 2번째 지름길에서 Yes를 한경우
                if (lastShortCutCheck2 == 1)
                {

                    pb3[lastShortCutCnt2].Image = null;
                    if (outlineCnt2 == 10) 
                    {
                        outlineCnt2++;
                    }
                    if (lastShortCutCnt2 < 8)
                    {
                        lastShortCutCnt2 = lastShortCutCnt2 + num;

                        if (ccnt1 == 1)
                        {
                            pb3[count2].Image = imgCharacter.Images[1];
                            ccnt1 = 0;
                        }
                        if (pb3[lastShortCutCnt2].Image == imgCharacter.Images[1])
                        {
                            count2 = lastShortCutCnt2;
                            ccnt1 = 1;
                        }
                        if (lastShortCutCnt2 < 7)
                        {
                            if (pb3[lastShortCutCnt2].Image == imgCharacter.Images[2])
                            {
                                pb3[lastShortCutCnt2].Image = imgCharacter.Images[1];
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                outlineCnt3 = 0;
                                firstShortCutCnt3 = 0;
                                centerShortCutCnt3 = 0;
                                lastShortCutCnt3 = 0;
                                firstShortCutCheck3 = 0;
                                centerShortCutCheck3 = 0;
                                lastShortCutCheck3 = 0;
                            }
                            if (pb3[lastShortCutCnt2].Image == imgCharacter.Images[3])
                            {
                                pb3[lastShortCutCnt2].Image = imgCharacter.Images[1];
                                MessageBox.Show("빨간돌이 파란돌에게 먹혔습니다");
                                outlineCnt4 = 0;
                                firstShortCutCnt4 = 0;
                                centerShortCutCnt4 = 0;
                                lastShortCutCnt4 = 0;
                                firstShortCutCheck4 = 0;
                                centerShortCutCheck4 = 0;
                                lastShortCutCheck4 = 0;
                            }
                        }
                        pb3[lastShortCutCnt2].Image = imgCharacter.Images[1];
                    }
                }


                //돌 도착 했을 때
                if (outlineCnt2 > 20) 
                {
                    label2.Text = "●"; 
                    label2.ForeColor = System.Drawing.Color.Blue;
                    btnBlue2.Visible = false; 
                }
                if (firstShortCutCnt2 > 11) 
                {
                    label2.Text = "●";
                    label2.ForeColor = System.Drawing.Color.Blue;
                    btnBlue2.Visible = false;

                }
                if (centerShortCutCnt2 > 3) 
                {
                    label2.Text = "●";
                    label2.ForeColor = System.Drawing.Color.Blue;
                    btnBlue2.Visible = false;

                }
                if (lastShortCutCnt2 > 6) 
                {
                    label2.Text = "●";
                    label2.ForeColor = System.Drawing.Color.Blue;
                    btnBlue2.Visible = false;

                }
            }



            //지름길 선택 여부
            if (outlineCnt2 == 5)
            {
                if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes) //지름길로 갈것인지 메세지를 띄움
                {
                    firstShortCutCheck2 = 1;
                }
                else
                {
                    firstShortCutCheck2 = 0;
                }
            }
            if (firstShortCutCnt2 == 3) 
            {
                if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    centerShortCutCheck2 = 1;
                }
                else
                {
                    centerShortCutCheck2 = 0;
                }
            }
            if (outlineCnt2 == 10)
            {
                if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lastShortCutCheck2 = 1;
                }
                else
                {
                    lastShortCutCheck2 = 0;
                }
            }
            finish();
        }

        private void btnRed1_Click(object sender, EventArgs e)
        {
            //외곽
            PictureBox[] pb1 = new PictureBox[] { horsePos1, horsePos2, horsePos3, horsePos4, horsePos5, horsePos6, horsePos7, horsePos8, horsePos9, horsePos10, horsePos11, horsePos12, horsePos13, horsePos14, horsePos15, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //1st대각선
            PictureBox[] pb2 = new PictureBox[] { horsePos6, horsePos21, horsePos22, horsePos28, horsePos24, horsePos25, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //2nd대각선
            PictureBox[] pb3 = new PictureBox[] { horsePos11, horsePos26, horsePos27, horsePos28, horsePos29, horsePos30, horsePos1, not1, not2, not3, not4, not5, not6, not7 };


            //외각라인 
            if (outlineCnt3 < 22)
            {
                pb1[outlineCnt3].Image = null;

                // 첫 지름길에서 NO를 선택한경우
                if (firstShortCutCheck3 == 0)
                {
                    //마지막 지름길에서 NO를 선택한경우
                    if (lastShortCutCheck3 == 0)
                    {
                        if (outlineCnt3 < 22)
                        {
                            //아웃라인 전진
                            outlineCnt3 += num;

                            //백도 처리
                            /*if(outlineCnt1 == -1)
                            {
                               
                            }*/

                            // ccnt가 1일때 즉 1P의말이 겹쳣을때를 말하는 코드이다.
                            if (ccnt == 1)
                            {
                                pb1[count3].Image = imgCharacter.Images[5];
                                ccnt = 0;
                            }

                            // 2P의 두개의돌이 만났을때
                            if (pb1[outlineCnt3].Image == imgCharacter.Images[3])
                            {
                                count3 = outlineCnt3; //count3에 outlineCnt1값을 넣어준다
                                ccnt = 1; // ccnt는 1로 변경해준다.
                                pb1[count3].Image = imgCharacter.Images[5];
                            }

                            //2P의 돌이 1P의 돌을 만낫을때
                            if (outlineCnt3 < 21)
                            {
                                //2P의 돌이 1P 첫번째돌을 만낫을때
                                if (pb1[outlineCnt3].Image == imgCharacter.Images[0])
                                {
                                    pb1[outlineCnt3].Image = imgCharacter.Images[2];
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다");

                                }
                                // 2P의 돌이 1P의 두번째돌을 만났을때
                                if (pb1[outlineCnt3].Image == imgCharacter.Images[3])
                                {
                                    pb1[outlineCnt3].Image = imgCharacter.Images[2];
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다");
                                }
                            }
                            pb1[outlineCnt3].Image = imgCharacter.Images[2];
                        }
                    }
                }
                //첫 지름길에서 yes를 한경우
                if (firstShortCutCheck3 == 1)
                {
                    pb2[firstShortCutCnt3].Image = null;
                    if (outlineCnt3 == 5)
                    {
                        outlineCnt3++;
                    }
                    if (firstShortCutCnt3 < 13)
                    {
                        // 중앙 지름길로 가지않앗을때
                        if (centerShortCutCheck3 != 1)
                        {
                            firstShortCutCnt3 += num;
                            if (ccnt1 == 1)
                            {
                                pb2[count3].Image = imgCharacter.Images[5];
                                ccnt1 = 0;
                            }
                            if (pb2[outlineCnt3].Image == imgCharacter.Images[2])
                            {
                                count3 = firstShortCutCnt3;
                                ccnt1 = 1;
                            }
                            if (firstShortCutCnt3 < 12)
                            {
                                if (pb2[firstShortCutCnt3].Image == imgCharacter.Images[0])
                                {
                                    pb2[firstShortCutCnt3].Image = imgCharacter.Images[2];
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                    outlineCnt1 = 0;
                                    firstShortCutCnt1 = 0;
                                    centerShortCutCnt1 = 0;
                                    lastShortCutCnt1 = 0;
                                    firstShortCutCheck1 = 0;
                                    centerShortCutCheck1 = 0;
                                    lastShortCutCheck1 = 0;
                                }
                                if (pb2[firstShortCutCnt3].Image == imgCharacter.Images[1])
                                {
                                    pb2[firstShortCutCnt3].Image = imgCharacter.Images[2];
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                    outlineCnt2 = 0;
                                    firstShortCutCnt2 = 0;
                                    centerShortCutCnt2 = 0;
                                    lastShortCutCnt2 = 0;
                                    firstShortCutCheck2 = 0;
                                    centerShortCutCheck2 = 0;
                                    lastShortCutCheck2 = 0;
                                }
                            }
                            pb2[firstShortCutCnt3].Image = imgCharacter.Images[2];
                        }
                    }
                }

                // 중앙 지름길에서 Yes를 한경우
                if (centerShortCutCheck3 == 1)
                {

                    pb3[3 + centerShortCutCnt3].Image = null;
                    if (firstShortCutCnt3 == 3)
                    {
                        firstShortCutCnt3++;
                    }
                    if (centerShortCutCnt3 < 5)
                    {

                        centerShortCutCnt3 += num;
                        if (ccnt1 == 1)
                        {
                            pb3[count3].Image = imgCharacter.Images[5];
                            ccnt1 = 0;
                        }
                        if (pb3[count3].Image == imgCharacter.Images[2])
                        {
                            count3 = 3 + centerShortCutCnt3;
                            ccnt1 = 1;
                        }

                        if (centerShortCutCnt3 < 4)
                        {
                            if (pb3[3 + centerShortCutCnt3].Image == imgCharacter.Images[0])
                            {
                                pb3[count3].Image = imgCharacter.Images[2];
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                outlineCnt1 = 0;
                                firstShortCutCnt1 = 0;
                                centerShortCutCnt1 = 0;
                                lastShortCutCnt1 = 0;
                                firstShortCutCheck1 = 0;
                                centerShortCutCheck1 = 0;
                                lastShortCutCheck1 = 0;
                            }
                            if (pb3[3 + centerShortCutCnt3].Image == imgCharacter.Images[3])
                            {
                                pb3[count3].Image = imgCharacter.Images[2];
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                outlineCnt2 = 0;
                                firstShortCutCnt2 = 0;
                                centerShortCutCnt2 = 0;
                                lastShortCutCnt2 = 0;
                                firstShortCutCheck2 = 0;
                                centerShortCutCheck2 = 0;
                                lastShortCutCheck2 = 0;
                            }
                        }
                        pb3[3 + centerShortCutCnt3].Image = imgCharacter.Images[2];


                    }
                }

                // 2번째 지름길에서 Yes를 한경우
                if (lastShortCutCheck3 == 1)
                {

                    pb3[lastShortCutCnt3].Image = null;
                    if (outlineCnt3 == 10)
                    {
                        outlineCnt3++;
                    }
                    if (lastShortCutCnt3 < 8)
                    {
                        lastShortCutCnt3 = lastShortCutCnt3 + num;

                        if (ccnt1 == 1)
                        {
                            pb3[count3].Image = imgCharacter.Images[5];
                            ccnt1 = 0;
                        }
                        if (pb3[lastShortCutCnt3].Image == imgCharacter.Images[3])
                        {
                            count3 = lastShortCutCnt3;
                            ccnt1 = 1;
                        }
                        if (lastShortCutCnt3 < 7)
                        {
                            if (pb3[lastShortCutCnt3].Image == imgCharacter.Images[0])
                            {
                                pb3[lastShortCutCnt3].Image = imgCharacter.Images[2];
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                outlineCnt1 = 0;
                                firstShortCutCnt1 = 0;
                                centerShortCutCnt1 = 0;
                                lastShortCutCnt1 = 0;
                                firstShortCutCheck1 = 0;
                                centerShortCutCheck1 = 0;
                                lastShortCutCheck1 = 0;
                            }
                            if (pb3[lastShortCutCnt3].Image == imgCharacter.Images[1])
                            {
                                pb3[lastShortCutCnt3].Image = imgCharacter.Images[2];
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                outlineCnt2 = 0;
                                firstShortCutCnt2 = 0;
                                centerShortCutCnt2 = 0;
                                lastShortCutCnt2 = 0;
                                firstShortCutCheck2 = 0;
                                centerShortCutCheck2 = 0;
                                lastShortCutCheck2 = 0;
                            }
                        }
                        pb3[lastShortCutCnt3].Image = imgCharacter.Images[2];
                    }
                }


                //돌 도착 했을 때
                if (outlineCnt3 > 20) // 첫번째 돌이 도착했을때 
                {
                    label3.Text = "●"; //현황파 label에 색칠
                    label3.ForeColor = System.Drawing.Color.Red;
                    btnRed1.Visible = false; // 2P말이동 버튼을 안보이게만든다.
                }
                if (firstShortCutCnt3 > 11) // 지름길1로 도착햇을때
                {
                    label3.Text = "●";
                    label3.ForeColor = System.Drawing.Color.Red;
                    btnRed1.Visible = false;

                }
                if (centerShortCutCnt3 > 3) // 중앙 지름길으로 도착햇을때
                {
                    label3.Text = "●";
                    label3.ForeColor = System.Drawing.Color.Red;
                    btnRed1.Visible = false;

                }
                if (lastShortCutCnt3 > 6) //지름길 2로 도착햇을때
                {
                    label3.Text = "●";
                    label3.ForeColor = System.Drawing.Color.Red;
                    btnRed1.Visible = false;

                }
            }


            // 지름길 선택 여부
            // 1번째지름길에 도착했을경우
            if (outlineCnt3 == 5)
            {
                if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes) //지름길로 갈것인지 메세지를 띄움
                {
                    firstShortCutCheck3 = 1;
                }
                else
                {
                    firstShortCutCheck3 = 0;
                }
            }
            // 중앙 지름길에 도착했을경우
            if (firstShortCutCnt3 == 3)
            {
                if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    centerShortCutCheck3 = 1;
                }
                else
                {
                    centerShortCutCheck3 = 0;
                }
            }
            // 2번째 지름길에 도착했을경우
            if (outlineCnt3 == 10)
            {
                if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lastShortCutCheck3 = 1;
                }
                else
                {
                    lastShortCutCheck3 = 0;
                }
            }
            finish();
        }

        private void btnRed2_Click(object sender, EventArgs e)
        {
            //외곽
            PictureBox[] pb1 = new PictureBox[] { horsePos1, horsePos2, horsePos3, horsePos4, horsePos5, horsePos6, horsePos7, horsePos8, horsePos9, horsePos10, horsePos11, horsePos12, horsePos13, horsePos14, horsePos15, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //1st대각선
            PictureBox[] pb2 = new PictureBox[] { horsePos6, horsePos21, horsePos22, horsePos28, horsePos24, horsePos25, horsePos16, horsePos17, horsePos18, horsePos19, horsePos20, horsePos1, not1, not2, not3, not4, not5, not6, not7 };
            //2nd대각선
            PictureBox[] pb3 = new PictureBox[] { horsePos11, horsePos26, horsePos27, horsePos28, horsePos29, horsePos30, horsePos1, not1, not2, not3, not4, not5, not6, not7 };


            //외각라인 
            if (outlineCnt4 < 22)
            {
                pb1[outlineCnt4].Image = null;

                // 첫 지름길에서 NO를 선택한경우
                if (firstShortCutCheck4 == 0)
                {
                    //마지막 지름길에서 NO를 선택한경우
                    if (lastShortCutCheck4 == 0)
                    {
                        if (outlineCnt4 < 22)
                        {
                            //아웃라인 전진
                            outlineCnt4 += num;

                            //백도 처리
                            /*if(outlineCnt1 == -1)
                            {
                               
                            }*/

                            // ccnt가 1일때 즉 1P의말이 겹쳣을때를 말하는 코드이다.
                            if (ccnt == 1)
                            {
                                pb1[count4].Image = imgCharacter.Images[5];
                                ccnt = 0;
                            }

                            // 2P의 두개의돌이 만났을때
                            if (pb1[outlineCnt4].Image == imgCharacter.Images[2])
                            {
                                count4 = outlineCnt4; //count4에 outlineCnt1값을 넣어준다
                                ccnt = 1; // ccnt는 1로 변경해준다.
                                pb1[count4].Image = imgCharacter.Images[5];
                            }

                            //2P의 돌이 1P의 돌을 만낫을때
                            if (outlineCnt4 < 21)
                            {
                                //2P의 돌이 1P 첫번째돌을 만낫을때
                                if (pb1[outlineCnt4].Image == imgCharacter.Images[0])
                                {
                                    pb1[outlineCnt4].Image = imgCharacter.Images[3];
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다");

                                }
                                // 2P의 돌이 1P의 두번째돌을 만났을때
                                if (pb1[outlineCnt4].Image == imgCharacter.Images[3])
                                {
                                    pb1[outlineCnt4].Image = imgCharacter.Images[3];
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다");
                                }
                            }
                            pb1[outlineCnt4].Image = imgCharacter.Images[3];
                        }
                    }
                }
                //첫 지름길에서 yes를 한경우
                if (firstShortCutCheck4 == 1)
                {
                    pb2[firstShortCutCnt4].Image = null;
                    if (outlineCnt4 == 5)
                    {
                        outlineCnt4++;
                    }
                    if (firstShortCutCnt4 < 13)
                    {
                        // 중앙 지름길로 가지않앗을때
                        if (centerShortCutCheck4 != 1)
                        {
                            firstShortCutCnt4 += num;
                            if (ccnt1 == 1)
                            {
                                pb2[count4].Image = imgCharacter.Images[5];
                                ccnt1 = 0;
                            }
                            if (pb2[outlineCnt4].Image == imgCharacter.Images[2])
                            {
                                count4 = firstShortCutCnt4;
                                ccnt1 = 1;
                            }
                            if (firstShortCutCnt4 < 12)
                            {
                                if (pb2[firstShortCutCnt4].Image == imgCharacter.Images[0])
                                {
                                    pb2[firstShortCutCnt4].Image = imgCharacter.Images[3];
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                    outlineCnt1 = 0;
                                    firstShortCutCnt1 = 0;
                                    centerShortCutCnt1 = 0;
                                    lastShortCutCnt1 = 0;
                                    firstShortCutCheck1 = 0;
                                    centerShortCutCheck1 = 0;
                                    lastShortCutCheck1 = 0;
                                }
                                if (pb2[firstShortCutCnt4].Image == imgCharacter.Images[1])
                                {
                                    pb2[firstShortCutCnt4].Image = imgCharacter.Images[3];
                                    MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                    outlineCnt2 = 0;
                                    firstShortCutCnt2 = 0;
                                    centerShortCutCnt2 = 0;
                                    lastShortCutCnt2 = 0;
                                    firstShortCutCheck2 = 0;
                                    centerShortCutCheck2 = 0;
                                    lastShortCutCheck2 = 0;
                                }
                            }
                            pb2[firstShortCutCnt4].Image = imgCharacter.Images[3];
                        }
                    }
                }

                // 중앙 지름길에서 Yes를 한경우
                if (centerShortCutCheck4 == 1)
                {

                    pb3[3 + centerShortCutCnt4].Image = null;
                    if (firstShortCutCnt4 == 3)
                    {
                        firstShortCutCnt4++;
                    }
                    if (centerShortCutCnt4 < 5)
                    {

                        centerShortCutCnt4 += num;
                        if (ccnt1 == 1)
                        {
                            pb3[count4].Image = imgCharacter.Images[5];
                            ccnt1 = 0;
                        }
                        if (pb3[count4].Image == imgCharacter.Images[2])
                        {
                            count4 = 3 + centerShortCutCnt4;
                            ccnt1 = 1;
                        }

                        if (centerShortCutCnt4 < 4)
                        {
                            if (pb3[3 + centerShortCutCnt4].Image == imgCharacter.Images[0])
                            {
                                pb3[count4].Image = imgCharacter.Images[3];
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                outlineCnt1 = 0;
                                firstShortCutCnt1 = 0;
                                centerShortCutCnt1 = 0;
                                lastShortCutCnt1 = 0;
                                firstShortCutCheck1 = 0;
                                centerShortCutCheck1 = 0;
                                lastShortCutCheck1 = 0;
                            }
                            if (pb3[3 + centerShortCutCnt4].Image == imgCharacter.Images[3])
                            {
                                pb3[count4].Image = imgCharacter.Images[3];
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                outlineCnt2 = 0;
                                firstShortCutCnt2 = 0;
                                centerShortCutCnt2 = 0;
                                lastShortCutCnt2 = 0;
                                firstShortCutCheck2 = 0;
                                centerShortCutCheck2 = 0;
                                lastShortCutCheck2 = 0;
                            }
                        }
                        pb3[3 + centerShortCutCnt4].Image = imgCharacter.Images[3];


                    }
                }

                // 2번째 지름길에서 Yes를 한경우
                if (lastShortCutCheck4 == 1)
                {

                    pb3[lastShortCutCnt4].Image = null;
                    if (outlineCnt4 == 10)
                    {
                        outlineCnt4++;
                    }
                    if (lastShortCutCnt4 < 8)
                    {
                        lastShortCutCnt4 = lastShortCutCnt4 + num;

                        if (ccnt1 == 1)
                        {
                            pb3[count4].Image = imgCharacter.Images[5];
                            ccnt1 = 0;
                        }
                        if (pb3[lastShortCutCnt4].Image == imgCharacter.Images[2])
                        {
                            count4 = lastShortCutCnt4;
                            ccnt1 = 1;
                        }
                        if (lastShortCutCnt4 < 7)
                        {
                            if (pb3[lastShortCutCnt4].Image == imgCharacter.Images[0])
                            {
                                pb3[lastShortCutCnt4].Image = imgCharacter.Images[3];
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                outlineCnt1 = 0;
                                firstShortCutCnt1 = 0;
                                centerShortCutCnt1 = 0;
                                lastShortCutCnt1 = 0;
                                firstShortCutCheck1 = 0;
                                centerShortCutCheck1 = 0;
                                lastShortCutCheck1 = 0;
                            }
                            if (pb3[lastShortCutCnt3].Image == imgCharacter.Images[1])
                            {
                                pb3[lastShortCutCnt3].Image = imgCharacter.Images[3];
                                MessageBox.Show("파란돌이 빨간돌에게 먹혔습니다.");
                                outlineCnt2 = 0;
                                firstShortCutCnt2 = 0;
                                centerShortCutCnt2 = 0;
                                lastShortCutCnt2 = 0;
                                firstShortCutCheck2 = 0;
                                centerShortCutCheck2 = 0;
                                lastShortCutCheck2 = 0;
                            }
                        }
                        pb3[lastShortCutCnt3].Image = imgCharacter.Images[3];
                    }
                }


                //돌 도착 했을 때
                if (outlineCnt4 > 20) // 첫번째 돌이 도착했을때 
                {
                    label4.Text = "●"; //현황파 label에 색칠
                    label4.ForeColor = System.Drawing.Color.Red;
                    btnRed2.Visible = false; // 2P말이동 버튼을 안보이게만든다.
                }
                if (firstShortCutCnt4 > 11) // 지름길1로 도착햇을때
                {
                    label4.Text = "●";
                    label4.ForeColor = System.Drawing.Color.Red;
                    btnRed2.Visible = false;

                }
                if (centerShortCutCnt4 > 3) // 중앙 지름길으로 도착햇을때
                {
                    label4.Text = "●";
                    label4.ForeColor = System.Drawing.Color.Red;
                    btnRed2.Visible = false;

                }
                if (lastShortCutCnt4 > 6) //지름길 2로 도착햇을때
                {
                    label4.Text = "●";
                    label4.ForeColor = System.Drawing.Color.Red;
                    btnRed2.Visible = false;

                }
            }


            // 지름길 선택 여부
            // 1번째지름길에 도착했을경우
            if (outlineCnt4 == 5)
            {
                if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes) //지름길로 갈것인지 메세지를 띄움
                {
                    firstShortCutCheck4 = 1;
                }
                else
                {
                    firstShortCutCheck4 = 0;
                }
            }
            // 중앙 지름길에 도착했을경우
            if (firstShortCutCnt4 == 3)
            {
                if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    centerShortCutCheck4 = 1;
                }
                else
                {
                    centerShortCutCheck4 = 0;
                }
            }
            // 2번째 지름길에 도착했을경우
            if (outlineCnt4 == 10)
            {
                if (MessageBox.Show("지름길로 가시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lastShortCutCheck4 = 1;
                }
                else
                {
                    lastShortCutCheck4 = 0;
                }
            }
            finish();
        }
    }
}
