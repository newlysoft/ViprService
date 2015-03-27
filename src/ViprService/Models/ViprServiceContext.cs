using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ViprService.Models
{
    public class ViprServiceContext : DbContext
    {
        public ViprServiceContext()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<ViprServiceUser> ViprServiceUsers { get; set; }

        public DbSet<Configuration> Configurations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class ViprServiceUser
    {
        // Key
        public string ViprServiceUserId { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // Maybe combined with ViprServiceUserId if we use GitHub login
        [Required]
        public string GitHubUserName { get; set; }

        // Use to access user's GitHub repo
        [Required]
        public string GitHubClientId { get; set; }

        // Use to access user's GitHub repo
        [Required]
        public string GitHubClientData { get; set; }
        
        public virtual ICollection<Configuration> Configurations { get; set; }

        public string Branch { get; set; }
    }

    public class Configuration
    {
        // Key
        public int ConfigurationId { get; set; }

        // Foreign Key
        public string ViprServiceUserId { get; set; }

        // Github path url to Configuration file (will include repo and branch info)
        public string ConfigurationUrl { get; set; }
    }


    //  CreateDatabaseIfNotExists
    public class DBInitializer : DropCreateDatabaseIfModelChanges<ViprServiceContext>
    {
        protected override void Seed(ViprServiceContext context)
        {
            List<ViprServiceUser> userList = new List<ViprServiceUser>();

            List<Configuration> configs = new List<Configuration>();

            configs.Add(new Configuration
            {
                ConfigurationId = 1,
                ViprServiceUserId = "ViprService002",
                ConfigurationUrl = @"https://github.com/Microsoft/ViprService/blob/master/src/OutlookService.config",
            });

            configs.Add(new Configuration
            {
                ConfigurationId = 2,
                ViprServiceUserId = "ViprService002",
                ConfigurationUrl = @"https://github.com/dotnet/corefx/blob/master/src/ExchangeService.config",
            });

            configs.ForEach(c => context.Configurations.Add(c));

            userList.Add(new ViprServiceUser
            {
                ViprServiceUserId = "ViprService002",
                Email = "ViprService002@outlook.com",
                GitHubUserName = "ViprServiceY",
                GitHubClientId = "11111",
                GitHubClientData = "Top Secret",
                Configurations = configs
            });

            userList.Add(new ViprServiceUser
            {
                ViprServiceUserId = "ViprService001",
                Email = "ViprService001@gmail.com",
                GitHubUserName = "ViprServiceX",
                GitHubClientId = "22222",
                GitHubClientData = "1234abcd",
            });

            userList.ForEach(u => context.ViprServiceUsers.Add(u));

            context.SaveChanges();
        }
    }
}