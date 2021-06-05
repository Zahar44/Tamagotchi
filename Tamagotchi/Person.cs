using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLibrary;

namespace Tamagotchi
{
    class Person
    {
        private readonly MoveHelper moveHelper;
        private IPersonAction action;
        public GraphicProvider Graphic { get; set; }

        public Person(int _x, int _y)
        {
            moveHelper = new MoveHelper { X = _x, Y = _y };
        }

        public void SetAction(IPersonAction personAction)
        {
            action = personAction;
        }

        public void StartAction(int _x, int _y, int delay)
        {
            action.Start(_x, _y, Graphic, moveHelper, delay);
        }

        public void DrowHeart(int _x, int _y)
        {
            var drover = Graphic.GetHeart();
            Console.ForegroundColor = ConsoleColor.Red;
            drover.Drow(_x, _y, drover.Contents[0][0]);
            Console.ResetColor();
        }
    }
}
