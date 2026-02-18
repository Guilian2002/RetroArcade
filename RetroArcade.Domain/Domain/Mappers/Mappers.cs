using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RetroArcade.Domain.Domain.Mappers
{
    internal static class Mappers
    {
        #region Account Mappers
        internal static Account ToAccount(this IDataRecord record)
        {
            string roleString = (string)record["Role"];
            Enum.TryParse(roleString, out Role roleResult);

            return new Account(
                (Guid)record["Id"],
                (string)record["Firstname"],
                (string)record["Lastname"],
                (string)record["Username"],
                (string)record["Email"],
                (string)record["Password"],
                roleResult
            );
        }
        internal static Account ToAccountLogin(this IDataRecord record)
        {
            string roleString = (string)record["Role"];
            Enum.TryParse(roleString, out Role roleResult);

            return new Account(
                (Guid)record["Id"],
                (string)record["Firstname"],
                (string)record["Lastname"],
                (string)record["Username"],
                (string)record["Email"],
                roleResult
            );
        }
        #endregion
        #region Building Mapper
        internal static Building ToBuilding(this IDataRecord record)
        {
            return new Building(
                (Guid)record["Id"],
                (string)record["Name"],
                (TimeSpan)record["OpeningHour"],
                (TimeSpan)record["ClosingHour"],
                (string)record["Street"],
                (string)record["Number"],
                (string)record["PostalCode"],
                (string)record["City"],
                (string)record["Country"]
            );
        }

        internal static Building ToBuildingWithRoom(this IDataRecord record)
        {
            List<Room> rooms = new List<Room>();

            if (record["RoomId"] != DBNull.Value)
            {
                var room = new Room(
                    (Guid)record["RoomId"],
                    (string)record["RoomName"],
                    (int)record["RoomNumber"],
                    (decimal)record["RoomPrice"],
                    new Building(),
                    (int)record["RoomMachineCapacity"]
                );

                rooms.Add(room);
            }
            return new Building(
                (Guid)record["BuildingId"],
                (string)record["BuildingName"],
                (TimeSpan)record["BuildingOpeningHour"],
                (TimeSpan)record["BuildingClosingHour"],
                (string)record["BuildingStreet"],
                (string)record["BuildingNumber"],
                (string)record["BuildingPostalCode"],
                (string)record["BuildingCity"],
                (string)record["BuildingCountry"],
                rooms
            );
        }
        #endregion
        #region Room Mapper
        public static Room ToRoom(this IDataRecord record)
        {
            var building = new Building(
                (Guid)record["BuildingId"],
                (string)record["BuildingName"],
                default, 
                default,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty
            );

            return new Room(
                (Guid)record["RoomId"],
                (string)record["RoomName"],
                (int)record["Number"],
                (Decimal)record["Price"],
                building,
                (int)record["MachineCapacity"]
            );
        }

        public static Room ToRoomWithMachines(this IDataRecord record)
        {
            Room room = new Room();
            List<RoomArcadeMachine> machines = new List<RoomArcadeMachine>();

            if (record["MachineId"] != DBNull.Value)
            {
                var arcadeMachine = new ArcadeMachine(
                    (Guid)record["MachineId"],
                    (string)record["MachineName"],
                    (string)record["MachineGameName"]
                );

                var roomMachine = new RoomArcadeMachine(
                    (string)record["ArcadeMachineState"],
                    (DateTime)record["ArcadeMachineInstallationDate"],
                    room,
                    arcadeMachine
                );

                machines.Add(roomMachine);
            }

            Building building = new Building(
                (Guid)record["BuildingId"],
                (string)record["BuildingName"],
                (TimeSpan)record["BuildingOpeningHour"],
                (TimeSpan)record["BuildingClosingHour"],
                (string)record["BuildingStreet"],
                (string)record["BuildingNumber"],
                (string)record["BuildingPostalCode"],
                (string)record["BuildingCity"],
                (string)record["BuildingCountry"]
            );

            room = new Room(
                (Guid)record["RoomId"],
                (string)record["RoomName"],
                (int)record["RoomNumber"],
                (decimal)record["RoomPrice"],
                building,
                machines,
                (int)record["RoomMachineCapacity"]
            );

            return room;
        }
        #endregion

        #region Booking Mapper
        public static Booking ToBooking(this IDataRecord record)
        {
            string statusString = (string)record["BookingStatus"];
            Enum.TryParse(statusString, out Status statusResult);

            return new Booking(
                (Guid)record["BookingId"],
                (DateTime)record["BookingBeginDate"],
                (DateTime)record["BookingEndDate"],
                (int)record["BookingGroupSize"],
                statusResult,
                (Decimal)record["BookingPrice"],
                new Room(
                    (Guid)record["RoomId"],
                    (string)record["RoomName"],
                    (int)record["RoomNumber"],
                    (Decimal)record["RoomPrice"],
                    new Building(
                        (Guid)record["BuildingId"],
                        (string)record["BuildingName"],
                        (TimeSpan)record["BuildingOpeningHour"],
                        (TimeSpan)record["BuildingClosingHour"],
                        (string)record["BuildingStreet"],
                        (string)record["BuildingNumber"],
                        (string)record["BuildingPostalCode"],
                        (string)record["BuildingCity"],
                        (string)record["BuildingCountry"]
                    ),
                    (int)record["RoomMachineCapacity"]
                )
            );
        }
        #endregion
    }
}
