using System;
using System.Threading;

namespace SmartCoffeeCooker00
{
    enum CoffeeProcess { None, CupPrepared, SteamingPipe, Roasting, Droping, Watering, Completion }; //...☎0010.SFCS0010_Enum.

    struct CoffeeMachineInfo  //...☎0020.SFCS0030_Struct.
    {
        public bool Power;
        public bool Ordered; // 주문여부
        public CoffeeProcess State;
        public int Coin; // 동전
        public int Price;  // 커피 한잔 가격.
        public int Bean;   // 커피원두 양 g 기준, 출력할 때 환산
        public int Water;  // 물의 양 미리 리터 기준, 출력할 때 환산

        public CoffeeMachineInfo(int _Coin)
        {
            Power = false;
            Ordered = false;
            State = CoffeeProcess.None;
            Coin = _Coin;
            Price = 200;
            Bean = 0;
            Water = 0;
        }
    }

    class ProgramCoffee
    {
        public static int MainMenuIndex = 0; // 메인 메뉴 선택 인덱스

        public static string[] MenuItem = { "   전원   ", " 동전투입 ", "   주문   ", " 주문취소 " };  //...☎0030.SFCS0050_Array.
        public static string[] ProcessMsg = new string[7] { "대기", "컵준비", "스팀청소", "원두볶기", "커피내리기", "물붓기", "완료" };

        static void Main(string[] args)
        {
            Console.SetWindowSize(99, 42); 
            CoffeeMachineInfo RCInfo = new CoffeeMachineInfo(0);

            while (true) //...☎0040.SFCS0070_Loop.173p.
            {
                showOutFrame();
                showCoffeeMachineInfo(RCInfo);
                showCoffeeProcessInfo(RCInfo, 0);
                showMenu(68, 7, MenuItem); //...사용자 입력을 받으면 화면이 반짝이지 않으므로 showMenu부터 만들것.

                switch (MainMenuIndex)
                {
                    case 0: // 전원
                        RCInfo.Power = !RCInfo.Power; //...버튼 하나로 On/Off 토글.
                        break;

                    case 1: // 동전투입.
                        if (!RCInfo.Power)
                        {
                            showMessageBox(51, 27, "전원이 꺼져 있습니다");
                            //...KeyPressed 될때까지 화면대기, true → 글자숨김, Console Class in C#. https://www.geeksforgeeks.org/console-class-in-c-sharp/
                            Console.ReadKey(true);
                            break;
                        }

                        if (RCInfo.State != CoffeeProcess.None)
                        {
                            showMessageBox(51, 27, " 커피를 만드는 중 일때는 동전을 넣을 수 없습니다");
                            Console.ReadKey(true);
                        }
                        else
                        {
                            showMessageBox(51, 27, " 동전 금액을 입력해주세요 : ");
                            try  //...☎0050.SFCS0080_Exception.
                            {
                                RCInfo.Coin = int.Parse(Console.ReadLine());

                                if (RCInfo.Coin > 500)
                                {
                                    RCInfo.Coin = 0;
                                    showMessageBox(51, 27, "500원 초과, 동전투입을 다시 눌러주세요. ");
                                    //RCInfo.Coin = int.Parse(Console.ReadLine());
                                    Console.ReadKey(true);
                                    break;
                                }
                                else if (RCInfo.Coin < RCInfo.Price)
                                {
                                    RCInfo.Coin = 0;
                                    showMessageBox(51, 27, "금액 부족, 동전투입을 다시 눌러주세요. ");
                                    //RCInfo.Coin = int.Parse(Console.ReadLine());
                                    Console.ReadKey(true);
                                    break;
                                }
                            }
                            catch (Exception e)
                            {
                                RCInfo.Coin = 0;
                            }
                        }
                        break;

                    case 2: // 주문

                        if (!RCInfo.Power)
                        {
                            showMessageBox(51, 27, "전원이 꺼져 있습니다");
                            
                            Console.ReadKey(true);
                            break;
                        }

                        if (RCInfo.State != CoffeeProcess.None)
                        {
                            showMessageBox(51, 27, " 커피를 만드는 중 일때는 동전을 넣을 수 없습니다");
                            Console.ReadKey(true);
                        }
                        else
                        {
                            if(RCInfo.Coin == 0)
                            {
                                showMessageBox(51, 27, " 동전투입을 눌러주세요.");
                                Console.ReadKey(true);
                                break;
                            }

                            try  //...☎0050.SFCS0080_Exception.
                            {
                                RCInfo.Coin = int.Parse(Console.ReadLine());

                                if (RCInfo.Coin > 500)
                                {
                                    showMessageBox(51, 27, "500원 초과, 동전투입을 다시 눌러주세요. ");
                                    //RCInfo.Coin = int.Parse(Console.ReadLine());
                                    Console.ReadKey(true);
                                    break;
                                }
                                else if (RCInfo.Coin < RCInfo.Price)
                                {
                                    showMessageBox(51, 27, "금액 부족, 동전투입을 다시 눌러주세요. ");
                                    //RCInfo.Coin = int.Parse(Console.ReadLine());
                                    Console.ReadKey(true);
                                    break;
                                }
                            }
                            catch (Exception e)
                            {
                                RCInfo.Coin = 0;
                            }
                        }

                        //// Note: 대기→ 컵준비→ 스팀청소→ 원두볶기→ 커피내리기→ 물붓기→ 완료 
                        showCoffeeProcessInfo(RCInfo, 800);
                        break;


                    case 3: // 주문취소
                        RCInfo.State = CoffeeProcess.None;
                        RCInfo.Coin = 0;
                        break;

                }

            }

        }

        private static void showCoffeeProcessInfo(CoffeeMachineInfo Info, int t)
        {

            int x = 16;
            int y = 7;
            string processSymbol = "";

            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x, y - 2);
            Console.Write("♣   커피공정표시   ♣");


            Console.SetCursorPosition(x, y);
            foreach (var coffeeProcess in Enum.GetValues(typeof(CoffeeProcess)))  //...☎0010.107p.
            {

                if (t > 0)
                {
                    processSymbol = "♥";

                    switch (ProcessMsg[(int)coffeeProcess])  //...☎Book.129p.if~else.
                    {
                        case "대기":
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "컵준비":
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case "스팀청소":
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case "원두볶기":
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "커피내리기":
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case "물붓기":
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "완료":
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                    }
                }
                else
                {
                    processSymbol = "⊙";
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.SetCursorPosition(x, y);
                Console.WriteLine("{0} : {1}", processSymbol, ProcessMsg[(int)coffeeProcess]); //...☎0020.045p.Currency.
                y = y + 1;
                Thread.Sleep(t);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
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
            ConsoleKeyInfo InputKey; 

            Console.SetCursorPosition(x, y - 2);
            Console.Write(" ♣ 메뉴 ♣ ");

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

        //...각종 상태 표시.
        private static void showCoffeeMachineInfo(CoffeeMachineInfo Info)
        {
            int x = 63;
            int y = 15;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x, y - 2);
            Console.Write("  ♣  커피머신표시   ♣    ");
            Console.SetCursorPosition(x, y);
            if (Info.Power)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("전원 상태 : ON");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("전원 상태 : OFF");
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x, y + 1);
            Console.Write("투입금액 : {0}", Info.Coin);

            Console.SetCursorPosition(x, y + 2);
            switch (Info.State)  //...☎Book.129p.if~else.
            {
                case CoffeeProcess.None:
                    Console.Write("커피 상태 : 대기 중  ");
                    break;
                case CoffeeProcess.CupPrepared:
                    Console.Write("커피 상태 : 컵 준비  ");
                    break;
                case CoffeeProcess.SteamingPipe:
                    Console.Write("커피 상태 : 스팀 살균  ");
                    break;
                case CoffeeProcess.Roasting:
                    Console.Write("커피 상태 : 원두 볶기  ");
                    break;
                case CoffeeProcess.Droping:
                    Console.Write("커피 상태 : 커피 드랍  ");
                    break;
                case CoffeeProcess.Watering:
                    Console.Write("커피 상태 : 물 공급  ");
                    break;
                case CoffeeProcess.Completion:
                    Console.Write("커피 상태 : 커피 나왔습니다. 감사합니다.");
                    break;
            }

            Console.SetCursorPosition(x, y + 3);
            Console.Write("원두 양 : {0:f1} 그램", Info.Bean / 1000.0f);
            Console.SetCursorPosition(x, y + 4);
            Console.Write("물 양 : {0:f1} 리터", Info.Water / 1000.0f);
        }

        // Note: 커피컵 출력 메서드
        private static void showCoffeeCupBox(int x, int y)
        {
            int height = 7;
            Console.SetCursorPosition(x, y);
            Console.Write("    ┌──────────┐    ");
            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("    │          │    ");
            }

            Console.SetCursorPosition(x, y + height);
            Console.Write("    └──────────┘    ");
            Console.SetCursorPosition(x, y - 3);
            Console.Write("                    ");
            Console.SetCursorPosition(x, y - 2);
            Console.Write("       커피컵       ");
            Console.SetCursorPosition(x, y - 1);
            Console.Write("                    ");
            //Console.SetCursorPosition(x, y + 6);
            //Console.Write("┤"); // 전원 부분
        }

        private static void showOutFrame()
        {
            showLeftSideBox(0, 0);
            showRightSideBox(48, 0);
            showCoffeeCupBox(16, 25);

            Console.SetCursorPosition(35, 2);
            Console.Write(" ♣   행복다방 커피머신   ♣ ");
        }

        // Note: 커피머신 상태 정보 박스와 메뉴 박스 출력 메서드
        private static void showRightSideBox(int x, int y)
        {
            int height = 40;
            Console.SetCursorPosition(x, y);
            Console.Write("┌──────────────────────────────────--──────────┐");
            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                //Console.Write("│                      │");
                //Console.Write("│----------------------------------------------│");
                Console.Write("│                                              │");
            }

            Console.SetCursorPosition(x, y + height);
            Console.Write("└─────────────────────────────────--───────────┘");
        }

        private static void showLeftSideBox(int x, int y)
        {
            int height = 40;
            Console.SetCursorPosition(x, y); //...x : column 위치, y: row 위치. https://www.geeksforgeeks.org/console-setcursorposition-method-in-c-sharp/
            Console.Write("┌──────────────────────────────────────────────┐");
            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                //Console.Write("│                      │"); //...blank 2배 차지해서 줄임.
                Console.Write("│ ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒ │"); //...blank 2배 차지해서 줄임.
                Console.SetCursorPosition(x, y);
            }

            Console.SetCursorPosition(x, y + height);
            Console.Write("└──────────────────────────────────────────────┘");
        }
    }
}
