using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using DefectLog.Core.Models;
using DefectLog.Core.Services;
using DefectLog.Web.Forms;
using DefectLog.Web.ViewModels;

namespace DefectLog.Web.Controllers
{
    public class DefectController : BaseApiController
    {
        private readonly IDefectService _defectService;

        public DefectController(IDefectService defectService)
        {
            _defectService = defectService;
        }

        // GET api/Defect
        public IEnumerable<DefectListItem> GetDefects(int versionId)
        {
            var defects = _defectService.GetAll(versionId)
                .Include(x => x.Status)
                .Include(x => x.Comments)
                .Include(x => x.Tester)
                .Include(x => x.PriorityLevel)
                .Include(x => x.Developer);

            return Mapper.Map<IEnumerable<DefectListItem>>(defects);
        }

        // GET api/Defect/5
        public Defect GetDefect(int id)
        {
            var defect = _defectService.Get(id);
        
            if (defect == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            return defect;
        }

        // PUT api/Defect/5
        public HttpResponseMessage PutDefect(int id, DefectForm form)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != form.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var defect = Mapper.Map<Defect>(form);
                _defectService.Update(defect);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Defect
        public HttpResponseMessage PostDefect(DefectForm form)
        {
            if (ModelState.IsValid)
            {
                var defect = Mapper.Map<Defect>(form);
                _defectService.Insert(defect);

                var response = Request.CreateResponse(HttpStatusCode.Created, form);

                //response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = defect.Id }));
                return response;
            }
            
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        // DELETE api/Defect/5
        //public HttpResponseMessage DeleteDefect(int id)
        //{
        //    Defect defect = db.Defects.Find(id);
        //    if (defect == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }

        //    db.Defects.Remove(defect);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, defect);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}