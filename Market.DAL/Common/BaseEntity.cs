namespace Market.DAL.Common
{
    /// <summary>
    /// Базовая сущность для создания пользователей с указанным generic id.
    /// </summary>
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
