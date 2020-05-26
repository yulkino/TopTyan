using System.Windows;

namespace Restaurant
{
    public class Table
    {
        public Point Position = new Point();
        public bool IsOccupated;
        public bool Served;
        public TableState FoodOnTable;

        public Table(Point position, TableState foodOnTable)
        {
            Position = position;
            FoodOnTable = foodOnTable;
        }

        public void Serve(TableState tableState)
        {
            Served = true;
            FoodOnTable = tableState;
        }

        public void Clean()
        {
            FoodOnTable = TableState.EmptyTable;
            IsOccupated = false;
            Served = false;
        }
    }
}
