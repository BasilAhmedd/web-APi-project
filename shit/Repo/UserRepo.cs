namespace shit.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _context;
        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUserById(int Id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == Id);
        }
        public void AddUser(UserDTO dto)
        {
            var u = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
            };
            _context.Users.Add(u);
            _context.SaveChanges();
        }
        public void UpdateUser(UserDTO dto)
        {
            var user = GetUserById(dto.Id);
            user.Email = dto.Email;
            user.Name = dto.Name;
            user.Password = dto.Password;
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
         
            _context.Users.Remove(user);
            _context.SaveChanges();
            
        }

        public User ValidateUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            
        }
    }
}

//private readonly ApplicationDbContext _context;

//public UserRepo(ApplicationDbContext context)
//{
//    _context = context;
//}
//public void AddUser(User user)
//{
//    var u = new User
//    {
//        Name = user.Name,
//        Email = user.Email,
//    };
//    _context.Users.Add(u);
//    _context.SaveChanges();

//}

//public void DeleteUser(User user)
//{
//    throw new NotImplementedException();
//}

//public User GetUserById(int Id)
//{
//    return _context.Users.FirstOrDefault(x => x.Id == Id); 
//}

//public void UpdateUser(User user)
//{
//    var u = _context.Users.FirstOrDefault(u => u.Id == user.Id);
//}