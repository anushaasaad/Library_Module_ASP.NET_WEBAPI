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
    public class authorController : ApiController
    {
        public IHttpActionResult getauthor()
        {
            librariaEntities4 lb = new librariaEntities4();
            var results = lb.Authors.ToList();
            return Ok(results);

        }
        public HttpResponseMessage Get(string id)
        {
            using (librariaEntities4 dbContext = new librariaEntities4())
            {
                var entity = dbContext.Authors.FirstOrDefault(e => e.author_id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Author with ID " + id.ToString() + "not found");
                }
            }
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Author author)
        {
            try
            {
                using (librariaEntities4 dbContext = new librariaEntities4())
                {
                    dbContext.Authors.Add(author);
                    dbContext.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, author);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        author.author_id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        // PUT api/<controller>/5
        public HttpResponseMessage Put(string id, [FromBody] Author author)
        {
            try
            {
                using (librariaEntities4 dbContext = new librariaEntities4())
                {
                    var entity = dbContext.Authors.FirstOrDefault(e => e.author_id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Author with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {

                        foreach (PropertyInfo property in typeof(Author).GetProperties().Where(p => p.CanWrite))
                        {
                            property.SetValue(entity, property.GetValue(author, null), null);
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
                using (librariaEntities4 dbContext = new librariaEntities4())
                {
                    var entity = dbContext.Authors.FirstOrDefault(e => e.author_id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Author with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        dbContext.Authors.Remove(entity);
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
