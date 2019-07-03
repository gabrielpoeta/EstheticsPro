using System.Collections.Generic;
using EstheticsPro.Core.ADO;

namespace EstheticsPro.Core.BaseApi
{
    public abstract class BaseService<T>
        where T : class, IEntity
    {
        private readonly UnitOfWork _unitOfWork;

        protected BaseService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected T Get<K>(K id)
            => _unitOfWork.Get<T, K>(id);
        protected List<T> GetAll()
            => _unitOfWork.GetAll<T>();
        protected long Insert(T entity)
            => _unitOfWork.Insert(entity);
        protected long Insert(IEnumerable<T> entities)
            => _unitOfWork.Insert(entities);
        protected bool Update(T entity)
            => _unitOfWork.Update(entity);
        protected bool Update(IEnumerable<T> entities)
            => _unitOfWork.Update(entities);
        protected bool Delete(T entity)
            => _unitOfWork.Delete(entity);
        protected bool Delete(IEnumerable<T> entities)
            => _unitOfWork.Delete(entities);
        protected bool DeleteAll()
            => _unitOfWork.DeleteAll<T>();

        public virtual T Post(T entity)
        {
            if (entity.HasId())
            {
                Update(entity);
                return entity;
            }
            entity.SetId(Insert(entity));
            return entity;
        }

    }
}
