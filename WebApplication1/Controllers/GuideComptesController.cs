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
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GuideComptesController : ApiController
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: api/GuideComptes
        public IQueryable<GuideCompte> GetGuideComptes()
        {
            return db.GuideComptes;
        }

        // GET: api/GuideComptes/5
        [ResponseType(typeof(GuideCompte))]
        public IHttpActionResult GetGuideCompte(int id)
        {
            GuideCompte guideCompte = db.GuideComptes.Find(id);
            if (guideCompte == null)
            {
                return NotFound();
            }

            return Ok(guideCompte);
        }

        [Route("api/GuideComptes/GetGuideCompteByEmail/{Email}")]
        [ResponseType(typeof(GuideCompte))]
        public IHttpActionResult GetGuideCompteByEmail(string Email)
        {
            GuideCompte userCompte = db.GuideComptes.Where(a => a.Email.Contains(Email.ToString())).FirstOrDefault();
            if (userCompte == null)
            {
                return NotFound();
            }

            return Ok(userCompte);
        }

        // PUT: api/GuideComptes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGuideCompte(int id, GuideCompte guideCompte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guideCompte.Id)
            {
                return BadRequest();
            }

            db.Entry(guideCompte).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuideCompteExists(id))
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

        // POST: api/GuideComptes
        [ResponseType(typeof(GuideCompte))]
        public IHttpActionResult PostGuideCompte(GuideCompte guideCompte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GuideComptes.Add(guideCompte);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = guideCompte.Id }, guideCompte);
        }

        // DELETE: api/GuideComptes/5
        [ResponseType(typeof(GuideCompte))]
        public IHttpActionResult DeleteGuideCompte(int id)
        {
            GuideCompte guideCompte = db.GuideComptes.Find(id);
            if (guideCompte == null)
            {
                return NotFound();
            }

            db.GuideComptes.Remove(guideCompte);
            db.SaveChanges();

            return Ok(guideCompte);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuideCompteExists(int id)
        {
            return db.GuideComptes.Count(e => e.Id == id) > 0;
        }
    }
}