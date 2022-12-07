using libraria_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace libraria_WebAPI.Controllers
{
    public class studentController : ApiController
    {
        public IHttpActionResult getstudent()
        {
            librariaEntities4 lb = new librariaEntities4();
            var results = lb.students.ToList();
            return Ok(results);

        }
        public HttpResponseMessage Get(string id)
        {
            using (librariaEntities4 dbContext = new librariaEntities4())
            {
                var entity = dbContext.students.FirstOrDefault(e => e.student_id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "student with ID " + id.ToString() + "not found");
                }
            }
        }
    }
}
