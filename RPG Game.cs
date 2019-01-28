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
            Hero Dash = new Hero("Jason", 3, 15);

            Ork Keith = new Ork("Ork Harvey", 1, 5);
            Ork Ben = new Ork("Ork Ben", 2, 8);

            Klingon Leeroy = new Klingon("Klingon Aaron", 3, 12);
            Klingon Seth = new Klingon("Klingon Josh", 5, 15);

            HarryPotter BlueDrag = new HarryPotter("Harry Potter gen.1", 7, 20, 2);
            HarryPotter RedDrag = new HarryPotter("Harry Potter gen.2", 8, 20, 3);
            HarryPotter BlackDrag = new HarryPotter("Harry Potter gen.3", 10, 25, 4);

            Story.BeforeOrk();
            Battle.WithOrk(Jason, Harvey);
            Battle.WithOrk(Jason, Ben);

            Dash.LevelUp();

            Story.BeforeKlingon();
            Battle.WithKlingon(Jason, Aaron);
            Battle.WithKlingon(Jason, Josh);

            Dash.LevelUp();

            Story.BeforeHarryPotter();
            Battle.WithHarryPotter(Jason, HarryPotter);
            Battle.WithHarryPotter(Jason, HarryPotter);
            Battle.WithHarryPotter(Jason, HarryPotter);

            Story.TheEnd();
        }
        public class Person
        {
            public string name;
            public int attack;
            public int health;

            public PLayer(string _name, int _attack, int _health)
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

            // Methods used in Battle ----------------------------------------

            public int Choice() // Produces heros decision
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

                    if (choice == 3) // Specials menu
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
                    SpinAttack(target);
                    Console.WriteLine("You used strobe light on your phone!");
                }

                if (decision == 5)
                {
                    DoubleSlash(target);
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

            // Battle methods -----------------------------------------

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
                    Console.WriteLine("Ork whacked you!");

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

            public void Slash(Hero target)
            {
                target.health -= attack + 5;
            }

            public void SheildSlam(Hero target)
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
                    Slash(target);
                    Console.WriteLine("Klingon slashed you!");
                }

                if (choice == 3)
                {
                    SheildSlam(target);
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

            public void FireBreath(Hero target)
            {
                target.health -= attack * 2;
            }

            public void Claw(Hero target)
            {
                target.health -= attack + 3;
            }

            public void Bite(Hero target)
            {
                target.health -= attack + 4;
            }

            public void HarryPotterTurn(int choice, Hero target)
            {
                if (choice == 1)
                {
                    NormAttack(target);
                    Console.WriteLine("Harry Potter spat on you!");
                }

                if (choice == 2)
                {
                    FireBreath(target);
                    Console.WriteLine("Harry Potter used molotov on you!");
                }

                if (choice == 3)
                {
                    Claw(target);
                    Console.WriteLine("Harry Potter shanked you!");
                }

                if (choice == 4)
                {
                    Bite(target);
                    Console.WriteLine("Harry Potter shot you!");
                }

                Console.ReadLine();
                Console.Clear();
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

            public static void WithBandit(Hero hero, Bandit bandit)
            {
                while (bandit.health > 0 && hero.health > 0)
                {
                    PrintTheStats(bandit, hero);

                    hero.YourTurn(hero.Choice(), bandit);

                    if (bandit.health > 0)
                    {
                        bandit.BanditTurn(bandit.EChoice(), hero);
                        IsHeroDead(hero);
                    }

                }

                Console.WriteLine("{0} was killed!", bandit.name);
                Console.ReadLine();
                Console.Clear();
            }

            public static void WithKlingon(Hero hero, Klingon Klingon)
            {
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

            public static void WithDragon(Hero hero, HarryPotter HarryPotter)
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
            public static void BeforeOrks()
            {
                Console.WriteLine("You are Jason, whos on his way to kill Harry Potter who is existing");
                Console.WriteLine("As you are on your way to Harrys house, you run into a couple of Orks.");
                Console.WriteLine("And they don't seem to friendly...");
                Console.ReadLine();
                Console.Clear();
            }

            public static void BeforeKlingon()
            {
                Console.WriteLine("The Orks weren't much match for you. Well Done! You continue on to the boat!");
                Console.WriteLine("However, a new movement has risen that wants to protect Harry Potter.");
                Console.WriteLine("Many people have joined this movement, including some Klingon.");
                Console.WriteLine("And uh oh, theres 2 of them that have found out about your quest.");
                Console.WriteLine("Maybe their friendly?");
                Console.ReadLine();
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