using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyMessenger.Server.Configs;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace MyMessenger.Server
{
	public class Book
	{
		public string ISBN { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string Language { get; set; }
		public int Pages { get; set; }
		public virtual Publisher Publisher { get; set; }
	}

	public class Publisher
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public virtual ICollection<Book> Books { get; set; }
	}
	
	public class LibraryContext : DbContext
	{
		public DbSet<Book> Book { get; set; }

		public DbSet<Publisher> Publisher { get; set; }

		public static Config Config;
		
		public LibraryContext(Config config)
		{
			Config = config;
		}
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(new ConnectionStringCompiler(Config.DbConfig).Compile());
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Publisher>(entity =>
			{
				entity.HasKey(e => e.ID);
				entity.Property(e => e.Name).IsRequired();
			});

			modelBuilder.Entity<Book>(entity =>
			{
				entity.HasKey(e => e.ISBN);
				entity.Property(e => e.Title).IsRequired();
				entity.HasOne(d => d.Publisher)
					.WithMany(p => p.Books);
			});
		}
	}
	public class Program
	{
		public static Config Config;
		
		public static void Main1(Config config)
		{
			Config = config;
			InsertData();
			PrintData();
		}

		private static void InsertData()
		{
			using (var context = new LibraryContext(Config))
			{
				// Creates the database if not exists
				context.Database.EnsureCreated();

				// Adds a publisher
				var publisher = new Publisher
				{
					Name = "Mariner Books"
				};
				context.Publisher.Add(publisher);

				// Adds some books
				context.Book.Add(new Book
				{
					ISBN = "978-0544003415",
					Title = "The Lord of the Rings",
					Author = "J.R.R. Tolkien",
					Language = "English",
					Pages = 1216,
					Publisher = publisher
				});
				context.Book.Add(new Book
				{
					ISBN = "978-0547247762",
					Title = "The Sealed Letter",
					Author = "Emma Donoghue",
					Language = "English",
					Pages = 416,
					Publisher = publisher
				});

				// Saves changes
				context.SaveChanges();
			}
		}

		private static void PrintData()
		{
			// Gets and prints all books in database
			using (var context = new LibraryContext(Config))
			{
				var books = context.Book
					.Include(p => p.Publisher);
				foreach (var book in books)
				{
					var data = new StringBuilder();
					data.AppendLine($"ISBN: {book.ISBN}");
					data.AppendLine($"Title: {book.Title}");
					data.AppendLine($"Publisher: {book.Publisher.Name}");
					Console.WriteLine(data.ToString());
				}
			}
		}
	}

	public class Class1
	{
	}
}
