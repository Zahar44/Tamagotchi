using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLibrary;

namespace Tamagotchi
{
    interface IPersonAction
    {
        void Start(int _x, int _y, GraphicProvider graphic, MoveHelper moveHelper, int delay);

        event PersonActionEventHandler ActionEvent;
    }
}
