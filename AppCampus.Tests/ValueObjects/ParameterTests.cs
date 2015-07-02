using AppCampus.Domain.Models.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCampus.Tests.ValueObjects
{
    [TestFixture]
    [Category("Parameter")]
    class ParameterTests
    {
        //public Parameter make_Parameter(Guid parameterDefinitionId, string value)
        //{
        //    return Parameter.From(parameterDefinitionId, value);
        //}

        //[TestCase("value")]
        //[TestCase("a")]
        //[TestCase("1")]
        //public void Create_ValidParameter_NewParameter(string value)
        //{
        //    var parameterDefinitionId = Guid.NewGuid();

        //    var parameter = make_Parameter(parameterDefinitionId, value);

        //    Assert.AreEqual(parameter.ParameterDefinitionId, parameterDefinitionId);
        //    Assert.AreEqual(parameter.Value, value);
        //}

        //[TestCase("")]
        //[TestCase(null)]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void Create_InvalidParameter_ThrowException(string value)
        //{
        //    var parameterDefinitionId = Guid.NewGuid();

        //    make_Parameter(parameterDefinitionId, value);
        //}

        //[TestCase]
        //public void Comparison_SameParameter_IsEquals()
        //{
        //    Guid parameterDefinitionId = Guid.NewGuid();
        //    string value = "value";

        //    var parameter1 = make_Parameter(parameterDefinitionId, value);
        //    var parameter2 = make_Parameter(parameterDefinitionId, value);

        //    Assert.AreEqual(parameter1, parameter2);
        //    Assert.AreEqual(parameter2, parameter1);
        //}

        //[TestCase]
        //public void Comparison_DifferentParameter_IsNotEquals()
        //{
        //    var parameter1 = make_Parameter(Guid.NewGuid(), "b");
        //    var parameter2 = make_Parameter(Guid.NewGuid(), "a");

        //    Assert.AreNotEqual(parameter1, parameter2);
        //    Assert.AreNotEqual(parameter2, parameter1);
        //}
    }
}
