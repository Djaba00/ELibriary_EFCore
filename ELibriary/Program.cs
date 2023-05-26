using System;
using ELibriary.Tables;
using ELibriary.Repositories;

namespace ELibriary
{
    class Program
    {
        public static void Main(string[] args)
        {
            UserRepository usersRepo = new UserRepository();
            BookRepository booksRepo = new BookRepository();

            using(var db = new AppContext())
            {
                ShowBooksData(db, usersRepo, booksRepo);
            }
        }

        public static void ShowBooksData(AppContext db, UserRepository usersRepo, BookRepository booksRepo)
        {
            Console.WriteLine($"1) Список книг жанра роман между 1500 и 1900 гг");
            foreach(Book book in booksRepo.SelectBooksByGenreAndYear(db, "Novel", 1500, 1900))
            {
                Console.WriteLine("\t" + book.Title + ", " + book.Author);
            };
            Console.WriteLine();

            Console.WriteLine($"2) Количество книг Харпер Ли в библиотеке = {booksRepo.CountBooksByAuthor(db, "Harper Lee")}\n");

            Console.WriteLine($"3) Количество книг жанра роман в библиотеке {booksRepo.CountBooksByGenre(db, "Novel")}\n");

            Console.WriteLine($"4) Война и мир Льва Толстого. В наличии - {booksRepo.BookByAuthorAndTitleExists(db, "Leo Tolstoy", "War and Peace")}\n");

            Console.WriteLine($"5) Война и мир на руках у читаля? - {booksRepo.BookIsAlivable(db, 1)}\n");

            Console.WriteLine($"6) Количество книг на руках у пользователя с чттельским билетом №1 - {booksRepo.CountBooksByUserId(db, 1)}\n");

            Console.WriteLine($"7) Последняя вышедшая книга - {booksRepo.NewestBook(db).Title}");

            Console.WriteLine($"8) Cписок книг в алфавитном порядке:");
            foreach(Book book in booksRepo.BooksSortedByTitle(db))
            {
                Console.WriteLine("\t" + book.Title + ", " + book.Author);
            };
            Console.WriteLine();

            Console.WriteLine($"9) Список книг по году в порядке убывания: ");
            foreach(Book book in booksRepo.BooksSortedByYear(db))
            {
                Console.WriteLine("\t" + book.Title + ", " + book.Year);
            };

        }

        public static void AddData(AppContext db, UserRepository usersRepo, BookRepository booksRepo)
        {
            var user1 = new User { Name = "Timofey", Email = "shipelenko@gmail.com" };
            var user2 = new User { Name = "Naya", Email = "naya@gmail.com" };
            var user3 = new User { Name = "Nikita", Email = "nikita@gmail.com" };
            var user4 = new User { Name = "Alexey", Email = "alexey@gmail.com" };

            var book1 = new Book { Title = "War and Peace", Author = "Leo Tolstoy", Genre = "Novel" , Year = 1899};
            var book2 = new Book { Title = "The Murders in the Rue Morgue", Author = "Edgar Allan Poe", Genre = "Detective fiction; Short story", Year = 1841};
            var book3 = new Book { Title = "The Prince", Author = "Niccolo Machiavelli", Genre = "Non-Fiction", Year = 1532};
            var book4 = new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Southern Gothic novel and Bildungsroman", Year = 1960};
            var book5 = new Book { Title = "The Master and Margarita", Author = "Mikhail Bulgakov", Genre = "Novel", Year = 1966};
            var book6 = new Book { Title = "The Catcher in the Rye", Author = "J.D Salinger", Genre = "Novel", Year = 1951};        

            user1.Books.AddRange( new[] {book1, book2});
            user2.Books.Add(book3);
            user3.Books.Add(book4);
            user4.Books.Add(book5);

            usersRepo.AddUsers(db, new[] {user1, user2, user3, user4});
            booksRepo.AddBooks(db, new[] {book1, book2, book3, book4, book5, book6});
        }
    }
}