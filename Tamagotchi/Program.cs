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
            var game = new Game { Speed = 25, Size = new Size(80, 24) };

            game.Start();
        }
    }
}
