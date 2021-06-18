using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TamagotchiLibrary;

namespace Tamagotchi
{
    [Serializable]
    class Person
    {
        private readonly MoveHelper moveHelper;
        private IPersonAction action;

        public DateTime CreatedTime { get; private set; }
        public GraphicProvider Graphic { get; set; }
        public Heart Heart { get; set; }
        public Point Location => moveHelper.Location;

        public event PersonDeathEventHandler Death;

        public Person(GraphicProvider graphic, Point location)
        {
            Graphic = graphic;
            moveHelper = new MoveHelper { Location = location };
            CreatedTime = DateTime.Now;
            Heart = new Heart(Graphic.GetHeart());
        }

        public Person(GraphicProvider graphic, Point location, int health)
        {
            Graphic = graphic;
            moveHelper = new MoveHelper { Location = location };
            CreatedTime = DateTime.Now;
            Heart = new Heart(Graphic.GetHeart(), health);
        }

        public void SetAction(IPersonAction personAction)
        {
            if(!(action is null))
            {
                action.ActionEvent -= OnAction;
            }

            action = personAction;
            action.ActionEvent += OnAction;
        }

        public void StartAction(int _x, int _y, int delay)
        {
            action.Start(_x, _y, Graphic, moveHelper, delay);
        }
        
        private void OnAction(object sender, PersonActionEventArg e)
        {
            switch (e.RequestState)
            {
                case PersonActionFeedback.Bad:
                    Heart.ReduceHealth();
                    RequireAlive();
                    break;
                case PersonActionFeedback.Good:
                    Heart.IncreaseHealth();
                    break;
                case PersonActionFeedback.None:
                default:
                    break;
            }
        }

        private void RequireAlive()
        {
            if (Heart.Health <= 0)
            {
                Graphic.GetDeathAnimation().Drow(moveHelper.Location.X, moveHelper.Location.Y, 100);
                Death?.Invoke(this, new PersonDeathEventArg { LiveTime = DateTime.Now - CreatedTime });
            }
        }
    }
}
