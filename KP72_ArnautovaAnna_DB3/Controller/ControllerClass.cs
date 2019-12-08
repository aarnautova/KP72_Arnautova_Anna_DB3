using KP72_ArnautovaAnna_DB3;
using Lab3_DB.Database;
using Lab3_DB.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3_DB.Controller
{
    public enum Operation
    {
        Get = 1,
        GetAll,
        Add,
        Update,
        Delete,
        Search
    }
    class ControllerClass
    {
        
        private ViewClass view;
        private RoomDAO roomDAO;
        private GuestDAO guestDAO;
        private BookingDAO bookingDAO;
        private DictionaryDAO dictionaryDAO;

        public ControllerClass(HotelContext db)
        {
            roomDAO = new RoomDAO(db);
            guestDAO = new GuestDAO(db);
            bookingDAO = new BookingDAO(db);
            dictionaryDAO = new DictionaryDAO(db);
            this.view = new ViewClass(dictionaryDAO.GetRoomTypes(), dictionaryDAO.GetGuestStatus());
        }

        public void Start()
        {
            while (true)
            {
               
                    while (true)
                    {
                        Entity entity = view.EntitiesMenu();
                        if (entity == Entity.Null) break;
                        else if(entity != Entity.Exception)
                        {

                                while (true)
                                {
                                    int operation = view.OperationsMenu();
                                    if (operation == 0) break;
                                    try
                                    {
                                        switch ((Operation)operation)
                                        {
                                            case Operation.Add:
                                                AddOperation();
                                                break;
                                            case Operation.Get:
                                                GetOperation();
                                                break;
                                            case Operation.GetAll:
                                                GetAllOperation();
                                                break;
                                            case Operation.Update:
                                                UpdateOperation();
                                                break;
                                            case Operation.Delete:
                                                DeleteOperation();
                                                break;
                                            case Operation.Search:
                                                SearchBookingOperation();
                                                break;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        view.Error(e.Message.ToString());
                                    }
                                    view.Wait();
                                }
                            }
                        }
            }
         }

        private void AddOperation() {
            switch (view.entity)
            {
                case Entity.Room:
                    Room room = view.RoomAddOrUpdateEnter();
                    roomDAO.Create(room);
                    break;
                case Entity.Guest:
                    Guest guest = view.GuestAddOrUpdateEnter();
                    guestDAO.Create(guest);
                    break;
                case Entity.Booking:
                    Booking booking = view.BookingAddOrUpdate();
                    bookingDAO.Create(booking);
                    break;
            }
            view.Success();
        }
        private void GetOperation()
        {
            int id = -1;
            while(id < 0) {
            id = view.EnterId();
            }
            switch (view.entity)
            {
                case Entity.Room:
                    List<Room> r_table = new List<Room>() { roomDAO.Get(id) };
                    view.PrintTable(view.Rooms_table(r_table));
                    break;
                case Entity.Guest:
                    List<Guest> g_table = new List<Guest>() { guestDAO.Get(id) };
                    view.PrintTable(view.Guests_table(g_table));
                    break;
                case Entity.Booking:
                    List<Booking> b_table = new List<Booking>() { bookingDAO.Get(id) };
                    view.PrintTable(view.Booking_table(b_table));
                    break;
            }
            view.Success();
        }
        private void GetAllOperation()
        {
            int page = 0;
            while (true)
            {
                switch (view.entity)
                {
                    case Entity.Room:
                        List<Room> r_table = roomDAO.GetAll(page);
                        view.PrintTable(view.Rooms_table(r_table));
                        break;
                    case Entity.Guest:
                        List<Guest> g_table = guestDAO.GetAll(page);
                        view.PrintTable(view.Guests_table(g_table));
                        break;
                    case Entity.Booking:
                        List<Booking> b_table = bookingDAO.GetAll(page);
                        view.PrintTable(view.Booking_table(b_table));
                        break;
                }
                int arr = view.Page();
                if (arr == 0) break;
                else page += arr;
                if (page < 0) page = 0;
            }
            view.Success();
        }
        private void UpdateOperation()
        {
            int id = -1;
            while (id < 0)
            {
                id = view.EnterId();
            }
            if (id < 0) throw new Exception("Wrong id");
            switch (view.entity)
            {
                case Entity.Room:
                    Room room = view.RoomAddOrUpdateEnter();
                    room.Id = id;
                    roomDAO.Update(room);
                    break;
                case Entity.Guest:
                    Guest guest = view.GuestAddOrUpdateEnter();
                    guest.Id = id;
                    guestDAO.Update(guest);
                    break;
                case Entity.Booking:
                    Booking booking = view.BookingAddOrUpdate();
                    booking.Id = id;
                    bookingDAO.Update(booking);
                    break;
            }
            view.Success();
        }
        private void DeleteOperation()
        {
            int id = -1;
            while (id < 0)
            {
                id = view.EnterId();
            }
            if (id < 0) throw new Exception("Wrong id");
            switch (view.entity)
            {
                case Entity.Room:
                    roomDAO.Delete(id);
                    break;
                case Entity.Guest:
                    guestDAO.Delete(id);
                    break;
                case Entity.Booking:
                    bookingDAO.Delete(id);
                    break;
            }
            view.Success();
        }

        private void SearchBookingOperation()
        {
            Booking searchdata = view.StaticSearch();
            List<Booking> b = bookingDAO.StaticSearch(searchdata.Arrival, searchdata.Departure, searchdata.Room.OceanView);
            view.PrintTable(view.Booking_table(b));
        }
    }
}
