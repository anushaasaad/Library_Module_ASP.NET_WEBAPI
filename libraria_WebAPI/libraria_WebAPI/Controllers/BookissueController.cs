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
    public class BookissueController : ApiController
    {
        public IHttpActionResult getissue()
        {
            librariaEntities4 lb = new librariaEntities4();
            var results = lb.Book_issue.ToList();
            return Ok(results);

        }
        public HttpResponseMessage Get(string id)
        {
            using (librariaEntities4 dbContext = new librariaEntities4())
            {
                var entity = dbContext.Book_issue.FirstOrDefault(e => e.book_id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "book with ID " + id.ToString() + "not found");
                }
            }
        }
        public HttpResponseMessage Post([FromBody] Book_issue issue)
        {
            try
            {
                using (librariaEntities4 dbContext = new librariaEntities4())
                {
                    dbContext.Book_issue.Add(issue);
                    dbContext.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, issue);
                    message.Headers.Location = new Uri(Request.RequestUri + issue.book_id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        // PUT api/<controller>/5
        public HttpResponseMessage Put(string id, [FromBody] Book_issue book)
        {
            try
            {
                using (librariaEntities4 dbContext = new librariaEntities4())
                {
                    var entity = dbContext.Book_issue.FirstOrDefault(e => e.book_id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Author with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {

                        foreach (PropertyInfo property in typeof(Book_issue).GetProperties().Where(p => p.CanWrite))
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
    }
}
