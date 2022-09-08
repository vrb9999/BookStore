using BusinessLayer.Interface;
using DatabaseLayer.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddBook")]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
                var result = bookBL.AddBook(bookModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Added Successfully..." });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to add book!!!" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                List<BookModel> listOfbooks = new List<BookModel>();
                listOfbooks = this.bookBL.GetAllBooks();
                return Ok(new { sucess = true, Message = "Data Fetched Successfully...", data = listOfbooks });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
