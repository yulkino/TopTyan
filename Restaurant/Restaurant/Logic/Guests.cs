using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Guests
    {
        public static List<Guest> GuestsList = new List<Guest>();

        public void RemoveGuest(Guest guest)
        {
            Guests.GuestsList.Remove(guest);
        }
    }
}
