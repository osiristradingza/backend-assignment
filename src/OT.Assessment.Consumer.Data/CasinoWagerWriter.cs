using Dapper;
using Microsoft.Extensions.Configuration;
using OT.Assessment.Consumer.Api.Entities;
using System.Data;
using System.Data.SqlClient;

namespace OT.Assessment.Consumer.Data
{
    public interface ICasinoWagerWriter
    {
        void Save(CasinoWager aCasinoWager);
    }
    public class CasinoWagerWriter : DataAccesBase, ICasinoWagerWriter
    {
        public CasinoWagerWriter() : base()
        {
            
        }
        public void Save(CasinoWager aCasinoWager)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Theme", aCasinoWager.Theme);
                parameters.Add("Provider", aCasinoWager.Provider);
                parameters.Add("GameName", aCasinoWager.GameName);
                parameters.Add("TransactionId", aCasinoWager.TransactionId);
                parameters.Add("BrandId", aCasinoWager.BrandId);
                parameters.Add("AccountId", aCasinoWager.AccountId);
                parameters.Add("Username", aCasinoWager.Username);
                parameters.Add("ExternalReferenceId", aCasinoWager.ExternalReferenceId);
                parameters.Add("TransactionTypeId", aCasinoWager.TransactionTypeId);
                parameters.Add("Amount", aCasinoWager.Amount);
                parameters.Add("CreatedDateTime", aCasinoWager.CreatedDateTime);
                parameters.Add("NumberOfBets", aCasinoWager.NumberOfBets);
                parameters.Add("CountryCode", aCasinoWager.CountryCode);
                parameters.Add("SessionData", aCasinoWager.SessionData);
                parameters.Add("Duration", aCasinoWager.Duration);
                var rowsAffected = connection.Execute("spInsertCasinoWager", parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
