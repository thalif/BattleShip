using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BattleShip;
namespace BattleShipGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<FlatStone> FLATS = new ObservableCollection<FlatStone>();
        BattleShipModel bs = new BattleShipModel();

        ObservableCollection<string> ShipLists = new ObservableCollection<string>();
        string SelectedShip = string.Empty;
        string Orientaiton = string.Empty;
        bool FLAG = true;
        public MainWindow()
        {
            InitializeComponent();
            LoadLocalData();
            SeaFlat.ItemsSource = bs.Board;
        }

      
        public void LoadLocalData()
        {
            ShipLists.Add("CARRIER");
            ShipLists.Add("CRUISER");
            ShipLists.Add("SUBMARINE");
            ShipLists.Add("DESTROYER");
            ShipList.ItemsSource = ShipLists;
        }
        

        private void SeaFlat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(InternalValidation())
            {
                if (FLAG)
                {
                    FLAG = !FLAG;
                    if (bs.CheckShipSpace(SeaFlat.SelectedIndex, SelectedShip, Orientaiton))
                    {
                        bs.WriteBoard(SelectedShip, SeaFlat.SelectedIndex, Orientaiton);
                        ShipLists.Remove(SelectedShip);
                        SelectedShip = string.Empty;
                        Orientaiton = string.Empty;
                    }
                    else
                    {
                        FLAG = true;
                    }
                }
                else
                {
                    FLAG = !FLAG;
                }
            }
        }

        public bool InternalValidation()
        {
            if (SelectedShip == string.Empty)
                return false;
            else if (Orientaiton == string.Empty)
                return false;
            else
                return true;
        }

        private void ShipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedShip = ShipList.SelectedValue as string;
        }

        private void xVertival_Click(object sender, RoutedEventArgs e)
        {
            Orientaiton = "V";
        }

        private void xHorizontal_Click(object sender, RoutedEventArgs e)
        {
            Orientaiton = "H";
        }
    }
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
    }
}
