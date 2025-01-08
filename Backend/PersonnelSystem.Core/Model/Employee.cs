namespace PersonnelSystem.Core.Model
{
    public class Employee
    {
        public static readonly int MAX_NAME_LENGTH = 100;


        public int Id { get; set; }
        public string Name { get; }
        public int? SubdivisionId { get; }

        private Employee(int id, string name, int? subdivisionId) 
        {
            Id = id;
            Name = name;
            SubdivisionId = subdivisionId;
        }

        public static (Employee Employee, string Error) CreateEmployee(int id, string name, int? subdivisionId)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGTH)
            {
                result = "Name cannot be empty or longer than 100 symbols";
            }
            return (new Employee(id, name, subdivisionId), result);
        }
    }
}
