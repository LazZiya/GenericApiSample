using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Data;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    /// <summary>
    /// Generic controller accepts any entity of type T that implements 
    /// IHasId<TKey> interface, so the entity Id can be of type int, string, etc...
    /// </summary>
    /// <typeparam name="T">type of the entity</typeparam>
    /// <typeparam name="TKey">Id type e.g. int or string</typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public abstract class GenericController<T, TKey> : ControllerBase
        where T : class, IHasId<TKey>
        where TKey : IEquatable<TKey> 
    {
        private readonly ApplicationDbContext _context;
        public GenericController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Generic
        [HttpGet]
        public IEnumerable<T> Get()
        {
            return _context.Set<T>().AsEnumerable();
        }

        // GET: api/Generic/5
        // Notice: dont use "Name = "Get" in generic controller e.g. [HttpGet("{id}", Name = "Get")]
        // because route names must be different in controllers
        [HttpGet("{id}")]
        public T Get(TKey id)
        {
            return _context.Set<T>().Find(id);
        }

        // POST: api/Generic
        [HttpPost]
        public void Post([FromBody] T value)
        {
            _context.Set<T>().Add(value);
            _context.SaveChanges();
        }

        // PUT: api/Generic/5
        [HttpPut("{id}")]
        public void Put(TKey id, [FromBody] T value)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                entity = value;
                _context.Entry<T>(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(TKey id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Entry<T>(entity).State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }
    }
}
