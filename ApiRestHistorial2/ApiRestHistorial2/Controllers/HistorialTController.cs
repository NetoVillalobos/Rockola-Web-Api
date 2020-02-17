using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiRestHistorial2.Models;

namespace ApiRestHistorial2.Controllers
{
    public class HistorialTController : ApiController
    {
        private DataProductsEntities db = new DataProductsEntities();

        // GET: api/HistorialT
        public IQueryable<HistorialT> GetHistorialTs()
        {
            return db.HistorialTs;
        }

        // GET: api/HistorialT/5
        [ResponseType(typeof(HistorialT))]
        public IHttpActionResult GetHistorialT(int id)
        {
            HistorialT historialT = db.HistorialTs.Find(id);
            if (historialT == null)
            {
                return NotFound();
            }

            return Ok(historialT);
        }

        // PUT: api/HistorialT/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHistorialT(int id, HistorialT historialT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != historialT.id)
            {
                return BadRequest();
            }

            db.Entry(historialT).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialTExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/HistorialT
        [ResponseType(typeof(HistorialT))]
        public IHttpActionResult PostHistorialT(HistorialT historialT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HistorialTs.Add(historialT);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = historialT.id }, historialT);
        }

        // DELETE: api/HistorialT/5
        [ResponseType(typeof(HistorialT))]
        public IHttpActionResult DeleteHistorialT(int id)
        {
            HistorialT historialT = db.HistorialTs.Find(id);
            if (historialT == null)
            {
                return NotFound();
            }

            db.HistorialTs.Remove(historialT);
            db.SaveChanges();

            return Ok(historialT);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HistorialTExists(int id)
        {
            return db.HistorialTs.Count(e => e.id == id) > 0;
        }
    }
}