using System.Linq;
using System.Web.Mvc;
using CarModificationCenter.Models;

namespace CarModificationCenter.Controllers
{
    [AllowAnonymous]
    public class ProjectController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Projects
        public ActionResult CompletedProjects()
        {
            return View();
        }

        // GET: ProjectProgress
        public ActionResult ProjectProgress()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProjectProgress(project model)
        {
                var getProjectName = db.Project.Where(u => u.ProjectID == model.ProjectID).Select(u => u.ProjectName);
                var materializeProjectName = getProjectName.ToList();
                var ProjectName = materializeProjectName[0];

                var getType = db.Project.Where(u => u.ProjectID == model.ProjectID).Select(u => u.Type);
                var materializeType = getType.ToList();
                var Type = materializeType[0];

                var getDesign = db.Project.Where(u => u.ProjectID == model.ProjectID).Select(u => u.Design);
                var materializeDesign = getDesign.ToList();
                var Design = materializeDesign[0];

                var getAcceptedDate = db.Project.Where(u => u.ProjectID == model.ProjectID).Select(u => u.AcceptedDate);
                var materializeAcceptedDate = getAcceptedDate.ToList();
                var AcceptedDate = materializeAcceptedDate[0];

                var getDeadline = db.Project.Where(u => u.ProjectID == model.ProjectID).Select(u => u.Deadline);
                var materializeDeadline = getDeadline.ToList();
                var Deadline = materializeDeadline[0];

                var getStatus = db.Project.Where(u => u.ProjectID == model.ProjectID).Select(u => u.Status);
                var materializeStatus = getStatus.ToList();
                var Status = materializeStatus[0];

                //var getProjectId = db.Project.Where(u => u.CustomerID == model.CustomerID).Select(u => u.ProjectID);
                //var materializeProjectId = getProjectId.ToList();
                //var ProjectId = materializeProjectId[0];

                model.Type = Type;
                model.Design = Design;
                model.AcceptedDate = AcceptedDate;
                model.Deadline = Deadline;
                model.Status = Status;
                model.ProjectName = ProjectName;

                return View(model);
        }
    }
}