using DatabaseLayer.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel bookModel);
        public List<BookModel> GetAllBooks();
        public BookModel GetBookById(int BookId);
        public BookModel UpdateBooks(int BookId, BookModel bookModel);
    }
}
