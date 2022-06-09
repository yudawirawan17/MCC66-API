﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            return Ok(result);
        }

        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var result = repository.Get(key);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            var result = repository.Insert(entity);
            return Ok(result);
        }

        [HttpPut]
        public ActionResult Update(Entity entity)
        {
            var result = repository.Update(entity);
            return Ok(result);
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            var result = repository.Delete(key);
            return Ok(result);
        }
    }
}