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

        internal static Account ToAccountList(this IDataRecord record)
        {
            string roleString = (string)record["AccountRole"];
            Enum.TryParse(roleString, out Role roleResult);

            return new Account(
                (Guid)record["AccountId"],
                (string)record["AccountFirstname"],
                (string)record["AccountLastname"],
                (string)record["AccountUsername"],
                (string)record["AccountEmail"],
                roleResult,
                record["AccountCreationDate"] == DBNull.Value ? null : (DateTime)record["AccountCreationDate"],
                record["AccountDisable"] == DBNull.Value ? null : (DateTime)record["AccountDisable"],
                (bool)record["AccountIsActive"]
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
                (string)record["Country"],
                default
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
                rooms,
                new Manager(
                    (Guid)record["ManagerId"],
                    (string)record["ManagerFirstname"],
                    (string)record["ManagerLastname"],
                    (string)record["ManagerUsername"],
                    (string)record["ManagerEmail"]
                )
            );
        }
        #endregion
        #region Room Mapper
        internal static Room ToRoom(this IDataRecord record)
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
                string.Empty,
                default
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

        internal static Room ToRoomWithMachines(this IDataRecord record)
        {
            Room room = new Room();
            List<RoomArcadeMachine> machines = new List<RoomArcadeMachine>();
            List<RoomFeedback> feedbacks = new List<RoomFeedback>();

            if (record["MachineId"] != DBNull.Value)
            {
                var arcadeMachine = new ArcadeMachine(
                    (Guid)record["MachineId"],
                    (string)record["MachineName"],
                    (string)record["MachineGameName"],
                    new Categorie(
                        (Guid)record["CategorieId"],
                        (string)record["CategorieName"]
                    )
                );

                var roomMachine = new RoomArcadeMachine(
                    (string)record["ArcadeMachineState"],
                    (DateTime)record["ArcadeMachineInstallationDate"],
                    room,
                    arcadeMachine
                );

                machines.Add(roomMachine);
            }

            if (record["RoomFeedbackId"] != DBNull.Value)
            {
                var roomFeedback = new RoomFeedback(
                    (Guid)record["RoomFeedbackId"],
                    (int)record["RoomFeedbackStars"],
                    (DateTime)record["RoomFeedbackCommentDate"],
                    (string)record["RoomFeedbackComment"],
                    (string)record["RoomFeedbackUsername"]
                );

                feedbacks.Add(roomFeedback);
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
                (string)record["BuildingCountry"],
                new Manager(
                    (Guid)record["ManagerId"],
                    (string)record["ManagerFirstname"],
                    (string)record["ManagerLastname"],
                    (string)record["ManagerUsername"],
                    (string)record["ManagerEmail"]
                )
            );

            room = new Room(
                (Guid)record["RoomId"],
                (string)record["RoomName"],
                (int)record["RoomNumber"],
                (decimal)record["RoomPrice"],
                building,
                machines,
                feedbacks,
                (int)record["RoomMachineCapacity"]
            );

            return room;
        }
        #endregion
        #region Booking Mapper
        internal static Booking ToBookingRoom(this IDataRecord record)
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
                    Guid.Empty,
                    (string)record["RoomName"],
                    (int)record["RoomNumber"],
                    default,
                    new Building(
                        Guid.Empty,
                        (string)record["BuildingName"],
                        default,
                        default,
                        "",
                        "",
                        "",
                        (string)record["BuildingCity"],
                        "",
                        default
                    ),
                    default
                )
            );
        }
        internal static Booking ToBooking(this IDataRecord record)
        {
            string statusString = (string)record["BookingStatus"];
            Enum.TryParse(statusString, out Status statusResult);

            return new Booking(
                (Guid)record["BookingId"],
                (DateTime)record["BookingBeginDate"],
                (DateTime)record["BookingEndDate"],
                (int)record["BookingGroupSize"],
                statusResult,
                (decimal)record["BookingPrice"],
                new Room(
                    Guid.Empty,
                    (string)record["RoomName"],
                    (int)record["RoomNumber"],
                    (decimal)record["BookingPrice"],
                    new Building(
                        Guid.Empty,
                        (string)record["BuildingName"],
                        default, default, "", "", "",
                        (string)record["BuildingCity"],
                        "", default
                    ),
                    new List<RoomArcadeMachine>(),
                    new List<RoomFeedback>(),
                    10
                )
            );
        }

        internal static Booking ToBookingList(this IDataRecord record)
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
                    string.Empty,
                    default,
                    default,
                    new Building()
                )
            );
        }
        #endregion
        #region ArcadeMachine Mappers
        internal static ArcadeMachine ToArcadeMachineList(this IDataRecord record)
        {
            return new ArcadeMachine(
                (Guid)record["ArcadeMachineId"],
                (string)record["Name"],
                (string)record["GameName"],
                new Categorie(
                    (Guid)record["CategorieId"],
                    (string)record["CategorieName"]
                )
            );
        }
        #endregion
    }
}
