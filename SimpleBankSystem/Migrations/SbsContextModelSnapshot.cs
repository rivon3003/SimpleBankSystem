using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SimpleBankSystem.Data;

namespace SimpleBankSystem.Migrations
{
    [DbContext(typeof(SbsContext))]
    partial class SbsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SimpleBankSystem.Models.Entity.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountName");

                    b.Property<string>("AccountNumber");

                    b.Property<decimal>("Balance");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });
        }
    }
}
