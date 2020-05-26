using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public enum Event
    {
        PressedE,
        PressedD,
        PressedS,
        PressedA,
        PressedW,
        CreatedTable,
        CreatedTablesForFood,
        GuestArrived,
        GuestGone,
        RatingUpdated,
        DishTaken,
        WaiterMoved,
        OrderAccepted,
        ServedTable,
        FinishGame
    }
}
