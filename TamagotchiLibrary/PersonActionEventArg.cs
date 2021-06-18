using System;
using System.Collections.Generic;
using System.Text;

namespace TamagotchiLibrary
{
    public delegate void PersonActionEventHandler(object sender, PersonActionEventArg e);

    public enum PersonActionFeedback
    {
        Bad = -1,
        None = 0,
        Good = 1
    }

    public class PersonActionEventArg : EventArgs 
    {
        public PersonActionFeedback RequestState { get; set; }
    }
}
