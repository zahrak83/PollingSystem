using Microsoft.EntityFrameworkCore;
using PollingSystem.Entities;
using PollingSystem.Infrastructure.Configurations;

namespace PollingSystem.Infrastructure
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-DG1LLR4\SQLEXPRESS;Database=Polling;Integrated Security=true;TrustServerCertificate=true;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> users { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<NormalUser> normalusers { get; set; }
        public DbSet<Survey> surveys { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<Option> options { get; set; }
        public DbSet<Vote> votes { get; set; }
        public DbSet<UserSurvey> usersurveys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new NormalUserConfigurations());
            modelBuilder.ApplyConfiguration(new AdminConfigurations());
            modelBuilder.ApplyConfiguration(new OptionConfigurations());
            modelBuilder.ApplyConfiguration(new QuestionConfigurations());
            modelBuilder.ApplyConfiguration(new SurveyConfigurations());
            modelBuilder.ApplyConfiguration(new VoteConfigurations());
            modelBuilder.ApplyConfiguration(new UserSurveyConfigurations());
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Admin>().HasData(
                new Admin { Id = 1, FullName = "John Smith", Username = "admin1", Password = "1234" },
                new Admin { Id = 2, FullName = "Sarah Johnson", Username = "admin2", Password = "1234" } 
                );

            
            modelBuilder.Entity<NormalUser>().HasData(
                new NormalUser { Id = 3, FullName = "Michael Brown", Username = "user1", Password = "1234" },
                new NormalUser { Id = 4, FullName = "Emily Davis", Username = "user2", Password = "1234" },
                new NormalUser { Id = 5, FullName = "David Wilson", Username = "user3", Password = "1234" },
                new NormalUser { Id = 6, FullName = "Sophia Miller", Username = "user4", Password = "1234" },
                new NormalUser { Id = 7, FullName = "James Anderson", Username = "user5", Password = "1234" }
            );

            modelBuilder.Entity<Survey>().HasData(
                new Survey { Id = 1, Title = "Product Quality Evaluation", AdminId = 1 },
                new Survey { Id = 2, Title = "Professor X Course Feedback", AdminId = 2 }
            );

            modelBuilder.Entity<Question>().HasData(
                new Question { Id = 1, Text = "How would you rate the overall quality of our products?", SurveyId = 1 },
                new Question { Id = 2, Text = "How satisfied are you with the packaging quality?", SurveyId = 1 }
            );

            modelBuilder.Entity<Question>().HasData(
                new Question { Id = 3, Text = "How would you rate Professor X's knowledge of the subject?", SurveyId = 2 },
                new Question { Id = 4, Text = "How well does Professor X interact with students?", SurveyId = 2 },
                new Question { Id = 5, Text = "Overall, how satisfied are you with Professor X's classes?", SurveyId = 2 }
            );

            modelBuilder.Entity<Option>().HasData(

                new Option { Id = 1, Text = "Excellent", QuestionId = 1 },
                new Option { Id = 2, Text = "Good", QuestionId = 1 },
                new Option { Id = 3, Text = "Average", QuestionId = 1 },
                new Option { Id = 4, Text = "Poor", QuestionId = 1 },

                new Option { Id = 5, Text = "Very well packaged", QuestionId = 2 },
                new Option { Id = 6, Text = "Good", QuestionId = 2 },
                new Option { Id = 7, Text = "Could be better", QuestionId = 2 },
                new Option { Id = 8, Text = "Poor", QuestionId = 2 },

                new Option { Id = 9, Text = "Excellent", QuestionId = 3 },
                new Option { Id = 10, Text = "Good", QuestionId = 3 },
                new Option { Id = 11, Text = "Average", QuestionId = 3 },
                new Option { Id = 12, Text = "Poor", QuestionId = 3 },

                new Option { Id = 13, Text = "Very friendly and respectful", QuestionId = 4 },
                new Option { Id = 14, Text = "Good", QuestionId = 4 },
                new Option { Id = 15, Text = "Acceptable", QuestionId = 4 },
                new Option { Id = 16, Text = "Unfriendly", QuestionId = 4 },

                new Option { Id = 17, Text = "Completely satisfied", QuestionId = 5 },
                new Option { Id = 18, Text = "Somewhat satisfied", QuestionId = 5 },
                new Option { Id = 19, Text = "Dissatisfied", QuestionId = 5 },
                new Option { Id = 20, Text = "Completely dissatisfied", QuestionId = 5 }
            );
        }
    }
}
