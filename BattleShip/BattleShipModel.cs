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
        char Default = '-';
        //Dictionary<int, char> Board;
        
        public ObservableCollection<FlatStone> P1Board = new ObservableCollection<FlatStone>();
        public ObservableCollection<FlatStone> P2Board = new ObservableCollection<FlatStone>();

        public ObservableCollection<FlatStone> P1Land = new ObservableCollection<FlatStone>();
        public ObservableCollection<FlatStone> P2Land = new ObservableCollection<FlatStone>();



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

        public ObservableCollection<FlatStone> Attack(ObservableCollection<FlatStone> Board, int position)
        {
            FlatStone c = Board[position];

            if(c == new FlatStone('S'))
            {
                Board[position] = new FlatStone('X');
                if(ShipStrength["SUBMARINE"] > 0)
                    ShipStrength["SUBMARINE"]--;
            }
            else if(c == new FlatStone('C'))
            {
                Board[position] = new FlatStone('X');
                if (ShipStrength["CARRIER"] > 0)
                    ShipStrength["CARRIER"]--;
            }
            else if(c == new FlatStone('R'))
            {
                Board[position] = new FlatStone('X');
                if (ShipStrength["CRUISER"] > 0)
                    ShipStrength["CRUISER"]--;
            }
            else if(c == new FlatStone('D'))
            {
                Board[position] = new FlatStone('X');
                if (ShipStrength["DESTROYER"] > 0)
                    ShipStrength["DESTROYER"]--;
            }
            else
            {
                Board[position] = new FlatStone('O');
            }
            return Board;
        }

        #region Print function Works with Console
        //public void PrintBoard()
        //{
        //    int count = 0;
        //    Console.ForegroundColor = System.ConsoleColor.White;
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        Console.Write("\t"+i.ToString());
        //    }
        //    Console.WriteLine("\n");
        //    Console.Write(0);
        //    int T = 1;
        //    for (int i = 0; i < Board.Count; i++)
        //    {
        //        count++;
        //        ConsoleColor(Board[i]);
        //        Console.Write("\t" + Board[i]);
        //        if (count == 10)
        //        {
        //            count = 0;
        //            Console.WriteLine();
        //            Console.WriteLine();
        //            if(T < 10)
        //            {
        //                Console.ForegroundColor = System.ConsoleColor.White;
        //                Console.Write(T.ToString());
        //                T++;
        //            }
        //        }
        //    }
        //    Console.WriteLine("\n\n");
        //    foreach(KeyValuePair<string, int> item in ShipStrength)
        //    {
        //        if(item.Value != 0)
        //        {
        //            Console.ForegroundColor = System.ConsoleColor.DarkGreen;
        //            Console.WriteLine(item.Key + "  - \t " + item.Value);
        //        }
        //        else
        //        {
        //            Console.ForegroundColor = System.ConsoleColor.Red;
        //            Console.WriteLine(item.Key + "  - \t " + item.Value);
        //        }
        //    } 
        //}
        #endregion
        public void ConsoleColor(FlatStone c)
        {
            if (c == new FlatStone(Default))
                Console.ForegroundColor = System.ConsoleColor.White;
            else if(c == new FlatStone('X'))
                Console.ForegroundColor = System.ConsoleColor.Red;
            else if(c == new FlatStone('O'))
                Console.ForegroundColor = System.ConsoleColor.Blue;
            else
                Console.ForegroundColor = System.ConsoleColor.Yellow;
        }

        public ObservableCollection<FlatStone> WriteBoard(ObservableCollection<FlatStone> Board, string ship, int position, string ori)
        {
            FlatStone shipOF = GetShip(GetShip(ship));
            if(GetOrientation(ori).Equals(Orientation.H))
            {
                if (CheckShipSpace(Board,position, ship, ori))
                {
                    for (int i = position; i < ((int)GetShip(ship) + position); i++)
                        Board[i] = shipOF;
                }
            }
            else if(GetOrientation(ori).Equals(Orientation.V))
            {
                if(CheckShipSpace(Board,position, ship, ori))
                {
                    for (int i = position; i < ((int)GetShip(ship) * 10) + position; i += 10)
                        Board[i] = shipOF;
                }
            }
            return Board;
        }

       
        public bool CheckShipSpace(ObservableCollection<FlatStone> Board,int index, string ship, string ori)
        {
            if(GetOrientation(ori).Equals(Orientation.H))
            {
                int IndexCalc = ((index) % 10);
                if (IndexCalc + (int)GetShip(ship) > 10)
                    return false;

                for (int i = index; i < index + (int)GetShip(ship); i++)
                    if (Board[i].Stone != Default)
                        return false;
            }
            else if(GetOrientation(ori).Equals(Orientation.V))
            {
                int IndexCalc = index + (((int)GetShip(ship) - 1) * 10);
                if (IndexCalc > 100)
                    return false;

                for (int i = index; i < ((int)GetShip(ship) * 10) + index; i += 10)
                    if (Board[i].Stone !=  Default)
                        return false;
            }
            return true;
        }
        public Orientation GetOrientation(string Ori)
        {
            if (Ori == "H")
                return Orientation.H;
            else if (Ori == "V")
                return Orientation.V;
            return Orientation.H;
        }
        public Ship GetShip(string ship)
        {
            if (ship == Ship.CARRIER.ToString())
                return Ship.CARRIER;
            else if (ship == Ship.CRUISER.ToString())
                return Ship.CRUISER;
            else if (ship == Ship.DESTROYER.ToString())
                return Ship.DESTROYER;
            else if (ship == Ship.SUBMARINE.ToString())
                return Ship.SUBMARINE;
            else
                return Ship.CARRIER;
        }

        public FlatStone GetShip(Ship ship)
        {
            if (ship.Equals(Ship.CARRIER))
                return new FlatStone('C');
            else if (ship.Equals(Ship.CRUISER))
                return new FlatStone('R');
            else if (ship.Equals(Ship.SUBMARINE))
                return new FlatStone('S');
            else if (ship.Equals(Ship.DESTROYER))
                return new FlatStone('D');

            return new FlatStone(Default);
        }

        public BattleShipModel()
        {
            ShipStrength.Add("SUBMARINE", 4);
            ShipStrength.Add("DESTROYER", 2);
            ShipStrength.Add("CRUISER", 3);
            ShipStrength.Add("CARRIER", 5);

            for (int i = 0; i < 100; i++)
            {
                P1Board.Add(new FlatStone('-'));
                P2Board.Add(new FlatStone('-'));

                P1Land.Add(new FlatStone('-'));
                P2Land.Add(new FlatStone('-'));
            }
        }
    }
    
}
