using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
  class Program
  {
    interface IValidationStrategy
    {
      void Validate();
    }

    abstract class AbstractValidationStrategy<T> : IValidationStrategy
    {
      protected T ObjToValidate { get; set; }

      public AbstractValidationStrategy(T obj)
      {
        ObjToValidate = obj;
      }

      // Algorithme encapsulé
      public abstract void Validate();
    }

    class User
    {
      public string FirstName { get; set; }
      public int Age { get; set; }
    }

    class UserOver18Validator : AbstractValidationStrategy<User>
    {
      public UserOver18Validator(User obj) : base(obj)
      {
      }

      public override void Validate()
      {
        // Algorithme encapsulé
        if (this.ObjToValidate.Age < 18) throw new Exception("User must be over 18");
      }
    }

    class ValidationContext
    {
      private List<IValidationStrategy> Validators { get; set; }

      public ValidationContext()
      {
        Validators = new List<IValidationStrategy>();
      }

      public ValidationContext AddValidationStrategy(IValidationStrategy validationStrategy)
      {
        Validators.Add(validationStrategy);
        return this;
      }

      public ValidationContext ApplyValidations()
      {
        this.Validators.ForEach(x => x.Validate());
        return this;
      }
    }


    static void Main(string[] args)
    {
      User someoneTooYoung = new User()
      {
        FirstName = "Steeve",
        Age = 14
      };

      ValidationContext context = new ValidationContext()
        .AddValidationStrategy(new UserOver18Validator(someoneTooYoung))
        .ApplyValidations();

    }
  }
}
