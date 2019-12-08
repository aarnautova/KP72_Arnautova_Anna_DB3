using Lab3_DB.Controller;
using Lab3_DB.Database;
using System;
using System.Collections.Generic;

namespace KP72_ArnautovaAnna_DB3
{
    class Program
    {
        static void Main(string[] args)
        {
            using(HotelContext dB = new HotelContext())
            {
                RoomDAO rd = new RoomDAO(dB);
                GuestDAO gd = new GuestDAO(dB);
                BookingDAO bd = new BookingDAO(dB);
                DictionaryDAO d = new DictionaryDAO(dB);
                ControllerClass controller = new ControllerClass(dB);
                controller.Start();
            }
        }
    }
}
