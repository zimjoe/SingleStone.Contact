using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleStone.Contact.Service.Validators
{
    public class ContactNameValidator : AbstractValidator<Models.ContactName>
    {
		public ContactNameValidator()
		{

			RuleFor(x => x.First).Length(0, 50);
			RuleFor(x => x.Middle).Length(0, 50);
			RuleFor(x => x.Last).Length(0, 50);
		}
	}
}
