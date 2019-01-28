using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Hero Jason = new Hero("Jason", 3, 15);

            Ork Harvey = new Ork("Ork Harvey", 1, 5);
            Ork Ben = new Ork("Ork Ben", 2, 8);

            Klingon Aaron = new Klingon("Klingon Aaron", 3, 12);
            Klingon Josh = new Klingon("Klingon Josh", 5, 15);

            HarryPotter HarryPotter1 = new HarryPotter("Harry Potter gen.1", 7, 20, 2);
            HarryPotter HarryPotter2 = new HarryPotter("Harry Potter gen.2", 8, 20, 3);
            HarryPotter HarryPotter3 = new HarryPotter("Harry Potter gen.3", 10, 25, 4);

            Story.BeforeOrk();
            Battle.WithOrk(Jason, Harvey);
            Battle.WithOrk(Jason, Ben);

            Jason.LevelUp();

            Story.BeforeKlingon();
            Battle.WithKlingon(Jason, Aaron);
            Battle.WithKlingon(Jason, Josh);

            Jason.LevelUp();

            Story.BeforeHarryPotter();
            Battle.WithHarryPotter(Jason, HarryPotter1);
            Battle.WithHarryPotter(Jason, HarryPotter2);
            Battle.WithHarryPotter(Jason, HarryPotter3);

            Story.TheEnd();
        }
        public class Person
        {
            public string name;
            public int attack;
            public int health;

            public Person(string _name, int _attack, int _health)
            {
                name = _name;
                attack = _attack;
                health = _health;
            }

            public void PrintStats()
            {
                Console.WriteLine("{0} stats:", name);
                Console.WriteLine("");
                Console.WriteLine("Attack value is: {0}", attack);
                Console.WriteLine("Health value is: {0}", health);
            }

            public void NormAttack(Person target)
            {
                target.health -= attack;
            }
        }
        public class Hero : Person
        {
            public int healLvl = 7, maxHealth = 15;

            public Hero(string _name, int _attack, int _health)
                : base(_name, _attack, _health)
            {
            }

            public void LevelUp()
            {
                Console.WriteLine("Level Up!");
                Console.WriteLine("Attack +3");
                Console.WriteLine("Max health +10");
                Console.WriteLine("Heal +5");
                Console.ReadLine();
                Console.Clear();

                attack += 3;
                maxHealth += 10;
                health = maxHealth;
                healLvl += 5;
            }

            // Special -------------------------------------------

            public void Heal()
            {
                health += healLvl;

                if (health > maxHealth)
                {
                    health = maxHealth;
                }
            }

            public void Strobelight(Enemy target)
            {
                target.health -= ((attack - 2) * 3);
            }

            public void Gun(Enemy target)
            {
                target.health -= attack * 2;
            }

            // Fighting decisions

            public int Choice() //Player choice
            {
                bool correctInput = true;
                int choice = 0, choice2;
                while (correctInput)
                {
                    Console.WriteLine("What shall we do Jason?");
                    Console.WriteLine("1. Attack");
                    Console.WriteLine("2. Heal");
                    Console.WriteLine("3. Special");

                    bool test = int.TryParse(Console.ReadLine(), out choice);
                    if (!test || choice > 3 || choice <= 0)
                    {
                        Console.WriteLine("Try again!");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }

                    if (choice == 3) // Special attacks menu
                    {
                        Console.WriteLine("Choose Special:");
                        Console.WriteLine("1. Strobe Light");
                        Console.WriteLine("2. Gun");
                        Console.WriteLine("3. <--- Go back");

                        bool test2 = int.TryParse(Console.ReadLine(), out choice2);
                        if (!test2 || choice2 > 3 || choice2 <= 0)
                        {
                            Console.WriteLine("Try again!");
                            Console.ReadLine();
                            Console.Clear();
                            continue;
                        }

                        if (choice2 == 1)
                        {
                            choice = 4;
                        }

                        if (choice2 == 2)
                        {
                            choice = 5;
                        }
                    }

                    if (choice == 1 || choice == 2 || choice == 4 || choice == 5)
                    {
                        break;
                    }
                }
                return choice;
            }

            public void YourTurn(int decision, Enemy target)
            {
                if (decision == 1)
                {
                    NormAttack(target);
                    Console.WriteLine("You spanked the enemy!");
                }

                if (decision == 2)
                {
                    Heal();
                    Console.WriteLine("You healed for {0} health!", healLvl);
                }

                if (decision == 4)
                {
                    Strobelight(target);
                    Console.WriteLine("You used strobe light on your phone!");
                }

                if (decision == 5)
                {
                    Gun(target);
                    Console.WriteLine("You used the Florida 'Stand your ground' law and used an AR-15!");
                }

            }
        }
        public class Enemy : Person
        {
            public int numOfAttack;

            public Enemy(string _name, int _attack, int _health)
                : base(_name, _attack, _health)
            {
            }

            // Enemy Battle methods -----------------------------------------

            public int EChoice()
            {
                int eChoice;
                Random ran = new Random();
                eChoice = ran.Next(1, numOfAttack + 1);
                return eChoice;
            }
        }

        public class Ork : Enemy
        {
            public Ork(string _name, int _attack, int _health)
                : base(_name, _attack, _health)
            {
                numOfAttack = 2;
            }

            //  Battle methods ---------------------------------------------

            public void Steal(Hero target)
            {
                target.health -= attack + 2;
            }

            public void OrkTurn(int choice, Hero target)
            {
                if (choice == 1)
                {
                    NormAttack(target);
                    Console.WriteLine("Ork whipped you!");

                }

                if (choice == 2)
                {
                    Steal(target);
                    Console.WriteLine("Ork stole from you!");

                }

                Console.ReadLine();
                Console.Clear();
            }
        }

        public class Klingon : Enemy
        {
            public Klingon(string _name, int _attack, int _health)
                : base(_name, _attack, _health)
            {
                numOfAttack = 3;
            }

            // Battle methods -------------------------------------------

            public void Kick(Hero target)
            {
                target.health -= attack + 5;
            }

            public void Face(Hero target)
            {
                target.health -= attack + 2;
            }

            public void KlingonTurn(int choice, Hero target)
            {
                if (choice == 1)
                {
                    NormAttack(target);
                    Console.WriteLine("Klingon punched you!");
                }

                if (choice == 2)
                {
                    Kick(target);
                    Console.WriteLine("Klingon kicked you!");
                }

                if (choice == 3)
                {
                    Face(target);
                    Console.WriteLine("Klingon slammed you with his face!");
                }

                Console.ReadLine();
                Console.Clear();
            }
        }

        public class HarryPotter : Enemy
        {
            public int armor;

            public HarryPotter(string _name, int _attack, int _health, int _armor)
                : base(_name, _attack, _health)
            {
                numOfAttack = 4;
                armor = _armor;
            }

            // Battle methods ------------------------------------------------------------

            public void Force(Hero target)
            {
                target.health -= attack * 2;
            }

            public void Lightsaber(Hero target)
            {
                target.health -= attack + 3;
            }

            public void Shank(Hero target)
            {
                target.health -= attack + 4;
            }

            public void HarryPotterTurn(int choice, Hero target)
            {
                if (choice == 1)
                {
                    NormAttack(target);
                    Console.WriteLine("Harry Potter used the force on you!");
                }
                if (choice == 2)
                {
                    Lightsaber(target);
                    Console.WriteLine("Harry Potter used a lightsaber on you!");
                }
                if (choice == 3)
                {
                    Shank(target);
                    Console.WriteLine("Harry Potter shanked you!");
                }
                if (choice == 4)
                {
                    Gun(target);
                    Console.WriteLine("Harry Potter shot you!");
                }
                Console.ReadLine();
                Console.Clear();
            }

            private void Gun(Hero target)
            {
                throw new NotImplementedException();
            }
        }
        class Battle
        {
            public static void IsHeroDead(Hero hero)
            {
                if (hero.health <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("I would say rest in piece");
                    Console.WriteLine("But you are in pieces!");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
            public static void PrintTheStats(Person person1, Person person2)
            {
                person1.PrintStats();
                Console.WriteLine("");
                person2.PrintStats();
                Console.WriteLine("");
            }
            public static void WithOrk(Hero hero, Ork Ork)
            {
                while (Ork.health > 0 && hero.health > 0)
                {
                    PrintTheStats(Ork, hero);

                    hero.YourTurn(hero.Choice(), Ork);

                    if (Ork.health > 0)
                    {
                        Ork.OrkTurn(Ork.EChoice(), hero);
                        IsHeroDead(hero);
                    }
                }
                Console.WriteLine("{0} was killed!", Ork.name);
                Console.ReadLine();
                Console.Clear();
            }
            public static void WithKlingon(Hero hero, Klingon Klingon)
            {
                //ensures that both the player and the enemy are alive
                while (Klingon.health > 0 && hero.health > 0)
                {
                    PrintTheStats(Klingon, hero);
                    hero.YourTurn(hero.Choice(), Klingon);
                    if (Klingon.health > 0)
                    {
                        Klingon.KlingonTurn(Klingon.EChoice(), hero);
                        IsHeroDead(hero);
                    }
                }
                Console.WriteLine("{0} was killed!", Klingon.name);
                Console.ReadLine();
                Console.Clear();
            }
            public static void WithHarryPotter(Hero hero, HarryPotter HarryPotter)
            {
                while (HarryPotter.health > 0 && hero.health > 0)
                {
                    PrintTheStats(HarryPotter, hero);
                    hero.YourTurn(hero.Choice(), HarryPotter);
                    if (HarryPotter.health > 0)
                    {
                        HarryPotter.HarryPotterTurn(HarryPotter.EChoice(), hero);
                        IsHeroDead(hero);
                    }
                }
                Console.WriteLine("{0} was killed !", HarryPotter.name);
                Console.ReadLine();
                Console.Clear();
            }
        }
        class Story
        {
            public static void BeforeOrk()
            {
                //Making the title look cool
//                Console.WriteLine("____________________  ________                                 ___.             ___ ___              .__                 ___ _______  ");
//                Console.WriteLine("\______   \______   \/  _____/     _________    _____   ____   \_ |__ ___.__.  /   |   \_____ _______|  |   ____ ___.__. \______   \  ");
//                Console.WriteLine(" |       _/|     ___/   \  ___    / ___\__  \  /     \_/ __ \   | __ <   |  | /    ~    \__  \\_  __ \  | _/ __ <   |  |  |       _/  ");
//                Console.WriteLine(" |    |   \|    |   \    \_\  \  / /_/  > __ \|  Y Y  \  ___/   | \_\ \___  | \    Y    // __ \|  | \/  |_\  ___/\___  |  |    |   \  ");
//                Console.WriteLine(" |____|_  /|____|    \______  /  \___  (____  /__|_|  /\___  >  |___  / ____|  \___|_  /(____  /__|  |____/\___  > ____|  |____|_  /  ");
//                Console.WriteLine("        \/                  \/  /_____/     \/      \/     \/       \/\/             \/      \/                \/\/              \/   ");
                //---------------------------------------------------------------------------------------------\\
                Console.WriteLine("You are Jason, a veteran warrior who was sent on behalf of the stormcloaks");
                Console.WriteLine("You are hunting Harry Potter for crimes against Skyrim and her people");
                Console.WriteLine("As you near Scotland, you are greeted my Orks,");
                Console.WriteLine("And they don't seem to friendly...");
                Console.ReadLine();
                Console.Clear();
            }
            public static void BeforeKlingon()
            {
                Console.WriteLine("The Orks are weak beings. Congrats! You continue on your mission");
                Console.WriteLine("However, a new movement has risen that wants to protect Harry Potter.");
                Console.WriteLine("Many people have joined this movement, including some Klingon.");
                Console.WriteLine("And uh oh, theres 2 of them that have found out about your quest.");
                Console.WriteLine("Maybe they're friendly?");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Nope.");
                Console.ReadLine();
                Console.Clear();
            }
            public static void BeforeHarryPotter()
            {
                Console.WriteLine("With the Orks defeated you continue on your journey!.");
                Console.WriteLine("After a while you make it to USS Enteprise...");
                Console.WriteLine("It's hot and little smokey in there.");
                Console.WriteLine("But the time has come to end Harry Potters life!");
                Console.ReadLine();
                Console.Clear();
            }
            public static void TheEnd()
            {
                Console.WriteLine("You killed Harry Potter and brought balance to the force!");
                Console.WriteLine("Congrats!");
                Console.ReadLine();
            }
        }
    }
}