﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Prova_JoseAndres.Models
{
	public class MongoDBContext
	{
		private IMongoDatabase _database { get; }

		public MongoDBContext()
		{
			MongoClientSettings settings =
				MongoClientSettings.FromUrl(new MongoUrl("mongodb://localhost:27017"));
			var mongoClient = new MongoClient(settings);
			_database = mongoClient.GetDatabase("vacina");
		}

		public IMongoCollection<Vacina> vacina
		{
			get
			{
				//Recupera colection de documents
				return _database.GetCollection<Vacina>("vacina");
			}
		}
	}
}
