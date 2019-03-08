﻿using HealthCheck.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheck.API.Services
{
    public class SessionService
    {
        private readonly Repository.Repository repository;

        public SessionService(IConfiguration config)
        {
            repository = new Repository.Repository(config.GetConnectionString("HealthCheckDB"), "healthcheck");
        }

        public async Task<Session> Create(Session session)
        {
            await repository.Insert<Session>(session);
            return session;
        }

        public async Task<IEnumerable<Session>> GetAll()
        {
            return await repository.List<Session>();
        }

        public async Task<Session> Get(string id)
        {
            var docId = new ObjectId(id);
            return await repository.Single<Session>(session => session._id == docId);
        }

        public async Task<Session> GetBySessionKey(string sessionKey)
        {
            return await repository.Single<Session>(session => session.SessionKey.Equals(sessionKey));
        }

        public async Task<IEnumerable<Session>> GetByCreatedById(string createdById)
        {
            return await repository.List<Session>(session => session.CreatedBy.Equals(createdById));
        }

        public async Task Delete(Session session)
        {
            await repository.Delete<Session>(session);
        }

        public async Task Update(string id, Session session)
        {
            var docId = new ObjectId(id);
            await repository.Update<Session>(id, session);
        }
    }
}