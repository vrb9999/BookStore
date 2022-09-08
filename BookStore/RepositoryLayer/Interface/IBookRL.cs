using DatabaseLayer.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        public BookModel AddBook(BookModel bookModel);
        public List<BookModel> GetAllBooks();
        public BookModel GetBookById(int BookId);
    }
}
