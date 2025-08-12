using Microsoft.EntityFrameworkCore;
using MyGameList.Models;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace MyGameList.Data
{
    public partial class MyGameListDbContext(DbContextOptions<MyGameListDbContext> options) : DbContext(options)
    {
        public DbSet<Games> Games => Set<Games>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName();
                if (tableName != null)
                {
                    entity.SetTableName(ToSnakeCase(tableName));
                }

                foreach (var property in entity.GetProperties())
                {
                    var columnName = property.GetColumnName();
                    if (columnName != null)
                    {
                        property.SetColumnName(ToSnakeCase(columnName));
                    }
                }

                foreach (var key in entity.GetKeys())
                {
                    var keyName = key.GetName();
                    if (keyName != null)
                    {
                        key.SetName(ToSnakeCase(keyName));
                    }
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    var constraintName = key.GetConstraintName();
                    if (constraintName != null)
                    {
                        key.SetConstraintName(ToSnakeCase(constraintName));
                    }
                }

                foreach (var index in entity.GetIndexes())
                {
                    var dbName = index.GetDatabaseName();
                    if (dbName != null)
                    {
                        index.SetDatabaseName(ToSnakeCase(dbName));
                    }
                }
            }
        }

        public string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = InitialUnderscoreRegex().Match(input);
            return startUnderscores + SnakeCaseRegex().Replace(input, "$1_$2").ToLower();
        }

        [GeneratedRegex(@"^_+")]
        private partial Regex InitialUnderscoreRegex();

        [GeneratedRegex(@"([a-z0-9])([A-Z])")]
        private partial Regex SnakeCaseRegex();
    }
}
