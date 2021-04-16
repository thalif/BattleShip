using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            BattleShipModel Game = new BattleShipModel();

            //Game.WriteBoard(BattleShipModel.Ship.CARRIER, 6, BattleShipModel.Orientation.H);
            //Game.WriteBoard(BattleShipModel.Ship.CRUISER, 57, BattleShipModel.Orientation.V);
            //Game.WriteBoard(BattleShipModel.Ship.SUBMARINE, 37, BattleShipModel.Orientation.H);
            //Game.WriteBoard(BattleShipModel.Ship.DESTROYER, 42, BattleShipModel.Orientation.V);

            //Game.PrintBoard();
            //for (int i = 0; i < 100; i++)
            //{
            //    Game.Attack(int.Parse(Console.ReadLine()) - 1);
            //    Game.PrintBoard();
            //}
            Console.ReadKey();
        }   
    }
}
