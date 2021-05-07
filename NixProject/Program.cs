using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NixProject
{
    class Program
    {
        static void Main(string[] args)
        {
            /*DateTime date = new DateTime(2021, 5,7);
            Guest guest1 = new Guest("Denis Polozov", date, "Khrakiv, Prosp Nauky 51");
            Guest guest2 = new Guest("Mykhailo Prokopchyk", date, "Khrakiv, Prosp Nauky 52");
            Guest guest3 = new Guest("Ivan Avdeenkok", date, "Khrakiv, Prosp Nauky 53");
            Room room1 = new Room(1, 350, 2, "Econom");
            Room room2 = new Room(2, 800, 3, "Lux");
            Room room3 = new Room(3, 1300, 6, "XXL");

            RoomManagment manag = new RoomManagment();
            manag.AddRoom(room1);
            manag.AddRoom(room2);
            manag.AddRoom(room3);
            manag.AddRoom(room2);
            manag.DeleteRoom(room1);
            manag.AddRoom(room1);

            manag.AddGuest(guest1);
            manag.AddGuest(guest2);
            manag.AddGuest(guest3);

            manag.BookRoom(room1, guest1, date, date.AddDays(10));
            manag.BookRoom(room2, guest2, date, date.AddDays(9));
            manag.BookRoom(room1, guest3, date.AddDays(3), date.AddDays(15));

            manag.CheckIn(guest1, room1, date, date.AddDays(10));
            manag.CheckOut(guest3, room1, date, date.AddDays(10));

            Console.WriteLine(manag.FreePlacesAmount(date, date.AddDays(15)));
            manag.WriteInFile();*/

            RoomManagment managment = new RoomManagment();

            while (true)
            {
                Console.WriteLine(@"1 - Добавить номер
2 - Удалить номер
3 - Добавить пользователя
4 - Забронировать номер на дату (от - до)
5 - Зарегестрировать вьезд
6 - Зарегестрировать выезд
7 - Количество свободных мест на дату (от - до)
8 - Выход");

                int action = Convert.ToInt32(Console.ReadLine());

                switch (action)
                {
                    case 1:
                        Console.WriteLine("#Номер (Enter)");                       
                        Console.WriteLine("Цена (Enter)");                       
                        Console.WriteLine("Кол-во мест (Enter)");
                        Console.WriteLine("Категория (Enter)");

                        int number1 = Convert.ToInt32(Console.ReadLine());
                        double price1 = Convert.ToDouble(Console.ReadLine());
                        int places1 = Convert.ToInt32(Console.ReadLine());
                        string category1 = Console.ReadLine();

                        Room room1 = new Room(number1, price1, places1, category1);
                        managment.AddRoom(room1);
                    break;
                    case 2:
                        Console.WriteLine("#Номер комнаты (Enter)");
                        int number2 = Convert.ToInt32(Console.ReadLine());

                        managment.DeleteRoom(number2);
                    break;
                    case 3:
                        Console.WriteLine("ФИО (Enter)");
                        Console.WriteLine("Дата рождения - 1111.11.11  (Enter)");
                        Console.WriteLine("Место прописки (Enter)");

                        string fullname3 = Console.ReadLine();
                        DateTime birthDate3 = Convert.ToDateTime(Console.ReadLine());
                        string residence3 = Console.ReadLine();

                        Guest guest3 = new Guest(fullname3, birthDate3, residence3);
                        managment.AddGuest(guest3);
                    break;
                    case 4:
                        Console.WriteLine("#Номер комнаты (Enter)");
                        Console.WriteLine("ФИО (Enter)");
                        Console.WriteLine("От 1111.11.11 (Enter)");
                        Console.WriteLine("До 2222.22.22 (Enter)");

                        int number4 = Convert.ToInt32(Console.ReadLine());
                        string fullname4 = Console.ReadLine();
                        string[] startDate4 = Console.ReadLine().Split('.');
                        string[] endDate4 = Console.ReadLine().Split('.');

                        Room room4 = managment.GetRoomByNumber(number4);
                        Guest guest4 = managment.GetGuestByNumber(fullname4);
                        DateTime entry4 = new DateTime(Convert.ToInt32(startDate4[0]), Convert.ToInt32(startDate4[1]), Convert.ToInt32(startDate4[2]));
                        DateTime departure4 = new DateTime(Convert.ToInt32(endDate4[0]), Convert.ToInt32(endDate4[1]), Convert.ToInt32(endDate4[2]));

                        managment.BookRoom(room4, guest4, entry4, departure4);
                    break;
                    case 5:
                        Console.WriteLine("#Номер комнаты (Enter)");
                        Console.WriteLine("ФИО (Enter)");
                        Console.WriteLine("От 1111.11.11 (Enter)");
                        Console.WriteLine("До 2222.22.22 (Enter)");

                        int number5 = Convert.ToInt32(Console.ReadLine());
                        string fullname5 = Console.ReadLine();
                        string[] startDate5 = Console.ReadLine().Split('.');
                        string[] endDate5 = Console.ReadLine().Split('.');

                        Room room5 = managment.GetRoomByNumber(number5);
                        Guest guest5 = managment.GetGuestByNumber(fullname5);
                        DateTime entry5 = new DateTime(Convert.ToInt32(startDate5[0]), Convert.ToInt32(startDate5[1]), Convert.ToInt32(startDate5[2]));
                        DateTime departure5 = new DateTime(Convert.ToInt32(endDate5[0]), Convert.ToInt32(endDate5[1]), Convert.ToInt32(endDate5[2]));

                        managment.CheckIn(guest5, room5, entry5, departure5);
                        break;
                    case 6:
                        Console.WriteLine(" #Номер комнаты (Enter)");
                        Console.WriteLine("ФИО (Enter)");
                        Console.WriteLine("От 1111.11.11 (Enter)");
                        Console.WriteLine("До 2222.22.22 (Enter)");

                        int number6 = Convert.ToInt32(Console.ReadLine());
                        string fullname6 = Console.ReadLine();
                        string[] startDate6 = Console.ReadLine().Split('.');
                        string[] endDate6 = Console.ReadLine().Split('.');

                        Room room6 = managment.GetRoomByNumber(number6);
                        Guest guest6 = managment.GetGuestByNumber(fullname6);
                        DateTime entry6 = new DateTime(Convert.ToInt32(startDate6[0]), Convert.ToInt32(startDate6[1]), Convert.ToInt32(startDate6[2]));
                        DateTime departure6 = new DateTime(Convert.ToInt32(endDate6[0]), Convert.ToInt32(endDate6[1]), Convert.ToInt32(endDate6[2]));

                        managment.CheckOut(guest6, room6, entry6, departure6);
                        break;
                    case 7:
                        Console.WriteLine("От 1111.11.11 (Enter)");
                        Console.WriteLine("До 2222.22.22 (Enter)");

                        string[] startDate7 = Console.ReadLine().Split('.');
                        string[] endDate7 = Console.ReadLine().Split('.');

                        DateTime entry7 = new DateTime(Convert.ToInt32(startDate7[0]), Convert.ToInt32(startDate7[1]), Convert.ToInt32(startDate7[2]));
                        DateTime departure7 = new DateTime(Convert.ToInt32(endDate7[0]), Convert.ToInt32(endDate7[1]), Convert.ToInt32(endDate7[2]));

                        Console.WriteLine(managment.FreePlacesAmount(entry7, departure7));
                        break;
                    case 8:
                        managment.WriteInFile();
                        Environment.Exit(0);
                        break;

                }
            }
        }
    }
}
