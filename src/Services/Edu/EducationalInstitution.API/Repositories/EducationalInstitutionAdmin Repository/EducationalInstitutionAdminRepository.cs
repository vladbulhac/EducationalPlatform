using Dapper;
using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository
{
    public class EducationalInstitutionAdminRepository : IEducationalInstitutionAdminRepository
    {
        public readonly DataContext context;
        public readonly string dbConnection;

        public EducationalInstitutionAdminRepository(DataContext context) => this.context = context ?? throw new ArgumentNullException(nameof(context));

        public EducationalInstitutionAdminRepository(string connectionString = null)
        {
            if (!string.IsNullOrEmpty(connectionString))
                dbConnection = connectionString;
            else
                dbConnection = ConfigurationHelper.GetCurrentSettings("ConnectionStrings:ConnectionToWriteDB") ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public EducationalInstitutionAdminRepository(DataContext context, string connectionString = null)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));

            if (!string.IsNullOrEmpty(connectionString))
                dbConnection = connectionString;
            else
                dbConnection = ConfigurationHelper.GetCurrentSettings("ConnectionStrings:ConnectionToWriteDB") ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<bool> DeleteAsync(Guid adminID, Guid educationalInstitutionID, CancellationToken cancellationToken = default)
        {
            var admin = await context.Admins.Where(a => a.AdminID == adminID && a.EducationalInstitutionID == educationalInstitutionID)
                                     .SingleOrDefaultAsync(cancellationToken);

            if (admin is null) return false;

            context.Remove(admin);
            return true;
        }

        public async Task<bool> DeleteAsync(ICollection<Guid> adminsIDs, Guid educationalInstitutionID, CancellationToken cancellationToken = default)
        {
            var admins = await context.Admins.Where(a => adminsIDs.Contains(a.AdminID) && a.EducationalInstitutionID == educationalInstitutionID)
                                             .ToListAsync(cancellationToken);

            if (admins is null || admins.Count == 0) return false;

            context.RemoveRange(admins);
            return true;
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