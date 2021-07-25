namespace Pecanha.Domain {
    public interface IServiceBase<T> where T : class {
        void Add(T obj);  
        void Update(T obj);        
    }
}
