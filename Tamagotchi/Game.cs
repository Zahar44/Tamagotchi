using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLibrary;

namespace Tamagotchi
{
    class Game
    {
        public Person Person { get; set; }
        public int Speed { get; set; }

        public Size Size { get; set; }

        public Game()
        {
        }

        public void Start()
        {
            SetBorder();
            var r = new Random();
            var actionFactory = new PersonActionFactory();

            Person.DrowHeart(Size.Width + 10, 3);
            while (true)
            {
                int x = r.Next(1, Size.Width - 14), y = r.Next(1, Size.Height - 9);
                Person.SetAction(actionFactory.GetRandomAction());
                Person.StartAction(x, y, r.Next(Speed - Speed / 2, Speed + Speed / 2));
            }
        }

        private void SetBorder()
        {
            int w = Size.Width + 1, h = Size.Height + 1;
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("".PadRight(w, '═'));
            Console.SetCursorPosition(0, h);
            Console.WriteLine("".PadRight(w + 1, '═'));

            for (int i = 0; i < h; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine("║");
                Console.SetCursorPosition(w, i);
                Console.WriteLine("║");
            }

            Console.SetCursorPosition(0, 0); Console.Write("╔");
            Console.SetCursorPosition(0, h); Console.Write("╚");
            Console.SetCursorPosition(w, 0); Console.Write("╗");
            Console.SetCursorPosition(w, h); Console.Write("╝");
            Console.ResetColor();
        }
    }
}
