using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Encounters
    {
        static Random rand = new Random();


        //Encounters Generic


        //Encounters
        public static void FirstEncounter()
        {
            Console.WriteLine("적이 당신을 바라보고있습니다... ");
            Console.ReadKey();
            Combat(false, "도적", 1, 4);
        }
        public static void BasicFightEncounter()
        {
            Console.Clear();
            Console.WriteLine("코너를 돌자.. 당신은 적을 마주합니다..");
            Console.ReadKey();
            Combat(true, "", 0, 0);
        }
        public static void WizardEncounter()
        {
            Console.Clear();
            Console.WriteLine("문을 열어 어두운 방에 들어서자 당신은 거구의 남성과 마주합니다..");
            Console.ReadKey();
            Combat(false, "Dark Wizard", 4, 2);

        }


        //Encounter Tools
        public static void RandomEncounter()
        {
            switch(rand.Next(0, 1))
            {
                case 0:
                    BasicFightEncounter();
                    break;
                case 1:
                    WizardEncounter();
                    break;
            }
        }

        public static void Combat(bool random, string name, int power, int health)
        {
            string n = "";
            int p = 0;
            int h = 0;
            if (random)
            {
                n = GetName();
                p = Program.currentPlayer.GetPower();
                h = Program.currentPlayer.GetHealth();
            }
            else
            {
                n = name;
                p = power;
                h = health;
            }
            while (h > 0)
            {
                Console.Clear();
                Console.WriteLine (n);
                Console.WriteLine(p + "/" + h);
                Console.WriteLine("======================");
                Console.WriteLine("| (A)ttack  (D)efend |");
                Console.WriteLine("|  (R)un    (H)eal   |");
                Console.WriteLine("======================");
                Console.WriteLine(" Potins: " + Program.currentPlayer.potion + " Health: " + Program.currentPlayer.health);
                string input = Console.ReadLine();
                if (input.ToLower() == "a" || input.ToLower() == "attack")
                {
                    //Attack
                    Console.WriteLine(n + "(을)를 공격했습니다.");

                    int damage = p - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) + rand.Next(1,4);
                    Console.WriteLine("당신은 " + damage + " 만큼 데미지를 잃고 " + attack + "만큼 피해를 입혔습니다!");
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (input.ToLower() == "d" || input.ToLower() == "defend")
                {
                    //Defend
                    Console.WriteLine(n + "이 공격을 하려하자 당신은 방어 태세를 취합니다.");

                    int damage = (p/4) - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) + rand.Next(1, 4);
                    Console.WriteLine("당신은 " + damage + " 만큼 데미지를 잃고 " + attack + "만큼 피해를 입혔습니다!");
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (input.ToLower() == "r" || input.ToLower() == "run")
                {
                    //Run
                    if(rand.Next(0, 2) == 0)
                    {
                        Console.WriteLine("당신은 "+n+"으로부터 도망치려다 붙잡혔습니다!");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;

                        Console.WriteLine("당신은 " + damage + "의 피해를 입고 붙잡혔습니다");
                    }
                    else
                    {
                        Console.WriteLine("당신은 " + n + "으로부터 도망쳤습니다!");
                    }
                }
                else if (input.ToLower() == "h" || input.ToLower() == "heal")
                {
                    //Heal
                    if(Program.currentPlayer.potion==0)
                    {
                        Console.WriteLine("당신은 배낭에서 포션을 꺼냈지만 빈 병인 것 같습니다.");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine(n+"이 당신에게"+damage+"의 피해를 입혔습니다!");
                    }
                    else
                    {
                        Console.WriteLine("당신은 배낭에서 포션을 꺼내 마십니다.");
                        int potionV = 5;
                        Console.WriteLine(potionV + "만큼 체력을 회복합니다.");
                        Program.currentPlayer.health += potionV;
                        Console.WriteLine("당신이 시간을 보낼때, "+n+"이 당신을 공격합니다.");
                        int damage = (p/2)- Program.currentPlayer.armorValue;
                        if(damage < 0)
                            damage = 0;
                        Console.WriteLine("당신은 " + damage + " 만큼의 피해를 입었습니다!");
                    }
                    Console.ReadKey();
                }
                if(Program.currentPlayer.health <= 0)
                {
                    //Death Code
                    Console.WriteLine(n + "가 일격을 가하자 당신은 쓰러졌습니다.");
                    Console.ReadKey();
                    System.Environment.Exit(0);
                }
                Console.ReadKey();
            }
            int c = rand.Next(10,50);
            Console.WriteLine("당신이 "+n+"으로부터의 승리에 취해있을 때, 적의 시체는 " +c+ " 골드로 변했다!");
            Program.currentPlayer.gold += c;
            Console.WriteLine("현재 플레이어의 골드: " + Program.currentPlayer.gold);
            Console.ReadKey();
        }

        public static string GetName()
        {
             switch(rand.Next(0, 4))
            {
                case 0:
                    return "Skeleton";
                    break;
                case 1:
                    return "Zombie";
                case 2:
                    return "human Cultist";
                case 3:
                    return "Grave Robber";
            }
            return "Human Rogue";
        }

    }
}
