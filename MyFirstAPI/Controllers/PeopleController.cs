using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyPeopleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        //public List<string> myPeople = new List<string>() { "ID", "Name", "HairColor" };

        private static readonly List<Person> _people = new List<Person>
        {
            new Person
            {
                Id = 1,
                Name = "Big chungus",
                HairColor = "Black"
            },
            new Person
            {
                Id = 2,
                Name = "Larry King",
                HairColor = "Purple"
            }
        };

        //finding an id
        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult Get(int id)
        {
            if(id > _people.Count)
            {
                return NotFound();
            }

            //This variable uses linq to find the first element with the given id of x
            var person = _people.FirstOrDefault(p => p.Id == id);
            //if we cant find that element return not found so we dont flood the user with gibberish
            if (person == null) return NotFound();
            //if not null, return what we found in our linq method
            return Ok(person);
        }

        /*
        //Finding a name
        [HttpGet("{name, haircolor}")]
        [Produces("application/json")]
        //         name          haircolor
        public (IActionResult, IActionResult) Get(string input)
        {
            //This variable uses linq to find the first element with the given y of x
            
            var name = _people.FirstOrDefault(p => p.Name == input);
            var hairColor = _people.FirstOrDefault(s => s.HairColor == input);

            //if both return no value
            if (name == null && hairColor == null)
            {
                return (NotFound(), NotFound());
            }

            if(name == null)
            {
                return (NotFound(), Ok(hairColor));
            }
            else if(hairColor == null)
            {
                return (Ok(name), NotFound());
            }

            return (Ok(name), Ok(hairColor));
        }
        */

        //this identifies from POST
        [HttpPost]
        public IActionResult Post([FromBody] Person newPerson)
        {
            _people.Add(newPerson);
            //Creates a 201
            return CreatedAtAction("Get", newPerson, new { id = new Random().Next() });
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_people);
        }
    }
}
