using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;

namespace ConsoleApp1
{
    public class Program
    {
        public static Player currentPlayer = new Player();
        public static bool mainLoop = true;
        static void Main()
        {
            //이름 입력
            Console.WriteLine("스파르타 게임");
            Console.Write("이름 : ");
            currentPlayer.name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("당신의 이름은 " + currentPlayer.name + "입니다.");
            Console.ReadKey();
           
            ChooseClass();

            Start();
            
            Encounters.FirstEncounter();
            while (mainLoop)
            {
                Encounters.RandomEncounter();
            }

        }

        public static void Start()
        {
            do
            {
                Console.Clear ();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine("\n1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("0. 종료");

                Console.Write("\n원하시는 행동을 입력해주세요 : ");
                string input = Console.ReadLine();

                switch(input)
                {
                    case "1":
                        // 상태 보기
                        Status();
                        break;
                    case "2":
                        // 인벤토리
                        OpenInventory();
                        break;
                    case "3":
                        // 여기에 상점 로직을 구현합니다.
                        shop.LoadShop(currentPlayer);
                        Console.ReadKey();
                        break;
                    case "0":
                        Console.WriteLine("게임을 종료합니다. 안녕히 가세요!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 0에서 3까지의 숫자를 입력하세요.");
                        break;
                }
            } while (true);
        }

        public static void Status()
        {
            do
            {
                Console.Clear();

                // 초기 기본 값 설정 
                Console.WriteLine($"Lv. {currentPlayer.level}");
                Console.WriteLine($"{currentPlayer.name} ({currentPlayer.job})");
                Console.WriteLine($"공격력: {currentPlayer.damage}(+{currentPlayer.GetAttackBonus()})");
                Console.WriteLine($"방어: {currentPlayer.armorValue}(+{currentPlayer.GetDefenseBonus()})");
                Console.WriteLine($"스태미너: {currentPlayer.health}");
                Console.WriteLine($"골드: {currentPlayer.gold} G");

                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력하세요.\n>>");
                string input = Console.ReadLine();

                if (input == "0")
                    break;
            }while(true);
        }
        
        static ClassType ChooseClass()
        {
            Console.Clear();
            Console.WriteLine("직업을 선택하세요");
            Console.WriteLine("[1] 전사");
            Console.WriteLine("[2] 궁수");
            Console.WriteLine("[3] 법사");

            ClassType choice = ClassType.None;
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    currentPlayer.job = ClassType.전사;
                    break;
                case "2":
                    currentPlayer.job = ClassType.궁수;
                    break;
                case "3":
                    currentPlayer.job = ClassType.마법사;
                    break;
            }
            return choice;
        }

        static void OpenInventory()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력하세요.\n>>");
                string input = Console.ReadLine();

                if(input == "1")
                {
                    ManageEquipment();
                }
                else if(input == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }while(true);
        }

        static void ManageEquipment()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("[아이템 목록]");

                // 현재 플레이어의 인벤토리에 있는 아이템들을 표시합니다.
                foreach (int itemNumber in currentPlayer.GetInventory())
                {
                    string itemName = GetItemName(itemNumber);
                    string equippedStatus = currentPlayer.IsItemEquipped(itemNumber) ? "[E]" : "";

                    Console.WriteLine($"{itemNumber}. {equippedStatus}{itemName}");
                }

                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    break;
                }
                else if (int.TryParse(input, out int itemNumber) && itemNumber >= 1 && itemNumber <= 3)
                {
                    EquipOrUnequipItem(itemNumber);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                }

            } while (true);
        }
        public static string GetItemName(int itemNumber)
        {
            switch (itemNumber)
            {
                case 1:
                    return "무쇠갑옷      | 방어력 +5 | 무쇠로 만들어져 튼튼한 갑옷입니다.";
                case 2:
                    return "스파르타의 창 | 공격력 +7 | 스파르타의 전사들이 사용했다는 전설의 창입니다.";
                case 3:
                    return "낡은 검       | 공격력 +2 | 쉽게 볼 수 있는 낡은 검 입니다.";
                default:
                    return "";
            }
        }

        static void EquipOrUnequipItem(int itemNumber)
        {
            if (!currentPlayer.IsItemEquipped(itemNumber))
            {
                currentPlayer.EquipItem(itemNumber);
                Console.WriteLine($"[{itemNumber}] 아이템이 장착되었습니다.");
            }
            else
            {
                currentPlayer.UnequipItem(itemNumber);
                Console.WriteLine($"[{itemNumber}] 아이템이 장착 해제되었습니다.");
            }
            UpdateStatus();
            Console.ReadKey();
        }

        static void UpdateStatus()
        {
            do
            {
                Console.Clear();
                Console.WriteLine($"Lv. {currentPlayer.level}");
                Console.WriteLine($"{currentPlayer.name} ({currentPlayer.job})");
                Console.WriteLine($"공격력: {currentPlayer.damage}(+{currentPlayer.GetAttackBonus()})");
                Console.WriteLine($"방어: {currentPlayer.armorValue}(+{currentPlayer.GetDefenseBonus()})");
                Console.WriteLine($"스태미너: {currentPlayer.health}");
                Console.WriteLine($"골드: {currentPlayer.gold} G");
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력하세요.\n>>");
                string input = Console.ReadLine();

                if (input == "0")
                    break;
            }while(true);
        }
    }
}
