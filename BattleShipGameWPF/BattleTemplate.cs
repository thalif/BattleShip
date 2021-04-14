using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BattleShipGameWPF
{
    class BattleTemplate : StyleSelector
    {
        public Style ImageTemplate
        { get; set; }
        public Style TextTemplate
        { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            FlatStone tempObj = item as FlatStone;
            if (tempObj.Stone == '-')
                return TextTemplate;
            else
                return ImageTemplate;
        }
    }
}
