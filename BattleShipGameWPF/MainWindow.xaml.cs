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
        ObservableCollection<FlatStone> Attacks = new ObservableCollection<FlatStone>();
        BattleShipModel bs = new BattleShipModel();



        ObservableCollection<string> ShipLists = new ObservableCollection<string>();
        string SelectedShip = string.Empty;
        string Orientaiton = string.Empty;
        bool FLAG = true;
        bool FLAGG = true;
        public MainWindow()
        {
            InitializeComponent();
            LoadLocalData();
            SeaFlat.ItemsSource = bs.Board;
            EmptySeaFlat.ItemsSource = Attacks;
            xHorizontal.IsEnabled = false;
        }

      
        public void LoadLocalData()
        {

            for(int i = 0; i < 100; i++)
            {
                Attacks.Add(new FlatStone('-'));
            }
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
            else
            {
                int index = SeaFlat.SelectedIndex;
                if (bs.Board[index].Stone != '-')
                {
                    MessageBox.Show("Remove Ship");
                }
                else
                {
                    MessageBox.Show("Do You want to remove");
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
            MakeShipPreview(SelectedShip);

            shipModel.Width = (int)shipModel.Width;
            shipModel.Height = (int)shipModel.Height;
            xVertival.IsEnabled = true;
            xHorizontal.IsEnabled = false;
            Orientaiton = "H";
        }

        void MakeShipPreview(string ship)
        {
            int width = 0;
            int height = 0;
            if (ship == "CARRIER")
            {
                width = 50 * 4;
                height = 30;
            }
            else if(ship == "CRUISER")
            {
                width = 30 * 4;
                height = 30;
            }
            else if (ship == "SUBMARINE")
            {
                width = 40 * 4;
                height = 30;
            }
            else if (ship == "DESTROYER")
            {
                width = 30 * 4;
                height = 30;
            }
            shipModel.Width = width;
            shipModel.Height = height;
        }

        private void xVertival_Click(object sender, RoutedEventArgs e)
        {
            Orientaiton = "V";
            xVertival.IsEnabled = false;
            xHorizontal.IsEnabled = true;

            int tempH = (int)shipModel.Width;
            int tempV = (int)shipModel.Height;

            shipModel.Width = tempV;
            shipModel.Height = tempH;

        }

        private void xHorizontal_Click(object sender, RoutedEventArgs e)
        {
            Orientaiton = "H";
            xVertival.IsEnabled = true;
            xHorizontal.IsEnabled = false;

            int tempH = (int)shipModel.Width;
            int tempV = (int)shipModel.Height;

            shipModel.Width = tempV;
            shipModel.Height = tempH;
        }

        private void xReadyBtn_Click(object sender, RoutedEventArgs e)
        {
            warPanel.Visibility = Visibility.Visible;
            settingPanel.Visibility = Visibility.Collapsed;
            
        }

        private void EmptySeaFlat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(FLAGG)
            {
                FLAGG = false;
                int index = EmptySeaFlat.SelectedIndex;
                if (bs.Board[index].Stone != '-')
                {
                    Attacks[index].Stone = 'A';
                }
                EmptySeaFlat.ItemsSource = null;
                EmptySeaFlat.ItemsSource = Attacks;
                FLAGG = true;
            }
            
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
