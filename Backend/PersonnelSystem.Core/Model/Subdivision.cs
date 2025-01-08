namespace PersonnelSystem.Core.Model
{
    public class Subdivision
    {
        public static readonly int MAX_NAME_LENGTH = 100;

        public int Id { get; }
        public string Name { get; }

        public int? HeaderId { get; }

        public List<int> Childs { get; } = new List<int>();

        public DateTime CreatedAt { get; }


        private Subdivision(int id, string name, DateTime createdAt, int? header, List<int>? childs)
        {
            Id = id;
            Name = name;
            HeaderId = header;
            CreatedAt = createdAt;
            if (childs != null)
            {
                Childs = childs;
            }
        }

        public static (Subdivision Subdivision, string Error) CreateSubdivision(int id, string name, DateTime createdAt)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGTH)
            {
                result = "Name cannot be empty or longer than 100 symbols";
            }
            return (new Subdivision(id, name, createdAt, null, null), result);
        }

        public static (Subdivision Subdivision, string Error) CreateSubdivision(int id, string name, DateTime createdAt, int? header, List<int>? childs)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGTH)
            {
                result = "Name cannot be empty or longer than 100 symbols";
            }
            return (new Subdivision(id, name, createdAt, header, childs), result);
        }
    }
}
