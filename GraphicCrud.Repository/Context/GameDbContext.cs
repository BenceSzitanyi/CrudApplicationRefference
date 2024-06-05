using GraphicCrud.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GraphicCrud.Repository.Context
{
    public class GameDbContext : DbContext
    {
        public DbSet<VideoGame> Games { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Developer> Developers { get; set; }

        public GameDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseInMemoryDatabase("db")
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoGame>(game => game
            .HasOne(game => game.Developer)
            .WithMany(developer => developer.Games)
            .HasForeignKey(game => game.DeveloperId)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<VideoGame>(game => game
            .HasOne(game => game.Publisher)
            .WithMany(publisher => publisher.Games)
            .HasForeignKey(game => game.PublisherId)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Developer>(developer => developer
            .HasOne(developer => developer.Publisher)
            .WithMany(publisher => publisher.Developers)
            .HasForeignKey(developer => developer.PublisherId)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<VideoGame>().HasData(new VideoGame[]
            {
                //id;title;date;score;platform;publisher;developer
                //EA
                new VideoGame("1;Star Wars: Battlefront;2015.11.19;73;Cross-Platform;1;1"),
                new VideoGame("2;Star Wars: Battlefront 2;2017.11.17;68;Cross-Platform;1;2"),
                new VideoGame("3;NFS Unbound;2022.12.02;77;PC;1;2"),
                new VideoGame("4;Mass Effect 3;2012.03.06;93;Cross-Platform;1;3"),
                new VideoGame("5;Mass Effect Andromeda;2017.03.23;71;Cross-Platform;1;3"),
                //Microsoft
                new VideoGame("6;Forza Horizon 5;2021.11.09;92;XBox;2;4"),
                new VideoGame("7;Forza Motorsport;2023.10.10;84;XBox;2;4"),
                new VideoGame("8;Gears of War 4;2016.10.11;84;XBox;2;5"),
                new VideoGame("9;Gears 5;2019.09.06;84;XBox;2;5"),
                new VideoGame("10;Max Payne;2001.06.23;89;XBox;2;6"),
                new VideoGame("11;Alan Wake;2010.05.18;83;XBox;2;6"),
                //Ubisoft
                new VideoGame("12;Far Cry 3;2012.12.04;88;Cross-Platform;3;7"),
                new VideoGame("13;Assasins Creed Unity;2014.11.11;72;Cross-Platform;3;7"),
                //Sony
                new VideoGame("14;The Last of Us;2013.07.14;88;PlayStation;4;8"),
                new VideoGame("15;Gran Turismo 7;2022.03.04;87;PlayStation;4;9"),
                new VideoGame("16;Detroit Become Human;2018.08.25;78;PlayStation;4;10"),
                new VideoGame("17;Until Dawn;2015.08.25;79;PlayStation;4;11"),
                //Nintendo
                new VideoGame("18;Legend of Zelda: Breath of the Wild;2015.11.12;97;Nintendo Switch;5;12"),
                new VideoGame("19;Wii Sports;2006.05.10;95;Nintendo Wii;5;12"),
                new VideoGame("20;Super Mario Bros;2009.11.15;87;Nintendo Wii;5;12"),
            });
            modelBuilder.Entity<Developer>().HasData(new Developer[]
            {
                //EA
                new Developer("1;DICE;1"),
                new Developer("2;Criterion Games;1"),
                new Developer("3;BioWare;1"),
                //Microsoft
                new Developer("4;Turn 10 Studios;2"),
                new Developer("5;The Coalition;2"),
                new Developer("6;Remedy Entertainment;2"),
                //Ubi
                new Developer("7;Ubisoft;3"),
                //Sony
                new Developer("8;Naughty Dog;4"),
                new Developer("9;Polyphony Digital;4"),
                new Developer("10;Quantic Dream;4"),
                new Developer("11;Supermassive Games;4"),
                //Csintendo
                new Developer("12;Nintendo;5"),
            });
            modelBuilder.Entity<Publisher>().HasData(new Publisher[]
            {
                new Publisher("1;Electronic Arts"),
                new Publisher("2;Microsoft Studios"),
                new Publisher("3;Ubisoft"),
                new Publisher("4;Sony"),
                new Publisher("5;Nintendo"),
            });
        }
    }
}
