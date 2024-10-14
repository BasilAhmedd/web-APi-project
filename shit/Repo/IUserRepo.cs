namespace shit.Repo
{
    public interface IUserRepo
    {
        User GetUserById(int Id);
        void UpdateUser(UserDTO dto);
        void AddUser(UserDTO dto);
        void DeleteUser(int id);
    }
}
