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
    
    [HttpGet]
    [Route("person/{name}")]
    public ActionResult<Person> GetPerson(string name)
    {
        return Ok(_context.Persons.FirstOrDefault(x => x.FullName == name));
    }
    
    [HttpGet]
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
    
    [HttpGet]
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
    
    [HttpGet]
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
    
    [HttpGet]
    [Route("person/{name}/siblings")]
    public ActionResult<IEnumerable<Person>> GetPersonSiblings(string name)
    {
        var person = _context.Persons.FirstOrDefault(x => x.FullName == name);

        if (person == null)
        {
            throw new Exception("Person not found");
        }

        var mother = _context.Persons.FirstOrDefault(x => x.FullName == person.Mother);
        var father = _context.Persons.FirstOrDefault(x => x.FullName == person.Father);

        IEnumerable<Person> siblings;

        if (mother != null && father == null)
        {
            siblings = _context.Persons.Where(x => x.Mother == mother.FullName);
        }
        else if (father != null && mother == null)
        {
            siblings = _context.Persons.Where(x => x.Father == father.FullName);
        }
        else if (mother == null && father == null)
        {
            siblings = _context.Persons.Where(x => x.FullName == "test");
        }
        else
        {
            siblings = _context.Persons.Where(x => x.Father == father!.FullName && x.Mother == mother!.FullName);
        }

        siblings = siblings.Where(x => x.FullName != name);

        return Ok(siblings);
    }

    [HttpGet]
    [Route("person/{name}/cousins/first")]
    public ActionResult<IEnumerable<Person>> GetPersonFirstCousins(string name)
    {
        var person = _context.Persons.FirstOrDefault(x => x.FullName == name);

        if (person == null)
        {
            throw new Exception("Person not found");
        }

        var cousins = new List<Person>();
        
        Person? motherGrandMother = null;
        Person? motherGrandFather = null;
        Person? fatherGrandMother = null;
        Person? fatherGrandFather = null;

        var mother = _context.Persons.FirstOrDefault(x => x.FullName == person.Mother);

        if (mother != null)
        {
            motherGrandMother = _context.Persons.FirstOrDefault(x => x.FullName == mother.Mother);
            motherGrandFather = _context.Persons.FirstOrDefault(x => x.FullName == mother.Father);
        }
        
        var father = _context.Persons.FirstOrDefault(x => x.FullName == person.Father);

        if (father != null)
        {
            fatherGrandMother = _context.Persons.FirstOrDefault(x => x.FullName == father.Mother);
            fatherGrandFather = _context.Persons.FirstOrDefault(x => x.FullName == father.Father);
        }

        var persons = _context.Persons.ToList();
        foreach (var people in persons)
        {
            Person? peopleMotherGrandMother = null;
            Person? peopleMotherGrandFather = null;
            Person? peopleFatherGrandMother = null;
            Person? peopleFatherGrandFather = null;
            
            var peopleMother = _context.Persons.FirstOrDefault(x => x.FullName == people.Mother);

            if (peopleMother != null)
            {
                peopleMotherGrandMother = _context.Persons.FirstOrDefault(x => x.FullName == peopleMother.Mother);
                peopleMotherGrandFather = _context.Persons.FirstOrDefault(x => x.FullName == peopleMother.Father);
                
                
            }
        
            var peopleFather = _context.Persons.FirstOrDefault(x => x.FullName == people.Father);

            if (peopleFather != null)
            {
                peopleFatherGrandMother = _context.Persons.FirstOrDefault(x => x.FullName == peopleFather.Mother)!;
                peopleFatherGrandFather = _context.Persons.FirstOrDefault(x => x.FullName == peopleFather.Father)!;
            }
        }
        

        return Ok(cousins);
    }

}