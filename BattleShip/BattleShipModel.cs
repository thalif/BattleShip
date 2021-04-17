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
        
        public ObservableCollection<FlatStone> P1Board = new ObservableCollection<FlatStone>();
        public ObservableCollection<FlatStone> P2Board = new ObservableCollection<FlatStone>();

        public ObservableCollection<FlatStone> P1Land = new ObservableCollection<FlatStone>();
        public ObservableCollection<FlatStone> P2Land = new ObservableCollection<FlatStone>();

        Dictionary<string, int> ShipStrength = new Dictionary<string, int>();

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
        #region For Console 
        public void ConsoleColor(FlatStone c)
        {
            if (c == new FlatStone(Default))
                Console.ForegroundColor = System.ConsoleColor.White;
            else if (c == new FlatStone('X'))
                Console.ForegroundColor = System.ConsoleColor.Red;
            else if (c == new FlatStone('O'))
                Console.ForegroundColor = System.ConsoleColor.Blue;
            else
                Console.ForegroundColor = System.ConsoleColor.Yellow;
        }
        #endregion

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
            if(P1Board[index].Stone != '-')
            {
                P1Land[index] = new FlatStone('A');
            }
            else
            {
                P1Land[index] = new FlatStone('M');
            }
        }
        public void AttackP2Land(int index)
        {
            if (P2Board[index].Stone != '-')
            {
                P2Land[index] = new FlatStone('A');
            }
            else
            {
                P2Land[index] = new FlatStone('M');
            }
        }


        public void WriteBoard_4_SHIP1(string ship, int position, string ori)
        {
            FlatStone shipOF = GetShipInitial(ship);
            if (GetOrientation(ori).Equals(Orientation.H))
            {
                if (CheckSpace4_SHIP1(position, ship, ori))
                {
                    for (int i = position; i < ((int)GetShip(ship) + position); i++)
                        P1Board[i] = shipOF;
                }
            }
            else if (GetOrientation(ori).Equals(Orientation.V))
            {
                if (CheckSpace4_SHIP1(position, ship, ori))
                {
                    for (int i = position; i < ((int)GetShip(ship) * 10) + position; i += 10)
                        P1Board[i] = shipOF;
                }
            }
        }
        public void WriteBoard_4_SHIP2(string ship, int position, string ori)
        {
            FlatStone shipOF = GetShipInitial(ship);
            if (GetOrientation(ori).Equals(Orientation.H))
            {
                if (CheckSpace4_SHIP2(position, ship, ori))
                {
                    for (int i = position; i < ((int)GetShip(ship) + position); i++)
                        P2Board[i] = shipOF;
                }
            }
            else if (GetOrientation(ori).Equals(Orientation.V))
            {
                if (CheckSpace4_SHIP2(position, ship, ori))
                {
                    for (int i = position; i < ((int)GetShip(ship) * 10) + position; i += 10)
                        P2Board[i] = shipOF;
                }
            }
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
                if (IndexCalc > 100)
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
                return new FlatStone('C');
            else if (ship == Ship.CRUISER.ToString())
                return new FlatStone('R');
            else if (ship == Ship.DESTROYER.ToString())
                return new FlatStone('D');
            else if (ship == Ship.SUBMARINE.ToString())
                return new FlatStone('S');
            else
                return new FlatStone('C');
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


        public BattleShipModel()
        {
            ShipStrength.Add("SUBMARINE", 4);
            ShipStrength.Add("DESTROYER", 2);
            ShipStrength.Add("CRUISER", 3);
            ShipStrength.Add("CARRIER", 5);

            FlatStone Default = new FlatStone('-');
            for (int i = 0; i < 100; i++)
            {
                P1Board.Add(Default);
                P2Board.Add(Default);

                P1Land.Add(Default);
                P2Land.Add(Default);
            }
        }
    }
    
}
