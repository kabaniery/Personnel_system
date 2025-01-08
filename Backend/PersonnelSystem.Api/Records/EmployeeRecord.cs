namespace PersonnelSystem.Api.Records
{
    public record EmployeeRecord(
        int Id,
        string Name,
        int? subdivisionId);
}
