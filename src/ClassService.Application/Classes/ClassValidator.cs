namespace ClassService.Application.Classes
{
    public static class ClassValidator
    {
        public const int MaxNameLength = 100;

        public static async Task<T> GetOrThrowAsync<T>(Func<Task<T?>> getEntity, Func<Exception> createException)
        {
            var entity = await getEntity();
            if (entity == null)
            {
                throw createException();
            }
            return entity;
        }
        
        
        public static string NormalizeAndValidateName(string? raw)
        {
            var name = (raw ?? string.Empty).Trim();
            if (name == "") throw new ArgumentException("Name is empty");
            if (name.Length > MaxNameLength)
                throw new ArgumentException($"Name must not exceed {MaxNameLength} characters");
            return name;
        }
    }
}