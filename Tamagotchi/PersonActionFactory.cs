using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    class PersonActionFactory
    {
        private bool init = true;
        private int lastAction = -1;

        public IPersonAction GetRandomAction()
        {
            if (init)
                return GetInitAction();

            return GenerateRangomAction();
        }

        public StayAction GetStayAction()
        {
            return new StayAction();
        }

        public MoveAction GetMoveAction()
        {
            return new MoveAction();
        }

        public EatAction GetEatAction()
        {
            return new EatAction();
        }

        public SleepAction GetSleepAction()
        {
            return new SleepAction();
        }

        private IPersonAction GenerateRangomAction()
        {
            var r = new Random();
            var n = r.Next(0, 5);
            while (n > 2 && lastAction == n)
            {
                n = r.Next(0, 5);
            }

            IPersonAction res;

            switch (n)
            {
                case 0:
                    res = GetStayAction();
                    break;
                case 1:
                case 2:
                    res = GetMoveAction();
                    break;
                case 3:
                    res = GetEatAction();
                    break;
                case 4:
                    res = GetSleepAction();
                    break;
                default:
                    throw new InvalidOperationException();
            }

            lastAction = n;
            return res;
        }

        private IPersonAction GetInitAction()
        {
            init = false;
            return new MoveAction();
        }
    }
}
