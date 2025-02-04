using System.Collections.Generic;

namespace SalonFryzjerski.Services
{
    public interface IEntityService<T>
    {
        List<T> LoadData(); // Pobiera dane z bazy
        bool IsValid(T entity, out string errors); // Waliduje dane encji
        bool SaveData(T entity); // Dodaje lub aktualizuje encjê w bazie
        bool DeleteData(int id); // Usuwa encjê na podstawie ID
    }
}
