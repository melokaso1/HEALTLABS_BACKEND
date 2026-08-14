using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <summary>
    /// One-shot data wipe: empties all public application tables while keeping schema
    /// and __EFMigrationsHistory. SeedAllAsync re-fills catalogs/demo users on API start.
    /// </summary>
    [DbContext(typeof(AppDbContext))]
    [Migration("20260814200000_WipeAllData")]
    public partial class WipeAllData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Truncate every public table except EF history. CASCADE handles FKs;
            // RESTART IDENTITY resets serial/identity sequences where present.
            migrationBuilder.Sql("""
                DO $$
                DECLARE
                    stmt text;
                BEGIN
                    SELECT string_agg(format('%I.%I', schemaname, tablename), ', ')
                    INTO stmt
                    FROM pg_tables
                    WHERE schemaname = 'public'
                      AND tablename <> '__EFMigrationsHistory';

                    IF stmt IS NOT NULL THEN
                        EXECUTE 'TRUNCATE TABLE ' || stmt || ' RESTART IDENTITY CASCADE';
                    END IF;
                END $$;
                """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Data cannot be restored; no-op.
        }
    }
}
