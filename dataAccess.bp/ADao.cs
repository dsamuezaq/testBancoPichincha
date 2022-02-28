using model.bp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace dataAccess.bp
{
    public class ADao
    {
        public ContextBancoPichincha Context { get; }
        protected EntityState StateInsert = EntityState.Added;
        protected ADao(ContextBancoPichincha _contextBancoPichincha)
        {
            Context = _contextBancoPichincha;
        }
        public bool InsertUpdateOrDelete<T>(T entity, string transaction) where T : class
        {
            try
            {

                var stateRegister = transaction == "I" ? EntityState.Added : transaction == "U" ? EntityState.Modified : EntityState.Deleted; ;

            if (stateRegister != EntityState.Deleted)
            {
                Context.Set<T>().Add(entity);
                Context.Entry(entity).State = stateRegister;
            }
            else
            {
                Context.Set<T>().RemoveRange(entity);
            }



            Context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Existe problema en base de datos", ex);
      

            }

         
        }
        public T InsertUpdateOrDeleteSelectAll<T>(T entity, string transaction) where T : class
        {
            try
            {
                var stateRegister = transaction == "I" ? EntityState.Added : transaction == "U" ? EntityState.Modified : EntityState.Deleted; ;

                if (stateRegister != EntityState.Deleted)
                {
                    Context.Set<T>().Add(entity);
                    Context.Entry(entity).State = stateRegister;
                }
                else
                {
                    Context.Set<T>().Remove(entity);
                }



                Context.SaveChanges();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Existe problema en base de datos(e001)", ex);


            }



        }
        public List<T> GetPaginatedList<T>(int pageIndex, int pageSize) where T : class, IEntity
        {
            var sortedList = Context.Set<T>()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return sortedList;
        }


        public List<T> SelectEntity<T>() where T : class
        {

            try
            {
                var _dataTable = Context.Set<T>()
                         .ToList();
                return _dataTable;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message, ex);
            
            }

        }
        public void Update<T>(T entity) where T : class
        {
            Context.Update(entity);
        }

        public void PhysicalDelete<T>(T entity) where T : class
        {
            try
            {


                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    Context.Set<T>().Add(entity);
                }

                Context.Set<T>().Remove(entity);

                Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Existe problema en base de datos(e001)", ex);

            }

        }
    }
}
