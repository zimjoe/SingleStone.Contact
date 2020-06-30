using SingleStone.Contact.Service.Validators;
using SingleStone.Contact.Service.Models;
using System;
using Xunit;
using FluentValidation;
using FluentValidation.TestHelper;

namespace SingleStone.Contact.Tests
{
    public class AddressValidatorShould
    {
        public AddressValidatorShould(){
            validator = new AddressValidator();
        }
        private AddressValidator validator;

        [Fact]
        public void NotAllowNullStreet()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Street, null as string);
        }

        [Fact]
        public void AllowUpto255CharStreet()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Street, new string('*', 255));
        }

        [Fact]
        public void NotAllowOver255CharStreet()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Street, new string('*', 256));
        }

        [Fact]
        public void NotAllowNullCity()
        {
            validator.ShouldHaveValidationErrorFor(x => x.City, null as string);
        }

        [Fact]
        public void AllowUpto100CharCity()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.City, new string('*', 100));
        }

        [Fact]
        public void NotAllowOver100CharCity()
        {
            validator.ShouldHaveValidationErrorFor(x => x.City, new string('*', 101));
        }

        [Fact]
        public void NotAllowNullState()
        {
            validator.ShouldHaveValidationErrorFor(x => x.City, null as string);
        }

        [Fact]
        public void Allow2CharState()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.State, new string('*', 2));
        }
        [Fact]
        public void NotAllowUnder2CharState()
        {
            validator.ShouldHaveValidationErrorFor(x => x.State, new string('*', 1));
        }
        [Fact]
        public void NotAllowOver2CharState()
        {
            validator.ShouldHaveValidationErrorFor(x => x.State, new string('*', 3));
        }

        [Fact]
        public void NotAllowNullZip()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Zip, null as string);
        }

        [Fact]
        public void NotAllowAlphaZip() {
            validator.ShouldHaveValidationErrorFor(x => x.Zip, "A1234");
        }

        [Theory]
        [InlineData("12-34")]
        [InlineData("123456")]
        [InlineData("1234")]
        [InlineData("123456-789")]
        public void NotAllowWrongFormatZip(string zip)
        {
            validator.ShouldHaveValidationErrorFor(x => x.Zip, zip);
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("123456789")]
        [InlineData("12345-6789")]
        public void NotAllowRightFormatZip(string zip)
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Zip, zip);
        }
    }
}
