using System.Text.Json;

namespace OOP_Project
{
    public class ExpenseJSONDao : Dao<Expense>
    {
        private const string JsonFilePath = "expenses.json";
        private List<Expense> expenses;

        protected override List<IEntity> Entities { get => expenses.Cast<IEntity>().ToList(); set => expenses = value.Cast<Expense>().ToList(); }

        public ExpenseJSONDao()
        {
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(JsonFilePath))
            {
                string json = File.ReadAllText(JsonFilePath);
                expenses = JsonSerializer.Deserialize<List<Expense>>(json);
            }
            else
            {
                expenses = new List<Expense>();
            }
        }

        private void SaveData()
        {
            string json = JsonSerializer.Serialize(expenses, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(JsonFilePath, json);
        }

        public override Expense Create(Expense entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (expenses.Any(e => e.Name == entity.Name && e.UserId == entity.UserId))
            {
                throw new InvalidOperationException($"Expense with name'{entity.Name}' already exists.");
            }

            entity.Id = GenerateId();
            expenses.Add(entity);
            SaveData();

            return entity;
        }

        public override void Delete(int id)
        {
            Expense ExpenseToRemove = expenses.Find(e => e.Id == id);
            if (ExpenseToRemove != null)
            {
                expenses.Remove(ExpenseToRemove);
                SaveData();
            }
        }

        public override IEnumerable<Expense> Find(Predicate<Expense> filter = null)
        {
            return filter != null ? expenses.FindAll(filter) : expenses;
        }
        

        public override Expense FindOne(int id)
        {
            return expenses.Find(u => u.Id == id);
        }

        public override void Update(Expense entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Expense existingExpense = expenses.Find(u => u.Id == entity.Id);
            if (existingExpense != null)
            {
                existingExpense.Name = entity.Name;
                existingExpense.Category = entity.Category;
                existingExpense.Amount = entity.Amount;
                existingExpense.UserId = entity.UserId;
                existingExpense.UpdatedAt = DateTime.Now;

                SaveData();
            }
        }

    }
}
