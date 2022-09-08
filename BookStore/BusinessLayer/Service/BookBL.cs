using BusinessLayer.Interface;
using DatabaseLayer.Book;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class BookBL : IBookBL
    {
        IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public BookModel AddBook(BookModel bookModel)
        {
            try
            {
                return this.bookRL.AddBook(bookModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BookModel> GetAllBooks()
        {
            return this.bookRL.GetAllBooks();
        }
    }
}
