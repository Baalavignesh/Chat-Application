﻿// <auto-generated />
using System;
using Chat_Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chat_Application.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230504151256_single-chat-table-updated")]
    partial class singlechattableupdated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Chat_Application.Models.GroupChat", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"));

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId");

                    b.HasIndex("AdminId");

                    b.ToTable("GroupChat");
                });

            modelBuilder.Entity("Chat_Application.Models.GroupChatMessage", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("GroupSenderId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("MessageId");

                    b.HasIndex("GroupId");

                    b.HasIndex("GroupSenderId");

                    b.ToTable("GroupChatMessage");
                });

            modelBuilder.Entity("Chat_Application.Models.GroupMembers", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberId"));

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MemberId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupMembers");
                });

            modelBuilder.Entity("Chat_Application.Models.MyChats", b =>
                {
                    b.Property<int>("MyChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MyChatId"));

                    b.Property<int>("ChatUserId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("SingleChatId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("MyChatId");

                    b.HasIndex("ChatUserId");

                    b.HasIndex("GroupId");

                    b.HasIndex("SingleChatId");

                    b.ToTable("MyChats");
                });

            modelBuilder.Entity("Chat_Application.Models.SingleChat", b =>
                {
                    b.Property<int>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChatId"));

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.HasKey("ChatId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("SingleChat");
                });

            modelBuilder.Entity("Chat_Application.Models.SingleChatMessage", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentChatId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("MessageId");

                    b.HasIndex("ParentChatId");

                    b.HasIndex("SenderId");

                    b.ToTable("SingleChatMessage");
                });

            modelBuilder.Entity("Chat_Application.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isOnline")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Chat_Application.Models.UserSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConnectionString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserSession");
                });

            modelBuilder.Entity("Chat_Application.Models.GroupChat", b =>
                {
                    b.HasOne("Chat_Application.Models.User", "Admin")
                        .WithMany("GroupChat")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("Chat_Application.Models.GroupChatMessage", b =>
                {
                    b.HasOne("Chat_Application.Models.GroupChat", "GroupChat")
                        .WithMany("GroupChatMessage")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Chat_Application.Models.User", "Sender")
                        .WithMany("GroupChatMessage")
                        .HasForeignKey("GroupSenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GroupChat");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Chat_Application.Models.GroupMembers", b =>
                {
                    b.HasOne("Chat_Application.Models.GroupChat", "GroupChat")
                        .WithMany("GroupMembers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Chat_Application.Models.User", "GroupMember")
                        .WithMany("GroupMembers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GroupChat");

                    b.Navigation("GroupMember");
                });

            modelBuilder.Entity("Chat_Application.Models.MyChats", b =>
                {
                    b.HasOne("Chat_Application.Models.User", "User")
                        .WithMany("MyChats")
                        .HasForeignKey("ChatUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Chat_Application.Models.GroupChat", "GroupChat")
                        .WithMany("MyChats")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Chat_Application.Models.SingleChat", "SingleChat")
                        .WithMany("MyChats")
                        .HasForeignKey("SingleChatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GroupChat");

                    b.Navigation("SingleChat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Chat_Application.Models.SingleChat", b =>
                {
                    b.HasOne("Chat_Application.Models.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chat_Application.Models.User", "Sender")
                        .WithMany("SingleChat")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Chat_Application.Models.SingleChatMessage", b =>
                {
                    b.HasOne("Chat_Application.Models.SingleChat", "SingleChat")
                        .WithMany("SingleChatMessages")
                        .HasForeignKey("ParentChatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Chat_Application.Models.User", "User")
                        .WithMany("SingleChatMessage")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SingleChat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Chat_Application.Models.UserSession", b =>
                {
                    b.HasOne("Chat_Application.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("Chat_Application.Models.UserSession", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Chat_Application.Models.GroupChat", b =>
                {
                    b.Navigation("GroupChatMessage");

                    b.Navigation("GroupMembers");

                    b.Navigation("MyChats");
                });

            modelBuilder.Entity("Chat_Application.Models.SingleChat", b =>
                {
                    b.Navigation("MyChats");

                    b.Navigation("SingleChatMessages");
                });

            modelBuilder.Entity("Chat_Application.Models.User", b =>
                {
                    b.Navigation("GroupChat");

                    b.Navigation("GroupChatMessage");

                    b.Navigation("GroupMembers");

                    b.Navigation("MyChats");

                    b.Navigation("SingleChat");

                    b.Navigation("SingleChatMessage");
                });
#pragma warning restore 612, 618
        }
    }
}
