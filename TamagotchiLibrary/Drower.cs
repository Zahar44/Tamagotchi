using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TamagotchiLibrary
{
    public class Drower
    {
        public List<List<String>> Content { get; set; }

        public Drower(List<List<String>> _content)
        {
            Content = _content;
        }

        public void Drow(int _x, int _y, int delay)
        {
            Console.CursorVisible = false;
            foreach (var content in Content)
            {
                var len = content.Max(p => p.Length);
                Clear(_x, _y, len, content.Count);
                Drow(_x, _y, content);
                Thread.Sleep(delay);
            }
            //Console.CursorVisible = true;
        }

        public void Drow(List<int> _x, List<int> _y, int delay)
        {
            Console.CursorVisible = false;
            int j = 0;
            for (int i = 1; i < _x.Count; i++)
            {
                var len = Content[j].Max(p => p.Length);
                Clear(_x[i - 1], _y[i - 1], len, Content[j].Count);
                Drow(_x[i], _y[i], Content[j]);
                j++;
                if (j > Content.Count - 1)
                {
                    j = Content.Count > 1 ? 1 : 0;
                    Content.Reverse();
                };
                Thread.Sleep(delay);
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
