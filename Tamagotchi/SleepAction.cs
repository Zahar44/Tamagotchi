using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLibrary;

namespace Tamagotchi
{
    class SleepAction : IPersonAction
    {
        public event PersonActionEventHandler ActionEvent;

        public void Start(int _x, int _y, GraphicProvider graphic, MoveHelper moveHelper, int delay)
        {
            var sleep = graphic.GetSleepAnimation();
            var stay = graphic.GetStayAnimation();

            sleep.Drow(moveHelper.Location.X, moveHelper.Location.Y, delay * 15);
            sleep.Content.Reverse();
            sleep.Drow(moveHelper.Location.X, moveHelper.Location.Y, delay * 15);
            sleep.Content.Reverse();

            stay.Drow(moveHelper.Location.X, moveHelper.Location.Y, delay);

            ActionEvent?.Invoke(this, new PersonActionEventArg { RequestState = PersonActionFeedback.None });
        }
    }
}
