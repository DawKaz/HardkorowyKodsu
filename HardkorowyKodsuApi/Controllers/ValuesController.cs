using HardkorowyKodsuApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HardkorowyKodsuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbSchemaController : ControllerBase
    {
        private readonly DbRepositorySQLSERVER _repository;

        public DbSchemaController(DbRepositorySQLSERVER repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all table and view names from the database.
        /// </summary>
        /// <returns>A list of table and view names.</returns>
        [HttpGet("tables-and-views")]
        public async Task<IActionResult> GetAllTablesAndViews()
        {
            try
            {
                var tablesAndViews = await _repository.GetAllTablesAndViewsAsync();
                return Ok(tablesAndViews);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = "Error occurred while getting tables and views.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Gets the structure of a specific table or view.
        /// </summary>
        /// <param name="tableName">The name of the table or view</param>
        /// <returns>The structure of the table or view.</returns>
        [HttpGet("structure")]
        public async Task<IActionResult> GetTableOrViewStructure([FromQuery] string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                return BadRequest(new { Message = "Table name is required." });

            try
            {
                var structure = await _repository.GetTableOrViewStructureAsync(tableName);

                if (structure == null || !structure.GetEnumerator().MoveNext())
                    return NotFound(new { Message = $"No structure found for table or view '{tableName}'." });

                return Ok(structure);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching the table or view structure.", Details = ex.Message });
            }
        }
    }
}
