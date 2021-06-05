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
        public void Start(int _x, int _y, GraphicProvider graphic, MoveHelper moveHelper, int delay)
        {
            var drower = graphic.GetStayAnimation();
            //var movementList = moveHelper.GetMovementList(_x, _y);
            drower.Drow(moveHelper.X, moveHelper.Y, delay * 10);
        }
    }
}
