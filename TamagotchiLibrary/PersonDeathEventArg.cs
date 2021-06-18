using System;
using System.Collections.Generic;
using System.Text;

namespace TamagotchiLibrary
{
    public delegate void PersonDeathEventHandler(object sender, PersonDeathEventArg e);

    public class PersonDeathEventArg
    {
        public TimeSpan LiveTime { get; set; }

    }
}
