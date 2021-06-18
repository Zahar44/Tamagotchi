using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLibrary;

namespace Tamagotchi
{
    [Serializable]
    class Heart
    {
        private readonly int maxHealth = 3;
        private readonly Drower drower;
        private int health;

        private int x, y;

        public int Health => health;

        public Heart(Drower heartDrower)
        {
            health = maxHealth;
            drower = heartDrower;
        }

        public Heart(Drower heartDrower, int _health)
        {
            health = _health;
            drower = heartDrower;
        }

        public void DrowHeart(int _x, int _y)
        {
            x = _x; y = _y;
            Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            drower.Drow(x, y, drower.Content[maxHealth - health]);
            Console.ResetColor();
        }

        public void DrowHeart()
        {
            Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            drower.Drow(x, y, drower.Content[maxHealth - health]);
            Console.ResetColor();
        }

        public void ReduceHealth()
        {
            health--;

            if(health > 0)
            {
                DrowHeart();
            }
        }

        public void IncreaseHealth()
        {
            if (health >= maxHealth)
                return;

            health++;
            DrowHeart();
        }

        private void Clear()
        {
            var heartContent = drower.Content[maxHealth - maxHealth];
            drower.Clear(x, y, heartContent.Max(x => x.Length), heartContent.Count);
        }
    }
}
