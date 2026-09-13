namespace ClassService.Infrastructure.Common
{
    /// <summary>
    /// Các mã lỗi của SQL Server hay gặp khi SaveChangesAsync() vi phạm ràng buộc ở tầng DB.
    /// Tham khảo: https://learn.microsoft.com/sql/relational-databases/errors-events/database-engine-events-and-errors
    /// </summary>
    public static class SqlServerErrorCodes
    {
        /// <summary>
        /// Vi phạm UNIQUE INDEX — thường gặp khi index được tạo qua EF Core Fluent API
        /// kiểu builder.HasIndex(...).IsUnique().
        /// </summary>
        public const int UniqueIndexViolation = 2601;

        /// <summary>
        /// Vi phạm UNIQUE CONSTRAINT hoặc PRIMARY KEY constraint
        /// (loại được tạo bằng CONSTRAINT ... UNIQUE thay vì CREATE UNIQUE INDEX).
        /// </summary>
        public const int UniqueConstraintViolation = 2627;
    }
}
