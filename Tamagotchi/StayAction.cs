using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLibrary;

namespace Tamagotchi
{
    class StayAction : IPersonAction
    {
        public event PersonActionEventHandler ActionEvent;

        public void Start(int _x, int _y, GraphicProvider graphic, MoveHelper moveHelper, int delay)
        {
            var drower = graphic.GetStayAnimation();
            //var movementList = moveHelper.GetMovementList(_x, _y);
            drower.Drow(moveHelper.Location.X, moveHelper.Location.Y, delay * 10);

            ActionEvent?.Invoke(this, new PersonActionEventArg { RequestState = PersonActionFeedback.None });
        }
    }
}
