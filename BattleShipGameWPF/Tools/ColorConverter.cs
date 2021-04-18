using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;


namespace BattleShipGameWPF
{
    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            char CCCC = (char)value;
            if(CCCC != '-')
                return new SolidColorBrush(Colors.Black);
            else
                return new SolidColorBrush(Colors.Blue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AttackConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(Colors.Beige);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AttackAsImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Get = (string)value;
            if(Get == "M")
            {
                return ".\\imgMiss.png";
            }
            else if(Get == "-")
            {
                return ".\\white.png";
            }
            else
            {
                return ".\\imgAttack.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AttackToShipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Val = (string)value;
            if (Val == "-")
                return ".\\ShipImages\\white.png";


            else if (Val == "S2H1")
                return ".\\ShipImages\\S2H1.png";
            else if (Val == "S2H2")
                return ".\\ShipImages\\S2H2.png";


            else if (Val == "S3H1")
                return ".\\ShipImages\\S3H1.png";
            else if (Val == "S3H2")
                return ".\\ShipImages\\S3H2.png";
            else if (Val == "S3H3")
                return ".\\ShipImages\\S3H3.png";


            else if (Val == "S4H1")
                return ".\\ShipImages\\S4H1.png";
            else if (Val == "S4H2")
                return ".\\ShipImages\\S4H2.png";
            else if (Val == "S4H3")
                return ".\\ShipImages\\S4H3.png";
            else if (Val == "S4H4")
                return ".\\ShipImages\\S4H4.png";


            else if (Val == "S5H1")
                return ".\\ShipImages\\S5H1.png";
            else if (Val == "S5H2")
                return ".\\ShipImages\\S5H2.png";
            else if (Val == "S5H3")
                return ".\\ShipImages\\S5H3.png";
            else if (Val == "S5H4")
                return ".\\ShipImages\\S5H4.png";
            else if (Val == "S5H5")
                return ".\\ShipImages\\S5H5.png";


            //Verticals

            else if(Val == "S2V1")
                return ".\\ShipImages\\S2V1.png";
            else if (Val == "S2V2")
                return ".\\ShipImages\\S2V2.png";


            else if (Val == "S3V1")
                return ".\\ShipImages\\S3V1.png";
            else if (Val == "S3V2")
                return ".\\ShipImages\\S3V2.png";
            else if (Val == "S3V3")
                return ".\\ShipImages\\S3V3.png";


            else if (Val == "S4V1")
                return ".\\ShipImages\\S4V1.png";
            else if (Val == "S4V2")
                return ".\\ShipImages\\S4V2.png";
            else if (Val == "S4V3")
                return ".\\ShipImages\\S4V3.png";
            else if (Val == "S4V4")
                return ".\\ShipImages\\S4V4.png";


            else if (Val == "S5V1")
                return ".\\ShipImages\\S5V1.png";
            else if (Val == "S5V2")
                return ".\\ShipImages\\S5V2.png";
            else if (Val == "S5V3")
                return ".\\ShipImages\\S5V3.png";
            else if (Val == "S5V4")
                return ".\\ShipImages\\S5V4.png";
            else if (Val == "S5V5")
                return ".\\ShipImages\\S5V5.png";


            else
            {
                return ".\\white.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
