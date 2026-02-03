using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Mappers
{
    internal static class Mappers
    {
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
    }
}
