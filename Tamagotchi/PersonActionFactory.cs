using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    class PersonActionFactory
    {
        public IPersonAction GetRandomAction()
        {
            var r = new Random();
            var n = r.Next(0, 2);
            if (n == 0)
                return new MoveAction();
            else
                return new StayAction();
        }
    }
}
