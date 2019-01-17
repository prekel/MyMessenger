﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyMessenger.Server;

namespace MyMessenger.Server.Migrations
{
    [DbContext(typeof(MessengerContext))]
    partial class MessengerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MyMessenger.Server.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LoginDateTime");

                    b.Property<string>("Nickname")
                        .IsRequired();

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("PasswordSalt");

                    b.Property<DateTime>("RegistrationDateTime");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("MyMessenger.Server.Entities.AccountDialog", b =>
                {
                    b.Property<int>("AccountId");

                    b.Property<int>("DialogId");

                    b.HasKey("AccountId", "DialogId");

                    b.HasIndex("DialogId");

                    b.ToTable("AccountsDialogs");
                });

            modelBuilder.Entity("MyMessenger.Server.Entities.Dialog", b =>
                {
                    b.Property<int>("DialogId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("DialogId");

                    b.ToTable("Dialogs");
                });

            modelBuilder.Entity("MyMessenger.Server.Entities.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<int>("DialogId");

                    b.Property<DateTime>("SendDateTime");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("MessageId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("DialogId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("MyMessenger.Server.Entities.AccountDialog", b =>
                {
                    b.HasOne("MyMessenger.Server.Entities.Account", "Account")
                        .WithMany("Dialogs")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyMessenger.Server.Entities.Dialog", "Dialog")
                        .WithMany("Members")
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyMessenger.Server.Entities.Message", b =>
                {
                    b.HasOne("MyMessenger.Server.Entities.Account", "Author")
                        .WithMany("Messages")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyMessenger.Server.Entities.Dialog", "Dialog")
                        .WithMany("Messages")
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
