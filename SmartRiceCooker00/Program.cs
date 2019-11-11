using System;
using System.Media;
using System.Threading;

namespace SmartRiceCooker00
{
    enum CookerProcess { None, Riceing, Watering, Washing, Droping, Cooking, Completion, Keeping }; //...☎0010.SFCS0010_Enum.

    struct RiceCookerInfo  //...☎0020.SFCS0030_Struct.
    {
        public bool CoverOpenClose; // 뚜껑 열기 닫기
        public bool Power;
        public CookerProcess State;
        public int Rice;   // 쌀의 양 g 기준, 출력할 때 환산
        public int Water;  // 물의 양 미리 리터 기준, 출력할 때 환산
        public int Number; // 인원수

        public RiceCookerInfo(int _Rice, int _Water)
        {
            Rice = _Rice;
            Water = _Water;
            State = CookerProcess.None;
            CoverOpenClose = false;
            Power = false;
            Number = 0;
        }
    }

    class Program
    {
        public static int MainMenuIndex = 0; // 메인 메뉴 선택 인덱스

        static void Main(string[] args)
        {
            Console.SetWindowSize(99, 36); //...Console Class in C#. https://www.geeksforgeeks.org/console-class-in-c-sharp/
            RiceCookerInfo RCInfo = new RiceCookerInfo(10000, 5000);
            SoundPlayer Sound = new SoundPlayer(); //...System.Media.SoundPlayer Class in C#. https://csharp.hotexamples.com/examples/-/System.Media.SoundPlayer/Play/php-system.media.soundplayer-play-method-examples.html

            string[] MenuItem = { "   전원   ", "   뚜껑   ", "   취사   ", "   보온   ", "   취소   ",
                                    "  인원수  ", "    쌀   ", "    물    "  };  //...☎0030.SFCS0050_Array.

            while (true) //...☎0040.SFCS0070_Loop.173p.
            {
                showOutFrame();
                showRiceBox(16, 11);
                showCoverOpenOrClose(RCInfo.CoverOpenClose);
                showRiceInfo(RCInfo);
                showPowerLine(RCInfo.Power);
                showRiceHeight(50, 2, RCInfo.Rice);
                showWaterHeight(74, 2, RCInfo.Water);

                showMenu(65, 25, MenuItem); //...사용자 입력을 받으면 화면이 반짝이지 않으므로 showMenu부터 만들것.
                //if (MainMenuIndex == 9)
                //    break;

                switch (MainMenuIndex)
                {
                    case 0: // 전원
                        RCInfo.Power = !RCInfo.Power;
                        if (RCInfo.Power)
                        {
                            Sound.SoundLocation = "power_on.wav";
                        }
                        else
                        {
                            Sound.SoundLocation = "power_off.wav";
                        }
                        Sound.Load();
                        Sound.PlaySync();
                        break;

                    case 1: // 뚜껑, 취사 중에 뚜껑이 열리면 안된다.
                        if (RCInfo.State == CookerProcess.Cooking)
                        {
                            showMessageBox(51, 27, " 취사 중 일때는 뚜껑을 열 수 없습니다");
                            Console.ReadKey(true);
                        }
                        else
                        {
                            RCInfo.CoverOpenClose = !RCInfo.CoverOpenClose;
                            if (RCInfo.CoverOpenClose)
                                Sound.SoundLocation = "cover_open.wav";
                            else
                                Sound.SoundLocation = "cover_close.wav";
                            Sound.Load();
                            Sound.Play();
                        }
                        break;

                    case 2: // 취사
                        if (!RCInfo.Power)
                        {
                            // 밧데리로 일부 메시지 전달
                            showMessageBox(51, 27, "전원이 꺼져 있습니다");
                            Console.ReadKey(true);
                            break;
                        }

                        if (RCInfo.CoverOpenClose)
                        {
                            // 밧데리로 일부 메시지 전달
                            showMessageBox(51, 27, "뚜껑이 열려져 있습니다");
                            Console.ReadKey(true);
                            break;
                        }

                        if (RCInfo.Number == 0)
                        {
                            // 밧데리로 일부 메시지 전달
                            showMessageBox(51, 27, "인원수를 미입력되어 있습니다.");
                            Thread.Sleep(3000); // 3초 정도 쓰레드의 실행을 중지하고, 다른 쓰레드에게 CPU 시간을 양보합니다.                            
                            showMessageBox(51, 27, " 식사할 인원 수 : ");
                            try
                            {
                                RCInfo.Number = int.Parse(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                RCInfo.Number = 0;
                            }
                            break;
                        }

                        // 일정한 시간 간격으로 쌀 넣기 -> 물 넣기 -> 쌀 씻기 -> 배수 -> 2 번 반복, 물 넣기부터 
                        //                     취사 -> 완료 -> 보온                       
                        // 필요한 쌀과 물의 공급이 되어 있는지를 체크한다.
                        int Rice = RCInfo.Rice - (RCInfo.Number * 160); // 쌀 일인분 160g
                        if (Rice < 0)
                        {
                            showMessageBox(51, 27, "  ??? 쌀 부족 ???");
                            Sound.SoundLocation = "쌀을보충해주세요.WAV";
                            Sound.Load();
                            Sound.Play();
                            Console.ReadKey(true);
                            break;
                        }

                        //  물통에서 물 빼기 (대략 인원수 x 170 ml ) * 5; //물로 씻는 거 2번 취사 1번 총 3번 양이 필요
                        //  씻을 때는 1인분 물 170의 두 배 사용, 필요한 물은 씻기 2번(170*4 ml) 취사 1 번(170 ml)
                        int Water;
                        Water = RCInfo.Water - (RCInfo.Number * 170) * 5;
                        if (Water < 0)
                        {
                            showMessageBox(51, 27, "  ??? 물 부족 ???");
                            Sound.SoundLocation = "물보충.WAV";
                            Sound.Load();
                            Sound.Play();
                            Console.ReadKey(true);
                            break;
                        }

                        // Note: 취사 시작 부분, 쌀 넣기 -> 물 넣기 -> 쌀 씻기 -> 배수 -> 취사 -> 보온
                        RCInfo.State = CookerProcess.Riceing;
                        showRiceInfo(RCInfo);

                        Sound.SoundLocation = "쌀넣기.WAV";
                        Sound.Load();
                        Sound.Play();

                        Console.SetCursorPosition(24, 12);
                        Console.Write("쌀 넣기");
                        Console.SetCursorPosition(18, 13);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Console.SetCursorPosition(18, 14);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Console.SetCursorPosition(18, 15);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Console.SetCursorPosition(18, 16);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Console.SetCursorPosition(18, 17);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        RCInfo.Rice = RCInfo.Rice - (RCInfo.Number * 160); // 1인분 160g
                        showRiceHeight(50, 2, RCInfo.Rice);
                        Thread.Sleep(3000); // 3초 정도 쓰레드의 실행을 중지하고, 다른 쓰레드에게 CPU 시간을 양보합니다.                            

                        for (int i = 0; i < 2; i++)
                        {
                            // Note: 물 넣기 --> 파란 색 보여 주기
                            RCInfo.State = CookerProcess.Watering;
                            RCInfo.Water = RCInfo.Water - (RCInfo.Number * 170 * 2);
                            showRiceInfo(RCInfo);

                            Sound.SoundLocation = "water_in.WAV";
                            Sound.Load();
                            Sound.Play();

                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(24, 12);
                            Console.Write("물 넣기");
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.SetCursorPosition(18, 13);
                            Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                            Console.SetCursorPosition(18, 14);
                            Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                            Console.SetCursorPosition(18, 15);
                            Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                            Console.SetCursorPosition(18, 16);
                            Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                            Console.SetCursorPosition(18, 17);
                            Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                            showWaterHeight(74, 2, RCInfo.Water);
                            Thread.Sleep(3000); // 3초 정도

                            // Note: 쌀 씻기 
                            Sound.SoundLocation = "쌀씻기.wav";
                            Sound.Load();
                            Sound.Play();
                            RCInfo.State = CookerProcess.Washing;
                            showRiceInfo(RCInfo);

                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(24, 12);
                            Console.Write("쌀 씻기");
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(18, 13);
                            Console.Write("~ ~ ~ ~ ~ ~ ~ ~ ~ ~");
                            Console.SetCursorPosition(18, 14);
                            Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                            Console.SetCursorPosition(18, 15);
                            Console.Write("~ ~ ~ ~ ~ ~ ~ ~ ~ ~");
                            Console.SetCursorPosition(18, 16);
                            Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                            Console.SetCursorPosition(18, 17);
                            Console.Write("~ ~ ~ ~ ~ ~ ~ ~ ~ ~");
                            Thread.Sleep(3000); // 3초 정도

                            // Note: 물 배수 
                            RCInfo.State = CookerProcess.Droping;
                            showRiceInfo(RCInfo);

                            Sound.SoundLocation = "water_out.WAV";
                            Sound.Load();
                            Sound.Play();

                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(24, 12);
                            Console.Write(" 배수 ");
                            for (int j = 0; j < 5; j++)
                            {
                                // 지우기
                                Console.BackgroundColor = ConsoleColor.Black;
                                for (int k = 0; k < j; k++)
                                {
                                    Console.SetCursorPosition(18, 13 + k);
                                    Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                                }

                                // 물 출력
                                Console.BackgroundColor = ConsoleColor.Blue;
                                for (int k = j; k < 5; k++)
                                {
                                    Console.SetCursorPosition(18, 13 + k);
                                    Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                                }
                                Thread.Sleep(700);
                            }

                        }

                        // Note: 취사용 물 공급
                        RCInfo.Water = RCInfo.Water - (RCInfo.Number * 170);
                        showWaterHeight(74, 2, RCInfo.Water);
                        showRiceInfo(RCInfo);

                        // Note: 취사 시작
                        RCInfo.State = CookerProcess.Cooking;
                        showRiceInfo(RCInfo);

                        Sound.SoundLocation = "rice.wav";
                        Sound.Load();
                        Sound.Play();

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(24, 12);
                        Console.Write("취사 중");
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(18, 13);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Console.SetCursorPosition(18, 14);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Console.SetCursorPosition(18, 15);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Console.SetCursorPosition(18, 16);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Console.SetCursorPosition(18, 17);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Thread.Sleep(7000); // 7초 정도

                        // Note: 완료 , 사운드 삐리릭...
                        RCInfo.State = CookerProcess.Completion;
                        showRiceInfo(RCInfo);
                        Sound.SoundLocation = "Ring10.wav";
                        Sound.Load();
                        Sound.Play();
                        Thread.Sleep(7000); // 3초 정도

                        Sound.SoundLocation = "밥완료.wav";
                        Sound.Load();
                        Sound.Play();

                        Console.SetCursorPosition(24, 12);
                        Console.Write("취사 완료");
                        Thread.Sleep(3000); // 3초 정도

                        // Note: 보온
                        RCInfo.State = CookerProcess.Keeping;
                        showRiceInfo(RCInfo);

                        Sound.SoundLocation = "맛있게드세요.wav";
                        Sound.Load();
                        Sound.Play();

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(24, 12);
                        Console.Write("보온 중  ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(18, 13);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Console.SetCursorPosition(18, 14);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Console.SetCursorPosition(18, 15);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Console.SetCursorPosition(18, 16);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Console.SetCursorPosition(18, 17);
                        Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
                        Thread.Sleep(3000); // 3초 정도
                        Console.ForegroundColor = ConsoleColor.White;

                        RCInfo.Number = 0; // Note: 인원수 초기화
                        break;

                    case 3: // Note:보온
                        if (!RCInfo.Power)
                        {
                            // 밧데리로 일부 메시지 전달
                            showMessageBox(51, 27, "전원이 꺼져 있습니다");
                            Console.ReadKey(true);
                            break;
                        }

                        RCInfo.State = CookerProcess.Keeping;
                        showRiceInfo(RCInfo);
                        break;

                    case 4: // 취소
                        RCInfo.State = CookerProcess.None;
                        showRiceInfo(RCInfo);
                        break;

                    case 5: // 인원수
                        if (!RCInfo.Power)
                        {
                            // 밧데리로 일부 메시지 전달
                            showMessageBox(51, 27, "전원이 꺼져 있습니다");
                            Console.ReadKey(true);
                            break;
                        }

                        showMessageBox(51, 27, " 식사할 인원 수 : ");
                        try
                        {
                            RCInfo.Number = int.Parse(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            RCInfo.Number = 0;
                        }
                        break;

                    case 6: // 쌀통 설정
                        {
                            string Message = "현재 쌀의 양(kg) : " + (RCInfo.Rice / 1000);
                            showMessageBox(51, 27, Message);
                            Console.SetCursorPosition(63, 29);
                            Console.Write("추가할 쌀 양(kg) : ");
                            string Amount = Console.ReadLine();
                            try
                            {
                                RCInfo.Rice += int.Parse(Amount) * 1000; // kg 단위
                                if (RCInfo.Rice > 18000) // 18kg 최대
                                {
                                    RCInfo.Rice -= int.Parse(Amount) * 1000;
                                    showMessageBox(51, 27, "양이 너무 많습니다");
                                    Console.ReadKey(true);
                                    break;
                                }
                            }
                            catch (Exception e)
                            {
                                break;
                            }
                        }
                        break;

                    case 7: // 뭍통 설정
                        {
                            string Message = "현재 물의 양(리터) : " + (RCInfo.Water / 1000);
                            showMessageBox(51, 27, Message);
                            string Amount = string.Empty;
                            Console.SetCursorPosition(63, 29);
                            Console.Write("추가할 물의 양(리터) : ");
                            Amount = Console.ReadLine();
                            try
                            {
                                RCInfo.Water += int.Parse(Amount) * 1000; // 리터를 밀리리터로 
                                if (RCInfo.Water > 18000)
                                {
                                    RCInfo.Water -= int.Parse(Amount) * 1000;
                                    showMessageBox(51, 27, "양이 너무 많습니다");
                                    Console.ReadKey(true);
                                    break;
                                }
                            }
                            catch (Exception e)
                            {
                                break;
                            }
                        }
                        break;
                }

            }

        }

        private static void showMessageBox(int x, int y, string Message)
        {
            int height = 3;
            Console.SetCursorPosition(x, y);
            Console.Write("┌───────────────────┐");
            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("│                                      │");
            }

            Console.SetCursorPosition(x, y + height);
            Console.Write("└───────────────────┘");
            Console.SetCursorPosition(x + 2, y + 1);
            Console.Write(Message);
        }

        private static void showMenu(int x, int y, string[] MenuItem)
        {
            ConsoleKeyInfo InputKey; //...http://www.dotnetframework.org/default.aspx/4@0/4@0/DEVDIV_TFS/Dev10/Releases/RTMRel/ndp/clr/src/BCL/System/ConsoleKeyInfo@cs/1305376/ConsoleKeyInfo@cs

            while (true)
            {
                for (int i = 0; i < MenuItem.Length; i++)
                {
                    if (MainMenuIndex == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(x, y + i);
                        Console.Write(MenuItem[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.SetCursorPosition(x, y + i);
                        Console.Write(MenuItem[i]);
                    }
                }

                InputKey = Console.ReadKey(true);
                if (InputKey.Key == ConsoleKey.Enter)
                    break;
                else if (InputKey.Key == ConsoleKey.UpArrow)
                {
                    MainMenuIndex--;
                    if (MainMenuIndex < 0)
                        MainMenuIndex = 0;
                }
                else if (InputKey.Key == ConsoleKey.DownArrow)
                {
                    MainMenuIndex++;
                    if (MainMenuIndex == MenuItem.Length)
                        MainMenuIndex = MenuItem.Length - 1;
                }
            }
        }

        // Note: 물 출력 메서드
        private static void showWaterHeight(int x, int y, int Amount)
        {
            int Height = Amount / 1000;
            // 지우는 부분
            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < 18; i++)
            {
                Console.SetCursorPosition(x, 2 + i);
                Console.Write("                    ");
            }

            Console.BackgroundColor = ConsoleColor.Blue;
            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(x, 19 - i);
                Console.Write("                    ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Note: 쌀 출력 메서드
        private static void showRiceHeight(int x, int y, int Amount)
        {
            int Height = Amount / 1000;
            // 지우는 부분
            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < 18; i++)
            {
                Console.SetCursorPosition(x, 2 + i);
                Console.Write("                    ");
            }

            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(x, 19 - i);
                Console.Write("⊙ ⊙ ⊙ ⊙ ⊙ ⊙ ⊙");
            }
        }

        private static void showPowerLine(bool power)
        {
            if (power)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(4, 17);
                Console.Write("──────");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(4, 17);
                Console.Write("──────");
            }
        }

        //...각종 상태 표시.
        private static void showRiceInfo(RiceCookerInfo Info)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(3, 25);
            if (Info.Power)
                Console.Write("전원 상태 : ON");
            else
                Console.Write("전원 상태 : OFF");

            Console.SetCursorPosition(3, 26);
            if (Info.CoverOpenClose)
                Console.Write("뚜껑 상태 : 열림");
            else
                Console.Write("뚜껑 상태 : 닫힘");

            Console.SetCursorPosition(3, 27);
            switch (Info.State)  //...☎Book.129p.if~else.
            {
                case CookerProcess.None:
                    Console.Write("밥솥 상태 : 대기 중  ");
                    break;
                case CookerProcess.Riceing:
                    Console.Write("밥솥 상태 : 밥 넣기  ");
                    break;
                case CookerProcess.Watering:
                    Console.Write("밥솥 상태 : 물 넣기  ");
                    break;
                case CookerProcess.Washing:
                    Console.Write("밥솥 상태 : 쌀 씻기  ");
                    break;
                case CookerProcess.Droping:
                    Console.Write("밥솥 상태 : 물 배수  ");
                    break;
                case CookerProcess.Cooking:
                    Console.Write("밥솥 상태 : 취사 중  ");
                    break;
                case CookerProcess.Completion:
                    Console.Write("밥솥 상태 : 취사 완료");
                    break;
                case CookerProcess.Keeping:
                    Console.Write("밥솥 상태 : 보온 중  ");
                    break;
            }

            Console.SetCursorPosition(3, 28);
            Console.Write("인원수 : {0}", Info.Number);
            Console.SetCursorPosition(3, 29);
            Console.Write("쌀 상태 : {0:f1} Kg", Info.Rice / 1000.0f);
            Console.SetCursorPosition(3, 30);
            Console.Write("물 상태 : {0:f1} 리터", Info.Water / 1000.0f);
        }

        // Note: 뚜껑 열기 닫기 출력 메서드
        private static void showCoverOpenOrClose(bool bOpen)
        {
            const int x = 16;
            if (bOpen)  //...☎Book.129p.if~else.
            {
                Console.SetCursorPosition(x, 3);
                Console.Write("┌┐");
                for (int i = 0; i < 6; i++)
                {
                    Console.SetCursorPosition(x, 4 + i);
                    Console.Write("││");
                }
                Console.SetCursorPosition(x, 10);
                Console.Write("└┘");
            }
            else
            {
                Console.SetCursorPosition(x, 9);
                Console.Write("┌──────────┐");
                Console.SetCursorPosition(x, 10);
                Console.Write("└──────────┘");
            }
        }

        // Note: 밥솥 출력 메서드
        private static void showRiceBox(int x, int y)
        {
            int height = 7;
            Console.SetCursorPosition(x, y);
            Console.Write("┌──────────┐");
            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("│          │");
            }

            Console.SetCursorPosition(x, y + height);
            Console.Write("└──────────┘");
            Console.SetCursorPosition(x + 10, y + 2);
            Console.Write("밥솥");
            Console.SetCursorPosition(x, y + 6);
            Console.Write("┤"); // 전원 부분
        }

        private static void showOutFrame()
        {
            {
                showRiceCookerBox(0, 0);
                showRiceOrWaterBox(48, 0);
                showRiceOrWaterBox(72, 0);
                showInfoOrMenuBox(0, 21);
                showInfoOrMenuBox(48, 21);
                Console.SetCursorPosition(17, 1);
                Console.Write("Smart 밥솥");
                Console.SetCursorPosition(58, 1);
                Console.Write("쌀통");
                Console.SetCursorPosition(82, 1);
                Console.Write("물통");
                Console.SetCursorPosition(18, 23);
                Console.Write("밥솥 정보");
                Console.SetCursorPosition(66, 23);
                Console.Write("(( 메뉴 ))");
            }
        }

        // Note: 밥솥 상태 정보 박스와 메뉴 박스 출력 메서드
        private static void showInfoOrMenuBox(int x, int y)
        {
            int height = 13;
            Console.SetCursorPosition(x, y);
            Console.Write("┌──────────────────────┐");
            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("│                      │");
            }

            Console.SetCursorPosition(x, y + height);
            Console.Write("└──────────────────────┘");
        }

        // Note: 쌀통과 물통 출력 메서드
        private static void showRiceOrWaterBox(int x, int y)
        {
            int height = 20;
            Console.SetCursorPosition(x, y);
            Console.Write("┌──────────────────────┐");
            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("│                                              │");
            }

            Console.SetCursorPosition(x, y + height);
            Console.Write("└──────────────────────┘");
        }

        // Note: 밥솥 출력 메서드
        private static void showRiceCookerBox(int x, int y)
        {
            int height = 20;
            Console.SetCursorPosition(x, y); //...x : column 위치, y: row 위치. https://www.geeksforgeeks.org/console-setcursorposition-method-in-c-sharp/
            Console.Write("┌──────────────────────┐");
            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("│                      │"); //...blank 2배 차지해서 줄임.
                Console.SetCursorPosition(x, y);
            }

            Console.SetCursorPosition(x, y + height);
            Console.Write("└──────────────────────┘");
        }
    }
}



/*
 * SmartRiceCooker00
 *     
    Console 프로그램으로 반복문의 사용, 전체적인 프로그램을 만들어 가는 구조적인 관점을 실습을 통해 경험해봅니다.

 */
