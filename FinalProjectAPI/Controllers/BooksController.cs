using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalProjectAPI.Controllers
{
    public class BooksController : ApiController
    {
        BookDBDataContext db = new BookDBDataContext();
        // GET: api/Books
        public IEnumerable<Book> Get()
        {
            var myBooks = db.Books;
            return myBooks;
        }

        // GET: api/Books/5
        public Book Get(int id)
        {
            // get book based on id from linq connection
            var books = db.Books;
            var book = from b in books where b.Id == id select b;
            Book b1 = book.FirstOrDefault();

            return b1;
        }

        // POST: api/Books
        public int Post([FromBody]Book book)
        {

            db.Books.InsertOnSubmit(book);
            db.SubmitChanges();
			return book.Id;
        }

        // PUT: api/Books/5
        public int Put(int id, [FromBody]Book updatedBook)
        {
            var books = db.Books;
            Book result = new Book();
            var b = from a in books where a.Id == id select a;
            foreach (var i in b)
            {
                result = i;
            }
            result.Name = updatedBook.Name;
            result.ReleaseDate = Convert.ToDateTime(updatedBook.ReleaseDate);
            db.SubmitChanges();
            return result.Id;
        }

        // DELETE: api/Books/5
        public void Delete(int id)
        {
            var book = from a in db.Books
                       where a.Id == id
                       select a;
            Book b1 = book.FirstOrDefault();

            db.Books.DeleteOnSubmit(b1);
            db.SubmitChanges();
        }
    }
}
