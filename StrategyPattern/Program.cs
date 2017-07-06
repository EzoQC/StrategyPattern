using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
  class Program
  {
    abstract class AbstractValidator<T>
    {
      protected T ObjToValidate { get; set; }

      public AbstractValidator(T obj)
      {
        ObjToValidate = obj; 
      }


      public abstract void Validate();
    }

    class User
    {
      public string FirstName { get; set; }
      public int Age { get; set; }
    }

    class UserValidator : AbstractValidator<User>
    {
      public UserValidator(User obj) : base(obj)
      {
      }

      public override void Validate()
      {
        if (this.ObjToValidate.FirstName == null) throw new Exception("A user needs a first name");
      }
    }

    class UserOver18Validator : AbstractValidator<User>
    {
      public UserOver18Validator(User obj) : base(obj)
      {
      }

      public override void Validate()
      {
        if (this.ObjToValidate.Age < 18) throw new Exception("User must be over 18");
      }
    }

    class ValidationContext<T>
    {
      private AbstractValidator<T> Validator { get; set; }

      public ValidationContext(AbstractValidator<T> validator)
      {
        this.Validator = validator;
      }

      public void ApplyValidation()
      {
        this.Validator.Validate();
      }
    }


    static void Main(string[] args)
    {
      User someoneTooYoung = new User()
      {
        FirstName = "Steeve",
        Age = 14
      };

      

      try
      {
        ValidationContext<User> context = new ValidationContext<User>(new UserOver18Validator(someoneTooYoung));
        context.ApplyValidation();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }

      User someUnnamedOne = new User()
      {
        FirstName = null,
        Age = 42
      };

      try
      {
        ValidationContext<User> context = new ValidationContext<User>(new UserValidator(someUnnamedOne));
        context.ApplyValidation();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }

    }
  }
}
