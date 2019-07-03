namespace EstheticsPro.Core.BaseApi
{
    public interface IEntity
    {
        bool HasId();
        void SetId(long id);
    }
}
