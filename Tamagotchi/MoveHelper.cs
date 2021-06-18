using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    [Serializable]
    class MoveHelper
    {
        public Point Location { get; set; }

        public List<int>[] GetMovementList(int _x, int _y)
        {
            var _yl = _y < Location.Y ? GetMovementListUp(_y) : GetMovementListDown(_y);
            var _xl = _x < Location.X ? GetMovementListLeft(_x) : GetMovementListRight(_x);
            var res = Normalize(_xl, _yl);
            Location = new Point(_x, _y);

            return res;
        }

        private List<int>[] Normalize(List<int> _xl, List<int> _yl)
        {
            var maxLen = Math.Max(_xl.Count, _yl.Count);

            for (int i = _xl.Count; i < maxLen; i++)
            {
                if (_xl.Count == 0)
                    _xl.Add(0);
                else
                    _xl.Add(_xl.Last());
            }

            for (int i = _yl.Count; i < maxLen; i++)
            {
                if (_yl.Count == 0)
                    _yl.Add(0);
                else
                    _yl.Add(_yl.Last());
            }

            return new List<int>[] { _xl, _yl };
        }

        private List<int> GetMovementListUp(int _y)
        {
            var res = new List<int>();

            for (int i = Location.Y; i >= _y; i--)
            {
                res.Add(i);
            }

            return res;
        }

        private List<int> GetMovementListDown(int _y)
        {
            var res = new List<int>();

            for (int i = Location.Y; i <= _y; i++)
            {
                res.Add(i);
            }

            return res;
        }


        private List<int> GetMovementListRight(int _x)
        {
            var res = new List<int>();

            for (int i = Location.X; i <= _x; i++)
            {
                res.Add(i);
            }

            return res;
        }

        private List<int> GetMovementListLeft(int _x)
        {
            var res = new List<int>();
            
            for (int i = Location.X; i >= _x; i--)
            {
                res.Add(i);
            }

            return res;
        }
    }
}
