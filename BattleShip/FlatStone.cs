using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class FlatStone : BindableBase
    {
        string _stone;
        public string Stone
        {
            get
            {
                return _stone;
            }
            set
            {
                _stone = value;
                RaisePropertyChanged("Stone");
            }
        }

        char _orientations;

        public char Orientations
        {
            get
            {
                return _orientations;
            }
            set
            {
                if(value == 'H' || value == 'V')
                {
                    _orientations = value;
                    RaisePropertyChanged("Orientation");
                }
            }
        }




        public FlatStone(string _stones)
        {
            this.Stone = _stones;
            this.Orientations = 'H';
        }
        public FlatStone()
        {

        }
    }
}
