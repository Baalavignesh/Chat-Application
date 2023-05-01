using Chat_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_Application
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            // Users - MyChats (One to Many)
            modelBuilder.Entity<User>()
                .HasMany(c => c.MyChats)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.ChatUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Users - SingleChat Sender (One to Many)
            modelBuilder.Entity<User>()
                .HasMany(c => c.SingleChat)
                .WithOne(c => c.Sender)
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Users - SingleChat Receiver (One to Many)
            //modelBuilder.Entity<User>()
            //    .HasMany(c => c.SingleChat)
            //  .WithOne(c => c.Receiver)
            //.HasForeignKey(c => c.ReceiverId)
            //.OnDelete(DeleteBehavior.Restrict);


            // Users - SingleChatMessage (One to Many)
            modelBuilder.Entity<User>()
                .HasMany(c => c.SingleChatMessage)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            //Users - groupChat(One to Many)
            modelBuilder.Entity<User>()
                .HasMany(c => c.GroupChat)
                .WithOne(c => c.Admin)
                .HasForeignKey(c => c.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Users - GroupMembers (One to Many)
            modelBuilder.Entity<User>()
                .HasMany(c => c.GroupMembers)
                .WithOne(c => c.GroupMember)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Users - GroupChatMessage (One to Many)
            modelBuilder.Entity<User>()
            .HasMany(c => c.GroupChatMessage)
            .WithOne(c => c.Sender)
            .HasForeignKey(c => c.GroupSenderId)
            .OnDelete(DeleteBehavior.Restrict);

            // MyChat
            // MyChats - SingleChat (Many to One)
            modelBuilder.Entity<MyChats>()
                .HasOne(c => c.SingleChat)
                .WithMany(c => c.MyChats)
                .HasForeignKey(c => c.SingleChatId)
                .OnDelete(DeleteBehavior.Restrict);

            // MyChats - GroupChat (Many to One)
            modelBuilder.Entity<MyChats>()
                .HasOne(c => c.GroupChat)
                .WithMany(c => c.MyChats)
                .HasForeignKey(c => c.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // SingleChat
            // SingleChat - SingleChatMessage (One to Many)
            modelBuilder.Entity<SingleChat>()
                .HasMany(c => c.SingleChatMessages)
                .WithOne(c => c.SingleChat)
                .HasForeignKey(c => c.ParentChatId)
                .OnDelete(DeleteBehavior.Restrict);

            // GroupChat
            // GroupChat - GroupChatMessage (One to Many)
            modelBuilder.Entity<GroupChat>()
                .HasMany(c => c.GroupChatMessage)
                .WithOne(c => c.GroupChat)
                .HasForeignKey(c => c.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GroupChat>()
                .HasMany(c => c.GroupMembers)
                .WithOne(c => c.GroupChat)
                .HasForeignKey(c => c.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

        }


        public DbSet<Chat_Application.Models.User>? User { get; set; }
        public DbSet<Chat_Application.Models.MyChats>? MyChats { get; set; }
        public DbSet<Chat_Application.Models.SingleChat>? SingleChat { get; set; }
        public DbSet<Chat_Application.Models.SingleChatMessage>? SingleChatMessage { get; set; }
        public DbSet<Chat_Application.Models.GroupChat>? GroupChat { get; set; }
        public DbSet<Chat_Application.Models.GroupMembers>? GroupMembers { get; set; }
        public DbSet<Chat_Application.Models.GroupChatMessage>? GroupChatMessage { get; set; }


    }
}
