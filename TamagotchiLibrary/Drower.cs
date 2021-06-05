using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TamagotchiLibrary
{
    public class Drower
    {
        public List<List<List<String>>> Contents { get; private set; }

        public Drower(List<List<List<String>>> _content)
        {
            Contents = _content;
        }

        public void Drow(int _x, int _y, int delay)
        {
            Console.CursorVisible = false;
            foreach (var resource in Contents)
            {
                foreach (var content in resource)
                {
                    var len = content.Max(p => p.Length);
                    Clear(_x, _y, len, content.Count);
                    Drow(_x, _y, content);
                    Thread.Sleep(delay);
                }
            }
            Contents.Reverse();
            //Console.CursorVisible = true;
        }

        public void Drow(List<int> _x, List<int> _y, int delay)
        {
            Console.CursorVisible = false;
            int j = 0;
            foreach (var resource in Contents)
            {
                for (int i = 1; i < _x.Count; i++)
                {
                    var len = resource[j].Max(p => p.Length);
                    Clear(_x[i - 1], _y[i - 1], len, resource[j].Count);
                    Drow(_x[i], _y[i], resource[j]);
                    j++;
                    if (j > resource.Count - 1)
                    {
                        j = Contents.Count > 1 ? 1 : 0;
                        resource.Reverse();
                    };
                    Thread.Sleep(delay);
                }
            }
            //Console.CursorVisible = true;
        }

        public void Drow(int _x, int _y, List<string> content)
        {
            int x = _x, y = _y;

            foreach (var line in content)
            {
                Console.SetCursorPosition(x, y++);
                Console.WriteLine(line);
            }
        }

        public void Drow(int _x, int _y, List<List<string>> content)
        {
            //int x = _x, y = _y;

            foreach (var c in content)
            {
                Drow(_x, _y, c);
            }
        }

        public void Clear(int _x, int _y, int? _xLen, int? _yLen)
        {
            if (_xLen is null || _yLen is null)
                return;

            _x = _x > 0 ? _x : 0;

            for (int i = _y; i < _y + _yLen + 1; i++)
            {
                Console.SetCursorPosition(_x, i);
                Console.Write($"".PadRight((int)_xLen + 1)); 
            }
        }
    }
}
