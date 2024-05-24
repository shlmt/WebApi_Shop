using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    internal class RatingRepository : IRatingRepository
    {
        private IConfiguration _configuration;

        public RatingRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task insertRating(Rating rating)
        {
            string query = "INSERT INTO Rating(host, method, path, referer, user_agent, record_date)" + "VALUES(@host, @method, @path, @referer, @user_agent, @record_date)";

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("webApiProject")))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@host", SqlDbType.NVarChar, 50).Value = rating.Host;
                cmd.Parameters.Add("@method", SqlDbType.NChar, 10).Value = rating.Method;
                cmd.Parameters.Add("@path", SqlDbType.NVarChar, 50).Value = rating.Path;
                cmd.Parameters.Add("@referer", SqlDbType.NVarChar, 100).Value = rating.Referer;
                cmd.Parameters.Add("@user_agent", SqlDbType.NVarChar, 1000).Value = rating.UserAgent;
                cmd.Parameters.Add("@record_date", SqlDbType.DateTime).Value = rating.RecordDate;

                conn.Open();
                int rowsAffwcted = cmd.ExecuteNonQuery();
                conn.Close();

                return rowsAffwcted;
            }
        }
    }
}
