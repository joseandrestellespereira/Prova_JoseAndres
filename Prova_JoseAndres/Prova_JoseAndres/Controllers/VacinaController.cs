using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prova_JoseAndres.Models;
using MongoDB.Bson;
using MongoDB.Driver;
namespace Prova_JoseAndres.Controllers
{
    public class VacinaController : Controller
    {
		private readonly MongoDBContext _mongoDBContext =
			new MongoDBContext();

		public IActionResult Index()
		{
			return View(_mongoDBContext.vacina.Find(s => true)
				.ToList());
		}

		public IActionResult Create() => View();

		[HttpPost]
		public IActionResult Create(Vacina vacina)
		{
			if (ModelState.IsValid)
			{
				_mongoDBContext.vacina.InsertOne(vacina);
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpGet]
		public IActionResult Delete(string Id)
		{
			var vacinaDel = _mongoDBContext.vacina
				.Find(s => s.Id == ObjectId.Parse(Id)).FirstOrDefault();
			return View(vacinaDel);
		}

		[HttpGet]
		public IActionResult Details(string Id)
		{
			var vacinaDel = _mongoDBContext.vacina
				.Find(s => s.Id == ObjectId.Parse(Id)).FirstOrDefault();
			return View(vacinaDel);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(string Id)
		{
			var vacinaDel = _mongoDBContext.vacina
				.DeleteOne(s => s.Id == ObjectId.Parse(Id));
			return RedirectToAction("Index");
		}

		public ActionResult Edit(string Id)
		{
			var serv = _mongoDBContext.vacina.Find(s => s.Id == ObjectId.Parse(Id)).FirstOrDefault();
			return View(serv);
		}

		[HttpPost]
		public ActionResult Edit(Vacina vacina, string Id)
		{
			if (ModelState.IsValid)
			{
				vacina.Id = ObjectId.Parse(Id);
				var filter = new BsonDocument("_id", vacina.Id);
				//var filter = Builders<Servidor>.Filter.Eq(s => s.Id, servidor.Id);
				_mongoDBContext.vacina.ReplaceOne(filter, vacina);

				return RedirectToAction("Index");
			}
			return View();
		}
	}
}