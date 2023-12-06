using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace OOP_Project
{
    public class UserJSONDao : Dao<User>, IUserDao<User>
    {
        private const string JsonFilePath = "users.json";
        private List<User> users;

        protected override List<IEntity> Entities { get => users.Cast<IEntity>().ToList(); set => users = value.Cast<User>().ToList(); }

        public UserJSONDao()
        {
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(JsonFilePath))
            {
                string json = File.ReadAllText(JsonFilePath);
                users = JsonSerializer.Deserialize<List<User>>(json);
            }
            else
            {
                users = new List<User>();
            }
        }

        private void SaveData()
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(JsonFilePath, json);
        }

        public override User Create(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (users.Any(u => u.Nickname == entity.Nickname))
            {
                throw new InvalidOperationException($"User with nickname '{entity.Nickname}' already exists.");
            }

            entity.Id = GenerateId();
            users.Add(entity);
            SaveData();

            return entity; 
        }

        public override void Delete(int id)
        {
            User userToRemove = users.Find(u => u.Id == id);
            if (userToRemove != null)
            {
                users.Remove(userToRemove);
                SaveData();
            }
        }

        public override IEnumerable<User> Find(Predicate<User> filter = null)
        {
            return filter != null ? users.FindAll(filter) : users;
        }

        public User FindByNickname(string nickname)
        {
            return users.Find(u => u.Nickname == nickname);
        }

        public override User FindOne(int id)
        {
            return users.Find(u => u.Id == id);
        }

        public override void Update(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            User existingUser = users.Find(u => u.Id == entity.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Password = entity.Password;
                existingUser.UpdatedAt = DateTime.Now;

                SaveData();
            }
        }        
    }
}
