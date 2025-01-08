using System.Text.Json;
using System.IO;

namespace Services
{
  public interface IStorageService
  {
    void Register(Person person);
    IEnumerable<Person> GetAll();
    
    //To delete the persons
    void Delete(Guid id);
  }
  public class Person
  {
    public string Name { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
    public Guid Id { get; set; }
  
  }

  public class StorageService : IStorageService
  {
    private const string storagePath = "./storage";

    public void Register(Person person)
    {
      var newPerson = new Person
      {
        Id = Guid.NewGuid(),
        Age = person.Age,
        Lastname = person.Lastname,
        Name = person.Name,
      };
      var serializedPerson = JsonSerializer.Serialize(newPerson);
      if (!Directory.Exists(storagePath))
      {
        Directory.CreateDirectory(storagePath);
      }
      File.WriteAllText($"{storagePath}/person-{newPerson.Id}.json", serializedPerson);
    }

    public IEnumerable<Person> GetAll()
    {
      var people = new List<Person>();
      if (Directory.Exists(storagePath))
      {
        var files = Directory.EnumerateFiles(storagePath);
        foreach (var file in files)
        {
          var serializedPerson = File.ReadAllText(file);
          var person = JsonSerializer.Deserialize<Person>(serializedPerson);
          if (person != null)
            people.Add(person);
        }
      }
      return 
        people
        .ToArray();
    }

    public void Delete(Guid id)
    {
      if (Directory.Exists(storagePath))
      {
        var record = 
          Directory.EnumerateFiles(storagePath).FirstOrDefault(file => file.Contains(id.ToString()));
        if (record != null)
        {
          File.Delete(record);
        }
      }
    }
  }
}