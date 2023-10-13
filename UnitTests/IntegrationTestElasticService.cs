namespace UnitTests
{
    /// <summary>
    /// Test works with real instance of elasticsearch
    /// </summary>
    [TestClass]
    public class IntegrationTestElasticService
    {
        //real elasticsearch hostname shall be provided here
        private const string ELASTIC_HOST_NAME = "http://10.100.102.165:9200";

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestMethodElasticCRUD()
        {
            IElasticService elasticService = new ElasticService(ELASTIC_HOST_NAME, "data-tube");

            var randomId = Guid.NewGuid().ToString();
            CompletedText expectedCompletedText = new CompletedText(randomId, "title1", "textData1", "author1");

            elasticService.Create(expectedCompletedText);
            var actualCompletedText = elasticService.Read(randomId);

            Assert.IsNotNull(actualCompletedText);
            Assert.IsTrue(expectedCompletedText.Equals(actualCompletedText));

            elasticService.Delete(randomId);

            try
            {
                elasticService.Read(randomId);
                Assert.Fail();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}