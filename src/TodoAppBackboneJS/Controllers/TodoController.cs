using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TodoAppBackboneJS.Models;

namespace TodoAppBackboneJS.Controllers
{
    public class TodoController : ApiController
    {
        private TodoAppBackboneJSContext db = new TodoAppBackboneJSContext();

        // GET api/Todo
        public IEnumerable<Todo> GetTodoes()
        {
            return db.Todoes.AsEnumerable();
        }

        // GET api/Todo/5
        public Todo GetTodo(int id)
        {
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return todo;
        }

        // PUT api/Todo/5
        public HttpResponseMessage PutTodo(int id, Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != todo.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(todo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Todo
        public HttpResponseMessage PostTodo(Todo todo)
        {
            if (ModelState.IsValid)
            {
                db.Todoes.Add(todo);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, todo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = todo.id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Todo/5
        public HttpResponseMessage DeleteTodo(int id)
        {
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Todoes.Remove(todo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, todo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}