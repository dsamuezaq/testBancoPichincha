using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using model.bp.dbo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model.bp
{
    public class ContextBancoPichincha : DbContext
    {
        private readonly string _connectionString;
        protected SqlConnection Connection;
        protected SqlConnection connection => Connection ?? (Connection = GetOpenConnection());

        public ContextBancoPichincha(DbContextOptions<ContextBancoPichincha> options)
                  : base(options)
        {
            _connectionString = options.FindExtension<SqlServerOptionsExtension>().ConnectionString;
        }
        public SqlConnection GetOpenConnection(bool mars = false)
        {

            var cs = _connectionString;
            if (mars)
            {
                var scsb = new SqlConnectionStringBuilder(cs)
                {
                    MultipleActiveResultSets = true
                };
                cs = scsb.ConnectionString;
            }
            var connection = new SqlConnection(cs);
            connection.Open();
            return connection;
        }
        public SqlConnection GetClosedConnection()
        {
            var conn = new SqlConnection(_connectionString);
            if (conn.State != ConnectionState.Closed) throw new InvalidOperationException("should be closed!");
            return conn;
        }

        /// <summary>
        /// Table Persona
        /// Creation :20220228
        /// </summary>
        public DbSet<Persona> Personas { get; set; }

        /// <summary>
        /// Table Cuenta
        /// Creation :20220228
        /// </summary>
        public DbSet<Cliente> Clientes { get; set; }

        /// <summary>
        /// Table Cuenta
        /// Creation :20220228
        /// </summary>
        public DbSet<Cuenta> Cuentas { get; set; }

        /// <summary>
        /// Table Cuenta
        /// Creation :20220228
        /// </summary>
        public DbSet<Movimiento> Movimientos { get; set; }
        



    }
}
