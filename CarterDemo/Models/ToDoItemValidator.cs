using FluentValidation;

namespace CarterDemo.Models
{
    public class ToDoItemValidator : AbstractValidator<ToDoItem>
    {
        public ToDoItemValidator()
        {
            RuleFor(item => item.Category).NotEmpty();
            RuleFor(item => item.Description).NotEmpty();
        }
    }
}
