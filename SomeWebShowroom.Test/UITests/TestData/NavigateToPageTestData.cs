namespace SomeWebShowroom.Test.UITests.TestData
{
    using System.Collections;
    using System.Collections.Generic;
    public class NavigateToPageTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "", "Home Page"};
            yield return new object[] { "Identity/register", "Register"};
            yield return new object[] { "Identity/login", "Login" };
            yield return new object[] { "products", "Browse Products" };
            yield return new object[] { "products/details/2", "Details" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
