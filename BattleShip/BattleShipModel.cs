using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class BattleShipModel
    {
        char Default = '-';
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

        Dictionary<int, char> Board;
        
        public void PrintBoard()
        {
            char[] _board = Board.Values.ToArray();
            int count = 0;
            for (int i = 0; i < _board.Length; i++)
            {
                count++;
                ConsoleColor(_board[i]);
                Console.Write("\t" + _board[i]);
                if (count == 10)
                {
                    count = 0;
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }
        public void ConsoleColor(char c)
        {
            if (c != Default)
                Console.ForegroundColor = System.ConsoleColor.Red;
            else
                Console.ForegroundColor = System.ConsoleColor.White;
        }

        public void WriteBoard(Ship ship, int position, Orientation ori)
        {
            char shipOF = GetShip(ship);
            if(ori.Equals(Orientation.H))
            {
                if (CheckShipSpace(position, ship, ori))
                {
                    for (int i = position; i < ((int)ship + position); i++)
                        Board[i] = shipOF;
                }
            }
            else if(ori.Equals(Orientation.V))
            {
                if(CheckShipSpace(position, ship, ori))
                {
                    for (int i = position; i < ((int)ship * 10) + position; i += 10)
                        Board[i] = shipOF;
                }
            }
        }

        public bool CheckShipSpace(int index, Ship ship, Orientation ori)
        {
            if(ori.Equals(Orientation.H))
            {
                int IndexCalc = (index % 10) - 1;
                if (IndexCalc + (int)ship > 10)
                    return false;

                for (int i = index; i < index + (int)ship; i++)
                    if (Board[i] != Default)
                        return false;
            }
            else if(ori.Equals(Orientation.V))
            {
                int IndexCalc = index + (((int)ship - 1) * 10);
                if (IndexCalc > 100)
                    return false;

                for (int i = index; i < ((int)ship * 10) + index; i += 10)
                    if (Board[i] != Default)
                        return false;
            }
            return true;
        }

        public char GetShip(Ship ship)
        {
            if (ship.Equals(Ship.CARRIER))
                return 'C';
            else if (ship.Equals(Ship.CRUISER))
                return 'R';
            else if (ship.Equals(Ship.SUBMARINE))
                return 'S';
            else if (ship.Equals(Ship.DESTROYER))
                return 'D';

            return Default;
        }

        public BattleShipModel()
        {
            Board =  new Dictionary<int, char>();
            for (int i = 1; i <= 100; i++)
                Board.Add(i, Default);
        }
    }

}
