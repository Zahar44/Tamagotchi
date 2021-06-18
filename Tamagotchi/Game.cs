using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TamagotchiLibrary;

namespace Tamagotchi
{
    class Game
    {
        private const string SAVE_FILE_PATH = "./../../save.xml";
        private bool run;

        public Person Person { get; private set; }
        public int Speed { get; set; }

        public Size Size { get; set; }

        public Game() 
        {
            TryLoad();
        }

        public void Start()
        {
            SetBorder();
            Person.Death += OnDeath;
            var r = new Random();
            var actionFactory = new PersonActionFactory();
            run = true;

            Person.Heart.DrowHeart(Size.Width + 10, 3);
            while (run)
            {
                var point = GetActionPoint();
                var action = actionFactory.GetRandomAction();
                Person.SetAction(action);
                
                LogDebugInfo(action);
                Person.StartAction(point.X, point.Y, r.Next(Speed - Speed / 2, Speed + Speed / 2));
                Save();
            }

            Console.WriteLine("Start new game? (y/n)");
            if (Char.ToLower(Console.ReadKey().KeyChar) == 'y')
            {
                CreatePerson(GetDefaultLocation());
                Start();
            }
        }

        public void Stop()
        {
            run = false;
        }

        [Conditional("DEBUG")]
        private void LogDebugInfo(IPersonAction action)
        {
            Console.SetCursorPosition(0, Size.Height + 2);
            Console.WriteLine($"Proceed { action.GetType().Name.PadRight(Size.Width, ' ') }");
        }

        private void OnDeath(object sender, PersonDeathEventArg e)
        {
            Console.Clear();
            Console.WriteLine($"Game over! :(\nTotal live time: {e.LiveTime.TotalSeconds}");
            File.Delete(SAVE_FILE_PATH);
            Stop();
        }

        private Point GetActionPoint()
        {
            var r = new Random();
            var res = Person.Location;

            try
            {
                if (r.Next(0, Size.Width) < res.X)
                {
                    res.X = r.Next(1, res.X);
                    res.Y = r.Next(1, Size.Height - 8);
                }
                else
                {
                    res.X = r.Next(res.X + 14, Size.Width - 14);
                    res.Y = r.Next(1, Size.Height - 8);
                }
            }
            catch (Exception)
            {
                return GetActionPoint();
            }

            //Console.SetCursorPosition(res.X, res.Y);
            //Console.Write("*");

            return res;
        }

        private void SetBorder()
        {
            Console.Clear();

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

        public void TryLoad()
        {
            Point? location = null;
            int health = 0;
            DateTime exitTime = DateTime.Now;

            try
            {
                Load(out location, out health, out exitTime);
            }
            catch
            {
                Console.WriteLine($"Load file is corrupted.\nStart new game?");
                Console.ReadLine();
                Console.Clear();
            }

            health -= (int)(DateTime.Now - exitTime).TotalMinutes / 3;
            health = health > 0 ? health : 1;
            CreatePerson(location, health);
        }

        public void Save()
        {
            using (var xtw = new XmlTextWriter(File.Create(SAVE_FILE_PATH), Encoding.ASCII))
            {
                xtw.Formatting = Formatting.Indented;
                xtw.WriteStartDocument();
                xtw.WriteStartElement("Person");
                xtw.WriteElementString("Location", $"{Person.Location.X}:{Person.Location.Y}");
                xtw.WriteElementString("Health", $"{Person.Heart.Health}");
                xtw.WriteElementString("ExitTime", $"{DateTime.Now}");

                //xtw.WriteEndElement();
                //xtw.WriteEndElement();
                //xtw.WriteEndElement();
            }
        }

        private void Load(out Point? location, out int health, out DateTime exitTime)
        {
            using (var file = File.OpenRead(SAVE_FILE_PATH))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                XmlNode root = doc.DocumentElement;

                var locationString = root.SelectSingleNode("Location").InnerText.Split(':');
                var healthString = root.SelectSingleNode("Health").InnerText;
                var exitTimeString = root.SelectSingleNode("ExitTime").InnerText;

                location = new Point(Int32.Parse(locationString[0]), Int32.Parse(locationString[1]));
                health = Int32.Parse(healthString);
                exitTime = DateTime.Parse(exitTimeString);
            }
        }

        private void CreatePerson(Point? location, int health = 0)
        {
            var graphicFactory = new GraphicFactory();
            var graphic = graphicFactory.GetGraphicProvider();
            Point _location = location is null ? GetDefaultLocation() : (Point)location;

            if(health > 0)
                Person = new Person(graphic, _location, health);
            else
                Person = new Person(graphic, _location);
        }

        private Point GetDefaultLocation() => new Point(Size.Width / 2, Size.Height / 2);
    }
}
