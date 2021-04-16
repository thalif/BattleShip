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
        //ObservableCollection<FlatStone> P1Attacks = new ObservableCollection<FlatStone>();
        //ObservableCollection<FlatStone> P2Attacks = new ObservableCollection<FlatStone>();

        ObservableCollection<BattleShip.FlatStone> tempBoard = new ObservableCollection<BattleShip.FlatStone>();

        BattleShipModel bs = new BattleShipModel();
        //ObservableCollection<BattleShip.FlatStone> P1Board = new ObservableCollection<BattleShip.FlatStone>();
        //ObservableCollection<BattleShip.FlatStone> P2Board = new ObservableCollection<BattleShip.FlatStone>();

        ObservableCollection<string> ShipLists = new ObservableCollection<string>();
        string SelectedShip = string.Empty;
        string Orientaiton = string.Empty;
        bool FLAG = true;
        bool L1FLAGG = true;
        bool L2FLAGG = true;

        bool PlayerFLAGG = true;                //    T = P1    ||      F = P2
        public MainWindow()
        {
            InitializeComponent();
            LoadLocalData();
            xHorizontal.IsEnabled = false;
        }
        

        public void LoadLocalData()
        {
            tempBoard = bs.P1Board;
            ShipLists.Add("CARRIER");
            ShipLists.Add("CRUISER");
            ShipLists.Add("SUBMARINE");
            ShipLists.Add("DESTROYER");

            ShipList.ItemsSource = ShipLists;
            SeaFlat.ItemsSource = bs.P1Board;
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
            else if (ship == "CRUISER")
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
        void BindBoard(ObservableCollection<BattleShip.FlatStone> board)
        {
            tempBoard = board;
            SeaFlat.ItemsSource = tempBoard;
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
            if(PlayerFLAGG)
            {
                if (ShipLists.Count() > 0)
                {

                }
                else
                {
                    xPlayerLabel.Text = "Player 2";
                    PlayerFLAGG = false;
                    ShipList.ItemsSource = ShipLists;

                    ShipLists.Add("CARRIER");
                    ShipLists.Add("CRUISER");
                    ShipLists.Add("SUBMARINE");
                    ShipLists.Add("DESTROYER");
                    BindBoard(bs.P2Board);
                }
            }
            else
            {
                xWarPanel.Visibility = Visibility.Visible;
                Player1Land.ItemsSource = bs.P1Land;
                Player2Land.ItemsSource = bs.P2Land;
            }
        }


        private void SeaFlat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InternalValidation())
            {
                if (FLAG)
                {
                    FLAG = !FLAG;
                    if (PlayerFLAGG)
                    {
                        if (bs.CheckShipSpace(bs.P1Board, SeaFlat.SelectedIndex, SelectedShip, Orientaiton))
                        {
                            bs.WriteBoard(bs.P1Board, SelectedShip, SeaFlat.SelectedIndex, Orientaiton);
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
                        if (bs.CheckShipSpace(bs.P2Board, SeaFlat.SelectedIndex, SelectedShip, Orientaiton))
                        {
                            bs.WriteBoard(bs.P2Board, SelectedShip, SeaFlat.SelectedIndex, Orientaiton);
                            ShipLists.Remove(SelectedShip);
                            SelectedShip = string.Empty;
                            Orientaiton = string.Empty;
                        }
                        else
                        {
                            FLAG = true;
                        }
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
                if (PlayerFLAGG)
                {
                    if (bs.P1Board[index].Stone != '-')
                        MessageBox.Show("Remove Ship");
                    else
                        MessageBox.Show("Do You want to remove");
                }
                else
                {
                    if (bs.P2Board[index].Stone != '-')
                        MessageBox.Show("Remove Ship");
                    else
                        MessageBox.Show("Do You want to remove");
                }
            }
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
        private void Player1Land_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(L1FLAGG)
            {
                L1FLAGG = false;
                int index = Player1Land.SelectedIndex;
                if (bs.P1Board[index].Stone != '-')
                {
                    bs.P1Land[index].Stone = 'A';
                }
                else
                {
                    bs.P1Land[index].Stone = 'M';
                }
                Player1Land.ItemsSource = null;
                Player1Land.ItemsSource = bs.P1Land;
                L1FLAGG = true;
            }
        }
        private void Player2Land_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (L2FLAGG)
            {
                L2FLAGG = false;
                int index = Player2Land.SelectedIndex;
                if (bs.P2Board[index].Stone != '-')
                {
                    bs.P2Land[index].Stone = 'A';
                }
                else
                {
                    bs.P2Land[index].Stone = 'M';
                }
                Player2Land.ItemsSource = null;
                Player2Land.ItemsSource = bs.P2Land;
                L2FLAGG = true;
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
