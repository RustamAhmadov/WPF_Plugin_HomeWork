using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public enum Gender { Male, Female, Others }
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MaxLength(20)]
        public string LastName { get; set; }

        [Required, MaxLength(20)]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }
        public string PicturePath { get; set; }

        public User(string firstName, string lastName, int age, string picturePath) { }
        public User() { }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
