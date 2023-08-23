using System.Reflection.PortableExecutable;

namespace SingleGame
{
    internal class Program
    {
        private static Character player;
        private static Item item;
        



        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
            OpenStatus();
            OpenInventory();
            EnterToDungeon();
            ManageEquipment();
        }

        static void GameDataSetting() // 초기 기본값 세팅
        {
            player = new Character($"Son", "전사", 1, 10, 5, 100, 1000);

            item = new Item($"철검","창","도끼", "가죽갑옷","판금갑옷","투구" , 10, 20,30,10,20,20);
        }
        //초기화면
        static void DisplayGameIntro() // 게임 인트로
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1. 상태창 확인\n");
            Console.WriteLine("2. 인벤토리 확인\n");
            Console.WriteLine("3. 던전입장\n");
            Console.WriteLine("4. 휴식하기\n");
            Console.ResetColor();
            Console.WriteLine("원하시는 행동을 입력해 주세요.");
            Console.WriteLine(" ");

            int input = CheckValidInput(1, 4);
            switch (input)
            {
                case 1:
                    OpenStatus(); // 상태창 열기
                    break;
                case 2:
                    OpenInventory(); // 인벤토리 열기
                    break;
                case 3:
                    EnterToDungeon(); // 던전 입장하기
                    break;
                case 4:
                    TakeARest(); //휴식하기
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }

        }

        static void OpenStatus()  // 상태창 열기
        {
            Console.Clear();

            
            int totalOffensePower = player.OffensePower + (player.EquippedWeapon?.OffensePower1 ?? 0); // 총 공격력(플레이어 기본 공격력 + 플레이어 무기 장착 공격력)
            int totalDefensePower = player.DefensePower + (player.EquippedArmor?.DefensePower1 ?? 0); // 총 방어력(플레이어 기본 방어력 + 플레이어 방어구 장착 방어력)
            int AddOffensePower = player.EquippedWeapon?.OffensePower1 ?? 0; // 플레이어 무기의 추가공격력
            int AddDefensePower = player.EquippedArmor?.DefensePower1 ?? 0; // 플레이어 방어구의 추가 방어력

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태창 확인\n");
            Console.WriteLine("캐릭터의 정보를 표시합니다.\n");
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})\n");
            Console.WriteLine($"공격력 : {player.OffensePower} + ({player.AddOffensePower})\n");
            Console.WriteLine($"방어력 : {player.DefensePower} + ({player.AddDefensePower})\n");
            Console.WriteLine($"체력 : {player.HealthPoint}\n");
            Console.WriteLine($"Gold : {player.Gold} G\n");
            Console.WriteLine("0. 나가기");
            Console.ResetColor();

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }
        static void OpenInventory() // 인벤토리 열기
        {
            Console.Clear();

            string equippedWeaponLabel = player.EquippedWeapon != null ? "[E] " : "[U]"; // 무기 장착 시 [E], 무기 해제 시 [U]표시
            string equippedArmorLabel = player.EquippedArmor != null ? "[E] " : "[U]"; //  방어구 장착 시 [E], 방어구 해제 시 [U]표시
            const int NameAlignment = -9;
            

            Console.WriteLine("인벤토리\n");
            Console.WriteLine("보유중인 아이템을 관리합니다.\n");
            Console.WriteLine("[아이템 목록]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"1. {equippedWeaponLabel,NameAlignment} 철검     | 공격력 + {item.OffensePower1} | 기본적으로 제공되는 무기입니다.\n");
            Console.WriteLine($"2. {equippedWeaponLabel,NameAlignment} 창       | 공격력 + {item.OffensePower2} | 기본적으로 제공되는 무기입니다.\n");
            Console.WriteLine($"3. {equippedWeaponLabel,NameAlignment} 도끼     | 공격력 + {item.OffensePower3} | 기본적으로 제공되는 무기입니다.\n");
            Console.WriteLine($"4. {equippedArmorLabel,NameAlignment} 가죽갑옷 | 방어력 + {item.DefensePower1} | 기본적으로 제공되는 방어구입니다.\n");
            Console.WriteLine($"5. {equippedArmorLabel,NameAlignment} 판금갑옷 | 방어력 + {item.DefensePower2} | 기본적으로 제공되는 방어구입니다.\n");
            Console.WriteLine($"6. {equippedArmorLabel,NameAlignment} 투구     | 방어력 + {item.DefensePower3} | 기본적으로 제공되는 방어구입니다.\n");
            Console.ResetColor();
            Console.WriteLine(" ");
            Console.WriteLine("1. 장착관리\n");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine(" ");
            Console.WriteLine("원하시는 행동을 입력해주세요");
           
            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 1:
                    ManageEquipment();
                    break;
                case 0 :
                    DisplayGameIntro();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;

            }
        }
        // 던전 입구
        static void EnterToDungeon()
        {
          Console.Clear ();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("던전입장");
            Console.ResetColor();
            Console.WriteLine("입장할 던전 난이도를 선택하세요.");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. 쉬운 던전     | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전     | 방어력 15 이상 권장");
            Console.WriteLine("3. 어려운 던전   | 방어력 25 이상 권장");
            Console.WriteLine("0. 나가기");
            Console.ResetColor();
            Console.WriteLine(" ");
            Console.WriteLine("원하시는 행동을 입력해주세요");

            int input = CheckValidInput (0, 3);
            switch (input)
            {
                case 0 :
                    DisplayGameIntro();
                    break;
                case 1 :
                    EnterToEasyMode();
                    break;
                case 2 :
                    EnterToNormalMode();
                    break;
                case 3 :
                    EnterToHardMode();
                    break;
            }

        }
        //이지 모드
        static void EnterToEasyMode()
        {           
            Console.Clear ();
            Console.WriteLine("쉬운 던전에 입장하셨습니다.");
            Console.WriteLine("시궁쥐가 등장하였습니다(권장 방어력 : 5)");
            PerformDungeon(Difficulty.Easy);
        }
        //노멀 모드
        static void EnterToNormalMode()
        {
            Console.Clear();
            Console.WriteLine("일반 던전에 입장하셨습니다.");
            Console.WriteLine("독사가 등장하였습니다(권장 방어력 :15)");
            PerformDungeon(Difficulty.Normal);

        }
        //하드 모드
        static void EnterToHardMode()
        {
            Console.Clear();
            Console.WriteLine("어려운 던전에 입장하셨습니다.");
            Console.WriteLine("성난 곰이 등장하였습니다(권장 방어력 : 25)");
            PerformDungeon(Difficulty.Hard);

        }
        // 난이도 설정
        enum Difficulty
        {
            Easy,
            Normal, 
            Hard    
        }
        // 던전 입장 후
        static void PerformDungeon(Difficulty difficulty)
        {
            Console.Clear();

            Random random = new Random();
            int recommendedDefense = GetRecommendedDefense(difficulty);

            Console.WriteLine($"던전을 수행하기 위해 권장 방어력: {recommendedDefense}");
            Console.WriteLine($"현재 방어력: {player.DefensePower}");

            // 플레이어 방어력이 권장 방어력보다 낮다면
            if (player.DefensePower < recommendedDefense)
            {
                
                if (random.NextDouble() < 0.4)
                {
                    Console.WriteLine("던전 실패! 체력이 감소합니다.");
                    int damage = player.HealthPoint / 2;
                    player.HealthPoint -= damage;
                    Console.WriteLine($"체력 감소: -{damage}");
                }
                else
                {
                    Console.WriteLine("던전을 성공적으로 통과했습니다!");
                }
            }
            else
            {
                int baseReward = GetBaseReward(difficulty);
                int additionalReward = GetAdditionalReward(player.OffensePower, difficulty);

                int totalReward = baseReward + additionalReward;

                Console.WriteLine($"던전 클리어! 보상을 획득합니다.");
                Console.WriteLine($"기본 보상: {baseReward} G");
                Console.WriteLine($"추가 보상: {additionalReward} G");
                Console.WriteLine($"총 보상: {totalReward} G");

                player.Gold += totalReward;

                // 체력 감소 계산
                int minHealthDecrease = 20;
                int maxHealthDecrease = 35;
                int healthDecreaseRange = maxHealthDecrease - minHealthDecrease + 1;
                int randomHealthDecrease = random.Next(minHealthDecrease, maxHealthDecrease + 1); // 랜덤 값 생성 수정
                int adjustedHealthDecrease = randomHealthDecrease + (player.DefensePower - recommendedDefense);
                player.HealthPoint -= adjustedHealthDecrease;

                Console.WriteLine($"체력 감소: -{adjustedHealthDecrease}");
            }
            // HP가 0이하로 떨어지면 게임 종료
            if (player.HealthPoint <= 0)
            {
                Console.WriteLine("체력이 모두 소진되어 게임 오버입니다.");
                DisplayGameIntro();
            }
            else
            {
                Console.WriteLine("던전 클리어! 계속 플레이하려면 아무 키나 누르세요.");
                Console.ReadKey();
                DisplayGameIntro();
            }
        }

        // 난이도별 권장 방어력
        static int GetRecommendedDefense(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return 5;
                case Difficulty.Normal:
                    return 15;
                case Difficulty.Hard:
                    return 25;
                default:
                    throw new ArgumentOutOfRangeException(nameof(difficulty));
            }
        }
        // 난이도별 보상
        static int GetBaseReward(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return 1000;
                case Difficulty.Normal:
                    return 1700;
                case Difficulty.Hard:
                    return 2500;
                default:
                    throw new ArgumentOutOfRangeException(nameof(difficulty));
            }
        }

        static int GetAdditionalReward(int offensePower, Difficulty difficulty)
        {
            Random random = new Random();
            double rewardPercentage = random.NextDouble() * offensePower * 2; // 0부터 offensePower * 2까지의 랜덤 값 생성

            switch (difficulty)
            {
                case Difficulty.Easy:
                    return (int)(rewardPercentage * 0.1);
                case Difficulty.Normal:
                    return (int)(rewardPercentage * 0.15);
                case Difficulty.Hard:
                    return (int)(rewardPercentage * 0.3);
                default:
                    throw new ArgumentOutOfRangeException(nameof(difficulty));
            }
        }
        static void TakeARest()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1.휴식하기\n");
            Console.WriteLine("0.나가기\n");
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine($"500 G를 내면 체력을 회복할 수 있습니다. (보유 골드: {player.Gold} G)");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    TakeARestConfirmation();
                    break;

            }
 
        }
        static void TakeARestConfirmation()
        {
            Console.Clear();

            Console.WriteLine("휴식을 취하시겠습니까? (Y/N)");
            string choice = Console.ReadLine();

            if (choice.ToUpper() == "Y")
            {
                if (player.Gold >= 500)
                {
                    player.Gold -= 500;
                    player.HealthPoint = Math.Min(player.HealthPoint + 50, 100);

                    Console.WriteLine("휴식을 취하여 체력이 회복되었습니다.");
                }
                else
                {
                    Console.WriteLine("골드가 부족하여 휴식을 취하지 못했습니다.");
                }
            }
            else if (choice.ToUpper() == "N")
            {
                Console.WriteLine("휴식을 취하지 않습니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Console.WriteLine("아무 키나 누르면 이전 화면으로 돌아갑니다.");
            Console.ReadKey();
            DisplayGameIntro();
        }

        static void ManageEquipment() // 장비창 관리하기
        {
            Console.Clear();

            // 무기 및 방어구 장착 여부에 따른 라벨 표시 [E]는 장착상태, [U]는 장착해제된 상태
            string equippedWeaponLabel = player.EquippedWeapon != null ? "[E] " : "[U]";
            string equippedArmorLabel = player.EquippedArmor != null ? "[E] " : "[U]";

            const int NameAlignment = 20;

            Console.WriteLine("장착관리");
            Console.WriteLine("장착할 아이템을 선택하세요.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{equippedWeaponLabel}{item.Sword,NameAlignment}                 | 공격력 + {item.OffensePower1}");
            Console.WriteLine($"{equippedWeaponLabel}{item.Spear,NameAlignment}                  | 공격력+  {item.OffensePower2}");
            Console.WriteLine($"{equippedWeaponLabel}{item.Ax,NameAlignment}                 | 공격력 + {item.OffensePower3}");
            Console.WriteLine($"{equippedArmorLabel}{item.LeatherArmor,NameAlignment}               | 방어력 + {item.DefensePower1}");
            Console.WriteLine($"{equippedArmorLabel}{item.PlateArmor,NameAlignment}               | 방어력 + {item.DefensePower2}");
            Console.WriteLine($"{equippedArmorLabel}{item.Helmet,NameAlignment}                 | 방어력 + {item.DefensePower3}");
            Console.ResetColor();
            Console.WriteLine(" ");

            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("1. 검 장착하기(1)\n");
            Console.WriteLine("2. 창 장착하기(2)\n");
            Console.WriteLine("3. 도끼 장착하기(3)\n");
            Console.WriteLine("4. 가죽갑옷 장착하기(4)\n");
            Console.WriteLine("5. 판금갑옷 장착하기(5)\n");
            Console.WriteLine("6. 투구 장착하기(6)\n");
            if (player.EquippedWeapon != null)
            {
                Console.WriteLine("7. 무기 해제하기(1,2,3)\n");
            }
            if (player.EquippedArmor != null)
            {
                Console.WriteLine("8. 방어구 해제하기(4,5,6)\n");
            }

            Console.Write("원하시는 행동을 입력해주세요");

            int input = CheckValidInput(0, 8);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    EquipOrUnequipWeapon(item.Sword, player.EquippedWeapon == null, 1); // 검을 장착하거나 해제함
                    break;
                case 2:
                    EquipOrUnequipWeapon(item.Spear, player.EquippedWeapon == null, 2); // 창을 장착하거나 해제함
                    break;
                case 3:
                    EquipOrUnequipWeapon(item.Ax, player.EquippedWeapon == null, 3); // 도끼를 장착하거나 해제함
                    break;
                case 4:
                    EquipOrUnequipArmor(item.LeatherArmor, player.EquippedArmor == null, 1); // 가죽갑옷을 장착하거나 해제함
                    
                    break;
                case 5:
                    EquipOrUnequipArmor(item.PlateArmor, player.EquippedArmor == null, 2); // 판금갑옷을 장착하거나 해제함
                    
                    break;
                case 6:
                    EquipOrUnequipArmor(item.Helmet, player.EquippedArmor == null, 3); // 투구를 장착하거나 해제함
                    
                    break;
                case 7:
                    EquipOrUnequipWeapon(null, false, 0); // 무기 해제
                    OpenInventory();
                    break;
                case 8:
                    EquipOrUnequipArmor(null, false, 0); // 방어구 해제
                    OpenInventory();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }
        static void EquipOrUnequipWeapon(string itemName, bool equip, int weaponNumber)
        {
            if (equip)
            {
                if (weaponNumber == 1)
                    player.EquippedWeapon = new Item(item.Sword, "", "", "", "", "", item.OffensePower1, 0, 0, 0, 0, 0);
                else if (weaponNumber == 2)
                    player.EquippedWeapon = new Item("", item.Spear, "", "", "", "", 0, item.OffensePower2, 0, 0, 0, 0);
                else if (weaponNumber == 3)
                    player.EquippedWeapon = new Item("", "", item.Ax, "", "", "", 0, 0, item.OffensePower3, 0, 0, 0);

                Console.WriteLine($"{itemName}를 장착했습니다.");
                player.AddOffensePower += player.EquippedWeapon.OffensePower1 +
                                          player.EquippedWeapon.OffensePower2 +
                                          player.EquippedWeapon.OffensePower3;
            }
            else
            {
                int removedOffensePower = 0;
                if (player.EquippedWeapon != null)
                {
                    if (weaponNumber == 1)
                        removedOffensePower = player.EquippedWeapon.OffensePower1;
                    else if (weaponNumber == 2)
                        removedOffensePower = player.EquippedWeapon.OffensePower2;
                    else if (weaponNumber == 3)
                        removedOffensePower = player.EquippedWeapon.OffensePower3;
                }

                player.EquippedWeapon = null;
                Console.WriteLine($"{itemName}를 해제했습니다.");
                player.AddOffensePower -= removedOffensePower; // 추가 공격력에서 해제한 공격력을 빼줍니다.
            }
            UpdatePlayerStats();
            OpenStatus(); // Status에 반영
        }
        static void EquipOrUnequipArmor(string itemName, bool equip, int armorNumber)
        {
            if (equip)
            {
                if (armorNumber == 1)
                    player.EquippedArmor = new Item("", "", "", item.LeatherArmor, "", "", 0, 0, 0, item.DefensePower1, 0, 0);
                else if (armorNumber == 2)
                    player.EquippedArmor = new Item("", "", "", "", item.PlateArmor, "", 0, 0, 0, 0, item.DefensePower2, 0);
                else if (armorNumber == 3)
                    player.EquippedArmor = new Item("", "", "", "", "", item.Helmet, 0, 0, 0, 0, 0, item.DefensePower3);

                Console.WriteLine($"{itemName}를 장착했습니다.");

                player.AddDefensePower += player.EquippedArmor.DefensePower1 +
                                          player.EquippedArmor.DefensePower2 +
                                          player.EquippedArmor.DefensePower3;
            }
            else
            {
                int removedDefensePower = 0;

                if (player.EquippedArmor != null)
                {
                    if (armorNumber == 1)
                        removedDefensePower = player.EquippedArmor.DefensePower1;
                    else if (armorNumber == 2)
                        removedDefensePower = player.EquippedArmor.DefensePower2;
                    else if (armorNumber == 3)
                        removedDefensePower = player.EquippedArmor.DefensePower3;
                }

                player.EquippedArmor = null;
                Console.WriteLine($"{itemName}를 해제했습니다.");
                player.AddDefensePower -= removedDefensePower; // 추가 방어력에서 해제한 방어력을 빼줍니다.
            }
            UpdatePlayerStats();
            OpenStatus(); // Status에 반영
        }
        static void UpdatePlayerStats()
        {
            int totalOffensePower = player.OffensePower + (player.EquippedWeapon?.OffensePower1 ?? 10) + 
                                                          (player.EquippedWeapon?.OffensePower2 ?? 20) +
                                                          (player.EquippedWeapon?.OffensePower3 ?? 30);
            int totalDefensePower = player.DefensePower + (player.EquippedArmor?.DefensePower1 ?? 10) +
                                                          (player.EquippedArmor?.DefensePower2 ?? 20) +
                                                          (player.EquippedArmor?.DefensePower3 ?? 20);
            int AddOffensePower = (player.EquippedWeapon?.OffensePower1 ?? 10) +
                                  (player.EquippedWeapon?.OffensePower2 ?? 20) +
                                  (player.EquippedWeapon?.OffensePower3 ?? 30);
            int AddDefensePower = (player.EquippedArmor?.DefensePower1 ?? 10) +
                                  (player.EquippedArmor?.DefensePower2 ?? 20) +
                                  (player.EquippedArmor?.DefensePower3 ?? 20);
            player.OffensePower = totalOffensePower;
            player.DefensePower = totalDefensePower;
        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        public class Character
        {
            public Character(string name,string job,int level, int offensePower, int defensePower, int healthPoint, int gold )
            {
                
                Name = name;
                Job = job;
                Level = level;
                OffensePower = offensePower;
                DefensePower = defensePower;
                HealthPoint = healthPoint;
                Gold = gold;
                EquippedWeapon = null;
                EquippedArmor = null;
            }

            public string Name { get; set; }
            public string Job { get; set; }
            public int Level { get; set; }
            public int OffensePower { get; set; }
            public int DefensePower { get; set; }
            public int HealthPoint { get; set; }
            public int Gold { get; set; }
            public Item? EquippedWeapon { get; set; }
            public Item? EquippedArmor { get; set; }
            
            public int AddOffensePower { get; set; } // 추가공격력

            public int AddDefensePower { get; set; } // 추가방어력

        }
        public class Item
        {
            
            public string Sword { get; set; }
            public string Spear { get; set; }
            public string Ax { get; set; }
            
           
            public string LeatherArmor { get;set; }
            public string PlateArmor { get; set; }
            public string Helmet { get; set; }
          

            public int OffensePower1 { get; set; }
            public int OffensePower2{ get; set; }
            public int OffensePower3 { get; set; }
            public int DefensePower1 { get; set;}
            public int DefensePower2 { get; set; }
            public int DefensePower3 { get; set; }
         
        
            

            public Item(string sword, string spear, string ax, string plateArmor, string leatherArmor, string helmet, int offensePower1,int offensPower2, int offensePower3, int defensePower1, int defensePower2, int defensePower3)
            {
                
                Sword = sword;
                Spear = spear;
                Ax = ax;

               
                LeatherArmor = leatherArmor;
                PlateArmor = plateArmor;
                Helmet = helmet;

                OffensePower1 = offensePower1;
                OffensePower2 = offensPower2;
                OffensePower3 = offensePower3;

                DefensePower1 = defensePower1;
                DefensePower2 = defensePower2;
                DefensePower3 = defensePower3;  
            }
        }
    }
 
}