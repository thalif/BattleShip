using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class FlatStone : BindableBase
    {
        char _stone;
        public char Stone
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
        public FlatStone(char _stones)
        {
            this.Stone = _stones;
        }
        public FlatStone()
        {

        }
    }
}
