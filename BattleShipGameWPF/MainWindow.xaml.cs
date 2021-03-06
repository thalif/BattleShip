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
        BattleShipModel bs = new BattleShipModel();

        ObservableCollection<BattleShip.FlatStone> tempBoard = new ObservableCollection<BattleShip.FlatStone>();
        
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
            xPlayerLabel.Text = "Player 1";
            ShipList.ItemsSource = bs.P1Ships;
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


        private void Vertival_Click(object sender, RoutedEventArgs e)
        {
            Orientaiton = "V";
            xVertival.IsEnabled = false;
            xHorizontal.IsEnabled = true;

            int tempH = (int)shipModel.Width;
            int tempV = (int)shipModel.Height;

            shipModel.Width = tempV;
            shipModel.Height = tempH;

        }
        private void Horizontal_Click(object sender, RoutedEventArgs e)
        {
            Orientaiton = "H";
            xVertival.IsEnabled = true;
            xHorizontal.IsEnabled = false;

            int tempH = (int)shipModel.Width;
            int tempV = (int)shipModel.Height;

            shipModel.Width = tempV;
            shipModel.Height = tempH;
        }
        private void ReadyBtn_Click(object sender, RoutedEventArgs e)
        {
            xP1ShipLife.ItemsSource = bs.P1ShipStrength;
            xP2ShipLife.ItemsSource = bs.P2ShipStrength;

            if(PlayerFLAGG)
            {
                xPlayerLabel.Text = "Player 2";
                PlayerFLAGG = false;
                ShipList.ItemsSource = bs.P2Ships;
                BindBoard(bs.P2Board);
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
                        if (bs.CheckSpace4_SHIP1(SeaFlat.SelectedIndex, SelectedShip, Orientaiton))
                        {
                            bs.WriteBoard_4_SHIP1(SelectedShip, SeaFlat.SelectedIndex, Orientaiton);

                            SelectedShip = string.Empty;
                            Orientaiton = string.Empty;
                        }
                        else
                            FLAG = true;
                    }
                    else
                    {
                        if (bs.CheckSpace4_SHIP2(SeaFlat.SelectedIndex, SelectedShip, Orientaiton))
                        {
                            bs.WriteBoard_4_SHIP2(SelectedShip, SeaFlat.SelectedIndex, Orientaiton);

                            SelectedShip = string.Empty;
                            Orientaiton = string.Empty;
                        }
                        else
                            FLAG = true;
                    }
                }
                else
                    FLAG = !FLAG;
            }
            else
            {
                int index = SeaFlat.SelectedIndex;
                if(FLAG)
                {
                    FLAG = false;
                    if (PlayerFLAGG)
                    {
                        if (bs.P1Board[index].Stone != "-")
                            bs.RemoveFrom_Ship1(index);
                    }
                    else
                    {
                        if (bs.P2Board[index].Stone != "-")
                            bs.RemoveFrom_Ship2(index);
                    }
                    SeaFlat.SelectedIndex = -1;
                    FLAG = true;
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

                bs.AttackP1Land(index);

                Player1Land.ItemsSource = null;
                Player1Land.ItemsSource = bs.P1Land;
                L1FLAGG = true;
                GameState();
            }
        }
        private void Player2Land_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (L2FLAGG)
            {
                L2FLAGG = false;
                int index = Player2Land.SelectedIndex;

                bs.AttackP2Land(index);

                Player2Land.ItemsSource = null;
                Player2Land.ItemsSource = bs.P2Land;
                L2FLAGG = true;
                GameState();
            }
        }
        void GameState()
        {
            if (bs.P1Life <= 0)
            {
                xResultBackgroundPanel.Visibility = Visibility.Visible;
                xResultPad.Visibility = Visibility.Visible;
                xWarPanel.IsEnabled = false;
                xResultTB.Text = "Player 2 Won";
            }   
            if (bs.P2Life <= 0)
            {
                xResultBackgroundPanel.Visibility = Visibility.Visible;
                xResultPad.Visibility = Visibility.Visible;
                xWarPanel.IsEnabled = false;
                xResultTB.Text = "Player 1 Won";
            }
        }

        private void xResultOk_Click(object sender, RoutedEventArgs e)
        {
            PlayerFLAGG = true;

            xWarPanel.IsEnabled = true;
            xWarPanel.Visibility = Visibility.Collapsed;
            xResultBackgroundPanel.Visibility = Visibility.Collapsed;
            xResultPad.Visibility = Visibility.Collapsed;
            bs = new BattleShipModel();
            LoadLocalData();
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
