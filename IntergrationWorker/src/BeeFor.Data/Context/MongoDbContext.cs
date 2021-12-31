using BeeFor.Domain.Entities.MongoDb;
using MongoDB.Driver;
using System;

namespace BeeFor.Data.Context
{
    public class MongoDbContext
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }

        private IMongoDatabase _database { get; }

        public MongoDbContext()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
            }
        }

        public IMongoCollection<CardLog> CardLogs
        {
            get
            {
                return _database.GetCollection<CardLog>("CardLogs");
            }
        }

        public IMongoCollection<LogAcaoPrincipal> LogsAcoesPrincipais
        {
            get
            {
                return _database.GetCollection<LogAcaoPrincipal>("LogsAcoesPrincipais");
            }
        }

        public IMongoCollection<LogIteracaoCard> LogsIteracoesCards
        {
            get
            {
                return _database.GetCollection<LogIteracaoCard>("LogsIteracoesCards");
            }
        }

        public IMongoCollection<CardEntregueLog> LogsCardsEntregues
        {
            get
            {
                return _database.GetCollection<CardEntregueLog>("LogsCardsEntregues");
            }
        }
    }
}
