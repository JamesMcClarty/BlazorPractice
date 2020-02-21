using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data;
using Microsoft.Data.SqlClient;

namespace BlazorDapperCRUD.Data
{
    public class VideoService : IVideoService
    {
        //Connection
        private readonly SQLConnectionConfiguration _config;

        public VideoService(SQLConnectionConfiguration config)
        {
            _config = config;
        }

        //CREATE

        public async Task<bool> VideoInsert(Video video)
        {
            using (var connection = new SqlConnection(_config.Value))
            {
                var paramaters = new DynamicParameters();
                paramaters.Add("Title", video.Title, DbType.String);
                paramaters.Add("DatePublished", video.DatePublished, DbType.Date);
                paramaters.Add("IsActive", video.IsActive, DbType.Boolean);

                const string query = @"Insert into Video (Title, DatePublished, IsActive) values (@Title, @DatePublished, @IsActive)";
                await connection.ExecuteAsync(query, new { video.Title, video.DatePublished, video.IsActive }, commandType: CommandType.Text);

            }
            return true;
        }

    }
}
