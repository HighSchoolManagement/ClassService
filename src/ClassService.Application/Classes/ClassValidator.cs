namespace ClassService.Application.Classes
{
    public static class ClassValidator
    {
        public static async Task<T> GetOrThrowAsync<T>(Func<Task<T?>> getEntity, Func<Exception> createException)
        {
            var entity = await getEntity();
            if (entity == null)
            {
                throw createException();
            }
            return entity;
        }
    }
}