using PalladiumRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;

namespace PalladiumRecords.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DbModel db = new DbModel())
            {
                var patientList = db.Patients.ToList<Patient>();
                return Json(new { rows = patientList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddEditData(long id = 0)
        {
            return View(new Patient());
        }

        [HttpPost]
        public ActionResult AddEditData(Patient patient)
        {
            using (DbModel db = new DbModel())
            {
                db.Patients.AddOrUpdate(patient);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpDelete]       
        public ActionResult DeleteData(long patientId)
        {
            using (DbModel db = new DbModel())
            {                
                Patient patient = db.Patients.Where(p => p.PatientID == patientId).FirstOrDefault<Patient>();
                db.Patients.Remove(patient);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}