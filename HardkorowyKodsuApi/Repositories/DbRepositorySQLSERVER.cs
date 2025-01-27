using Dapper;
using System.Data;

namespace HardkorowyKodsuApi.Repositories
{
    public class DbRepositorySQLSERVER
    {
        private readonly IDbConnection _dbConnection;

        public DbRepositorySQLSERVER(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Gets all table and view names from the database.
        /// </summary>
        /// <returns>A list of table and view names.</returns>
        public virtual async Task<IEnumerable<string>> GetAllTablesAndViewsAsync()
        {
            const string query = @"
                SELECT TABLE_SCHEMA + '.' + TABLE_NAME AS FullName
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_TYPE IN ('BASE TABLE', 'VIEW')
                ORDER BY TABLE_SCHEMA, TABLE_NAME";

            return await _dbConnection.QueryAsync<string>(query);
        }

        /// <summary>
        /// Gets the structure of a table or view, given its name.
        /// </summary>
        /// <param name="tableName">The name of the table or view (schema.table format).</param>
        /// <returns>A list of column details, including name, type, and additional properties.</returns>
        public virtual async Task<IEnumerable<string>> GetTableOrViewStructureAsync(string tableName)
        {
            const string query = @"
                SELECT 
                    COLUMN_NAME
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA + '.' + TABLE_NAME = @TableName
                ORDER BY ORDINAL_POSITION";

            return await _dbConnection.QueryAsync<string>(query, new { TableName = tableName });
        }
    }
}
