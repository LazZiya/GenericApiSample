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
    /// ...
    /// All methods exposed as virtual, so it can be overridden in inherited api controllers
    /// </summary>
    /// <typeparam name="T">type of the entity</typeparam>
    /// <typeparam name="TKey">Id type e.g. int or string</typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public abstract class GenericBaseController<T, TKey> : ControllerBase
        where T : class, IHasId<TKey>, IOrdered
        where TKey : IEquatable<TKey>
    {
        private readonly ApplicationDbContext _context;
        public GenericBaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Generic
        [HttpGet("list/{pageNo}-{pageSize}")]
        public virtual (IEnumerable<T>, int) Get(int pageNo, int pageSize)
        {
            var query = _context.Set<T>();

            var totalRecords = query.Count();
            var items = query.OrderBy(x => x.Name)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .AsEnumerable();

            return (items, totalRecords);
        }

        // GET: api/Generic/5
        // Notice: dont use "Name = "Get" in generic controller e.g. [HttpGet("{id}", Name = "Get")]
        // because route names must be different in controllers
        [HttpGet("{id}")]
        public virtual T Get(TKey id)
        {
            return _context.Set<T>().Find(id);
        }

        // POST: api/Generic
        [HttpPost]
        public virtual bool Post([FromBody] T value)
        {
            _context.Set<T>().Add(value);
            return _context.SaveChanges() > 0;
        }

        // PUT: api/Generic/5
        [HttpPut("{id}")]
        public virtual bool Put(TKey id, [FromBody] T value)
        {
            var entity = _context.Set<T>().AsNoTracking().SingleOrDefault(x => x.Id.Equals(id));
            if (entity != null)
            {
                _context.Entry<T>(value).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }

            return false;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public virtual bool Delete(TKey id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Entry<T>(entity).State = EntityState.Deleted;
                return _context.SaveChanges() > 0;
            }

            return false;
        }
    }
}
