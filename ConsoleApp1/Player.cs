using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp1
{
    public class Player
    {
        Random rand;


        public string name;
        public ClassType job;
        public int level = 1;
        public int gold = 1500;
        public int health = 100;
        public int damage = 10;
        public int armorValue = 5;
        public int potion = 5;
        public int weaponValue = 1;

        public int mods = 0;

        private List<int> equippedItems = new List<int>();
        
        private List<int> purchasedItems = new List<int>(); // 구매한 아이템 리스트
        private List<int> inventory = new List<int> { 1, 2, 3 }; // 기본 아이템이 포함된 인벤토리 항목 리스트
        // 주어진 번호의 아이템을 장착
        public void EquipItem(int itemNumber)
        {
            if (!equippedItems.Contains(itemNumber))
            {
                equippedItems.Add(itemNumber);
                // 아이템이 장착될 때마다 스탯 업데이트
                UpdatePlayerStats();
            }
        }
        // 주어진 번호의 아이템을 해제
        public void UnequipItem(int itemNumber)
        {
            if (equippedItems.Contains(itemNumber))
            {
                equippedItems.Remove(itemNumber);
                // 아이템이 장착 해제될 때마다 스탯 업데이트
                UpdatePlayerStats();
            }
        }

        // 아이템 장착 여부 확인
        public bool IsItemEquipped(int itemNumber)
        {
            return equippedItems.Contains(itemNumber);
        }

        // 장착된 아이템을 기반으로 플레이어 스탯을 업데이트
        private void UpdatePlayerStats()
        {
            // 기본 스탯 설정
            health = 100;
            damage = 10;
            armorValue = 5; // 기본 방어력 값

            // 장착된 아이템을 반복하면서 스탯 업데이트
            foreach (int itemNumber in equippedItems)
            {
                switch (itemNumber)
                {
                    case 1:
                        // 무쇠갑옷
                        armorValue += 5;
                        break;
                    case 2:
                        // 스파르타의 창
                        damage += 7;
                        break;
                    case 3:
                        damage += 2; //낡은 검
                        break;
                }
            }
        }
        public int GetAttackBonus()
        {
            int bonus = 0;
            foreach (int itemNumber in equippedItems)
            {
                switch (itemNumber)
                {
                    case 2:
                        bonus += 7; // 스파르타의 창 추가 공격력
                        break;
                    case 3:
                        bonus += 2; // 낡은 검 추가 공격력
                        break;
                        // 다른 아이템에 대한 추가 case를 필요에 따라 추가하세요.
                }
            }
            return bonus;
        }

        public int GetDefenseBonus()
        {
            int bonus = 0;
            foreach (int itemNumber in equippedItems)
            {
                switch (itemNumber)
                {
                    case 1:
                        bonus += 5; // 무쇠갑옷 추가 방어력
                        break;
                        // 다른 아이템에 대한 추가 case를 필요에 따라 추가하세요.
                }
            }
            return bonus;
        }

        public List<int> GetInventory()
        {
            return inventory;
        }

        // 플레이어가 특정 아이템을 이미 구매했는지 확인
        public bool HasPurchasedItem(int itemNumber)
        {
            return purchasedItems.Contains(itemNumber);
        }
        // 아이템을 구매하고 구매한 아이템 목록에 추가
        public void PurchaseItem(int itemNumber, int itemPrice)
        {
            gold -= itemPrice;
            purchasedItems.Add(itemNumber);
        }

        public int GetHealth()
        {
            int upper = (2 * mods + 5);
            int lower = (mods + 2);
            return rand.Next(lower, upper);
        }
        public int GetPower()
        {
            int upper = (2 * mods + 2);
            int lower = (mods + 1);
            return rand.Next(lower, upper);
        }


    }
    public enum ClassType
    {
        None = 0,
        전사 = 1,
        궁수 = 2,
        마법사 = 3
    }

    
}
