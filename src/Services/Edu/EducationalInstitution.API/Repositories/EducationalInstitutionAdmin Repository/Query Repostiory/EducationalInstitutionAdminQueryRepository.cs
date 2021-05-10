using Dapper;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository.Query_Repostiory
{
    public class EducationalInstitutionAdminQueryRepository : IEducationalInstitutionAdminQueryRepository
    {
        public readonly string dbConnection;

        public EducationalInstitutionAdminQueryRepository(string connectionString = null)
        {
            if (!string.IsNullOrEmpty(connectionString))
                dbConnection = connectionString;
            else
                dbConnection = ConfigurationHelper.GetCurrentSettings("ConnectionStrings:ConnectionToWriteDB") ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<GetAllEducationalInstitutionAdminsQueryResult> GetAllAdminsForEducationalInstitutionAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
        {
            await using (var connection = new SqlConnection(dbConnection))
            {
                await connection.OpenAsync(cancellationToken);

                var queryResult = await connection.QueryAsync<Guid>(@"
                                                                SELECT AdminID
                                                                FROM Admins
                                                                WHERE EducationalInstitutionID=@ID AND EntityAccess_IsDisabled=0",
                                                                new { ID = educationalInstitutionID });

                return new() { AdminsIDs = queryResult.ToList() };
            }
        }
    }
}