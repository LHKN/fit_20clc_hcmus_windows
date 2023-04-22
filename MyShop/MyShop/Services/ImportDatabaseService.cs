using MyShop.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MyShop.Services
{
    public class ImportDatabaseService
    {
        public async Task<List<Book>> ReadBookDataFromExcelFile(StorageFile file)
        {
            var _books = new List<Book>();
            using (var stream = await file.OpenStreamForReadAsync())
            {
                using (var excelPackage = new OfficeOpenXml.ExcelPackage(stream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets[1];
                    int rows = worksheet.Dimension.Rows;
                    int columns = worksheet.Dimension.Columns;

                    for (int row = 2; row <= rows; row++) // Assuming data starts from row 2
                    {
                        // Read data from Excel cells and create a data model
                        Book dataModel = new Book
                        {
                            Id = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                            Title = worksheet.Cells[row, 2].Value?.ToString(),
                            Author = worksheet.Cells[row, 3].Value?.ToString(),
                            Description = worksheet.Cells[row, 4].Value?.ToString(),
                            PublishedDate = DateOnly.Parse(worksheet.Cells[row, 5].Value?.ToString()),
                            Image = worksheet.Cells[row, 6].Value?.ToString(),
                            GenreId = Convert.ToInt32(worksheet.Cells[row, 7].Value?.ToString()),
                            Price = Convert.ToInt32(worksheet.Cells[row, 8].Value),
                            Quantity = Convert.ToInt32(worksheet.Cells[row, 9].Value),
                            // Add more properties for other columns as needed
                        };
                        _books.Add(dataModel); // Add data model to the collection
                    }
                }
            }
            return _books;
        }

        public async Task<List<Genre>> ReadBookGenreFromExcelFile(StorageFile file)
        {
            var _genres = new List<Genre>();
            using (var stream = await file.OpenStreamForReadAsync())
            {
                using (var excelPackage = new OfficeOpenXml.ExcelPackage(stream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets[1];
                    int rows = worksheet.Dimension.Rows;
                    int columns = worksheet.Dimension.Columns;

                    for (int row = 2; row <= rows; row++) // Assuming data starts from row 2
                    {
                        // Read data from Excel cells and create a data model
                        Genre dataModel = new Genre
                        {
                            Id = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                            Name = worksheet.Cells[row, 2].Value?.ToString(),
                            // Add more properties for other columns as needed
                        };
                        _genres.Add(dataModel); // Add data model to the collection
                    }
                }
            }
            return _genres;
        }

    }
}
