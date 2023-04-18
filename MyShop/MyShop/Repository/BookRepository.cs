using Microsoft.Data.SqlClient;
using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyShop.Repository
{
    class BookRepository : RepositoryBase, IBookRepository
    {

        public async Task<bool> Add(Book book)
        {
            bool isSuccessful = false;
            var connection = GetConnection();

            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                string sql = "insert into BOOK (title,author,description,genre_id,price,quantity,published_date,image)" +
                    "values (@title,@author,@description,@genre_id,@price,@quantity,@published_date, @image)";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@title", SqlDbType.NVarChar).Value = book.Title;
                command.Parameters.Add("@author", SqlDbType.NVarChar).Value = book.Author;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = book.Description;
                command.Parameters.Add("@image", SqlDbType.NVarChar).Value = book.Image;
                command.Parameters.Add("@genre_id", SqlDbType.Int).Value = book.GerneId;
                command.Parameters.Add("@price", SqlDbType.Int).Value = book.Price;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = book.Quantity;
                command.Parameters.Add("@published_date", SqlDbType.Date).Value = book.PublishedDate;
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0) { isSuccessful = true; }
                    else { isSuccessful = false; }
                }
                catch (SqlException exc) {
                    string msg = exc.Message + "\n" + book.Title + "|" + book.Author + "|" + book.Description + "|" + book.Image + "|" + book.GerneId + "|" + book.Price + "|" + book.Quantity + "|" + book.PublishedDate;
                    MessageBox.Show(msg);
                }


                connection.Close();
            }
            return isSuccessful;
        }

        public async Task<bool> Edit(Book book)
        {
            bool isSuccessful = false;
            var connection = GetConnection();

            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                string sql = "update BOOK set title=@title,author=@author,description=@description,genre_id=@genre_id," +
                    "price=@price,quantity=@quantity,published_date=@published_date,image=@image " +
                    "where id = @id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@title", SqlDbType.NVarChar).Value = book.Title;
                command.Parameters.Add("@author", SqlDbType.NVarChar).Value = book.Author;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = book.Description;
                command.Parameters.Add("@image", SqlDbType.NVarChar).Value = book.Image;
                command.Parameters.Add("@genre_id", SqlDbType.Int).Value = book.GerneId;
                command.Parameters.Add("@price", SqlDbType.Int).Value = book.Price;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = book.Quantity;
<<<<<<< HEAD
                command.Parameters.Add("@published_date", SqlDbType.Date).Value = book.PublishedDate;
=======
                command.Parameters.Add("@published_date", SqlDbType.DateTime).Value = book.PublishedDate;
>>>>>>> bcb6f031140599b2b5291bbae7cbd5cebb4164e1
                command.Parameters.Add("@id", SqlDbType.Int).Value = book.Id;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0) { isSuccessful = true; }
                else { isSuccessful = false; }

                connection.Close();
            }
            return isSuccessful;
        }

        public async Task<ObservableCollection<Book>> GetAll()
        {
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            var connection = GetConnection();

            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                string sql = "select id,title,image,author,description,genre_id,price,quantity,published_date from BOOK";
                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string title = Convert.ToString(reader["title"]);
                    string author = Convert.ToString(reader["author"]);
                    string description = Convert.ToString(reader["description"]);
                    string image = Convert.ToString(reader["image"]);
                    int genre_id = Convert.ToInt32(reader["genre_id"]);
                    int price = Convert.ToInt32(reader["price"]);
                    int quantity = Convert.ToInt32(reader["quantity"]);
                    object obj = reader["published_date"];
                    DateOnly published_date = obj == null || obj == DBNull.Value ? default(DateOnly) : DateOnly.FromDateTime(Convert.ToDateTime(obj));

                    books.Add(new Book
                    {
                        Id = id,
                        Title = title,
                        Author = author,
                        Description = description,
                        Image = image,
                        GerneId = genre_id,
                        Price = price,
                        Quantity = quantity,
                        PublishedDate = published_date
                    });
                }
                reader.Close();

                connection.Close();
            }

            return books;
        }

        public async Task<Book> GetById(int id)
        {
            Book newBook = null;
            var connection = GetConnection();

            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                string sql = "select title,author,description,image,genre_id,price,quantity,published_date from BOOK" +
                    "where id = @id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string title = Convert.ToString(reader["title"]);
                    string author = Convert.ToString(reader["author"]);
                    string description = Convert.ToString(reader["description"]);
                    string image = Convert.ToString(reader["image"]);
                    int genre_id = Convert.ToInt32(reader["genre_id"]);
                    int price = Convert.ToInt32(reader["price"]);
                    int quantity = Convert.ToInt32(reader["quantity"]);
                    DateOnly published_date = DateOnly.FromDateTime(Convert.ToDateTime(reader["published_date"]));

                    newBook = new Book
                    {
                        Id = id,
                        Title = title,
                        Author = author,
                        Description = description,
                        Image = image,
                        GerneId = genre_id,
                        Price = price,
                        Quantity = quantity,
                        PublishedDate = published_date
                    };
                }
                connection.Close();
            }
            return newBook;
        }

        public async Task<List<Genre>> GetGenres()
        {
            List<Genre> genres = new List<Genre>();
            var connection = GetConnection();

            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                string sql = "select id,name from GENRE";
                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = Convert.ToString(reader["name"]);

                    genres.Add(new Genre
                    {
                        Id = id,
                        Name = name
                    });
                }
                reader.Close();

                connection.Close();
            }

            return genres;
        }

        public async Task<bool> Remove(int id)
        {
            bool isSuccessful = false;
            var connection = GetConnection();

            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                string sql = "delete from BOOK where id = @id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0) { isSuccessful = true; }
                else { isSuccessful = false; }

                connection.Close();
            }
            return isSuccessful;
        }
    }
}
