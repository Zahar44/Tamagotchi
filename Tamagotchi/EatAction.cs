using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TamagotchiLibrary;

namespace Tamagotchi
{
    class EatAction : IPersonAction
    {
        public event PersonActionEventHandler ActionEvent;

        public void Start(int _x, int _y, GraphicProvider graphic, MoveHelper moveHelper, int delay)
        {
            var response = MessageBox.Show("Feed?", "Tamagotchi", MessageBoxButtons.YesNo);

            if(response == DialogResult.Yes)
            {
                var eat = graphic.GetFood();
                eat.Drow(_x, _y, 0);

                var move = new MoveAction();
                move.Start(_x, _y, graphic, moveHelper, delay);
            }

            ActionEvent?.Invoke(this, new PersonActionEventArg 
            { 
                RequestState = response == DialogResult.No ? PersonActionFeedback.Bad : PersonActionFeedback.Good 
            });
        }
    }
}
