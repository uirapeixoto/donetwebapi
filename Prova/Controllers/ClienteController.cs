using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prova.Models;

namespace Prova.Controllers
{
    public class ClienteController : ApiController
    {

        private ProjetoModeloDBEntities db = new ProjetoModeloDBEntities();

        
        // GET: api/Cliente
        public IEnumerable<Clientes> Get()
        {
            return db.Clientes.AsEnumerable();
        }

        // GET: api/Cliente/5
        public Clientes Get(int id)
        {
            Clientes cliente = db.Clientes.Find(id);
            
            if(cliente == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return cliente;
        }

        // POST: api/Cliente
        public HttpResponseMessage Post(Clientes cliente)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    if(cliente.ClienteId == 0)
                    {
                        cliente.DataCadastro = DateTime.Now;
                        db.Clientes.Add(cliente);
                        db.SaveChanges();

                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, cliente);
                        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = cliente.ClienteId }));
                        return response;
                    }
                    else
                    {
                        db.Entry(cliente).State = EntityState.Modified;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        // PUT: api/Cliente/5
        public HttpResponseMessage Put(int id, Clientes cliente)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if(id != cliente.ClienteId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(cliente).State = EntityState.Modified;

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

        // DELETE: api/Cliente/5
        [HttpPost]
        [Route("cliente/delete")]
        public HttpResponseMessage Delete(int id)
        {
            Clientes cliente = db.Clientes.Find(id);

            if(cliente == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Clientes.Remove(cliente);

            try
            {
                db.SaveChanges();

            }catch(DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
