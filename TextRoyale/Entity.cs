using System;

namespace TextRoyale
{

    class Entity
    {
        private string name;
        private int hp;
        private int maxHP;
        private int dmgMin;
        private int dmgMax;

        public Entity(string name, int hp, int dmgMin, int dmgMax)
        {
            this.name = name;
            this.hp = hp;
            maxHP = hp;
            this.dmgMin = dmgMin;
            this.dmgMax = dmgMax;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool IsMaxHP()
        {
            return hp == maxHP;
        }

        public bool IsAlive()
        {
            return hp > 0;
        }

        public int RollDamage()
        {
            return Program.rand.Next(dmgMin, dmgMax + 1);
        }

        public void ReceiveDamage(int dmg)
        {
            if (dmg >= 0) { Console.WriteLine(name + " was hit with " + dmg + " DMG!"); }
            else { Console.WriteLine(name + " was healed for " + dmg + " HP!"); }
            hp -= dmg;
            hp = hp >= 0 ? hp : 0;
            hp = hp <= maxHP ? hp : maxHP;
        }
        public void PrintInfo()
        {
            string hpStatus = hp == 0 ? "DEAD" : hp + "/" + maxHP;
            string deadText = hp == 0 ? "(X_X)" : "";
            Console.WriteLine(deadText + "[Name: " + name + ", HP: " + hpStatus + ", DMG: " + dmgMin + "-" + dmgMax + "]" + deadText);
        }
    }
}
