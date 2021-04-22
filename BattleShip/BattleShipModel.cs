using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class BattleShipModel : FlatStone
    {
        string Default = "-";

        public ObservableCollection<FlatStone> P1Board = new ObservableCollection<FlatStone>();
        public ObservableCollection<FlatStone> P2Board = new ObservableCollection<FlatStone>();

        public ObservableCollection<FlatStone> P1Land = new ObservableCollection<FlatStone>();
        public ObservableCollection<FlatStone> P2Land = new ObservableCollection<FlatStone>();


        public ObservableCollection<string> P1Ships = new ObservableCollection<string>();
        public ObservableCollection<string> P2Ships = new ObservableCollection<string>();

        public ObservableCollection<int> P1ShipStrength = new ObservableCollection<int>();
        public ObservableCollection<int> P2ShipStrength = new ObservableCollection<int>();

        Dictionary<string, int> ShipStrength = new Dictionary<string, int>();

        public enum Ship
        {
            CARRIER = 5,
            CRUISER = 3,
            SUBMARINE = 4,
            DESTROYER = 2
        }
        public enum Orientation
        {
            H,
            V
        }


        public void AttackP1Land(int index)
        {
            if (P1Board[index].Stone[0] == 'S')
            {
                if(P1Land[index].Stone == "-")
                {
                    P1Land[index] = new FlatStone("A");
                    P1ShipStrength.RemoveAt(0);
                }
            }
            else
                P1Land[index] = new FlatStone("M");
        }
        public void AttackP2Land(int index)
        {
            if (P2Board[index].Stone[0] == 'S')
            {
                if(P2Land[index].Stone == "-")
                {
                    P2Land[index] = new FlatStone("A");
                    P2ShipStrength.RemoveAt(0);
                }
            }
            else
                P2Land[index] = new FlatStone("M");
        }


        public void WriteBoard_4_SHIP1(string ship, int position, string ori)
        {
            if (GetOrientation(ori).Equals(Orientation.H))
            {
                List<FlatStone> ToBind = GetShipImageH(ship);
                if (CheckSpace4_SHIP1(position, ship, ori))
                {
                    int s = 0;
                    for (int i = position; i < ((int)GetShip(ship) + position); i++)
                        P1Board[i] = ToBind[s++];

                    P1Ships.Remove(ship);
                }
            }
            else if (GetOrientation(ori).Equals(Orientation.V))
            {
                List<FlatStone> ToBind = GetShipImageV(ship);
                if (CheckSpace4_SHIP1(position, ship, ori))
                {
                    int s = 0;
                    for (int i = position; i < ((int)GetShip(ship) * 10) + position; i += 10)
                        P1Board[i] = ToBind[s++];

                    P1Ships.Remove(ship);
                }
            }
        }
        public void WriteBoard_4_SHIP2(string ship, int position, string ori)
        {
            FlatStone shipOF = GetShipInitial(ship);
            if (GetOrientation(ori).Equals(Orientation.H))
            {
                List<FlatStone> ToBind = GetShipImageH(ship);
                if (CheckSpace4_SHIP2(position, ship, ori))
                {
                    int s = 0;
                    for (int i = position; i < ((int)GetShip(ship) + position); i++)
                        P2Board[i] = ToBind[s++];

                    P2Ships.Remove(ship);
                }
            }
            else if (GetOrientation(ori).Equals(Orientation.V))
            {
                List<FlatStone> ToBind = GetShipImageV(ship);
                if (CheckSpace4_SHIP2(position, ship, ori))
                {
                    int s = 0;
                    for (int i = position; i < ((int)GetShip(ship) * 10) + position; i += 10)
                        P2Board[i] = ToBind[s++];

                    P2Ships.Remove(ship);
                }
            }
        }


        public void RemoveFrom_Ship1(int index)
        {
            string shipOf = P1Board[index].Stone;
            string Ship = shipOf.Substring(0, 2);

            foreach(FlatStone item in P1Board)
            {
                if (item.Stone.Contains(Ship))
                    item.Stone = "-";
            }

            P1Ships.Add(GetShipName4Init(Ship));
        }
        public void RemoveFrom_Ship2(int index)
        {
            string shipOf = P2Board[index].Stone;
            string Ship = shipOf.Substring(0, 2);

            foreach (FlatStone item in P2Board)
            {
                if (item.Stone.Contains(Ship))
                    item.Stone = "-";
            }
            P2Ships.Add(GetShipName4Init(Ship));
        }


        List<FlatStone> GetShipImageV(string ship)
        {
            List<FlatStone> toReturn = new List<FlatStone>();
            if (ship == "SUBMARINE")
            {
                toReturn.Add(new FlatStone("S4V1"));
                toReturn.Add(new FlatStone("S4V2"));
                toReturn.Add(new FlatStone("S4V3"));
                toReturn.Add(new FlatStone("S4V4"));
            }
            else if (ship == "DESTROYER")
            {
                toReturn.Add(new FlatStone("S2V1"));
                toReturn.Add(new FlatStone("S2V2"));
            }
            else if (ship == "CARRIER")
            {
                toReturn.Add(new FlatStone("S5V1"));
                toReturn.Add(new FlatStone("S5V2"));
                toReturn.Add(new FlatStone("S5V3"));
                toReturn.Add(new FlatStone("S5V4"));
                toReturn.Add(new FlatStone("S5V5"));
            }
            else
            {
                toReturn.Add(new FlatStone("S3V1"));
                toReturn.Add(new FlatStone("S3V2"));
                toReturn.Add(new FlatStone("S3V3"));
            }
            return toReturn;
        }
        List<FlatStone> GetShipImageH(string ship)
        {
            List<FlatStone> toReturn = new List<FlatStone>();
            if (ship == "SUBMARINE")
            {
                toReturn.Add(new FlatStone("S4H1"));
                toReturn.Add(new FlatStone("S4H2"));
                toReturn.Add(new FlatStone("S4H3"));
                toReturn.Add(new FlatStone("S4H4"));
            }
            else if (ship == "DESTROYER")
            {
                toReturn.Add(new FlatStone("S2H1"));
                toReturn.Add(new FlatStone("S2H2"));
            }
            else if (ship == "CARRIER")
            {
                toReturn.Add(new FlatStone("S5H1"));
                toReturn.Add(new FlatStone("S5H2"));
                toReturn.Add(new FlatStone("S5H3"));
                toReturn.Add(new FlatStone("S5H4"));
                toReturn.Add(new FlatStone("S5H5"));
            }
            else
            {
                toReturn.Add(new FlatStone("S3H1"));
                toReturn.Add(new FlatStone("S3H2"));
                toReturn.Add(new FlatStone("S3H3"));
            }
            return toReturn;
        }

        public bool CheckSpace4_SHIP1(int index, string ship, string ori)
        {
            if (GetOrientation(ori).Equals(Orientation.H))
            {
                int IndexCalc = ((index) % 10);
                if (IndexCalc + (int)GetShip(ship) > 10)
                    return false;

                for (int i = index; i < index + (int)GetShip(ship); i++)
                    if (P1Board[i].Stone != Default)
                        return false;
            }
            else if (GetOrientation(ori).Equals(Orientation.V))
            {
                int IndexCalc = index + (((int)GetShip(ship) - 1) * 10);
                if (IndexCalc > 99)
                    return false;

                for (int i = index; i < ((int)GetShip(ship) * 10) + index; i += 10)
                    if (P1Board[i].Stone != Default)
                        return false;
            }
            return true;
        }
        public bool CheckSpace4_SHIP2(int index, string ship, string ori)
        {
            if (GetOrientation(ori).Equals(Orientation.H))
            {
                int IndexCalc = ((index) % 10);
                if (IndexCalc + (int)GetShip(ship) > 10)
                    return false;

                for (int i = index; i < index + (int)GetShip(ship); i++)
                    if (P2Board[i].Stone != Default)
                        return false;
            }
            else if (GetOrientation(ori).Equals(Orientation.V))
            {
                int IndexCalc = index + (((int)GetShip(ship) - 1) * 10);
                if (IndexCalc > 100)
                    return false;

                for (int i = index; i < ((int)GetShip(ship) * 10) + index; i += 10)
                    if (P2Board[i].Stone != Default)
                        return false;
            }
            return true;
        }


        static string GetShipName4Init(string init)
        {
            if (init == "S2")
                return Ship.DESTROYER.ToString();
            else if (init == "S3")
                return Ship.CRUISER.ToString();
            else if (init == "S4")
                return Ship.SUBMARINE.ToString();
            else
                return Ship.CARRIER.ToString();
        }
        public Orientation GetOrientation(string Ori)
        {
            if (Ori == "H")
                return Orientation.H;
            else if (Ori == "V")
                return Orientation.V;
            return Orientation.H;
        }
        public FlatStone GetShipInitial(string ship)
        {
            if (ship == Ship.CARRIER.ToString())
                return new FlatStone("C");
            else if (ship == Ship.CRUISER.ToString())
                return new FlatStone("R");
            else if (ship == Ship.DESTROYER.ToString())
                return new FlatStone("D");
            else if (ship == Ship.SUBMARINE.ToString())
                return new FlatStone("S");
            else
                return new FlatStone("C");
        }
        public Ship GetShip(string ship)
        {
            if (ship.Equals(Ship.CARRIER.ToString()))
                return Ship.CARRIER;
            else if (ship.Equals(Ship.CRUISER.ToString()))
                return Ship.CRUISER;
            else if (ship.Equals(Ship.SUBMARINE.ToString()))
                return Ship.SUBMARINE;
            else if (ship.Equals(Ship.DESTROYER.ToString()))
                return Ship.DESTROYER;

            return Ship.CARRIER;
        }

        static int TotalShipStrength
        {
            get
            {
                int AllStrength = 0;
                AllStrength += (int)Ship.CARRIER;
                AllStrength += (int)Ship.CRUISER;
                AllStrength += (int)Ship.DESTROYER;
                AllStrength += (int)Ship.SUBMARINE;

                return AllStrength;
            }
        }

        public int P1Life
        {
            get
            {
                return P1ShipStrength.Count;
            }
        }
        public int P2Life
        {
            get
            {
                return P2ShipStrength.Count;
            }
        }



        public BattleShipModel()
        {
            ShipStrength.Add("SUBMARINE", 4);
            ShipStrength.Add("DESTROYER", 2);
            ShipStrength.Add("CRUISER", 3);
            ShipStrength.Add("CARRIER", 5);


            for(int i = 0; i < TotalShipStrength; i++)
            {
                P1ShipStrength.Add(0);
                P2ShipStrength.Add(0);
            }

            FlatStone Default = new FlatStone("-");
            for (int i = 0; i < 100; i++)
            {
                P1Board.Add(Default);
                P2Board.Add(Default);

                P1Land.Add(Default);
                P2Land.Add(Default);
            }
            string[] ShipList = new string[] { "SUBMARINE", "DESTROYER", "CRUISER", "CARRIER" };
            foreach(string item in ShipList)
            {
                P1Ships.Add(item);
                P2Ships.Add(item);
            }
            
        }
    }
    
}
