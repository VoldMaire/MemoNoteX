namespace MemoNoteModel
{
    [DataBaseClass("User")]
    public class User : ActiveRecord<User>
    {
        [DataBaseProperty("Name")]
        public string Name { get; set; }

        [DataBaseProperty("Login")]
        public string Login { get; set; }

        [DataBaseProperty("Password")]
        public string Password { get; set; }

        public User()
        {
        }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return Name + "(" + Login + ")";
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            User u = obj as User;
            if (u == null)
            {
                return false;
            }

            return u.Login == this.Login && u.Name == this.Name &&
                   u.Password == this.Password;
        }
    }
}
