using FluentValidation;
using RestoranOtomasyonu.Entities.Intefaces;
using RestoranOtomasyonu.Entities.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Repository
{
    public class EntityRepositoryBase<TContext, TEntity,TValidator> : IEntitiyRepository<TContext, TEntity>
        where TContext : DbContext, new()
        where TEntity : class,IEntity ,new()
        where TValidator : IValidator, new()
    {
        public bool AddOrUpdate(TContext context, TEntity entity)
        {
            TValidator validator = new TValidator();
            bool validationResult = ValidatorTools.Validates(validator, entity);
            if (validationResult)
            {
                context.Set<TEntity>().AddOrUpdate(entity);
            }
            return validationResult;
        }

        public void Delete(TContext context, Expression<Func<TEntity, bool>> filter)
        {
            context.Set<TEntity>().Remove(context.Set<TEntity>().FirstOrDefault(filter));
        }

        public List<TEntity> GetAll(TContext context, Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
        }

        public TEntity GetByFilter(TContext context, Expression<Func<TEntity, bool>> filter)
        {
            return context.Set<TEntity>().FirstOrDefault(filter);
        }

        public void Save(TContext context)
        {
            context.SaveChanges();
        }
    }
}
