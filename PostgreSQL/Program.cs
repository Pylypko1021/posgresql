using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data.Common;


namespace PostgreSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            String connection_fly = "Server=localhost;Port=5432;User id=postgres;Password=gegyhegut619;Database=Fly Booking;";
            
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connection_fly);
            
            npgSqlConnection.Open();
           
            Console.WriteLine("Соединение с БД1 открыто");
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM fly", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            if (npgSqlDataReader.HasRows)
            {
                Console.WriteLine("Таблица: fly");
                
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                    Console.WriteLine(dbDataRecord["Booking id"] + "   " + dbDataRecord["Client Name"] + "   " + dbDataRecord["Fly Number"] + "   " + dbDataRecord["From"] + "   " + dbDataRecord["To"] + "   " + dbDataRecord["Date"]);
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            */
            String connection_hotel = "Server=localhost;Port=5432;User id=postgres;Password=gegyhegut619;Database=Hotel_Booking;";
            NpgsqlConnection hotel = new NpgsqlConnection(connection_hotel);
            hotel.Open();
            Console.WriteLine("Соединение с БД2 открыто");
            int id = 0;
            String client_name = "";
            String hotel_name = "Klinton";
            DateTime arrival = DateTime.Now;
            DateTime departure = DateTime.Now;
            Console.Write("Insert id= ");
            id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Insert client_name= ");
            client_name = Convert.ToString(Console.ReadLine());
            Console.Write("Insert hotel_name= ");
            hotel_name = Convert.ToString(Console.ReadLine());
            Console.Write("Insert arrival= ");
            arrival = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Insert department= ");
            departure = Convert.ToDateTime(Console.ReadLine());
            NpgsqlCommand comm = new NpgsqlCommand("BEGIN; INSERT INTO hotel(booking_id, client_name, hotel_name, arrival, departure) VALUES (@id, @client ,@hotel, @arrival, @departure)", hotel);
            NpgsqlParameter npgSqlParameterId = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer);
            NpgsqlParameter npgSqlParameterClient = new NpgsqlParameter("@client", NpgsqlTypes.NpgsqlDbType.Varchar);
            NpgsqlParameter npgSqlParameterHotel = new NpgsqlParameter("@hotel", NpgsqlTypes.NpgsqlDbType.Varchar);
            NpgsqlParameter npgSqlParameterArrival = new NpgsqlParameter("@arrival", NpgsqlTypes.NpgsqlDbType.Date);
            NpgsqlParameter npgSqlParameterDeparture = new NpgsqlParameter("@departure", NpgsqlTypes.NpgsqlDbType.Date);
            npgSqlParameterId.Value = id;
            npgSqlParameterClient.Value = client_name;
            npgSqlParameterHotel.Value = hotel_name;
            npgSqlParameterArrival.Value = arrival;
            npgSqlParameterDeparture.Value = departure;
            int free_space = 0;
            comm.Parameters.AddRange(new NpgsqlParameter[] { npgSqlParameterId,
            npgSqlParameterClient, npgSqlParameterHotel, npgSqlParameterArrival, npgSqlParameterDeparture});
            int count = comm.ExecuteNonQuery();
            if (count == 1)
                Console.WriteLine("Запись вставлена");
            else
                Console.WriteLine("Не удалось вставить новую запись");
            String connection_space = "Server=localhost;Port=5432;User id=postgres;Password=gegyhegut619;Database=free_space;";
            NpgsqlConnection space = new NpgsqlConnection(connection_space);
            space.Open();
            NpgsqlCommand space_comm1 = new NpgsqlCommand("SELECT * FROM space_free", space);
            NpgsqlDataReader npgSqlDataReader2 = space_comm1.ExecuteReader();
            
            if (npgSqlDataReader2.HasRows)
            {

                while (npgSqlDataReader2.Read())
                {
                    free_space = npgSqlDataReader2.GetInt32(0);
                }
            }
            npgSqlDataReader2.Close();
            if (free_space < 0)
            {
                NpgsqlCommand comm1 = new NpgsqlCommand("ROLLBACK", hotel);
                NpgsqlDataReader npgSqlDataReader4 = comm1.ExecuteReader();
                npgSqlDataReader4.Close();
                Console.WriteLine("Транзакция неуспешна");
            }
            else
            {
                NpgsqlCommand space_comm = new NpgsqlCommand("UPDATE space_free set num = num-1", space);
                //NpgsqlCommand hotel_comm = new NpgsqlCommand("SELECT * FROM free_space", hotel);
                NpgsqlDataReader npgSqlDataReader1 = space_comm.ExecuteReader();
                npgSqlDataReader1.Close();
                NpgsqlCommand comm2 = new NpgsqlCommand("COMMIT", hotel);
                NpgsqlDataReader npgSqlDataReader5 = comm2.ExecuteReader();
                npgSqlDataReader5.Close();
                Console.WriteLine("Транзакция успешна");
            }




            space.Close();
            /*
            foreach (DbDataRecord dbDataRecord1 in npgSqlDataReader1)
                if (hotel_name==dbDataRecord1["hotel_name"])
                    {
                    Console.WriteLine("REPLAY");
                    }
            */
            //  }
            // else
            //   Console.WriteLine("Запрос не вернул строк");




            Console.ReadKey(true);
        
        }
    }
}
