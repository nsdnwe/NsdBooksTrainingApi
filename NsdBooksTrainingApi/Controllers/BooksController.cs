using NsdBooksTrainingApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description; // <= [ResponseType(typeof(Book))]
using NsdBooksTrainingApi;

namespace NsdBooksTrainingApi.Controllers
{
    public class BooksController : ApiController {
        private DB db = new DB();

        // GET: api/Books
        public IQueryable<Book> GetBooks() {
            return db.Books;
        }

        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id) {
            Book book = db.Books.Find(id);

            // Return last book
            if (id < 1) {
                var books = db.Books.OrderByDescending(z => z.id).ToList();
                book = books.FirstOrDefault();
                if (id < 0) System.Threading.Thread.Sleep(2000);
            }
            if (book == null) {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(Book book) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            db.Entry(book).State = EntityState.Modified;

            try {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException) {
                if (!BookExists(book.id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return Ok(book);
        }

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return Ok(book);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id) {
            Book book = db.Books.Find(id);
            if (book == null) {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id) {
            return db.Books.Count(e => e.id == id) > 0;
        }
    }
}
