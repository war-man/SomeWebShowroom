namespace SomeWebShowroom.Test.Controllers.TestData
{
    using System.Collections;
    using System.Collections.Generic;
    public class ModelTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {"email", "Email", "Invalid email" };
            yield return new object[] { null, "Email", "Email is required" };         
           
            yield return new object[] { null, "Username", "Username is required" };
            yield return new object[] { "hejj", "Username", "Username is too short" };
            yield return new object[] { "sdsdsdsdsdsdsdsdsdsda", "Username", "Username is too long" };

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
