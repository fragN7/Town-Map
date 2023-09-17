using backend.Model;
using backend.Repo;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

public class MyController : ControllerBase
{
    private readonly DatabaseContext _context;

    public MyController(DatabaseContext context)
    {
        _context = context;
    }
    
    [Route("person/{name}")]
    public ActionResult<Person> GetPerson(string name)
    {
        return Ok(_context.Persons.FirstOrDefault(x => x.FullName == name));
    }
    
    [Route("person/{name}/parents")]
    public ActionResult<ICollection<Person>> GetPersonParents(string name)
    {
        var person = _context.Persons.FirstOrDefault(x => x.FullName == name);

        if (person == null)
        {
            throw new Exception("Person not found");
        }
        
        var mother = _context.Persons.FirstOrDefault(x => x.FullName == person.Mother);
        var father = _context.Persons.FirstOrDefault(x => x.FullName == person.Father);

        var parents = new List<Person>
        {
            mother!,
            father!
        };

        return Ok(parents);
    }
    
    [Route("person/{name}/kids")]
    public ActionResult<ICollection<Person>> GetPersonKids(string name)
    {
        var person = _context.Persons.FirstOrDefault(x => x.FullName == name);

        if (person == null)
        {
            throw new Exception("Person not found");
        }
        
        var kids = _context.Persons.Where(x => x.Mother == name || x.Father == name);

        return Ok(kids);
    }
    
    [Route("person/{name}/grandparents")]
    public ActionResult<ICollection<Person>> GetPersonGrandParents(string name)
    {
        var person = _context.Persons.FirstOrDefault(x => x.FullName == name);

        if (person == null)
        {
            throw new Exception("Person not found");
        }
        
        var mother = _context.Persons.FirstOrDefault(x => x.FullName == person.Mother);
        
        var motherGrandMother = _context.Persons.FirstOrDefault(x => x.FullName == mother!.Mother);
        var motherGrandFather = _context.Persons.FirstOrDefault(x => x.FullName == mother!.Father);

        var father = _context.Persons.FirstOrDefault(x => x.FullName == person.Father);
        
        var fatherGrandMother = _context.Persons.FirstOrDefault(x => x.FullName == father!.Mother);
        var fatherGrandFather = _context.Persons.FirstOrDefault(x => x.FullName == father!.Father);

        var grandParents = new List<Person>
        {
            motherGrandMother!,
            motherGrandFather!,
            fatherGrandMother!,
            fatherGrandFather!
        };

        return Ok(grandParents);
    }
    
    
    [Route("person/{name}/siblings")]
    public ActionResult<ICollection<Person>> GetPersonSiblings(string name)
    {
        var person = _context.Persons.FirstOrDefault(x => x.FullName == name);

        if (person == null)
        {
            throw new Exception("Person not found");
        }
        
        var mother = _context.Persons.FirstOrDefault(x => x.FullName == person.Mother);
        var father = _context.Persons.FirstOrDefault(x => x.FullName == person.Father);

        var siblings = _context.Persons.Where(x => x.Mother == mother!.FullName && x.Father == father!.FullName);

        return Ok(siblings);
    }
    
    [Route("person/{name}/cousins/first")]
    public ActionResult<ICollection<Person>> GetPersonFirstCousins(string name)
    {
        var person = _context.Persons.FirstOrDefault(x => x.FullName == name);

        if (person == null)
        {
            throw new Exception("Person not found");
        }
        
        var mother = _context.Persons.FirstOrDefault(x => x.FullName == person.Mother);
        
        var motherGrandMother = _context.Persons.FirstOrDefault(x => x.FullName == mother!.Mother);
        var motherGrandFather = _context.Persons.FirstOrDefault(x => x.FullName == mother!.Father);

        var father = _context.Persons.FirstOrDefault(x => x.FullName == person.Father);
        
        var fatherGrandMother = _context.Persons.FirstOrDefault(x => x.FullName == father!.Mother);
        var fatherGrandFather = _context.Persons.FirstOrDefault(x => x.FullName == father!.Father);

        var cousins = string.Empty;

        return Ok(cousins);
    }
}