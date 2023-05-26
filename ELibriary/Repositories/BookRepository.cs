using System;
using ELibriary.Tables;

namespace ELibriary.Repositories
{
    class BookRepository
    {
        #region [Select_Operations]
        public Book SelectBookById(AppContext db, int id)
        {
            var book = db.Books.Where(book => book.Id == id).FirstOrDefault();
            if (book is not null)
            {
                return book;
            }
            else
            {
                Console.WriteLine("Книга не найдена");
                return null;
            }
        }

        public List<Book> SelectAllBooks(AppContext db)
        {
            var books = db.Books.ToList();
            return books;
        }

        public List<Book> SelectBooksByGenreAndYear(AppContext db, string genre, int yearFrom, int yearTo)
        {
            var books = db.Books.Where(b => b.Year >= yearFrom && b.Year <= yearTo).ToList();
            if(books is not null)
            {
                return books;
            }
            else
            {
                Console.WriteLine("Книги не найдены");
                return null;
            }
        }

        public int CountBooksByAuthor(AppContext db, string author)
        {
            return db.Books.Count(b => b.Author == author);
        }

        public int CountBooksByGenre(AppContext db, string genre)
        {
            return db.Books.Count(b => b.Genre == genre);
        }

        public bool BookByAuthorAndTitleExists(AppContext db, string author, string title)
        {
            return db.Books.Any(b => b.Author == author && b.Title == title);
        }

        public bool BookIsAlivable(AppContext db, int bookId)
        {
            return db.Books.Where(b => b.Id == bookId).Any(b => b.UserId == default);
        }

        public int CountBooksByUserId(AppContext db, int userId)
        {
            return db.Users.Where(u => u.Id == userId).Select(u => u.Books).Count();
        }

        public Book NewestBook(AppContext db)
        {
            int maxYear = db.Books.Max(b => b.Year);

            return db.Books.Where(b => b.Year == maxYear).First();
        }

        public List<Book> BooksSortedByTitle(AppContext db)
        {
            return db.Books.OrderBy(b => b.Title).ToList();
        }

        public List<Book> BooksSortedByYear(AppContext db)
        {
            return db.Books.OrderByDescending(b => b.Year).ToList();
        }

        #endregion

        #region [Business_Operations]
        
        public void BorrowBookById(AppContext db, int bookId, int UserId)
        {
            var book = db.Books.Where(b => b.Id == bookId).FirstOrDefault();

            if(book is not null)
            {
                book.UserId = UserId;
            }
            else
            {
                Console.WriteLine("Книга не найдена");
            }
        }

        public void ReturnBookById(AppContext db, int bookId)
        {
            var book = db.Books.Where(b => b.Id == bookId).FirstOrDefault();

            if(!(book is null))
            {
                book.UserId = default;
            }
            else
            {
                Console.WriteLine("Книга не найдена");
            }
        }
        
        public void AddBook(AppContext db, Book book)
        {
            try
            {
                db.Books.Add(book);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
 
        public void AddBooks(AppContext db, Book[] books)
        {
            try
            {
                db.Books.AddRange(books);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteBook(AppContext db, Book book)
        {
            try
            {
                db.Books.Remove(book);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteBooks(AppContext db, Book[] books)
        {
            try
            {
                db.Books.RemoveRange(books);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}