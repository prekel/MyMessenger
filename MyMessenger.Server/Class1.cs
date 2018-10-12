using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
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

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql("server=51.158.73.185;database=TestDB;user=vladislav;password=;SslMode=none");
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
		public static void Main1(string[] args)
		{
						InsertData();
						PrintData();

			//var passwd = Console.ReadLine();
			//var connectionInfo = new PasswordConnectionInfo("51.158.73.185", "vladislav", passwd);
			//connectionInfo.Timeout = TimeSpan.FromSeconds(30);

			//using (var client = new SshClient(connectionInfo))
			//{
			//	try
			//	{
			//		Console.WriteLine("Trying SSH connection...");
			//		client.Connect();
			//		if (client.IsConnected)
			//		{
			//			Console.WriteLine("SSH connection is active: {0}", client.ConnectionInfo.ToString());
			//		}
			//		else
			//		{
			//			Console.WriteLine("SSH connection has failed: {0}", client.ConnectionInfo.ToString());
			//		}

			//		Console.WriteLine("\r\nTrying port forwarding...");
			//		var portFwld = new ForwardedPortLocal(IPAddress.Loopback.ToString(), 22, "localhost", 3305);
			//		client.AddForwardedPort(portFwld);
			//		portFwld.Start();
			//		if (portFwld.IsStarted)
			//		{
			//			Console.WriteLine("Port forwarded: {0}", portFwld.ToString());
			//		}
			//		else
			//		{
			//			Console.WriteLine("Port forwarding has failed.");
			//		}

			//	}
			//	catch (SshException e)
			//	{
			//		Console.WriteLine("SSH client connection error: {0}", e.Message);
			//	}
			//	catch (System.Net.Sockets.SocketException e)
			//	{
			//		Console.WriteLine("Socket connection error: {0}", e.Message);
			//	}

			//}

			//Console.WriteLine("\r\nTrying database connection...");
			//DBConnect dbConnect = new DBConnect("localhost", "test_database", "root", "passwrod123", "4479");

			//var ct = dbConnect.Count("packages");
			//Console.WriteLine(ct.ToString());


		}

		private static void InsertData()
		{
			using (var context = new LibraryContext())
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
			using (var context = new LibraryContext())
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
