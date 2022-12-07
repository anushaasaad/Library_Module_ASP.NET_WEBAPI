using libraria_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace libraria_WebAPI.Controllers
{
    public class bookController : ApiController
    {
        public IHttpActionResult getbook()
        {
            librariaEntities2 lb = new librariaEntities2();
            var results = lb.Book_inventory.ToList();
            return Ok(results);

        }
        public HttpResponseMessage Get(string id)
        {
            using (librariaEntities2 dbContext = new librariaEntities2())
            {
                var entity = dbContext.Book_inventory.FirstOrDefault(e => e.book_id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Book with ID " + id.ToString() + "not found");
                }
            }
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Book_inventory book)
        {
            try
            {
                using (librariaEntities2 dbContext = new librariaEntities2())
                {
                    dbContext.Book_inventory.Add(book);
                    dbContext.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, book);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        book.book_id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        // PUT api/<controller>/5
        public HttpResponseMessage Put(string id, [FromBody] Book_inventory book)
        {
            try
            {
                using (librariaEntities2 dbContext = new librariaEntities2())
                {
                    var entity = dbContext.Book_inventory.FirstOrDefault(e => e.book_id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Author with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {

                        foreach (PropertyInfo property in typeof(Author).GetProperties().Where(p => p.CanWrite))
                        {
                            property.SetValue(entity, property.GetValue(book, null), null);
                        }
                        dbContext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(string id)
        {
            try
            {
                using (librariaEntities2 dbContext = new librariaEntities2())
                {
                    var entity = dbContext.Book_inventory.FirstOrDefault(e => e.book_id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Author with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        dbContext.Book_inventory.Remove(entity);
                        dbContext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
