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
    public class UserComptesController : ApiController
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: api/UserComptes
        public IQueryable<UserCompte> GetUserComptes()
        {
            return db.UserComptes;
        }

        //GetSearchuser
        [Route("api/UserComptes/GetUserCompte/{name}")]
        [ResponseType(typeof(List<UserCompte>))]
        public IHttpActionResult GetUserCompte(string name)
        {
            List<UserCompte> userComptes = db.UserComptes.Where(a=>a.FirstName.Contains(name)).ToList();
            if (userComptes == null)
            {
                return NotFound();
            }

            return Ok(userComptes);
        }

        [Route("api/UserComptes/GetUserCompteByEmail/{Email}")]
        [ResponseType(typeof(UserCompte))]
        public IHttpActionResult GetUserCompteByEmail(string Email)
        {
            UserCompte userCompte = db.UserComptes.Where(a => a.Email.Contains(Email.ToString())).FirstOrDefault();
            if (userCompte == null)
            {
                return NotFound();
            }

            return Ok(userCompte);
        }

        // GET: api/UserComptes/5
        [ResponseType(typeof(UserCompte))]
        public IHttpActionResult GetUserCompte(int id)
        {
            UserCompte userCompte = db.UserComptes.Find(id);
            if (userCompte == null)
            {
                return NotFound();
            }

            return Ok(userCompte);
        }

        // PUT: api/UserComptes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserCompte(int id, UserCompte userCompte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userCompte.Id)
            {
                return BadRequest();
            }

            db.Entry(userCompte).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCompteExists(id))
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

        // POST: api/UserComptes
        [ResponseType(typeof(UserCompte))]
        public IHttpActionResult PostUserCompte(UserCompte userCompte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserComptes.Add(userCompte);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userCompte.Id }, userCompte);
        }

        // DELETE: api/UserComptes/5
        [ResponseType(typeof(UserCompte))]
        public IHttpActionResult DeleteUserCompte(int id)
        {
            UserCompte userCompte = db.UserComptes.Find(id);
            if (userCompte == null)
            {
                return NotFound();
            }

            db.UserComptes.Remove(userCompte);
            db.SaveChanges();

            return Ok(userCompte);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserCompteExists(int id)
        {
            return db.UserComptes.Count(e => e.Id == id) > 0;
        }
    }
}