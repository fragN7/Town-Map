﻿using backend.Model;
using backend.Repo;
using backend.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api")]
public class MyController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly PersonValidator _validator;

    public MyController(DatabaseContext context, PersonValidator validator)
    {
        _context = context;
        _validator = validator;
    }
    
    [HttpGet]
    [Route("persons")]
    public ActionResult<IEnumerable<Person>> GetPersons()
    {
        return Ok(_context.Persons.ToList());
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

    private async Task<IEnumerable<Person>> GetKidsAsync(string name)
    {
        var person = await _context.Persons.FirstOrDefaultAsync(x => x.FullName == name);

        if (person == null)
        {
            throw new Exception("Person not found");
        }

        var kids = await _context.Persons
            .Where(x => x.Mother == name || x.Father == name)
            .ToListAsync();

        return kids;
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

    private async Task<IEnumerable<Person>> GetSiblingsAsync(string name)
    {
        var person = await _context.Persons.SingleOrDefaultAsync(x => x.FullName == name);

        if (person == null)
        {
            throw new Exception("Person not found");
        }

        var mother = await _context.Persons.SingleOrDefaultAsync(x => x.FullName == person.Mother);
        var father = await _context.Persons.SingleOrDefaultAsync(x => x.FullName == person.Father);

        IEnumerable<Person> siblings;

        if (mother != null && father == null)
        {
            siblings = await _context.Persons.Where(x => x.Mother == mother.FullName).ToListAsync();
        }
        else if (father != null && mother == null)
        {
            siblings = await _context.Persons.Where(x => x.Father == father.FullName).ToListAsync();
        }
        else if (mother == null && father == null)
        {
            siblings = Enumerable.Empty<Person>();
        }
        else
        {
            siblings = await _context.Persons
                .Where(x => x.Father == father!.FullName && x.Mother == mother!.FullName)
                .ToListAsync();
        }

        siblings = siblings.Where(x => x.FullName != name);

        return siblings;
    }

    [HttpGet]
    [Route("person/{name}/cousins/first")]
    public async Task<ActionResult<IEnumerable<Person>>> GetPersonFirstCousins(string name)
    {
        var person = await _context.Persons.FirstOrDefaultAsync(x => x.FullName == name);

        if (person == null)
        {
            throw new Exception("Person not found");
        }

        var cousins = new List<Person>();

        var mother = await _context.Persons.FirstOrDefaultAsync(x => x.FullName == person.Mother);

        if (mother != null)
        {
            var motherSiblings = await GetSiblingsAsync(mother.FullName);
            foreach (var sibling in motherSiblings)
            {
                var siblingMotherKids = await GetKidsAsync(sibling.FullName);
                cousins.AddRange(siblingMotherKids);
            }
        }

        var father = await _context.Persons.FirstOrDefaultAsync(x => x.FullName == person.Father);

        if (father != null)
        {
            var fatherSiblings = await GetSiblingsAsync(father.FullName);
            foreach (var sibling in fatherSiblings)
            {
                var siblingFatherKids = await GetKidsAsync(sibling.FullName);
                cousins.AddRange(siblingFatherKids);
            }
        }

        return Ok(cousins);
    }

    [HttpPost]
    [Route("insert")]
    public ActionResult<Person> AddPerson([FromBody] Person person)
    {
        var result = _validator.ValidatePerson(person);
        if (result != string.Empty)
        {
            throw new Exception(result);
        }

        _context.Persons.Add(person);
        _context.SaveChanges();
        return Ok(person);
    }

}