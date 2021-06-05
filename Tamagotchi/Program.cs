using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLibrary;

namespace Tamagotchi
{
    class Program
    {
        static void Main(string[] args)
        {
            var graphicFactory = new GraphicFactory();
            var graphic = graphicFactory.GetGraphicProvider();
            var gameSize = new Size(60, 20);
            var person = new Person(gameSize.Width / 2, gameSize.Height / 2) { Graphic = graphic };
            var game = new Game { Person = person, Speed = 50, Size = gameSize };

            game.Start();
        }
    }
}
