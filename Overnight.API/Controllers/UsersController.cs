using Microsoft.AspNetCore.Mvc;
using Overnight.API.Models;
using Overnight.EF;
using Overnight.EF.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Overnight.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly OvernightContext _context;

		public UsersController(OvernightContext context)
		{
			_context = context;
		}

		// GET: api/<UsersController>
		[HttpGet]
		public IEnumerable<UserDTO> Get()
		{
			IEnumerable<UserDTO> users = _context.Users.Select(x => new UserDTO { Id = x.UserId, FirstName = x.FirstName, LastName = x.LastName, Email = x.Email });
			return users;
		}

		// GET api/<UsersController>/5
		[HttpGet("{id}")]
		public UserDTO Get(int id)
		{
			var user = _context.Users.Where(x => x.UserId == id).Select(x => new UserDTO
			{
				Id = x.UserId,
				Email = x.Email,
				FirstName = x.FirstName,
				LastName = x.LastName,
			}).FirstOrDefault();

			return user;
		}

		// POST api/<UsersController>
		[HttpPost]
		public void Post([FromBody] UserDTO user)
		{
			_context.Users.Add(new User { FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, Password = user.Password, CreatedDate = DateTime.UtcNow });
			_context.SaveChanges();
		}

		// PUT api/<UsersController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] UserDTO user)
		{
			var _ = _context.Users.FirstOrDefault(x => x.UserId == id);
			_.Email = user.Email;
			_.FirstName = user.FirstName;
			_.LastName = user.LastName;

			_context.Users.Update(_);
			_context.SaveChanges();
		}

		// DELETE api/<UsersController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			_context.Users.Remove(_context.Users.Find(id));
			_context.SaveChanges();
		}
	}
}
