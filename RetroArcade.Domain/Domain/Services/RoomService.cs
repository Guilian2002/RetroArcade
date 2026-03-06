using BStorm.Tools.Database;
using RetroArcade.Domain.CustomErrors;
using RetroArcade.Domain.Domain.Commands.RoomCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Mappers;
using RetroArcade.Domain.Domain.Queries.BuildingQueries;
using RetroArcade.Domain.Domain.Queries.RoomQueries;
using RetroArcade.Domain.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Results;

namespace RetroArcade.Domain.Domain.Services
{
    public class RoomService : IRoomRepository
    {
        private readonly DbConnection _dbConnection;

        public RoomService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
        }

        public CqsResult<IEnumerable<Room>> Execute(GetAllRoomsQuery query)
        {
            try
            {
                return _dbConnection.ExecuteReader("SP_Room_Get_All",
                    dr => dr.ToRoom(), true, parameters:query).ToList();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult<Room> Execute(GetRoomByIdQuery query)
        {
            try
            {
                var room = _dbConnection.ExecuteReader("SP_Room_Get",
                    dr => dr.ToRoomWithMachines(), true, parameters: query);

                var finalRoom = room
                    .GroupBy(r => r.Id)
                    .Select(group => {
                        var firstRoom = group.First();
                        firstRoom.RoomArcadeMachines = group
                            .SelectMany(r => r.RoomArcadeMachines)
                            .ToList();
                        return firstRoom;
                    })
                    .FirstOrDefault();

                if (finalRoom is null)
                    return Errors.RoomNotFound;

                return finalRoom;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult Execute(AddRoomCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_Room_Insert", true, command);

                if (rows is 0)
                    return Error.Create("Erreur lors de l'insertion");

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult Execute(UpdateRoomCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_Room_Update", true, parameters: command);

                if (rows == 0)
                    return Errors.RoomNotFound;

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult Execute(DeleteRoomCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_Room_Delete", true, parameters: command);

                if (rows == 0)
                    return Errors.BookingNotFound;

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
