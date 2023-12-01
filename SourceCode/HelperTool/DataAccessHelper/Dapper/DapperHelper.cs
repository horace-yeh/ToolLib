using Dapper;
using System.Data;
using static Dapper.SqlMapper;

namespace DataAccessHelper.Dapper
{
    #region simple

    //public class Store
    //{
    //    public string Name { get; set; } = string.Empty;
    //    public int BusinessEntityID { get; set; }
    //}

    //private Task simple()
    //{
    //    var input = new Store() { Name = "Next-Door Bike Store", BusinessEntityID = 123 };
    //    var sql = @"select * from [Sales].[Store] Where BusinessEntityID = @BusinessEntityID And Name = @Name";
    //    var pms = new DynamicParameters();
    //    pms.Add("Name", input.Name, _dbTypeMappingDict[input.Name.GetType()], size: 50);
    //    pms.Add("BusinessEntityID", input.BusinessEntityID, _dbTypeMappingDict[input.BusinessEntityID.GetType()]);
    //    var data = await DapperQueryAsync<Store>(sql, pms);
    //}

    #endregion simple

    public class DapperHelper
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly Dictionary<Type, DbType> _dbTypeMappingDict;

        public DapperHelper(ConnectionFactory connectionFactoty)
        {
            _connectionFactory = connectionFactoty;
            _dbTypeMappingDict = GetDbTypeMappingDict();
        }

        public async Task<int> NonQueryAsync(string sql, IDynamicParameters pms)
        {
            var effectCounter = 0;
            using (var conn = _connectionFactory.CreateConnection())
            {
                effectCounter = await conn.ExecuteAsync(sql, pms);
            }
            return effectCounter;
        }

        public async Task<int> NonQuerySpAsync(string sql, IDynamicParameters pms)
        {
            var effectCounter = 0;
            using (var conn = _connectionFactory.CreateConnection())
            {
                effectCounter = await conn.ExecuteAsync(sql, pms, commandType: CommandType.StoredProcedure);
            }
            return effectCounter;
        }

        public async Task<IEnumerable<T>> DapperQueryAsync<T>(string sql, IDynamicParameters pms, int? timeout = null)
        {
            using (var conn = _connectionFactory.CreateConnection())
            {
                var temp = await conn.QueryAsync<T>(sql, pms, commandTimeout: timeout);
                return temp;
            }
        }

        public async Task<IEnumerable<T>> QuerySpAsync<T>(string sql, IDynamicParameters pms, int? timeout = null)
        {
            using (var conn = _connectionFactory.CreateConnection())
            {
                var temp = await conn.QueryAsync<T>(sql, pms, commandType: CommandType.StoredProcedure, commandTimeout: timeout);
                return temp;
            }
        }

        #region DbType Description

        private static Dictionary<Type, DbType> GetDbTypeMappingDict()
        {
            var typeMap = new Dictionary<Type, DbType>();
            typeMap[typeof(byte)] = DbType.Byte;
            typeMap[typeof(sbyte)] = DbType.SByte;
            typeMap[typeof(short)] = DbType.Int16;
            typeMap[typeof(ushort)] = DbType.UInt16;
            typeMap[typeof(int)] = DbType.Int32;
            typeMap[typeof(uint)] = DbType.UInt32;
            typeMap[typeof(long)] = DbType.Int64;
            typeMap[typeof(ulong)] = DbType.UInt64;
            typeMap[typeof(float)] = DbType.Single;
            typeMap[typeof(double)] = DbType.Double;
            typeMap[typeof(decimal)] = DbType.Decimal;
            typeMap[typeof(bool)] = DbType.Boolean;
            typeMap[typeof(string)] = DbType.String;
            typeMap[typeof(char)] = DbType.StringFixedLength;
            typeMap[typeof(Guid)] = DbType.Guid;
            typeMap[typeof(DateTime)] = DbType.DateTime;
            typeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
            typeMap[typeof(byte[])] = DbType.Binary;
            typeMap[typeof(byte?)] = DbType.Byte;
            typeMap[typeof(sbyte?)] = DbType.SByte;
            typeMap[typeof(short?)] = DbType.Int16;
            typeMap[typeof(ushort?)] = DbType.UInt16;
            typeMap[typeof(int?)] = DbType.Int32;
            typeMap[typeof(uint?)] = DbType.UInt32;
            typeMap[typeof(long?)] = DbType.Int64;
            typeMap[typeof(ulong?)] = DbType.UInt64;
            typeMap[typeof(float?)] = DbType.Single;
            typeMap[typeof(double?)] = DbType.Double;
            typeMap[typeof(decimal?)] = DbType.Decimal;
            typeMap[typeof(bool?)] = DbType.Boolean;
            typeMap[typeof(char?)] = DbType.StringFixedLength;
            typeMap[typeof(Guid?)] = DbType.Guid;
            typeMap[typeof(DateTime?)] = DbType.DateTime;
            typeMap[typeof(DateTimeOffset?)] = DbType.DateTimeOffset;
            return typeMap;
        }

        #endregion DbType Description
    }
}
