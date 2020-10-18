using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using TagTeam.ShoppingCart.Domain;
using TagTeam.ShoppingCart.Domain.CustomModels;
using TagTeam.ShoppingCart.Service.Interfaces;

namespace TagTeam.ShoppingCart.Service
{
    public class TestService : ITest_Interface
    {
        private readonly string _connectionString;

        public TestService(string connectionString)
        {
            _connectionString = connectionString;

        }

        public async Task<BaseModel> Select()
        {
            try
            {

                //using (var connection = new SqlConnection(_connectionString))
                //{
                //    DynamicParameters para = new DynamicParameters();
                //    string JsonData = JsonConvert.SerializeObject(null);
                //    para.Add("@JsonData", JsonData, DbType.String);
                //    para.Add("@Operation", "I", DbType.String);
                //    para.Add("@SkipApproval", 0, DbType.Boolean);

                //    await connection.ExecuteAsync("[cal].[InsertCallCenterOfficer]", para, commandType: System.Data.CommandType.StoredProcedure);

                //}
                Test test = new Test();
                test.testValue1 = "Test Data 1";
                test.testValue2 = "Test Data 2";
                return new BaseModel() { code = "1000", description = "Success", data = test };
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = null };
            }

        }
    }
}
